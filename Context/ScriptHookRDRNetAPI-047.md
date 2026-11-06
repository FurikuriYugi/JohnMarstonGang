<!-- ScriptHookRDRNetAPI lines 11501-11750 of 30005 -->
```csharp
        </member>
        <member name="M:RDR2.Native.VEHICLE.SET_VEHICLE_INDIVIDUAL_DOORS_LOCKED(System.Int32,System.Int32,System.Int32)">
            <summary>
            doorId: see SET_VEHICLE_DOOR_SHUT
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.EXPLODE_VEHICLE(System.Int32,System.Boolean,System.Boolean,System.UInt64,System.UInt64)">
            <summary>
            Explodes a selected vehicle.
            
            Vehicle vehicle = Vehicle you want to explode.
            BOOL isAudible = If explosion makes a sound.
            BOOL isInvisible = If the explosion is invisible or not.
            
            First BOOL does not give any visual explosion, the vehicle just falls apart completely but slowly and starts to burn.
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._0x9868C0D0134855F7(System.UInt64)">
            <summary>
            nullsub, doesn't do anything
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.SET_BOAT_REMAINS_ANCHORED_WHILE_PLAYER_IS_DRIVER(System.Int32,System.Boolean,System.Boolean)">
            <summary>
            Old name: _SET_BOAT_FROZEN_WHEN_ANCHORED
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.SET_FORCE_LOW_LOD_ANCHOR_MODE(System.Int32,System.Boolean)">
            <summary>
            Sets boat to be anchored on spawn, called together with SET_BOAT_ANCHOR and _SET_BOAT_ANCHOR_BUOYANCY_COEFFICIENT
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.SET_BOAT_LOW_LOD_ANCHOR_DISTANCE(System.Int32,System.Single)">
            <summary>
            Value: mostly 99999.9f
            
            Old name: _SET_BOAT_MOVEMENT_RESISTANCE
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._0x9E8711C81AA17876(System.Int32,System.Boolean)">
            <summary>
            Forcing high LOD buoyancy for vehicle: p1 = false
            _SET_A*
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.SET_VEHICLE_STRONG(System.Int32,System.Boolean)">
            <summary>
            If set to true, vehicle will not take crash damage, but is still susceptible to damage from bullets and explosives
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.IS_VEHICLE_SEAT_FREE(System.Int32,System.Int32)">
            <summary>
            seatIndex: see CREATE_PED_INSIDE_VEHICLE
            Use GET_VEHICLE_MAX_NUMBER_OF_PASSENGERS(vehicle) - 1 for last seat index.
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.GET_PED_IN_VEHICLE_SEAT(System.Int32,System.Int32)">
            <summary>
            seatIndex: see CREATE_PED_INSIDE_VEHICLE
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.GET_LAST_PED_IN_VEHICLE_SEAT(System.Int32,System.Int32)">
            <summary>
            seatIndex: see CREATE_PED_INSIDE_VEHICLE
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._GET_PED_IN_DRAFT_HARNESS(System.Int32,System.Int32)">
            <summary>
            enum eDraftHarness
            {
            	DRAFT_HARNESS_LR,
            	DRAFT_HARNESS_RR,
            	DRAFT_HARNESS_LM,
            	DRAFT_HARNESS_RM,
            	DRAFT_HARNESS_LF,
            	DRAFT_HARNESS_RF,
            	DRAFT_HARNESS_COUNT
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.BRING_VEHICLE_TO_HALT(System.Int32,System.Single,System.Int32,System.Boolean)">
            <summary>
            This native makes the vehicle stop immediately
            
            distance defines how far it will travel until stopping.
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._IS_VEHICLE_BROUGHT_TO_HALT(System.Int32)">
            <summary>
            Only returns true if BRING_VEHICLE_TO_HALT is called on vehicle beforehand
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.STOP_BRINGING_VEHICLE_TO_HALT(System.Int32)">
            <summary>
            Old name: _STOP_BRING_VEHICLE_TO_HALT
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.SET_VEHICLE_DOORS_SHUT(System.Int32,System.Boolean)">
            <summary>
            Closes all doors of a vehicle:
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.SET_VEHICLE_TYRES_CAN_BURST(System.Int32,System.Boolean)">
            <summary>
            Allows you to toggle bulletproof tires.
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.SET_VEHICLE_DOOR_OPEN(System.Int32,System.Int32,System.Boolean,System.Boolean)">
            <summary>
            doorId: see SET_VEHICLE_DOOR_SHUT
            Can also be used on trains and its wagons
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.REMOVE_VEHICLE_WINDOW(System.Int32,System.Int32)">
            <summary>
            windowIndex:
            0 = Front Right Window
            1 = Front Left Window
            2 = Back Right Window
            3 = Back Left Window
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._SET_VEHICLE_DIRT_LEVEL_2(System.Int32,System.Single)">
            <summary>
            dirtLevel: 0.0 - 1.0
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._SET_VEHICLE_MUD_LEVEL(System.Int32,System.Single)">
            <summary>
            mudLevel: 0.0 - 1.0
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.SET_VEHICLE_LIGHTS(System.Int32,System.Int32)">
            <summary>
            Sets the vehicle's lights state.
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._0x331CBD247FC5DAA8(System.UInt32,System.Single,System.Single,System.Single,System.Boolean,System.Boolean)">
            <summary>
            Returns trackIndex
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._0x0516FAE561276EFC(System.Int32)">
            <summary>
            Takes value returned from 0x45853F4E17D847D5
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._GET_TRAIN_TRACK_FROM_TRAIN_VEHICLE(System.Int32)">
            <summary>
            Returns trackIndex
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._GET_TRAIN_VEHICLE_FROM_TRACK_INDEX(System.Int32)">
            <summary>
            Returns train
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._0xF05DFAF1ADFEF2CD(System.UInt32,System.Single,System.Single,System.Single,System.Boolean,System.Boolean)">
            <summary>
            _IS_* (related to CREATE_MISSION_TRAIN)
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._0x6C87F49BFA181DB5(System.Single,System.Single,System.Single)">
            <summary>
            Returns trackIndex
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._GET_TRACK_INDEX_FROM_COORDS(System.Single,System.Single,System.Single)">
            <summary>
            Returns trackIndex
            _E* - _F*
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._SET_TRAIN_MAX_SPEED(System.Int32,System.Single)">
            <summary>
            Maximum possible speed is 30.0 (108 km/h)
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._GET_TRAIN_CARRIAGE_TRAILER_NUMBER(System.Int32)">
            <summary>
            Returns iNumCars - to be used with GET_TRAIN_CARRIAGE (trailerNumber)
            _C* (O, P, Q, R)
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._GET_TRAIN_MODEL_FROM_TRAIN_CONFIG_BY_CAR_INDEX(System.UInt32,System.Int32)">
            <summary>
            Returns modelHash
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._GET_NUM_CARS_FROM_TRAIN_CONFIG(System.UInt32)">
            <summary>
            Returns amount for CAN_REGISTER_MISSION_VEHICLES
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._GET_TRAIN_CAR(System.Int32)">
            <summary>
            Returns train car, use GET_TRAIN_CARRIAGE when trailerNumber is bigger than 0
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE._SET_TRAIN_LEAVE_STATION(System.Int32)">
            <summary>
            Restarts the train
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.REQUEST_VEHICLE_RECORDING(System.Int32,System.String)">
            <summary>
            Request the vehicle recording defined by the lowercase format string "%s%03d.yvr". For example, REQUEST_VEHICLE_RECORDING(1, "FBIs1UBER") corresponds to fbis1uber001.yvr.
            For all vehicle recording/playback natives, "script" is a common prefix that usually corresponds to the script/mission the recording is used in, "recording" is its int suffix, and "id" corresponds to a unique identifier within the recording streaming module.
            (GTA) Note that only 24 recordings (hardcoded in multiple places) can ever active at a given time before clobbering begins.
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.HAS_VEHICLE_RECORDING_BEEN_LOADED(System.Int32,System.String)">
            <summary>
            See REQUEST_VEHICLE_RECORDING
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.REMOVE_VEHICLE_RECORDING(System.UInt64,System.UInt64*)">
            <summary>
            See REQUEST_VEHICLE_RECORDING
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.GET_POSITION_OF_VEHICLE_RECORDING_AT_TIME(System.Int32,System.Single,System.String)">
            <summary>
            This native does no interpolation between pathpoints. The same position will be returned for all times up to the next pathpoint in the recording.
            
            See REQUEST_VEHICLE_RECORDING
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.GET_ROTATION_OF_VEHICLE_RECORDING_AT_TIME(System.Int32,System.Single,System.String)">
            <summary>
            This native does no interpolation between pathpoints. The same rotation will be returned for all times up to the next pathpoint in the recording.
            
            See REQUEST_VEHICLE_RECORDING
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.START_PLAYBACK_RECORDED_VEHICLE(System.Int32,System.Int32,System.String,System.Boolean)">
            <summary>
            p3 is some flag related to 'trailers' (invokes CVehicle::GetTrailer).
            
            See REQUEST_VEHICLE_RECORDING
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.FORCE_PLAYBACK_RECORDED_VEHICLE_UPDATE(System.Int32,System.Boolean)">
            <summary>
            Often called after START_PLAYBACK_RECORDED_VEHICLE and SKIP_TIME_IN_PLAYBACK_RECORDED_VEHICLE; similar in use to FORCE_ENTITY_AI_AND_ANIMATION_UPDATE.
            </summary>
        </member>
        <member name="M:RDR2.Native.VEHICLE.SKIP_TIME_IN_PLAYBACK_RECORDED_VEHICLE(System.Int32,System.Single)">
            <summary>
            SET_TIME_POSITION_IN_RECORDING can be emulated by: desired_time - GET_TIME_POSITION_IN_RECORDING(vehicle)
```
