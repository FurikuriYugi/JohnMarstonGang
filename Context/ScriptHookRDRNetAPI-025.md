<!-- ScriptHookRDRNetAPI lines 6001-6250 of 30005 -->
```csharp
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.NETWORK_GET_PROMOTION_DLG_SEEN_COUNT">
            <summary>
            Hardcoded to return zero.
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.NETWORK_SESSION_IS_PRIVATE">
            <summary>
            Checks for session flag 'SF_PRIVATE'
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._NETWORK_SESSION_GET_SESSION_ID(System.UInt64*)">
            <summary>
            Note: this native was added in build 1311.23
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._NETWORK_SESSION_ARE_SESSION_IDS_EQUAL(System.UInt64*,System.UInt64*)">
            <summary>
            Note: this native was added in build 1311.23
            </summary>
        </member>
        <!-- Badly formed XML comment ignored for member "M:RDR2.Native.NETWORK.NETWORK_REQUEST_SESSION_SEAMLESS(System.Int32,System.Int32,System.UInt64*)" -->
        <member name="M:RDR2.Native.NETWORK.NETWORK_SESSION_REQUEST_SESSION_SEAMLESS(System.Int32,System.Int32,System.Int32,System.UInt64*)">
            <summary>
            Equivalent to NETWORK_REQUEST_SESSION_SEAMLESS if userHash == 0.
            Otherwise it is equivalent to NETWORK_SESSION_REQUEST_SESSION_COMPETITIVE(flags, MATCHTYPE_SEAMLESS, userHash, 0, sessionRequestId);
            
            p1 is unused
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.NETWORK_SESSION_REQUEST_SESSION_COMPETITIVE(System.Int32,System.Int32,System.Int32,System.Int32,System.UInt64*)">
            <summary>
            matchType:
            enum eMatchType
            {
            	MATCHTYPE_DEPRECATED,
            	MATCHTYPE_UGCPLAYLIST,
            	MATCHTYPE_UGCMISSION,
            	MATCHTYPE_MINIGAME,
            	MATCHTYPE_SEAMLESS,
            	MATCHTYPE_PRIVATE_DO_NOT_USE
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.NETWORK_SESSION_REQUEST_SESSION_PRIVATE(System.Int32,System.Int32,System.Int32,System.UInt64*)">
            <summary>
            Session flag 'SF_PRIVATE' is set internally
            p1 represents max amount of players in private session
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._NETWORK_SESSION_REQUEST_SESSION_ON_CALL(System.Int32,System.Int32,System.UInt64*,System.Int32,System.UInt64*)">
            <summary>
            category:
            enum eOnCallType
            {
            	NETWORK_SESSION_REQUEST_ON_CALL_TYPE_STORY = 2,
            	NETWORK_SESSION_REQUEST_ON_CALL_TYPE_MATCH = 3
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._NETWORK_SESSION_GET_SESSION_REQUEST_RESULT(System.UInt64*,System.Int32*)">
            <summary>
            Returns result of session request:
            0 - NOT_FOUND
            1 - IN_PROGRESS
            2 - TIMEOUT
            3 - PLAYER_OFFLINE
            4 - GANG_MEMBERS_CHANGED
            5 - PLAYER_CANCELLED
            6 - PLAYER_SET_TOO_LARGE
            7 - MATCH_ACCEPTED
            8 - OTHER
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._0xFD4272A137703449">
            <summary>
            Only used in R* Script startup_clip
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._NETWORK_SESSION_PLAYLIST_GO_TO_NEXT_CONTENT">
            <summary>
            Only used in R* Script net_rolling_playlist
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._NETWORK_SESSION_PLAYLIST_GET_UPCOMING_CONTENT">
            <summary>
            Only used in R* Script net_rolling_playlist
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._0x71FA2D1880C48032(System.Boolean)">
            <summary>
            Only used in R* Script fm_race_controller
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._0x316FD416C432C761">
            <summary>
            Note: this native was added in build 1311.23
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._0x062842D61D0D53FD">
            <summary>
            Note: this native was added in build 1311.23
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._NETWORK_IS_IN_SESSION_LOBBY">
            <summary>
            Hardcoded to return false.
            </summary>
        </member>
        <!-- Badly formed XML comment ignored for member "M:RDR2.Native.NETWORK._NETWORK_SESSION_ADD_SESSION_FLAGS(System.Int32)" -->
        <member name="M:RDR2.Native.NETWORK.NETWORK_SESSION_REMOVE_SESSION_FLAGS(System.Int32)">
            <summary>
            See _NETWORK_SESSION_ADD_SESSION_FLAGS
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._0x78271BC02AE9AF83(System.Int32)">
            <summary>
            Note: this native was added in build 1436.31
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._NETWORK_ARE_ONLINE_NOTIFICATIONS_SHOWN_IN_STORY_MODE">
            <summary>
            Returns value of fwuiCachedSetting "general.onlineNotificationsInStoryMode"
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.NETWORK_IS_IN_PLATFORM_PARTY">
            <summary>
            Hardcoded to return false.
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.NETWORK_IS_IN_PLATFORM_PARTY_CHAT">
            <summary>
            Hardcoded to return false.
            </summary>
        </member>
        <!-- Badly formed XML comment ignored for member "M:RDR2.Native.NETWORK.NETWORK_GET_MAX_NUM_PARTICIPANTS" -->
        <member name="M:RDR2.Native.NETWORK.NETWORK_GET_PLAYER_INDEX_FROM_PED(System.Int32)">
            <summary>
            Returns the Player associated to a given Ped when in an online session.
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.NETWORK_GET_NUM_CONNECTED_PLAYERS">
            <summary>
            Returns the amount of players connected in the current session. Only works when connected to a session/server.
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.PARTICIPANT_ID">
            <summary>
            Return the local Participant ID
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.PARTICIPANT_ID_TO_INT">
            <summary>
            Return the local Participant ID.
            
            This native is exactly the same as 'PARTICIPANT_ID' native.
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._0x6CF82A7F65A5AD5F(System.Int32,System.UInt64*)">
            <summary>
            _NETWORK_GET_A* - _NETWORK_GET_D*
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._0x236321F1178A5446(System.Int32,System.Int32,System.UInt64*)">
            <summary>
            _NETWORK_GET_A* - _NETWORK_GET_D*
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._0x950ACD8F05B7B9DF(System.UInt64)">
            <summary>
            Related to container entity (RANSACK_ATTACHED_LOCKBOX / RANSACK_ATTACHED_CHEST)
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._0xF260AF6F43953316(System.Int32)">
            <summary>
            Same Native Handler as VEH_TO_NET, PED_TO_NET, OBJ_TO_NET and NETWORK_GET_NETWORK_ID_FROM_ENTITY
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.VEH_TO_NET(System.Int32)">
            <summary>
            Returns the network ID of the given vehicle.
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.PED_TO_NET(System.Int32)">
            <summary>
            Returns the network ID of the given ped.
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.OBJ_TO_NET(System.Int32)">
            <summary>
            Returns the network ID of the given object.
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.NET_TO_PED(System.Int32)">
            <summary>
            gets the ped id of a network id
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.NET_TO_OBJ(System.Int32)">
            <summary>
            gets the object id of a network id
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.NET_TO_ENT(System.Int32)">
            <summary>
            gets the entity id of a network id
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._0x0CC28C08613BA9E5(System.Int32)">
            <summary>
            nullsub, doesn't do anything
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.NETWORK_GET_GAMERTAG_FROM_HANDLE(System.UInt64*)">
            <summary>
            Always returns a null string.
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.NETWORK_DISPLAYNAMES_FROM_HANDLES_START(System.UInt64*,System.UInt64)">
            <summary>
            Hardcoded to return -1.
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.NETWORK_GET_DISPLAYNAMES_FROM_HANDLES(System.UInt64,System.UInt64,System.UInt64)">
            <summary>
            Hardcoded to return zero.
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._NETWORK_GET_DISPLAY_NAME_FROM_HANDLE(System.UInt64*,System.String)">
            <summary>
            Example:
            
            char displayName[64];
            if (_NETWORK_GET_DISPLAY_NAME_FROM_HANDLE(handle, displayName))
            {
            	// use displayName
            }
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._0xF302AB9D978352EE(System.Int32)">
            <summary>
            Returns the entity's network ID.
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._0x4538EE7C321590BC(System.Int32)">
            <summary>
            Returns the entity associated with the given network ID.
            </summary>
        </member>
```
