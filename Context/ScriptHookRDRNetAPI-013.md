<!-- ScriptHookRDRNetAPI lines 3001-3250 of 30005 -->
```csharp
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY.GET_OFFSET_FROM_ENTITY_IN_WORLD_COORDS(System.Int32,System.Single,System.Single,System.Single)">
            <summary>
            Offset values are relative to the entity.
            
            x = left/right
            y = forward/backward
            z = up/down
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY.GET_ENTITY_ROLL(System.Int32)">
            <summary>
            Displays the current ROLL axis of the entity [-180.0000/180.0000+]
            (Sideways Roll) such as a vehicle tipped on its side
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY.GET_ENTITY_SPEED(System.Int32)">
            <summary>
            Result is in meters per second (m/s)
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY.GET_OBJECT_INDEX_FROM_ENTITY_INDEX(System.Int32)">
            <summary>
            Simply returns whatever is passed to it (Regardless of whether the handle is valid or not).
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY.GET_PED_INDEX_FROM_ENTITY_INDEX(System.Int32)">
            <summary>
            Simply returns whatever is passed to it (Regardless of whether the handle is valid or not).
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY.GET_VEHICLE_INDEX_FROM_ENTITY_INDEX(System.Int32)">
            <summary>
            Simply returns whatever is passed to it (Regardless of whether the handle is valid or not).
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY.GET_WORLD_POSITION_OF_ENTITY_BONE(System.Int32,System.Int32)">
            <summary>
            Returns the coordinates of an entity-bone.
            https://github.com/femga/rdr3_discoveries/tree/master/boneNames
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY._0x5E214112806591EA(System.Int32,System.Int32)">
            <summary>
            Attaches scenario to bone with an offset
            _GET_I* - _GET_M*
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY.GET_ENTITY_TYPE(System.Int32)">
            <summary>
            Returns entityType: https://github.com/Halen84/RDR3-Native-Flags-And-Enums/tree/main/eEntityType
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY.IS_ENTITY_AT_COORD(System.Int32,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Boolean,System.Boolean,System.Int32)">
            <summary>
            Checks if entity is within x/y/zSize distance of x/y/z. 
            
            Last three are unknown ints, almost always p7 = 0, p8 = 1, p9 = 0
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY.IS_ENTITY_AT_ENTITY(System.Int32,System.Int32,System.Single,System.Single,System.Single,System.Boolean,System.Boolean,System.Int32)">
            <summary>
            Checks if entity1 is within the box defined by x/y/zSize of entity2.
            
            Last three parameters are almost always p5 = 0, p6 = 1, p7 = 0
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY.IS_ENTITY_IN_ANGLED_AREA(System.Int32,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Boolean,System.Boolean,System.UInt64)">
            <summary>
            Creates a spherical cone at origin that extends to surface with the angle specified. Then returns true if the entity is inside the spherical cone
            
            Angle is measured in degrees.
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY.GET_ENTITY_SUBMERGED_LEVEL(System.Int32)">
            <summary>
            Get how much of the entity is submerged.  1.0f is whole entity.
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY._0xFF9965C47FA404DA(System.Int32,System.Boolean)">
            <summary>
            SET_ENTITY_LO*
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY.SCRIPT_OVERRIDE_ENTITY_LOOT_TABLE_PERMANENT(System.Int32,System.UInt32)">
            <summary>
            Sets the loot table an entity will carry. Returns true if loot table has been successfully set. Returns false if entity is not a ped or object.
            https://github.com/femga/rdr3_discoveries/blob/master/AI/EVENTS/loot_rewards.lua
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY._GET_SCRIPT_OVERRIDE_ENTITY_LOOT_TABLE_PERMANENT(System.Int32,System.UInt32*)">
            <summary>
            Returns false if entity is not a ped or object.
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY.GET_CARRIABLE_ENTITY_STATE(System.Int32)">
            <summary>
            enum eCarriableState
            {
            	CARRIABLE_STATE_NONE,
            	CARRIABLE_STATE_TRANSITIONING_TO_HOGTIED,
            	CARRIABLE_STATE_CARRIABLE_INTRO,
            	CARRIABLE_STATE_CARRIABLE,
            	CARRIABLE_STATE_BEING_PICKED_UP_FROM_GROUND,
            	CARRIABLE_STATE_CARRIED_BY_HUMAN,
            	CARRIABLE_STATE_BEING_PLACED_ON_GROUND,
            	CARRIABLE_STATE_CARRIED_BY_MOUNT,
            	CARRIABLE_STATE_BEING_PLACED_ON_MOUNT,
            	CARRIABLE_STATE_BEING_PICKED_UP_FROM_MOUNT,
            	CARRIABLE_STATE_BEING_CUT_FREE,
            	CARRIABLE_STATE_BEING_PLACED_ON_GROUND_ESCAPE,
            	CARRIABLE_STATE_BEING_PLACED_IN_VEHICLE
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY._GET_OPTIMAL_CARRY_CONFIG(System.Int32,System.Int32)">
            <summary>
            Valid indices: 0 - 3
            Index 1 always returns a `hogtied` config, doesn't matter the entity.
            It's for humans only and the ped must be resurrected first if it's dead.
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY._GET_ENTITY_CARRY_CONFIG(System.Int32)">
            <summary>
            Returns zero if the entity is not a carriable
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY._GET_CARRIABLE_FROM_ENTITY(System.Int32)">
            <summary>
            Returns a hash of an entity's name. (Alternative Name: _GET_ENTITY_PROMPT_NAME_HASH)
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY._SET_ENTITY_CARCASS_TYPE(System.Int32,System.UInt32)">
            <summary>
            Changes type and quality of skins
            type hashes: https://pastebin.com/C1WvQjCy
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY._0x383F64263F946E45(System.Int32*,System.Int32,System.Int32,System.Int32,System.UInt64,System.Int32)">
            <summary>
            Used when checking if ped is in water
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY._GET_ENTITY_CARRYING_FLAG(System.Int32,System.Int32)">
            <summary>
            flagId: see _SET_ENTITY_CARRYING_FLAG
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY._SET_ENTITY_CARRYING_FLAG(System.Int32,System.Int32,System.Boolean)">
            <summary>
            flagId: https://github.com/femga/rdr3_discoveries/tree/master/AI/CARRYING_FLAGS
            https://github.com/Halen84/RDR3-Native-Flags-And-Enums/tree/main/CCarryingFlags__Flags
            
            enum eCarryingFlag
            {
            	CARRYING_FLAG_CAN_BE_CUT_FREE = 1,
            	CARRYING_FLAG_CAN_BE_CARRIED_ON_FOOT = 2,
            	CARRYING_FLAG_CAN_BE_DROPPED = 4,
            	CARRYING_FLAG_CAN_BE_CARRIED_WHEN_DEAD = 7,
            	CARRYING_FLAG_CAN_CARRY_ANYTHING = 9,
            	CARRYING_FLAG_DISABLE_PROMPT_LOS_CHECKS = 19,
            	CARRYING_FLAG_FORCE_ALLOW_WARP_TO_SAFE_GROUND_LOCATION = 23,
            	CARRYING_FLAG_PICKUPS_IGNORE_HEIGHT_RESTRICTIONS = 26,
            	CARRYING_FLAG_CLEAN_UP_WHEN_NOT_CARRIED = 27,
            	CARRYING_FLAG_BLOCK_KNOCK_OFF_PED_VARIATIONS_FROM_CARRIABLE_INTERACTIONS = 29,
            	CARRYING_FLAG_HIT_WHEN_CARRIABLE = 31,
            	CARRYING_FLAG_DISABLE_CARRIABLE_INTERACTIONS_ON_THIS_MOUNT = 34,
            	CARRYING_FLAG_FORCE_HIDE_PROMPT_GROUP = 37,
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY._0xC3ABCFBC7D74AFA5(System.Int32,System.Int32,System.Boolean)">
            <summary>
            Returns BOOL in ida
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY._0x371D179701D9C082(System.Int32)">
            <summary>
            Called if entity is in water and submerged level is larger than 1f. If CARRYING_FLAG_FORCE_ALLOW_WARP_TO_SAFE_GROUND_LOCATION is true, it gets disabled as well.
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY.APPLY_FORCE_TO_ENTITY_CENTER_OF_MASS(System.Int32,System.Int32,System.Single,System.Single,System.Single,System.Boolean,System.Boolean,System.Boolean,System.Boolean)">
            <summary>
            p6/relative - makes the xyz force not relative to world coords, but to something else
            p7/highForce - setting false will make the force really low
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY.ATTACH_ENTITY_TO_ENTITY(System.Int32,System.Int32,System.Int32,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Boolean,System.Boolean,System.Boolean,System.Boolean,System.Int32,System.Boolean,System.Boolean,System.Boolean)">
            <summary>
            Attaches entity1 to bone (boneIndex) of entity2.
            
            boneIndex - this is different to boneID, use GET_PED_BONE_INDEX to get the index from the ID. use the index for attaching to specific bones. entity1 will be attached to entity2's centre if bone index given doesn't correspond to bone indexes for that entity type.
            https://github.com/femga/rdr3_discoveries/tree/master/boneNames
            
            useSoftPinning - if set to false attached entity will not detach when fixed
            collision - controls collision between the two entities (FALSE disables collision).
            isPed - pitch doesn't work when false and roll will only work on negative numbers (only peds)
            vertexIndex - position of vertex
            fixedRot - if false it ignores entity vector
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY.DELETE_ENTITY(System.Int32*)">
            <summary>
            Deletes the specified entity, then sets the handle pointed to by the pointer to NULL.
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY._DELETE_ENTITY_2(System.Int32*)">
            <summary>
            Must be called from a background script, otherwise it will do nothing.
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY._IS_ENTITY_FROZEN(System.Int32)">
            <summary>
            Getter for FREEZE_ENTITY_POSITION
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY.SET_ENTITY_SHOULD_FREEZE_WAITING_ON_COLLISION(System.Int32,System.Boolean)">
            <summary>
            Old name: _SET_ENTITY_CLEANUP_BY_ENGINE
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY.SET_ENTITY_AS_MISSION_ENTITY(System.Int32,System.Boolean,System.Boolean)">
            <summary>
            Makes the specified entity (ped, vehicle or object) persistent. Persistent entities will not automatically be removed by the engine.
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY.SET_ENTITY_AS_NO_LONGER_NEEDED(System.Int32*)">
            <summary>
            Marks the specified entity (ped, vehicle or object) as no longer needed.
            Entities marked as no longer needed, will be deleted as the engine sees fit.
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY.SET_PED_AS_NO_LONGER_NEEDED(System.Int32*)">
            <summary>
            This is an alias of SET_ENTITY_AS_NO_LONGER_NEEDED.
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY.SET_VEHICLE_AS_NO_LONGER_NEEDED(System.Int32*)">
            <summary>
            This is an alias of SET_ENTITY_AS_NO_LONGER_NEEDED.
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY.SET_OBJECT_AS_NO_LONGER_NEEDED(System.Int32*)">
            <summary>
            This is an alias of SET_ENTITY_AS_NO_LONGER_NEEDED.
            </summary>
        </member>
        <member name="M:RDR2.Native.ENTITY._DOES_THREAD_OWN_THIS_ENTITY(System.Int32)">
            <summary>
```
