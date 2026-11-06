<!-- ScriptHookRDRNetAPI lines 12001-12250 of 30005 -->
```csharp
            Gets the trailer of a vehicle and puts it into the trailer parameter.
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.CAN_SHUFFLE_SEAT(System.Int32,System.Int32)">
            <summary>
            seatIndex: see CREATE_PED_INSIDE_VEHICLE
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._SET_VEHICLE_LOD_LEVEL(System.Int32,System.Int32)">
            <summary>
            Ranges from -1 to 2? (internal type is int8)
            https://imgur.com/a/bPzHcft
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.COPY_VEHICLE_DAMAGES(System.Int32,System.Int32)">
            <summary>
            Copies sourceVehicle's damage (broken bumpers, broken lights, etc.) to targetVehicle.
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.IS_VEHICLE_VISIBLE(System.Int32)">
            <summary>
            Requires a visibility tracker on the vehicle (TRACK_VEHICLE_VISIBILITY)
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._0xCBF88256E44D5D39(System.Int32,System.Boolean)">
            <summary>
            Used in Script Function MC_LOCAL_SETUP_VEH - enabling transitions
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._0xC325A6BAA62CF8A2(System.Int32,System.Boolean)">
            <summary>
            Used in Script Function MC_LOCAL_SETUP_VEH - enabling transitions
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.SET_VEHICLE_AI_CAN_USE_EXCLUSIVE_SEATS(System.Int32,System.Boolean)">
            <summary>
            Used to be incorrectly named SET_VEHICLE_EXCLUSIVE_DRIVER
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.SET_VEHICLE_EXCLUSIVE_DRIVER(System.Int32,System.Int32,System.Int32)">
            <summary>
            index: 0 - 1
            
            Used to be incorrectly named _SET_VEHICLE_EXCLUSIVE_DRIVER_2
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.SET_DISABLE_SUPERDUMMY(System.Int32,System.Boolean)">
            <summary>
            Old name: _SET_DISABLE_SUPERDUMMY_MODE
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.GET_VEHICLE_BODY_HEALTH(System.Int32)">
            <summary>
            Seems related to vehicle health, like the one in IV.
            Max 1000, min 0.
            Vehicle does not necessarily explode or become undrivable at 0.
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._0x012701ED938B85DE(System.Single,System.Single)">
            <summary>
            Only used in SP Scripts.
            Related to Vehicle Speed.
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._GET_NUM_DRAFT_VEHICLE_HARNESS_PED(System.UInt32)">
            <summary>
            Returns number of horses a wagon can have
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._GET_ROWING_OARS(System.Int32,System.Int32*,System.Int32*)">
            <summary>
            Returns handles of boat paddles entities.
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._SET_FORCE_COACH_ROBBERY_LOOT(System.Int32,System.UInt32)">
            <summary>
            Hashes: COACH2_BOOT_LOOT_ITEMS_COACHROB_RSC, COACH2_BOOT_LOOT_ITEMS_COACHROB, COACH2_MARY3
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._BREAK_OFF_DRAFT_WHEEL(System.Int32,System.Int32,System.Single)">
            <summary>
            Params: destroyingForce is usually 100f in R* Scripts
            Similar to 0xD4F5EFB55769D272, _A*
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._0x0794199B25E499E1(System.Int32,System.Boolean)">
            <summary>
            _SET_VEHICLE_S*
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._0x73118A3EE9C9B6DB(System.Int32,System.Int32,System.Boolean)">
            <summary>
            _SET_VEHICLE_WHEELS_*
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._0xE1C0F8781BF130C2(System.Int32,System.Int32)">
            <summary>
            Only used in R* SP Script rcm_abigail31: p1 = 5
            _GET_VEHICLE_T* - _GET_VO*
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._0x18714953CCED17D3(System.Int32)">
            <summary>
            _ARE_ANY_VEHICLE_(WHEELS_DESTROYED?)*
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._0x41F0B254DDF71473(System.Int32)">
            <summary>
            _H*
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._IS_VEHICLE_DOOR_BROKEN(System.Int32,System.Int32)">
            <summary>
            doorId: see SET_VEHICLE_DOOR_SHUT
            </summary>
        </member>
        <!-- Badly formed XML comment ignored for member "M:RDR2.Native.VEHICLE._BREAK_OFF_VEHICLE_WHEEL(System.Int32,System.Int32)" -->
        <member name="M:RDR2.Native.VEHICLE._DELETE_VEHICLE_LANTERNS(System.Int32)">
            <summary>
            Spawn without lanterns set
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._0x6DE072AC8A95FFC1(System.Int32,System.Boolean)">
            <summary>
            _SET_INSTANTLY_* - _SET_MISSION_TRAIN*
            </summary>
        </member>
        <!-- Badly formed XML comment ignored for member "M:RDR2.Native.VEHICLE._GET_DRAFT_VEHICLE_DESIRED_SPEED(System.Int32)" -->
        <member name="M:RDR2.Native.VEHICLE._0xC4A2C11FC0D41916(System.Int32,System.Boolean)">
            <summary>
            _SET_DRAFT_VEHICLE_(STOP?)*
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._0xFC4F15A7DDDC47B1(System.Int32,System.Boolean)">
            <summary>
            _SET_DRAFT_VEHICLE_*
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._0x4C60C333F9CCA2B6(System.Int32,System.Boolean)">
            <summary>
            Params: p1 usually true in R* Scripts
            _SET_DRAFT_VEHICLE_*
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._0x06A09A6E0C6D2A84(System.Int32,System.Boolean)">
            <summary>
            _SET_TRAIN_*
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._0xAE7E66A61E7C17A5(System.Int32,System.Boolean)">
            <summary>
            _SET_TRAIN_*
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._0xEF28A614B4B264B8(System.Int32,System.Boolean)">
            <summary>
            _SET_TRAIN_*
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._0x04F0579DBDD32F34(System.Int32)">
            <summary>
            _SET_VEHICLE_*
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._0x12F6C6ED3EFF42DE(System.Int32,System.Single,System.Single,System.Single)">
            <summary>
            Params: coords = GET_ENTITY_VELOCITY
            _SET_VELOCITY*
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._CREATE_MISSION_TRAIN(System.UInt32,System.Single,System.Single,System.Single,System.Boolean,System.Boolean,System.Boolean,System.Boolean)">
            <summary>
            configHash: https://alloc8or.re/rdr3/doc/enums/eTrainConfig.txt
            For more information, see trainconfigs.ymt
            To make the train AI controlled, set conductor to true and set the speed once.
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._SET_BALLOON_HOVER_STATE(System.Int32,System.Single)">
            <summary>
            Params: 1.0f will make balloon hover
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._SET_BATCH_TARP_HEIGHT(System.Int32,System.Single,System.Boolean)">
            <summary>
            Total height is calculated using: cargo ratio + pelt ratio (by pelt count)
            Screenshot: https://imgur.com/a/nsomtiv
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._0x762FDC4C19E5A981(System.Int32,System.Boolean)">
            <summary>
            Seems to be related while setting a (door) state of specific trains (midlandboxcar05x, privateboxcar01x, privateboxcar02x, midlandrefrigeratorCar, privateArmoured, armoredCar01x)
            </summary>
        </member>
        <member name="M:RDR2.Native.VOICE._0x4791899615D70FA2(System.Int32,System.Int32,System.Int32)">
            <summary>
            Params: p1 = 2; p2 = 3 in R* Script net_main_offline
            </summary>
        </member>
        <member name="M:RDR2.Native.VOICE._0x767931C727DF2ED7(System.Int32,System.Int32)">
            <summary>
            Returns p2 value of 0x4791899615D70FA2
            </summary>
        </member>
        <member name="M:RDR2.Native.VOLUME._ADD_BOUNDS_TO_AGGREGATE_VOLUME(System.Int32,System.Int32)">
            <summary>
            _ADD_R* - _ADD_V(OLUME?)*
            </summary>
        </member>
        <member name="M:RDR2.Native.VOLUME._REMOVE_BOUNDS_FROM_AGGREGATE_VOLUME(System.Int32,System.Int32)">
            <summary>
            _REMOVE_E* - _REMOVE_R*
            </summary>
        </member>
        <member name="M:RDR2.Native.VOLUME.IS_POINT_IN_VOLUME(System.Int32,System.Single,System.Single,System.Single)">
            <summary>
            Old name: _IS_POSITION_INSIDE_VOLUME
            </summary>
        </member>
        <member name="M:RDR2.Native.VOLUME._0x748C5F51A18CB8F0(System.UInt64)">
            <summary>
            nullsub, doesn't do anything
            </summary>
        </member>
        <member name="M:RDR2.Native.VOLUME._CREATE_VOLUME_LOCK(System.Single,System.Single,System.Single,System.Single,System.Int32,System.UInt64)">
            <summary>
            Params: p5 is always 0
            </summary>
        </member>
        <member name="M:RDR2.Native.VOLUME._CREATE_VOLUME_LOCK_ATTACHED_TO_ENTITY(System.Int32,System.Single,System.Int32,System.UInt64)">
            <summary>
            Params: p3 is always 0
            </summary>
        </member>
        <member name="M:RDR2.Native.VOLUME.GET_VOLUME_LOCK_REQUEST_STATUS(System.Int32)">
            <summary>
            enum eVolumeLockRequestStatus
            {
            	VOLUME_LOCK_REQUEST_STATUS_INVALID,
            	VOLUME_LOCK_REQUEST_STATUS_READY,
            	VOLUME_LOCK_REQUEST_STATUS_IN_PROGRESS,
            	VOLUME_LOCK_REQUEST_STATUS_SUCCEEDED,
            	VOLUME_LOCK_REQUEST_STATUS_FAILED
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.VOLUME._0xFA15C9A320E707B0">
            <summary>
            nullsub, doesn't do anything
            </summary>
        </member>
```
