<!-- ScriptHookRDRNetAPI lines 10001-10250 of 30005 -->
```csharp
        <member name="M:RDR2.Native.PLAYER._SET_BOW_STAMINA_DRAIN_SPEED(System.Int32,System.Single)">
            <summary>
            Decreases Stamina bar drain speed by % when drawing a bow.
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._0xBB6EA5D59E926095(System.Int32,System.UInt32)">
            <summary>
            NPEW__ENUM__EMOTE_CATEGORY_INVALID = -1
            NPEW__ENUM__EMOTE_CATEGORY_ACTIONS
            NPEW__ENUM__EMOTE_CATEGORY_ANTAGONIZE
            NPEW__ENUM__EMOTE_CATEGORY_REACTIONS
            NPEW__ENUM__EMOTE_CATEGORY_GREET
            NPEW__ENUM__NUM_EMOTE_CATEGORIES
            NPEW__ENUM__NUM_DISPLAY_TEXTURES
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._SET_PLAYER_RESET_FLAG(System.Int32,System.Int32,System.Boolean)">
            <summary>
            https://github.com/femga/rdr3_discoveries/tree/master/AI/PLAYER_RESET_FLAGS
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._GET_PLAYER_RESET_FLAG(System.Int32,System.Int32)">
            <summary>
            playerResetFlag: See 0x9F9A829C6751F3C7
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._SET_PLAYER_OWNS_MOUNT(System.Int32,System.Int32)">
            <summary>
            Seems to enable active horse equipment prompt when being near it and enables the control that opens the inventory as well
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._SET_PLAYER_HUNTING_WAGON(System.Int32,System.Int32)">
            <summary>
            Only applies to HUNTERCART01
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER.GET_DISCOVERABLE_NAME_HASH_AND_TYPE_FOR_ENTITY(System.Int32,System.UInt32*)">
            <summary>
            Returns name hash (name) and outHash includes the type.
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._GET_CONSTRUCTED_DISCOVERED_CHARACTER_NAME(System.UInt32,System.Boolean,System.Boolean)">
            <summary>
            p0: mostly Ped Hashes
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._SET_LOCKON_FOCUS_FIRE_VFX(System.Int32,System.String)">
            <summary>
            Focus Fire VFX start for player: p1 = focusfire
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._0x0B7803F6F7BB43E0">
            <summary>
            Hardcoded to return zero/false.
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._0xC74EB3F2EC169F6B(System.UInt64)">
            <summary>
            Hardcoded to return zero/false.
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._0x3B296934DB026873(System.UInt64,System.UInt64)">
            <summary>
            nullsub, doesn't do anything
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._0x57028FD99886F6F9">
            <summary>
            _IS_PLAYER_D* - _IS_PLAYER_F*
            </summary>
        </member>
        <member name="M:RDR2.Native.POPULATION._ADD_AMBIENT_AVOIDANCE_RESTRICTION(System.Int32,System.Int32,System.Int32,System.UInt32,System.UInt32,System.UInt32,System.Int32)">
            <summary>
            flags: https://github.com/Halen84/RDR3-Native-Flags-And-Enums/tree/main/PedFilterFlags
            </summary>
        </member>
        <member name="M:RDR2.Native.POPULATION._REMOVE_AMBIENT_AVOIDANCE_RESTRICTION(System.Int32)">
            <summary>
            flags: see 0xB56D41A694E42E86
            </summary>
        </member>
        <member name="M:RDR2.Native.POPULATION._ADD_AMBIENT_SPAWN_RESTRICTION(System.Int32,System.Int32,System.Int32,System.UInt32,System.UInt32,System.UInt32,System.Int32)">
            <summary>
            flags: see 0xB56D41A694E42E86
            </summary>
        </member>
        <!-- Badly formed XML comment ignored for member "M:RDR2.Native.POPULATION._SET_PED_SHOULD_IGNORE_AVOIDANCE_VOLUMES(System.Int32,System.Int32)" -->
        <member name="M:RDR2.Native.POPULATION._GET_RANDOM_FISH_TYPE_FOR_LOCATION">
            <summary>
            Returns model hash of the closest fish
            </summary>
        </member>
        <member name="M:RDR2.Native.POPULATION._0xEC116EDB683AD479(System.Boolean)">
            <summary>
            Only used for Special Event (XMAS).
            _SET_P*
            </summary>
        </member>
        <member name="M:RDR2.Native.PROPSET._REQUEST_PROP_SET_2(System.UInt32)">
            <summary>
            Same as _REQUEST_PROP_SET
            </summary>
        </member>
        <member name="M:RDR2.Native.PROPSET._HAS_PROP_SET_LOADED_2(System.UInt32)">
            <summary>
            Same as _HAS_PROP_SET_LOADED
            </summary>
        </member>
        <member name="M:RDR2.Native.PROPSET._CREATE_PROP_SET_2(System.UInt32,System.Single,System.Single,System.Single,System.Int32,System.Single,System.Single,System.Boolean,System.Boolean)">
            <summary>
            Same as _CREATE_PROP_SET
            https://github.com/femga/rdr3_discoveries/blob/master/objects/propsets_list.lua
            </summary>
        </member>
        <member name="M:RDR2.Native.PROPSET._CREATE_PROP_SET_4(System.UInt32,System.Single,System.Single,System.Single,System.Int32,System.Single,System.Boolean,System.Int32,System.Boolean)">
            <summary>
            Same as CREATE_PROP_SET_INSTANCE_ATTACHED_TO_ENTITY
            </summary>
        </member>
        <!-- Badly formed XML comment ignored for member "M:RDR2.Native.PROPSET._ADD_PROP_SET_FOR_VEHICLE(System.Int32,System.UInt32)" -->
        <member name="M:RDR2.Native.PROPSET._ADD_ADDITIONAL_PROP_SET_FOR_VEHICLE(System.Int32,System.UInt32)">
            <summary>
            https://github.com/femga/rdr3_discoveries/blob/master/vehicles/vehicle_modding/vehicle_propsets.lua
            </summary>
        </member>
        <member name="M:RDR2.Native.PROPSET._GET_VEHICLE_LIGHT_PROP_SET(System.Int32)">
            <summary>
            Returns PropSet handle to be used with _GET_PROP_SET_MODEL
            </summary>
        </member>
        <member name="M:RDR2.Native.PROPSET._ADD_LIGHT_PROP_SET_TO_VEHICLE(System.Int32,System.UInt32)">
            <summary>
            To remove propsets either parse a zero as hash or call 0xE31C0CB1C3186D40
            0xA6A9712955F53D9C returns lightPropset Hashes
            https://github.com/femga/rdr3_discoveries/blob/master/vehicles/vehicle_modding/vehicle_lantern_propsets.lua
            </summary>
        </member>
        <member name="M:RDR2.Native.PROPSET._GET_TRAIN_CARRIAGE_PROP_SET(System.Int32)">
            <summary>
            Example before/after deleting a train carriage's propset: https://imgur.com/a/qRNrIrK
            </summary>
        </member>
        <member name="M:RDR2.Native.RECORDING.REPLAY_PREVENT_RECORDING_THIS_FRAME">
            <summary>
            nullsub, doesn't do anything
            
            Old name: _STOP_RECORDING_THIS_FRAME
            </summary>
        </member>
        <member name="M:RDR2.Native.REPLAY.REPLAY_SYSTEM_HAS_REQUESTED_A_SCRIPT_CLEANUP">
            <summary>
            Hardcoded to return false.
            
            Old name: _IS_INTERIOR_RENDERING_DISABLED
            </summary>
        </member>
        <member name="M:RDR2.Native.REPLAY.SET_SCRIPTS_HAVE_CLEANED_UP_FOR_REPLAY_SYSTEM">
            <summary>
            nullsub, doesn't do anything
            </summary>
        </member>
        <member name="M:RDR2.Native.REPLAY.OPEN_VIDEO_EDITOR">
            <summary>
            Hardcoded to return true.
            </summary>
        </member>
        <member name="M:RDR2.Native.REPLAY.CLOSE_VIDEO_EDITOR(System.UInt64)">
            <summary>
            Hardcoded to return true.
            </summary>
        </member>
        <member name="M:RDR2.Native.REPLAY.IS_VIDEO_EDITOR_RUNNING">
            <summary>
            Hardcoded to return false.
            </summary>
        </member>
        <member name="M:RDR2.Native.SCRIPTS.HAS_SCRIPT_LOADED(System.String)">
            <summary>
            Returns if a script has been loaded into the game. Used to see if a script was loaded after requesting.
            </summary>
        </member>
        <member name="M:RDR2.Native.SCRIPTS.SCRIPT_THREAD_ITERATOR_RESET">
            <summary>
            Starts a new iteration of the current threads.
            Call this first, then SCRIPT_THREAD_ITERATOR_GET_NEXT_THREAD_ID (0x30B4FA1C82DD4B9F)
            </summary>
        </member>
        <member name="M:RDR2.Native.SCRIPTS.SCRIPT_THREAD_ITERATOR_GET_NEXT_THREAD_ID">
            <summary>
            If the function returns 0, the end of the iteration has been reached.
            </summary>
        </member>
        <member name="M:RDR2.Native.SCRIPTS.GET_NUMBER_OF_THREADS_RUNNING_THE_SCRIPT_WITH_THIS_HASH(System.UInt32)">
            <summary>
            Gets the number of instances of the specified script is currently running.
            
            Actually returns numRefs - 1.
            if (program)
            	v3 = rage::scrProgram::GetNumRefs(program) - 1;
            return v3;
            
            Old name: _GET_NUMBER_OF_REFERENCES_OF_SCRIPT_WITH_NAME_HASH
            </summary>
        </member>
        <member name="M:RDR2.Native.SCRIPTS._GET_THREAD_EXIT_REASON">
            <summary>
            enum eThreadExitReason
            {
            	THREAD_EXIT_REASON_NONE,
            	THREAD_EXIT_REASON_BACKGROUND_THREAD_STOPPED,
            	THREAD_EXIT_REASON_SESSION_MERGE,
            	THREAD_EXIT_REASON_SCENARIO_OUT_OF_SCOPE,
            	THREAD_EXIT_REASON_REQUESTED_BY_SCRIPT
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.SCRIPTS.GET_NUMBER_OF_EVENTS(System.Int32)">
            <summary>
            eventGroup: 0 = SCRIPT_EVENT_QUEUE_AI (CEventGroupScriptAI), 1 = SCRIPT_EVENT_QUEUE_NETWORK (CEventGroupScriptNetwork), 2 = unk, 3 = unk, 4 = SCRIPT_EVENT_QUEUE_ERRORS (CEventGroupScriptErrors)
            </summary>
        </member>
        <member name="M:RDR2.Native.SCRIPTS.GET_EVENT_EXISTS(System.Int32,System.Int32)">
            <summary>
            eventGroup: 0 = SCRIPT_EVENT_QUEUE_AI (CEventGroupScriptAI), 1 = SCRIPT_EVENT_QUEUE_NETWORK (CEventGroupScriptNetwork), 2 = unk, 3 = unk, 4 = SCRIPT_EVENT_QUEUE_SCRIPT_ERRORS (CEventGroupScriptErrors)
            </summary>
        </member>
        <member name="M:RDR2.Native.SCRIPTS.GET_EVENT_AT_INDEX(System.Int32,System.Int32)">
            <summary>
            eventGroup: 0 = SCRIPT_EVENT_QUEUE_AI (CEventGroupScriptAI), 1 = SCRIPT_EVENT_QUEUE_NETWORK (CEventGroupScriptNetwork), 2 = unk, 3 = unk, 4 = SCRIPT_EVENT_QUEUE_SCRIPT_ERRORS (CEventGroupScriptErrors)
            
            Returns event name hash: https://alloc8or.re/rdr3/doc/enums/eEventType.txt
            </summary>
        </member>
        <member name="M:RDR2.Native.SCRIPTS.GET_EVENT_DATA(System.Int32,System.Int32,System.UInt64*,System.Int32)">
            <summary>
            eventGroup: 0 = SCRIPT_EVENT_QUEUE_AI (CEventGroupScriptAI), 1 = SCRIPT_EVENT_QUEUE_NETWORK (CEventGroupScriptNetwork), 2 = unk, 3 = unk, 4 = SCRIPT_EVENT_QUEUE_SCRIPT_ERRORS (CEventGroupScriptErrors)
            
            Note: eventDataSize is NOT the size in bytes, it is the size determined by the SIZE_OF operator (RAGE Script operator, not C/C++ sizeof). That is, the size in bytes divided by 8 (script variables are always 8-byte aligned!).
            
            https://github.com/femga/rdr3_discoveries/tree/master/AI/EVENTS
            </summary>
        </member>
        <!-- Badly formed XML comment ignored for member "M:RDR2.Native.SCRIPTS.TRIGGER_SCRIPT_EVENT(System.Int32,System.UInt64*,System.Int32,System.Int32,System.Int32*)" -->
        <member name="M:RDR2.Native.SCRIPTS._0x11B0A0B282FA9B10(System.Boolean)">
            <summary>
            Used in Script Function DISABLE_REGISTERED_WORLD_BRAINS
            </summary>
        </member>
        <member name="M:RDR2.Native.SCRIPTS.IS_LOADING_SCREEN_VISIBLE">
            <summary>
```
