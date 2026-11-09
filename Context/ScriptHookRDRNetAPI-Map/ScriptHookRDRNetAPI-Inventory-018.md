<!-- ScriptHookRDRNetAPI-Inventory chunk 18 of 26; methods=192; sha256=bfb7607d94b2de7f8530289377f163caad81098afc1fb7ee86191810a6cc6bba -->
## `RDR2.Pickup`

- Kind: class
- Base type: `RDR2.PoolObject`

- `public System.Void RDR2.Pickup.Delete()`
- `public System.Boolean RDR2.Pickup.Equals(RDR2.Pickup obj)`
- `public System.Boolean RDR2.Pickup.Equals(System.Object obj)`
- `public System.Boolean RDR2.Pickup.Exists()`
- `public static System.Boolean RDR2.Pickup.Exists(RDR2.Pickup pickup)`
- `public System.Int32 RDR2.Pickup.GetHashCode()`
- `public System.Boolean RDR2.Pickup.ObjectExists()`
- `public static System.Boolean RDR2.Pickup.op_Equality(RDR2.Pickup left, RDR2.Pickup right)`
- `public static System.Boolean RDR2.Pickup.op_Inequality(RDR2.Pickup left, RDR2.Pickup right)`

## `RDR2.PickupType`

- Kind: enum
- Base type: `System.Enum`

*No public methods declared.*

