<!-- ScriptHookRDRNetAPI lines 5751-6000 of 30005 -->
```csharp
        </member>
        <member name="M:RDR2.Native.MISC.ADD_TACTICAL_NAV_MESH_POINT(System.Single,System.Single,System.Single,System.Int32)">
            <summary>
            Params: p3 is 0 in R* Script utopia2
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.DISPLAY_ONSCREEN_KEYBOARD(System.Int32,System.String,System.String,System.String,System.String,System.String,System.String,System.Int32)">
            <summary>
            enum eOnscreenKeyboardTextType
            {
            	KTEXTTYPE_INVALID = -1,
            	KTEXTTYPE_DEFAULT,
            	KTEXTTYPE_EMAIL,
            	KTEXTTYPE_PASSWORD,
            	KTEXTTYPE_NUMERIC,
            	KTEXTTYPE_ALPHABET,
            	KTEXTTYPE_GAMERTAG,
            	KTEXTTYPE_FILENAME,
            	KTEXTTYPE_COUNT
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.UPDATE_ONSCREEN_KEYBOARD">
            <summary>
            Returns the current status of the onscreen keyboard, and updates the output.
            
            Status Codes:
            
            0 - User still editing
            1 - User has finished editing
            2 - User has canceled editing
            3 - Keyboard isn't active
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.GET_ONSCREEN_KEYBOARD_RESULT">
            <summary>
            Returns NULL unless UPDATE_ONSCREEN_KEYBOARD() returns 1 in the same tick.
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.CANCEL_ONSCREEN_KEYBOARD">
            <summary>
            Old name: _CANCEL_ONSCREEN_KEYBOARD
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.ACTION_MANAGER_ENABLE_ACTION(System.UInt32,System.Boolean)">
            <summary>
            Appears to remove stealth kill action from memory (?)
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.GET_REAL_WORLD_TIME">
            <summary>
            Returns GET_GAME_TIMER() / 1000
            Only used in rcm_pearson1.ysc
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.START_END_USER_BENCHMARK">
            <summary>
            Begins with START_*. Next character in the name is either D or E.
            
            Old name: _START_BENCHMARK_RECORDING
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.STOP_END_USER_BENCHMARK">
            <summary>
            Begins with STOP_*. Next character in the name is either D or E.
            
            Old name: _STOP_BENCHMARK_RECORDING
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.RESET_END_USER_BENCHMARK">
            <summary>
            Begins with RESET_*. Next character in the name is either D or E.
            
            Old name: _RESET_BENCHMARK_RECORDING
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.SAVE_END_USER_BENCHMARK">
            <summary>
            Saves the benchmark recording to %USERPROFILE%\Documents\Rockstar Games\Red Dead Redemption 2\Benchmarks and submits some metrics.
            
            Old name: _SAVE_BENCHMARK_RECORDING
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.UI_STARTED_END_USER_BENCHMARK">
            <summary>
            Hardcoded to return false.
            
            Old name: _UI_IS_SINGLEPLAYER_PAUSE_MENU_ACTIVE
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.GET_BENCHMARK_ITERATIONS">
            <summary>
            Returns value of the '-benchmarkIterations' command line option.
            
            Old name: _GET_BENCHMARK_ITERATIONS_FROM_COMMAND_LINE
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.GET_BENCHMARK_PASS">
            <summary>
            Returns value of the '-benchmarkPass' command line option.
            
            Old name: _GET_BENCHMARK_PASS_FROM_COMMAND_LINE
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC._0xDC057B86FC157031">
            <summary>
            Hardcoded to return one/true.
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC._GET_ITEM_TYPE(System.Int32)">
            <summary>
            0 = invalid
            1 = CEntity
            2 = rage::volBase
            3 = rage::volSphere
            4 = rage::volBox
            5 = rage::volAggregate
            6 = rage::volCylinder
            7 = CScriptedCoverPoint
            8 = rage::ptfxScriptInfo
            9 = CPed
            10 = CVehicle
            11 = CObject
            12 = CItemSet
            13 = CPersistentCharacter
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC._0x553D67295DDD2309(System.Int32)">
            <summary>
            UPDATE_PICKUP_COLLECTIBLE: set Eagle Eye fountain
            _J*, _K*, _L*
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.REGISTER_INTERACTION_LOCKON_PROMPT(System.Int32,System.String,System.Single,System.Single,System.Int32,System.Single,System.Single,System.Int32,System.Boolean,System.Int32)">
            <summary>
            p3 is usually the same value of radius
            p8 determines whether the ILO prompt is a lock on prompt with RMB
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC._CREATE_AI_MEMORY(System.UInt64*,System.Int32)">
            <summary>
            aiMemoryType: https://github.com/Halen84/RDR3-Native-Flags-And-Enums/tree/main/_CREATE_AI_MEMORY
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC._0xEB946B9E579729AD(System.Int32,System.UInt64)">
            <summary>
            Not implemented.
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC._SET_AI_MEMORY_REACTIONS_ENABLED(System.Boolean)">
            <summary>
            Used in CAIConditionAmbientAIMemoryReactionsEnabled
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC._GET_LOOTING_EVENT_HAS_FIRED(System.Int32,System.String)">
            <summary>
            Event names in the scripts: MGBegin, MGEnd, ReadyForCut
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC._0xA08111B053D84B4D(System.UInt64)">
            <summary>
            _CLEAR*
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC._SET_GAME_LOGIC_PAUSED">
            <summary>
            Note: this native was added in build 1232.56
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.STOP_CURRENT_LOADING_PROGRESS_TIMER">
            <summary>
            nullsub, doesn't do anything
            </summary>
        </member>
        <member name="M:RDR2.Native.MISSIONDATA.MISSIONDATA_WAS_COMPLETED(System.UInt32)">
            <summary>
            see: missions.meta
            </summary>
        </member>
        <member name="M:RDR2.Native.MISSIONDATA._MISSIONDATA_SET_MISSION_RATING(System.UInt32,System.Int32)">
            <summary>
            MISSION_RATING_INCOMPLETE = 0,
            MISSION_RATING_SKIPPED,
            MISSION_RATING_COMPLETE,
            MISSION_RATING_BRONZE,
            MISSION_RATING_SILVER,
            MISSION_RATING_GOLD,
            </summary>
        </member>
        <member name="M:RDR2.Native.MISSIONDATA._MISSIONDATA_SET_REPLAY_STATE_LOCKED(System.UInt32,System.Int32)">
            <summary>
            replayState: MISSIONDATA_GET_REPLAY_STATE
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.NETWORK_GET_NP_UNAVAILABLE_REASON">
            <summary>
            Hardcoded to return zero.
            
            ==== PS4 specific info ====
            
            Returns some sort of unavailable reason:
            -1 = REASON_INVALID
             0 = REASON_OTHER
             1 = REASON_SYSTEM_UPDATE
             2 = REASON_GAME_UPDATE
             3 = REASON_SIGNED_OUT
             4 = REASON_AGE
             5 = REASON_CONNECTION
            
            =================================
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.NETWORK_HAS_VALID_ROS_CREDENTIALS">
            <summary>
            Returns whether the signed-in user has valid Rockstar Online Services (ROS) credentials.
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.NETWORK_IS_HOST">
            <summary>
            If you are host, returns true else returns false.
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._0x3E8CCE6769DB5F34(System.Int32)">
            <summary>
            Stadia only; always returns -1 on other platforms. p0 may be a BOOL.
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.NETWORK_IS_PROMOTION_ENABLED">
            <summary>
            Hardcoded to return false.
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.NETWORK_IS_CUSTOM_UPSELL_ENABLED">
            <summary>
            Hardcoded to return false.
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.NETWORK_SHOULD_SHOW_PROMOTION_DLG">
            <summary>
            Hardcoded to return false.
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._0xFC6FCF4C03F1BBF6">
            <summary>
            nullsub, doesn't do anything
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._0x160F0CE6D76A39C9">
            <summary>
            nullsub, doesn't do anything
```
