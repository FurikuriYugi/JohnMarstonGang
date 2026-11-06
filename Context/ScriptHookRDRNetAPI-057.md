<!-- ScriptHookRDRNetAPI lines 14001-14250 of 30005 -->
```csharp
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.ChangeStepTime">
            <summary>
            Time not in contact (airborne) before step is changed. If -ve don't change step.
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.0f.
            Max value = 5.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.BalanceIndefinitely">
            <summary>
            Ignore maxSteps and maxBalanceTime and try to balance forever.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.MovingFloor">
            <summary>
            Temporary variable to ignore movingFloor code that generally causes the character to fall over if the feet probe a moving object e.g. treading on a gun.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.AirborneStep">
            <summary>
            When airborne try to step.  Set to false for e.g. shotGun reaction.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.UseComDirTurnVelThresh">
            <summary>
            Velocity below which the balancer turns in the direction of the COM forward instead of the ComVel - for use with shot from running with high upright constraint use 1.9.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.MinKneeAngle">
            <summary>
            Minimum knee angle (-ve value will mean this functionality is not applied).  0.4 seems a good value.
            </summary>
            <remarks>
            Default value = -0.5f.
            Min value = -0.5f.
            Max value = 1.5f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.FlatterSwingFeet">
            <summary>
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.FlatterStaticFeet">
            <summary>
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.AvoidLeg">
            <summary>
            If true then balancer tries to avoid leg2leg collisions/avoid crossing legs. Avoid tries to not step across a line of the inside of the stance leg's foot.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.AvoidFootWidth">
            <summary>
            NB. Very sensitive. Avoid tries to not step across a line of the inside of the stance leg's foot. AvoidFootWidth = how much inwards from the ankle this line is in (m).
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.AvoidFeedback">
            <summary>
            NB. Very sensitive. Avoid tries to not step across a line of the inside of the stance leg's foot. Avoid doesn't allow the desired stepping foot to cross the line.  avoidFeedback = how much of the actual crossing of that line is fedback as an error.
            </summary>
            <remarks>
            Default value = 0.6f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.LeanAgainstVelocity">
            <summary>
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.StepDecisionThreshold">
            <summary>
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.StepIfInSupport">
            <summary>
            The balancer sometimes decides to step even if balanced.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.AlwaysStepWithFarthest">
            <summary>
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.StandUp">
            <summary>
            Standup more with increased velocity.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.DepthFudge">
            <summary>
            Supposed to increase foot friction: Impact depth of a collision with the foot is changed when the balancer is running - impact.SetDepth(impact.GetDepth() - depthFudge).
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.DepthFudgeStagger">
            <summary>
            Supposed to increase foot friction: Impact depth of a collision with the foot is changed when staggerFall is running - impact.SetDepth(impact.GetDepth() - depthFudgeStagger).
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.FootFriction">
            <summary>
            Foot friction multiplier is multiplied by this amount if balancer is running.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 40.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.FootFrictionStagger">
            <summary>
            Foot friction multiplier is multiplied by this amount if staggerFall is running.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 40.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.BackwardsLeanCutoff">
            <summary>
            Backwards lean threshold to cut off stay upright forces. 0.0 Vertical - 1.0 horizontal.  0.6 is a sensible value.  NB: the balancer does not fail in order to give stagger that extra step as it falls.  A backwards lean of GT 0.6 will generally mean the balancer will soon fail without stayUpright forces.
            </summary>
            <remarks>
            Default value = 1.1f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.GiveUpHeightEnd">
            <summary>
            If this value is different from giveUpHeight, actual giveUpHeight will be ramped toward this value.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 1.5f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.BalanceAbortThresholdEnd">
            <summary>
            If this value is different from balanceAbortThreshold, actual balanceAbortThreshold will be ramped toward this value.
            </summary>
            <remarks>
            Default value = 0.6f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.GiveUpRampDuration">
            <summary>
            Duration of ramp from start of behavior for above two parameters. If smaller than 0, no ramp is applied.
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.LeanToAbort">
            <summary>
            Lean at which to send abort message when maxSteps or maxBalanceTime is reached.
            </summary>
            <remarks>
            Default value = 0.6f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="T:RDR2.NaturalMotion.ConfigureBalanceResetHelper">
            <summary>
            Reset the values configurable by the Configure Balance message to their defaults.
            </summary>
        </member>
        <member name="M:RDR2.NaturalMotion.ConfigureBalanceResetHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the ConfigureBalanceResetHelper for sending a ConfigureBalanceReset <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the ConfigureBalanceReset <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
            <remarks>
            Reset the values configurable by the Configure Balance message to their defaults.
            </remarks>
        </member>
        <member name="T:RDR2.NaturalMotion.ConfigureSelfAvoidanceHelper">
            <summary>
            This single message allows to configure self avoidance for the character.BBDD Self avoidance tech.
            </summary>
```
