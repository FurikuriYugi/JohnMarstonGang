<!-- ScriptHookRDRNetAPI-Inventory chunk 2 of 26; methods=426; sha256=bfb7607d94b2de7f8530289377f163caad81098afc1fb7ee86191810a6cc6bba -->
## `RDR2.Entity`

- Kind: class
- Base type: `RDR2.PoolObject`

- `public RDR2.Blip RDR2.Entity.AddBlip(RDR2.BlipType blipType)`
- `public System.Void RDR2.Entity.ApplyForce(RDR2.Math.Vector3 direction)`
- `public System.Void RDR2.Entity.ApplyForce(RDR2.Math.Vector3 direction, RDR2.Math.Vector3 rotation)`
- `public System.Void RDR2.Entity.ApplyForce(RDR2.Math.Vector3 direction, RDR2.Math.Vector3 rotation, System.Int32 forceType)`
- `public System.Void RDR2.Entity.ApplyForceRelative(RDR2.Math.Vector3 direction)`
- `public System.Void RDR2.Entity.ApplyForceRelative(RDR2.Math.Vector3 direction, RDR2.Math.Vector3 rotation)`
- `public System.Void RDR2.Entity.ApplyForceRelative(RDR2.Math.Vector3 direction, RDR2.Math.Vector3 rotation, System.Int32 forceType)`
- `public System.Void RDR2.Entity.AttachTo(RDR2.Entity entity, System.Int32 boneIndex)`
- `public System.Void RDR2.Entity.AttachTo(RDR2.Entity entity, System.Int32 boneIndex, RDR2.Math.Vector3 position, RDR2.Math.Vector3 rotation)`
- `public System.Void RDR2.Entity.ClearProofs()`
- `public System.Void RDR2.Entity.Delete()`
- `public System.Void RDR2.Entity.Detach()`
- `public System.Boolean RDR2.Entity.Equals(RDR2.Entity obj)`
- `public System.Boolean RDR2.Entity.Equals(System.Object obj)`
- `public System.Boolean RDR2.Entity.Exists()`
- `public static System.Boolean RDR2.Entity.Exists(RDR2.Entity entity)`
- `public static RDR2.Entity RDR2.Entity.FromHandle(System.Int32 handle)`
- `public RDR2.Entity RDR2.Entity.GetEntityAttachedTo()`
- `public System.Int32 RDR2.Entity.GetHashCode()`
- `public RDR2.Math.Vector3 RDR2.Entity.GetOffsetPosition(RDR2.Math.Vector3 offset)`
- `public RDR2.Math.Vector3 RDR2.Entity.GetPositionOffset(RDR2.Math.Vector3 worldCoords)`
- `public System.Boolean RDR2.Entity.IsAtEntity(RDR2.Entity entity, RDR2.Math.Vector3 distance)`
- `public System.Boolean RDR2.Entity.IsAttachedTo(RDR2.Entity entity)`
- `public System.Boolean RDR2.Entity.IsInAngledArea(RDR2.Math.Vector3 origin, RDR2.Math.Vector3 edge, System.Single angle)`
- `public System.Boolean RDR2.Entity.IsInArea(RDR2.Math.Vector3 minBounds, RDR2.Math.Vector3 maxBounds)`
- `public System.Boolean RDR2.Entity.IsInArea(RDR2.Math.Vector3 pos1, RDR2.Math.Vector3 pos2, System.Single angle)`
- `public System.Boolean RDR2.Entity.IsInRangeOf(RDR2.Math.Vector3 position, System.Single distance)`
- `public System.Boolean RDR2.Entity.IsNearEntity(RDR2.Entity entity, RDR2.Math.Vector3 distance)`
- `public System.Boolean RDR2.Entity.IsTouching(RDR2.Entity entity)`
- `public System.Boolean RDR2.Entity.IsTouching(RDR2.Model model)`
- `public System.Boolean RDR2.Entity.RequestOwnership()`
- `public System.Void RDR2.Entity.SetNoCollision(RDR2.Entity entity, System.Boolean toggle)`
- `public static System.Boolean RDR2.Entity.op_Equality(RDR2.Entity left, RDR2.Entity right)`
- `public static RDR2.Entity RDR2.Entity.op_Implicit(System.Int32 handle)`
- `public static System.Boolean RDR2.Entity.op_Inequality(RDR2.Entity left, RDR2.Entity right)`

## `RDR2.EventReaction`

- Kind: enum
- Base type: `System.Enum`

*No public methods declared.*

Enum values:
- `DefaultAngry` = `1345150177`
- `DefaultBrave` = `1781933509`
- `DefaultCurious` = `-1778605437`
- `DefaultDefuse` = `-1675652957`
- `DefaultPanic` = `1618376518`
- `DefaultScared` = `-1967172690`
- `DefaultShocked` = `-372548123`
- `FleeHumanMajorThreat` = `-2111647205`
- `FleeScared` = `759577278`
- `TaskCombatHigh` = `1103872808`
- `TaskCombatMedium` = `623557147`
- `TaskCombatPanic` = `-996719768`
- `TaskCombatReact` = `-1342511871`

## `RDR2.FatiguedAttribute`

- Kind: class
- Base type: `RDR2.PedAttribute`

*No public methods declared.*

## `RDR2.Game`

- Kind: class
- Base type: `System.Object`

- `public static System.Void RDR2.Game.DisableAllControlsThisFrame()`
- `public static System.Void RDR2.Game.DisableControlThisFrame(RDR2.eInputType control)`
- `public static System.Boolean RDR2.Game.DoesGXTEntryExist(System.String entry)`
- `public static System.Void RDR2.Game.EnableControlThisFrame(RDR2.eInputType control)`
- `public static System.Void RDR2.Game.FadeScreenIn(System.Int32 duration)`
- `public static System.Void RDR2.Game.FadeScreenOut(System.Int32 duration)`
- `public static System.Single RDR2.Game.GetControlNormal(RDR2.eInputType control)`
- `public static System.Int32 RDR2.Game.GetControlValue(RDR2.eInputType control)`
- `public static System.Single RDR2.Game.GetDisabledControlNormal(RDR2.eInputType control)`
- `public static System.String RDR2.Game.GetGXTEntry(System.String entry)`
- `public static RDR2.Global RDR2.Game.GetGlobalPtr(System.Int32 globalId)`
- `public static System.String RDR2.Game.GetUserInput(System.Int32 maxLength)`
- `public static System.String RDR2.Game.GetUserInput(System.String windowTitle, System.Int32 maxLength)`
- `public static System.String RDR2.Game.GetUserInput(System.String windowTitle, System.String defaultText, System.Int32 maxLength)`
- `public static System.Boolean RDR2.Game.IsControlEnabled(RDR2.eInputType control)`
- `public static System.Boolean RDR2.Game.IsControlJustPressed(RDR2.eInputType control)`
- `public static System.Boolean RDR2.Game.IsControlJustReleased(RDR2.eInputType control)`
- `public static System.Boolean RDR2.Game.IsControlPressed(RDR2.eInputType control)`
- `public static System.Boolean RDR2.Game.IsControlReleased(RDR2.eInputType control)`
- `public static System.Boolean RDR2.Game.IsDisabledControlJustPressed(RDR2.eInputType control)`
- `public static System.Boolean RDR2.Game.IsDisabledControlJustReleased(RDR2.eInputType control)`
- `public static System.Boolean RDR2.Game.IsDisabledControlPressed(RDR2.eInputType control)`
- `public static System.UInt32 RDR2.Game.Joaat(System.String key)`
- `public static System.Void RDR2.Game.Pause(System.Boolean toggle)`
- `public static System.Void RDR2.Game.PauseClock(System.Boolean toggle)`
- `public static System.Void RDR2.Game.SetControlNormal(RDR2.eInputType control, System.Single value)`

## `RDR2.GameVersion`

- Kind: enum
- Base type: `System.Enum`

*No public methods declared.*

Enum values:
- `Unknown` = `-1`
- `v1_0_1207_60` = `0`
- `v1_0_1207_69` = `1`
- `v1_0_1207_73` = `2`
- `v1_0_1207_77` = `3`
- `v1_0_1207_80` = `4`
- `v1_0_1232_13` = `5`
- `v1_0_1232_17` = `6`
- `v1_0_1311_12` = `7`
- `v1_0_1436_25` = `8`
- `v1_0_1436_31` = `9`
- `v1_0_1491_16` = `10`

