<!-- ScriptHookRDRNetAPI-Inventory chunk 1 of 26; methods=51; sha256=bfb7607d94b2de7f8530289377f163caad81098afc1fb7ee86191810a6cc6bba -->
# ScriptHookRDRNetAPI public API inventory

- Assembly: `ScriptHookRDRNetAPI`
- Assembly version: `2.2.0.0`
- File version: `2.2.0`
- Target framework: `.NETFramework,Version=v4.8`
- SHA-256: `bfb7607d94b2de7f8530289377f163caad81098afc1fb7ee86191810a6cc6bba`
- Public types: 319
- Public methods: 8140

Methods are listed under the type that declares them.
This inventory records signatures, not runtime behavior.

## `RDR2.AccelerationAttribute`

- Kind: class
- Base type: `RDR2.PedAttribute`

*No public methods declared.*

## `RDR2.AgilityAttribute`

- Kind: class
- Base type: `RDR2.PedAttribute`

*No public methods declared.*

## `RDR2.AnimScene`

- Kind: class
- Base type: `System.Object`

- `public System.Void RDR2.AnimScene.Abort()`
- `public System.Void RDR2.AnimScene.AddEntity(System.String entityId, RDR2.Entity entity)`
- `public System.Void RDR2.AnimScene.Delete()`
- `public System.Boolean RDR2.AnimScene.DoesEntityIDExist(System.String entityId)`
- `public System.Boolean RDR2.AnimScene.Exists()`
- `public System.Void RDR2.AnimScene.GetOrigin(RDR2.Math.Vector3* outPosition, RDR2.Math.Vector3* outRotation)`
- `public System.Boolean RDR2.AnimScene.IsEntityPlayingAnimScene(RDR2.Entity entity)`
- `public System.Boolean RDR2.AnimScene.IsInSection(System.String sectionName)`
- `public System.Void RDR2.AnimScene.Load()`
- `public System.Void RDR2.AnimScene.RemoveEntity(System.String entityId, RDR2.Entity entity)`
- `public System.Void RDR2.AnimScene.SetOrigin(RDR2.Math.Vector3 position, RDR2.Math.Vector3 rotation)`
- `public System.Void RDR2.AnimScene.Skip()`
- `public System.Void RDR2.AnimScene.Start()`
- `public static System.Int32 RDR2.AnimScene.op_Implicit(RDR2.AnimScene e)`

## `RDR2.Audio`

- Kind: class
- Base type: `System.Object`

- `public static System.Int32 RDR2.Audio.PlaySound(System.String soundName, System.String soundset)`
- `public static System.Int32 RDR2.Audio.PlaySoundFrontend(System.String soundName, System.String soundset)`
- `public static System.Boolean RDR2.Audio.PrepareSound(System.String soundName, System.String soundset)`
- `public static System.Boolean RDR2.Audio.PrepareSoundset(System.String soundset)`
- `public static System.Void RDR2.Audio.ReleaseSound(System.Int32 id)`
- `public static System.Void RDR2.Audio.SetAudioFlag(System.String flagName, System.Boolean toggle)`
- `public static System.Void RDR2.Audio.SetAudioVariable(System.Int32 id, System.String variableName, System.Single variableValue)`
- `public static System.Void RDR2.Audio.SetAudioVariable(System.String soundName, System.String soundSet, System.String variableName, System.Single variableValue)`
- `public static System.Void RDR2.Audio.StopSound(System.Int32 id)`
- `public static System.Void RDR2.Audio.StopSound(System.String soundName, System.String soundset)`

## `RDR2.Blip`

- Kind: class
- Base type: `RDR2.PoolObject`

- `public System.Void RDR2.Blip.AddModifier(RDR2.eBlipModifier modifier)`
- `public System.Void RDR2.Blip.Delete()`
- `public System.Boolean RDR2.Blip.Equals(RDR2.Blip other)`
- `public System.Boolean RDR2.Blip.Equals(System.Object obj)`
- `public System.Boolean RDR2.Blip.Exists()`
- `public System.Int32 RDR2.Blip.GetHashCode()`
- `public System.Void RDR2.Blip.RemoveModifier(RDR2.eBlipModifier modifier)`

## `RDR2.BlipSprite`

- Kind: enum
- Base type: `System.Enum`

*No public methods declared.*

## `RDR2.BlipType`

- Kind: enum
- Base type: `System.Enum`

*No public methods declared.*

