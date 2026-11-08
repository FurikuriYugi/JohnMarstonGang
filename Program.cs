using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Text;

internal static class Program
{
    private const int MaximumMarkdownLinesPerChunk = 700;
    private const string InventoryBaseName = "ScriptHookRDRNetAPI-Inventory";

    private static int Main(string[] args)
    {
        if (args.Length != 2 ||
            args[0] == "--help" ||
            args[0] == "-h")
        {
            PrintUsage();
            return args.Length == 0 ||
                   (args.Length == 1 &&
                    (args[0] == "--help" || args[0] == "-h"))
                ? 0
                : 1;
        }

        string assemblyPath = Path.GetFullPath(args[0]);
        string outputRoot = Path.GetFullPath(args[1]);

        if (!File.Exists(assemblyPath))
        {
            Console.Error.WriteLine(
                "API assembly not found: " + assemblyPath);
            return 1;
        }

        try
        {
            return Extract(assemblyPath, outputRoot);
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine(
                "Extraction failed: " + exception);
            return 1;
        }
    }

    private static int Extract(
        string assemblyPath,
        string outputRoot)
    {
        string assemblyDirectory =
            Path.GetDirectoryName(assemblyPath);

        AppDomain.CurrentDomain.AssemblyResolve +=
            delegate (object sender, ResolveEventArgs eventArgs)
            {
                string dependencyName =
                    new AssemblyName(eventArgs.Name).Name + ".dll";

                string dependencyPath = Path.Combine(
                    assemblyDirectory,
                    dependencyName);

                return File.Exists(dependencyPath)
                    ? Assembly.LoadFrom(dependencyPath)
                    : null;
            };

        Assembly assembly = Assembly.LoadFrom(assemblyPath);
        string assemblyHash = ComputeSha256(assemblyPath);
        List<string> warnings = new List<string>();

        Type[] exportedTypes = GetExportedTypes(
            assembly,
            warnings);

        List<ApiTypeRecord> typeRecords = new List<ApiTypeRecord>();

        foreach (Type type in exportedTypes
            .Where(item => item != null && item.IsVisible)
            .OrderBy(item => item.FullName, StringComparer.Ordinal))
        {
            ApiTypeRecord record = ReadType(type, warnings);
            typeRecords.Add(record);
        }

        AssemblyName assemblyName = assembly.GetName();
        FileVersionInfo fileVersion =
            FileVersionInfo.GetVersionInfo(assemblyPath);

        TargetFrameworkAttribute frameworkAttribute =
            assembly.GetCustomAttributes(
                    typeof(TargetFrameworkAttribute),
                    false)
                .Cast<TargetFrameworkAttribute>()
                .FirstOrDefault();

        ApiInventory inventory = new ApiInventory
        {
            ToolVersion = "1.0.0",
            AssemblyPath = assemblyPath,
            AssemblyName = assemblyName.Name,
            AssemblyVersion = assemblyName.Version == null
                ? null
                : assemblyName.Version.ToString(),
            FileVersion = fileVersion.FileVersion,
            ProductVersion = fileVersion.ProductVersion,
            TargetFramework = frameworkAttribute == null
                ? null
                : frameworkAttribute.FrameworkName,
            ModuleVersionId =
                assembly.ManifestModule.ModuleVersionId.ToString(),
            Sha256 = assemblyHash,
            ExtractedUtc = DateTime.UtcNow.ToString(
                "yyyy-MM-ddTHH:mm:ssZ",
                CultureInfo.InvariantCulture),
            MethodScope =
                "Public methods declared on each exported public type. " +
                "Inherited methods are listed under their declaring type.",
            PublicTypeCount = typeRecords.Count,
            PublicMethodCount = typeRecords.Sum(
                item => item.Methods.Count),
            Warnings = warnings,
            Types = typeRecords
        };

        string runDirectory = Path.Combine(
            outputRoot,
            "ScriptHookRDRNetAPI-" +
            assemblyHash.Substring(0, 12));

        Directory.CreateDirectory(runDirectory);

        string jsonPath = Path.Combine(
            runDirectory,
            InventoryBaseName + ".json");

        File.WriteAllText(
            jsonPath,
            JsonConvert.SerializeObject(
                inventory,
                Newtonsoft.Json.Formatting.Indented),
            new UTF8Encoding(false));

        List<MarkdownBlock> blocks =
            BuildMarkdownBlocks(inventory);

        List<MarkdownChunk> chunks =
            GroupMarkdownBlocks(blocks);

        WriteMarkdownChunks(
            chunks,
            runDirectory,
            assemblyHash);

        WriteIndex(
            inventory,
            chunks,
            runDirectory);

        Console.WriteLine("Assembly: " + assemblyPath);
        Console.WriteLine("Version: " + inventory.AssemblyVersion);
        Console.WriteLine("SHA-256: " + assemblyHash);
        Console.WriteLine("Public types: " + inventory.PublicTypeCount);
        Console.WriteLine("Public methods: " + inventory.PublicMethodCount);
        Console.WriteLine("Output folder: " + runDirectory);
        Console.WriteLine("JSON: " + jsonPath);
        Console.WriteLine(
            "Markdown chunks: " + chunks.Count);

        if (warnings.Count > 0)
        {
            Console.WriteLine(
                "Warnings: " + warnings.Count +
                " (see the JSON inventory)");
        }

        return 0;
    }

