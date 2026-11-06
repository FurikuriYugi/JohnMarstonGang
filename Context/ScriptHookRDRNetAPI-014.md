<!-- ScriptHookRDRNetAPI lines 3251-3500 of 30005 -->
```csharp
            Returns true if calling script owns specified entity
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY._0xC0EDEF16D90661EE(System.Int32,System.Single)">
            <summary>
            SET_ENTITY_A*
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY.SET_ENTITY_CAN_BE_TARGETED_WITHOUT_LOS(System.Int32,System.Boolean)">
            <summary>
            Sets whether the entity can be targeted without being in line-of-sight.
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY.SET_ENTITY_COORDS_NO_OFFSET(System.Int32,System.Single,System.Single,System.Single,System.Boolean,System.Boolean,System.Boolean)">
            <summary>
            Axis - Invert Axis Flags
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY.SET_ENTITY_HEALTH(System.Int32,System.Int32,System.Int32)">
            <summary>
            Sets the entity's health. healthAmount sets the health value to that, and sets the maximum health core value. Setting healthAmount to 0 will kill the entity. entityKilledBy parameter can also be 0
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY.SET_ENTITY_INVINCIBLE(System.Int32,System.Boolean)">
            <summary>
            Sets a ped or an object totally invincible. It doesn't take any kind of damage. Peds will not ragdoll on explosions.
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY._0xAF7F3099B9FEB535(System.Int32,System.Single,System.Single,System.Single)">
            <summary>
            SET_ENTITY_LO*
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY._SET_ENTITY_THREAT_TIER(System.Int32,System.Int32,System.Boolean)">
            <summary>
            tier: https://github.com/Halen84/RDR3-Native-Flags-And-Enums/tree/main/eEntityThreatTier
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY.HAS_COLLISION_LOADED_AROUND_POSITION(System.Single,System.Single,System.Single)">
            <summary>
            Old name: _HAS_COLLISION_LOADED_AT_COORDS
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY.SET_ENTITY_PROOFS(System.Int32,System.Int32,System.Boolean)">
            <summary>
            https://github.com/femga/rdr3_discoveries/tree/master/AI/ENTITY_PROOFS
            BOOL p2: handles an additional special proofs flag, so it simply indicates whether it should be enabled or disabled, not sure what exactly it proofs the entity from though
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY._GET_ENTITY_PROOFS(System.Int32)">
            <summary>
            Note: this native was removed in 1232 but added back in 1311
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY.SET_ENTITY_VELOCITY(System.Int32,System.Single,System.Single,System.Single)">
            <summary>
            Note that the third parameter(denoted as z) is "up and down" with positive numbers encouraging upwards movement.
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY.SET_ENTITY_LOD_DIST(System.Int32,System.Int32)">
            <summary>
            LOD distance can be 0 to 0xFFFF (higher values will result in 0xFFFF) as it is actually stored as a 16-bit value (aka uint16_t).
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY.GET_ENTITY_LOD_DIST(System.Int32)">
            <summary>
            Returns the LOD distance of an entity.
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY.SET_ENTITY_ALPHA(System.Int32,System.Int32,System.Boolean)">
            <summary>
            skin - everything alpha except skin
            Set entity alpha level. Ranging from 0 to 255 but changes occur after every 20 percent (after every 51).
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY._0x37B01666BAE8F7EF(System.Int32)">
            <summary>
            Seems to return true if entity is burned / scorched
            _GET_ENTITY_*
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY.CREATE_MODEL_SWAP(System.Single,System.Single,System.Single,System.Single,System.UInt32,System.UInt32,System.Boolean)">
            <summary>
            Only works with objects!
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY.SET_ENTITY_NOWEAPONDECALS(System.Int32,System.Boolean)">
            <summary>
            Old name: _SET_ENTITY_DECALS_DISABLED
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY._SEARCH_BUILDING_POOL_FOR_ENTITY_WITH_THIS_MODEL(System.UInt32)">
            <summary>
            Alternative Name: _GET_ENTITY_FROM_MAP_OBJECT; You can get existing objects and manipulate them using this native.
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY._GET_ENTITY_BY_DOORHASH(System.UInt32,System.Int32)">
            <summary>
            Params: p1 = 0 in R* Scripts (GET_DOOR_ENTITY_FROM_ID)
            https://github.com/femga/rdr3_discoveries/blob/master/doorHashes/doorhashes.lua
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY._0x8E46E18AA828334F(System.Int32,System.String,System.String)">
            <summary>
            Used in Script Function GENERIC_ITEM_HAS_ANIM_COMPLETED
            _GET_ENTITY_*
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY._0xDF8E49EA89A01DB1(System.UInt64,System.UInt64,System.UInt64)">
            <summary>
            Hardcoded to return zero/false.
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY.PLAY_ENTITY_ANIM(System.Int32,System.String,System.String,System.Single,System.Boolean,System.Boolean,System.Boolean,System.Single,System.UInt64)">
            <summary>
            https://github.com/femga/rdr3_discoveries/tree/master/animations
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY._SET_ENTITY_ANIM_CURRENT_TIME(System.Int32,System.String,System.String,System.Single)">
            <summary>
            https://gfycat.com/amazingmiserlyamericanquarterhorse
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY._0x5826EFD6D73C4DE5(System.Int32)">
            <summary>
            _REMOVE_DECALS_* - _REMOVE_FORCED*
            </summary>
        </member>
        <member name="M:RDR2.Native.EVENT.ADD_SHOCKING_EVENT_AT_POSITION(System.UInt32,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Int32,System.Int32)">
            <summary>
            eventType: https://alloc8or.re/rdr3/doc/enums/eEventType.txt
            https://github.com/femga/rdr3_discoveries/blob/master/AI/EVENTS
            </summary>
        </member>
        <member name="M:RDR2.Native.EVENT.ADD_SHOCKING_EVENT_FOR_ENTITY(System.UInt32,System.Int32,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Boolean,System.Boolean,System.Int32,System.Int32)">
            <summary>
            eventType: https://alloc8or.re/rdr3/doc/enums/eEventType.txt
            </summary>
        </member>
        <member name="M:RDR2.Native.EVENT.IS_SHOCKING_EVENT_IN_SPHERE(System.UInt32,System.Single,System.Single,System.Single,System.Single)">
            <summary>
            eventType: https://alloc8or.re/rdr3/doc/enums/eEventType.txt
            </summary>
        </member>
        <member name="M:RDR2.Native.EVENT._REMOVE_ALL_SHOCKING_EVENTS_OF_TYPE_IN_AREA(System.UInt32,System.Single,System.Single,System.Single,System.Single,System.Boolean)">
            <summary>
            eventType: https://alloc8or.re/rdr3/doc/enums/eEventType.txt
            </summary>
        </member>
        <member name="M:RDR2.Native.EVENT.REMOVE_ALL_SHOCKING_EVENTS_OF_TYPE(System.UInt32,System.Boolean)">
            <summary>
            eventType: https://alloc8or.re/rdr3/doc/enums/eEventType.txt
            </summary>
        </member>
        <member name="M:RDR2.Native.EVENT._0x36D0F2BA2C0D9BDE(System.Int32,System.Int32)">
            <summary>
            _ADD* (_ADD_SHOCKING_EVENT_* ?)
            </summary>
        </member>
        <member name="M:RDR2.Native.EVENT._0x7C511E91738A0828(System.Int32,System.Int32,System.Int32,System.UInt32)">
            <summary>
            Only used in R* SP Scripts
            Hash only used in R* Script mob3.ysc: ROBBERY
            _ADD_PED*
            </summary>
        </member>
        <member name="M:RDR2.Native.EVENT._0xB6F4825153920582">
            <summary>
            _S* (_SUPPRESS_EVENTS_NEXT_FRAME?)
            </summary>
        </member>
        <member name="M:RDR2.Native.EVENT._0x4B2B1A891D437CA7(System.Single)">
            <summary>
            Only used in R* SP Script coachrobberies
            _SET_S*
            </summary>
        </member>
        <member name="M:RDR2.Native.EVENT._0x9520175B35E2268D(System.Int32,System.Boolean)">
            <summary>
            _SET_P*
            </summary>
        </member>
        <member name="M:RDR2.Native.EVENT._0x18E93EBFC1FCFA48(System.Int32,System.Boolean,System.Boolean)">
            <summary>
            Only used in R* SP Script beat_rat_infestation and homeinvasion
            </summary>
        </member>
        <member name="M:RDR2.Native.EVENT._0x56B3410626A473E7(System.UInt64)">
            <summary>
            Only used in R* SP Script beat_rat_infestation
            Params: p0 = value returned by 0x18E93EBFC1FCFA48
            </summary>
        </member>
        <member name="M:RDR2.Native.EVENT._ADD_MODEL_TO_EVENT_MONITOR(System.UInt32,System.Boolean,System.Boolean)">
            <summary>
            Models used in the scripts: P_REGISTER05X, P_REGISTER06X, P_REGISTER03X, PLAYER_ZERO, PLAYER_THREE, A_C_HORSE_MORGAN_FLAXENCHESTNUT
            </summary>
        </member>
        <member name="M:RDR2.Native.EVENT._0xAD17A18215DD23D6(System.Int32,System.Int32,System.Int32)">
            <summary>
            Might return time since some (?) event.
            </summary>
        </member>
        <member name="M:RDR2.Native.EVENT._EVENT_GET_RECENT_EVENT(System.Int32,System.Int32,System.Int32)">
            <summary>
            Returns eventType
            </summary>
        </member>
        <member name="M:RDR2.Native.EVENT._0x797B3D4D92E56094(System.String,System.UInt32)">
            <summary>
            _IS_EVENT
            </summary>
        </member>
        <member name="M:RDR2.Native.EVENT._0x1D1B448D719415AB(System.Int32)">
            <summary>
            _GET*
            </summary>
        </member>
        <member name="M:RDR2.Native.EVENT._0x83D43F0FD5276E4D(System.Int32,System.Int32)">
            <summary>
            _GET*
            </summary>
        </member>
        <member name="M:RDR2.Native.EVENT._0xE28D7FC9FD32ABEB(System.Int32,System.UInt32,System.Int32)">
            <summary>
            HAS_ACTOR_RECEIVED_TRACKED_EVENT_THAT_SHOULD_ALERT - iTimeSinceEvent >= iTimeLimitMS
            </summary>
        </member>
        <member name="M:RDR2.Native.EVENT._0x1A5C5D350068A673(System.Int32,System.Int32)">
            <summary>
            AGGRO_CHECK_PROPERTY_DAMAGE: Property damage found with event
            </summary>
        </member>
        <member name="M:RDR2.Native.FIRE.START_SCRIPT_FIRE(System.Single,System.Single,System.Single,System.Int32,System.Single,System.Boolean,System.String,System.Single,System.Int32)">
            <summary>
            Starts a fire:
            
            xyz: Location of fire
            maxChildren: The max amount of times a fire can spread to other objects. Must be 25 or less, or the function will do nothing.
            isGasFire: Whether or not the fire is powered by gasoline.
            </summary>
        </member>
        <member name="M:RDR2.Native.FIRE.ADD_EXPLOSION(System.Single,System.Single,System.Single,System.Int32,System.Single,System.Boolean,System.Boolean,System.Single)">
            <summary>
            https://github.com/femga/rdr3_discoveries/tree/master/graphics/explosions
            
            explosionType:
            enum eExplosionTag
            {
            	EXP_TAG_DONTCARE = -1,
```