Enum values:
- `BLIP_STYLE_ACCURATE_AREA_BOUNDS_OVERLAY` = `4106959529`
- `BLIP_STYLE_ACCURATE_AREA_BOUNDS_OVERLAY_WARNING` = `3882023977`
- `BLIP_STYLE_ADVERSARY` = `2208833468`
- `BLIP_STYLE_AMBIENT_COACH` = `2891075329`
- `BLIP_STYLE_AREA` = `1247852480`
- `BLIP_STYLE_AREA_BOUNDS` = `4008921239`
- `BLIP_STYLE_AREA_BOUNDS_OVERLAY` = `3725492857`
- `BLIP_STYLE_AREA_BOUNDS_OVERLAY_BR_GRID` = `3923922796`
- `BLIP_STYLE_AREA_BOUNDS_OVERLAY_BR_GRID_WARNING` = `1814509446`
- `BLIP_STYLE_AREA_BOUNDS_OVERLAY_WARNING` = `125034305`
- `BLIP_STYLE_AREA_BURN` = `719976738`
- `BLIP_STYLE_AREA_DIRECTIONAL` = `2705769321`
- `BLIP_STYLE_AREA_DIRECTIONAL_HEIGHT` = `497342920`
- `BLIP_STYLE_AREA_FOW` = `2033377404`
- `BLIP_STYLE_AREA_NO_DIM` = `620596860`
- `BLIP_STYLE_AREA_TERRITORY_TRAIN` = `1743498875`
- `BLIP_STYLE_AVOID_AREA` = `1091921700`
- `BLIP_STYLE_AVOID_RADIUS` = `3628545275`
- `BLIP_STYLE_BOUNTY_HUNTER` = `522464759`
- `BLIP_STYLE_BOUNTY_HUNTER_BASE` = `119074000`
- `BLIP_STYLE_BOUNTY_HUNTER_RADIUS` = `3506444034`
- `BLIP_STYLE_BOUNTY_TARGET` = `953018525`
- `BLIP_STYLE_BOUNTY_TARGET_INCAPACITATED` = `455691738`
- `BLIP_STYLE_CAMP` = `1560611276`
- `BLIP_STYLE_CAMPFIRE` = `3958904061`
- `BLIP_STYLE_CAMP_CHORE` = `3620083493`
- `BLIP_STYLE_CAMP_ENEMY` = `2339406903`
- `BLIP_STYLE_CAMP_HIDDEN` = `1720107185`
- `BLIP_STYLE_CAMP_REQUEST` = `2350212892`
- `BLIP_STYLE_CAR` = `2892655483`
- `BLIP_STYLE_CENTER` = `1050715200`
- `BLIP_STYLE_CHALLENGE_OBJECTIVE` = `3212601184`
- `BLIP_STYLE_CHECKPOINT_CHALLENGE` = `2014011982`
- `BLIP_STYLE_COP` = `2699917098`
- `BLIP_STYLE_COP_BASE` = `662885764`
- `BLIP_STYLE_COP_DOG` = `4273604703`
- `BLIP_STYLE_COP_DOG_BASE` = `674366726`
- `BLIP_STYLE_COP_PERSISTENT` = `3885213530`
- `BLIP_STYLE_COP_SCRIPT` = `4176956878`
- `BLIP_STYLE_CREATOR_CURSOR` = `1142472593`
- `BLIP_STYLE_CREATOR_DEFAULT` = `3101023822`
- `BLIP_STYLE_CREATOR_PROP` = `1144721026`
- `BLIP_STYLE_DEBUG_BLUE` = `700277485`
- `BLIP_STYLE_DEBUG_GREEN` = `2794217282`
- `BLIP_STYLE_DEBUG_PINKK` = `2849522031`
- `BLIP_STYLE_DEBUG_RED` = `1724328120`
- `BLIP_STYLE_DEBUG_YELLOW` = `2700331448`
- `BLIP_STYLE_DEPUTY_RESIDENT` = `408731731`
- `BLIP_STYLE_DEPUTY_RESIDENT_BASE` = `1055455511`
- `BLIP_STYLE_DISTRICT_HEARTLANDS` = `2152624693`
- `BLIP_STYLE_ENEMY` = `831283580`
- `BLIP_STYLE_ENEMY_NO_THREAT` = `3455597687`
- `BLIP_STYLE_ENEMY_PASSIVE_THREAT` = `4108623209`
- `BLIP_STYLE_ENEMY_SEVERE` = `314400844`
- `BLIP_STYLE_EVADE_AREA` = `1774867085`
- `BLIP_STYLE_EVENT_AREA` = `3799988940`
- `BLIP_STYLE_EYEWITNESS` = `3245577145`
- `BLIP_STYLE_EYEWITNESS_NO_CONDITION` = `3285036404`
- `BLIP_STYLE_FAKE_WANTED_RADIUS` = `693035517`
- `BLIP_STYLE_FETCH_TRAIN` = `4214170130`
- `BLIP_STYLE_FIREWOOD_BLIP` = `2674412910`
- `BLIP_STYLE_FISHING_SPOT` = `2846069362`
- `BLIP_STYLE_FM_EVENT` = `618936128`
- `BLIP_STYLE_FM_EVENT_AREA` = `2863453024`
- `BLIP_STYLE_FM_EVENT_COACH` = `2064908458`
- `BLIP_STYLE_FM_EVENT_RADIUS` = `4247468123`
- `BLIP_STYLE_FREE_CAMERA` = `4121956820`
- `BLIP_STYLE_FRIENDLY` = `4205537449`
- `BLIP_STYLE_FRIENDLY_ON_RADAR` = `907426021`
- `BLIP_STYLE_GANG_BOUNTY_RADIUS` = `3904258639`
- `BLIP_STYLE_GANG_WAYPOINT` = `3225762461`
- `BLIP_STYLE_GOLDEN_HAT` = `1416689804`
- `BLIP_STYLE_HERDING_ANIMAL` = `3594038332`
- `BLIP_STYLE_HERDING_DOG` = `1572362730`
- `BLIP_STYLE_HERDING_MAIN` = `2511574845`
- `BLIP_STYLE_HERDING_STRAGGLER` = `4080805145`
- `BLIP_STYLE_HITCHING_POST` = `1687768444`
- `BLIP_STYLE_LEGENDARY_ANIMAL_AREA` = `4186308536`
- `BLIP_STYLE_LEGENDARY_ANIMAL_CLUE` = `1538154804`
- `BLIP_STYLE_LOOT_OBJECTIVE` = `1055493006`
- `BLIP_STYLE_MINIGAME` = `3616722801`
- `BLIP_STYLE_MISSION` = `1673015813`
- `BLIP_STYLE_MISSION_HIDDEN` = `3702259048`
- `BLIP_STYLE_MISSION_RADAR_ONLY` = `164094739`
- `BLIP_STYLE_MISSION_RADIUS` = `2735059990`
- `BLIP_STYLE_MISSION_RADIUS_HIDDEN` = `1986697381`
- `BLIP_STYLE_MP_CAMP_FOLLOWER` = `849732319`
- `BLIP_STYLE_MP_CORPSE_FADE` = `1877557666`
- `BLIP_STYLE_MP_CORPSE_FADE_LOCAL` = `99680225`
- `BLIP_STYLE_MP_DOWNED` = `2052944744`
- `BLIP_STYLE_MP_HIDEOUT` = `3787785074`
- `BLIP_STYLE_MP_JOB_PLAYER_ALLY` = `4019007654`
- `BLIP_STYLE_MP_JOB_PLAYER_ENEMY` = `855494199`
- `BLIP_STYLE_MP_JOB_PLAYER_ENEMY_ANIMATE_ON_ATTACK` = `2400917684`
- `BLIP_STYLE_MP_JOB_PLAYER_ENEMY_ANIMATE_ON_ATTACK_FTB` = `3937356524`
- `BLIP_STYLE_MP_JOB_PLAYER_ENEMY_NO_THREAT` = `3560175595`
- `BLIP_STYLE_MP_MISSION_GIVER` = `1699087565`
- `BLIP_STYLE_MP_PLAYER` = `1775959122`
- `BLIP_STYLE_MP_PLAYLIST_CORONA` = `2737154776`
- `BLIP_STYLE_MP_SCHEDULE_SERIES` = `1476777140`
- `BLIP_STYLE_MP_SERIES` = `3948026683`
- `BLIP_STYLE_NEUTRAL` = `2545348716`
- `BLIP_STYLE_NEUTRAL_OBJECTIVE` = `1631143573`
- `BLIP_STYLE_OBJECTIVE_MINOR` = `203020899`
- `BLIP_STYLE_OBJECTIVE_VOL_BOX` = `1259054292`
- `BLIP_STYLE_OBJECTIVE_VOL_ROUND` = `3986381328`
- `BLIP_STYLE_OTHER_SESSION_PLAYER` = `1325312906`
- `BLIP_STYLE_PICKUP` = `3969327396`
- `BLIP_STYLE_PICKUP_AMMO` = `1937361305`
- `BLIP_STYLE_PICKUP_ANIMAL` = `2700663341`
- `BLIP_STYLE_PICKUP_ANIMAL_DOWNED` = `3918829332`
- `BLIP_STYLE_PICKUP_ANIMAL_DOWNED_ENEMY` = `3478740762`
- `BLIP_STYLE_PICKUP_ANIMAL_SKIN` = `3053226306`
- `BLIP_STYLE_PICKUP_COLLECTABLE` = `123293891`
- `BLIP_STYLE_PICKUP_HERB` = `1425226969`
- `BLIP_STYLE_PICKUP_HERB_NEW` = `1002193118`
- `BLIP_STYLE_PICKUP_HORSE` = `2744695433`
- `BLIP_STYLE_PICKUP_OBJECT` = `2098831270`
- `BLIP_STYLE_PICKUP_VEHICLE` = `2312377750`
- `BLIP_STYLE_PICKUP_WEAPON` = `1664425300`
- `BLIP_STYLE_PICKUP_WEAPON_DIRECTIONAL` = `1154431407`
- `BLIP_STYLE_PICKUP_WILDERNESS_CHEST` = `1585736816`
- `BLIP_STYLE_PING_AUTO` = `2985729140`
- `BLIP_STYLE_PING_BEAT_EXPLOSION` = `3025592296`
- `BLIP_STYLE_PING_BELL_RING` = `1538479870`
- `BLIP_STYLE_PING_FAST` = `720995816`
- `BLIP_STYLE_PING_FAST_LARGE` = `1614652896`
- `BLIP_STYLE_PING_LEGENDARY_ANIMAL_CLUE` = `4081083337`
- `BLIP_STYLE_PING_MEDIUM` = `1762854635`
- `BLIP_STYLE_PING_MEDIUM_BEAT` = `3517348944`
- `BLIP_STYLE_PING_MEDIUM_LONG` = `275273191`
- `BLIP_STYLE_PING_MEDIUM_NO_BLIP` = `2945941961`
- `BLIP_STYLE_PING_MEDIUM_SLOW` = `1583747453`
- `BLIP_STYLE_PING_MEDIUM_SMALL` = `1279034750`
- `BLIP_STYLE_PING_MEDIUM_THREAT` = `633753327`
- `BLIP_STYLE_PING_SLOW` = `840083300`
- `BLIP_STYLE_PING_TREE_FALL` = `1885824029`
- `BLIP_STYLE_PLANT_DYNAMITE` = `3462179504`
- `BLIP_STYLE_PLAYER_COACH` = `631964804`
- `BLIP_STYLE_PLAYER_HAT_BLIP` = `486881925`
- `BLIP_STYLE_PLAYER_HORSE` = `2369362204`
- `BLIP_STYLE_PLAYER_HORSE_SADDLE` = `3779449111`
- `BLIP_STYLE_POI` = `2641016523`
- `BLIP_STYLE_POSSE_FORMATION_AREA` = `2488998439`
- `BLIP_STYLE_PROC_HOME_ROBBERY` = `3660005328`
- `BLIP_STYLE_PROC_HOME_ROBBERY_ENEMY` = `2572815164`
- `BLIP_STYLE_PROC_MISSION` = `3358750662`
- `BLIP_STYLE_PROC_MISSION_INSTANCED` = `607462501`
- `BLIP_STYLE_PROC_MISSION_RADIUS` = `2293350859`
- `BLIP_STYLE_PROPERTY` = `3322000913`
- `BLIP_STYLE_PROPERTY_OWNER` = `3694370989`
- `BLIP_STYLE_RADIUS` = `3012174784`
- `BLIP_STYLE_RANDOM_EVENT_PULSE` = `1291203949`
- `BLIP_STYLE_RCM` = `2957021944`
- `BLIP_STYLE_RCM_AREA` = `227551412`
- `BLIP_STYLE_RCM_BOUNTY` = `2809744749`
- `BLIP_STYLE_RCM_BOUNTY_HIDDEN` = `13700166`
- `BLIP_STYLE_RCM_HIDDEN` = `588610391`
- `BLIP_STYLE_RCM_TRACKED` = `2103462781`
- `BLIP_STYLE_RCM_TRACKED_HIDDEN` = `3262036492`
- `BLIP_STYLE_REGION_LOCKDOWN` = `2989626703`
- `BLIP_STYLE_SELECTED_REGION` = `3780837220`
- `BLIP_STYLE_SHOP` = `168093330`
- `BLIP_STYLE_SP_CORPSE` = `61565164`
- `BLIP_STYLE_SP_CORPSE_FADE` = `643138661`
- `BLIP_STYLE_TEMPORARY_HORSE` = `3063973875`
- `BLIP_STYLE_TOWN` = `3911271093`
- `BLIP_STYLE_TRACKING` = `3121518984`
- `BLIP_STYLE_TRACKING_DOT` = `2245554123`
- `BLIP_STYLE_TRAIN` = `3895470911`
- `BLIP_STYLE_TRAIN_BOOKMARK` = `1446775399`
- `BLIP_STYLE_TURRET_WEAPON` = `1276047687`
- `BLIP_STYLE_UNDISCOVERED` = `3537309847`
- `BLIP_STYLE_UPCOMING_OBJECTIVE` = `681199565`
- `BLIP_STYLE_WANTED_RADIUS` = `1165324392`
- `BLIP_STYLE_WANTED_STATE` = `2092520574`
- `BLIP_STYLE_WAYPOINT` = `2244253819`
- `CompanionGray` = `422991367`
- `DestinationDark` = `1687768444`
- `DestinationGray` = `3616722801`
- `DestinationLarge` = `408396114`
- `DestinationSmall` = `203020899`
- `EnemyPink` = `2592059583`
- `Flashing` = `1260140857`
- `PickupWhite` = `3969327396`
- `PosseCamp` = `1560611276`
- `WeaponWhite` = `1664425300`
- `WhiteDot` = `2957021944`

