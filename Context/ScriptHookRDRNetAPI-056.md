<!-- ScriptHookRDRNetAPI lines 13751-14000 of 30005 -->
```csharp
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.HipLeanAcc">
            <summary>
            Multiplier on the floorAcceleration added to the leanHips.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.LeanAccMax">
            <summary>
            Max floorAcceleration allowed for lean and leanHips.
            </summary>
            <remarks>
            Default value = 5.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.ResistAcc">
            <summary>
            Level of cheat force added to character to resist the effect of floorAcceleration (anti-Acceleration) - added to upperbody.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.ResistAccMax">
            <summary>
            Max floorAcceleration allowed for anti-Acceleration. If  GT 20.0 then it is probably in a crash.
            </summary>
            <remarks>
            Default value = 3.0f.
            Min value = 0.0f.
            Max value = 20.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.FootSlipCompOnMovingFloor">
            <summary>
            This parameter will be removed when footSlipCompensation preserves the foot angle on a moving floor]. If the character detects a moving floor and footSlipCompOnMovingFloor is false then it will turn off footSlipCompensation - at footSlipCompensation preserves the global heading of the feet.  If footSlipCompensation is off then the character usually turns to the side in the end although when turning the vehicle turns it looks promising for a while.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.AnkleEquilibrium">
            <summary>
            Ankle equilibrium angle used when static balancing.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.ExtraFeetApart">
            <summary>
            Additional feet apart setting.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.DontStepTime">
            <summary>
            Amount of time at the start of a balance before the character is allowed to start stepping.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.BalanceAbortThreshold">
            <summary>
            When the character gives up and goes into a fall.  Larger values mean that the balancer can lean more before failing.
            </summary>
            <remarks>
            Default value = 0.6f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.GiveUpHeight">
            <summary>
            Height between lowest foot and COM below which balancer will give up.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 1.5f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.StepClampScale">
            <summary>
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.StepClampScaleVariance">
            <summary>
            Variance in clamp scale every step. If negative only takes away from clampScale.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.PredictionTimeHip">
            <summary>
            Amount of time (seconds) into the future that the character tries to move hip to (kind of).  Will be controlled by balancer in future but can help recover spine quicker from bending forwards to much.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.PredictionTime">
            <summary>
            Amount of time (seconds) into the future that the character tries to step to. Bigger values try to recover with fewer, bigger steps. Smaller values recover with smaller steps, and generally recover less.
            </summary>
            <remarks>
            Default value = 0.2f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.PredictionTimeVariance">
            <summary>
            Variance in predictionTime every step. If negative only takes away from predictionTime.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.MaxSteps">
            <summary>
            Maximum number of steps that the balancer will take.
            </summary>
            <remarks>
            Default value = 100.
            Min value = 1.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.MaxBalanceTime">
            <summary>
            Maximum time(seconds) that the balancer will balance for.
            </summary>
            <remarks>
            Default value = 50.0f.
            Min value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.ExtraSteps">
            <summary>
            Allow the balancer to take this many more steps before hitting maxSteps. If negative nothing happens(safe default).
            </summary>
            <remarks>
            Default value = -1.
            Min value = -1.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.ExtraTime">
            <summary>
            Allow the balancer to balance for this many more seconds before hitting maxBalanceTime.  If negative nothing happens(safe default).
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.FallType">
            <summary>
            How to fall after maxSteps or maxBalanceTime.
            </summary>
            <remarks>
            Default value = <see cref="F:RDR2.NaturalMotion.FallType.RampDownStiffness"/>.
            If <see cref="F:RDR2.NaturalMotion.FallType.Slump"/> BCR has to be active.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.FallMult">
            <summary>
            Multiply the rampDown of stiffness on falling by this amount ( GT 1 fall quicker).
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 100.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.FallReduceGravityComp">
            <summary>
            Reduce gravity compensation as the legs weaken on falling.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.RampHipPitchOnFail">
            <summary>
            Bend over when falling after maxBalanceTime.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.StableLinSpeedThresh">
            <summary>
            Linear speed threshold for successful balance.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.StableRotSpeedThresh">
            <summary>
            Rotational speed threshold for successful balance.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.FailMustCollide">
            <summary>
            The upper body of the character must be colliding and other failure conditions met to fail.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.IgnoreFailure">
            <summary>
            Ignore maxSteps and maxBalanceTime and try to balance forever.
```