    private static Type[] GetExportedTypes(
        Assembly assembly,
        List<string> warnings)
    {
        try
        {
            return assembly.GetExportedTypes();
        }
        catch (ReflectionTypeLoadException exception)
        {
            foreach (Exception loaderException in
                exception.LoaderExceptions ?? new Exception[0])
            {
                if (loaderException != null)
                {
                    warnings.Add(
                        "Type loading warning: " +
                        loaderException.Message);
                }
            }

            return (exception.Types ?? new Type[0])
                .Where(type => type != null)
                .ToArray();
        }
    }

    private static ApiTypeRecord ReadType(
        Type type,
        List<string> warnings)
    {
        ApiTypeRecord record = new ApiTypeRecord
        {
            FullName = type.FullName ?? type.Name,
            Namespace = type.Namespace,
            Kind = GetTypeKind(type),
            BaseType = type.BaseType == null
                ? null
                : FormatType(type.BaseType),
            GenericParameters = type.IsGenericTypeDefinition
                ? type.GetGenericArguments()
                    .Select(argument => argument.Name)
                    .ToList()
                : new List<string>(),
            Methods = new List<ApiMethodRecord>(),
            EnumValues = ReadEnumValues(type)
        };

        try
        {
            const BindingFlags flags =
                BindingFlags.Public |
                BindingFlags.Static |
                BindingFlags.Instance |
                BindingFlags.DeclaredOnly;

            MethodInfo[] methods = type.GetMethods(flags);

            foreach (MethodInfo method in methods
                .Where(IsPublicApiMethod)
                .OrderBy(item => item.Name, StringComparer.Ordinal)
                .ThenBy(FormatMethodSignature, StringComparer.Ordinal))
            {
                record.Methods.Add(ReadMethod(method));
            }
        }
        catch (Exception exception)
        {
            warnings.Add(
                "Could not read methods for " +
                record.FullName +
                ": " +
                exception.Message);
        }

        return record;
    }

    private static bool IsPublicApiMethod(MethodInfo method)
    {
        if (!method.IsPublic)
        {
            return false;
        }

        // Exclude property/event accessors. Keep operators such as
        // op_Implicit because they are callable public API methods.
        return !method.IsSpecialName ||
               method.Name.StartsWith(
                   "op_",
                   StringComparison.Ordinal);
    }

    private static ApiMethodRecord ReadMethod(
        MethodInfo method)
    {
        ApiMethodRecord record = new ApiMethodRecord
        {
            Name = method.Name,
            DeclaringType = method.DeclaringType == null
                ? null
                : method.DeclaringType.FullName,
            IsStatic = method.IsStatic,
            ReturnType = FormatType(method.ReturnType),
            GenericParameters = method.IsGenericMethod
                ? method.GetGenericArguments()
                    .Where(argument => argument.IsGenericParameter)
                    .Select(argument => argument.Name)
                    .ToList()
                : new List<string>(),
            Parameters = new List<ApiParameterRecord>()
        };

        foreach (ParameterInfo parameter in method.GetParameters())
        {
            record.Parameters.Add(ReadParameter(parameter));
        }

        ObsoleteAttribute obsoleteAttribute =
            method.GetCustomAttributes(
                    typeof(ObsoleteAttribute),
                    false)
                .Cast<ObsoleteAttribute>()
                .FirstOrDefault();

        record.IsObsolete = obsoleteAttribute != null;
        record.ObsoleteMessage = obsoleteAttribute == null
            ? null
            : obsoleteAttribute.Message;

        record.Signature = FormatMethodSignature(method);

        return record;
    }