## `RDR2.BodyHeatAttribute`

- Kind: class
- Base type: `RDR2.PedAttribute`

*No public methods declared.*

## `RDR2.BodyWeightAttribute`

- Kind: class
- Base type: `RDR2.PedAttribute`

*No public methods declared.*

## `RDR2.BondingAttribute`

- Kind: class
- Base type: `RDR2.PedAttribute`

*No public methods declared.*

## `RDR2.BoneID`

- Kind: enum
- Base type: `System.Enum`

*No public methods declared.*

Enum values:
- `BONETAG_HEAD` = `31086`
- `BONETAG_ROOT` = `0`
- `BONETAG_SPINE3` = `24818`
- `CHASSIS_GUN` = `8586`
- `FACIAL_R_LipCorner` = `10509`
- `FISHBAG_BOTTOM` = `3897`
- `ID_BOGIE_F` = `22732`
- `ID_BOGIE_R` = `22616`
- `ID_BRAKE_F` = `46422`
- `ID_BRAKE_R` = `46306`
- `ID_CHASSIS` = `52564`
- `ID_CHASSIS_DUMMY` = `39433`
- `ID_CP_BACK` = `36522`
- `ID_CP_B_NECKLASSO` = `56089`
- `ID_CP_CHEST` = `53684`
- `ID_CP_C_NeckAlignment` = `52697`
- `ID_CP_F_NECKLASSO` = `57565`
- `ID_CP_L_ManeGrip` = `40802`
- `ID_CP_L_NECKLASSO` = `47971`
- `ID_CP_L_UPPERARM` = `24238`
- `ID_CP_R_ManeGrip` = `65009`
- `ID_CP_R_NECKLASSO` = `50185`
- `ID_CP_R_UPPERARM` = `65198`
- `ID_ES_SATCHELROOT` = `44562`
- `ID_FACIAL_C_SnoutUpper` = `60304`
- `ID_FB_JAW` = `16351`
- `ID_FB_L_EYE` = `5956`
- `ID_FB_L_EYE_AMB` = `25260`
- `ID_FB_R_EYE` = `6468`
- `ID_FB_R_EYE_AMB` = `27474`
- `ID_FOOT_BRAKE` = `40007`
- `ID_GC_BREATH_LEFT` = `44415`
- `ID_GC_BREATH_RIGHT` = `45951`
- `ID_GC_CROUCHING` = `15238`
- `ID_GC_HAIR_SCALE` = `13201`
- `ID_GC_HEAD_X` = `19336`
- `ID_GC_HIGH_HEELS` = `15570`
- `ID_GC_HIGH_HEELS_META` = `28462`
- `ID_GC_NECK_X` = `64744`
- `ID_GC_NECK_Y` = `64745`
- `ID_GC_NECK_Z` = `64746`
- `ID_GC_PRONE` = `59703`
- `ID_GC_SITTING` = `45903`
- `ID_GEAR_BIG` = `54652`
- `ID_GEAR_SMALL` = `43564`
- `ID_GUN_GRIPR` = `18308`
- `ID_HANDLE_LF` = `47402`
- `ID_HANDLE_LR` = `47414`
- `ID_HANDLE_RF` = `45258`
- `ID_HANDLE_RR` = `45270`
- `ID_IK_L_FOOT` = `65245`
- `ID_IK_L_HAND` = `36029`
- `ID_IK_R_FOOT` = `35502`
- `ID_IK_R_HAND` = `6286`
- `ID_INVALID` = `-1`
- `ID_MAGAZINE_CLIP` = `2062`
- `ID_MH_RIBCAGE0` = `59273`
- `ID_MH_RIBCAGE1` = `59274`
- `ID_OH_FACINGDIR` = `55074`
- `ID_OH_HEAD` = `27981`
- `ID_OH_INDEPENDENTMOVER` = `22284`
- `ID_OH_LOOKDIR` = `41178`
- `ID_OH_L_ARM_TWIST` = `30610`
- `ID_OH_L_CLAVICLE` = `7929`
- `ID_OH_L_FOOT` = `926`
- `ID_OH_L_HAND` = `38989`
- `ID_OH_MOVER` = `64248`
- `ID_OH_NECK` = `52596`
- `ID_OH_PELVIS` = `33219`
- `ID_OH_R_ARM_TWIST` = `26920`
- `ID_OH_R_CLAVICLE` = `32136`
- `ID_OH_R_FOOT` = `36350`
- `ID_OH_R_HAND` = `9246`
- `ID_OH_UPPERFIXUP` = `9212`
- `ID_PH_BELT_FRONT` = `40833`
- `ID_PH_BELT_MELEE` = `40893`
- `ID_PH_BELT_REAR` = `32915`
- `ID_PH_BELT_THROWER` = `2656`
- `ID_PH_BOW` = `59452`
- `ID_PH_CARRIABLE` = `31899`
- `ID_PH_CIGARETTE` = `9650`
- `ID_PH_DETONATORPROP` = `41388`
- `ID_PH_HEART` = `8906`
- `ID_PH_JAW` = `61276`
- `ID_PH_L_BREAST` = `64986`
- `ID_PH_L_CARRIABLE_SHOULDER` = `15077`
- `ID_PH_L_FOOT` = `2718`
- `ID_PH_L_HAND` = `37709`
- `ID_PH_L_HEEL` = `40453`
- `ID_PH_L_HIP` = `58630`
- `ID_PH_RIFLE` = `37600`
- `ID_PH_RIFLE_INVERTED` = `60005`
- `ID_PH_R_BOOT` = `54157`
- `ID_PH_R_BREAST` = `64826`
- `ID_PH_R_CARRIABLE_SHOULDER` = `60525`
- `ID_PH_R_FOOT` = `38142`
- `ID_PH_R_HAND` = `7966`
- `ID_PH_R_HEEL` = `10710`
- `ID_PH_R_HIP` = `60844`
- `ID_PH_R_SHOULDER` = `38382`
- `ID_PH_SADDLE_ATTACH_L` = `56141`
- `ID_PH_SADDLE_ATTACH_LB` = `60530`
- `ID_PH_SADDLE_ATTACH_R` = `56243`
- `ID_PH_SADDLE_ATTACH_RB` = `62034`
- `ID_PH_SaddleHorn` = `62111`
- `ID_PH_WEAPON` = `3086`
- `ID_PH_WEAPON_BOW` = `21922`
- `ID_PUMP_LEVER` = `2513`
- `ID_RB_L_ARMROLL` = `45546`
- `ID_RB_L_FOREARMROLL` = `29956`
- `ID_RB_L_THIGHROLL` = `61747`
- `ID_RB_R_ARMROLL` = `48106`
- `ID_RB_R_FOREARMROLL` = `62258`
- `ID_RB_R_THIGHROLL` = `58057`
- `ID_SADDLE_SEAT_L` = `11885`
- `ID_SH_BODY_STRAP_00` = `19108`
- `ID_SH_BODY_STRAP_01` = `19109`
- `ID_SH_BODY_STRAP_02` = `19110`
- `ID_SH_BODY_STRAP_03` = `19111`
- `ID_SH_BODY_STRAP_04` = `19112`
- `ID_SH_BODY_STRAP_05` = `19113`
- `ID_SH_BODY_STRAP_06` = `19114`
- `ID_SH_BODY_STRAP_07` = `19115`
- `ID_SH_BODY_STRAP_08` = `19116`
- `ID_SH_BODY_STRAP_09` = `19117`
- `ID_SH_L_STIRRUP0` = `9049`
- `ID_SH_L_STIRRUP1` = `9050`
- `ID_SH_L_STIRRUP2` = `9051`
- `ID_SH_L_STIRRUP3` = `9052`
- `ID_SH_R_STIRRUP0` = `50009`
- `ID_SH_R_STIRRUP1` = `50010`
- `ID_SH_R_STIRRUP2` = `50011`
- `ID_SH_R_STIRRUP3` = `50012`
- `ID_SKEL_FRONTSEAT` = `499`
- `ID_SKEL_HEAD` = `21030`
- `ID_SKEL_JAW` = `58559`
- `ID_SKEL_L_CALF` = `55120`
- `ID_SKEL_L_CLAVICLE` = `30226`
- `ID_SKEL_L_FINGER00` = `41403`
- `ID_SKEL_L_FINGER01` = `41404`
- `ID_SKEL_L_FINGER02` = `41405`
- `ID_SKEL_L_FINGER10` = `41323`
- `ID_SKEL_L_FINGER11` = `41324`
- `ID_SKEL_L_FINGER12` = `41325`
- `ID_SKEL_L_FINGER13` = `41326`
- `ID_SKEL_L_FINGER20` = `41307`
- `ID_SKEL_L_FINGER21` = `41308`
- `ID_SKEL_L_FINGER22` = `41309`
- `ID_SKEL_L_FINGER23` = `41310`
- `ID_SKEL_L_FINGER30` = `41355`
- `ID_SKEL_L_FINGER31` = `41356`
- `ID_SKEL_L_FINGER32` = `41357`
- `ID_SKEL_L_FINGER33` = `41358`
- `ID_SKEL_L_FINGER40` = `41339`
- `ID_SKEL_L_FINGER41` = `41340`
- `ID_SKEL_L_FINGER42` = `41341`
- `ID_SKEL_L_FINGER43` = `41342`
- `ID_SKEL_L_FOOT` = `45454`
- `ID_SKEL_L_FOREARM` = `53675`
- `ID_SKEL_L_HAND` = `34606`
- `ID_SKEL_L_SATCHELROOT` = `54203`
- `ID_SKEL_L_SATCHEL_ROOT` = `54203`
- `ID_SKEL_L_TAIL0` = `37103`
- `ID_SKEL_L_THIGH` = `65478`
- `ID_SKEL_L_TOE0` = `53081`
- `ID_SKEL_L_TOE1` = `53082`
- `ID_SKEL_L_UPPERARM` = `37873`
- `ID_SKEL_NECK0` = `14283`
- `ID_SKEL_NECK1` = `14284`
- `ID_SKEL_NECK2` = `14285`
- `ID_SKEL_NECK3` = `14286`
- `ID_SKEL_NECK4` = `14287`
- `ID_SKEL_NECK5` = `14288`
- `ID_SKEL_PELVIS` = `56200`
- `ID_SKEL_REARSEAT` = `36182`
- `ID_SKEL_ROOT` = `0`
- `ID_SKEL_R_CALF` = `43312`
- `ID_SKEL_R_CLAVICLE` = `54802`
- `ID_SKEL_R_FINGER00` = `16827`
- `ID_SKEL_R_FINGER01` = `16828`
- `ID_SKEL_R_FINGER02` = `16829`
- `ID_SKEL_R_FINGER10` = `16747`
- `ID_SKEL_R_FINGER11` = `16748`
- `ID_SKEL_R_FINGER12` = `16749`
- `ID_SKEL_R_FINGER13` = `16750`
- `ID_SKEL_R_FINGER20` = `16731`
- `ID_SKEL_R_FINGER21` = `16732`
- `ID_SKEL_R_FINGER22` = `16733`
- `ID_SKEL_R_FINGER23` = `16734`
- `ID_SKEL_R_FINGER30` = `16779`
- `ID_SKEL_R_FINGER31` = `16780`
- `ID_SKEL_R_FINGER32` = `16781`
- `ID_SKEL_R_FINGER33` = `16782`
- `ID_SKEL_R_FINGER40` = `16763`
- `ID_SKEL_R_FINGER41` = `16764`
- `ID_SKEL_R_FINGER42` = `16765`
- `ID_SKEL_R_FINGER43` = `16766`
- `ID_SKEL_R_FOOT` = `33646`
- `ID_SKEL_R_FOREARM` = `54187`
- `ID_SKEL_R_HAND` = `22798`
- `ID_SKEL_R_SATCHELROOT` = `34484`
- `ID_SKEL_R_SATCHEL_ROOT` = `34484`
- `ID_SKEL_R_TAIL0` = `43676`
- `ID_SKEL_R_THIGH` = `6884`
- `ID_SKEL_R_TOE0` = `41273`
- `ID_SKEL_R_TOE1` = `41274`
- `ID_SKEL_R_UPPERARM` = `46065`
- `ID_SKEL_SADDLE` = `29245`
- `ID_SKEL_SPINE0` = `14410`
- `ID_SKEL_SPINE1` = `14411`
- `ID_SKEL_SPINE10` = `44374`
- `ID_SKEL_SPINE11` = `44375`
- `ID_SKEL_SPINE12` = `44376`
- `ID_SKEL_SPINE13` = `44377`
- `ID_SKEL_SPINE2` = `14412`
- `ID_SKEL_SPINE3` = `14413`
- `ID_SKEL_SPINE4` = `14414`
- `ID_SKEL_SPINE5` = `14415`
- `ID_SKEL_SPINE6` = `14416`
- `ID_SKEL_SPINE7` = `14417`
- `ID_SKEL_SPINE8` = `14418`
- `ID_SKEL_SPINE9` = `14419`
- `ID_SKEL_SPINE_ROOT` = `11569`
- `ID_SKEL_TAIL0` = `30992`
- `ID_SKEL_TAIL1` = `30993`
- `ID_SKEL_TAIL10` = `9113`
- `ID_SKEL_TAIL11` = `9114`
- `ID_SKEL_TAIL12` = `9115`
- `ID_SKEL_TAIL13` = `9116`
- `ID_SKEL_TAIL14` = `9117`
- `ID_SKEL_TAIL15` = `9118`
- `ID_SKEL_TAIL2` = `30994`
- `ID_SKEL_TAIL3` = `30995`
- `ID_SKEL_TAIL4` = `30996`
- `ID_SKEL_TAIL5` = `30997`
- `ID_SKEL_TAIL6` = `30998`
- `ID_SKEL_TAIL7` = `30999`
- `ID_SKEL_TAIL8` = `31000`
- `ID_SKEL_TAIL9` = `31001`
- `ID_WHEEL_LF` = `27922`
- `ID_WHEEL_LR` = `27902`
- `ID_WHEEL_RF` = `26418`
- `ID_WHEEL_RR` = `26398`
- `ID_XH_L_HAND00` = `64687`
- `ID_XH_R_HAND00` = `64847`
- `ID_XH_R_HAND01` = `64848`
- `ID_XL_PELT_MAIN` = `32459`
- `ID_w_melee_lasso03x_Bone_02` = `10508`
- `ID_w_melee_lasso03x_Bone_04` = `10510`
- `ID_w_melee_lasso03x_Bone_05` = `10511`
- `ID_w_melee_lasso03x_Bone_06` = `10512`
- `ID_w_melee_lasso03x_Bone_07` = `10513`
- `ID_w_melee_lasso03x_Bone_08` = `10514`
- `ID_w_melee_lasso03x_Bone_09` = `10515`
- `ID_w_melee_lasso03x_Bone_10` = `10618`
- `ID_w_melee_lasso03x_Bone_11` = `10619`
- `ID_w_melee_lasso03x_Bone_12` = `10620`
- `ID_w_melee_lasso03x_Bone_13` = `10621`
- `ID_w_melee_lasso03x_Bone_14` = `10622`
- `ID_w_melee_lasso03x_Bone_15` = `10623`
- `ID_w_melee_lasso03x_Bone_16` = `10624`
- `IK_dside_seat_feet` = `61027`
- `IK_pside_seat_feet` = `49763`
- `TURRET_1BARREL` = `45666`

