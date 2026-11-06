<!-- ScriptHookRDRNetAPI lines 14251-14500 of 30005 -->
```csharp
        </member>
        <member name="M:RDR2.NaturalMotion.ConfigureSelfAvoidanceHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the ConfigureSelfAvoidanceHelper for sending a ConfigureSelfAvoidance <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the ConfigureSelfAvoidance <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
            <remarks>
            This single message allows to configure self avoidance for the character.BBDD Self avoidance tech.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureSelfAvoidanceHelper.UseSelfAvoidance">
            <summary>
            Enable or disable self avoidance tech.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureSelfAvoidanceHelper.OverwriteDragReduction">
            <summary>
            Specify whether self avoidance tech should use original IK input target or the target that has been already modified by getStabilisedPos() tech i.e. function that compensates for rotational and linear velocity of shoulder/thigh.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureSelfAvoidanceHelper.TorsoSwingFraction">
            <summary>
            Place the adjusted target this much along the arc between effector (wrist) and target, value in range [0,1].
            </summary>
            <remarks>
            Default value = 0.8f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureSelfAvoidanceHelper.MaxTorsoSwingAngleRad">
            <summary>
            Max value on the effector (wrist) to adjusted target offset.
            </summary>
            <remarks>
            Default value = 0.8f.
            Min value = 0.0f.
            Max value = 1.6f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureSelfAvoidanceHelper.SelfAvoidIfInSpineBoundsOnly">
            <summary>
            Restrict self avoidance to operate on targets that are within character torso bounds only.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureSelfAvoidanceHelper.SelfAvoidAmount">
            <summary>
            Amount of self avoidance offset applied when angle from effector (wrist) to target is greater then right angle i.e. when total offset is a blend between where effector currently is to value that is a product of total arm length and selfAvoidAmount. SelfAvoidAmount is in a range between [0, 1].
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureSelfAvoidanceHelper.OverwriteTwist">
            <summary>
            Overwrite desired IK twist with self avoidance procedural twist.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureSelfAvoidanceHelper.UsePolarPathAlgorithm">
            <summary>
            Use the alternative self avoidance algorithm that is based on linear and polar target blending. WARNING: It only requires "radius" in terms of parametrization.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureSelfAvoidanceHelper.Radius">
            <summary>
            Self avoidance radius, measured out from the spine axis along the plane perpendicular to that axis. The closer is the proximity of reaching target to that radius, the more polar (curved) motion is used for offsetting the target. WARNING: Parameter only used by the alternative algorithm that is based on linear and polar target blending.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.ConfigureBulletsHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the ConfigureBulletsHelper for sending a ConfigureBullets <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the ConfigureBullets <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.ImpulseSpreadOverParts">
            <summary>
            Spreads impulse across parts. Currently only for spine parts, not limbs.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.ImpulseLeakageStrengthScaled">
            <summary>
            For weaker characters subsequent impulses remain strong.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.ImpulsePeriod">
            <summary>
            Duration that impulse is spread over (triangular shaped).
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.ImpulseTorqueScale">
            <summary>
            An impulse applied at a point on a body equivalent to an impulse at the center of the body and a torque.  This parameter scales the torque component. (The torque component seems to be excite the rage looseness bug which sends the character in a sometimes wildly different direction to an applied impulse).
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.LoosenessFix">
            <summary>
            Fix the rage looseness bug by applying only the impulse at the center of the body unless it is a spine part then apply the twist component only of the torque as well.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.ImpulseDelay">
            <summary>
            Time from hit before impulses are being applied.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.ImpulseReductionPerShot">
            <summary>
            By how much are subsequent impulses reduced (e.g. 0.0: no reduction, 0.1: 10% reduction each new hit).
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.ImpulseRecovery">
            <summary>
            Recovery rate of impulse strength per second (impulse strength from 0.0:1.0).  At 60fps a impulseRecovery=60.0 will recover in 1 frame.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 60.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.ImpulseMinLeakage">
            <summary>
            The minimum amount of impulse leakage allowed.
            </summary>
            <remarks>
            Default value = 0.2f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.TorqueMode">
            <summary>
            </summary>
            <remarks>
            Default value = <see cref="F:RDR2.NaturalMotion.TorqueMode.Disabled"/>.
            If <see cref="F:RDR2.NaturalMotion.TorqueMode.Proportional"/> - proportional to character strength, can reduce impulse amount.
            If <see cref="F:RDR2.NaturalMotion.TorqueMode.Additive"/> - no reduction of impulse and not proportional to character strength.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.TorqueSpinMode">
            <summary>
            </summary>
            <remarks>
            Default value = <see cref="F:RDR2.NaturalMotion.TorqueSpinMode.FromImpulse"/>.
            If <see cref="F:RDR2.NaturalMotion.TorqueSpinMode.Flipping"/> a burst effect is achieved.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.TorqueFilterMode">
            <summary>
            </summary>
            <remarks>
            Default value = <see cref="F:RDR2.NaturalMotion.TorqueFilterMode.ApplyEveryBullet"/>.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.TorqueAlwaysSpine3">
            <summary>
            Always apply torques to spine3 instead of actual part hit.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.TorqueDelay">
            <summary>
            Time from hit before torques are being applied.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.TorquePeriod">
            <summary>
            Duration of torque.
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.TorqueGain">
            <summary>
            Multiplies impulse magnitude to arrive at torque that is applied.
            </summary>
            <remarks>
            Default value = 4.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.TorqueCutoff">
            <summary>
            Minimum ratio of impulse that remains after converting to torque (if in strength-proportional mode).
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
```