## `RDR2.GameplayCamera`

- Kind: class
- Base type: `System.Object`

- `public static System.Void RDR2.GameplayCamera.ClampPitch(System.Single min, System.Single max)`
- `public static System.Void RDR2.GameplayCamera.ClampYaw(System.Single min, System.Single max)`
- `public static RDR2.Math.Vector3 RDR2.GameplayCamera.GetOffsetFromWorldCoords(RDR2.Math.Vector3 worldCoords)`
- `public static RDR2.Math.Vector3 RDR2.GameplayCamera.GetOffsetInWorldCoords(RDR2.Math.Vector3 offset)`

## `RDR2.Gender`

- Kind: enum
- Base type: `System.Enum`

*No public methods declared.*

Enum values:
- `Female` = `1`
- `Male` = `0`

## `RDR2.Global`

- Kind: struct
- Base type: `System.ValueType`

- `public System.Single RDR2.Global.GetFloat()`
- `public System.Int32 RDR2.Global.GetInt()`
- `public System.String RDR2.Global.GetString()`
- `public RDR2.Math.Vector3 RDR2.Global.GetVector3()`
- `public System.Void RDR2.Global.SetFloat(System.Single value)`
- `public System.Void RDR2.Global.SetInt(System.Int32 value)`
- `public System.Void RDR2.Global.SetString(System.String value)`
- `public System.Void RDR2.Global.SetVector3(RDR2.Math.Vector3 value)`

## `RDR2.GlobalCollection`

- Kind: class
- Base type: `System.Object`

- `public RDR2.Global RDR2.GlobalCollection.Get(System.Int32 globalId)`

## `RDR2.GritAttribute`

- Kind: class
- Base type: `RDR2.PedAttribute`

*No public methods declared.*

## `RDR2.HealthAttribute`

- Kind: class
- Base type: `RDR2.PedAttribute`

*No public methods declared.*

## `RDR2.HealthCore`

- Kind: class
- Base type: `RDR2.PedCoreAttribute`

*No public methods declared.*

## `RDR2.HungerAttribute`

- Kind: class
- Base type: `RDR2.PedAttribute`

*No public methods declared.*

## `RDR2.IDeletable`

- Kind: interface

- `public System.Void RDR2.IDeletable.Delete()`

## `RDR2.IExistable`

- Kind: interface

- `public System.Boolean RDR2.IExistable.Exists()`

## `RDR2.IHandleable`

- Kind: interface

- `public System.Boolean RDR2.IHandleable.Exists()`

## `RDR2.ISpatial`

- Kind: interface

*No public methods declared.*

## `RDR2.InebriatedAttribute`

- Kind: class
- Base type: `RDR2.PedAttribute`

*No public methods declared.*

## `RDR2.InputMethod`

- Kind: enum
- Base type: `System.Enum`

*No public methods declared.*

Enum values:
- `GamePad` = `1`
- `Keyboard` = `0`

## `RDR2.InstinctAttribute`

- Kind: class
- Base type: `RDR2.PedAttribute`

*No public methods declared.*

## `RDR2.MarkerType`

- Kind: enum
- Base type: `System.Enum`

*No public methods declared.*

Enum values:
- `Cube` = `1857541051`
- `Cylinder` = `2499653143`
- `Halo` = `1761849619`
- `Halo_Point` = `3594803014`
- `Halo_Rotate` = `131916342`
- `Num_0` = `704524378`
- `Num_1` = `3821609969`
- `Num_2` = `3581904734`
- `Num_3` = `1080515868`
- `Num_4` = `1318386039`
- `Num_5` = `592159461`
- `Num_6` = `4189212595`
- `Num_7` = `123726606`
- `Num_8` = `3716388694`
- `Ring` = `3959630557`
- `Sphere` = `1348700857`
- `num_9` = `3925225531`

## `RDR2.Math.Matrix`

- Kind: struct
- Base type: `System.ValueType`

- `public static RDR2.Math.Matrix RDR2.Math.Matrix.Add(RDR2.Math.Matrix left, RDR2.Math.Matrix right)`
- `public System.Single RDR2.Math.Matrix.Determinant()`
- `public static RDR2.Math.Matrix RDR2.Math.Matrix.Divide(RDR2.Math.Matrix left, RDR2.Math.Matrix right)`
- `public static RDR2.Math.Matrix RDR2.Math.Matrix.Divide(RDR2.Math.Matrix left, System.Single right)`
- `public System.Boolean RDR2.Math.Matrix.Equals(RDR2.Math.Matrix other)`
- `public System.Boolean RDR2.Math.Matrix.Equals(System.Object obj)`
- `public static System.Boolean RDR2.Math.Matrix.Equals(ref RDR2.Math.Matrix value1, ref RDR2.Math.Matrix value2)`
- `public static RDR2.Math.Matrix RDR2.Math.Matrix.FromArray(System.Single[] floatArray)`
- `public System.Int32 RDR2.Math.Matrix.GetHashCode()`
- `public System.Void RDR2.Math.Matrix.Inverse()`
- `public static RDR2.Math.Matrix RDR2.Math.Matrix.Inverse(RDR2.Math.Matrix matrix)`
- `public static RDR2.Math.Matrix RDR2.Math.Matrix.Lerp(RDR2.Math.Matrix start, RDR2.Math.Matrix end, System.Single amount)`
- `public static RDR2.Math.Matrix RDR2.Math.Matrix.Multiply(RDR2.Math.Matrix left, RDR2.Math.Matrix right)`
- `public static RDR2.Math.Matrix RDR2.Math.Matrix.Multiply(RDR2.Math.Matrix left, System.Single right)`
- `public static RDR2.Math.Matrix RDR2.Math.Matrix.Negate(RDR2.Math.Matrix matrix)`
- `public static RDR2.Math.Matrix RDR2.Math.Matrix.RotationAxis(RDR2.Math.Vector3 axis, System.Single angle)`
- `public static RDR2.Math.Matrix RDR2.Math.Matrix.RotationQuaternion(RDR2.Math.Quaternion rotation)`
- `public static RDR2.Math.Matrix RDR2.Math.Matrix.RotationX(System.Single angle)`
- `public static RDR2.Math.Matrix RDR2.Math.Matrix.RotationY(System.Single angle)`
- `public static RDR2.Math.Matrix RDR2.Math.Matrix.RotationYawPitchRoll(System.Single yaw, System.Single pitch, System.Single roll)`
- `public static RDR2.Math.Matrix RDR2.Math.Matrix.RotationZ(System.Single angle)`
- `public static RDR2.Math.Matrix RDR2.Math.Matrix.Scaling(RDR2.Math.Vector3 scale)`
- `public static RDR2.Math.Matrix RDR2.Math.Matrix.Scaling(System.Single x, System.Single y, System.Single z)`
- `public static RDR2.Math.Matrix RDR2.Math.Matrix.Subtract(RDR2.Math.Matrix left, RDR2.Math.Matrix right)`
- `public System.Single[] RDR2.Math.Matrix.ToArray()`
- `public System.String RDR2.Math.Matrix.ToString()`
- `public static RDR2.Math.Matrix RDR2.Math.Matrix.Translation(RDR2.Math.Vector3 amount)`
- `public static RDR2.Math.Matrix RDR2.Math.Matrix.Translation(System.Single x, System.Single y, System.Single z)`
- `public static RDR2.Math.Matrix RDR2.Math.Matrix.Transpose(RDR2.Math.Matrix matrix)`
- `public static RDR2.Math.Matrix RDR2.Math.Matrix.op_Addition(RDR2.Math.Matrix left, RDR2.Math.Matrix right)`
- `public static RDR2.Math.Matrix RDR2.Math.Matrix.op_Division(RDR2.Math.Matrix left, RDR2.Math.Matrix right)`
- `public static RDR2.Math.Matrix RDR2.Math.Matrix.op_Division(RDR2.Math.Matrix left, System.Single right)`
- `public static System.Boolean RDR2.Math.Matrix.op_Equality(RDR2.Math.Matrix left, RDR2.Math.Matrix right)`
- `public static System.Boolean RDR2.Math.Matrix.op_Inequality(RDR2.Math.Matrix left, RDR2.Math.Matrix right)`
- `public static RDR2.Math.Matrix RDR2.Math.Matrix.op_Multiply(RDR2.Math.Matrix left, RDR2.Math.Matrix right)`
- `public static RDR2.Math.Matrix RDR2.Math.Matrix.op_Multiply(RDR2.Math.Matrix left, System.Single right)`
- `public static RDR2.Math.Matrix RDR2.Math.Matrix.op_Multiply(System.Single left, RDR2.Math.Matrix right)`
- `public static RDR2.Math.Matrix RDR2.Math.Matrix.op_Subtraction(RDR2.Math.Matrix left, RDR2.Math.Matrix right)`
- `public static RDR2.Math.Matrix RDR2.Math.Matrix.op_UnaryNegation(RDR2.Math.Matrix matrix)`

