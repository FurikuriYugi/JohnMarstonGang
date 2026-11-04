using System.Collections.Generic;
using Newtonsoft.Json;

public class CommandSystem
{
    private readonly List<CommandData> _commands = new List<CommandData>();

    public List<CommandData> GetCommands()
    {
        return new List<CommandData>(_commands);
    }

    public CommandData GetCommand(string commandId)
    {
        if (string.IsNullOrWhiteSpace(commandId))
            return null;

        return _commands.Find(x => x != null && x.CommandId == commandId);
    }

    public void RegisterCommand(CommandData command)
    {
        if (command == null || string.IsNullOrWhiteSpace(command.CommandId))
            return;

        _commands.RemoveAll(x => x != null && x.CommandId == command.CommandId);
        _commands.Add(command);
    }

    public void RemoveCommand(string commandId)
    {
        _commands.RemoveAll(x => x != null && x.CommandId == commandId);
    }

    public string SaveState()
    {
        return JsonConvert.SerializeObject(_commands, Formatting.Indented);
    }

    public void LoadState(string json)
    {
        _commands.Clear();
        if (string.IsNullOrWhiteSpace(json))
            return;

        List<CommandData> commands = JsonConvert.DeserializeObject<List<CommandData>>(json);
        if (commands != null)
            _commands.AddRange(commands);
    }
}