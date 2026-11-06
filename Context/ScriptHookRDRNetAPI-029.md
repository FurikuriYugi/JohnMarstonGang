<!-- ScriptHookRDRNetAPI lines 7001-7250 of 30005 -->
```csharp
        <member name="M:RDR2.Native.PATHFIND.REQUEST_PATH_NODES_IN_AREA_THIS_FRAME(System.Single,System.Single,System.Single,System.Single)">
            <summary>
            Old name: REQUEST_PATHS_PREFER_ACCURATE_BOUNDINGSTRUCT
            </summary>
        </member>
        <member name="M:RDR2.Native.PATHFIND.IS_POINT_ON_ROAD(System.Single,System.Single,System.Single,System.Int32)">
            <summary>
            Gets a value indicating whether the specified position is on a road.
            </summary>
        </member>
        <member name="M:RDR2.Native.PATHFIND.IS_NAVMESH_LOADED_IN_AREA(System.Single,System.Single,System.Single,System.Single,System.Single,System.Single)">
            <summary>
            Returns whether navmesh for the region is loaded.
            </summary>
        </member>
        <member name="M:RDR2.Native.PATHFIND._NAVMESH_QUERY_STATUS(System.UInt64)">
            <summary>
            Returns eNavMeshQueryStatus
            enum eNavMeshQueryStatus
            {
            	QS_NOT_FOUND,
            	QS_COMPLETE,
            	QS_PENDING
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.PATHFIND.GET_APPROX_FLOOR_FOR_POINT(System.Single,System.Single)">
            <summary>
            Returns CGameWorldHeightMap's minimum Z value at specified point (grid node).
            </summary>
        </member>
        <member name="M:RDR2.Native.PATHFIND.SET_IGNORE_NO_GPS_FLAG(System.Boolean)">
            <summary>
            nullsub, doesn't do anything
            </summary>
        </member>
        <member name="M:RDR2.Native.PATHFIND._0xF2A2177AC848B3A8(System.Int32,System.Int32,System.Int32)">
            <summary>
            GPS disabled zone: p1 = 0
            Clearing GPS disabled zone: p1 = 1
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.DELETE_PED(System.Int32*)">
            <summary>
            Deletes the specified ped, then sets the handle pointed to by the pointer to NULL.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x7C08E7CB8D951B70(System.Int32,System.Single)">
            <summary>
            Only used in SP
            _SET_REMOVE_PED*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.CLONE_PED_TO_TARGET(System.Int32,System.Int32)">
            <summary>
            Copies ped's components and props to targetPed.
            Can be used to clear anything from a ped by cloning it, including bullet holes.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.IS_PED_IN_VEHICLE(System.Int32,System.Int32,System.Boolean)">
            <summary>
            Gets a value indicating whether the specified ped is in the specified vehicle.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.IS_PED_IN_ANY_VEHICLE(System.Int32,System.Boolean)">
            <summary>
            Gets a value indicating whether the specified ped is in any vehicle.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.IS_PED_INJURED(System.Int32)">
            <summary>
            Gets a value indicating whether this ped's health is below its injured threshold.
            
            The default threshold is 100.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.IS_PED_FATALLY_INJURED(System.Int32)">
            <summary>
            Gets a value indicating whether this ped's health is below its fatally injured threshold. The default threshold is 100.
            If the handle is invalid, the function returns true.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._SET_PED_BLEEDOUT_PROFILE(System.Int32,System.UInt32)">
            <summary>
            Bleedout profiles:
            Animal_FastBleedout
            Animal_Generic
            Human_FastBleedout
            Human_Generic
            Human_Mission
            
            For more information, see common/data/ai/peddamageinfo.meta
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.IS_PED_RELOADING(System.Int32)">
            <summary>
            Returns whether the specified ped is reloading.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._IS_ANIMAL_CONTROLLED_BY_A_PLAYER(System.Int32)">
            <summary>
            Returns true only if it's a player ped and an animal as well.
            _IS_ANY_* - _IS_CONTROL_*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.CREATE_PED_INSIDE_VEHICLE(System.Int32,System.UInt32,System.Int32,System.Boolean,System.Boolean,System.Boolean)">
            <summary>
            seatIndex:
            enum eVehicleSeat
            {
            	VS_ANY_PASSENGER = -2,
            	VS_DRIVER,
            	VS_FRONT_RIGHT,
            	VS_BACK_LEFT,
            	VS_BACK_RIGHT,
            	VS_EXTRA_LEFT_1,
            	VS_EXTRA_RIGHT_1,
            	VS_EXTRA_LEFT_2,
            	VS_EXTRA_RIGHT_2,
            	VS_EXTRA_LEFT_3,
            	VS_EXTRA_RIGHT_3,
            	VS_NUM_SEATS
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.FORCE_ALL_HEADING_VALUES_TO_ALIGN(System.Int32)">
            <summary>
            Old name: _FREEZE_PED_CAMERA_ROTATION
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.IS_PED_FACING_PED(System.Int32,System.Int32,System.Single)">
            <summary>
            angle is ped's view cone
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.IS_PED_IN_MELEE_COMBAT(System.Int32)">
            <summary>
            Notes: The function only returns true while the ped is: 
            A.) Swinging a random melee attack (including pistol-whipping)
            
            B.) Reacting to being hit by a melee attack (including pistol-whipping)
            
            C.) Is locked-on to an enemy (arms up, strafing/skipping in the default fighting-stance, ready to dodge+counter). 
            
            You don't have to be holding the melee-targeting button to be in this stance; you stay in it by default for a few seconds after swinging at someone. If you do a sprinting punch, it returns true for the duration of the punch animation and then returns false again, even if you've punched and made-angry many peds
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x6DB875AFC584FA32(System.Int32,System.Int32)">
            <summary>
            Only used in R* SP Script winter1: p1 = 5000
            _SET_PED_M*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x9C81338B2E62CE0A(System.Int32,System.Int32,System.Int32)">
            <summary>
            If returned true: CHECK_SHOT_NEAR: Succeeded shot near
            If returned false: CHECK_SHOT_NEAR: Failed shot near
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.IS_PED_SHOOTING(System.Int32)">
            <summary>
            Returns whether the specified ped is shooting.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.TIME_SINCE_PED_LAST_SHOT(System.Int32)">
            <summary>
            Returns time since the specified ped last shot, in seconds. (fPlayerJustShotTime)
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.SET_PED_ACCURACY(System.Int32,System.Int32)">
            <summary>
            accuracy = 0-100, 100 being perfectly accurate
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.EXPLODE_PED_HEAD(System.Int32,System.UInt32)">
            <summary>
            Forces the ped to fall back and kills it.
            
            It doesn't really explode the ped's head but it kills the ped
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.ADD_ARMOUR_TO_PED(System.Int32,System.Int32)">
            <summary>
            Same as SET_PED_ARMOUR, but ADDS 'amount' to the armor the Ped already has.
            </summary>
        </member>
        <!-- Badly formed XML comment ignored for member "M:RDR2.Native.PED._0x5CA20FBE49891BBD(System.Int32,System.Int32)" -->
        <member name="M:RDR2.Native.PED.SET_PED_INTO_VEHICLE(System.Int32,System.Int32,System.Int32)">
            <summary>
            Ped: The ped to warp.
            vehicle: The vehicle to warp the ped into.
            seatIndex: see CREATE_PED_INSIDE_VEHICLE
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x606D529DADA3C940(System.Int32,System.UInt64)">
            <summary>
            Not implemented.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.IS_PED_MALE(System.Int32)">
            <summary>
            Returns true/false if the ped is/isn't male.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.IS_PED_HUMAN(System.Int32)">
            <summary>
            Returns true/false if the ped is/isn't humanoid.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.GET_VEHICLE_PED_IS_IN(System.Int32,System.Boolean)">
            <summary>
            Gets the vehicle the specified Ped is in.
            
            If the Ped is not in a vehicle and includeLastVehicle is true, the vehicle they were last in is returned.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.RESET_PED_LAST_VEHICLE(System.Int32)">
            <summary>
            Resets the value for the last vehicle driven by the Ped.
            </summary>
        </member>
        <!-- Badly formed XML comment ignored for member "M:RDR2.Native.PED._0x6E8B87139854022D(System.Int32,System.String)" -->
        <member name="M:RDR2.Native.PED._0x9E3842E5DAD69F80(System.Int32)">
            <summary>
            Only used in SP R* Script loanshark_hunter
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._SET_SCENARIO_PED_DENSITY_THIS_FRAME(System.UInt32)">
            <summary>
            Sets the scenario ped density to the given config.
            
            Valid configs:
            - BLACKWATER
            - DEFAULT
            - NEWBORDEAUX
            - RHODES
            - STRAWBERRY
            - TUMBLEWEED
            - VALENTINE
            - VANHORN
            
            See common/data/ai/densityscoringconfigs.meta for more information.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xBFA6B7731C3BAF02">
            <summary>
            Only used in R* Script tg_p (CLIENT__AMBIENT_POPULATION - Player starts the populate in region)
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.SET_PED_NON_CREATION_AREA(System.Single,System.Single,System.Single,System.Single,System.Single,System.Single)">
```
