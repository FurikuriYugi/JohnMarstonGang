<!-- ScriptHookRDRNetAPI lines 15501-15750 of 30005 -->
```csharp
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureLimitsHelper.Lean2">
            <summary>
            </summary>
            <remarks>
            Default value = 1.6f.
            Min value = 0.0f.
            Max value = 3.1f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureLimitsHelper.Twist">
            <summary>
            </summary>
            <remarks>
            Default value = 1.6f.
            Min value = 0.0f.
            Max value = 3.1f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureLimitsHelper.Margin">
            <summary>
            Joint limit margin to add to current animation limits when using those to set runtime limits.
            </summary>
            <remarks>
            Default value = 0.2f.
            Min value = 0.0f.
            Max value = 3.1f.
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.ConfigureSoftLimitHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the ConfigureSoftLimitHelper for sending a ConfigureSoftLimit <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the ConfigureSoftLimit <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureSoftLimitHelper.Index">
            <summary>
            Select limb that the soft limit is going to be applied to.
            </summary>
            <remarks>
            Default value = 0.
            Min value = 0.
            Max value = 3.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureSoftLimitHelper.Stiffness">
            <summary>
            Stiffness of the soft limit.
            Parameter is used to calculate spring term that contributes to the desired acceleration.
            </summary>
            <remarks>
            Default value = 15.0f.
            Min value = 0.0f.
            Max value = 30.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureSoftLimitHelper.Damping">
            <summary>
            Damping of the soft limit.
            Parameter is used to calculate damper term that contributes to the desired acceleration.
            To have the system critically dampened set it to 1.0.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.9f.
            Max value = 1.1f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureSoftLimitHelper.LimitAngle">
            <summary>
            Soft limit angle.
            Positive angle in RAD, measured relatively either from hard limit maxAngle (approach direction = -1) or minAngle (approach direction = 1).
            This angle will be clamped if outside the joint hard limit range.
            </summary>
            <remarks>
            Default value = 0.4f.
            Min value = 0.0f.
            Max value = 6.3f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureSoftLimitHelper.ApproachDirection">
            <summary>
            Limit angle can be measured relatively to joints hard limit minAngle or maxAngle.
            Set it to +1 to measure soft limit angle relatively to hard limit minAngle that corresponds to the maximum stretch of the elbow.
            Set it to -1 to measure soft limit angle relatively to hard limit maxAngle that corresponds to the maximum stretch of the knee.
            </summary>
            <remarks>
            Default value = 1.
            Min value = -1.
            Max value = 1.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureSoftLimitHelper.VelocityScaled">
            <summary>
            Scale stiffness based on character angular velocity.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="T:RDR2.NaturalMotion.ConfigureShotInjuredArmHelper">
            <summary>
            This single message allows you to configure the injured arm reaction during shot.
            </summary>
        </member>
        <member name="M:RDR2.NaturalMotion.ConfigureShotInjuredArmHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the ConfigureShotInjuredArmHelper for sending a ConfigureShotInjuredArm <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the ConfigureShotInjuredArm <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
            <remarks>
            This single message allows you to configure the injured arm reaction during shot.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureShotInjuredArmHelper.InjuredArmTime">
            <summary>
            Length of the reaction.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureShotInjuredArmHelper.HipYaw">
            <summary>
            Amount of hip twist.  (Negative values twist into bullet direction - probably not what is wanted).
            </summary>
            <remarks>
            Default value = 0.8f.
            Min value = -2.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureShotInjuredArmHelper.HipRoll">
            <summary>
            Amount of hip roll.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = -2.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureShotInjuredArmHelper.ForceStepExtraHeight">
            <summary>
            Additional height added to stepping foot.
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.0f.
            Max value = 0.7f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureShotInjuredArmHelper.ForceStep">
            <summary>
            Force a step to be taken whether pushed out of balance or not.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureShotInjuredArmHelper.StepTurn">
            <summary>
            Turn the character using the balancer.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureShotInjuredArmHelper.VelMultiplierStart">
            <summary>
            Start velocity where parameters begin to be ramped down to zero linearly.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 20.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureShotInjuredArmHelper.VelMultiplierEnd">
            <summary>
            End velocity of ramp where parameters are scaled to zero.
            </summary>
            <remarks>
            Default value = 5.0f.
            Min value = 1.0f.
            Max value = 40.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureShotInjuredArmHelper.VelForceStep">
            <summary>
            Velocity above which a step is not forced.
            </summary>
            <remarks>
            Default value = 0.8f.
            Min value = 0.0f.
            Max value = 20.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureShotInjuredArmHelper.VelStepTurn">
            <summary>
            Velocity above which a stepTurn is not asked for.
            </summary>
            <remarks>
            Default value = 0.8f.
            Min value = 0.0f.
            Max value = 20.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureShotInjuredArmHelper.VelScales">
            <summary>
            Use the velocity scaling parameters.
            Tune for standing still then use velocity scaling to make sure a running character stays balanced (the turning tends to make the character fall over more at speed).
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="T:RDR2.NaturalMotion.ConfigureShotInjuredLegHelper">
            <summary>
            This single message allows you to configure the injured leg reaction during shot.
            </summary>
        </member>
        <member name="M:RDR2.NaturalMotion.ConfigureShotInjuredLegHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the ConfigureShotInjuredLegHelper for sending a ConfigureShotInjuredLeg <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the ConfigureShotInjuredLeg <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
            <remarks>
            This single message allows you to configure the injured leg reaction during shot.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureShotInjuredLegHelper.TimeBeforeCollapseWoundLeg">
            <summary>
            Time before a wounded leg is set to be weak and cause the character to collapse.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureShotInjuredLegHelper.LegInjuryTime">
            <summary>
            Leg injury duration (reaction to being shot in leg).
            </summary>
            <remarks>
            Default value = 0.4f.
            Min value = 0.0f.
```
