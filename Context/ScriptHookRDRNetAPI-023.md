<!-- ScriptHookRDRNetAPI lines 5501-5750 of 30005 -->
```csharp
            Consider using this faster native instead: BUILTIN::VDIST - DVIST always takes in consideration the 3D coordinates.
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.GET_HEADING_FROM_VECTOR_2D(System.Single,System.Single)">
            <summary>
            dx = x1 - x2
            dy = y1 - y2
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC._SET_BIT_FLAG(System.UInt64*,System.Int32)">
            <summary>
            Similar to SET_BIT but specifically designed for large (>32 flags) bit flag sets.
            The flags are stored in an int array where each int has the ability to hold 32 flags.
            Flags 0-31 would be stored in the first int, flags 32-63 in the second int, etc.
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.GET_HASH_KEY(System.String)">
            <summary>
            Computes a hash for the given string. It is hashed using Jenkins' One-at-a-Time hash algorithm (https://en.wikipedia.org/wiki/Jenkins_hash_function)
            Note: this implementation is case-insensitive.
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC._GET_EASING_CURVE_VALUE(System.Single,System.Single,System.Single,System.Int32)">
            <summary>
            https://easings.net/
            
            enum class eEasingCurveType
            {
            	TYPE_LINEAR,
            	TYPE_QUADRATIC_IN,
            	TYPE_QUADRATIC_OUT,
            	TYPE_QUADRATIC_INOUT,
            	TYPE_CUBIC_IN,
            	TYPE_CUBIC_OUT,
            	TYPE_CUBIC_INOUT,
            	TYPE_QUARTIC_IN,
            	TYPE_QUARTIC_OUT,
            	TYPE_QUARTIC_INOUT,
            	TYPE_QUINTIC_IN,
            	TYPE_QUINTIC_OUT,
            	TYPE_QUINTIC_INOUT,
            	TYPE_EXPONENTIAL_IN,
            	TYPE_EXPONENTIAL_OUT,
            	TYPE_EXPONENTIAL_INOUT,
            	TYPE_SINE_IN,
            	TYPE_SINE_OUT,
            	TYPE_SINE_INOUT,
            	TYPE_CIRCULAR_IN,
            	TYPE_CIRCULAR_OUT,
            	TYPE_CIRCULAR_INOUT,
            	TYPE_BOUNCE_IN,
            	TYPE_BOUNCE_OUT,
            	TYPE_BOUNCE_INOUT,
            	TYPE_CUSTOM
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.CLEAR_AREA(System.Single,System.Single,System.Single,System.Single,System.Int32)">
            <summary>
            flag |= 1 | 1048576 | 524288
            ALL_BASE, PROJECTILES, AMBIENT_POPULATION, BROADCAST
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.SET_FADE_IN_AFTER_DEATH_ARREST(System.Boolean)">
            <summary>
            Sets whether the game should fade in after the player dies or is arrested.
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.SHOULD_USE_METRIC_MEASUREMENTS">
            <summary>
            Returns whether the game's measurement system is set to metric.
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC._SHOULD_USE_METRIC_MEASUREMENTS_2">
            <summary>
            Same as SHOULD_USE_METRIC_MEASUREMENTS
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.IS_PROJECTILE_IN_AREA(System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Boolean)">
            <summary>
            Determines whether there is a projectile within the specified coordinates. The coordinates form a rectangle.
            
            ownedByPlayer = only projectiles fired by the player will be detected.
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.IS_PROJECTILE_TYPE_IN_AREA(System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Int32,System.Boolean)">
            <summary>
            Determines whether there is a projectile of a specific type within the specified coordinates. The coordinates form a rectangle.
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.HAS_BULLET_IMPACTED_IN_AREA(System.Single,System.Single,System.Single,System.Single,System.Boolean,System.Boolean)">
            <summary>
            p3 - possibly radius?
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.IS_ORBIS_VERSION">
            <summary>
            Hardcoded to return false.
            Checks for PS4 Game Build.
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.IS_DURANGO_VERSION">
            <summary>
            Hardcoded to return false.
            Checks for XBOXONE Game Build.
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.IS_PC_VERSION">
            <summary>
            Hardcoded to return true.
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.IS_STADIA_VERSION">
            <summary>
            Hardcoded to return false.
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.IS_STRING_NULL_OR_EMPTY_OR_SPACES(System.String)">
            <summary>
            Returns true if the entire string consists only of space characters.
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.STRING_TO_INT(System.String,System.Int32*)">
            <summary>
            Returns false if it's a null or empty string or if the string is too long. outInteger will be set to -999 in that case.
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC._INT_TO_STRING(System.Int32,System.String,System.String)">
            <summary>
            Note: the buffer should be exactly 32 bytes long
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.VAR_STRING(System.Int32,RDR2.Native.InputArgument[])">
            <summary>
            Note: The first bit in 'flags' must not be set.
            It is also required to pass at least one extra argument (this must be a text label string or hash).
            When passing a hash, flags should be 0.
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC._CREATE_COLOR_STRING(System.Int32)">
            <summary>
            Returns a formatted string (0x%x)
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.SET_GAME_PAUSED(System.Boolean)">
            <summary>
            Make sure to call this from the correct thread if you're using multiple threads because all other threads except the one which is calling SET_GAME_PAUSED will be paused.
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.SET_CHEAT_ACTIVE(System.Int32)">
            <summary>
            Cheats are GTA IV cheats:
            
            0 = unknown
            1 = unknown (same as 0)
            2 = Max Health and Armor
            3 = Raise Wanted Level
            4 = Lower Wanted Level
            5 = unknown (does nothing)
            6 = Change Weather
            7 = Spawn Annihilator
            8 = Spawn NRG 900
            9 = Spawn FBI
            10 = Spawn Jetmax
            11 = Spawn Comet
            12 = Spawn Turismo
            13 = Spawn Cognoscenti
            14 = Spawn Super GT
            15 = Spawn Sanchez
            
            Initially used in Max Payne 3, that's why we know the name.
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC._0xB711EB4BC8D06013">
            <summary>
            nullsub, doesn't do anything
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.POPULATE_NOW">
            <summary>
            spawns a few distant/out-of-sight peds, vehicles, animals etc each time it is called
            </summary>
        </member>
        <!-- Badly formed XML comment ignored for member "M:RDR2.Native.MISC._QUEUE_SAVEGAME_OPERATION(System.Int32)" -->
        <member name="M:RDR2.Native.MISC._GET_STATUS_OF_SAVEGAME_OPERATION(System.Int32)">
            <summary>
            Only 0 and 1 are valid for p0, higher values causes the native to return 2.
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.COPY_SCRIPT_STRUCT(System.UInt64*,System.UInt64*,System.Int32)">
            <summary>
            Old name: _COPY_MEMORY
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.ENABLE_DISPATCH_SERVICE(System.Int32,System.Boolean)">
            <summary>
            enum DispatchType
            {
            	DT_Invalid,
            	DT_PoliceAutomobile,
            	DT_PoliceHelicopter,
            	DT_FireDepartment,
            	DT_SwatAutomobile,
            	DT_AmbulanceDepartment,
            	DT_PoliceRiders,
            	DT_PoliceVehicleRequest,
            	DT_PoliceRoadBlock,
            	DT_PoliceAutomobileWaitPulledOver,
            	DT_PoliceAutomobileWaitCruising,
            	DT_Gangs,
            	DT_SwatHelicopter,
            	DT_PoliceBoat,
            	DT_ArmyVehicle,
            	DT_OnFoot,
            	DT_PoliceDogs
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.BLOCK_DISPATCH_SERVICE_RESOURCE_CREATION(System.Int32,System.Boolean)">
            <summary>
            dispatchService: see ENABLE_DISPATCH_SERVICE
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.CREATE_INCIDENT(System.Int32,System.Single,System.Single,System.Single,System.Int32,System.Single,System.Int32*,System.UInt64,System.UInt64)">
            <summary>
            dispatchService: see ENABLE_DISPATCH_SERVICE
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC._CREATE_INCIDENT_WITH_ENTITIES(System.Int32,System.Single,System.Single,System.Single,System.Int32,System.Single,System.Int32*)">
            <summary>
            dispatchService: see ENABLE_DISPATCH_SERVICE
            
            The entities must be added to itemSet.
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.DELETE_INCIDENT(System.Int32)">
            <summary>
            Delete an incident with a given id.
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC._ADD_POP_MULTIPLIER_VOLUME(System.Int32,System.Single,System.Single,System.Boolean,System.Boolean)">
            <summary>
            Only used in script function PROCESS_ZONE_CREATION
            Returns Pop multiplier volume ID
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC._0x6BCF7B5CD338281A(System.UInt64,System.UInt64,System.UInt64)">
            <summary>
            _SET_DISPATCH_*, unused
            </summary>
```
