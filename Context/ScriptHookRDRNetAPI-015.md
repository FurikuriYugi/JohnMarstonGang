<!-- ScriptHookRDRNetAPI lines 3501-3750 of 30005 -->
```csharp
            	EXP_TAG_GRENADE,
            	EXP_TAG_STICKYBOMB,
            	EXP_TAG_MOLOTOV,
            	EXP_TAG_MOLOTOV_VOLATILE,
            	EXP_TAG_HI_OCTANE,
            	EXP_TAG_CAR,
            	EXP_TAG_PLANE,
            	EXP_TAG_PETROL_PUMP,
            	EXP_TAG_DIR_STEAM,
            	EXP_TAG_DIR_FLAME,
            	EXP_TAG_DIR_WATER_HYDRANT,
            	EXP_TAG_BOAT,
            	EXP_TAG_BULLET,
            	EXP_TAG_SMOKEGRENADE,
            	EXP_TAG_BZGAS,
            	EXP_TAG_GAS_CANISTER,
            	EXP_TAG_EXTINGUISHER,
            	EXP_TAG_TRAIN,
            	EXP_TAG_DIR_FLAME_EXPLODE,
            	EXP_TAG_VEHICLE_BULLET,
            	EXP_TAG_BIRD_CRAP,
            	EXP_TAG_FIREWORK,
            	EXP_TAG_TORPEDO,
            	EXP_TAG_TORPEDO_UNDERWATER,
            	EXP_TAG_LANTERN,
            	EXP_TAG_DYNAMITE,
            	EXP_TAG_DYNAMITESTACK,
            	EXP_TAG_DYNAMITE_VOLATILE,
            	EXP_TAG_RIVER_BLAST,
            	EXP_TAG_PLACED_DYNAMITE,
            	EXP_TAG_FIRE_ARROW,
            	EXP_TAG_DYNAMITE_ARROW,
            	EXP_TAG_PHOSPHOROUS_BULLET,
            	EXP_TAG_LIGHTNING_STRIKE,
            	EXP_TAG_TRACKING_ARROW,
            	EXP_TAG_POISON_BOTTLE
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.FIRE.ADD_OWNED_EXPLOSION(System.Int32,System.Single,System.Single,System.Single,System.Int32,System.Single,System.Boolean,System.Boolean,System.Single)">
            <summary>
            explosionType: see ADD_EXPLOSION
            </summary>
        </member>
        <member name="M:RDR2.Native.FIRE._0xB7DF150605EEDC9B(System.Int32,System.Int32,System.Single,System.Single,System.Single,System.Int32,System.Single,System.Boolean,System.Boolean,System.Single)">
            <summary>
            _ADD_EXPLOSION_*(ON ENTITY?)
            </summary>
        </member>
        <member name="M:RDR2.Native.FIRE.ADD_EXPLOSION_WITH_USER_VFX(System.Single,System.Single,System.Single,System.Int32,System.UInt32,System.Single,System.Boolean,System.Boolean,System.Single)">
            <summary>
            explosionType: see ADD_EXPLOSION
            Change explosionFx (Visual Effect) for specified explosionType
            </summary>
        </member>
        <member name="M:RDR2.Native.FIRE.IS_EXPLOSION_IN_AREA(System.Int32,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single)">
            <summary>
            explosionType: see ADD_EXPLOSION
            </summary>
        </member>
        <member name="M:RDR2.Native.FIRE.IS_EXPLOSION_ACTIVE_IN_AREA(System.Int32,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single)">
            <summary>
            explosionType: see ADD_EXPLOSION
            </summary>
        </member>
        <member name="M:RDR2.Native.FIRE.IS_EXPLOSION_IN_SPHERE(System.Int32,System.Single,System.Single,System.Single,System.Single)">
            <summary>
            explosionType: see ADD_EXPLOSION
            </summary>
        </member>
        <member name="M:RDR2.Native.FIRE.IS_EXPLOSION_IN_ANGLED_AREA(System.Int32,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single)">
            <summary>
            explosionType: see ADD_EXPLOSION
            </summary>
        </member>
        <member name="M:RDR2.Native.FIRE._IS_EXPLOSION_IN_VOLUME(System.Int32,System.Int32)">
            <summary>
            explosionType: see ADD_EXPLOSION
            </summary>
        </member>
        <member name="M:RDR2.Native.FIRE.GET_OWNER_OF_EXPLOSION_IN_ANGLED_AREA(System.Int32,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single)">
            <summary>
            explosionType: see ADD_EXPLOSION
            </summary>
        </member>
        <member name="M:RDR2.Native.FIRE._0x24DB6B9F2B719043(System.Single)">
            <summary>
            Only used in R* SP Related Camp Scripts
            </summary>
        </member>
        <!-- Badly formed XML comment ignored for member "M:RDR2.Native.FIRE._IS_PED_SHOCKING_EVENT_ACTIVE(System.Int32,System.Int32)" -->
        <member name="M:RDR2.Native.FLOCK.GET_ANIMAL_TUNING_FLOAT_PARAM(System.Int32,System.Int32)">
            <summary>
            index: https://github.com/Halen84/RDR3-Native-Flags-And-Enums/tree/main/eAnimalTuningFloats
            https://github.com/femga/rdr3_discoveries/tree/master/AI/ANIMAL_TUNING_FLOAT_PARAMS
            </summary>
        </member>
        <member name="M:RDR2.Native.FLOCK.GET_ANIMAL_TUNING_BOOL_PARAM(System.Int32,System.Int32)">
            <summary>
            index: https://github.com/Halen84/RDR3-Native-Flags-And-Enums/tree/main/eAnimalTuningBools
            https://github.com/femga/rdr3_discoveries/tree/master/AI/ANIMAL_TUNING_BOOL_PARAMS
            </summary>
        </member>
        <member name="M:RDR2.Native.FLOCK._0xF2CCA7B68CFAB2B9(System.UInt32,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single)">
            <summary>
            species: SPECIES_BIRD_CROW
            </summary>
        </member>
        <member name="M:RDR2.Native.FLOCK._GET_ANIMAL_IS_WILD(System.Int32)">
            <summary>
            Ped (horse) will run away from players and mounting will trigger them to buck until disabled.
            Used for: REL_DOMESTICATED_ANIMAL
            </summary>
        </member>
        <member name="M:RDR2.Native.FLOCK._GET_ANIMAL_RARITY(System.Int32)">
            <summary>
            enum eAnimalRarityLevel
            {
            	ARL_COMMON,
            	ARL_RARE,
            	ARL_LEGENDARY,
            	ARL_NUMRARITYLEVELS
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.FLOCK._SET_ANIMAL_RARITY(System.Int32,System.Int32)">
            <summary>
            rarityLevel: see _GET_ANIMAL_RARITY
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS.GET_STATUS_OF_LOAD_MISSION_CREATOR_PHOTO(System.String)">
            <summary>
            contentId: returned by NETWORK::_UGC_QUERY_GET_CREATOR_PHOTO(uVar0, 0, sParam3)
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._0x0D5B19C34068FEE7(System.UInt64)">
            <summary>
            Gets set to 1 when GET_STATUS_OF_TAKE_HIGH_QUALITY_PHOTO = PHOTO_OPERATION_SUCCEEDED
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS.GET_STATUS_OF_SAVE_HIGH_QUALITY_PHOTO">
            <summary>
            0 = succeeded
            1 = getting status
            2 = failed
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS.BEGIN_CREATE_LOW_QUALITY_COPY_OF_PHOTO(System.Int32)">
            <summary>
            Called together with FREE_MEMORY_FOR_LOW_QUALITY_PHOTO
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS.GET_STATUS_OF_CREATE_LOW_QUALITY_COPY_OF_PHOTO(System.UInt64)">
            <summary>
            Old name: _GET_STATUS_OF_DRAW_LOW_QUALITY_PHOTO
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._GET_MAX_NUMBER_OF_LOCAL_PHOTOS">
            <summary>
            Always returns 200.
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS.GET_STATUS_OF_SORTED_LIST_OPERATION">
            <summary>
            0 = succeeded
            1 = getting status
            2 = failed
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._SET_LIGHTS_COLOR_FOR_ENTITY(System.Int32,System.Int32,System.Int32,System.Int32)">
            <summary>
            https://gfycat.com/meagerfaireyra
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._SET_LIGHTS_TYPE_FOR_ENTITY(System.Int32,System.Int32)">
            <summary>
            type must be less than or equal to 20
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._DRAW_MARKER(System.UInt32,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Int32,System.Int32,System.Int32,System.Int32,System.Boolean,System.Boolean,System.Int32,System.Boolean,System.String,System.String,System.Boolean)">
            <summary>
            https://github.com/femga/rdr3_discoveries/blob/master/graphics/markers/marker_types.lua
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS.SET_CHECKPOINT_RGBA(System.Int32,System.Int32,System.Int32,System.Int32,System.Int32)">
            <summary>
            Sets the checkpoint color.
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS.SET_CHECKPOINT_RGBA2(System.Int32,System.Int32,System.Int32,System.Int32,System.Int32)">
            <summary>
            Sets the checkpoint icon color.
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS.DRAW_RECT(System.Single,System.Single,System.Single,System.Single,System.Int32,System.Int32,System.Int32,System.Int32,System.Boolean,System.Boolean)">
            <summary>
            Draws a rectangle on the screen.
            
            -x: The relative X point of the center of the rectangle. (0.0-1.0, 0.0 is the left edge of the screen, 1.0 is the right edge of the screen)
            
            -y: The relative Y point of the center of the rectangle. (0.0-1.0, 0.0 is the top edge of the screen, 1.0 is the bottom edge of the screen)
            
            -width: The relative width of the rectangle. (0.0-1.0, 1.0 means the whole screen width)
            
            -height: The relative height of the rectangle. (0.0-1.0, 1.0 means the whole screen height)
            
            -R: Red part of the color. (0-255)
            
            -G: Green part of the color. (0-255)
            
            -B: Blue part of the color. (0-255)
            
            -A: Alpha part of the color. (0-255, 0 means totally transparent, 255 means totally opaque)
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS.SET_SCRIPT_GFX_DRAW_BEHIND_PAUSEMENU(System.Boolean)">
            <summary>
            Sets a flag defining whether or not script draw commands should continue being drawn behind the pause menu. This is usually used for draw commands that are used with a world render target.
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS.SET_SCRIPT_GFX_DRAW_ORDER(System.Int32)">
            <summary>
            Sets the draw order for script draw commands.
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS.DRAW_SPRITE(System.String,System.String,System.Single,System.Single,System.Single,System.Single,System.Single,System.Int32,System.Int32,System.Int32,System.Int32,System.Boolean)">
            <summary>
            Draws a 2D sprite on the screen.
            
            Parameters:
            textureDict - Name of texture dictionary to load texture from
            
            textureName - Name of texture to load from texture dictionary
            
            screenX/Y - Screen offset (0.5 = center)
            scaleX/Y - Texture scaling. Negative values can be used to flip the texture on that axis. (0.5 = half)
            
            heading - Texture rotation in degrees (default = 0.0) positive is clockwise, measured in degrees
            
            red,green,blue - Sprite color (default = 255/255/255)
            
            alpha - opacity level
            
            https://github.com/femga/rdr3_discoveries/tree/master/useful_info_from_rpfs/textures
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS.SET_TV_AUDIO_FRONTEND(System.Boolean)">
            <summary>
            Probably changes tvs from being a 3d audio to being "global" audio
            </summary>
```