## `RDR2.Camera`

- Kind: class
- Base type: `RDR2.PoolObject`

- `public System.Void RDR2.Camera.AttachTo(RDR2.Entity entity, RDR2.Math.Vector3 offset)`
- `public System.Void RDR2.Camera.AttachTo(RDR2.Ped ped, System.Int32 boneIndex, RDR2.Math.Vector3 offset)`
- `public System.Void RDR2.Camera.Delete()`
- `public System.Void RDR2.Camera.Detach()`
- `public System.Boolean RDR2.Camera.Equals(RDR2.Camera obj)`
- `public System.Boolean RDR2.Camera.Equals(System.Object obj)`
- `public System.Boolean RDR2.Camera.Exists()`
- `public static System.Boolean RDR2.Camera.Exists(RDR2.Camera camera)`
- `public System.Int32 RDR2.Camera.GetHashCode()`
- `public RDR2.Math.Vector3 RDR2.Camera.GetOffsetFromWorldCoords(RDR2.Math.Vector3 worldCoords)`
- `public RDR2.Math.Vector3 RDR2.Camera.GetOffsetInWorldCoords(RDR2.Math.Vector3 offset)`
- `public System.Void RDR2.Camera.PointAt(RDR2.Entity target)`
- `public System.Void RDR2.Camera.PointAt(RDR2.Entity target, RDR2.Math.Vector3 offset)`
- `public System.Void RDR2.Camera.PointAt(RDR2.Math.Vector3 target)`
- `public System.Void RDR2.Camera.Render(System.Boolean bShouldRender, System.Boolean bEaseCam = False, System.Int32 iEaseTime = 3000)`
- `public System.Void RDR2.Camera.Shake(RDR2.CameraShake shakeType, System.Single amplitude)`
- `public System.Void RDR2.Camera.StopPointing()`
- `public System.Void RDR2.Camera.StopShaking()`
- `public static System.Boolean RDR2.Camera.op_Equality(RDR2.Camera left, RDR2.Camera right)`
- `public static System.Boolean RDR2.Camera.op_Inequality(RDR2.Camera left, RDR2.Camera right)`

