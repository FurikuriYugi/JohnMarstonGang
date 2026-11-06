<!-- ScriptHookRDRNetAPI lines 8251-8500 of 30005 -->
```csharp
            Washing player's face/hands now
            _CLEAR_PED_E* - _CLEAR_PED_L*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.WAS_PED_SKELETON_UPDATED(System.Int32)">
            <summary>
            Despite this function's name, it simply returns whether the specified handle is a Ped.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.GET_PED_BONE_COORDS(System.Int32,System.Int32,System.Single,System.Single,System.Single)">
            <summary>
            Gets the position of the specified bone of the specified ped.
            
            ped: The ped to get the position of a bone from.
            boneId: The ID of the bone to get the position from. This is NOT the index.
            offsetX: The X-component of the offset to add to the position relative to the bone's rotation.
            offsetY: The Y-component of the offset to add to the position relative to the bone's rotation.
            offsetZ: The Z-component of the offset to add to the position relative to the bone's rotation.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.ADD_SCENARIO_BLOCKING_AREA(System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Boolean,System.Int32)">
            <summary>
            blockingFlags: https://github.com/Halen84/RDR3-Native-Flags-And-Enums/tree/main/eScenarioBlockingFlags
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._ADD_SCENARIO_BLOCKING_VOLUME(System.Int32,System.Boolean,System.Int32)">
            <summary>
            flag: see ADD_SCENARIO_BLOCKING_AREA
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.IS_PED_USING_SCENARIO_HASH(System.Int32,System.UInt32)">
            <summary>
            Equivalent to IS_PED_USING_SCENARIO from V but takes a hash instead of a string.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._CAN_PED_USE_SCENARIO_POINT(System.Int32,System.Int32,System.UInt64,System.UInt64,System.UInt64)">
            <summary>
            p2 is always 0, p3 is always 0, p4 is always 1
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x1148F706CF4EBDDA(System.Int32,System.UInt32,System.Int32)">
            <summary>
            _CAN_PED_SEE* - _CAN_PED_USE_(SCENARIO_HASH?)*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.TOGGLE_SCENARIO_PED_COWER_IN_PLACE(System.Int32,System.Boolean)">
            <summary>
            If toggle is true, when the ped is using a scenario he will stop it and become scared
            If toggle is false, the ped will not be scared anymore and continue his scenario
            
            Old name: _SET_PED_SCARED_WHEN_USING_SCENARIO
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.SET_PED_SHOULD_PLAY_DIRECTED_NORMAL_SCENARIO_EXIT(System.Int32,System.Single,System.Single,System.Single)">
            <summary>
            Old name: _SET_PED_SHOULD_PLAY_DIRECTED_SCENARIO_EXIT
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.SET_PED_SHOULD_PLAY_FLEE_SCENARIO_EXIT(System.Int32,System.Single,System.Single,System.Single,System.Int32)">
            <summary>
            lookIntensity:
            0 - REACT_LOOK_NONE
            1 - REACT_LOOK_LOW
            2 - REACT_LOOK_MEDIUM
            3 - REACT_LOOK_HIGH
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.SET_PED_SHOULD_PLAY_COMBAT_SCENARIO_EXIT(System.Int32,System.Single,System.Single,System.Single,System.Int32)">
            <summary>
            lookIntensity: see SET_PED_SHOULD_PLAY_FLEE_SCENARIO_EXIT
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.SET_PED_SHOULD_PLAY_EMOTIONAL_SCENARIO_EXIT(System.Int32,System.Single,System.Single,System.Single,System.Int32,System.Boolean)">
            <summary>
            lookIntensity: see SET_PED_SHOULD_PLAY_FLEE_SCENARIO_EXIT
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.SET_PED_SHOULD_PLAY_QUICK_SCENARIO_EXIT(System.Int32,System.Single,System.Single,System.Single,System.Int32,System.Boolean)">
            <summary>
            lookIntensity: see SET_PED_SHOULD_PLAY_FLEE_SCENARIO_EXIT
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._REQUEST_PED_FACIAL_MOOD_THIS_FRAME(System.Int32,System.UInt32,System.Int32)">
            <summary>
            Hashes: MOODAGITATED, MOODAIMING, MOODANGRY, MOODBITCHY, MOODCONCENTRATION, MOODCONCENTRATIONEXTREME, MOODCURIOUS, MOODDEAD, MOODDEFUSE, MOODEXERTIONEXTREME, MOODEXERTIONMEDIUM, MOODEXERTIONMILD, MOODHAPPY, MOODINJUREDMILD, MOODINTIMIDATED, MOODKNOCKEDOUT, MOODNERVOUS, MOODNORMAL, MOODNORMALNEUTRAL, MOODPANIC, MOODSCARED, MOODSHOCKED, MOODSLEEPING, MOODSMUG, MOODTIRED
            Params: p2 = 6 in R* Scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x7EDB3C766B0D073F(System.Int32)">
            <summary>
            Only used in R* Script net_moonshine_property
            _A* - _B*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x4F63433CE3C08230(System.Int32,System.Boolean)">
            <summary>
            Only used in R* Script shop_harriet
            _SET_PED_F*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._SET_PED_CAN_UNK_BODYPART_IK(System.Int32,System.Boolean)">
            <summary>
            _SET_PED_CAN_(?)_IK*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xCD9E5F94A2F38683(System.Int32,System.Boolean)">
            <summary>
            _SET_PED_R* - _SET_PED_S*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.SET_PED_CLOTH_PIN_FRAMES(System.Int32,System.Boolean)">
            <summary>
            Old name: SET_PED_CLOTH_PACKAGE_INDEX
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x1D4636C90BBEFACB(System.Int32,System.Int32)">
            <summary>
            _SET_PED_CA* - _SET_PED_CO*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xEF371232BC6053E1(System.Int32)">
            <summary>
            _ADD_*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x86F0B6730C32AC14(System.Int32,System.Boolean)">
            <summary>
            _SET_PED_*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x8101BA1C0B462412(System.Int32,System.Int32)">
            <summary>
            _ATTACH_*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.SET_PED_CONFIG_FLAG(System.Int32,System.Int32,System.Boolean)">
            <summary>
            flagId: https://alloc8or.re/rdr3/doc/enums/ePedScriptConfigFlags.txt
            https://github.com/femga/rdr3_discoveries/tree/master/AI/CPED_CONFIG_FLAGS
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.SET_PED_RESET_FLAG(System.Int32,System.Int32,System.Boolean)">
            <summary>
            Needs to be called every frame
            https://github.com/femga/rdr3_discoveries/tree/master/AI/CPED_RESET_FLAGS
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.GET_PED_CONFIG_FLAG(System.Int32,System.Int32,System.Boolean)">
            <summary>
            flagId: see SET_PED_CONFIG_FLAG
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.IS_PED_EVASIVE_DIVING(System.Int32,System.Int32*)">
            <summary>
            Presumably returns the Entity that the Ped is currently diving out of the way of.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._SHOOT_TRIGGER_AT_COORDS(System.Int32,System.Single,System.Single,System.Single,System.Int32,System.Single,System.Int32,System.Single)">
            <summary>
            Triggers a gunshot
            Params: p5 = -1 in R* Scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._SET_PED_DISABLE_KICK_MOVE(System.Int32,System.Boolean)">
            <summary>
            Disables being able to kick move ped.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x078076AB50FB117F(System.Int32,System.Single,System.Single,System.Single,System.Single,System.Boolean)">
            <summary>
            If returned true: There are enemy peds near friendly turn in ped. Going to aggro.
            If returned false: Moving back to idle as there aren't any remaining enemy peds near ped
            _IS_PED_IN_*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._SET_PED_CAN_BE_LASSOED(System.Int32,System.Boolean)">
            <summary>
            SET_PED_CAN_*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.SET_PED_COMBAT_MOVEMENT(System.Int32,System.Int32)">
            <summary>
            0 - Stationary (Will just stand in place)
            1 - Defensive (Will try to find cover and very likely to blind fire)
            2 - Offensive (Will attempt to charge at enemy but take cover as well)
            3 - Suicidal Offensive (Will try to flank enemy in a suicidal attack)
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x815C0074A1BC0D93(System.Int32,System.Int32)">
            <summary>
            _SET_PED_COMBAT_R - _SET_PED_C*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xFFDE295662405B25(System.Int32)">
            <summary>
            _GET_PED_COMBAT_M* - _GET_PED_C*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.SET_PED_COMBAT_ABILITY(System.Int32,System.Int32)">
            <summary>
            abilityLevel:
            enum eCombatAbilityLevel
            {
            	CAL_POOR,
            	CAL_AVERAGE,
            	CAL_PROFESSIONAL
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.SET_PED_COMBAT_RANGE(System.Int32,System.Int32)">
            <summary>
            range:
            enum eCombatRange
            {
            	CR_NEAR,
            	CR_MEDIUM,
            	CR_FAR,
            	CR_VERY_FAR
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.SET_PED_COMBAT_ATTRIBUTES(System.Int32,System.Int32,System.Boolean)">
            <summary>
            attributeIndex: https://alloc8or.re/rdr3/doc/enums/eCombatAttribute.txt
            https://github.com/femga/rdr3_discoveries/tree/master/AI/COMBAT_ATTRIBUTES
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._SET_PED_COMBAT_ATTRIBUTE_HASH(System.Int32,System.UInt32)">
            <summary>
            Hashes: GUARD, COMBAT_ANIMAL, LAW, LAW_SHERIFF
            _SET_PED_COMBAT_A* - _SET_PED_COMBAT_M*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.SET_PED_TARGET_LOSS_RESPONSE(System.Int32,System.Int32)">
            <summary>
            TLR_ExitTask = 0,
            TLR_NeverLoseTarget,
            TLR_SearchForTarget
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x712B2C2B2471B493(System.Int32,System.UInt32)">
            <summary>
            _SET_PED_MO*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._SET_PED_COMBAT_STYLE(System.Int32,System.UInt32,System.Int32,System.Single)">
            <summary>
            https://github.com/femga/rdr3_discoveries/tree/master/AI/COMBAT_STYLES
            Params: p2 is usually 1, sometimes 0 or 2
            duration in seconds, -1.0 = forever
```
