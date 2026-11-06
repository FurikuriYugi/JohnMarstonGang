<!-- ScriptHookRDRNetAPI lines 10751-11000 of 30005 -->
```csharp
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_REACT(System.Int32,System.Int32,System.Single,System.Single,System.Single,System.String,System.Single,System.Single,System.Int32)">
            <summary>
            Makes a ped react to an entity.
            Params: reactingTo Entity can be 0, p8 is always 4
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_WANDER_STANDARD(System.Int32,System.Single,System.Int32)">
            <summary>
            Makes ped walk around the area.
            
            set p1 to 10.0f and p2 to 10 if you want the ped to walk anywhere without a duration.
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_HORSE_ACTION(System.Int32,System.Int32,System.Int32,System.UInt64)">
            <summary>
            https://github.com/femga/rdr3_discoveries/tree/master/tasks/TASK_HORSE_ACTION
            Params: p2, p3 are set to 0 in R* Scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_ANIMAL_INTERACTION(System.Int32,System.Int32,System.UInt32,System.UInt32,System.Boolean)">
            <summary>
            https://github.com/femga/rdr3_discoveries/tree/master/tasks/TASK_ANIMAL_INTERACTION
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_FOLLOW_NAV_MESH_TO_COORD(System.Int32,System.Single,System.Single,System.Single,System.Single,System.Int32,System.Single,System.Int32,System.Single)">
            <summary>
            If no timeout, set timeout to -1.
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK._SET_PED_PATH_LADDER_COST_MODIFIER(System.Int32,System.Single)">
            <summary>
            _SET_PED_PATH_P*
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_PLAY_ANIM(System.Int32,System.String,System.String,System.Single,System.Single,System.Int32,System.Int32,System.Single,System.Boolean,System.Int32,System.Boolean,System.String,System.Boolean)">
            <summary>
            https://github.com/femga/rdr3_discoveries/tree/master/animations
            flags: https://github.com/Halen84/RDR3-Native-Flags-And-Enums/tree/main/eScriptedAnimFlags
            ikFlags: https://github.com/Halen84/RDR3-Native-Flags-And-Enums/tree/main/eIkControlFlags 
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_PLAY_ANIM_ADVANCED(System.Int32,System.String,System.String,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Int32,System.Int32,System.Single,System.Int32,System.Int32,System.Int32)">
            <summary>
            flags: see TASK_PLAY_ANIM
            ikFlags: see TASK_PLAY_ANIM
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.START_TASK_ITEM_INTERACTION(System.Int32,System.UInt32,System.UInt32,System.Int32,System.Int32,System.Single)">
            <summary>
            Params: p3 = 0, 1; p5 = 0.0f, -1.0f
            https://github.com/femga/rdr3_discoveries/tree/master/tasks/TASK_ITEM_INTERACTION
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK._TASK_ITEM_INTERACTION_3(System.Int32,System.UInt32,System.UInt64*,System.UInt64,System.UInt64,System.UInt64,System.Single)">
            <summary>
            Params: p3, p4, p5, p6: 0, 0, 0, -1.0f in R* Scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK._GET_ITEM_INTERACTION_ENTITY_FROM_PED(System.Int32,System.UInt32)">
            <summary>
            item hashes: PRIMARYITEM, P_MUGCOFFEE01X_PH_R_HAND, P_BOTTLEBEER01X_PH_R_HAND
            http://prntscr.com/1qtp3bz
            https://github.com/femga/rdr3_discoveries/tree/master/tasks/TASK_ITEM_INTERACTION
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK._0x678D3226CF70B9C8(System.Int32,System.Boolean)">
            <summary>
            Only used in R* SP Script beat_washed_ashore
            Returns Object prop for TASK::_TASK_ITEM_INTERACTION_2
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_EVASIVE_ANIM(System.Int32,System.Int32,System.Int32)">
            <summary>
            Params: p2 is returned by BUILTIN::SHIFT_LEFT
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_LOOK_AT_ENTITY(System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32)">
            <summary>
            param3: duration in ms, use -1 to look forever
            param4: using 2048 is fine
            param5: using 3 is fine
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK._0x508F5053E3F6F0C4(System.Int32,System.Single,System.Single,System.Single,System.Single)">
            <summary>
            Only used in R* SP Scripts
            Params: p4 = 1.0f - 30.0f
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.GET_SEQUENCE_PROGRESS(System.Int32)">
            <summary>
            returned values:
            0 to 7 = task that's currently in progress, 0 meaning the first one.
            -1 no task sequence in progress.
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.GET_SCRIPT_TASK_STATUS(System.Int32,System.UInt32,System.Boolean)">
            <summary>
            Gets the status of a script-assigned task, and returns an int between 0-8
            taskHash: https://alloc8or.re/rdr3/doc/enums/eScriptTaskHash.txt 
            
            WAITING_TO_START_TASK = 0,
            PERFORMING_TASK
            DORMANT_TASKduration
            VACANT_STAGE
            GROUP_TASK_STAGE
            ATTRACTOR_SCRIPT_TASK_STAGE
            SECONDARY_TASK_STAGE
            TASK_NOT_FOUND
            FINISHED_TASK
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.REACT_LOOK_AT(System.Int32,System.Int32,System.Int32,System.Int32,System.Single,System.Int32,System.Int32,System.UInt64,System.UInt64)">
            <summary>
            lookIntensity: see SET_PED_SHOULD_PLAY_FLEE_SCENARIO_EXIT
            
            exitAnimation: LOOK_RETURN_GENERIC = 1,
            LOOK_RETURN_DISMISSIVE = 2,
            LOOK_RETURN_RELIEVED = 3
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_LEAVE_ANY_VEHICLE(System.Int32,System.Int32,System.Int32)">
            <summary>
            flags: See TASK_ENTER_VEHICLE
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_AIM_GUN_AT_ENTITY(System.Int32,System.Int32,System.Int32,System.Boolean,System.Int32)">
            <summary>
            duration: the amount of time in milliseconds to do the task.  -1 will keep the task going until either another task is applied, or CLEAR_ALL_TASKS() is called with the ped
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_TURN_PED_TO_FACE_ENTITY(System.Int32,System.Int32,System.Int32,System.Single,System.Single,System.Single)">
            <summary>
            duration: the amount of time in milliseconds to do the task. -1 will keep the task going until either another task is applied, or CLEAR_ALL_TASKS() is called with the ped
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_SHUFFLE_TO_NEXT_VEHICLE_SEAT(System.Int32,System.Int32)">
            <summary>
            Makes the specified ped shuffle to the next vehicle seat.
            The ped MUST be in a vehicle and the vehicle parameter MUST be the ped's current vehicle.
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_TURN_PED_TO_FACE_COORD(System.Int32,System.Single,System.Single,System.Single,System.Int32)">
            <summary>
            duration in milliseconds
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_VEHICLE_TEMP_ACTION(System.Int32,System.Int32,System.Int32,System.Int32)">
            <summary>
            Documentation from GTA V, might be the same in RDR:
            
            '1 - brake
            '3 - brake + reverse
            '4 - turn left 90 + braking
            '5 - turn right 90 + braking
            '6 - brake strong (handbrake?) until time ends
            '7 - turn left + accelerate
            '7 - turn right + accelerate
            '9 - weak acceleration
            '10 - turn left + restore wheel pos to center in the end
            '11 - turn right + restore wheel pos to center in the end
            '13 - turn left + go reverse
            '14 - turn left + go reverse
            '16 - crash the game after like 2 seconds :)
            '17 - keep actual state, game crashed after few tries
            '18 - game crash
            '19 - strong brake + turn left/right
            '20 - weak brake + turn left then turn right
            '21 - weak brake + turn right then turn left
            '22 - brake + reverse
            '23 - accelerate fast
            '24 - brake
            '25 - brake turning left then when almost stopping it turns left more
            '26 - brake turning right then when almost stopping it turns right more
            '27 - brake until car stop or until time ends
            '28 - brake + strong reverse acceleration
            '30 - performs a burnout (brake until stop + brake and accelerate)
            '31 - accelerate + handbrake
            '32 - accelerate very strong
            
            Seems to be this:
            Works on NPCs, but overrides their current task. If inside a task sequence (and not being the last task), "time" will work, otherwise the task will be performed forever until tasked with something else
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_VEHICLE_DRIVE_TO_DESTINATION(System.Int32,System.Int32,System.Single,System.Single,System.Single,System.Single,System.Int32,System.Int32,System.Single,System.Single,System.Boolean)">
            <summary>
            flags: 67108864, 2097152, 524564, 524675 (eDrivingFlags)
            p7 = 6 or 3
            p8 = x coordinate
            p9 - 8.f
            p10 = false
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK._TASK_VEHICLE_DRIVE_TO_DESTINATION_2(System.Int32,System.Single,System.Single,System.Single,System.Single,System.Int32,System.Int32,System.Single,System.Single)">
            <summary>
            Tasks vehicle towards owner
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK._TASK_VEHICLE_FLEE_ON_CLEANUP(System.Int32,System.Single,System.Single,System.Single,System.Single,System.UInt32)">
            <summary>
            Vehicle Auto Drive (?)
            p1/p2/p3: usually 1f, 1f, 0f or 0f, 0f, 0f
            Speed: usually 8f
            Types: 1148979456 (task with flee), 1148979587 (dismissing the vehicle)
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_VEHICLE_DRIVE_STRAIGHT_TO_POINT(System.Int32,System.Int32,System.Single,System.Single,System.Single,System.Single,System.Single,System.Int32)">
            <summary>
            Old name: _TASK_VEHICLE_DRIVE_TO_POINT
            flag: 524419 and 0 in shop_horse_shop R* Script
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK._TASK_VEHICLE_DRIVE_TO_POINT_2(System.Int32,System.Single,System.Single,System.Single,System.Single,System.Single,System.UInt64)">
            <summary>
            Params: p4 = 3.f or 8.f, p5 = 0.25f, p6 = 0 in R* Scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_VEHICLE_MISSION_PED_TARGET(System.Int32,System.Int32,System.Int32,System.Int32,System.Single,System.Int32,System.Single,System.Single,System.Boolean)">
            <summary>
            See TASK_VEHICLE_MISSION
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_CLIMB(System.Int32,System.Boolean)">
            <summary>
            Climbs or vaults the nearest thing.
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.CLEAR_PED_TASKS_IMMEDIATELY(System.Int32,System.Boolean,System.Boolean)">
            <summary>
            Immediately stops the pedestrian from whatever it's doing. They stop fighting, animations, etc. they forget what they were doing.
            
            resetCrouch TRUE = ped will stand up if crouching, FALSE = ped will remain crouching if crouched
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_GOTO_ENTITY_AIMING(System.Int32,System.Int32,System.Single,System.Single)">
            <summary>
            ped = Ped you want to perform this task.
            target = the Entity they should aim at.
            distanceToStopAt = distance from the target, where the ped should stop to aim.
            StartAimingDist = distance where the ped should start to aim.
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.DOES_SCRIPTED_COVER_POINT_EXIST_AT_COORDS(System.UInt64,System.UInt64,System.UInt64,System.UInt64)">
            <summary>
            Checks if there is a cover point at position
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_PUT_PED_DIRECTLY_INTO_MELEE(System.Int32,System.Int32,System.UInt32,System.Single,System.Single,System.Boolean,System.Int32)">
```
