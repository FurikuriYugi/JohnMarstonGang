<!-- ScriptHookRDRNetAPI lines 10501-10750 of 30005 -->
```csharp
            Only used in R* SP Scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.STATS._0x302E71C1D9EE75B9(System.UInt64*,System.UInt32,System.Int32*)">
            <summary>
            statId: see STAT_ID_IS_VALID
            </summary>
        </member>
        <member name="M:RDR2.Native.STATS._0x4DAC398297981B87(System.Int32)">
            <summary>
            Only used in R* SP Scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.STATS.CHAL_IS_GOAL_ACTIVE(System.UInt32,System.UInt32)">
            <summary>
            https://github.com/femga/rdr3_discoveries/blob/master/AI/EVENTS/challenge_goals.lua
            </summary>
        </member>
        <member name="M:RDR2.Native.STATS._0x4F2D5FA23DB992DE">
            <summary>
            Only used in R* Script net_char_creator
            </summary>
        </member>
        <member name="M:RDR2.Native.STATS._0x4E463A3CDEFFFE96">
            <summary>
            Only used in R* Script net_char_creator
            </summary>
        </member>
        <member name="M:RDR2.Native.STREAMING.REQUEST_MODEL(System.UInt32,System.Boolean)">
            <summary>
            Request a model to be loaded into memory.
            </summary>
        </member>
        <member name="M:RDR2.Native.STREAMING.HAS_MODEL_LOADED(System.UInt32)">
            <summary>
            Checks if the specified model has loaded into memory.
            </summary>
        </member>
        <member name="M:RDR2.Native.STREAMING.SET_MODEL_AS_NO_LONGER_NEEDED(System.UInt32)">
            <summary>
            Marks the model as no longer needed.
            </summary>
        </member>
        <member name="M:RDR2.Native.STREAMING.IS_MODEL_IN_CDIMAGE(System.UInt32)">
            <summary>
            Returns whether the specified model exists in the game.
            </summary>
        </member>
        <member name="M:RDR2.Native.STREAMING.IS_MODEL_VALID(System.UInt32)">
            <summary>
            Returns whether the specified model is valid
            </summary>
        </member>
        <member name="M:RDR2.Native.STREAMING.IS_MODEL_A_VEHICLE(System.UInt32)">
            <summary>
            Returns whether the specified model represents a vehicle.
            </summary>
        </member>
        <member name="M:RDR2.Native.STREAMING.HAS_CLIP_SET_LOADED(System.String)">
            <summary>
            Alias for HAS_ANIM_SET_LOADED.
            </summary>
        </member>
        <member name="M:RDR2.Native.STREAMING.REMOVE_CLIP_SET(System.String)">
            <summary>
            Alias for REMOVE_ANIM_SET.
            </summary>
        </member>
        <member name="M:RDR2.Native.STREAMING._GET_IPL_BOUNDING_SPHERE(System.UInt32,RDR2.Math.Vector3*,System.Single*)">
            <summary>
            Outputs IPL position and radius (previously wrongly named heading)
            https://github.com/femga/rdr3_discoveries/blob/master/imaps/imaps_with_coords_and_heading.lua
            </summary>
        </member>
        <member name="M:RDR2.Native.STREAMING.REQUEST_IPL_HASH(System.UInt32)">
            <summary>
            Old name: _REQUEST_IMAP
            </summary>
        </member>
        <member name="M:RDR2.Native.STREAMING.REQUEST_IPL_BY_HASH(System.UInt32)">
            <summary>
            Old name: _REQUEST_IMAP_2
            </summary>
        </member>
        <member name="M:RDR2.Native.STREAMING.REMOVE_IPL_HASH(System.UInt32)">
            <summary>
            Old name: _REMOVE_IMAP
            </summary>
        </member>
        <member name="M:RDR2.Native.STREAMING.REMOVE_IPL_BY_HASH(System.UInt32)">
            <summary>
            Old name: _REMOVE_IMAP_2
            </summary>
        </member>
        <member name="M:RDR2.Native.STREAMING.IS_IPL_ACTIVE_HASH(System.UInt32)">
            <summary>
            Old name: _IS_IMAP_ACTIVE
            </summary>
        </member>
        <member name="M:RDR2.Native.STREAMING.IS_IPL_ACTIVE_BY_HASH(System.UInt32)">
            <summary>
            Old name: _IS_IMAP_ACTIVE_2
            </summary>
        </member>
        <member name="M:RDR2.Native.STREAMING._IS_POSITION_INSIDE_IPL_STREAMING_EXTENTS(System.UInt32,System.Single,System.Single,System.Single)">
            <summary>
            Returns true if IPL is streamed in (?)
            </summary>
        </member>
        <member name="M:RDR2.Native.STREAMING.SET_FOCUS_ENTITY(System.Int32)">
            <summary>
            It seems to make the entity's coords mark the point from which LOD-distances are measured. In my testing, setting a vehicle as the focus entity and moving that vehicle more than 300 distance units away from the player will make the level of detail around the player go down drastically (shadows disappear, textures go extremely low res, etc). The player seems to be the default focus entity.
            </summary>
        </member>
        <member name="M:RDR2.Native.STREAMING.SET_ALL_MAPDATA_CULLED(System.UInt64)">
            <summary>
            nullsub, doesn't do anything
            </summary>
        </member>
        <member name="M:RDR2.Native.STREAMING._0xB9B9E47EDB9D63DB">
            <summary>
            Sorts some unknown data.
            
            Likely SORT_*
            </summary>
        </member>
        <member name="M:RDR2.Native.STREAMING._0x032A14D082A9B269(System.UInt32)">
            <summary>
            _SET_E* or _SET_F*
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_PAUSE(System.Int32,System.Int32)">
            <summary>
            This tasks the ped to do nothing for the specified amount of milliseconds.
            This is useful if you want to add a delay between tasks when using a sequence task.
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_STAND_STILL(System.Int32,System.Int32)">
            <summary>
            Makes the specified ped stand still for (time) milliseconds.
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_DISEMBARK_NEAREST_TRAIN_CARRIAGE(System.Int32,System.Single,System.Int32)">
            <summary>
            flags: See TASK_ENTER_VEHICLE
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_ENTER_VEHICLE(System.Int32,System.Int32,System.Int32,System.Int32,System.Single,System.Int32,System.UInt64)">
            <summary>
            flags: https://github.com/Halen84/RDR3-Native-Flags-And-Enums/tree/main/eEnterExitVehicleFlags
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_LEAVE_VEHICLE(System.Int32,System.Int32,System.Int32,System.Int32)">
            <summary>
            flags: See TASK_ENTER_VEHICLE
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_MOUNT_ANIMAL(System.Int32,System.Int32,System.Int32,System.Int32,System.Single,System.Int32,System.UInt64,System.UInt64)">
            <summary>
            timer: in ms, if it reaches 0 it will auto warp the ped on the horse
            mountStyle: See TASK_ENTER_VEHICLE
            Flags will still apply to mountStyle
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_DISMOUNT_ANIMAL(System.Int32,System.Int32,System.UInt64,System.UInt64,System.UInt64,System.Int32)">
            <summary>
            Dismounts the ped from the animal it's mounted on. taskFlag affects what side the rider gets off. p2-p5 are almost always 0.
            flags: See TASK_ENTER_VEHICLE
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_MOVE_FOLLOW_ROAD_USING_NAVMESH(System.Int32,System.Single,System.Single,System.Single,System.Single,System.UInt64)">
            <summary>
            Params: moveBlendRatio commonly 1.25f, p5 is always 0 in R* Scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_ACHIEVE_HEADING(System.Int32,System.Single,System.Int32)">
            <summary>
            Makes the specified ped achieve the specified heading.
            
            pedHandle: The handle of the ped to assign the task to.
            heading: The desired heading.
            timeout: The time, in milliseconds, to allow the task to complete. If the task times out, it is canceled, and the ped will stay at the heading it managed to reach in the time.
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_FLUSH_ROUTE">
            <summary>
            Clears the current point route. Call this before TASK_EXTEND_ROUTE and TASK_FOLLOW_POINT_ROUTE.
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_EXTEND_ROUTE(System.Single,System.Single,System.Single)">
            <summary>
            Adds a new point to the current point route. Call TASK_FLUSH_ROUTE before the first call to this. Call TASK_FOLLOW_POINT_ROUTE to make the Ped go the route.
            
            A maximum of 8 points can be added.
            </summary>
        </member>
        <!-- Badly formed XML comment ignored for member "M:RDR2.Native.TASK.TASK_ENTER_ANIM_SCENE(System.Int32,System.Int32,System.String,System.String,System.Single,System.Boolean,System.Int32,System.Int32,System.Single)" -->
        <member name="M:RDR2.Native.TASK.TASK_GO_TO_WHISTLE(System.Int32,System.Int32,System.Int32)">
            <summary>
            enum eWhistleType
            {
            	WHISTLE_MAIN,
            	WHISTLE_SECONDARY,
            	WHISTLE_DOUBLE,
            	WHISTLE_URGENT,
            	WHISTLE_LONG
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK._TASK_FLEE_FROM_COORD(System.UInt64,System.UInt64,System.UInt64,System.UInt64,System.UInt64,System.UInt64,System.UInt64,System.UInt64,System.UInt64,System.UInt64,System.UInt64,System.UInt64)">
            <summary>
            fleeType: see TASK_FLEE_COORD
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK._TASK_FLEE_FROM_PED(System.Int32,System.Int32,System.Single,System.Single,System.Single,System.Single,System.Int32,System.Int32,System.Single,System.Int32)">
            <summary>
            fleeType: see TASK_FLEE_COORD
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_SMART_FLEE_COORD(System.Int32,System.Single,System.Single,System.Single,System.Single,System.Int32,System.Int32,System.Single)">
            <summary>
            Makes the specified ped flee the specified distance from the specified position.
            fleeType: see TASK_FLEE_COORD
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_SMART_FLEE_PED(System.Int32,System.Int32,System.Single,System.Int32,System.Int32,System.Single,System.Int32)">
            <summary>
            Makes a ped run away from another ped (fleeFromTarget)
            
            fleeDistance = ped will flee this distance
            fleeTime = ped will flee for this amount of time, set to "-1" to flee forever
            fleeType = see TASK_FLEE_COORD, can be 0, R* Scripts: fm_mission/race_controller: 66048; fme_escaped_convicts: 2260992, 2523136, 2359296; la_alligator/fox: 2097152; net_fetch: 17301536; net_stable_mount: 540928
            fleeSpeed = mostly 3f, rarely 1f in R* Scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_FLEE_COORD(System.Int32,System.Single,System.Single,System.Single,System.Int32,System.Int32,System.Single,System.Int32,System.Int32)">
            <summary>
            Params: p5 = some flag?, p6 = -1.0f, p8 = 0 in R* Scripts
            fleeStyle: https://github.com/Halen84/RDR3-Native-Flags-And-Enums/tree/main/eFleeStyle
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.TASK_FLEE_PED(System.Int32,System.Int32,System.Int32,System.Int32,System.Single,System.Int32,System.Int32)">
            <summary>
            Params: p4 = -1.0f, p5 = -1, p6 = 0 in R* Scripts
            fleeStyle: see TASK_FLEE_COORD
            </summary>
        </member>
        <member name="M:RDR2.Native.TASK.ADD_FLEE_TARGET_PED(System.Int32,System.Int32,System.Single)">
            <summary>
            Params: p2 is always -1.f in R* Scripts
```