Enum values:
- `AmmoArrow` = `3978139609`
- `AmmoArrowConfusion` = `3855699813`
- `AmmoArrowDisorient` = `3169064546`
- `AmmoArrowDrain` = `929889182`
- `AmmoArrowWound` = `2138662981`
- `AmmoPistol` = `544828034`
- `AmmoRepeater` = `497466042`
- `AmmoRevolver` = `1979271852`
- `AmmoRifle` = `3837603782`
- `AmmoShotgun` = `2012476125`
- `AmmoSingleArrow` = `1353448401`
- `AmmoSingleArrowConfusion` = `3943753200`
- `AmmoSingleArrowDisorient` = `2358077247`
- `AmmoSingleArrowDrain` = `355461720`
- `AmmoSingleArrowDynamite` = `4269369578`
- `AmmoSingleArrowFire` = `2992705794`
- `AmmoSingleArrowImproved` = `3211263446`
- `AmmoSingleArrowPoison` = `2431867823`
- `AmmoSingleArrowSmallGame` = `1884943282`
- `AmmoSingleArrowTrail` = `1350754031`
- `AmmoSingleArrowWound` = `3149849472`
- `AmmoThrowingKnivesConfusion` = `2922743127`
- `AmmoThrowingKnivesDisorient` = `110127245`
- `AmmoThrowingKnivesDrain` = `2409108311`
- `AmmoThrowingKnivesWound` = `1249983006`
- `CustomScript` = `738282662`
- `HealthStandard` = `2406513688`
- `MoneyVariable` = `4263048111`
- `PortableExplosive` = `1723684897`
- `PortablePackage` = `2158727964`
- `RobbedSatchelItem` = `684949289`
- `WeaponAmmoArrowDynamiteBundle` = `1996182547`
- `WeaponBow` = `3482294004`
- `WeaponBowMp` = `854703859`
- `WeaponFishingrod` = `2926847858`
- `WeaponMeleeAncientHatchet` = `2418276255`
- `WeaponMeleeBrokenSword` = `461723829`
- `WeaponMeleeBrokenSwordMp` = `2912811066`
- `WeaponMeleeCleaver` = `438815942`
- `WeaponMeleeCleaverMp` = `1314665823`
- `WeaponMeleeHatchet` = `2389640029`
- `WeaponMeleeHatchetDoubleBit` = `4158058872`
- `WeaponMeleeHatchetDoubleBitRusted` = `2620822818`
- `WeaponMeleeHatchetHewing` = `185404527`
- `WeaponMeleeHatchetHunter` = `3144257184`
- `WeaponMeleeHatchetHunterRusted` = `1702503778`
- `WeaponMeleeHatchetMp` = `2183204859`
- `WeaponMeleeHatchetViking` = `2791907135`
- `WeaponMeleeKnife` = `3429450153`
- `WeaponMeleeKnifeBear` = `2406793291`
- `WeaponMeleeKnifeCivilWar` = `2298419876`
- `WeaponMeleeKnifeJawbone` = `1824774815`
- `WeaponMeleeKnifeJohn` = `1559494862`
- `WeaponMeleeKnifeMiner` = `1036234969`
- `WeaponMeleeKnifeMp` = `583220986`
- `WeaponMeleeKnifeVampire` = `3861851746`
- `WeaponMeleeLanternElectric` = `244896013`
- `WeaponMeleeLassoMp` = `945354992`
- `WeaponMeleeMachete` = `3532402635`
- `WeaponMeleeMacheteMp` = `1244610670`
- `WeaponMeleeTorch` = `1008526372`
- `WeaponMeleeTorchMp` = `352402388`
- `WeaponMoonshinejug` = `16319985`
- `WeaponPistolMauser` = `3045593999`
- `WeaponPistolMauserDrunk` = `538532644`
- `WeaponPistolMauserMp` = `2113230413`
- `WeaponPistolSemiauto` = `1712010287`
- `WeaponPistolSemiautoMp` = `4031551310`
- `WeaponPistolVolcanic` = `493518845`
- `WeaponPistolVolcanicMp` = `1688152590`
- `WeaponRepeaterCarbine` = `2168626664`
- `WeaponRepeaterCarbineMp` = `2872874642`
- `WeaponRepeaterHenry` = `2008054987`
- `WeaponRepeaterHenryMp` = `3651035686`
- `WeaponRepeaterWinchester` = `3318588934`
- `WeaponRepeaterWinchesterMp` = `308469619`
- `WeaponRevolverCattleman` = `4224840272`
- `WeaponRevolverCattlemanJohn` = `4133361995`
- `WeaponRevolverCattlemanMexican` = `1160884805`
- `WeaponRevolverCattlemanMp` = `1868573732`
- `WeaponRevolverCattlemanPig` = `2430514325`
- `WeaponRevolverDoubleaction` = `118589836`
- `WeaponRevolverDoubleactionExotic` = `3198491192`
- `WeaponRevolverDoubleactionMicah` = `279300629`
- `WeaponRevolverDoubleactionMp` = `66733930`
- `WeaponRevolverSchofield` = `1070119363`
- `WeaponRevolverSchofieldCalloway` = `681867381`
- `WeaponRevolverSchofieldGolden` = `760617877`
- `WeaponRevolverSchofieldMp` = `2701485108`
- `WeaponRifleBoltaction` = `522446563`
- `WeaponRifleBoltactionMp` = `423713854`
- `WeaponRifleSpringfield` = `907844324`
- `WeaponRifleSpringfieldMp` = `2838490755`
- `WeaponRifleVarmint` = `1709589141`
- `WeaponRifleVarmintMp` = `2979486397`
- `WeaponShotgunDoublebarrel` = `3153806410`
- `WeaponShotgunDoublebarrelExotic` = `929211691`
- `WeaponShotgunDoublebarrelMp` = `1767269332`
- `WeaponShotgunPump` = `1961725845`
- `WeaponShotgunPumpMp` = `3962723451`
- `WeaponShotgunRepeating` = `1639058103`
- `WeaponShotgunRepeatingMp` = `428718594`
- `WeaponShotgunSawedoff` = `1198399823`
- `WeaponShotgunSawedoffMp` = `1745420990`
- `WeaponShotgunSemiauto` = `1091187097`
- `WeaponShotgunSemiautoMp` = `151178647`
- `WeaponSingleArrow` = `2256309713`
- `WeaponSingleArrowFire` = `3921815358`
- `WeaponSniperrifleCarcano` = `1875878693`
- `WeaponSniperrifleCarcanoMp` = `2307240145`
- `WeaponSniperrifleRollingblock` = `2321875570`
- `WeaponSniperrifleRollingblockExotic` = `331751277`
- `WeaponSniperrifleRollingblockMp` = `1854500762`
- `WeaponThrownDynamite` = `1659765534`
- `WeaponThrownDynamiteMp` = `2950357727`
- `WeaponThrownDynamiteVolatile` = `4089373537`
- `WeaponThrownMolotov` = `2687718315`
- `WeaponThrownMolotovMp` = `2719172563`
- `WeaponThrownMolotovVolatile` = `3169313105`
- `WeaponThrownThrowingKnives` = `3500664075`
- `WeaponThrownThrowingKnivesConfuse` = `1231750396`
- `WeaponThrownThrowingKnivesDisorient` = `373183416`
- `WeaponThrownThrowingKnivesDrain` = `2235586447`
- `WeaponThrownThrowingKnivesImproved` = `3921925065`
- `WeaponThrownThrowingKnivesMp` = `630632717`
- `WeaponThrownThrowingKnivesPoison` = `1807635241`
- `WeaponThrownThrowingKnivesTrail` = `2786470405`
- `WeaponThrownThrowingKnivesWound` = `125254971`
- `WeaponThrownTomahawk` = `3397143806`
- `WeaponThrownTomahawkAncient` = `973919056`
- `WeaponThrownTomahawkHoming` = `2417257289`
- `WeaponThrownTomahawkImproved` = `3810199903`
- `WeaponThrownTomahawkMp` = `3448951512`

