<!-- ScriptHookRDRNetAPI lines 18251-18500 of 30005 -->
```csharp
        <member name="P:RDR2.NaturalMotion.ArmsWindmillAdaptiveHelper.ArmDirection">
            <summary>
            </summary>
            <remarks>
            Default value = <see cref="F:RDR2.NaturalMotion.ArmDirections.Adaptive"/>.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillAdaptiveHelper.DisableOnImpact">
            <summary>
            If true, each arm will stop windmilling if it hits the ground.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillAdaptiveHelper.SetBackAngles">
            <summary>
            If true, back angles will be set to compliment arms windmill.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillAdaptiveHelper.UseAngMom">
            <summary>
            If true, use angular momentum about com to choose arm circling direction. Otherwise use com angular velocity.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillAdaptiveHelper.BendLeftElbow">
            <summary>
            If true, bend the left elbow to give a stunt man type scramble look.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillAdaptiveHelper.BendRightElbow">
            <summary>
            If true, bend the right elbow to give a stunt man type scramble look.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillAdaptiveHelper.Mask">
            <summary>
            Two character body-masking value, bitwise joint mask or bitwise logic string of two character body-masking value  (see Active Pose notes for possible values).
            </summary>
            <remarks>
            Default value = "ub".
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the BalancerCollisionsReactionHelper for sending a BalancerCollisionsReaction <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the BalancerCollisionsReaction <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.NumStepsTillSlump">
            <summary>
            Begin slump and stop stepping after this many steps.
            </summary>
            <remarks>
            Default value = 4.
            Min value = 0.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.Stable2SlumpTime">
            <summary>
            Time after becoming stable leaning against a wall that slump starts.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.ExclusionZone">
            <summary>
            Steps are ihibited to not go closer to the wall than this (after impact).
            </summary>
            <remarks>
            Default value = 0.2f.
            Min value = 0.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.FootFrictionMultStart">
            <summary>
            Friction multiplier applied to feet when slump starts.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 4.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.FootFrictionMultRate">
            <summary>
            Friction multiplier reduced by this amount every second after slump starts (only if character is not slumping).
            </summary>
            <remarks>
            Default value = 2.0f.
            Min value = 0.0f.
            Max value = 50.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.BackFrictionMultStart">
            <summary>
            Friction multiplier applied to back when slump starts.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 4.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.BackFrictionMultRate">
            <summary>
            Friction multiplier reduced by this amount every second after slump starts (only if character is not slumping).
            </summary>
            <remarks>
            Default value = 2.0f.
            Min value = 0.0f.
            Max value = 50.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.ImpactLegStiffReduction">
            <summary>
            Reduce the stiffness of the legs by this much as soon as an impact is detected.
            </summary>
            <remarks>
            Default value = 3.0f.
            Min value = 0.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.SlumpLegStiffReduction">
            <summary>
            Reduce the stiffness of the legs by this much as soon as slump starts.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.SlumpLegStiffRate">
            <summary>
            Rate at which the stiffness of the legs is reduced during slump.
            </summary>
            <remarks>
            Default value = 8.0f.
            Min value = 0.0f.
            Max value = 50.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.ReactTime">
            <summary>
            Time that the character reacts to the impact with ub flinch and writhe.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.ImpactExagTime">
            <summary>
            Time that the character exaggerates impact with spine.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.GlanceSpinTime">
            <summary>
            Duration that the glance torque is applied for.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.GlanceSpinMag">
            <summary>
            Magnitude of the glance torque.
            </summary>
            <remarks>
            Default value = 50.0f.
            Min value = 0.0f.
            Max value = 1000.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.GlanceSpinDecayMult">
            <summary>
            Multiplier used when decaying torque spin over time.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.IgnoreColWithIndex">
            <summary>
            Used so impact with the character that is pushing you over doesn't set off the behavior.
            </summary>
            <remarks>
            Default value = -2.
            Min value = -2.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.SlumpMode">
            <summary>
            0=Normal slump(less movement then slump and movement LT small), 1=fast slump, 2=less movement then slump.
            </summary>
            <remarks>
            Default value = 1.
            Min value = 0.
            Max value = 2.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.ReboundMode">
            <summary>
            0=fall2knees/slump if shot not running, 1=stumble, 2=slump, 3=restart.
            </summary>
            <remarks>
            Default value = 0.
            Min value = 0.
            Max value = 3.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.IgnoreColMassBelow">
            <summary>
            Collisions with non-fixed objects with mass below this will not set this behavior off (e.g. ignore guns).
            </summary>
            <remarks>
            Default value = 20.0f.
            Min value = -1.0f.
            Max value = 1000.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.ForwardMode">
            <summary>
            0=slump, 1=fallToKnees if shot is running, otherwise slump.
```