    private static ApiParameterRecord ReadParameter(
        ParameterInfo parameter)
    {
        Type parameterType = parameter.ParameterType;
        string passingMode = "value";

        if (parameterType.IsByRef)
        {
            parameterType = parameterType.GetElementType();

            if (parameter.IsOut)
            {
                passingMode = "out";
            }
            else if (parameter.IsIn)
            {
                passingMode = "in";
            }
            else
            {
                passingMode = "ref";
            }
        }

        bool isParams =
            parameter.GetCustomAttributes(
                    typeof(ParamArrayAttribute),
                    false)
                .Length > 0;

        string defaultValue = null;

        if (parameter.IsOptional)
        {
            defaultValue = GetDefaultValue(parameter);
        }

        return new ApiParameterRecord
        {
            Name = parameter.Name,
            Type = FormatType(parameterType),
            PassingMode = passingMode,
            IsParams = isParams,
            IsOptional = parameter.IsOptional,
            DefaultValue = defaultValue
        };
    }

    private static string GetDefaultValue(
        ParameterInfo parameter)
    {
        try
        {
            object value = parameter.DefaultValue;

            if (value == null)
            {
                return "null";
            }

            if (value == DBNull.Value ||
                value == Missing.Value)
            {
                return null;
            }

            if (value is string)
            {
                return "\"" +
                       ((string)value)
                           .Replace("\\", "\\\\")
                           .Replace("\"", "\\\"") +
                       "\"";
            }

            if (value is char)
            {
                return "'" +
                       value.ToString()
                           .Replace("\\", "\\\\")
                           .Replace("'", "\\'") +
                       "'";
            }

            return Convert.ToString(
                value,
                CultureInfo.InvariantCulture);
        }
        catch
        {
            return null;
        }
    }

    private static string FormatMethodSignature(
        MethodInfo method)
    {
        StringBuilder builder = new StringBuilder();

        builder.Append("public ");

        if (method.IsStatic)
        {
            builder.Append("static ");
        }

        builder.Append(FormatType(method.ReturnType));
        builder.Append(' ');

        if (method.DeclaringType != null)
        {
            builder.Append(FormatType(method.DeclaringType));
            builder.Append('.');
        }

        builder.Append(method.Name);

        if (method.IsGenericMethod)
        {
            Type[] genericArguments = method.GetGenericArguments()
                .Where(argument => argument.IsGenericParameter)
                .ToArray();

            if (genericArguments.Length > 0)
            {
                builder.Append('<');
                builder.Append(
                    string.Join(
                        ", ",
                        genericArguments.Select(
                            argument => argument.Name)));
                builder.Append('>');
            }
        }

        builder.Append('(');

        ParameterInfo[] parameters = method.GetParameters();

        for (int index = 0; index < parameters.Length; index++)
        {
            if (index > 0)
            {
                builder.Append(", ");
            }

            builder.Append(FormatParameter(parameters[index]));
        }

        builder.Append(')');

        return builder.ToString();
    }

    private static string FormatParameter(
        ParameterInfo parameter)
    {
        Type parameterType = parameter.ParameterType;
        string prefix = string.Empty;

        if (parameterType.IsByRef)
        {
            parameterType = parameterType.GetElementType();

            if (parameter.IsOut)
            {
                prefix = "out ";
            }
            else if (parameter.IsIn)
            {
                prefix = "in ";
            }
            else
            {
                prefix = "ref ";
            }
        }

        if (parameter.GetCustomAttributes(
                typeof(ParamArrayAttribute),
                false).Length > 0)
        {
            prefix = "params ";
        }

        StringBuilder builder = new StringBuilder();
        builder.Append(prefix);
        builder.Append(FormatType(parameterType));
        builder.Append(' ');
        builder.Append(parameter.Name ?? "arg");

        if (parameter.IsOptional)
        {
            string defaultValue = GetDefaultValue(parameter);

            if (defaultValue != null)
            {
                builder.Append(" = ");
                builder.Append(defaultValue);
            }
        }

        return builder.ToString();
    }