## `RDR2.Player`

- Kind: class
- Base type: `System.Object`

- `public System.Boolean RDR2.Player.ChangeModel(RDR2.Model model)`
- `public System.Void RDR2.Player.DisableFiringThisFrame(System.Boolean toggle)`
- `public System.Boolean RDR2.Player.Equals(RDR2.Player obj)`
- `public System.Boolean RDR2.Player.Equals(System.Object obj)`
- `public System.Boolean RDR2.Player.Exists()`
- `public RDR2.Entity RDR2.Player.GetFreeAimEntity()`
- `public System.Int32 RDR2.Player.GetHashCode()`
- `public RDR2.Entity RDR2.Player.GetInteractionLockonTargetEntity()`
- `public System.Boolean RDR2.Player.IsAimingAtEntity(RDR2.Entity entity)`
- `public System.Void RDR2.Player.SetPlayerControl(System.Boolean bEnabled, RDR2.eSetPlayerControlFlags iFlags = SPC_NONE, System.Boolean bPreventHeadingChange = True)`
- `public System.Void RDR2.Player.SetSuperJumpThisFrame()`
- `public static System.Boolean RDR2.Player.op_Equality(RDR2.Player left, RDR2.Player right)`
- `public static RDR2.Player RDR2.Player.op_Implicit(System.Int32 handle)`
- `public static System.Int32 RDR2.Player.op_Implicit(RDR2.Player e)`
- `public static System.Boolean RDR2.Player.op_Inequality(RDR2.Player left, RDR2.Player right)`

## `RDR2.PoisonedAttribute`

- Kind: class
- Base type: `RDR2.PedAttribute`

*No public methods declared.*

## `RDR2.PoolObject`

- Kind: class
- Base type: `System.Object`

- `public System.Void RDR2.PoolObject.Delete()`
- `public System.Boolean RDR2.PoolObject.Exists()`
- `public static System.Int32 RDR2.PoolObject.op_Implicit(RDR2.PoolObject e)`

## `RDR2.Prop`

- Kind: class
- Base type: `RDR2.Entity`

*No public methods declared.*

## `RDR2.RaycastLOSOptions`

- Kind: enum
- Base type: `System.Enum`

*No public methods declared.*

Enum values:
- `All` = `511`
- `Everything` = `511`
- `Foilage` = `256`
- `Glass` = `64`
- `Map` = `1`
- `None` = `0`
- `Objects` = `16`
- `Peds` = `4`
- `Pickups` = `32`
- `Ragdoll` = `8`
- `Vehicles` = `2`
- `Water` = `128`

## `RDR2.RaycastOptions`

- Kind: enum
- Base type: `System.Enum`

*No public methods declared.*

Enum values:
- `Default` = `7`
- `IgnoreGlass` = `1`
- `IgnoreNoCollision` = `4`
- `IgnoreSeeThrough` = `2`

## `RDR2.RaycastResult`

- Kind: struct
- Base type: `System.ValueType`

*No public methods declared.*

## `RDR2.Raycasting`

- Kind: class
- Base type: `System.Object`

- `public static RDR2.RaycastResult RDR2.Raycasting.CenterScreenRaycast(System.Single maxDist, RDR2.RaycastLOSOptions losFlags = Map, RDR2.Entity entityToIgnore = null)`
- `public static RDR2.RaycastResult RDR2.Raycasting.Raycast(RDR2.Math.Vector3 source, RDR2.Math.Vector3 dest, RDR2.RaycastLOSOptions losFlags = Map, RDR2.Entity entityToIgnore = null, RDR2.RaycastOptions options = Default)`
- `public static RDR2.RaycastResult RDR2.Raycasting.Raycast(RDR2.Math.Vector3 source, RDR2.Math.Vector3 direction, System.Single maxDist, RDR2.RaycastLOSOptions losFlags = Map, RDR2.Entity entityToIgnore = null, RDR2.RaycastOptions options = Default)`

## `RDR2.RelationshipGroup`

- Kind: struct
- Base type: `System.ValueType`

