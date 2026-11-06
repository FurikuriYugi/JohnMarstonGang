<!-- ScriptHookRDRNetAPI lines 23501-23750 of 30005 -->
```csharp
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.SpineBlendZero">
            <summary>
            Spine is always blended with zero pose this much and up to 1 as the character become stationary.  If negative no blend is ever applied.
            </summary>
            <remarks>
            Default value = 0.6f.
            Min value = -0.1f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.BulletProofVest">
            <summary>
            Looseness applied to spine is different if bulletProofVest is true.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.AlwaysResetLooseness">
            <summary>
            Looseness always reset on shotNewBullet even if previous looseness ramp still running.  Except for the neck which has it's own ramp.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.AlwaysResetNeckLooseness">
            <summary>
            Neck looseness always reset on shotNewBullet even if previous looseness ramp still running.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.AngVelScale">
            <summary>
            How much to scale the angular velocity coming in from animation of a part if it is in angVelScaleMask (otherwise scale by 1.0).
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.AngVelScaleMask">
            <summary>
            Parts to scale the initial angular velocity by angVelScale (otherwize scale by 1.0).
            </summary>
            <remarks>
            Default value = "fb".
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.FlingWidth">
            <summary>
            Width of the fling behavior.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.FlingTime">
            <summary>
            Duration of the fling behavior.
            </summary>
            <remarks>
            Default value = 0.6f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.TimeBeforeReachForWound">
            <summary>
            Time, in seconds, before the character begins to grab for the wound on the first hit.
            </summary>
            <remarks>
            Default value = 0.2f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.ExagDuration">
            <summary>
            Exaggerate bullet duration (at exagMag/exagTwistMag).
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.ExagMag">
            <summary>
            Exaggerate bullet spine Lean magnitude.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.ExagTwistMag">
            <summary>
            Exaggerate bullet spine Twist magnitude.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.ExagSmooth2Zero">
            <summary>
            Exaggerate bullet duration ramping to zero after exagDuration.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.ExagZeroTime">
            <summary>
            Exaggerate bullet time spent at 0 spine lean/twist after exagDuration + exagSmooth2Zero.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.CpainSmooth2Time">
            <summary>
            Conscious pain duration ramping from zero to cpainMag/cpainTwistMag.
            </summary>
            <remarks>
            Default value = 0.2f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.CpainDuration">
            <summary>
            Conscious pain duration at cpainMag/cpainTwistMag after cpainSmooth2Time.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.CpainMag">
            <summary>
            Conscious pain spine Lean(back/Forward) magnitude (Replaces spinePainMultiplier).
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.CpainTwistMag">
            <summary>
            Conscious pain spine Twist/Lean2Side magnitude Replaces spinePainTwistMultiplier).
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.CpainSmooth2Zero">
            <summary>
            Conscious pain ramping to zero after cpainSmooth2Time + cpainDuration (Replaces spinePainTime).
            </summary>
            <remarks>
            Default value = 1.5f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.Crouching">
            <summary>
            Is the guy crouching or not.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.ChickenArms">
            <summary>
            Type of reaction.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.ReachForWound">
            <summary>
            Type of reaction.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.Fling">
            <summary>
            Type of reaction.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.AllowInjuredArm">
            <summary>
            Injured arm code runs if arm hit (turns and steps and bends injured arm).
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.AllowInjuredLeg">
            <summary>
            When false injured leg is not bent and character does not bend to reach it.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.AllowInjuredLowerLegReach">
            <summary>
            When false don't try to reach for injured Lower Legs (shins/feet).
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.AllowInjuredThighReach">
            <summary>
            When false don't try to reach for injured Thighs.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.StableHandsAndNeck">
            <summary>
```