## `RDR2.Math.Quaternion`

- Kind: struct
- Base type: `System.ValueType`

- `public static RDR2.Math.Quaternion RDR2.Math.Quaternion.Add(RDR2.Math.Quaternion left, RDR2.Math.Quaternion right)`
- `public static System.Single RDR2.Math.Quaternion.AngleBetween(RDR2.Math.Quaternion a, RDR2.Math.Quaternion b)`
- `public System.Void RDR2.Math.Quaternion.Conjugate()`
- `public static RDR2.Math.Quaternion RDR2.Math.Quaternion.Conjugate(RDR2.Math.Quaternion value)`
- `public static RDR2.Math.Quaternion RDR2.Math.Quaternion.Divide(RDR2.Math.Quaternion left, RDR2.Math.Quaternion right)`
- `public static System.Single RDR2.Math.Quaternion.Dot(RDR2.Math.Quaternion left, RDR2.Math.Quaternion right)`
- `public System.Boolean RDR2.Math.Quaternion.Equals(RDR2.Math.Quaternion other)`
- `public System.Boolean RDR2.Math.Quaternion.Equals(System.Object obj)`
- `public static System.Boolean RDR2.Math.Quaternion.Equals(ref RDR2.Math.Quaternion value1, ref RDR2.Math.Quaternion value2)`
- `public static RDR2.Math.Quaternion RDR2.Math.Quaternion.Euler(RDR2.Math.Vector3 euler)`
- `public static RDR2.Math.Quaternion RDR2.Math.Quaternion.Euler(System.Single zaxis, System.Single xaxis, System.Single yaxis)`
- `public static RDR2.Math.Quaternion RDR2.Math.Quaternion.FromToRotation(RDR2.Math.Vector3 fromDirection, RDR2.Math.Vector3 toDirection)`
- `public System.Int32 RDR2.Math.Quaternion.GetHashCode()`
- `public System.Void RDR2.Math.Quaternion.Invert()`
- `public static RDR2.Math.Quaternion RDR2.Math.Quaternion.Invert(RDR2.Math.Quaternion quaternion)`
- `public System.Single RDR2.Math.Quaternion.Length()`
- `public System.Single RDR2.Math.Quaternion.LengthSquared()`
- `public static RDR2.Math.Quaternion RDR2.Math.Quaternion.Lerp(RDR2.Math.Quaternion start, RDR2.Math.Quaternion end, System.Single amount)`
- `public static RDR2.Math.Quaternion RDR2.Math.Quaternion.Multiply(RDR2.Math.Quaternion left, RDR2.Math.Quaternion right)`
- `public static RDR2.Math.Quaternion RDR2.Math.Quaternion.Multiply(RDR2.Math.Quaternion quaternion, System.Single scale)`
- `public static RDR2.Math.Quaternion RDR2.Math.Quaternion.Negate(RDR2.Math.Quaternion quaternion)`
- `public System.Void RDR2.Math.Quaternion.Normalize()`
- `public static RDR2.Math.Quaternion RDR2.Math.Quaternion.Normalize(RDR2.Math.Quaternion quaternion)`
- `public static RDR2.Math.Quaternion RDR2.Math.Quaternion.RotateTowards(RDR2.Math.Quaternion from, RDR2.Math.Quaternion to, System.Single maxDegreesDelta)`
- `public static RDR2.Math.Quaternion RDR2.Math.Quaternion.RotationAxis(RDR2.Math.Vector3 axis, System.Single angle)`
- `public static RDR2.Math.Quaternion RDR2.Math.Quaternion.RotationMatrix(RDR2.Math.Matrix matrix)`
- `public static RDR2.Math.Quaternion RDR2.Math.Quaternion.RotationYawPitchRoll(System.Single yaw, System.Single pitch, System.Single roll)`
- `public static RDR2.Math.Quaternion RDR2.Math.Quaternion.Slerp(RDR2.Math.Quaternion start, RDR2.Math.Quaternion end, System.Single amount)`
- `public static RDR2.Math.Quaternion RDR2.Math.Quaternion.SlerpUnclamped(RDR2.Math.Quaternion a, RDR2.Math.Quaternion b, System.Single t)`
- `public static RDR2.Math.Quaternion RDR2.Math.Quaternion.Subtract(RDR2.Math.Quaternion left, RDR2.Math.Quaternion right)`
- `public System.String RDR2.Math.Quaternion.ToString()`
- `public System.String RDR2.Math.Quaternion.ToString(System.String format)`
- `public static RDR2.Math.Quaternion RDR2.Math.Quaternion.op_Addition(RDR2.Math.Quaternion left, RDR2.Math.Quaternion right)`
- `public static RDR2.Math.Quaternion RDR2.Math.Quaternion.op_Division(RDR2.Math.Quaternion left, RDR2.Math.Quaternion right)`
- `public static System.Boolean RDR2.Math.Quaternion.op_Equality(RDR2.Math.Quaternion left, RDR2.Math.Quaternion right)`
- `public static System.Boolean RDR2.Math.Quaternion.op_Inequality(RDR2.Math.Quaternion left, RDR2.Math.Quaternion right)`
- `public static RDR2.Math.Quaternion RDR2.Math.Quaternion.op_Multiply(RDR2.Math.Quaternion left, RDR2.Math.Quaternion right)`
- `public static RDR2.Math.Quaternion RDR2.Math.Quaternion.op_Multiply(RDR2.Math.Quaternion quaternion, System.Single scale)`
- `public static RDR2.Math.Quaternion RDR2.Math.Quaternion.op_Multiply(System.Single scale, RDR2.Math.Quaternion quaternion)`
- `public static RDR2.Math.Quaternion RDR2.Math.Quaternion.op_Subtraction(RDR2.Math.Quaternion left, RDR2.Math.Quaternion right)`
- `public static RDR2.Math.Quaternion RDR2.Math.Quaternion.op_UnaryNegation(RDR2.Math.Quaternion quaternion)`

## `RDR2.Math.Vector2`

- Kind: struct
- Base type: `System.ValueType`

