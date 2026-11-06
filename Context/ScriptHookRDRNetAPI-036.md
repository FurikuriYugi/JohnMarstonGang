<!-- ScriptHookRDRNetAPI lines 8751-9000 of 30005 -->
```csharp
            Overrides the ped's collision capsule radius for the current tick.
            Must be called every tick to be effective.
            
            Setting this to 0.001 will allow warping through some objects.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.SPAWNPOINTS_START_SEARCH(System.Single,System.Single,System.Single,System.Single,System.Single,System.Int32,System.Single,System.Int32,System.Single)">
            <summary>
            Params: p4 = 35.f, duration = 5000 in R* Scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.SPAWNPOINTS_START_SEARCH_IN_ANGLED_AREA(System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Int32,System.Single,System.Int32,System.Single)">
            <summary>
            Searching area between coords 1 and 2
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._REQUEST_PED_EMOTIONAL_PRESET(System.Int32,System.String)">
            <summary>
            For more information, see common:/data/emotional_presets.meta
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._HAS_PED_EMOTIONAL_PRESET_LOADED(System.Int32,System.String)">
            <summary>
            See _REQUEST_PED_EMOTIONAL_PRESET
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._REMOVE_PED_EMOTIONAL_PRESET(System.Int32,System.String)">
            <summary>
            See _REQUEST_PED_EMOTIONAL_PRESET
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x290B2E6CCDE532E1(System.Int32)">
            <summary>
            _IS_PED_L* - _IS_PED_M*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x0EEF7A81C17679DB(System.Int32)">
            <summary>
            _IS_PED_L* - _IS_PED_M*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.IS_TARGET_PED_IN_PERCEPTION_AREA(System.Int32,System.Int32,System.Single,System.Single,System.Single,System.Single)">
            <summary>
            Returns true if ped is in perception (focused and looking at target ped)
            Most float params are -1.f in R* Scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x913D04A5176F84C9(System.Int32)">
            <summary>
            _IS_PED_S* - _IS_PED_U*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._GET_PED_LASSO_HOGTIE_FLAG(System.Int32,System.Int32)">
            <summary>
            https://github.com/Halen84/RDR3-Native-Flags-And-Enums/tree/main/Lasso%20Hogtie%20Flags
            https://github.com/femga/rdr3_discoveries/tree/master/AI/LASSO_HOGTIE_FLAG
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._GET_LASSOER_OF_PED(System.Int32)">
            <summary>
            _IS_PED_S* - _IS_PED_U*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.SET_LOOTING_FLAG(System.Int32,System.Int32,System.Boolean)">
            <summary>
            https://github.com/Halen84/RDR3-Native-Flags-And-Enums/tree/main/CLootingFlags__Flags
            https://github.com/femga/rdr3_discoveries/tree/master/AI/LOOTING_FLAGS
            
            lootFlag:
            enum eLootFlag
            {
            	LOOT_FLAG_IS_CRITICAL_LOOT_TARGET = 7,
            	LOOT_FLAG_IGNORE_WATER_CHECKS = 8,
            	LOOT_FLAG_ANIMAL_FLAGGED_FOR_TAGGING = 23,
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._GET_LOOTING_FLAG(System.Int32,System.Int32)">
            <summary>
            lootFlag: see SET_LOOTING_FLAG
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._REFRESH_LOOT_STATE_FOR_PED(System.Int32,System.Int32,System.Int32*,System.Int32,System.Int32)">
            <summary>
            Returns loot state
            enum eLootState
            {
            	LAP_NONE,
            	LAP_RESUMING,
            	LAP_GETTING_ON_FOOT,
            	LAP_DISTANT_NAV,
            	LAP_CHOOSING_ACTION,
            	LAP_APPROACHING,
            	LAP_ENTERING,
            	LAP_LOOTING,
            	LAP_EXITING
            };
            
            _POSSE_* - _REGISTER_HATED*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.GET_PED_LOOT_STATUS_MP(System.Int32)">
            <summary>
            enum ePedLootStatus
            {
            	PLS_NONE,
            	PLS_PRE_LOOT,
            	PLS_SAMPLING,
            	PLS_SKINNING
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.SET_PED_CAN_BE_INCAPACITATED(System.Int32,System.Boolean)">
            <summary>
            When set on a player ped, its just like when you die in RDO
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x29924EB8EE9DB926(System.Int32,System.Single)">
            <summary>
            _SET_PED_K* or _SET_PED_L*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xB285AD0EC870B2DF(System.Int32,System.UInt32)">
            <summary>
            Params: hash - ARTHUR or JOHN
            _SET_PED_(A-D)*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xC494C76A34266E82(System.Int32,System.Int32)">
            <summary>
            [WARDROBE] Player took off their disguise: p1 = 13
            [WARDROBE] Player put on a disguise: p1 = 0
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xC3995D396F1D97B6(System.Int32,System.Int32,System.Int32)">
            <summary>
            _GET_PED_G* - _GET_PED_I*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x947E43F544B6AB34(System.Int32,System.Int32,System.Int32,System.Int32)">
            <summary>
            flags: https://github.com/Halen84/RDR3-Native-Flags-And-Enums/tree/main/0x947E43F544B6AB34
            _GET_PED_G* - _GET_PED_I(NTERACTION?)*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xE737D5F14304A2EC(System.Int32,System.Int32,System.Int32)">
            <summary>
            Only used in R* Script nb_animal_attack: p2 = 120000
            _SET_PED_SH* - _SET_PED_SP*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._GET_PED_ATTITUDE(System.Int32,System.Int32)">
            <summary>
            AI_ATTITUDE_NEUTRAL = 0,
            AI_ATTITUDE_FRIENDLY,
            AI_ATTITUDE_WARY,
            AI_ATTITUDE_COMBATIVE,
            AI_ATTITUDE_NEVER_MET
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._GET_PED_MOTIVATION(System.Int32,System.Int32,System.Int32)">
            <summary>
            If targetPed is set to 0 the ped motivationState affects everyone
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._SET_PED_MOTIVATION(System.Int32,System.Int32,System.Single,System.Int32)">
            <summary>
            enum eMotivationState
            {
            	TOILET_STATE,
            	FEAR_STATE,
            	ANGRY_STATE,
            	AGITATION_STATE,
            	HUNGRY_STATE,
            	TIRED_STATE,
            	SAD_STATE,
            	BRAVE_STATE,
            	OFFER_ITEM_STATE,
            	SUSPICION,
            	DRUNK_STATE
            };
            
            If targetPed is set to 0 the ped motivationState affects everyone
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._GET_IS_PED_MOTIVATION_STATE_ENABLED(System.Int32,System.Int32)">
            <summary>
            motivationState: see _SET_PED_MOTIVATION
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._SET_PED_MOTIVATION_STATE_OVERRIDE(System.Int32,System.Int32,System.Boolean)">
            <summary>
            motivationState: see _SET_PED_MOTIVATION
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._SET_PED_MOTIVATION_MODIFIER(System.Int32,System.Int32,System.Single)">
            <summary>
            The higher the modifier, the slower the motivationState value will decrease
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._SET_PED_SCENT(System.Int32,System.Single)">
            <summary>
            0.0 - 1.0
            Modifies the "scent line" on the ped's body when using Eagle Eye.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._SET_PED_VOICE_VOLUME(System.Int32,System.Single)">
            <summary>
            _SET_PED_S* - _SET_PED_T*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._SET_PED_FACE_FEATURE(System.Int32,System.Int32,System.Single)">
            <summary>
            Note: You have to update the ped's variation (using 0xCC8CA3E88256E58F) after calling this native
            
            index = Ped expression ID's / face features
            List of face features: https://pastebin.com/9jb88FXW
            value: -1.0 to 1.0
            
            This native also allows you to change a horse's gender.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._SET_PED_CULL_RANGE(System.Int32,System.Single,System.Single)">
            <summary>
            The higher the multiplier the less the engine renders culls (https://docs.unity3d.com/Manual/OcclusionCulling.html)
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._GET_META_PED_TYPE(System.Int32)">
            <summary>
            enum eMetaPedType
            {
            	MPT_MALE,
            	MPT_FEMALE,
            	MPT_TEEN,
            	MPT_ANIMAL,
            	MPT_NONE
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xBD0E4F52F6D95242(System.Int32)">
            <summary>
            _IS_PED_M* - _IS_PED_O*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._SET_META_PED_TAG(System.Int32,System.UInt32,System.UInt32,System.UInt32,System.UInt32,System.UInt32,System.Int32,System.Int32,System.Int32)">
            <summary>
            Use to apply metaped player components
            Replaces asset, alternatively you can remove assets using REMOVE_TAG_FROM_META_PED
            </summary>
        </member>
```