- `public System.Void RDR2.RelationshipGroup.ClearRelationshipBetweenGroups(RDR2.RelationshipGroup targetGroup, RDR2.eRelationshipType relationship, System.Boolean bidirectionally = False)`
- `public System.Boolean RDR2.RelationshipGroup.Equals(RDR2.RelationshipGroup group)`
- `public System.Boolean RDR2.RelationshipGroup.Equals(System.Object obj)`
- `public System.Int32 RDR2.RelationshipGroup.GetHashCode()`
- `public RDR2.eRelationshipType RDR2.RelationshipGroup.GetRelationshipBetweenGroups(RDR2.RelationshipGroup targetGroup)`
- `public System.Void RDR2.RelationshipGroup.Remove()`
- `public System.Void RDR2.RelationshipGroup.SetRelationshipBetweenGroups(RDR2.RelationshipGroup targetGroup, RDR2.eRelationshipType relationship, System.Boolean bidirectionally = False)`
- `public System.String RDR2.RelationshipGroup.ToString()`
- `public static System.Boolean RDR2.RelationshipGroup.op_Equality(RDR2.RelationshipGroup left, RDR2.RelationshipGroup right)`
- `public static RDR2.Native.InputArgument RDR2.RelationshipGroup.op_Implicit(RDR2.RelationshipGroup value)`
- `public static RDR2.RelationshipGroup RDR2.RelationshipGroup.op_Implicit(System.Int32 source)`
- `public static RDR2.RelationshipGroup RDR2.RelationshipGroup.op_Implicit(System.String source)`
- `public static RDR2.RelationshipGroup RDR2.RelationshipGroup.op_Implicit(System.UInt32 source)`
- `public static System.Boolean RDR2.RelationshipGroup.op_Inequality(RDR2.RelationshipGroup left, RDR2.RelationshipGroup right)`

## `RDR2.RequireScript`

- Kind: class
- Base type: `System.Attribute`

*No public methods declared.*

## `RDR2.Rope`

- Kind: class
- Base type: `RDR2.PoolObject`

- `public System.Void RDR2.Rope.ActivatePhysics()`
- `public System.Void RDR2.Rope.Delete()`
- `public System.Void RDR2.Rope.Detach(RDR2.Entity entity)`
- `public System.Boolean RDR2.Rope.Equals(RDR2.Rope obj)`
- `public System.Boolean RDR2.Rope.Equals(System.Object obj)`
- `public System.Boolean RDR2.Rope.Exists()`
- `public static System.Boolean RDR2.Rope.Exists(RDR2.Rope rope)`
- `public System.Int32 RDR2.Rope.GetHashCode()`
- `public RDR2.Math.Vector3 RDR2.Rope.GetVertexCoord(System.Int32 vertex)`
- `public System.Boolean RDR2.Rope.IsAttachedTo(RDR2.Entity entity)`
- `public System.Void RDR2.Rope.Release()`
- `public static System.Boolean RDR2.Rope.op_Equality(RDR2.Rope left, RDR2.Rope right)`
- `public static System.Boolean RDR2.Rope.op_Inequality(RDR2.Rope left, RDR2.Rope right)`

## `RDR2.RopeType`

- Kind: enum
- Base type: `System.Enum`

*No public methods declared.*

Enum values:
- `Normal` = `4`

## `RDR2.Script`

- Kind: class
- Base type: `System.Object`

- `public System.Void RDR2.Script.Abort()`
- `public System.String RDR2.Script.GetRelativeFilePath(System.String filePath)`
- `public static T RDR2.Script.InstantiateScript<T>()`
- `public System.Void RDR2.Script.Pause()`
- `public System.Void RDR2.Script.Resume()`
- `public System.String RDR2.Script.ToString()`
- `public static System.Void RDR2.Script.Wait(System.Int32 ms)`
- `public static System.Void RDR2.Script.Yield()`

## `RDR2.ScriptSettings`

- Kind: class
- Base type: `System.Object`

- `public T[] RDR2.ScriptSettings.GetAllValues<T>(System.String section, System.String name)`
- `public T RDR2.ScriptSettings.GetValue<T>(System.String section, System.String name, T defaultvalue)`
- `public static RDR2.ScriptSettings RDR2.ScriptSettings.Load(System.String filename)`
- `public System.Boolean RDR2.ScriptSettings.Save()`
- `public System.Void RDR2.ScriptSettings.SetValue<T>(System.String section, System.String name, T value)`

## `RDR2.ScriptedSpeechParams`

- Kind: struct
- Base type: `System.ValueType`

*No public methods declared.*

## `RDR2.SicknessAttribute`

- Kind: class
- Base type: `RDR2.PedAttribute`

*No public methods declared.*

## `RDR2.SpeedAttribute`

- Kind: class
- Base type: `RDR2.PedAttribute`

*No public methods declared.*

## `RDR2.StaminaAttribute`

- Kind: class
- Base type: `RDR2.PedAttribute`

*No public methods declared.*

## `RDR2.StaminaCore`

- Kind: class
- Base type: `RDR2.PedCoreAttribute`

*No public methods declared.*

## `RDR2.StrengthAttribute`

- Kind: class
- Base type: `RDR2.PedAttribute`

