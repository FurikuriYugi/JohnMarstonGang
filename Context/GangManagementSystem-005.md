<!-- GangManagementSystem.cs lines 1001-1186 of 1186 -->
```csharp

    public GangManagementData ExportManagementData()
    {
        return new GangManagementData
        {
            Hideouts = hideouts.Values.ToList(),
            PendingRewards = pendingRewards.Values.ToList(),
            Milestones = milestones.Values.ToList(),
            ActiveHideoutId = ActiveHideoutId
        };
    }

    public void LoadFromData(GangManagementData data)
    {
        if (data == null)
        {
            return;
        }

        hideouts.Clear();
        pendingRewards.Clear();
        milestones.Clear();

        if (data.Hideouts != null)
        {
            foreach (Hideout hideout in data.Hideouts)
            {
                if (hideout == null ||
                    string.IsNullOrWhiteSpace(hideout.HideoutId))
                {
                    continue;
                }

                EnsureChest(hideout);
                hideouts[hideout.HideoutId] = hideout;
            }
        }

        if (data.PendingRewards != null)
        {
            foreach (PendingJobReward reward in data.PendingRewards)
            {
                if (reward == null ||
                    string.IsNullOrWhiteSpace(reward.RewardId))
                {
                    continue;
                }

                NormalizePendingReward(reward);
                pendingRewards[reward.RewardId] = reward;
            }
        }

        if (data.Milestones != null)
        {
            foreach (GangMilestone milestone in data.Milestones)
            {
                if (milestone == null ||
                    string.IsNullOrWhiteSpace(milestone.MilestoneId))
                {
                    continue;
                }

                if (milestone.UnlockedCapabilities == null)
                {
                    milestone.UnlockedCapabilities =
                        new Dictionary<string, string>();
                }

                milestones[milestone.MilestoneId] = milestone;
            }
        }

        ActiveHideoutId = hideouts.ContainsKey(data.ActiveHideoutId)
            ? data.ActiveHideoutId
            : null;
    }

    // Absorbed from GangManager; SaveRoot remains the repository's
    // persistence contract for the roster and gang-management data.
    public SaveRoot ExportSaveData()
    {
        return new SaveRoot
        {
            GangMembers = GetMembers(),
            GangManagement = ExportManagementData()
        };
    }

    public void ImportSaveData(SaveRoot data)
    {
        gangMembers.Clear();

        if (data == null)
        {
            LoadFromData(null);
            return;
        }

        if (data.GangMembers != null)
        {
            foreach (GangMember member in data.GangMembers)
            {
                if (member == null ||
                    string.IsNullOrWhiteSpace(member.MemberId))
                {
                    continue;
                }

                gangMembers[member.MemberId] = member;
            }
        }

        LoadFromData(data.GangManagement);
    }

    public void WriteToSaveRoot(SaveRoot data)
    {
        if (data == null)
        {
            throw new ArgumentNullException(nameof(data));
        }

        data.SaveVersion = Math.Max(data.SaveVersion, 6);
        data.GangMembers = GetMembers();
        data.GangManagement = ExportManagementData();
    }

    public void LoadFromSaveRoot(SaveRoot data)
    {
        ImportSaveData(data);
    }

    public string SaveState()
    {
        return JsonConvert.SerializeObject(
            ExportSaveData(),
            Formatting.Indented);
    }

    public void LoadState(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            ImportSaveData(new SaveRoot());
            return;
        }

        try
        {
            SaveRoot saveRoot =
                JsonConvert.DeserializeObject<SaveRoot>(json);
            if (saveRoot != null)
            {
                ImportSaveData(saveRoot);
                return;
            }
        }
        catch (JsonException)
        {
            // Try the legacy GangManager format below.
        }

        try
        {
            List<GangMember> legacyMembers =
                JsonConvert.DeserializeObject<List<GangMember>>(json);
            SaveRoot legacySave = new SaveRoot
            {
                GangMembers = legacyMembers ?? new List<GangMember>()
            };
            ImportSaveData(legacySave);
        }
        catch (JsonException)
        {
            // Leave the current in-memory state unchanged for invalid data.
        }
    }

    #endregion

    private static int Clamp(int value, int minimum, int maximum)
    {
        return Math.Max(minimum, Math.Min(maximum, value));
    }
}
```
