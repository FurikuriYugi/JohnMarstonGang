using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

public class CommandSystem
{
    private readonly List<CommandData> _commands =
        new List<CommandData>();

    public List<CommandData> GetCommands()
    {
        return new List<CommandData>(_commands);
    }

    public CommandData GetCommand(string commandId)
    {
        if (string.IsNullOrWhiteSpace(commandId))
        {
            return null;
        }

        return _commands.Find(command =>
            command != null &&
            string.Equals(
                command.CommandId,
                commandId,
                StringComparison.Ordinal));
    }

    public List<CommandData> GetActiveCommands()
    {
        return _commands
            .Where(command =>
                command != null &&
                !command.IsCompleted)
            .ToList();
    }

    public CommandData CreateCommand(
        string issuerId,
        string squadId,
        string targetId,
        string commandType,
        string combatStyle,
        string currentTask,
        bool isStealthMode,
        bool isFollowMode)
    {
        CommandData command = new CommandData
        {
            CommandId = Guid.NewGuid().ToString("N"),
            IssuerId = issuerId,
            CommandType = commandType,
            TargetIds = string.IsNullOrWhiteSpace(targetId)
                ? new List<string>()
                : new List<string> { targetId },
            Parameter = JsonConvert.SerializeObject(
                new TacticalCommandParameters
                {
                    SquadId = squadId,
                    CombatStyle = combatStyle,
                    CurrentTask = currentTask,
                    IsStealthMode = isStealthMode,
                    IsFollowMode = isFollowMode
                }),
            IsCompleted = false
        };

        RegisterCommand(command);
        return command;
    }

    public void RegisterCommand(CommandData command)
    {
        if (command == null ||
            string.IsNullOrWhiteSpace(command.CommandId))
        {
            return;
        }

        if (command.TargetIds == null)
        {
            command.TargetIds = new List<string>();
        }

        _commands.RemoveAll(existing =>
            existing != null &&
            string.Equals(
                existing.CommandId,
                command.CommandId,
                StringComparison.Ordinal));

        _commands.Add(command);
    }

    public void RemoveCommand(string commandId)
    {
        if (string.IsNullOrWhiteSpace(commandId))
        {
            return;
        }

        _commands.RemoveAll(command =>
            command != null &&
            string.Equals(
                command.CommandId,
                commandId,
                StringComparison.Ordinal));
    }

    public void CompleteCommand(string commandId)
    {
        CommandData command = GetCommand(commandId);

        if (command != null)
        {
            command.IsCompleted = true;
        }
    }

    public void ProcessActiveCommands(
        TacticalDecisionSystem tacticalDecisionSystem)
    {
        if (tacticalDecisionSystem == null)
        {
            return;
        }

        foreach (CommandData command in GetActiveCommands())
        {
            TacticalCommandParameters parameters =
                DeserializeParameters(command.Parameter);

            string targetId = command.TargetIds == null ||
                              command.TargetIds.Count == 0
                ? null
                : command.TargetIds[0];

            TacticalOrderData order =
                tacticalDecisionSystem.CreateOrder(
                    parameters.SquadId,
                    command.IssuerId,
                    targetId,
                    command.CommandType,
                    parameters.CombatStyle,
                    parameters.CurrentTask,
                    parameters.IsStealthMode,
                    parameters.IsFollowMode);

            if (order != null)
            {
                command.IsCompleted = true;
            }
        }
    }

    public string SaveState()
    {
        return JsonConvert.SerializeObject(
            _commands,
            Formatting.Indented);
    }

    public void LoadState(string json)
    {
        _commands.Clear();

        if (string.IsNullOrWhiteSpace(json))
        {
            return;
        }

        List<CommandData> commands;

        try
        {
            commands =
                JsonConvert.DeserializeObject<List<CommandData>>(json);
        }
        catch (JsonException)
        {
            return;
        }

        if (commands == null)
        {
            return;
        }

        foreach (CommandData command in commands)
        {
            RegisterCommand(command);
        }
    }

    private static TacticalCommandParameters DeserializeParameters(
        string parameter)
    {
        if (string.IsNullOrWhiteSpace(parameter))
        {
            return new TacticalCommandParameters();
        }

        try
        {
            TacticalCommandParameters parameters =
                JsonConvert.DeserializeObject<TacticalCommandParameters>(
                    parameter);

            if (parameters != null)
            {
                return parameters;
            }
        }
        catch (JsonException)
        {
            // Older or generic commands may have a plain-text parameter.
        }

        return new TacticalCommandParameters
        {
            CurrentTask = parameter
        };
    }

    private sealed class TacticalCommandParameters
    {
        public string SquadId { get; set; }
        public string CombatStyle { get; set; }
        public string CurrentTask { get; set; }
        public bool IsStealthMode { get; set; }
        public bool IsFollowMode { get; set; }
    }
}