*No public methods declared.*

## `RDR2.TaskInvoker`

- Kind: class
- Base type: `System.Object`

- `public System.Void RDR2.TaskInvoker.AimAt(RDR2.Entity entity, System.Int32 duration)`
- `public System.Void RDR2.TaskInvoker.AimAt(RDR2.Math.Vector3 coords, System.Int32 duration)`
- `public System.Void RDR2.TaskInvoker.ClearLookAt()`
- `public System.Void RDR2.TaskInvoker.ClearSecondaryTasks()`
- `public System.Void RDR2.TaskInvoker.ClearTasks(System.Boolean immediately)`
- `public System.Void RDR2.TaskInvoker.Climb()`
- `public System.Void RDR2.TaskInvoker.ClimbLadder()`
- `public System.Void RDR2.TaskInvoker.Combat(RDR2.Ped ped, System.Int32 duration)`
- `public System.Void RDR2.TaskInvoker.Cower(System.Int32 duration, RDR2.Ped pedToCowerFrom = null)`
- `public System.Void RDR2.TaskInvoker.CutHogtiedPedFree(RDR2.Ped target)`
- `public System.Void RDR2.TaskInvoker.DismountAnimal(RDR2.eEnterExitVehicleFlags flags = None)`
- `public System.Void RDR2.TaskInvoker.DoHorseAction(System.Int32 action)`
- `public System.Void RDR2.TaskInvoker.DriveToCoord(RDR2.Vehicle vehicle, RDR2.Math.Vector3 coords, System.Single speed, System.UInt32 vehicleModel, RDR2.eDrivingFlags drivingMode, System.Single targetRadius, System.Single straightLineDist)`
- `public System.Void RDR2.TaskInvoker.DriveWander(RDR2.Vehicle vehicle, System.Single speed, RDR2.eDrivingFlags drivingMode)`
- `public System.Void RDR2.TaskInvoker.Duck(System.Int32 time)`
- `public System.Void RDR2.TaskInvoker.EnterCover()`
- `public System.Void RDR2.TaskInvoker.EnterVehicle(RDR2.Vehicle vehicle, System.Int32 timeout, RDR2.eVehicleSeat seat, System.Single speed, RDR2.eEnterExitVehicleFlags flags = ResumeIfInterrupted)`
- `public System.Void RDR2.TaskInvoker.ExitCover()`
- `public System.Void RDR2.TaskInvoker.FightAgainstHatedTargets(System.Single radius)`
- `public System.Void RDR2.TaskInvoker.FightAgainstHatedTargets(System.Single radius, System.Single duration)`
- `public System.Void RDR2.TaskInvoker.Flee(RDR2.Math.Vector3 fleeFrom, System.Int32 duration = -1, RDR2.eFleeStyle fleeStyle = MajorThreat)`
- `public System.Void RDR2.TaskInvoker.Flee(RDR2.Ped pedToFleeFrom, System.Int32 duration = -1, RDR2.eFleeStyle fleeStyle = MajorThreat)`
- `public System.Void RDR2.TaskInvoker.FlyAway(RDR2.Ped pedToFlyFrom)`
- `public System.Void RDR2.TaskInvoker.FlyToCoord(RDR2.Math.Vector3 coords, System.Single speed)`
- `public System.Void RDR2.TaskInvoker.FollowRoad(System.Single speed, RDR2.Math.Vector3 destination = null)`
- `public System.Void RDR2.TaskInvoker.FollowToOffsetOfEntity(RDR2.Entity entity, RDR2.Math.Vector3 offset, System.Single speed, System.Int32 time = -1, System.Single radius = 0.1, System.Boolean relativeOffset = True)`
- `public System.Void RDR2.TaskInvoker.ForceMotionState(System.UInt32 motionStateHash)`
- `public System.Void RDR2.TaskInvoker.GoStraightTo(RDR2.Math.Vector3 coords, System.Single speed, System.Int32 time, System.Single finalHeading, System.Single radius = 0.5)`
- `public System.Void RDR2.TaskInvoker.GoTo(RDR2.Entity entity, System.Single speed, System.Int32 duration, RDR2.Math.Vector2 offset = null)`
- `public System.Void RDR2.TaskInvoker.GoTo(RDR2.Math.Vector3 coords, System.Single speed, System.Int32 timeout)`
- `public System.Void RDR2.TaskInvoker.GoToWhistle(RDR2.Ped whistlerPed)`
- `public System.Void RDR2.TaskInvoker.GuardCurrentPosition(System.Single patrolProximity, System.Single radius)`
- `public System.Void RDR2.TaskInvoker.HandsUp(System.Int32 duration, RDR2.Ped pedToFace = null, System.Int32 timeToFacePed = -1)`
- `public System.Void RDR2.TaskInvoker.HitchAnimal(RDR2.Ped mount, RDR2.Math.Vector3 coords)`
- `public System.Void RDR2.TaskInvoker.HitchAnimal(RDR2.Ped mount, System.Single hitchingPostSearchRadius)`
- `public System.Void RDR2.TaskInvoker.HogtiePed(RDR2.Ped target)`
- `public System.Void RDR2.TaskInvoker.Jump()`
- `public System.Void RDR2.TaskInvoker.KnockedOut(System.Single angle, System.Boolean permanently)`
- `public System.Void RDR2.TaskInvoker.KnockedOutAndHogtied(System.Single angle, System.Boolean immediately)`
- `public System.Void RDR2.TaskInvoker.LassoPed(RDR2.Ped ped)`
- `public System.Void RDR2.TaskInvoker.LeadHorse(RDR2.Ped horse)`
- `public System.Void RDR2.TaskInvoker.LeaveAnyVehicle(RDR2.eEnterExitVehicleFlags flags = None)`
- `public System.Void RDR2.TaskInvoker.LeaveVehicle(RDR2.Vehicle vehicle, RDR2.eEnterExitVehicleFlags flags = None)`
- `public System.Void RDR2.TaskInvoker.LookAt(RDR2.Entity entity, System.Int32 duration)`
- `public System.Void RDR2.TaskInvoker.LookAt(RDR2.Math.Vector3 coords, System.Int32 duration)`
- `public System.Void RDR2.TaskInvoker.LootEntity(RDR2.Entity entity)`
- `public System.Void RDR2.TaskInvoker.MountAnimal(RDR2.Ped mount, System.Int32 time, RDR2.eVehicleSeat seat, System.Single speed, RDR2.eEnterExitVehicleFlags flags = ResumeIfInterrupted)`
- `public System.Void RDR2.TaskInvoker.Pause(System.Int32 ms)`
- `public System.Void RDR2.TaskInvoker.PerformSequence(RDR2.TaskSequence sequence)`
- `public System.Void RDR2.TaskInvoker.PickupCarriableEntity(RDR2.Entity entity)`
- `public System.Void RDR2.TaskInvoker.PlantBomb(RDR2.Math.Vector3 coords, System.Single heading)`
- `public System.Void RDR2.TaskInvoker.PlayAnimation(System.String animDict, System.String animName, System.Int32 duration)`
- `public System.Void RDR2.TaskInvoker.PlayAnimation(System.String animDict, System.String animName, System.Int32 duration, System.Single speed = 8)`
- `public System.Void RDR2.TaskInvoker.PlayAnimation(System.String animDict, System.String animName, System.Int32 duration, System.Single speed = 8, RDR2.eScriptedAnimFlags animFlags = None, RDR2.eIkControlFlags ikflags = None)`
- `public System.Void RDR2.TaskInvoker.PlayAnimation(System.String animDict, System.String animName, System.Single blendInSpeed, System.Single blendOutSpeed, System.Int32 duration, RDR2.eScriptedAnimFlags animFlags = None, RDR2.eIkControlFlags ikFlags = None, System.String filter = "")`
- `public System.Void RDR2.TaskInvoker.React(RDR2.Math.Vector3 reactTowards, System.String reactionName)`
- `public System.Void RDR2.TaskInvoker.React(RDR2.Ped pedToReactTo, System.String reactionName)`
- `public System.Void RDR2.TaskInvoker.React(System.Int32 shockingEventHandle)`
- `public System.Void RDR2.TaskInvoker.Reload()`
- `public System.Void RDR2.TaskInvoker.SeekCoverFrom(RDR2.Math.Vector3 coords, System.Int32 time)`
- `public System.Void RDR2.TaskInvoker.SeekCoverFrom(RDR2.Ped ped, System.Int32 time)`
- `public System.Void RDR2.TaskInvoker.ShootAt(RDR2.Entity entity, System.Int32 duration, RDR2.eFiringPattern firingPattern)`
- `public System.Void RDR2.TaskInvoker.ShootAt(RDR2.Math.Vector3 coords, System.Int32 duration, RDR2.eFiringPattern firingPattern)`
- `public System.Void RDR2.TaskInvoker.ShuffleToNextVehicleSeat(RDR2.Vehicle vehicle)`
- `public System.Void RDR2.TaskInvoker.SlideTo(RDR2.Entity entity, System.Single heading, System.Single speed = 0.75)`
- `public System.Void RDR2.TaskInvoker.SlideTo(RDR2.Math.Vector3 coords, System.Single heading, System.Single speed = 0.75)`
- `public System.Void RDR2.TaskInvoker.StandGuard(RDR2.Math.Vector3 coords)`
- `public System.Void RDR2.TaskInvoker.StandStill(System.Int32 time)`
- `public System.Void RDR2.TaskInvoker.StartItemInteraction(System.UInt32 itemHash, System.UInt32 interactionAnimHash)`
- `public System.Void RDR2.TaskInvoker.StartScenario(System.UInt32 scenarioHash, RDR2.Math.Vector3 position, System.Int32 duration, System.Boolean teleportToScenario, System.Boolean isSittingScenario)`
- `public System.Void RDR2.TaskInvoker.StopAnimation(System.String animDict, System.String animName)`
- `public System.Void RDR2.TaskInvoker.StopLeadingHorse()`
- `public System.Void RDR2.TaskInvoker.TurnTo(RDR2.Entity entity, System.Int32 duration)`
- `public System.Void RDR2.TaskInvoker.TurnTo(RDR2.Math.Vector3 coords, System.Int32 duration)`
- `public System.Void RDR2.TaskInvoker.UseRandomScenarioInGroup(System.UInt32 scenarioGroupHash)`
- `public System.Void RDR2.TaskInvoker.WalkAway(RDR2.Entity entityToWalkFrom)`
- `public System.Void RDR2.TaskInvoker.WanderAround()`
- `public System.Void RDR2.TaskInvoker.WanderAround(RDR2.Math.Vector3 position, System.Single radius)`