## `RDR2.CameraShake`

- Kind: enum
- Base type: `System.Enum`

*No public methods declared.*

Enum values:
- `DRUNK_SHAKE` = `7`
- `HAND_SHAKE` = `3`
- `JOLT_SHAKE` = `4`
- `LARGE_EXPLOSION_SHAKE` = `2`
- `MEDIUM_EXPLOSION_SHAKE` = `1`
- `NONE` = `-1`
- `SMALL_EXPLOSION_SHAKE` = `0`
- `VIBRATE_SHAKE` = `5`
- `WOBBLY_SHAKE` = `6`

## `RDR2.CourageAttribute`

- Kind: class
- Base type: `RDR2.PedAttribute`

*No public methods declared.*

## `RDR2.DeadEyeAttribute`

- Kind: class
- Base type: `RDR2.PedAttribute`

*No public methods declared.*

## `RDR2.DeadEyeCore`

- Kind: class
- Base type: `RDR2.PedCoreAttribute`

*No public methods declared.*

## `RDR2.DirtinessAttribute`

- Kind: class
- Base type: `RDR2.PedAttribute`

*No public methods declared.*

## `RDR2.DirtinessHatAttribute`

- Kind: class
- Base type: `RDR2.PedAttribute`

*No public methods declared.*

## `RDR2.DirtinessSkinAttribute`

- Kind: class
- Base type: `RDR2.PedAttribute`

*No public methods declared.*

