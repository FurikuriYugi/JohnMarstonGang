<!-- ScriptHookRDRNetAPI lines 5251-5500 of 30005 -->
```csharp
            If modifierHash is 0, ALL modifiers will be removed.
            </summary>
        </member>
        <member name="M:RDR2.Native.MAP._0x01B928CA2E198B01(System.UInt64)">
            <summary>
            _CLEAR*
            </summary>
        </member>
        <member name="M:RDR2.Native.MAP._ABANDON_BLIP(System.Int32)">
            <summary>
            It's unclear what exactly this does, but I assume it marks the blip as "no longer needed"
            </summary>
        </member>
        <member name="M:RDR2.Native.MAP.FORCE_SONAR_BLIPS_THIS_FRAME">
            <summary>
            Doesn't actually return anything.
            </summary>
        </member>
        <member name="M:RDR2.Native.MAP.SET_GPS_FLAGS(System.Int32,System.Single)">
            <summary>
            https://alloc8or.re/rdr3/doc/enums/rage__eGpsFlags.txt
            </summary>
        </member>
        <member name="M:RDR2.Native.MAP.CLEAR_GPS_FLAGS">
            <summary>
            Clears the GPS flags.
            </summary>
        </member>
        <member name="M:RDR2.Native.MAP.CLEAR_GPS_MULTI_ROUTE">
            <summary>
            Does the same as SET_GPS_MULTI_ROUTE_RENDER(false);
            </summary>
        </member>
        <member name="M:RDR2.Native.MAP.DISPLAY_RADAR(System.Boolean)">
            <summary>
            If Minimap / Radar should be displayed.
            </summary>
        </member>
        <member name="M:RDR2.Native.MAP._SET_RADAR_CONFIG_TYPE(System.UInt32,System.UInt32)">
            <summary>
            https://github.com/femga/rdr3_discoveries/blob/master/graphics/minimap/radar/radar_configs.lua
            configHash: -1943724816, 347777538, -117986897, -789269373, -547506804, -1986542417, 2080113112
            p1: usually 898171178 or 0 in R* scripts (doesn't seems to have any effect)
            </summary>
        </member>
        <member name="M:RDR2.Native.MAP._ADD_PROP_TO_MINIMAP(System.UInt32,System.Single,System.Single,System.Single,System.Int32)">
            <summary>
            list of minimap props: https://github.com/femga/rdr3_discoveries/tree/master/graphics/minimap/minimapObjects
            </summary>
        </member>
        <member name="M:RDR2.Native.MAP.SET_MINIMAP_HIDE_FOW(System.Boolean)">
            <summary>
            Reveals the entire minimap (FOW = Fog of War)
            </summary>
        </member>
        <member name="M:RDR2.Native.MAP._SET_FOW_UPDATE_PLAYER_OVERRIDE(System.Boolean,System.UInt32)">
            <summary>
            Used for GUARMA MODE; Enabled: toggle = false, 0; Disabled: toggle = true, 0
            Hash p1 seems to be unused, always 0
            </summary>
        </member>
        <member name="M:RDR2.Native.MAP.SET_MINIMAP_FOW_REVEAL_COORDINATE(System.Single,System.Single,System.Single,System.UInt32)">
            <summary>
            Up to eight coordinates may be revealed per frame
            </summary>
        </member>
        <member name="M:RDR2.Native.MAP._SET_MINIMAP_ZONE(System.UInt32)">
            <summary>
            hash can be the hash of "guarma" or "world".
            </summary>
        </member>
        <member name="M:RDR2.Native.MAP.LOCK_MINIMAP_ANGLE(System.Int32)">
            <summary>
            Locks the minimap to the specified angle in integer degrees.
            
            angle: The angle in whole degrees. If less than 0 or greater than 360, unlocks the angle.
            </summary>
        </member>
        <member name="M:RDR2.Native.MAP._MAP_ENABLE_REGION_BLIP(System.UInt32,System.UInt32)">
            <summary>
            regionHash: https://github.com/femga/rdr3_discoveries/tree/master/graphics/minimap/wanted_regions
            </summary>
        </member>
        <member name="M:RDR2.Native.MAP._0x44813684F72B563C(System.Int32,System.UInt64)">
            <summary>
            _CLEAR*
            </summary>
        </member>
        <member name="M:RDR2.Native.MAP._0x7563CBCA99253D1A(System.Int32,System.UInt32)">
            <summary>
            FM_CLIENT_SETUP_EAGLE_EYE - setting up eagle eye for entity
            </summary>
        </member>
        <member name="M:RDR2.Native.MINIGAME._DOMINOES_BUY_IN(System.UInt64)">
            <summary>
            Hardcoded to return zero/false.
            </summary>
        </member>
        <member name="M:RDR2.Native.MINIGAME._DOMINOES_PLACE_DOMINO(System.UInt64,System.UInt64)">
            <summary>
            Hardcoded to return zero/false.
            </summary>
        </member>
        <member name="M:RDR2.Native.MINIGAME._DOMINOES_REQUEST_VALID_PLACEMENTS(System.UInt64)">
            <summary>
            Hardcoded to return zero/false.
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.IS_MAG_DEMO_1_ACTIVE">
            <summary>
            magdemo = magazine demo, i. e. for magazines such as IGN, pre play phases to prepare articles etc. - example 2012 builds for V
            Hardcoded to return false.
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.SET_TIME_SCALE(System.Single)">
            <summary>
            Maximum value is 1.0f
            At a value of 0.0f the game will still run at a minimum time scale.
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.SET_MISSION_FLAG(System.Boolean)">
            <summary>
            If true, the player can't save the game.
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.SET_RANDOM_EVENT_FLAG(System.Boolean)">
            <summary>
            If the parameter is true, sets the random event flag to true, if the parameter is false, the function does nothing at all.
            Does nothing if the mission flag is set.
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.ACTIVITY_FEED_CREATE(System.String,System.String)">
            <summary>
            nullsub, doesn't do anything
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.ACTIVITY_FEED_ADD_SUBSTRING_TO_CAPTION(System.String)">
            <summary>
            nullsub, doesn't do anything
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC._0xFF252E2BAFB7330F(System.UInt64)">
            <summary>
            nullsub, doesn't do anything
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.ACTIVITY_FEED_ACTION_START_WITH_COMMAND_LINE(System.String,System.String)">
            <summary>
            nullsub, doesn't do anything
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.ACTIVITY_FEED_ACTION_START_WITH_COMMAND_LINE_ADD(System.String)">
            <summary>
            nullsub, doesn't do anything
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC._0xAF530E56505D1BD6(System.UInt64)">
            <summary>
            Hardcoded to return one/true.
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.ACTIVITY_FEED_POST">
            <summary>
            nullsub, doesn't do anything
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC._0x1096603B519C905F(System.String)">
            <summary>
            _SET_MISSION_NAME_*(FOR_ACTIVITY?/MINIGAME?)
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.SET_WEATHER_TYPE(System.UInt32,System.Boolean,System.Boolean,System.Boolean,System.Single,System.Boolean)">
            <summary>
            https://github.com/femga/rdr3_discoveries/blob/master/weather/weather_types.lua
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.GET_CURR_WEATHER_STATE(System.UInt32*,System.UInt32*,System.Single*)">
            <summary>
            Params: percentWeather2: 0f - 0.75f in R* Scripts
            
            Old name: _GET_WEATHER_TYPE_TRANSITION
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.SET_CURR_WEATHER_STATE(System.UInt32,System.UInt32,System.Single,System.Boolean)">
            <summary>
            Params: BOOL p3 is always true
            
            Old name: _SET_WEATHER_TYPE_TRANSITION
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC._GET_FORCED_WEATHER(System.UInt32*,System.UInt32*)">
            <summary>
            Returns the weather type that has been set by a script
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC._SET_WEATHER_VARIATION(System.String,System.String)">
            <summary>
            https://github.com/femga/rdr3_discoveries/blob/master/weather/weather_variations.lua
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.WATER_OVERRIDE_SET_SHOREWAVEAMPLITUDE(System.Single)">
            <summary>
            Only used in smuggler2 script
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.WATER_OVERRIDE_SET_OCEANWAVEMAXAMPLITUDE(System.Single)">
            <summary>
            Only used in smuggler2 script
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.SET_RAIN(System.Single)">
            <summary>
            Old name: _SET_RAIN_LEVEL
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.FORCE_LIGHTNING_FLASH">
            <summary>
            creates single lightning+thunder at random position
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC._FORCE_LIGHTNING_FLASH_AT_COORDS(System.Single,System.Single,System.Single,System.Single)">
            <summary>
            p3 is always -1.0f in the scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC._0xA9342743B634A462(System.UInt64)">
            <summary>
            nullsub, doesn't do anything
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC._GET_GAME_TIMER_NON_SCALED_CLIPPED">
            <summary>
            Returns rage::fwTimer::sm_nonScaledClippedTime
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.GET_SYSTEM_TIME_STEP">
            <summary>
            Old name: _GET_BENCHMARK_TIME
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC._READ_INT_AS_FLOAT(System.Int32)">
            <summary>
            Reads the passed value as floating point value and returns it.
            Example: _READ_INT_AS_FLOAT(0x3F800000) returns 1.0f because 0x3F800000 is the hexadecimal representation of 1.0f.
            </summary>
        </member>
        <member name="M:RDR2.Native.MISC.GET_DISTANCE_BETWEEN_COORDS(System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Boolean)">
            <summary>
            If useZ is false, only the 2D plane (X-Y) will be considered for calculating the distance.
            
```