## `RDR2.TaskSequence`

- Kind: class
- Base type: `System.Object`

- `public System.Void RDR2.TaskSequence.Close()`
- `public System.Void RDR2.TaskSequence.Close(System.Boolean repeat)`
- `public System.Void RDR2.TaskSequence.Dispose()`

## `RDR2.UI.Alignment`

- Kind: enum
- Base type: `System.Enum`

*No public methods declared.*

Enum values:
- `Center` = `0`
- `Left` = `1`
- `Right` = `2`

## `RDR2.UI.ContainerElement`

- Kind: class
- Base type: `System.Object`

- `public System.Void RDR2.UI.ContainerElement.Draw()`
- `public System.Void RDR2.UI.ContainerElement.Draw(System.Drawing.SizeF offset)`
- `public System.Void RDR2.UI.ContainerElement.ScaledDraw()`
- `public System.Void RDR2.UI.ContainerElement.ScaledDraw(System.Drawing.SizeF offset)`

## `RDR2.UI.CustomSprite`

- Kind: class
- Base type: `System.Object`

- `public System.Void RDR2.UI.CustomSprite.Draw()`
- `public System.Void RDR2.UI.CustomSprite.Draw(System.Drawing.SizeF offset)`
- `public System.Void RDR2.UI.CustomSprite.ScaledDraw()`
- `public System.Void RDR2.UI.CustomSprite.ScaledDraw(System.Drawing.SizeF offset)`

