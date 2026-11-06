<!-- ScriptHookRDRNetAPI lines 23751-24000 of 30005 -->
```csharp
            Additional stability for hands and neck (less loose).
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.Melee">
            <summary>
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.FallingReaction">
            <summary>
            0=Rollup, 1=Catchfall, 2=rollDownStairs, 3=smartFall.
            </summary>
            <remarks>
            Default value = 0.
            Min value = 0.
            Max value = 3.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.UseExtendedCatchFall">
            <summary>
            Keep the character active instead of relaxing at the end of the catch fall.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.InitialWeaknessZeroDuration">
            <summary>
            Duration for which the character's upper body stays at minimum stiffness (not quite zero).
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.InitialWeaknessRampDuration">
            <summary>
            Duration of the ramp to bring the character's upper body stiffness back to normal levels.
            </summary>
            <remarks>
            Default value = 0.4f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.InitialNeckDuration">
            <summary>
            Duration for which the neck stays at intial stiffness/damping.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.InitialNeckRampDuration">
            <summary>
            Duration of the ramp to bring the neck stiffness/damping back to normal levels.
            </summary>
            <remarks>
            Default value = 0.4f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.UseCStrModulation">
            <summary>
            If enabled upper and lower body strength scales with character strength, using the range given by parameters below.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.CStrUpperMin">
            <summary>
            Proportions to what the strength would be normally.
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.1f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.CStrUpperMax">
            <summary>
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.1f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.CStrLowerMin">
            <summary>
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.1f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.CStrLowerMax">
            <summary>
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.1f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.DeathTime">
            <summary>
            Time to death (HACK for underwater). If -ve don't ever die.
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.0f.
            Max value = 1000.0f.
            </remarks>
        </member>
        <member name="T:RDR2.NaturalMotion.ShotNewBulletHelper">
            <summary>
            Send new wound information to the shot.  Can cause shot to restart it's performance in part or in whole.
            </summary>
        </member>
        <member name="M:RDR2.NaturalMotion.ShotNewBulletHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the ShotNewBulletHelper for sending a ShotNewBullet <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the ShotNewBullet <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
            <remarks>
            Send new wound information to the shot.  Can cause shot to restart it's performance in part or in whole.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotNewBulletHelper.BodyPart">
            <summary>
            Part ID on the body where the bullet hit.
            </summary>
            <remarks>
            Default value = 0.
            Min value = 0.
            Max value = 21.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotNewBulletHelper.LocalHitPointInfo">
            <summary>
            If true then normal and hitPoint should be supplied in local coordinates of bodyPart.  If false then normal and hitPoint should be supplied in World coordinates.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotNewBulletHelper.Normal">
            <summary>
            Normal coming out of impact point on character.  Can be local or global depending on localHitPointInfo.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, -1.0f).
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotNewBulletHelper.HitPoint">
            <summary>
            Position of impact on character. Can be local or global depending on localHitPointInfo.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotNewBulletHelper.BulletVel">
            <summary>
            Bullet velocity in world coordinates.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            Min value = -2000.0f.
            Max value = 2000.0f.
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.ShotSnapHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the ShotSnapHelper for sending a ShotSnap <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the ShotSnap <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotSnapHelper.Snap">
            <summary>
            Add a Snap to shot.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotSnapHelper.SnapMag">
            <summary>
            The magnitude of the reaction.
            </summary>
            <remarks>
            Default value = 0.4f.
            Min value = -10.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotSnapHelper.SnapMovingMult">
            <summary>
            MovingMult*snapMag = The magnitude of the reaction if moving(comVelMag) faster than movingThresh.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 20.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotSnapHelper.SnapBalancingMult">
            <summary>
            BalancingMult*snapMag = The magnitude of the reaction if balancing = (not lying on the floor/ not upper body not collided) and not airborne.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 20.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotSnapHelper.SnapAirborneMult">
            <summary>
            AirborneMult*snapMag = The magnitude of the reaction if airborne.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 20.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotSnapHelper.SnapMovingThresh">
            <summary>
            If moving(comVelMag) faster than movingThresh then mvingMult applied to stunMag.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 20.0f.
            </remarks>
        </member>
```
