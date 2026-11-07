<!-- Main lines 251-500 of 1135 -->
```csharp

        string[] likelyNames =
        {
            "Handle",
            "PromptHandle",
            "NativeHandle",
            "_handle",
            "m_handle",
            "_promptHandle",
            "m_promptHandle"
        };

        Type promptType = prompt.GetType();

        foreach (string name in likelyNames)
        {
            PropertyInfo property = promptType.GetProperty(name, flags);

            if (property != null &&
                property.GetIndexParameters().Length == 0)
            {
                try
                {
                    int value = Convert.ToInt32(
                        property.GetValue(prompt, null));

                    if (value >= 0)
                    {
                        return value;
                    }
                }
                catch
                {
                    // Continue trying other likely handle members.
                }
            }

            FieldInfo field = promptType.GetField(name, flags);

            if (field != null)
            {
                try
                {
                    int value = Convert.ToInt32(field.GetValue(prompt));

                    if (value >= 0)
                    {
                        return value;
                    }
                }
                catch
                {
                    // Continue trying other likely handle members.
                }
            }
        }

        /*
         * Fallback for the prototype: inspect integer members if the API
         * exposes the native prompt handle under an undocumented name.
         */
        foreach (FieldInfo field in promptType.GetFields(flags))
        {
            if (field.FieldType != typeof(int) &&
                field.FieldType != typeof(uint))
            {
                continue;
            }

            try
            {
                int value = Convert.ToInt32(field.GetValue(prompt));

                if (value >= 0)
                {
                    return value;
                }
            }
            catch
            {
                // Ignore unrelated or inaccessible fields.
            }
        }

        foreach (PropertyInfo property in promptType.GetProperties(flags))
        {
            if (property.GetIndexParameters().Length != 0 ||
                (property.PropertyType != typeof(int) &&
                 property.PropertyType != typeof(uint)))
            {
                continue;
            }

            try
            {
                int value = Convert.ToInt32(
                    property.GetValue(prompt, null));

                if (value >= 0)
                {
                    return value;
                }
            }
            catch
            {
                // Ignore unrelated or inaccessible properties.
            }
        }

        return -1;
    }

    private void ClearRecruitmentPrompt()
    {
        if (_recruitmentPrompt != null)
        {
            _recruitmentPrompt.Enabled = false;
            _recruitmentPrompt.Visible = false;

            if (_recruitmentPromptAttached &&
                _recruitmentPromptNativeHandle >= 0 &&
                _recruitmentPromptGroup >= 0)
            {
                /*
                 * Test candidate for the v2.2.0 native wrapper name and
                 * parameter order. Remove the action prompt from its
                 * previous target group before discarding its state.
                 */
                RDR2.Native.UI.PROMPT_REMOVE_GROUP(
                    _recruitmentPromptNativeHandle,
                    _recruitmentPromptGroup);
            }
        }

        _recruitmentPrompt = null;
        _recruitmentPromptTargetHandle = -1;
        _recruitmentPromptGroup = -1;
        _recruitmentPromptNativeHandle = -1;
        _recruitmentPromptAttached = false;
        _promptHandleFailureReported = false;
    }

    private Ped GetInteractionLockonTargetPed()
    {
        return Game.Player.GetInteractionLockonTargetEntity() as Ped;
    }

    private void ToggleCompanionFollowState(
        string companionId,
        Ped companionPed)
    {
        if (string.IsNullOrWhiteSpace(companionId) ||
            companionPed == null ||
            !companionPed.Exists())
        {
            return;
        }

        WorldEntityData entity =
            _entityRegistry.GetEntity(companionId);

        CompanionData companion =
            _companionSystem.GetCompanion(companionId);

        bool shouldFollow = entity != null
            ? !entity.IsFollowingPlayer
            : companion != null && !companion.IsFollowingPlayer;

        Ped playerPed = shouldFollow ? Game.Player.Character : null;

        if (shouldFollow &&
            (playerPed == null || !playerPed.Exists()))
        {
            return;
        }

        string task = shouldFollow ? "FollowPlayer" : "Stay";

        if (entity != null)
        {
            entity.IsFollowingPlayer = shouldFollow;
            entity.CurrentTask = task;
        }

        if (companion != null)
        {
            companion.IsFollowingPlayer = shouldFollow;
            companion.CurrentTask = task;
        }

        _gangManagementSystem.ApplyTacticalContext(
            companionId,
            companion != null ? companion.CurrentCombatStyle : null,
            task,
            shouldFollow);

        companionPed.AlwaysKeepTask = true;

        if (shouldFollow)
        {
            companionPed.Task.FollowToOffsetOfEntity(
                playerPed,
                new Vector3(0f, -2f, 0f),
                2f,
                -1,
                3f,
                true);
        }
        else
        {
            companionPed.Task.StandStill(-1);
        }

        SaveAll();

        RDR2.UI.Screen.PrintSubtitle(
            shouldFollow
                ? "Gang member is following you."
                : "Gang member will stay here.");
    }

    private void DismissCompanion(
        string companionId,
        Ped companionPed)
    {
        if (string.IsNullOrWhiteSpace(companionId) ||
            companionPed == null ||
            !companionPed.Exists())
        {
            return;
        }

        companionPed.Task.ClearTasks(true);
        companionPed.AlwaysKeepTask = false;

        _runtimeCompanionPeds.Remove(companionId);
        _companionSystem.RemoveCompanion(companionId);
        _entityRegistry.RemoveEntity(companionId);

        if (_gangManagementSystem.MemberExists(companionId))
        {
            _gangManagementSystem.RemoveMember(companionId);
        }

        SaveAll();
        RDR2.UI.Screen.PrintSubtitle("Gang member dismissed.");
    }

    private bool IsRecruitmentCandidate(Ped ped)
    {
```