## `RDR2.UI.Drawing`

- Kind: class
- Base type: `System.Object`

- `public static System.Void RDR2.UI.Drawing.DrawRect(System.Single screenX, System.Single screenY, System.Single width, System.Single height, System.Int32 red, System.Int32 green, System.Int32 blue, System.Int32 alpha, System.Boolean centered)`
- `public static System.Void RDR2.UI.Drawing.DrawSprite(System.String textureDict, System.String textureName, System.Single screenX, System.Single screenY, System.Single width, System.Single height, System.Single rotation, System.Int32 red, System.Int32 green, System.Int32 blue, System.Int32 alpha, System.Boolean centered)`
- `public static System.Void RDR2.UI.Drawing.DrawText(System.String text, System.Single screenX, System.Single screenY, System.Single scale, System.Int32 red, System.Int32 green, System.Int32 blue, System.Int32 alpha)`

## `RDR2.UI.Font`

- Kind: enum
- Base type: `System.Enum`

*No public methods declared.*

Enum values:
- `Arial` = `15`
- `ArialDEBUG` = `4`
- `CabritoNormDemi` = `6`
- `DreamerScript` = `12`
- `DroidSerifPro` = `1`
- `HapnaSlabSerifDemiBold` = `0`
- `HelveticaNeueLT47LightCn` = `8`
- `RDRCatalogueBold` = `5`
- `RDRCatalogueNumbers` = `3`
- `RDRChalkHand` = `7`
- `RDRGothica` = `2`
- `RDRLedgerHand` = `13`
- `RDRLino` = `16`
- `RDRLinoNumbers` = `9`
- `RDRTurri` = `17`
- `Redemption` = `10`
- `RockstarGamertagCond` = `11`
- `RockstarTag` = `14`

