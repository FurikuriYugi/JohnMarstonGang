using System.Collections.Generic;
using System;
using System.Linq;
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

    public List<CommandData> GetActiveCommands()
    {
        return _commands
            .Where(x => x != null && !x.IsCompleted)
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
            SquadId = squadId,
            TargetId = targetId,
            CommandType = commandType,
            CombatStyle = combatStyle,
            CurrentTask = currentTask,
            IsStealthMode = isStealthMode,
            IsFollowMode = isFollowMode,
            IsCompleted = false,
            Notes = new List<string>()
        };

        RegisterCommand(command);
        return command;
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

    public void CompleteCommand(string commandId)
    {
        CommandData command = GetCommand(commandId);
        if (command != null)
            command.IsCompleted = true;
    }

    public void ProcessActiveCommands(TacticalDecisionSystem tacticalDecisionSystem)
    {
        if (tacticalDecisionSystem == null)
            return;

        foreach (CommandData command in GetActiveCommands())
        {
            if (string.IsNullOrWhiteSpace(command.LinkedOrderId))
            {
                TacticalOrderData order = tacticalDecisionSystem.CreateOrder(
                    command.SquadId,
                    command.IssuerId,
                    command.TargetId,
                    command.CommandType,
                    command.CombatStyle,
                    command.CurrentTask,
                    command.IsStealthMode,
                    command.IsFollowMode);

                if (order != null)
                    command.LinkedOrderId = order.OrderId;
            }
        }
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
        {
            foreach (CommandData command in commands)
            {
                if (command == null || string.IsNullOrWhiteSpace(command.CommandId))
                    continue;

                if (command.Notes == null)
                    command.Notes = new List<string>();

                RegisterCommand(command);
            }
        }
    }
}