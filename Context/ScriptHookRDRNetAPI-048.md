<!-- ScriptHookRDRNetAPI lines 11751-12000 of 30005 -->
```csharp
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._SET_MISSION_TRAIN_WARP_TO_COORDS(System.Int32,System.Single,System.Single,System.Single,System.Boolean)">
            <summary>
            Notice: BOOL p4 was wrongly named takePassengers (?)
            Can be used to rotate the train by setting the BOOL direction
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.IS_VEHICLE_DOOR_FULLY_OPEN(System.Int32,System.Int32)">
            <summary>
            doorId: see SET_VEHICLE_DOOR_SHUT
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.SET_VEHICLE_ENGINE_ON(System.Int32,System.Boolean,System.Boolean)">
            <summary>
            Starts or stops the engine on the specified vehicle.
            
            vehicle: The vehicle to start or stop the engine on.
            value: true to turn the vehicle on; false to turn it off.
            instantly: if true, the vehicle will be set to the state immediately; otherwise, the current driver will physically turn on or off the engine.
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.SET_VEHICLE_DOOR_CONTROL(System.Int32,System.Int32,System.Int32,System.Single)">
            <summary>
            doorId: see SET_VEHICLE_DOOR_SHUT
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.SET_VEHICLE_DOOR_LATCHED(System.Int32,System.Int32,System.Boolean,System.Boolean,System.Boolean)">
            <summary>
            doorId: see SET_VEHICLE_DOOR_SHUT
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.SET_VEHICLE_DOOR_SHUT(System.Int32,System.Int32,System.Boolean)">
            <summary>
            doorId: enum eDoorId
            {
            	VEH_EXT_DOOR_INVALID_ID = -1,
            	VEH_EXT_DOOR_DSIDE_F,
            	VEH_EXT_DOOR_DSIDE_M,
            	VEH_EXT_DOOR_DSIDE_M1,
            	VEH_EXT_DOOR_DSIDE_M2,
            	VEH_EXT_DOOR_DSIDE_R,
            	VEH_EXT_DOOR_PSIDE_F,
            	VEH_EXT_DOOR_PSIDE_M,
            	VEH_EXT_DOOR_PSIDE_M1,
            	VEH_EXT_DOOR_PSIDE_M2,
            	VEH_EXT_DOOR_PSIDE_R,
            	VEH_EXT_BONNET,
            	VEH_EXT_BOOT
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.SET_VEHICLE_DOOR_BROKEN(System.Int32,System.Int32,System.Boolean)">
            <summary>
            doorId: see SET_VEHICLE_DOOR_SHUT
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.SET_VEHICLE_IS_CONSIDERED_BY_PLAYER(System.Int32,System.Boolean)">
            <summary>
            Setting this to false, makes the specified vehicle to where if you press Y your character doesn't even attempt the animation to enter the vehicle. Hence it's not considered aka ignored.
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.SET_DOOR_ALLOWED_TO_BE_BROKEN_OFF(System.Int32,System.Int32,System.Boolean)">
            <summary>
            doorId: see SET_VEHICLE_DOOR_SHUT
            
            Old name: _SET_VEHICLE_DOOR_CAN_BREAK
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._GET_TRAIN_DIRECTION_FROM_INDEX(System.Int32)">
            <summary>
            https://i.imgur.com/1rHibjW.jpg
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._0x09034479E6E3E269(System.Int32,System.UInt32*,System.Int32*)">
            <summary>
            _RESET_VEHICLE_*
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._GET_TRAIN_TRACK_JUNCTION_AT_COORDS(System.UInt32,System.Single,System.Single,System.Single,System.Int32*)">
            <summary>
            Outputs junctionIndex, to be used with 0xE6C5E2125EB210C1
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._SET_TRAIN_TRACK_JUNCTION_SWITCH(System.UInt32,System.Int32,System.Boolean)">
            <summary>
            trainTrack: FREIGHT_GROUP, TRAINS3, BRAITHWAITES2_TRACK_CONFIG, TRAINS_OLD_WEST01, TRAINS_OLD_WEST03, TRAINS_NB1, TRAINS_INTERSECTION1_ANN
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._0x3ABFA128F5BF5A70(System.UInt32,System.Int32,System.Boolean)">
            <summary>
            Called together with 0xE6C5E2125EB210C1 in R* Script medium_update
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._DETACH_WAGON_ENTITY_FROM_TRAIN(System.Int32)">
            <summary>
            Only used in train_robbery4 R* Script
            _C* - _DEL*
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._0x1180A2974D251B7B(System.Int32)">
            <summary>
            Returns p1 for 0xBA958F68031DDBFC (stationIndex)
            _GET_N* (NEAREST_STATION_FOR_TRAIN?)
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.GET_CURRENT_STATION_FOR_TRAIN(System.Int32)">
            <summary>
            Returns p1 for 0xBA958F68031DDBFC (stationIndex)
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._0x9CC94A948EAF5372(System.Int32,System.Int32)">
            <summary>
            _GET_P - _GET_T*
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._GET_CURRENT_TRACK_FOR_TRAIN(System.Int32)">
            <summary>
            Returns p0 for 0xBA958F68031DDBFC (trackIndex)
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._GET_STATION_COORS_FROM_TRAIN_STATION_DATA(System.Int32,System.Int32)">
            <summary>
            Returns Coords of vStation
            p0 - NET_TRAIN_MANAGER_GET_TRAIN_STATION_DATA
            _GET_P* - _GET_T*
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._TRIGGER_TRAIN_WHISTLE(System.Int32,System.String,System.Boolean,System.Boolean)">
            <summary>
            whistleSequence: ACKNOWLEDGE, BACKING_UP, CROSSING, DANGER, MOVING, NEXT_STATION, PASSING, STOPPED
            p2 = true seems to mute the sound
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.SET_VEHICLE_FIXED(System.Int32)">
            <summary>
            This fixes a vehicle.
            If the vehicle's engine's broken then you cannot fix it with this native.
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.SET_VEHICLE_LIMIT_SPEED_WHEN_PLAYER_INACTIVE(System.Int32,System.Boolean)">
            <summary>
            _SET_VEHICLE_LI*
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.SET_VEHICLE_STEER_BIAS(System.Int32,System.Single)">
            <summary>
            Locks the vehicle's steering to the desired angle, explained below.
            
            Requires to be called onTick. Steering is unlocked the moment the function stops being called on the vehicle.
            
            Steer bias:
            -1.0 = full right
            0.0 = centered steering
            1.0 = full left
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.SET_VEHICLE_EXTRA(System.Int32,System.Int32,System.Boolean)">
            <summary>
            Note: only some vehicle have extras
            https://github.com/femga/rdr3_discoveries/blob/master/vehicles/vehicle_modding/vehicle_extras.lua
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.SET_VEHICLE_DAMAGE(System.Int32,System.Single,System.Single,System.Single,System.Single,System.Single,System.Boolean)">
            <summary>
            Apply damage to vehicle at a location. Location is relative to vehicle model (not world).
            
            Radius of effect damage applied in a sphere at impact location
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.GET_VEHICLE_ENGINE_HEALTH(System.Int32)">
            <summary>
            Returns 1000.0 if the function is unable to get the address of the specified vehicle or if it's not a vehicle.
            
            Minimum: -4000
            Maximum: 1000
            
            -4000: Engine is destroyed
            0 and below: Engine catches fire and health rapidly declines
            300: Engine is smoking and losing functionality
            1000: Engine is perfect
            </summary>
        </member>
        <!-- Badly formed XML comment ignored for member "M:RDR2.Native.VEHICLE.SET_VEHICLE_ENGINE_HEALTH(System.Int32,System.Single)" -->
        <member name="M:RDR2.Native.VEHICLE.GET_VEHICLE_PETROL_TANK_HEALTH(System.Int32)">
            <summary>
            1000 is max health
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.SET_VEHICLE_PETROL_TANK_HEALTH(System.Int32,System.Single)">
            <summary>
            1000 is max health
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.IS_VEHICLE_STUCK_TIMER_UP(System.Int32,System.Int32,System.Int32)">
            <summary>
            VEH_STUCK_ON_ROOF = 0,
            VEH_STUCK_ON_SIDE,
            VEH_STUCK_HUNG_UP,
            VEH_STUCK_JAMMED
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.START_VEHICLE_HORN(System.Int32,System.Int32,System.UInt32,System.Boolean)">
            <summary>
            Sounds the horn for the specified vehicle.
            
            vehicle: The vehicle to activate the horn for.
            mode: The hash of "NORMAL" or "HELDDOWN". Can be 0.
            duration: The duration to sound the horn, in milliseconds.
            
            Note: If a player is in the vehicle, it will only sound briefly.
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.SET_VEHICLE_HAS_STRONG_AXLES(System.Int32,System.Boolean)">
            <summary>
            if true, axles won't bend.
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._SET_VEHICLE_SNOW_LEVEL(System.Int32,System.Single)">
            <summary>
            snowLevel: 0.0 - 1.0
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._SET_VEHICLE_WET_LEVEL(System.Int32,System.Single)">
            <summary>
            wetLevel: 0.0 - 1.0
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._SET_VEHICLE_TINT(System.Int32,System.Int32)">
            <summary>
            https://github.com/femga/rdr3_discoveries/blob/master/vehicles/vehicle_modding/vehicle_tints.lua
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._SET_VEHICLE_LIVERY(System.Int32,System.Int32)">
            <summary>
            https://github.com/femga/rdr3_discoveries/blob/master/vehicles/vehicle_modding/vehicle_liveries.lua
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.ARE_ANY_VEHICLE_SEATS_FREE(System.Int32)">
            <summary>
            Returns false if every seat is occupied.
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.SET_VEHICLE_EXPLODES_ON_HIGH_EXPLOSION_DAMAGE(System.Int32,System.Boolean)">
            <summary>
            Sets a vehicle to be strongly resistant to explosions. p0 is the vehicle; set p1 to false to toggle the effect on/off.
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.GET_VEHICLE_TRAILER_VEHICLE(System.Int32,System.Int32*)">
            <summary>
```
