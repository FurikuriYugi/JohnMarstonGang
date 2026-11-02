using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class InteractionHookSystem
{
    private readonly string _savePath;
    private readonly List<InteractionHook> _hooks = new List<InteractionHook>();

    public bool Enabled { get; set; } = true;
    public int InteractionRange { get; set; } = 3;
    public int SafeInteractionRange { get; set; } = 2;
    public int DoorInteractionRange { get; set; } = 2;

    public IReadOnlyList<InteractionHook> Hooks => _hooks;

    public InteractionHookSystem(string saveFileName = "InteractionHookSystem.json")
    {
        _savePath = Path.Combine("scripts", saveFileName);
        Load();
    }

    public void RegisterDoor(string hookId, string entityModel, string promptText = "Open Door")
    {
        RegisterHook(hookId, HookType.Door, entityModel, promptText);
    }

    public void RegisterSafe(string hookId, string entityModel, string promptText = "Inspect Safe")
    {
        RegisterHook(hookId, HookType.Safe, entityModel, promptText);
    }

    public void RegisterHook(string hookId, HookType type, string entityModel, string promptText)
    {
        if (string.IsNullOrWhiteSpace(hookId) || string.IsNullOrWhiteSpace(entityModel))
            return;

        var existing = _hooks.FirstOrDefault(x => x.HookId == hookId);
        if (existing == null)
        {
            _hooks.Add(new InteractionHook
            {
                HookId = hookId,
                Type = type,
                EntityModel = entityModel,
                PromptText = string.IsNullOrWhiteSpace(promptText) ? DefaultPrompt(type) : promptText,
                Enabled = true,
                CreatedUtc = DateTime.UtcNow
            });
        }
        else
        {
            existing.Type = type;
            existing.EntityModel = entityModel;
            existing.PromptText = string.IsNullOrWhiteSpace(promptText) ? DefaultPrompt(type) : promptText;
            existing.Enabled = true;
        }

        Save();
    }

    public bool RemoveHook(string hookId)
    {
        if (string.IsNullOrWhiteSpace(hookId))
            return false;

        bool removed = _hooks.RemoveAll(x => x.HookId == hookId) > 0;
        if (removed)
            Save();

        return removed;
    }

    public void DisableHook(string hookId)
    {
        var hook = _hooks.FirstOrDefault(x => x.HookId == hookId);
        if (hook == null)
            return;

        hook.Enabled = false;
        Save();
    }

    public void EnableHook(string hookId)
    {
        var hook = _hooks.FirstOrDefault(x => x.HookId == hookId);
        if (hook == null)
            return;

        hook.Enabled = true;
        Save();
    }

    public IEnumerable<InteractionHook> GetNearbyHooks(Vector3 playerPosition, Func<InteractionHook, Vector3?> resolveHookPosition)
    {
        if (!Enabled || resolveHookPosition == null)
            yield break;

        foreach (var hook in _hooks)
        {
            if (hook == null || !hook.Enabled)
                continue;

            var pos = resolveHookPosition(hook);
            if (!pos.HasValue)
                continue;

            float range = hook.Type == HookType.Safe ? SafeInteractionRange : DoorInteractionRange;
            if (Vector3.Distance(playerPosition, pos.Value) <= range)
                yield return hook;
        }
    }

    public bool TryInteract(string hookId, out string resultMessage)
    {
        resultMessage = "No interaction found.";

        if (string.IsNullOrWhiteSpace(hookId))
            return false;

        var hook = _hooks.FirstOrDefault(x => x.HookId == hookId);
        if (hook == null || !hook.Enabled)
        {
            resultMessage = "That interaction is unavailable.";
            return false;
        }

        if (hook.Type == HookType.Door)
        {
            resultMessage = "Door interaction triggered.";
            return true;
        }

        if (hook.Type == HookType.Safe)
        {
            resultMessage = "Safe interaction triggered.";
            return true;
        }

        resultMessage = "Interaction triggered.";
        return true;
    }

    public void Clear()
    {
        _hooks.Clear();
        Save();
    }

    public void Save()
    {
        try
        {
            Directory.CreateDirectory("scripts");
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_savePath, JsonSerializer.Serialize(_hooks, options));
        }
        catch
        {
        }
    }

    public void Load()
    {
        try
        {
            if (!File.Exists(_savePath))
                return;

            var json = File.ReadAllText(_savePath);
            var loaded = JsonSerializer.Deserialize<List<InteractionHook>>(json);

            _hooks.Clear();
            if (loaded != null)
                _hooks.AddRange(loaded.Where(x => !string.IsNullOrWhiteSpace(x.HookId) && !string.IsNullOrWhiteSpace(x.EntityModel)));
        }
        catch
        {
            _hooks.Clear();
        }
    }

    private string DefaultPrompt(HookType type)
    {
        switch (type)
        {
            case HookType.Safe:
                return "Inspect Safe";
            case HookType.Door:
                return "Open Door";
            default:
                return "Interact";
        }
    }
}

public class InteractionHook
{
    public string HookId { get; set; }
    public HookType Type { get; set; }
    public string EntityModel { get; set; }
    public string PromptText { get; set; }
    public bool Enabled { get; set; }
    public DateTime CreatedUtc { get; set; }
}

public enum HookType
{
    Door,
    Safe,
    Generic
}