- `public static RDR2.Math.Vector2 RDR2.Math.Vector2.Add(RDR2.Math.Vector2 left, RDR2.Math.Vector2 right)`
- `public static System.Single RDR2.Math.Vector2.Angle(RDR2.Math.Vector2 from, RDR2.Math.Vector2 to)`
- `public static RDR2.Math.Vector2 RDR2.Math.Vector2.Clamp(RDR2.Math.Vector2 value, RDR2.Math.Vector2 min, RDR2.Math.Vector2 max)`
- `public static System.Single RDR2.Math.Vector2.Distance(RDR2.Math.Vector2 position1, RDR2.Math.Vector2 position2)`
- `public static System.Single RDR2.Math.Vector2.DistanceSquared(RDR2.Math.Vector2 position1, RDR2.Math.Vector2 position2)`
- `public System.Single RDR2.Math.Vector2.DistanceTo(RDR2.Math.Vector2 position)`
- `public System.Single RDR2.Math.Vector2.DistanceToSquared(RDR2.Math.Vector2 position)`
- `public static RDR2.Math.Vector2 RDR2.Math.Vector2.Divide(RDR2.Math.Vector2 value, System.Single scale)`
- `public static System.Single RDR2.Math.Vector2.Dot(RDR2.Math.Vector2 left, RDR2.Math.Vector2 right)`
- `public System.Boolean RDR2.Math.Vector2.Equals(RDR2.Math.Vector2 other)`
- `public System.Boolean RDR2.Math.Vector2.Equals(System.Object obj)`
- `public static System.Boolean RDR2.Math.Vector2.Equals(ref RDR2.Math.Vector2 value1, ref RDR2.Math.Vector2 value2)`
- `public System.Int32 RDR2.Math.Vector2.GetHashCode()`
- `public System.Single RDR2.Math.Vector2.Length()`
- `public System.Single RDR2.Math.Vector2.LengthSquared()`
- `public static RDR2.Math.Vector2 RDR2.Math.Vector2.Lerp(RDR2.Math.Vector2 start, RDR2.Math.Vector2 end, System.Single amount)`
- `public static RDR2.Math.Vector2 RDR2.Math.Vector2.Maximize(RDR2.Math.Vector2 value1, RDR2.Math.Vector2 value2)`
- `public static RDR2.Math.Vector2 RDR2.Math.Vector2.Minimize(RDR2.Math.Vector2 value1, RDR2.Math.Vector2 value2)`
- `public static RDR2.Math.Vector2 RDR2.Math.Vector2.Modulate(RDR2.Math.Vector2 left, RDR2.Math.Vector2 right)`
- `public static RDR2.Math.Vector2 RDR2.Math.Vector2.Multiply(RDR2.Math.Vector2 value, System.Single scale)`
- `public static RDR2.Math.Vector2 RDR2.Math.Vector2.Negate(RDR2.Math.Vector2 value)`
- `public System.Void RDR2.Math.Vector2.Normalize()`
- `public static RDR2.Math.Vector2 RDR2.Math.Vector2.Normalize(RDR2.Math.Vector2 vector)`
- `public static RDR2.Math.Vector2 RDR2.Math.Vector2.RandomXY()`
- `public static RDR2.Math.Vector2 RDR2.Math.Vector2.Reflect(RDR2.Math.Vector2 vector, RDR2.Math.Vector2 normal)`
- `public static System.Single RDR2.Math.Vector2.SignedAngle(RDR2.Math.Vector2 from, RDR2.Math.Vector2 to)`
- `public static RDR2.Math.Vector2 RDR2.Math.Vector2.Subtract(RDR2.Math.Vector2 left, RDR2.Math.Vector2 right)`
- `public System.Single RDR2.Math.Vector2.ToHeading()`
- `public System.String RDR2.Math.Vector2.ToString()`
- `public System.String RDR2.Math.Vector2.ToString(System.String format)`
- `public static RDR2.Math.Vector2 RDR2.Math.Vector2.op_Addition(RDR2.Math.Vector2 left, RDR2.Math.Vector2 right)`
- `public static RDR2.Math.Vector2 RDR2.Math.Vector2.op_Division(RDR2.Math.Vector2 vector, System.Single scale)`
- `public static System.Boolean RDR2.Math.Vector2.op_Equality(RDR2.Math.Vector2 left, RDR2.Math.Vector2 right)`
- `public static RDR2.Math.Vector3 RDR2.Math.Vector2.op_Implicit(RDR2.Math.Vector2 vector)`
- `public static System.Boolean RDR2.Math.Vector2.op_Inequality(RDR2.Math.Vector2 left, RDR2.Math.Vector2 right)`
- `public static RDR2.Math.Vector2 RDR2.Math.Vector2.op_Multiply(RDR2.Math.Vector2 vector, System.Single scale)`
- `public static RDR2.Math.Vector2 RDR2.Math.Vector2.op_Multiply(System.Single scale, RDR2.Math.Vector2 vector)`
- `public static RDR2.Math.Vector2 RDR2.Math.Vector2.op_Subtraction(RDR2.Math.Vector2 left, RDR2.Math.Vector2 right)`
- `public static RDR2.Math.Vector2 RDR2.Math.Vector2.op_UnaryNegation(RDR2.Math.Vector2 value)`

## `RDR2.Math.Vector3`

- Kind: struct
- Base type: `System.ValueType`

- `public static RDR2.Math.Vector3 RDR2.Math.Vector3.Add(RDR2.Math.Vector3 left, RDR2.Math.Vector3 right)`
- `public static System.Single RDR2.Math.Vector3.Angle(RDR2.Math.Vector3 from, RDR2.Math.Vector3 to)`
- `public RDR2.Math.Vector3 RDR2.Math.Vector3.Around(System.Single distance)`
- `public static RDR2.Math.Vector3 RDR2.Math.Vector3.Clamp(RDR2.Math.Vector3 value, RDR2.Math.Vector3 min, RDR2.Math.Vector3 max)`
- `public static RDR2.Math.Vector3 RDR2.Math.Vector3.Cross(RDR2.Math.Vector3 left, RDR2.Math.Vector3 right)`
- `public static System.Single RDR2.Math.Vector3.Distance(RDR2.Math.Vector3 position1, RDR2.Math.Vector3 position2)`
- `public static System.Single RDR2.Math.Vector3.Distance2D(RDR2.Math.Vector3 position1, RDR2.Math.Vector3 position2)`
- `public static System.Single RDR2.Math.Vector3.DistanceSquared(RDR2.Math.Vector3 position1, RDR2.Math.Vector3 position2)`
- `public static System.Single RDR2.Math.Vector3.DistanceSquared2D(RDR2.Math.Vector3 position1, RDR2.Math.Vector3 position2)`
- `public System.Single RDR2.Math.Vector3.DistanceTo(RDR2.Math.Vector3 position)`
- `public System.Single RDR2.Math.Vector3.DistanceTo2D(RDR2.Math.Vector3 position)`
- `public System.Single RDR2.Math.Vector3.DistanceToSquared(RDR2.Math.Vector3 position)`
- `public System.Single RDR2.Math.Vector3.DistanceToSquared2D(RDR2.Math.Vector3 position)`
- `public static RDR2.Math.Vector3 RDR2.Math.Vector3.Divide(RDR2.Math.Vector3 value, System.Single scale)`
- `public static System.Single RDR2.Math.Vector3.Dot(RDR2.Math.Vector3 left, RDR2.Math.Vector3 right)`
- `public System.Boolean RDR2.Math.Vector3.Equals(RDR2.Math.Vector3 other)`
- `public System.Boolean RDR2.Math.Vector3.Equals(System.Object obj)`
- `public static System.Boolean RDR2.Math.Vector3.Equals(ref RDR2.Math.Vector3 value1, ref RDR2.Math.Vector3 value2)`
- `public System.Int32 RDR2.Math.Vector3.GetHashCode()`
- `public System.Single RDR2.Math.Vector3.Length()`
- `public System.Single RDR2.Math.Vector3.LengthSquared()`
- `public static RDR2.Math.Vector3 RDR2.Math.Vector3.Lerp(RDR2.Math.Vector3 start, RDR2.Math.Vector3 end, System.Single amount)`
- `public static RDR2.Math.Vector3 RDR2.Math.Vector3.Maximize(RDR2.Math.Vector3 value1, RDR2.Math.Vector3 value2)`
- `public static RDR2.Math.Vector3 RDR2.Math.Vector3.Minimize(RDR2.Math.Vector3 value1, RDR2.Math.Vector3 value2)`
- `public static RDR2.Math.Vector3 RDR2.Math.Vector3.Modulate(RDR2.Math.Vector3 left, RDR2.Math.Vector3 right)`
- `public static RDR2.Math.Vector3 RDR2.Math.Vector3.Multiply(RDR2.Math.Vector3 value, System.Single scale)`
- `public static RDR2.Math.Vector3 RDR2.Math.Vector3.Negate(RDR2.Math.Vector3 value)`
- `public System.Void RDR2.Math.Vector3.Normalize()`
- `public static RDR2.Math.Vector3 RDR2.Math.Vector3.Normalize(RDR2.Math.Vector3 vector)`
- `public static RDR2.Math.Vector3 RDR2.Math.Vector3.Project(RDR2.Math.Vector3 vector, RDR2.Math.Vector3 onNormal)`
- `public static RDR2.Math.Vector3 RDR2.Math.Vector3.ProjectOnPlane(RDR2.Math.Vector3 vector, RDR2.Math.Vector3 planeNormal)`
- `public static RDR2.Math.Vector3 RDR2.Math.Vector3.RandomXY()`
- `public static RDR2.Math.Vector3 RDR2.Math.Vector3.RandomXYZ()`
- `public static RDR2.Math.Vector3 RDR2.Math.Vector3.Reflect(RDR2.Math.Vector3 vector, RDR2.Math.Vector3 normal)`
- `public static System.Single RDR2.Math.Vector3.SignedAngle(RDR2.Math.Vector3 from, RDR2.Math.Vector3 to, RDR2.Math.Vector3 planeNormal)`
- `public static RDR2.Math.Vector3 RDR2.Math.Vector3.Subtract(RDR2.Math.Vector3 left, RDR2.Math.Vector3 right)`
- `public System.Single RDR2.Math.Vector3.ToHeading()`
- `public System.String RDR2.Math.Vector3.ToString()`
- `public static RDR2.Math.Vector3 RDR2.Math.Vector3.op_Addition(RDR2.Math.Vector3 left, RDR2.Math.Vector3 right)`
- `public static RDR2.Math.Vector3 RDR2.Math.Vector3.op_Division(RDR2.Math.Vector3 vector, System.Single scale)`
- `public static System.Boolean RDR2.Math.Vector3.op_Equality(RDR2.Math.Vector3 left, RDR2.Math.Vector3 right)`
- `public static RDR2.Math.Vector2 RDR2.Math.Vector3.op_Implicit(RDR2.Math.Vector3 vector)`
- `public static System.Boolean RDR2.Math.Vector3.op_Inequality(RDR2.Math.Vector3 left, RDR2.Math.Vector3 right)`
- `public static RDR2.Math.Vector3 RDR2.Math.Vector3.op_Multiply(RDR2.Math.Vector3 vector, System.Single scale)`
- `public static RDR2.Math.Vector3 RDR2.Math.Vector3.op_Multiply(System.Single scale, RDR2.Math.Vector3 vector)`
- `public static RDR2.Math.Vector3 RDR2.Math.Vector3.op_Subtraction(RDR2.Math.Vector3 left, RDR2.Math.Vector3 right)`
- `public static RDR2.Math.Vector3 RDR2.Math.Vector3.op_UnaryNegation(RDR2.Math.Vector3 vector)`

