<!-- ScriptHookRDRNetAPI lines 3751-4000 of 30005 -->
```csharp
        </member>
        <member name="M:RDR2.Native.GRAPHICS.GET_SCREEN_RESOLUTION(System.Int32*,System.Int32*)">
            <summary>
            Hardcoded to always set x to 1280 and y to 720.
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS.SET_ARTIFICIAL_LIGHTS_STATE(System.Boolean)">
            <summary>
            Does not affect weapons, particles, fire/explosions, flashlights or the sun.
            When set to true, all emissive textures (including ped components that have light effects), street lights, building lights, vehicle lights, etc will all be turned off.
            
            state: True turns off all artificial light sources in the map: buildings, street lights, car lights, etc. False turns them back on.
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._0x1A9F09AB458D49C6(System.Boolean)">
            <summary>
            Used in shop scripts for CATALOG_BOOK
            false = Normal -> [CATALOG_BOOK_SHUTDOWN]
            true = Trees flickering? -> [CATALOG_BOOK_OPEN]
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS.CREATE_TRACKED_POINT">
            <summary>
            Creates a tracked point, useful for checking the visibility of a 3D point on screen.
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._0xDFE332A5DA6FE7C9(System.Int32)">
            <summary>
            Returns iNumPixels, iPixelsVisible
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS.SET_GRASS_CULL_SPHERE(System.Single,System.Single,System.Single,System.Single,System.Int32)">
            <summary>
            Returns handle to be used with REMOVE_GRASS_CULL_SPHERE
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._ADD_VEG_MODIFIER_ZONE(System.Int32,System.Int32,System.Int32,System.Int32)">
            <summary>
            Adds Vegetation Blocking Zone, Added Snow Flattening veg mod Zone
            Returns veg modifier handle
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS.ADD_VEG_MODIFIER_SPHERE(System.Single,System.Single,System.Single,System.Single,System.Int32,System.Int32,System.Int32)">
            <summary>
            Returns veg modifier handle
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._GENERATE_SWATCH_TEXTURE_DIRECTLY(System.Int32,System.UInt64)">
            <summary>
            Example: https://pastebin.com/tTgpER9A
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._GENERATE_SWATCH_TEXTURE(System.Int32,System.UInt32,System.Int32,System.Boolean)">
            <summary>
            Example:
            local hash = GetHashKey("CLOTHING_ITEM_M_EYES_001_TINT_001")
            _GENERATE_SWATCH_TEXTURE(0, hash, 0, true)
            metapedType: see 0xEC9A1261BF0CE510
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS.CASCADE_SHADOWS_ENABLE_ENTITY_TRACKER(System.Boolean)">
            <summary>
            When this is set to ON, shadows only draw as you get nearer.
            
            When OFF, they draw from a further distance.
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS.CASCADE_SHADOWS_SET_SHADOW_SAMPLE_TYPE(System.String)">
            <summary>
            Possible values:
            "CSM_ST_POINT"
            "CSM_ST_LINEAR"
            "CSM_ST_BOX3x3"
            "CSM_ST_BOX4x4"
            "CSM_ST_DITHER2_LINEAR"
            "CSM_ST_CUBIC"
            "CSM_ST_POISSON16"
            "CSM_ST_SOFT8"
            "CSM_ST_SOFT16"
            "CSM_ST_SOFT32"
            "CSM_ST_DITHER16_RPDB"
            "CSM_ST_POISSON16_RPDB_GNORM"
            "CSM_ST_HIGHRES_BOX4x4"
            "CSM_ST_ESM"
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS.RESET_ADAPTATION(System.Int32)">
            <summary>
            Sets an unknown value related to timecycles.
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS.SET_HIDOF_OVERRIDE(System.Boolean,System.Boolean,System.Single,System.Single,System.Single,System.Single)">
            <summary>
            Old name: _SET_HIDOF_ENV_BLUR_PARAMS
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._0x21F00E08CBB5F37B(System.String)">
            <summary>
            Params: component - used in odriscolls1 and sean1 R* SP Script: COMPONENT_BINOCULARS_SCOPE01
            Triggers the binocular scaleform
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._0x5AC6E0FA028369DE">
            <summary>
            Closes the the binocular scaleform
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS.START_PARTICLE_FX_NON_LOOPED_AT_COORD(System.String,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Boolean,System.Boolean,System.Boolean)">
            <summary>
            https://github.com/femga/rdr3_discoveries/blob/master/graphics/ptfx/ptfx_assets_non_looped.lua
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS.START_PARTICLE_FX_LOOPED_AT_COORD(System.String,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Boolean,System.Boolean,System.Boolean,System.Boolean)">
            <summary>
            https://github.com/femga/rdr3_discoveries/blob/master/graphics/ptfx/ptfx_assets_looped.lua
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._SET_PARTICLE_FX_LOOPED_UPDATE_DISTANT_SMOKE(System.Int32,System.Single)">
            <summary>
            _SET_PARTICLE_FX_LOOPED_FA* - _SET_PARTICLE_FX_LOOPED_OF*
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS.USE_PARTICLE_FX_ASSET(System.String)">
            <summary>
            fxName: see data_0/data/effects/ptfx/fxlists/
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS.RESET_PARTICLE_FX_OVERRIDE(System.String)">
            <summary>
            Resets the effect of SET_PARTICLE_FX_OVERRIDE
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._0x4FB67D172C4476F3(System.Int32,System.String,System.String,System.Single)">
            <summary>
            p1: AMB_ANN_COAL_CHUTE_DIVE, AMB_ANN_COAL_CHUTE
            p2: EMIT
            p3: either 0.0f or 1.0f
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS.SET_PARTICLE_FX_AMBIENT_COLOUR(System.Int32,System.String,System.Single,System.Single,System.Single)">
            <summary>
            Related to Campfires.
            p1: AMB_BONFIRE_MP, AMB_CAMPFIRE_LRG_MP
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._0xD1472AFF30C103D6(System.Single)">
            <summary>
            Only used in R* Script nb_stalking_hunter
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._0x4046493D2EEACA0E">
            <summary>
            _DISABLE_*
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS.SET_PICKUP_LIGHT(System.Int32,System.Boolean)">
            <summary>
            https://imgur.com/a/I2swSDJ
            
            Old name: _SET_PICKUP_OBJECT_GLOW_ENABLED
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._SET_PEARLESCENT_FX_ENABLED(System.Int32,System.Boolean)">
            <summary>
            Enables/disables a kind of 'shiny' effect on metals.
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS.REMOVE_DECALS_IN_RANGE(System.Single,System.Single,System.Single,System.Single)">
            <summary>
            Removes all decals in range from a position, it includes the bullet holes, blood pools, petrol...
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._ADD_BLOOD_POOL(System.Single,System.Single,System.Single,System.Boolean)">
            <summary>
            https://i.imgur.com/ULQU9US.jpg
            More rounded and small puddle
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._ADD_BLOOD_POOL_2(System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Boolean,System.Single,System.Boolean)">
            <summary>
            https://i.imgur.com/rPITUCV.jpg
            More customizable and more like quadrants
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._ADD_BLOOD_POOLS_FOR_PED(System.Int32)">
            <summary>
            Creates blood pools for the given ped in some interval for a few seconds.
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._0xB032C085D9A03907">
            <summary>
            _SET_D* or _SET_E*
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._0xFB680A9B33D0EDBE(System.Boolean)">
            <summary>
            _DISABLE_*
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._0x41F88A85A579A61D(System.Single)">
            <summary>
            Used in CREATE_BEZIER_BLOOD_TRAIL_OF_TYPE
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._BLOOD_TRAIL_FOR_WAYPOINT(System.String,System.Single)">
            <summary>
            p1: 0.3f in R* Scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._0x812C1563185C6FB2">
            <summary>
            Used in CREATE_BEZIER_BLOOD_TRAIL_OF_TYPE
            _ENABLE_*
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._0x4BD66B4E3427689B(System.String)">
            <summary>
            Used in CREATE_BEZIER_BLOOD_TRAIL_OF_TYPE
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._0x1460B644397453EB">
            <summary>
            _RESET_*
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._DISABLE_FAR_ARTIFICIAL_LIGHTS(System.Boolean)">
            <summary>
            Only used in guama1 R* Script
            Disables lod/distant lights when BOOL is set to true
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._0xC06F2F45A73EABCD(System.Int32)">
            <summary>
            Used in NET_CAMP_SPIRIT_ANIMAL_CLEAR_ANIMAL_VISIBILITY
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS.SET_TIMECYCLE_MODIFIER(System.String)">
            <summary>
            https://github.com/femga/rdr3_discoveries/blob/master/graphics/timecycles
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._GET_MODIFIED_VISIBILITY_DISTANCE">
            <summary>
            _GET_C* - _GET_E*
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS.ENABLE_MOON_CYCLE_OVERRIDE(System.Single)">
            <summary>
            Old name: _ENABLE_EXTRA_TIMECYCLE_MODIFIER_STRENGTH
            </summary>
```