    private static string FormatType(Type type)
    {
        if (type == null)
        {
            return "void";
        }

        if (type.IsByRef)
        {
            return FormatType(type.GetElementType());
        }

        if (type.IsPointer)
        {
            return FormatType(type.GetElementType()) + "*";
        }

        if (type.IsArray)
        {
            int rank = type.GetArrayRank();
            string commas = new string(',', Math.Max(0, rank - 1));

            return FormatType(type.GetElementType()) +
                   "[" +
                   commas +
                   "]";
        }

        if (type.IsGenericParameter)
        {
            return type.Name;
        }

        if (!type.IsGenericType)
        {
            return (type.FullName ?? type.Name).Replace('+', '.');
        }

        Type genericDefinition = type.GetGenericTypeDefinition();
        string genericName =
            (genericDefinition.FullName ?? genericDefinition.Name)
                .Replace('+', '.');

        int tickIndex = genericName.IndexOf('`');

        if (tickIndex >= 0)
        {
            genericName = genericName.Substring(0, tickIndex);
        }

        string[] arguments = type.GetGenericArguments()
            .Select(FormatType)
            .ToArray();

        return genericName +
               "<" +
               string.Join(", ", arguments) +
               ">";
    }

    private static string GetTypeKind(Type type)
    {
        if (type.IsEnum)
        {
            return "enum";
        }

        if (typeof(MulticastDelegate).IsAssignableFrom(type))
        {
            return "delegate";
        }

        if (type.IsInterface)
        {
            return "interface";
        }

        if (type.IsValueType)
        {
            return "struct";
        }

        return "class";
    }

    private static List<ApiEnumValueRecord> ReadEnumValues(
        Type type)
    {
        List<ApiEnumValueRecord> values =
            new List<ApiEnumValueRecord>();

        if (!type.IsEnum)
        {
            return values;
        }

        FieldInfo[] fields = type.GetFields(
            BindingFlags.Public |
            BindingFlags.Static);

        foreach (FieldInfo field in fields
            .OrderBy(item => item.Name, StringComparer.Ordinal))
        {
            object rawValue = field.GetRawConstantValue();

            values.Add(new ApiEnumValueRecord
            {
                Name = field.Name,
                Value = Convert.ToString(
                    rawValue,
                    CultureInfo.InvariantCulture)
            });
        }

        return values;
    }