## `RDR2.Model`

- Kind: struct
- Base type: `System.ValueType`

- `public System.Boolean RDR2.Model.Equals(RDR2.Model obj)`
- `public System.Boolean RDR2.Model.Equals(System.Object obj)`
- `public System.Int32 RDR2.Model.GetHashCode()`
- `public System.Boolean RDR2.Model.Request(System.Int32 timeout)`
- `public System.Void RDR2.Model.Request()`
- `public System.String RDR2.Model.ToString()`
- `public static System.Boolean RDR2.Model.op_Equality(RDR2.Model left, RDR2.Model right)`
- `public static RDR2.Model RDR2.Model.op_Implicit(RDR2.PedHash source)`
- `public static RDR2.Model RDR2.Model.op_Implicit(System.Int32 source)`
- `public static RDR2.Model RDR2.Model.op_Implicit(System.String source)`
- `public static RDR2.Model RDR2.Model.op_Implicit(System.UInt32 source)`
- `public static RDR2.PedHash RDR2.Model.op_Implicit(RDR2.Model source)`
- `public static System.Int32 RDR2.Model.op_Implicit(RDR2.Model source)`
- `public static System.UInt32 RDR2.Model.op_Implicit(RDR2.Model source)`
- `public static System.Boolean RDR2.Model.op_Inequality(RDR2.Model left, RDR2.Model right)`

## `RDR2.Native.AICOVERPOINT`

- Kind: class
- Base type: `System.Object`

- `public static System.Void RDR2.Native.AICOVERPOINT.TASK_ENTER_COVER(System.Int32 ped)`
- `public static System.Void RDR2.Native.AICOVERPOINT.TASK_EXIT_COVER(System.Int32 ped)`
- `public static System.Void RDR2.Native.AICOVERPOINT._0x140B3CB1D424A945(System.Int32 ped, System.UInt32 weaponHash)`
- `public static System.Void RDR2.Native.AICOVERPOINT._0x3C7A9C2C953128FE(System.Int32 ped)`
- `public static System.Int32 RDR2.Native.AICOVERPOINT._0x53E4D0C079CA6855(System.Int32 handle)`
- `public static System.Int32 RDR2.Native.AICOVERPOINT._0x5F5B1B7E8E8F94C6(System.Int32 ped)`
- `public static System.Void RDR2.Native.AICOVERPOINT._0x64340DC208D671D5(System.String coverLayer)`
- `public static System.Void RDR2.Native.AICOVERPOINT._0x733077295AB51304(System.UInt64* args)`
- `public static System.Void RDR2.Native.AICOVERPOINT._0x7A1FDCF35EAA140F(System.String coverLayer)`
- `public static System.Int32 RDR2.Native.AICOVERPOINT._0x957D7E750216D74B(System.Int32 ped)`
- `public static System.Void RDR2.Native.AICOVERPOINT._0xEBA51A294C73292E(System.UInt64* args)`
- `public static System.Int32 RDR2.Native.AICOVERPOINT._ADD_SCRIPTED_COVER_POINT(System.UInt64* data)`
- `public static System.Boolean RDR2.Native.AICOVERPOINT._ARE_LOAD_COVER_ANIMS_LOADED(System.Int32 ped)`
- `public static System.Boolean RDR2.Native.AICOVERPOINT._DOES_COVER_POINT_EXIST(System.Int32 handle)`
- `public static System.Void RDR2.Native.AICOVERPOINT._REQUEST_FLINCH_COVER_ANIM(System.Int32 ped)`
- `public static System.Void RDR2.Native.AICOVERPOINT._STOP_RUNNING_COVER_ANIMS(System.Int32 ped)`
- `public static System.Void RDR2.Native.AICOVERPOINT._TASK_AI_SEEK_COVER_TO_COVER_POINT(System.UInt64* args)`

## `RDR2.Native.AITRANSPORT`

- Kind: class
- Base type: `System.Object`

