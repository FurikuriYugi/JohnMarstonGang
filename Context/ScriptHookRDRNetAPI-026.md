<!-- ScriptHookRDRNetAPI lines 6251-6500 of 30005 -->
```csharp
        <member name="M:RDR2.Native.NETWORK.NETWORK_IS_PENDING_FRIEND(System.UInt64*)">
            <summary>
            Hardcoded to return false.
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._NETWORK_CAN_ADD_FRIEND(System.UInt64*)">
            <summary>
            On PC this returns true if gamerHandle is a valid handle.
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.NETWORK_SET_PLAYER_IS_PASSIVE(System.Boolean)">
            <summary>
            Old name: _NETWORK_SET_PASSIVE_MODE_OPTION
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._0xBC7D36946D19E60E(System.Boolean)">
            <summary>
            Only used in fm_race_controller R* Script (PROCESS_LOCAL_PLAYER_INIT)
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._0x880A7202301E282B(System.UInt64*,System.UInt64*,System.Single,System.Single,System.Single,System.Single,System.UInt64)">
            <summary>
            Params: p5 = 50.f, p6 = 0 in R* Script net_fetch (NET_FETCH_CLIENT_UPDATE_PED_ROLE_CLUE_IDLE)
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._0xC964FCD3D1720697">
            <summary>
            nullsub, doesn't do anything
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._0xEC089F84A9C16C62">
            <summary>
            nullsub, doesn't do anything
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._0x02C4C6C2900D84DF(System.Int32,System.UInt64)">
            <summary>
            Only used in SP R* Script dominoes_sp: p1 = 0
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.NETWORK_SET_ENTITY_ONLY_EXISTS_FOR_PARTICIPANTS(System.Int32,System.Boolean)">
            <summary>
            if set to true other network players can't see it
            if set to false other network player can see it
            =========================================
            ^^ I attempted this by grabbing an object with GET_ENTITY_PLAYER_IS_FREE_AIMING_AT and setting this naive no matter the toggle he could still see it.
            
            pc or last gen?
            
            ^^ last-gen
            
            Old name: _NETWORK_SET_ENTITY_INVISIBLE_TO_NETWORK
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.GET_NUM_RESERVED_MISSION_OBJECTS(System.Boolean)">
            <summary>
            p0 appears to be for MP
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.GET_NUM_RESERVED_MISSION_PEDS(System.Boolean)">
            <summary>
            p0 appears to be for MP
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.GET_NUM_RESERVED_MISSION_VEHICLES(System.Boolean)">
            <summary>
            p0 appears to be for MP
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._GET_RESERVED_MISSION_ENTITIES_FOR_THREAD(System.Int32,System.Int32*,System.Int32*,System.Int32*,System.Int32*,System.Int32*,System.Int32*)">
            <summary>
            Only used in R* Script net_stable_manager
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.GET_RESERVED_MISSION_ENTITIES_IN_AREA(System.Single,System.Single,System.Single,System.Boolean,System.Int32*,System.Int32*,System.Int32*,System.Int32*)">
            <summary>
            Used in Script Function NET_ACE_CLIENT_VERIFY_ENTITY_RESERVATIONS
            Coords: Slot world position
            
            Old name: _GET_RESERVATIONS_FOR_SLOT_WORLD_POSITION
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._0x5F328FC909F0E0FF(System.Int32,System.Int32,System.Int32,System.Int32)">
            <summary>
            Note: this native was added in build 1311.23
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.GET_MAX_NUM_NETWORK_OBJECTS">
            <summary>
            Always returns 60
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.GET_MAX_NUM_NETWORK_PEDS">
            <summary>
            Always returns 110
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.GET_MAX_NUM_NETWORK_VEHICLES">
            <summary>
            Always returns 40
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.GET_MAX_NUM_NETWORK_PICKUPS">
            <summary>
            Always returns 80
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._0x979765465A6F25FC(System.Int32,System.Boolean)">
            <summary>
            Must be called from a background script, otherwise it will do nothing.
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._0x5133CF81924F1129">
            <summary>
            Hardcoded to return zero.
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._0x1E4E097D71D449FB(System.Boolean)">
            <summary>
            Note: this native was added in build 1311.23
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._0x982D7AD755B8F62C(System.Boolean)">
            <summary>
            Note: this native was added in build 1311.23
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._0x917AD74BDCF8B6E9(System.Boolean)">
            <summary>
            Note: this native was added in build 1311.23
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._0xF8DC69DC1AD19072(System.Boolean)">
            <summary>
            Note: this native was added in build 1311.23
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._0x744BFBB0CA908161(System.Boolean)">
            <summary>
            Note: this native was added in build 1311.23
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._0x106CBDD5077DEDE1(System.Boolean)">
            <summary>
            Note: this native was added in build 1311.23
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._0xBAF7E2979442B29F(System.Boolean)">
            <summary>
            Note: this native was added in build 1311.23
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._0x039B692B3318FAB6(System.Boolean)">
            <summary>
            Note: this native was added in build 1311.23
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._0x4835413EA6F9C9CD(System.Boolean)">
            <summary>
            Note: this native was added in build 1311.23
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.GET_TIME_OFFSET(System.Int32,System.Int32)">
            <summary>
            Adds the first argument to the second.
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.IS_TIME_LESS_THAN(System.Int32,System.Int32)">
            <summary>
            Subtracts the second argument from the first, then returns whether the result is negative.
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.IS_TIME_MORE_THAN(System.Int32,System.Int32)">
            <summary>
            Subtracts the first argument from the second, then returns whether the result is negative.
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.GET_TIME_DIFFERENCE(System.Int32,System.Int32)">
            <summary>
            Subtracts the second argument from the first.
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.CONVERT_POSIX_TIME(System.Int32,System.UInt64*)">
            <summary>
            Takes the specified time and writes it to the structure specified in the second argument.
            
            struct date_time
            {
                int year;
                int PADDING1;
                int month;
                int PADDING2;
                int day;
                int PADDING3;
                int hour;
                int PADDING4;
                int minute;
                int PADDING5;
                int second;
                int PADDING6;
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.NETWORK_IS_PLAYER_IN_MP_CUTSCENE(System.Int32)">
            <summary>
            Note: scripts seem to indicate that this was renamed to NETWORK_IS_PLAYER_IN_MP_FAST_INSTANCE
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._0x34BC1E79546BA543(System.Boolean)">
            <summary>
            _NETWORK_A* - _NETWORK_C*
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._0x26A867C0B7A456D1(System.Int32)">
            <summary>
            _GET_LAUNCH_PARAM_(RESPOT?)*
            Name is probably invalid since this native only reads data from parsed entity.
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.SET_LOCAL_PLAYER_AS_GHOST(System.Boolean)">
            <summary>
            Old name: _SET_LOCAL_PLAYER_AS_GHOST
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.IS_ENTITY_A_GHOST(System.Int32)">
            <summary>
            Old name: _IS_ENTITY_GHOSTED_TO_LOCAL_PLAYER
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._SET_PLAYER_VISIBILITY_TO_LOCAL_PLAYER_DISABLED(System.Int32,System.Boolean)">
            <summary>
            _SET_PLAYER_V* - _SET_S*
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK.IS_OBJECT_REASSIGNMENT_IN_PROGRESS">
            <summary>
            Note: this native was added in build 1311.16
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._0x039AD6B57D5179FF">
            <summary>
            Note: this native was added in build 1311.23
            </summary>
        </member>
        <member name="M:RDR2.Native.NETWORK._0x02B3CDD652B3CDD6">
            <summary>
            Note: this native was added in build 1311.16
            
            GET_NUM_*
            </summary>
```
