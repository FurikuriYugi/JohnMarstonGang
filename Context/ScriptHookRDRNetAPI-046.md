<!-- ScriptHookRDRNetAPI lines 11251-11500 of 30005 -->
```csharp
            Baits: see 0x9B0C7FA063E67629
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK._SET_FISHING_BAIT(System.Int32,System.String,System.Boolean,System.Boolean)">
            <summary>
            Baits: p_fishHook02x, p_baitBread01x, p_baitCorn01x, p_baitCheese01x, p_baitWorm01x, p_baitCricket01x, p_crawdad01x, p_finisheDragonfly01x, p_finisdFishlure01x, p_finishdCrawd01x, p_finisheDragonflyLegendary01x, p_finisdFishlureLegendary01x, p_finishdCrawdLegendary01x, p_lgoc_spinner_v4
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK._PED_FISHINGROD_HOOK_OBJECT(System.Int32,System.Int32)">
            <summary>
            Used with 'P_BODYPARTARMFLOAT02X' model in fishing_core.c
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_PLAY_EMOTE_WITH_HASH(System.Int32,System.Int32,System.Int32,System.UInt32,System.Boolean,System.Boolean,System.Boolean,System.Boolean,System.Boolean)">
            <summary>
            https://github.com/femga/rdr3_discoveries/blob/master/animations/kit_emotes_list.lua
            emote: https://alloc8or.re/rdr3/doc/enums/eEmote.txt
            
            enum eEmoteType
            {
            	EMOTE_TYPE_INVALID = -1,
            	EMOTE_TYPE_REACT,
            	EMOTE_TYPE_ACTION,
            	EMOTE_TYPE_TAUNT,
            	EMOTE_TYPE_GREET,
            	EMOTE_TYPE_TWIRL_GUN,
            	EMOTE_TYPE_DANCE_FLOOR
            };
            
            enum eEmotePlaybackMode
            {
            	EMOTE_PM_INVALID = -1,
            	EMOTE_PM_UPPERBODY,
            	EMOTE_PM_UPPERBODY_LOOP,
            	EMOTE_PM_FULLBODY,
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK._TASK_PLAY_EMOTE(System.Int32,System.Int32,System.Int32,System.UInt32,System.Boolean,System.Boolean,System.Boolean,System.Boolean,System.Boolean)">
            <summary>
            Similar to 0xB31A277C1AC7B7FF but checks if the ped's inventory contains the specified emote kit.
            </summary>
        </member>
        <member name="M:RDR2.Native.TELEMETRY._TELEMETRY_CREATE_UUID(System.UInt64*)">
            <summary>
            Works in MP only.
            </summary>
        </member>
        <member name="M:RDR2.Native.UIDEBUG._BG_DISPLAY_TEXT(System.String,System.Single,System.Single)">
            <summary>
            Note: you must use VAR_STRING
            </summary>
        </member>
        <member name="M:RDR2.Native.UIDEBUG._BG_SET_TEXT_COLOR(System.Int32,System.Int32,System.Int32,System.Int32)">
            <summary>
            https://github.com/femga/rdr3_discoveries/tree/master/useful_info_from_rpfs/colours
            </summary>
        </member>
        <member name="M:RDR2.Native.UIEVENTS.EVENTS_UI_IS_PENDING(System.UInt32)">
            <summary>
            Old name: _EVENT_MANAGER_IS_EVENT_PENDING
            </summary>
        </member>
        <member name="M:RDR2.Native.UIEVENTS.EVENTS_UI_GET_MESSAGE(System.UInt32,System.UInt64*)">
            <summary>
            eventData:
            struct UI_SCRIPT_EVENT
            {
            	alignas(8) eUIScriptEventType eventType; // https://alloc8or.re/rdr3/doc/enums/eUIScriptEventType.txt
            	alignas(8) int intParam;
            	alignas(8) Hash hashParam;
            	alignas(8) Hash datastoreParam;
            };
            
            Old name: _EVENT_MANAGER_GET_EVENT
            </summary>
        </member>
        <member name="M:RDR2.Native.UIEVENTS.EVENTS_UI_PEEK_MESSAGE(System.UInt32,System.UInt64*)">
            <summary>
            eventData: see EVENTS_UI_GET_MESSAGE
            
            Old name: _EVENT_MANAGER_PEEK_EVENT
            </summary>
        </member>
        <member name="M:RDR2.Native.UIEVENTS.EVENTS_UI_POP_MESSAGE(System.UInt32)">
            <summary>
            Old name: _EVENT_MANAGER_POP_EVENT
            </summary>
        </member>
        <member name="M:RDR2.Native.UIFEED._UI_FEED_POST_HELP_TEXT(System.UInt64*,System.UInt64*,System.Boolean)">
            <summary>
            Example : https://pastebin.com/GvdBp8Dh
            </summary>
        </member>
        <member name="M:RDR2.Native.UIFEED._UI_FEED_POST_LOCATION_SHARD(System.UInt64*,System.UInt64*,System.Boolean,System.Boolean)">
            <summary>
            Example : https://pastebin.com/h1YzycuR
            </summary>
        </member>
        <member name="M:RDR2.Native.UIFEED._UI_FEED_POST_OBJECTIVE(System.UInt64*,System.UInt64*,System.Boolean)">
            <summary>
            Example : https://pastebin.com/13tuRa63
            </summary>
        </member>
        <member name="M:RDR2.Native.UIFEED._UI_FEED_POST_FEED_TICKER(System.UInt64*,System.UInt64*,System.Boolean)">
            <summary>
            Display text on right of the screen, Example : https://pastebin.com/n1YmNe25
            </summary>
        </member>
        <member name="M:RDR2.Native.UIFEED._UI_FEED_POST_SAMPLE_TOAST(System.UInt64*,System.UInt64*,System.Boolean,System.Boolean)">
            <summary>
            Example : https://pastebin.com/YZMBkAmW
            </summary>
        </member>
        <member name="M:RDR2.Native.UIFEED._UI_FEED_POST_SAMPLE_NOTIFICATION(System.UInt64*,System.UInt64*,System.Int32,System.Int32)">
            <summary>
            Example : https://pastebin.com/kAtEMQTD
            </summary>
        </member>
        <member name="M:RDR2.Native.UIFEED._0x18D6869FBFFEC0F8(System.UInt64*,System.UInt64*,System.Boolean,System.Boolean)">
            <summary>
            Only used in R* SP Scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.UIFEED.UI_FEED_CLEAR_CHANNEL(System.Int32,System.Boolean,System.Boolean)">
            <summary>
            feedChannel: https://github.com/Halen84/RDR3-Native-Flags-And-Enums/tree/main/eUIFeedChannel
            </summary>
        </member>
        <member name="M:RDR2.Native.UIFEED._UI_FEED_CLEAR_ALL_CHANNELS">
            <summary>
            Hides Toast Notifications
            </summary>
        </member>
        <member name="M:RDR2.Native.UIFEED._UI_FEED_CLEAR_HELP_TEXT_FEED(System.Int32,System.Boolean)">
            <summary>
            Clears help text
            </summary>
        </member>
        <member name="M:RDR2.Native.UIFEED._0x4E88A65968A55C78(System.UInt64*,System.Boolean)">
            <summary>
            Returns feedMessage
            </summary>
        </member>
        <member name="M:RDR2.Native.UIFEED._0x0FD07141AD048AAE(System.UInt64*,System.Boolean)">
            <summary>
            Only used in R* SP Script beat_animal_attack
            Returns feedMessage
            </summary>
        </member>
        <member name="M:RDR2.Native.UIFEED._UI_FEED_GET_MESSAGE_STATE(System.Int32)">
            <summary>
            Returns messageState, see https://github.com/Halen84/RDR3-Native-Flags-And-Enums/tree/main/eUIMessageState
            </summary>
        </member>
        <member name="M:RDR2.Native.UIFEED.UI_FEED_GET_CURRENT_MESSAGE(System.Int32)">
            <summary>
            feedChannel: see UI_FEED_CLEAR_CHANNEL
            Returns feedMessage
            </summary>
        </member>
        <member name="M:RDR2.Native.UITUTORIAL._UITUTORIAL_SET_RPG_ICON_VISIBILITY(System.Int32,System.Int32)">
            <summary>
            enum eRpgIcons
            {
            	ICON_STAMINA,
            	ICON_STAMINA_CORE,
            	ICON_DEADEYE,
            	ICON_DEADEYE_CORE,
            	ICON_HEALTH,
            	ICON_HEALTH_CORE,
            	ICON_HORSE_HEALTH,
            	ICON_HORSE_HEALTH_CORE,
            	ICON_HORSE_STAMINA,
            	ICON_HORSE_STAMINA_CORE,
            	ICON_HORSE_COURAGE,
            	ICON_HORSE_COURAGE_CORE
            };
            
            enum eRpgIconVisibility
            {
            	ICON_VISIBILITY_WAIT_TO_HIDE,
            	ICON_VISIBILITY_ALWAYS_SHOW,
            	ICON_VISIBILITY_ALWAYS_HIDE,
            	ICON_VISIBILITY_ALWAYS_BLINK
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.UISTICKYFEED._UI_STICKY_FEED_CREATE_ERROR_MESSAGE(System.UInt64*,System.UInt64*,System.Boolean)">
            <summary>
            Example: https://pastebin.com/EJD7ytnz
            </summary>
        </member>
        <member name="M:RDR2.Native.UISTICKYFEED._UI_STICKY_FEED_CREATE_DEATH_FAIL_MESSAGE(System.UInt64*,System.UInt64*,System.Boolean)">
            <summary>
            Example: https://pastebin.com/JygJShNU
            </summary>
        </member>
        <member name="M:RDR2.Native.UISTICKYFEED._UI_STICKY_FEED_CREATE_WARNING_MESSAGE(System.UInt64*,System.UInt64*,System.Boolean)">
            <summary>
            Example: https://pastebin.com/6mLtee2S
            </summary>
        </member>
        <member name="M:RDR2.Native.UISTICKYFEED._UI_STICKY_FEED_UPDATE_MESSAGE(System.Int32,System.UInt64*,System.Boolean)">
            <summary>
            Seems to only update _UI_STICKY_FEED_CREATE_ERROR_MESSAGE(0x9F2CC2439A04E7BA) and _UI_STICKY_FEED_CREATE_DEATH_FAIL_MESSAGE(0x815C4065AE6E6071) message.
            Example: https://pastebin.com/nDrJyWq2
            </summary>
        </member>
        <member name="M:RDR2.Native.UISTICKYFEED._UI_STICKY_FEED_IS_CHANNEL_ACTIVE(System.Int32)">
            <summary>
            stickyFeedChannel: https://github.com/Halen84/RDR3-Native-Flags-And-Enums/tree/main/eUIStickyFeedChannel
            </summary>
        </member>
        <member name="M:RDR2.Native.UISTICKYFEED._UI_STICKY_FEED_GET_MESSAGE_STATE(System.Int32)">
            <summary>
            Returns state of sticky feed message, see 0x59FA676177DBE4C9
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._0x6355602C02EDC6DF(System.Int32,System.UInt64)">
            <summary>
            Only used in R* Script beat_train_holdup: p1 = 1
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._CREATE_DRAFT_VEHICLE(System.UInt32,System.Single,System.Single,System.Single,System.Single,System.Boolean,System.Boolean,System.Boolean,System.UInt32,System.Boolean)">
            <summary>
            Identical to CREATE_VEHICLE but allows to set draftAnimalPopGroup (see popgroups.#mt for DRAFT_HORSES_*)
            </summary>
        </member>
        <!-- Badly formed XML comment ignored for member "M:RDR2.Native.VEHICLE.DELETE_VEHICLE(System.Int32*)" -->
        <member name="M:RDR2.Native.VEHICLE.SET_VEHICLE_ALLOW_NO_PASSENGERS_LOCKON(System.Int32,System.Boolean)">
            <summary>
            Makes the vehicle accept no passengers.
            </summary>
        </member>
        <!-- Badly formed XML comment ignored for member "M:RDR2.Native.VEHICLE.IS_VEHICLE_STOPPED(System.Int32)" -->
        <member name="M:RDR2.Native.VEHICLE.GET_VEHICLE_NUMBER_OF_PASSENGERS(System.Int32)">
            <summary>
            Gets the number of passengers, NOT including the driver. Use IS_VEHICLE_SEAT_FREE(Vehicle, -1) to also check for the driver
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.IS_SEAT_WARP_ONLY(System.Int32,System.Int32)">
            <summary>
            seatIndex: see CREATE_PED_INSIDE_VEHICLE
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.SET_DISABLE_RANDOM_TRAINS_THIS_FRAME(System.Boolean)">
            <summary>
            nullsub, doesn't do anything
            </summary>
```