- `public static System.Boolean RDR2.Native.AITRANSPORT.GET_TRANSPORT_CONFIG_FLAG(System.Int32 transportEntity, System.Int32 flagId, System.Boolean p2)`
- `public static System.Boolean RDR2.Native.AITRANSPORT.IS_PED_ENTERING_TRANSPORT(System.Int32 ped, System.Int32 transportEntity, System.Boolean p2)`
- `public static System.Boolean RDR2.Native.AITRANSPORT.IS_PED_EXITING_TRANSPORT(System.Int32 ped, System.Int32 transportEntity)`
- `public static System.Void RDR2.Native.AITRANSPORT.SET_PED_OFF_TRANSPORT_SEAT(System.Int32 ped, System.Int32 flags)`
- `public static System.Void RDR2.Native.AITRANSPORT.SET_PED_ON_TRANSPORT_SEAT(System.Int32 ped, System.Int32 transportEntity, System.Int32 seat, System.Int32 flags)`
- `public static System.Void RDR2.Native.AITRANSPORT.SET_TRANSPORT_ACCESSIBLE_SEAT_FLAGS(System.Int32 transportEntity, System.Int32 flags)`
- `public static System.Void RDR2.Native.AITRANSPORT.SET_TRANSPORT_CONFIG_FLAG(System.Int32 transportEntity, System.Int32 flagId, System.Boolean value)`
- `public static System.Void RDR2.Native.AITRANSPORT.TASK_ENTER_TRANSPORT(System.UInt64* args)`
- `public static System.Void RDR2.Native.AITRANSPORT.TASK_EXIT_TRANSPORT(System.UInt64* args)`
- `public static System.Void RDR2.Native.AITRANSPORT._0x13F138225C202F66(System.Int32 transportEntity, System.Int32 p1)`
- `public static System.Void RDR2.Native.AITRANSPORT._0x4248AB2EEB3C75AD(System.Int32 transportEntity, System.Int32 ped, System.Boolean p2)`
- `public static System.Void RDR2.Native.AITRANSPORT._0x4B6C9A43F7D9109B(System.UInt64 p0, System.UInt64 p1)`
- `public static System.Void RDR2.Native.AITRANSPORT._0x5639FBEA922788DA(System.Int32 transportEntity)`
- `public static System.Void RDR2.Native.AITRANSPORT._0x8C8371EDFAF014A0(System.Int32 ped, System.Single p1)`
- `public static System.Void RDR2.Native.AITRANSPORT._0xF8C20282B237E3F7(System.Int32 ped)`
- `public static System.Int32 RDR2.Native.AITRANSPORT._GET_PED_IN_TRANSPORT_SEAT(System.Int32 transportEntity, System.Int32 seatIndex)`
- `public static System.UInt64 RDR2.Native.AITRANSPORT._GET_TRANSPORT_USAGE_FLAGS(System.Int32 transportEntity, System.Int32* flags)`
- `public static System.Boolean RDR2.Native.AITRANSPORT._IS_PED_ON_TRANSPORT_ENTITY(System.Int32 ped, System.Int32 transportEntity)`
- `public static System.Boolean RDR2.Native.AITRANSPORT._IS_PED_ON_TRANSPORT_SEAT(System.Int32 ped, System.Boolean p1)`
- `public static System.Boolean RDR2.Native.AITRANSPORT._IS_TRANSPORT_SEAT_FREE(System.Int32 transportEntity, System.Int32 seatIndex)`
- `public static System.Boolean RDR2.Native.AITRANSPORT._IS_TRANSPORT_SEAT_OCCUPIED(System.Int32 transportEntity, System.Int32 seatIndex)`
- `public static System.Void RDR2.Native.AITRANSPORT._SET_AI_CAN_USE_TRANSPORT(System.Int32 transportEntity, System.Boolean state)`
- `public static System.Void RDR2.Native.AITRANSPORT._SET_PED_USE_TRANSPORT_SEAT_PREFERENCE(System.Int32 ped, System.Int32 transportEntity, System.Int32 preferenceSlot, System.Int32 p3, System.Int32 seatIndex)`
- `public static System.Void RDR2.Native.AITRANSPORT._SET_TRANSPORT_USAGE_FLAGS(System.Int32 transportEntity, System.Int32 flags)`

## `RDR2.Native.ANIMSCENE`

- Kind: class
- Base type: `System.Object`