    private static List<MarkdownBlock> BuildMarkdownBlocks(
        ApiInventory inventory)
    {
        List<MarkdownBlock> blocks =
            new List<MarkdownBlock>();

        StringBuilder metadata = new StringBuilder();
        metadata.AppendLine("# ScriptHookRDRNetAPI public API inventory");
        metadata.AppendLine();
        metadata.AppendLine("- Assembly: `" + inventory.AssemblyName + "`");
        metadata.AppendLine("- Assembly version: `" +
                             inventory.AssemblyVersion + "`");
        metadata.AppendLine("- File version: `" +
                             inventory.FileVersion + "`");
        metadata.AppendLine("- Target framework: `" +
                             inventory.TargetFramework + "`");
        metadata.AppendLine("- SHA-256: `" + inventory.Sha256 + "`");
        metadata.AppendLine("- Public types: " +
                             inventory.PublicTypeCount);
        metadata.AppendLine("- Public methods: " +
                             inventory.PublicMethodCount);
        metadata.AppendLine();
        metadata.AppendLine("Methods are listed under the type that declares them.");
        metadata.AppendLine("This inventory records signatures, not runtime behavior.");
        metadata.AppendLine();

        blocks.Add(new MarkdownBlock
        {
            TypeName = "(inventory metadata)",
            MethodCount = 0,
            Text = metadata.ToString()
        });

        foreach (ApiTypeRecord type in inventory.Types)
        {
            StringBuilder block = new StringBuilder();

            block.AppendLine("## `" + type.FullName + "`");
            block.AppendLine();
            block.AppendLine("- Kind: " + type.Kind);

            if (!string.IsNullOrEmpty(type.BaseType))
            {
                block.AppendLine("- Base type: `" + type.BaseType + "`");
            }

            if (type.GenericParameters.Count > 0)
            {
                block.AppendLine(
                    "- Generic parameters: `" +
                    string.Join(", ", type.GenericParameters) +
                    "`");
            }

            block.AppendLine();

            if (type.Methods.Count == 0)
            {
                block.AppendLine("*No public methods declared.*");
                block.AppendLine();
            }
            else
            {
                foreach (ApiMethodRecord method in type.Methods)
                {
                    block.AppendLine("- `" + method.Signature + "`");

                    if (method.IsObsolete)
                    {
                        block.AppendLine(
                            "  - Obsolete" +
                            (string.IsNullOrEmpty(method.ObsoleteMessage)
                                ? "."
                                : ": " + method.ObsoleteMessage));
                    }
                }

                block.AppendLine();
            }

            if (type.EnumValues.Count > 0)
            {
                block.AppendLine("Enum values:");

                foreach (ApiEnumValueRecord value in type.EnumValues)
                {
                    block.AppendLine(
                        "- `" + value.Name + "` = `" + value.Value + "`");
                }

                block.AppendLine();
            }

            blocks.Add(new MarkdownBlock
            {
                TypeName = type.FullName,
                MethodCount = type.Methods.Count,
                Text = block.ToString()
            });
        }

        return blocks;
    }

    private static List<MarkdownChunk> GroupMarkdownBlocks(
        List<MarkdownBlock> blocks)
    {
        List<MarkdownChunk> chunks =
            new List<MarkdownChunk>();

        MarkdownChunk current = new MarkdownChunk();

        foreach (MarkdownBlock block in blocks)
        {
            int blockLineCount = CountLines(block.Text);

            if (current.Blocks.Count > 0 &&
                current.LineCount + blockLineCount >
                MaximumMarkdownLinesPerChunk)
            {
                chunks.Add(current);
                current = new MarkdownChunk();
            }

            current.Blocks.Add(block);
            current.LineCount += blockLineCount;
            current.MethodCount += block.MethodCount;
        }

        if (current.Blocks.Count > 0)
        {
            chunks.Add(current);
        }

        return chunks;
    }

    private static void WriteMarkdownChunks(
        List<MarkdownChunk> chunks,
        string outputDirectory,
        string assemblyHash)
    {
        for (int index = 0; index < chunks.Count; index++)
        {
            MarkdownChunk chunk = chunks[index];
            string fileName = InventoryBaseName +
                              "-" +
                              (index + 1).ToString(
                                  "D3",
                                  CultureInfo.InvariantCulture) +
                              ".md";

            StringBuilder builder = new StringBuilder();

            builder.AppendLine(
                "<!-- " +
                InventoryBaseName +
                " chunk " +
                (index + 1) +
                " of " +
                chunks.Count +
                "; methods=" +
                chunk.MethodCount +
                "; sha256=" +
                assemblyHash +
                " -->");

            foreach (MarkdownBlock block in chunk.Blocks)
            {
                builder.Append(block.Text);
            }

            string filePath = Path.Combine(
                outputDirectory,
                fileName);

            File.WriteAllText(
                filePath,
                builder.ToString(),
                new UTF8Encoding(false));

            chunk.FileName = fileName;
        }
    }

