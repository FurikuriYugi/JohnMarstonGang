<!-- ScriptHookRDRNetAPI lines 2501-2750 of 30005 -->
```csharp
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM._0x4285804FD65D8066(System.String,System.Boolean)">
            <summary>
            script_rel: DRUNK_SHAKE, REINFORCED_LASSO_STRUGGLE_SHAKE, CORRECTOR_SHAKE, MINIGAME_BOUNTY_SHAKE, HAND_SHAKE, MINIGAME_TRAIN_SHAKE
            script_mp_rel: DRUNK_SHAKE, REINFORCED_LASSO_STRUGGLE_SHAKE
            _STOP_GAMEPLAY_CAM* - _STOP_I*
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM.SET_GAMEPLAY_CAM_FOLLOW_PED_THIS_UPDATE(System.Int32)">
            <summary>
            Forces gameplay cam to specified ped as if you were the ped or spectating it
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM.SET_GAMEPLAY_CAM_IGNORE_ENTITY_COLLISION_THIS_UPDATE(System.Int32)">
            <summary>
            Old name: _DISABLE_CAM_COLLISION_FOR_ENTITY
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM._0x0F1FFEF5D54AE832">
            <summary>
            NPLOI_UPDATE__GUN_SPINNING_PREVIEW - Adjusting Camera / Ped Reset Flags This Frame
            _DISABLE_*
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM._SET_GAMEPLAY_CAM_INITIAL_ZOOM(System.Single)">
            <summary>
            Used in Script Function SHOP_CAMERA_SUPPORT_START_NEW_ORBIT
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM.SET_THIRD_PERSON_CAM_RELATIVE_HEADING_LIMITS_THIS_UPDATE(System.Single,System.Single)">
            <summary>
            minimum: Degrees between -180f and 180f.
            maximum: Degrees between -180f and 180f.
            
            Clamps the gameplay camera's current yaw.
            
            Eg. _CLAMP_GAMEPLAY_CAM_YAW(0.0f, 0.0f) will set the horizontal angle directly behind the player.
            
            Old name: _CLAMP_GAMEPLAY_CAM_YAW
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM.SET_THIRD_PERSON_CAM_RELATIVE_PITCH_LIMITS_THIS_UPDATE(System.Single,System.Single)">
            <summary>
            minimum: Degrees between -90f and 90f.
            maximum: Degrees between -90f and 90f.
            
            Clamps the gameplay camera's current pitch.
            
            Eg. _CLAMP_GAMEPLAY_CAM_PITCH(0.0f, 0.0f) will set the vertical angle directly behind the player.
            
            Old name: _CLAMP_GAMEPLAY_CAM_PITCH
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM.SET_THIRD_PERSON_CAM_ORBIT_DISTANCE_LIMITS_THIS_UPDATE(System.Single,System.Single)">
            <summary>
            Old name: _ANIMATE_GAMEPLAY_CAM_ZOOM
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM.SET_IN_VEHICLE_CAM_STATE_THIS_UPDATE(System.Int32,System.Int32)">
            <summary>
            Forces gameplay cam to specified vehicle as if you were in it
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM.DISABLE_ON_FOOT_FIRST_PERSON_VIEW_THIS_UPDATE">
            <summary>
            Old name: _DISABLE_FIRST_PERSON_CAM_THIS_FRAME
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM.DISABLE_FIRST_PERSON_FLASH_EFFECT_THIS_UPDATE">
            <summary>
            nullsub, doesn't do anything
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM._FORCE_THIRD_PERSON_CAM_THIS_FRAME">
            <summary>
            Forces camera position to second furthest 3rd person
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM._FORCE_THIRD_PERSON_CAM_FAR_THIS_FRAME">
            <summary>
            Forces camera position to furthest 3rd person
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM._FORCE_FIRST_PERSON_CAM_THIS_FRAME">
            <summary>
            Returns true if first person camera is active in saloon1.ysc
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM._DISABLE_FIRST_PERSON_CAM_THIS_FRAME_2">
            <summary>
            Does the same as 0x9C473089A934C930
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM.SET_FIRST_PERSON_AIM_CAM_RELATIVE_PITCH_LIMITS_THIS_UPDATE(System.Single,System.Single)">
            <summary>
            Old name: _SET_FIRST_PERSON_CAM_PITCH_RANGE
            </summary>
        </member>
        <!-- Badly formed XML comment ignored for member "M:RDR2.Native.CAM.SET_GAMEPLAY_ENTITY_HINT(System.Int32,System.Single,System.Single,System.Single,System.Boolean,System.Int32,System.Int32,System.Int32,System.UInt64)" -->
        <member name="M:RDR2.Native.CAM._0x88544C0E3291DCAE(System.Boolean)">
            <summary>
            UPDATE_PLAYER_PLAYING_STATE - Releasing Lasso Hint Cam
            Return type char in ida
            _SET_GAMEPLAY_HINT_*
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM.SET_GAMEPLAY_HINT_CAMERA_RELATIVE_SIDE_OFFSET(System.Single)">
            <summary>
            Old name: _SET_GAMEPLAY_HINT_ANIM_OFFSETX
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM.SET_GAMEPLAY_HINT_CAMERA_RELATIVE_VERTICAL_OFFSET(System.Single)">
            <summary>
            Old name: _SET_GAMEPLAY_HINT_ANIM_OFFSETY
            </summary>
        </member>
        <!-- Badly formed XML comment ignored for member "M:RDR2.Native.CAM._START_CAMERA_ORBIT(System.UInt64*)" -->
        <member name="M:RDR2.Native.CAM.DISABLE_CINEMATIC_BONNET_CAMERA_THIS_UPDATE">
            <summary>
            Old name: _DISABLE_VEHICLE_FIRST_PERSON_CAM_THIS_FRAME
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM.INVALIDATE_CINEMATIC_VEHICLE_IDLE_MODE">
            <summary>
            Old name: _INVALIDATE_VEHICLE_IDLE_CAM
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM._0x975F6EBB62632FE3">
            <summary>
            _IS_SCRIPTED_S*
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM._FORCE_CINEMATIC_DEATH_CAM_ON_PED(System.Int32)">
            <summary>
            Used for DUELING_MANAGE_DEATH_CAMERA - Initializing death camera
            Params: targetPed = death cam focuses on it
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM._SET_START_CINEMATIC_DEATH_CAM(System.Boolean)">
            <summary>
            Used for DUELING_MANAGE_DEATH_CAMERA - Initializing death camera
            _SET_P* - _SET_S*
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM._0x1204EB53A5FBC63D">
            <summary>
            Used for DUELING_LOCK_CAMERA_CONTROLS_THIS_FRAME: Disabling look/aim controls
            _IS_SC(REEN_)* (?)
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM._CREATE_KILL_CAM(System.Int32)">
            <summary>
            Creates Kill Cam for specified Ped Handle
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM._0xA8BA2E0204D8486F">
            <summary>
            NPLOI_UPDATE__GUN_SPINNING_PREVIEW - Adjusting Camera / Ped Reset Flags This Frame
            _DISABLE_*
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM._0xC3742F1FDF0A6824">
            <summary>
            Camera will be or is running
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM._0x595550376B7EA230(System.String)">
            <summary>
            Checks data related to Cinematic Cam Locations, if the check fails, the location is being loaded using 0x1B3C2D961F5FC0E1.
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM.CINEMATIC_LOCATION_OVERRIDE_TARGET_ENTITY_THIS_UPDATE(System.String,System.Int32)">
            <summary>
            Only used in R* Script fm_mission_controller
            </summary>
        </member>
        <member name="M:RDR2.Native.CLOCK.SET_CLOCK_TIME(System.Int32,System.Int32,System.Int32)">
            <summary>
            SET_CLOCK_TIME(12, 34, 56);
            </summary>
        </member>
        <member name="M:RDR2.Native.CLOCK.GET_CLOCK_HOURS">
            <summary>
            Gets the current ingame hour, expressed without zeros. (09:34 will be represented as 9)
            </summary>
        </member>
        <member name="M:RDR2.Native.CLOCK.GET_CLOCK_MINUTES">
            <summary>
            Gets the current ingame clock minute.
            </summary>
        </member>
        <member name="M:RDR2.Native.CLOCK.GET_CLOCK_SECONDS">
            <summary>
            Gets the current ingame clock second. Note that ingame clock seconds change really fast since a day in RDR is only 48 minutes in real life.
            </summary>
        </member>
        <member name="M:RDR2.Native.CLOCK._GET_SECONDS_SINCE_BASE_YEAR">
            <summary>
            Base year is 1898.
            </summary>
        </member>
        <member name="M:RDR2.Native.CLOCK.GET_CLOCK_DAY_OF_WEEK">
            <summary>
            Gets the current day of the week.
            
            0: Sunday
            1: Monday
            2: Tuesday
            3: Wednesday
            4: Thursday
            5: Friday
            6: Saturday
            </summary>
        </member>
        <member name="M:RDR2.Native.CLOCK._GET_POSIX_TIME_STRUCT(System.UInt64*)">
            <summary>
            Same as GET_POSIX_TIME except that it takes a single pointer to a struct.
            </summary>
        </member>
        <member name="M:RDR2.Native.COLLECTION._COLLECTABLE_GET_CATEGORY(System.UInt32)">
            <summary>
            Used in Script Function NET_COLLECTABLES_HANDLE_ITEM_ADDED
            Returns collectableCategory Hash
            </summary>
        </member>
        <member name="M:RDR2.Native.COLLECTION._0x755901C7598B97BC(System.UInt32,System.Int32)">
            <summary>
            Returns p2 (index?) for 0xB9020EC89C07DF04
            </summary>
        </member>
        <member name="M:RDR2.Native.COLLECTION._0xB9020EC89C07DF04(System.UInt32,System.Int32,System.Int32)">
            <summary>
            Accepts more hashes than 0xCC644BC1DD655269
            Such as: CAROLINA_PARAKEETS, DINO_BONES, EGRET_FEATHERS, ...
            </summary>
        </member>
        <member name="M:RDR2.Native.COLLECTION._0x6BAB7ACED1017204(System.UInt32,System.Int32)">
            <summary>
            Params: collectableCategory = WEEKLY_COLLECTABLES
            </summary>
        </member>
        <member name="M:RDR2.Native.COLLECTION._0x61BEFBA3CE7A3BC8(System.UInt32,System.Int32)">
            <summary>
            Params: collectableCategory = WEEKLY_COLLECTABLES
            </summary>
        </member>
        <member name="M:RDR2.Native.COLLECTION.COLLECTABLE_GET_CATEGORY_ITEM_SET_BUY_AWARD(System.UInt32,System.UInt32)">
            <summary>
            collectableCategory: ANTIQUE_BOTTLES, BIRD_EGGS, ARROWHEADS, FAMILY_HEIRLOOMS, WILD_FLOWERS, COINS, LOST_JEWELRY_RINGS, LOST_JEWELRY_BRACELETS, LOST_JEWELRY_EARRINGS, LOST_JEWELRY_NECKLACES, TAROT_CARDS_CUPS, TAROT_CARDS_PENTACLES, TAROT_CARDS_SWORDS, TAROT_CARDS_WANDS, FOSSILS_COMMON, FOSSILS_UNCOMMON, FOSSILS_RARE
```