- `public static System.Void RDR2.Native.ANIMSCENE.ABORT_ANIM_SCENE(System.Int32 animScene, System.Boolean p1)`
- `public static System.Void RDR2.Native.ANIMSCENE.ATTACH_ANIM_SCENE_TO_ENTITY(System.Int32 animScene, System.Int32 entity, System.Int32 p2)`
- `public static System.Void RDR2.Native.ANIMSCENE.ATTACH_ANIM_SCENE_TO_ENTITY_PRESERVING_LOCATION(System.Int32 animScene, System.Int32 entity, System.Int32 p2)`
- `public static System.Void RDR2.Native.ANIMSCENE.BLOCK_ANIM_SCENE_FADING_NEXT_FRAME(System.Boolean p0, System.Boolean p1)`
- `public static System.Boolean RDR2.Native.ANIMSCENE.CHECK_OWNERSHIP_OF_ANIM_SCENE(System.Int32 animScene)`
- `public static System.Boolean RDR2.Native.ANIMSCENE.COULD_ANIM_SCENE_ENTITY_REACH_EXIT_NEXT_FRAME(System.Int32 animScene, System.String entityName, System.UInt64 p2, System.UInt64 p3)`
- `public static System.Void RDR2.Native.ANIMSCENE.DETACH_ANIM_SCENE(System.Int32 animScene)`
- `public static System.Void RDR2.Native.ANIMSCENE.DETACH_ANIM_SCENE_PRESERVING_LOCATION(System.Int32 animScene)`
- `public static System.Boolean RDR2.Native.ANIMSCENE.DOES_ANIM_SCENE_EXIST(System.Int32 animScene)`
- `public static System.Void RDR2.Native.ANIMSCENE.FADE_ANIM_SCENE_AUDIO_IN(System.Int32 animScene, System.Single p1)`
- `public static System.Void RDR2.Native.ANIMSCENE.FADE_ANIM_SCENE_AUDIO_OUT(System.Int32 animScene, System.Single p1)`
- `public static System.Boolean RDR2.Native.ANIMSCENE.GET_ANIM_SCENE_BOOL(System.Int32 animScene, System.String name)`
- `public static System.Int32 RDR2.Native.ANIMSCENE.GET_ANIM_SCENE_CURRENT_ACTIVE_CAMERA_COUNT(System.Int32 animScene)`
- `public static System.Boolean RDR2.Native.ANIMSCENE.GET_ANIM_SCENE_ENTITY_LOCATION_DATA(System.Int32 animScene, System.String entityName, RDR2.Math.Vector3* matrix, System.Boolean p3, System.String playbackListName, System.Int32 p5)`
- `public static System.Single RDR2.Native.ANIMSCENE.GET_ANIM_SCENE_FLOAT(System.Int32 animScene, System.String name)`
- `public static System.Int32 RDR2.Native.ANIMSCENE.GET_ANIM_SCENE_INT(System.Int32 animScene, System.String name)`
- `public static System.Void RDR2.Native.ANIMSCENE.GET_ANIM_SCENE_ORIGIN(System.Int32 animScene, RDR2.Math.Vector3* position, RDR2.Math.Vector3* rotation, System.Int32 order)`
- `public static System.Single RDR2.Native.ANIMSCENE.GET_ANIM_SCENE_PHASE(System.Int32 animScene)`
- `public static System.Boolean RDR2.Native.ANIMSCENE.HAS_ANIM_SCENE_EXITED(System.Int32 animScene, System.Boolean p1)`
- `public static System.Boolean RDR2.Native.ANIMSCENE.HAS_ENTITY_EXITED_ANIM_SCENE(System.Int32 animScene, System.String entityName)`
- `public static System.Boolean RDR2.Native.ANIMSCENE.IS_ANIM_SCENE_EXITING_THIS_FRAME(System.Int32 animScene)`
- `public static System.Boolean RDR2.Native.ANIMSCENE.IS_ANIM_SCENE_FINISHED(System.Int32 animScene, System.Boolean p1)`
- `public static System.Boolean RDR2.Native.ANIMSCENE.IS_ANIM_SCENE_IN_SECTION(System.Int32 animScene, System.String sectionName, System.Boolean p2)`
- `public static System.Boolean RDR2.Native.ANIMSCENE.IS_ANIM_SCENE_LOADED(System.Int32 animScene, System.Boolean p1, System.Boolean p2)`
- `public static System.Boolean RDR2.Native.ANIMSCENE.IS_ANIM_SCENE_METADATA_LOADED(System.Int32 animScene, System.Boolean p1)`
- `public static System.Boolean RDR2.Native.ANIMSCENE.IS_ANIM_SCENE_RUNNING(System.Int32 animScene, System.Boolean p1)`
- `public static System.Boolean RDR2.Native.ANIMSCENE.IS_ENTITY_EXITING_ANIM_SCENE_THIS_FRAME(System.Int32 animScene, System.String entityName)`
- `public static System.Boolean RDR2.Native.ANIMSCENE.IS_ENTITY_PLAYING_ANIM_SCENE(System.Int32 entity, System.Int32 animScene)`
- `public static System.Void RDR2.Native.ANIMSCENE.LOAD_ANIM_SCENE(System.Int32 animScene)`
- `public static System.Void RDR2.Native.ANIMSCENE.REMOVE_ANIM_SCENE_ENTITY(System.Int32 animScene, System.String entityName, System.Int32 entity)`
- `public static System.Boolean RDR2.Native.ANIMSCENE.REQUEST_ANIM_SCENE_PLAY_LIST(System.Int32 animScene, System.String playlistName)`
- `public static System.Void RDR2.Native.ANIMSCENE.RESET_ANIM_SCENE(System.Int32 animScene, System.String playbackListName)`
- `public static System.Void RDR2.Native.ANIMSCENE.RESUME_ANIM_SCENE_FROM_LAST_CHECKPOINT(System.Int32 animScene)`
- `public static System.Void RDR2.Native.ANIMSCENE.SET_ANIM_SCENE_BOOL(System.Int32 animScene, System.String name, System.Boolean value, System.Boolean p3)`
- `public static System.Void RDR2.Native.ANIMSCENE.SET_ANIM_SCENE_ENTITY(System.Int32 animScene, System.String entityName, System.Int32 entity, System.Int32 flags)`
- `public static System.Void RDR2.Native.ANIMSCENE.SET_ANIM_SCENE_FLOAT(System.Int32 animScene, System.String name, System.Single value, System.Boolean p3, System.Boolean p4)`
- `public static System.Void RDR2.Native.ANIMSCENE.SET_ANIM_SCENE_INT(System.Int32 animScene, System.String name, System.Int32 value, System.Boolean p3)`
- `public static System.Void RDR2.Native.ANIMSCENE.SET_ANIM_SCENE_ORIGIN(System.Int32 animScene, RDR2.Math.Vector3 vec, RDR2.Math.Vector3 rotation, System.Int32 order)`
- `public static System.Void RDR2.Native.ANIMSCENE.SET_ANIM_SCENE_ORIGIN(System.Int32 animScene, System.Single posX, System.Single posY, System.Single posZ, System.Single rotX, System.Single rotY, System.Single rotZ, System.Int32 order)`
- `public static System.Void RDR2.Native.ANIMSCENE.SET_ANIM_SCENE_PAUSED(System.Int32 animScene, System.Boolean toggle)`
- `public static System.Void RDR2.Native.ANIMSCENE.SET_ANIM_SCENE_PLAYBACK_LIST(System.Int32 animScene, System.String playbackListName)`
- `public static System.Void RDR2.Native.ANIMSCENE.SET_ANIM_SCENE_PLAY_LIST(System.Int32 animScene, System.String playlistName, System.Boolean p2)`
- `public static System.Void RDR2.Native.ANIMSCENE.SET_ANIM_SCENE_RATE(System.Int32 animScene, System.Single rate)`
- `public static System.Void RDR2.Native.ANIMSCENE.START_ANIM_SCENE(System.Int32 animScene)`
- `public static System.Void RDR2.Native.ANIMSCENE.TAKE_OWNERSHIP_OF_ANIM_SCENE(System.Int32 animScene)`
- `public static System.Void RDR2.Native.ANIMSCENE.TRIGGER_ANIM_SCENE_SKIP(System.Int32 animScene)`
- `public static System.Boolean RDR2.Native.ANIMSCENE.WAS_ANIM_SCENE_SKIPPED(System.Int32 animScene)`
- `public static System.Boolean RDR2.Native.ANIMSCENE._0x1407F5115FB9583E(System.Int32 animScene, System.String p1)`
- `public static System.Void RDR2.Native.ANIMSCENE._0x1AD896BF43619551()`
- `public static System.Boolean RDR2.Native.ANIMSCENE._0x1C5D33A4293E6DDE(System.Int32 animScene, System.String phaseName)`
- `public static System.Boolean RDR2.Native.ANIMSCENE._0x2DB524750DC41ED4()`
- `public static System.Void RDR2.Native.ANIMSCENE._0x3641FCD53E59B335(System.Int32 mgmHandle, System.Int32 ped, System.String secondaryVoiceString)`
- `public static System.Boolean RDR2.Native.ANIMSCENE._0x3B393716C3FD8237(System.Int32 ped)`
- `public static System.Boolean RDR2.Native.ANIMSCENE._0x4B85B3CF91972222(System.Int32 animScene)`
- `public static System.Boolean RDR2.Native.ANIMSCENE._0x5D7BFDA2290B4E39(System.String p0)`
- `public static System.Boolean RDR2.Native.ANIMSCENE._0x61B2AAEF645DDAF0(System.Int32 mgmEventHandle, System.String p1, System.Int32 seatId, System.Int32 p3, System.Boolean p4)`
- `public static System.Boolean RDR2.Native.ANIMSCENE._0x73616E64696C132E(System.Int32 animScene, System.Boolean p1)`
- `public static System.Boolean RDR2.Native.ANIMSCENE._0x9AAE3C1148A09BCA(System.Int32 animScene)`
- `public static System.Boolean RDR2.Native.ANIMSCENE._0xA96619FE85159ED2(System.Int32 animScene)`
- `public static System.Void RDR2.Native.ANIMSCENE._0xAE6DE22DE0ED4554(System.Int32 mgmHandle, System.Int32 ped)`
- `public static System.Void RDR2.Native.ANIMSCENE._0xB1A196BAFE650402(System.Int32 mgmHandle, System.Int32 ped)`
- `public static System.Void RDR2.Native.ANIMSCENE._0xC1193521E3B9FADD(System.Int32 entity, System.Boolean p1)`
- `public static System.Void RDR2.Native.ANIMSCENE._0xCDCD7B2D49AEE73A(System.Boolean p0)`
- `public static System.Boolean RDR2.Native.ANIMSCENE._0xD70C7A30412F8FA0(System.Int32 animScene)`
- `public static System.Void RDR2.Native.ANIMSCENE._0xE12D7B4B959644CD()`
- `public static System.Boolean RDR2.Native.ANIMSCENE._0xEA41D44A8D42057B()`
- `public static System.Void RDR2.Native.ANIMSCENE._CLEAR_ANIM_SCENE_WAS_SKIPPED(System.Int32 animScene)`
- `public static System.Void RDR2.Native.ANIMSCENE._CLEAR_BREAKOUT_ARCHETYPE(System.Int32 ped)`
- `public static System.Int32 RDR2.Native.ANIMSCENE._CREATE_ANIM_SCENE(System.String animDict, System.Int32 flags, System.String playbackListName, System.Boolean p3, System.Boolean p4)`
- `public static System.Int32 RDR2.Native.ANIMSCENE._CREATE_MGM_SYSTEM(System.String mgmFilename)`
- `public static System.Void RDR2.Native.ANIMSCENE._DELETE_ANIM_SCENE(System.Int32 animScene)`
- `public static System.Void RDR2.Native.ANIMSCENE._DELETE_MGM_SYSTEM(System.Int32 mgmHandle)`
- `public static System.Boolean RDR2.Native.ANIMSCENE._DOES_ANIM_SCENE_OWNERSHIP_OF_ENTITY_EXIST(System.Int32 animScene, System.String entityName)`
- `public static System.Boolean RDR2.Native.ANIMSCENE._DOES_ANIM_SCENE_PLAY_LIST_EXIST(System.Int32 animScene, System.String playbackListName)`
- `public static System.Boolean RDR2.Native.ANIMSCENE._DOES_ENTITY_WITH_ID_EXIST_IN_ANIM_SCENE(System.Int32 animScene, System.String entityId)`
- `public static System.UInt32 RDR2.Native.ANIMSCENE._GET_ANIM_SCENE_DICT(System.Int32 animScene)`
- `public static System.Single RDR2.Native.ANIMSCENE._GET_ANIM_SCENE_DURATION(System.Int32 animScene)`
- `public static System.Int32 RDR2.Native.ANIMSCENE._GET_ANIM_SCENE_OBJECT(System.Int32 animScene, System.String name, System.Boolean isNetwork)`
- `public static System.Int32 RDR2.Native.ANIMSCENE._GET_ANIM_SCENE_PED(System.Int32 animScene, System.String name, System.Boolean isNetwork)`
- `public static System.Int32 RDR2.Native.ANIMSCENE._GET_ANIM_SCENE_PLAYBACK_LIST_PHASE_AUDIO_LOAD_STRESS(System.Int32 animScene, System.String phaseName)`
- `public static System.Single RDR2.Native.ANIMSCENE._GET_ANIM_SCENE_RATE(System.Int32 animScene)`
- `public static System.Single RDR2.Native.ANIMSCENE._GET_ANIM_SCENE_TIME(System.Int32 animScene)`
- `public static System.Int32 RDR2.Native.ANIMSCENE._GET_ANIM_SCENE_VEHICLE(System.Int32 animScene, System.String name, System.Boolean isNetwork)`
- `public static System.Boolean RDR2.Native.ANIMSCENE._HAS_ENTITY_ENTERED_ANIM_SCENE(System.Int32 animScene, System.String entityName)`
- `public static System.Boolean RDR2.Native.ANIMSCENE._IS_ANIM_SCENE_ABORTED(System.Int32 animScene)`
- `public static System.Boolean RDR2.Native.ANIMSCENE._IS_ANIM_SCENE_LOADING(System.Int32 animScene, System.Boolean p1)`
- `public static System.Boolean RDR2.Native.ANIMSCENE._IS_ANIM_SCENE_METADATA_ASSET_IN_RANGE_LOADING(System.Int32 animScene, System.Boolean p1)`
- `public static System.Boolean RDR2.Native.ANIMSCENE._IS_ANIM_SCENE_PAUSED(System.Int32 animScene)`
- `public static System.Boolean RDR2.Native.ANIMSCENE._IS_ANIM_SCENE_PLAYBACK_LIST_PHASE_ACTIVE(System.Int32 animScene, System.String phaseName)`
- `public static System.Boolean RDR2.Native.ANIMSCENE._IS_ANIM_SCENE_PLAYBACK_LIST_PHASE_LOADED(System.Int32 animScene, System.String phaseName)`
- `public static System.Boolean RDR2.Native.ANIMSCENE._IS_ANIM_SCENE_PLAYBACK_LIST_PHASE_LOADING(System.Int32 animScene, System.String phaseName)`
- `public static System.Boolean RDR2.Native.ANIMSCENE._IS_ANIM_SCENE_SKIPPABLE(System.Int32 animScene)`
- `public static System.Boolean RDR2.Native.ANIMSCENE._IS_MGM_SYSTEM_LOADED(System.String mgmFilename)`
- `public static System.Boolean RDR2.Native.ANIMSCENE._LOAD_MGM_ASSETS(System.String mgmFilename)`
- `public static System.Void RDR2.Native.ANIMSCENE._PAUSE_SCRIPT_THREADS(System.Boolean toggle)`
- `public static System.Boolean RDR2.Native.ANIMSCENE._RELEASE_ANIM_SCENE_PLAY_LIST(System.Int32 animScene, System.String playlistName)`
- `public static System.Void RDR2.Native.ANIMSCENE._REQUEST_PHOTO_MODE_DEFREEZE()`
- `public static System.Void RDR2.Native.ANIMSCENE._REQUEST_PHOTO_MODE_FREEZE()`
- `public static System.Void RDR2.Native.ANIMSCENE._SET_BREAKOUT_ARCHETYPE(System.Int32 ped, System.String archetype)`
- `public static System.Void RDR2.Native.ANIMSCENE._SET_MGM_EVENT(System.Int32 mgmEventHandle, System.String p1, System.UInt64 seatId, System.Int32 p3, System.Single p4)`