    private static void WriteIndex(
        ApiInventory inventory,
        List<MarkdownChunk> chunks,
        string outputDirectory)
    {
        StringBuilder builder = new StringBuilder();

        builder.AppendLine("# ScriptHookRDRNetAPI inventory index");
        builder.AppendLine();
        builder.AppendLine("- Assembly: `" + inventory.AssemblyName + "`");
        builder.AppendLine("- Version: `" + inventory.AssemblyVersion + "`");
        builder.AppendLine("- SHA-256: `" + inventory.Sha256 + "`");
        builder.AppendLine("- JSON: `" + InventoryBaseName + ".json`");
        builder.AppendLine();

        for (int index = 0; index < chunks.Count; index++)
        {
            MarkdownChunk chunk = chunks[index];

            builder.Append("- [");
            builder.Append(chunk.FileName);
            builder.Append("](");
            builder.Append(chunk.FileName);
            builder.Append(") — ");
            builder.Append(chunk.Blocks.Count);
            builder.Append(" types, ");
            builder.Append(chunk.MethodCount);
            builder.AppendLine(" methods");
        }

        string indexPath = Path.Combine(
            outputDirectory,
            InventoryBaseName + "-INDEX.md");

        File.WriteAllText(
            indexPath,
            builder.ToString(),
            new UTF8Encoding(false));
    }

    private static int CountLines(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return 0;
        }

        using (StringReader reader = new StringReader(text))
        {
            int count = 0;

            while (reader.ReadLine() != null)
            {
                count++;
            }

            return count;
        }
    }

    private static string ComputeSha256(string path)
    {
        using (SHA256 sha256 = SHA256.Create())
        using (FileStream stream = File.OpenRead(path))
        {
            byte[] hash = sha256.ComputeHash(stream);

            return BitConverter.ToString(hash)
                .Replace("-", string.Empty)
                .ToLowerInvariant();
        }
    }

    private static void PrintUsage()
    {
        Console.WriteLine(
            "Usage: ApiInventoryExtractor.exe " +
            "<path-to-ScriptHookRDRNetAPI.dll> " +
            "<output-root>");
        Console.WriteLine();
        Console.WriteLine(
            "Example:");
        Console.WriteLine(
            "  ApiInventoryExtractor.exe " +
            "\"C:\\Games\\RDR2\\ScriptHookRDRNetAPI.dll\" " +
            "\"C:\\Mods\\JohnMarstonGang\\Context\\ApiInventory\"");
    }
}

internal sealed class ApiInventory
{
    public string ToolVersion { get; set; }
    public string AssemblyPath { get; set; }
    public string AssemblyName { get; set; }
    public string AssemblyVersion { get; set; }
    public string FileVersion { get; set; }
    public string ProductVersion { get; set; }
    public string TargetFramework { get; set; }
    public string ModuleVersionId { get; set; }
    public string Sha256 { get; set; }
    public string ExtractedUtc { get; set; }
    public string MethodScope { get; set; }
    public int PublicTypeCount { get; set; }
    public int PublicMethodCount { get; set; }
    public List<string> Warnings { get; set; }
    public List<ApiTypeRecord> Types { get; set; }
}

internal sealed class ApiTypeRecord
{
    public string FullName { get; set; }
    public string Namespace { get; set; }
    public string Kind { get; set; }
    public string BaseType { get; set; }
    public List<string> GenericParameters { get; set; }
    public List<ApiEnumValueRecord> EnumValues { get; set; }
    public List<ApiMethodRecord> Methods { get; set; }
}

internal sealed class ApiMethodRecord
{
    public string Name { get; set; }
    public string DeclaringType { get; set; }
    public bool IsStatic { get; set; }
    public string ReturnType { get; set; }
    public List<string> GenericParameters { get; set; }
    public List<ApiParameterRecord> Parameters { get; set; }
    public string Signature { get; set; }
    public bool IsObsolete { get; set; }
    public string ObsoleteMessage { get; set; }
}

internal sealed class ApiParameterRecord
{
    public string Name { get; set; }
    public string Type { get; set; }
    public string PassingMode { get; set; }
    public bool IsParams { get; set; }
    public bool IsOptional { get; set; }
    public string DefaultValue { get; set; }
}

internal sealed class ApiEnumValueRecord
{
    public string Name { get; set; }
    public string Value { get; set; }
}

internal sealed class MarkdownBlock
{
    public string TypeName { get; set; }
    public int MethodCount { get; set; }
    public string Text { get; set; }
}

internal sealed class MarkdownChunk
{
    public string FileName { get; set; }
    public int LineCount { get; set; }
    public int MethodCount { get; set; }
    public List<MarkdownBlock> Blocks { get; private set; }

    public MarkdownChunk()
    {
        Blocks = new List<MarkdownBlock>();
    }
}