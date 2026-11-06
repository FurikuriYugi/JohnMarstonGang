<!-- ScriptHookRDRNetAPI lines 26501-26750 of 30005 -->
```csharp
            Arm damping during the yanked timescale i.e. timeAtStartValues.
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.YankedHelper.SpineDampingStart">
            <summary>
            Spine damping during the yanked timescale i.e. timeAtStartValues.
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.YankedHelper.SpineStiffnessStart">
            <summary>
            Spine stiffness during the yanked timescale i.e. timeAtStartValues.
            </summary>
            <remarks>
            Default value = 3.0f.
            Min value = 0.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.YankedHelper.TimeAtStartValues">
            <summary>
            Time spent with Start values for arms and spine stiffness and damping i.e. for whiplash effect.
            </summary>
            <remarks>
            Default value = 0.4f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.YankedHelper.RampTimeFromStartValues">
            <summary>
            Time spent ramping from Start to end values for arms and spine stiffness and damping i.e. for whiplash effect (occurs after timeAtStartValues).
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.YankedHelper.StepsTillStartEnd">
            <summary>
            Steps taken before lowerBodyStiffness starts ramping down.
            </summary>
            <remarks>
            Default value = 2.
            Min value = 0.
            Max value = 100.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.YankedHelper.TimeStartEnd">
            <summary>
            Time from start of behavior before lowerBodyStiffness starts ramping down by perStepReduction1.
            </summary>
            <remarks>
            Default value = 100.0f.
            Min value = 0.0f.
            Max value = 100.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.YankedHelper.RampTimeToEndValues">
            <summary>
            Time spent ramping from lowerBodyStiffness to lowerBodyStiffnessEnd.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.YankedHelper.LowerBodyStiffness">
            <summary>
            LowerBodyStiffness should be 12.
            </summary>
            <remarks>
            Default value = 12.0f.
            Min value = 0.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.YankedHelper.LowerBodyStiffnessEnd">
            <summary>
            LowerBodyStiffness at end.
            </summary>
            <remarks>
            Default value = 8.0f.
            Min value = 0.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.YankedHelper.PerStepReduction">
            <summary>
            LowerBody stiffness will be reduced every step to make the character fallover.
            </summary>
            <remarks>
            Default value = 1.5f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.YankedHelper.HipPitchForward">
            <summary>
            Amount to bend forward at the hips (+ve forward, -ve backwards).
            Behavior switches between hipPitchForward and hipPitchBack.
            </summary>
            <remarks>
            Default value = 0.6f.
            Min value = -1.3f.
            Max value = 1.3f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.YankedHelper.HipPitchBack">
            <summary>
            Amount to bend backwards at the hips (+ve backwards, -ve forwards).
            Behavior switches between hipPitchForward and hipPitchBack.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = -1.3f.
            Max value = 1.3f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.YankedHelper.SpineBend">
            <summary>
            Bend/Twist the spine amount.
            </summary>
            <remarks>
            Default value = 0.7f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.YankedHelper.FootFriction">
            <summary>
            Foot friction when standing/stepping.  0.5 gives a good slide sometimes.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.YankedHelper.TurnThresholdMin">
            <summary>
            Min angle at which the turn with toggle to the other direction (actual toggle angle is chosen randomly in range min to max).
            If it is 1 then it will never toggle.
            If negative then no turn is applied.
            </summary>
            <remarks>
            Default value = 0.6f.
            Min value = -0.1f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.YankedHelper.TurnThresholdMax">
            <summary>
            Max angle at which the turn with toggle to the other direction (actual toggle angle is chosen randomly in range min to max).
            If it is 1 then it will never toggle.
            If negative then no turn is applied.
            </summary>
            <remarks>
            Default value = 0.6f.
            Min value = -0.1f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.YankedHelper.UseHeadLook">
            <summary>
            Enable and provide a look-at target to make the character's head turn to face it while balancing.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.YankedHelper.HeadLookPos">
            <summary>
            Position of thing to look at.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.YankedHelper.HeadLookInstanceIndex">
            <summary>
            Level index of thing to look at.
            </summary>
            <remarks>
            Default value = -1.
            Min value = -1.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.YankedHelper.HeadLookAtVelProb">
            <summary>
            Probability [0-1] that headLook will be looking in the direction of velocity when stepping.
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.YankedHelper.ComVelRDSThresh">
            <summary>
            For handsAndKnees catchfall ONLY: comVel above which rollDownstairs will start.
            </summary>
            <remarks>
            Default value = 2.0f.
            Min value = 0.0f.
            Max value = 20.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.YankedHelper.HulaPeriod">
            <summary>
            0.25 A complete wiggle will take 4*hulaPeriod.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.YankedHelper.HipAmplitude">
            <summary>
            Amount of hip movement.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 4.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.YankedHelper.SpineAmplitude">
            <summary>
            Amount of spine movement.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 4.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.YankedHelper.MinRelaxPeriod">
```