## `RDR2.Native.ATTRIBUTE`

- Kind: class
- Base type: `System.Object`

- `public static System.Void RDR2.Native.ATTRIBUTE.ADD_ATTRIBUTE_POINTS(System.Int32 ped, System.Int32 attributeIndex, System.Int32 p2)`
- `public static System.Void RDR2.Native.ATTRIBUTE.DISABLE_ATTRIBUTE_OVERPOWER(System.Int32 ped, System.Int32 attributeIndex)`
- `public static System.Void RDR2.Native.ATTRIBUTE.ENABLE_ATTRIBUTE_OVERPOWER(System.Int32 ped, System.Int32 attributeIndex, System.Single value, System.Boolean makeSound)`
- `public static System.Int32 RDR2.Native.ATTRIBUTE.GET_ATTRIBUTE_BASE_RANK(System.Int32 ped, System.Int32 attributeIndex)`
- `public static System.Int32 RDR2.Native.ATTRIBUTE.GET_ATTRIBUTE_BONUS_RANK(System.Int32 ped, System.Int32 coreIndex)`
- `public static System.Int32 RDR2.Native.ATTRIBUTE.GET_ATTRIBUTE_POINTS(System.Int32 ped, System.Int32 attributeIndex)`
- `public static System.Int32 RDR2.Native.ATTRIBUTE.GET_ATTRIBUTE_RANK(System.Int32 ped, System.Int32 attributeIndex)`
- `public static System.Int32 RDR2.Native.ATTRIBUTE.GET_DEFAULT_ATTRIBUTE_POINTS_NEEDED_FOR_RANK(System.UInt32 modelHash, System.Int32 attributeIndex, System.Int32 rank)`
- `public static System.Int32 RDR2.Native.ATTRIBUTE.GET_DEFAULT_ATTRIBUTE_RANK(System.Int32 ped, System.Int32 attributeIndex)`
- `public static System.Int32 RDR2.Native.ATTRIBUTE.GET_DEFAULT_MAX_ATTRIBUTE_RANK(System.Int32 ped, System.Int32 attributeIndex)`
- `public static System.Int32 RDR2.Native.ATTRIBUTE.GET_MAX_ATTRIBUTE_POINTS(System.Int32 ped, System.Int32 attributeIndex)`
- `public static System.Int32 RDR2.Native.ATTRIBUTE.GET_MAX_ATTRIBUTE_RANK(System.Int32 ped, System.Int32 attributeIndex)`
- `public static System.Void RDR2.Native.ATTRIBUTE.SET_ATTRIBUTE_BASE_RANK(System.Int32 ped, System.Int32 attributeIndex, System.Int32 newValue)`
- `public static System.Void RDR2.Native.ATTRIBUTE.SET_ATTRIBUTE_BONUS_RANK(System.Int32 ped, System.Int32 attributeIndex, System.Int32 newValue)`
- `public static System.Void RDR2.Native.ATTRIBUTE.SET_ATTRIBUTE_POINTS(System.Int32 ped, System.Int32 attributeIndex, System.Int32 p2)`
- `public static System.Void RDR2.Native.ATTRIBUTE.STOP_ITEM_PREVIEW()`
- `public static System.Void RDR2.Native.ATTRIBUTE._ENABLE_ATTRIBUTE_CORE_OVERPOWER(System.Int32 ped, System.Int32 coreIndex, System.Single value, System.Boolean makeSound)`
- `public static System.Single RDR2.Native.ATTRIBUTE._GET_ATTRIBUTE_CORE_OVERPOWER_SECONDS_LEFT(System.Int32 ped, System.Int32 coreIndex)`
- `public static System.Int32 RDR2.Native.ATTRIBUTE._GET_ATTRIBUTE_CORE_VALUE(System.Int32 ped, System.Int32 coreIndex)`
- `public static System.Single RDR2.Native.ATTRIBUTE._GET_ATTRIBUTE_OVERPOWER_SECONDS_LEFT(System.Int32 ped, System.Int32 attributeIndex)`
- `public static System.Boolean RDR2.Native.ATTRIBUTE._IS_ATTRIBUTE_CORE_OVERPOWERED(System.Int32 ped, System.Int32 coreIndex)`
- `public static System.Boolean RDR2.Native.ATTRIBUTE._IS_ATTRIBUTE_OVERPOWERED(System.Int32 ped, System.Int32 attributeIndex)`
- `public static System.Void RDR2.Native.ATTRIBUTE._SET_ATTRIBUTE_CORE_VALUE(System.Int32 ped, System.Int32 coreIndex, System.Int32 value)`
- `public static System.Void RDR2.Native.ATTRIBUTE._SET_STATUS_EFFECT_CORE_ICON(System.Int32 statusEffectType)`
- `public static System.Void RDR2.Native.ATTRIBUTE._SET_STATUS_EFFECT_PERIODIC_ICON(System.Int32 statusEffectType)`
- `public static System.Void RDR2.Native.ATTRIBUTE._START_ITEM_PREVIEW(System.UInt64 p0, System.Int32 p1)`
- `public static System.Void RDR2.Native.ATTRIBUTE._STOP_STATUS_EFFECT_PERIODIC_ICON(System.Int32 statusEffectType)`