## `RDR2.UI.Hud`

- Kind: class
- Base type: `System.Object`

- `public static System.Void RDR2.UI.Hud.DisableHudContext(RDR2.UI.eHudContext component)`
- `public static System.Void RDR2.UI.Hud.EnableHudContext(RDR2.UI.eHudContext component)`
- `public static System.Void RDR2.UI.Hud.ShowCursorThisFrame()`

## `RDR2.UI.IElement`

- Kind: interface

- `public System.Void RDR2.UI.IElement.Draw()`
- `public System.Void RDR2.UI.IElement.Draw(System.Drawing.SizeF offset)`
- `public System.Void RDR2.UI.IElement.ScaledDraw()`
- `public System.Void RDR2.UI.IElement.ScaledDraw(System.Drawing.SizeF offset)`

## `RDR2.UI.ISpriteElement`

- Kind: interface

*No public methods declared.*

## `RDR2.UI.PromptTimingEvent`

- Kind: enum
- Base type: `System.Enum`

*No public methods declared.*

Enum values:
- `Long` = `2275990963`
- `Medium` = `3474870750`
- `PlayerFocusTiming` = `1753096650`
- `PlayerReactionTiming` = `1891588183`
- `RustlingCalmTiming` = `3130155889`
- `Short` = `1704213876`

## `RDR2.UI.Screen`

- Kind: class
- Base type: `System.Object`

- `public static System.Void RDR2.UI.Screen.ClearEffect(System.String effectName)`
- `public static System.Void RDR2.UI.Screen.DisplaySubtitle(System.String message)`
- `public static System.Void RDR2.UI.Screen.DisplaySubtitle(params System.String[] message)`
- `public static System.Void RDR2.UI.Screen.FadeIn(System.Int32 duration)`
- `public static System.Void RDR2.UI.Screen.FadeOut(System.Int32 duration)`
- `public static System.Boolean RDR2.UI.Screen.IsEffectActive(System.String effectName)`
- `public static System.Void RDR2.UI.Screen.PlayEffect(System.String effectName)`
- `public static System.Void RDR2.UI.Screen.PrintSubtitle(System.String message)`
- `public static System.Void RDR2.UI.Screen.PrintSubtitle(params System.String[] message)`
- `public static System.Void RDR2.UI.Screen.RequestLetterBox()`
- `public static System.Void RDR2.UI.Screen.RequestLetterBoxImmediately(System.Boolean bEnabled)`
- `public static System.Void RDR2.UI.Screen.StopAllEffects()`
- `public static System.Void RDR2.UI.Screen.StopEffect(System.String effectName)`
- `public static System.Drawing.PointF RDR2.UI.Screen.WorldToScreen(RDR2.Math.Vector3 position, System.Boolean scaleWidth = False)`

## `RDR2.UI.Sprite`

- Kind: class
- Base type: `System.Object`

- `public System.Void RDR2.UI.Sprite.Dispose()`
- `public System.Void RDR2.UI.Sprite.Draw()`
- `public System.Void RDR2.UI.Sprite.Draw(System.Drawing.SizeF offset)`
- `public System.Void RDR2.UI.Sprite.ScaledDraw()`
- `public System.Void RDR2.UI.Sprite.ScaledDraw(System.Drawing.SizeF offset)`

## `RDR2.UI.TextElement`

- Kind: class
- Base type: `System.Object`

- `public System.Void RDR2.UI.TextElement.Draw()`
- `public System.Void RDR2.UI.TextElement.Draw(System.Drawing.SizeF offset)`
- `public System.Void RDR2.UI.TextElement.ScaledDraw()`
- `public System.Void RDR2.UI.TextElement.ScaledDraw(System.Drawing.SizeF offset)`

## `RDR2.UI.eCursor`

- Kind: enum
- Base type: `System.Enum`

*No public methods declared.*

Enum values:
- `Arrow` = `0`
- `ArrowDimmed` = `1`
- `ArrowDown` = `3`
- `ArrowLeft` = `4`
- `ArrowNegative` = `7`
- `ArrowPositive` = `6`
- `ArrowRight` = `5`
- `ArrowTrimming` = `8`
- `ArrowUp` = `2`
- `HandFinger` = `11`
- `HandFingerDimmed` = `12`
- `HandFingerDollar` = `20`
- `HandFingerDown` = `14`
- `HandFingerLeft` = `15`
- `HandFingerNegative` = `18`
- `HandFingerPositive` = `17`
- `HandFingerRight` = `16`
- `HandFingerTrimming` = `19`
- `HandFingerUp` = `13`
- `HandGrab` = `10`
- `HandOpen` = `9`
- `Normal` = `0`

