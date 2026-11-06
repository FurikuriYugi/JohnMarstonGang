<!-- ScriptHookRDRNetAPI lines 11001-11250 of 30005 -->
```csharp
            <summary>
            meleeStyles: AR_GRAPPLE_BACK_FROM_BACK, AR_GRAPPLE_MOUNT_FACEDOWN_FROM_FRONT, AR_ALLIGATOR_LEAPKILL, AR_ALLIGATOR_WAIST_AUTOKILL_FRONT
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_PUT_PED_DIRECTLY_INTO_GRAPPLE(System.Int32,System.Int32,System.UInt32,System.Single,System.Single,System.Boolean,System.Int32)">
            <summary>
            grappleStyle: AR_GRAPPLE_STRUGGLE, AR_ALLIGATOR_LEG_GRAB_CHALLENGE_FAIL, AR_GRAPPLE_BACK_FROM_BACK, AR_GRAPPLE_BACK_DEFEND, AR_GRAPPLE_FRONT_FROM_FRONT
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.SET_DRIVE_TASK_MAX_CRUISE_SPEED(System.Int32,System.Single)">
            <summary>
            Not implemented.
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.CREATE_SCENARIO_POINT_HASH(System.UInt32,System.Single,System.Single,System.Single,System.Single,System.UInt64,System.UInt64,System.Boolean)">
            <summary>
            Returns scenario
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.CREATE_SCENARIO_POINT_HASH_ATTACHED_TO_ENTITY(System.Int32,System.UInt32,System.Single,System.Single,System.Single,System.Single,System.UInt64,System.UInt64,System.Boolean)">
            <summary>
            Returns scenario
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.GET_PROP_FOR_SCENARIO_POINT(System.Int32,System.String)">
            <summary>
            Old name: _GET_SCENARIO_POINT_ENTITY
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK._SET_SCENARIO_POINT_FLAG(System.Int32,System.Int32,System.Boolean)">
            <summary>
            flag: https://github.com/Halen84/RDR3-Native-Flags-And-Enums/tree/main/CScenarioPointFlags__Flags
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK._0xADC45010BC17AF0E(System.UInt64,System.UInt64)">
            <summary>
            _SET_SCENARIO_POINT_*
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK._GET_SCENARIO_POINT_ENTITY(System.Int32)">
            <summary>
            Note: The current name for this native is the old name of 0x295514F198EFD0CA
            Old name for this native: _GET_ENTITY_SCENARIO_POINT_IS_ATTACHED_TO
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK._GET_SCENARIO_POINT_COORDS(System.Int32,System.Boolean)">
            <summary>
            Params: p1 is always true in R* Scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK._GET_SCENARIO_POINT_HEADING(System.Int32,System.Boolean)">
            <summary>
            Params: p1 is always true in R* Scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK._0xA7479FB665361EDB(System.UInt64,System.UInt64)">
            <summary>
            _SET_SCENARIO_*
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.GET_SCENARIO_POINTS_IN_AREA(System.Single,System.Single,System.Single,System.Single,System.UInt64*,System.Int32)">
            <summary>
            Note: scenariosInRadius is an array, and its size and values should be aligned to 8 bytes.
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK._SET_SCENARIO_CONTAINER_OPENING_STATE(System.Int32,System.Boolean)">
            <summary>
            Opens/closes containers: ChestDugUp
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK._GET_SCENARIO_CONTAINER_OPENING_STATE(System.Int32)">
            <summary>
            Returns m_eContainerState
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_START_SCENARIO_IN_PLACE_HASH(System.Int32,System.UInt32,System.Int32,System.Boolean,System.UInt32,System.Single,System.Boolean)">
            <summary>
            https://github.com/femga/rdr3_discoveries/blob/master/animations/scenarios
            Params: duration in milliseconds
            
            conditionalHash (optionally):
            0 = play random conditional anim.
            Every conditional anim has requirements to play it.
            If requirements are not met, ped plays random allowed conditional anim or can be stuck.
            For example, this scenario type has possible conditional anim WORLD_HUMAN_LEAN_BACK_WALL_SMOKING_MALE_D, but it can not be played by player, because condition is set to NOT be CAIConditionIsPlayer (check file amb_rest.meta and amb_rest_CA.meta with OPENIV to clarify requirements).
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK._TASK_START_SCENARIO_IN_PLACE_2(System.Int32,System.UInt64,System.String,System.Int32,System.Boolean,System.Single,System.Boolean)">
            <summary>
            Takes scenario point handle instead of hash
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_COMBAT_HATED_TARGETS_IN_AREA(System.Int32,System.Single,System.Single,System.Single,System.Single,System.Int32,System.UInt64)">
            <summary>
            Despite its name, it only attacks ONE hated target. The one closest to the specified position.
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_COMBAT_HATED_TARGETS_AROUND_PED(System.Int32,System.Single,System.Int32,System.UInt64)">
            <summary>
            Despite its name, it only attacks ONE hated target. The one closest hated target.
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_WHISTLE_ANIM(System.Int32,System.UInt32,System.UInt32)">
            <summary>
            https://github.com/femga/rdr3_discoveries/blob/master/AI/EVENTS/aud_ped_whistle_types.lua
            p2: UNSPECIFIED
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.IS_PED_IN_WRITHE(System.Int32)">
            <summary>
            This native checks if a ped is on the ground, in pain from a (gunshot) wound.
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_STAY_IN_COVER(System.Int32)">
            <summary>
            Makes the ped run to take cover
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_GO_TO_ENTITY_WHILE_AIMING_AT_ENTITY(System.Int32,System.UInt64,System.UInt64,System.UInt64,System.UInt64,System.UInt64,System.UInt64,System.UInt64,System.UInt64,System.UInt64,System.UInt64)">
            <summary>
            shootatEntity:
            If true, peds will shoot at Entity till it is dead.
            If false, peds will just walk till they reach the entity and will cease shooting.
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.SET_HIGH_FALL_TASK(System.Int32,System.Int32,System.Int32,System.Int32)">
            <summary>
            Makes the ped ragdoll like when falling from a great height
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_FORCE_MOTION_STATE(System.Int32,System.UInt32,System.Boolean)">
            <summary>
            motionStateHash: see FORCE_PED_MOTION_STATE
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK._GET_TASK_MOVE_NETWORK_ID(System.Int32)">
            <summary>
            Returns hash of the underlying move network def, see move_networks.xml
            https://alloc8or.re/rdr3/doc/misc/move_networks.txt
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.IS_PED_BEING_ARRESTED(System.Int32)">
            <summary>
            This function is hard-coded to always return false.
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_DUEL(System.Int32,System.UInt64,System.Single,System.Int32,System.Single,System.Int32,System.Single,System.Single,System.Single,System.Single,System.Int32)">
            <summary>
            Params: p4 either 0.2f, 0.25f, 0.31f, 0.4f
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_CARRIABLE(System.Int32,System.UInt32,System.Int32,System.Int32,System.Int32)">
            <summary>
            carriableSlot:
             7 > Back of a horse
             6 > Right side of a horse
             5 > Left side of a horse
            flags:
             512: enables the prompt being the name of the item when using a generic item
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK._SET_HOGTIE_ESCAPE_TIMER(System.Int32,System.Single)">
            <summary>
            Sets the time it takes for a hogtied ped to escape
            -1.0f for ped to never escape
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK._REQUEST_HERB_COMPOSITE_ASSET(System.UInt32)">
            <summary>
            https://github.com/femga/rdr3_discoveries/tree/master/objects/composites
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.ARE_COMPOSITE_LOOTABLE_ENTITY_DEF_ASSETS_LOADED(System.UInt32)">
            <summary>
            Returns true when requested asset is loaded
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK._CREATE_HERB_COMPOSITES(System.UInt32,System.Single,System.Single,System.Single,System.Single,System.Int32,System.UInt64*,System.Int32)">
            <summary>
            groundSetting: 0: spawn on ground, 2 (1?): do not spawn on ground
            p7: -1 in R* Scripts
            Returns compositeId
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK._DELETE_PATCH_OBJECTS_FROM_HERB_COMPOSITES(System.Int32,System.Boolean)">
            <summary>
            Params: p1 is always false except in script nb_egg_protector
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK._GET_HERB_COMPOSITE_NUM_ENTITIES(System.Int32,System.UInt64*)">
            <summary>
            Flowers, Stalks or whatever the composite has
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.UNHOGTIE_PED(System.Int32,System.Int32,System.UInt32,System.String,System.String,System.Single)">
            <summary>
            getupSetHash: see nm_blend_out_sets.meta
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK._IS_HAT_BEING_PICKED_UP(System.Int32)">
            <summary>
            Returns true while a hat is being picked up
            _IS_A* - _IS_D*
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK._IS_HAT_BEING_PICKED_UP_2(System.Int32)">
            <summary>
            Returns true while a hat is being picked up. Similar to 0x11CD066F54DA0133
            _IS_A* - _IS_D*
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK._TASK_EQUIP_HAT(System.Int32,System.Int32)">
            <summary>
            _A*
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.SET_ENHANCED_BREAK_FREE(System.Int32,System.Boolean,System.String)">
            <summary>
            clipset: CLIPSET@MECH_HOGTIE@HUMAN@BREAKOUT_MG@GROUND, CLIPSET@MECH_HOGTIE@HUMAN@BREAKOUT_MG@SHOULDER, CLIPSET@MECH_HOGTIE@HUMAN@BREAKOUT_MG@MOUNT
            clipset can also be 0
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_MELEE(System.Int32,System.Int32,System.UInt32,System.UInt64,System.UInt64,System.Single,System.UInt64,System.Single)">
            <summary>
            Params: p2: AR_TAKEDOWN_FRONT, AR_EXECUTION_FRONT, 0 in R* Scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_GRAPPLE(System.Int32,System.Int32,System.UInt32,System.Int32,System.Single,System.Int32,System.Int32)">
            <summary>
            grappleStyle: AR_GRAPPLE_MOUNT_STANDING_FROM_FRONT, AR_GRAPPLE_MOUNT_STANDING_FROM_RIGHT, AR_GRAPPLE_MOUNT_STANDING_FROM_BACK, AR_GRAPPLE_MOUNT_STANDING_FROM_LEFT, AR_GRAPPLE_MOUNT_FROM_FRONT, AR_WOLF_EXECUTION_ENTER_FROM_BACK, AR_GRAPPLE_DRAG_FRONT_ON_ASS, AR_GRAPPLE_FRONT_FROM_LEFT_FAR, AR_BEAR_CHALLENGE_FRONT, AR_GRAPPLE_FRONT_FROM_FRONT, AR_GRAPPLE_MOUNT_FACEUP_FROM_FRONT
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK._0x41D1331AFAD5A091(System.Int32,System.Int32,System.UInt64)">
            <summary>
            _SET_PED_*
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK._GET_TASK_FISHING(System.Int32,System.UInt64*)">
            <summary>
            Fishing Research: https://pastebin.com/NmK5ZLVs
            Only used in R* Scripts fishing_core and av_fishing_river
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK._SET_TASK_FISHING(System.Int32,System.UInt64*)">
            <summary>
            Only used in R* Scripts fishing_core and av_fishing_river
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_SWAP_FISHING_BAIT(System.Int32,System.String,System.Boolean)">
            <summary>
```
