<!-- ScriptHookRDRNetAPI lines 25001-25250 of 30005 -->
```csharp
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotConfigureArmsHelper.Fling2TimeBefore">
            <summary>
            Time after hit that the fling will start (allows for a bit of loose arm movement from bullet impact.snap etc).
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotConfigureArmsHelper.Fling2Time">
            <summary>
            Duration of the fling behavior.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotConfigureArmsHelper.Fling2MStiffL">
            <summary>
            Muscle stiffness of the left arm.
            If negative then uses the shots underlying muscle stiffness from controlStiffness (i.e. respects looseness).
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = -1.0f.
            Max value = 1.5f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotConfigureArmsHelper.Fling2MStiffR">
            <summary>
            Muscle stiffness of the right arm.
            If negative then uses the shots underlying muscle stiffness from controlStiffness (i.e. respects looseness).
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.0f.
            Max value = 1.5f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotConfigureArmsHelper.Fling2RelaxTimeL">
            <summary>
            Maximum time before the left arm relaxes in the fling.
            It will relax automatically when the arm has completed it's bent arm fling.
            This is what causes the arm to straighten.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotConfigureArmsHelper.Fling2RelaxTimeR">
            <summary>
            Maximum time before the right arm relaxes in the fling.
            It will relax automatically when the arm has completed it's bent arm fling.
            This is what causes the arm to straighten.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotConfigureArmsHelper.Fling2AngleMinL">
            <summary>
            Minimum fling angle for left arm.
            Fling angle is random in the range fling2AngleMin:fling2AngleMax.
            Angle of fling in radians measured from the body horizontal sideways from shoulder.
            Positive is up, 0 shoulder level, negative down.
            </summary>
            <remarks>
            Default value = -1.5f.
            Min value = -1.5f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotConfigureArmsHelper.Fling2AngleMaxL">
            <summary>
            Maximum fling angle for left arm.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = -1.5f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotConfigureArmsHelper.Fling2AngleMinR">
            <summary>
            Minimum fling angle for right arm.
            </summary>
            <remarks>
            Default value = -1.5f.
            Min value = -1.5f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotConfigureArmsHelper.Fling2AngleMaxR">
            <summary>
            Maximum fling angle for right arm.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = -1.5f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotConfigureArmsHelper.Fling2LengthMinL">
            <summary>
            Minimum left arm length.
            Arm length is random in the range fling2LengthMin:fling2LengthMax.
            Arm length maps one to one with elbow angle.
            These values are scaled internally for the female character.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.3f.
            Max value = 0.6f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotConfigureArmsHelper.Fling2LengthMaxL">
            <summary>
            Maximum left arm length.
            </summary>
            <remarks>
            Default value = 0.6f.
            Min value = 0.3f.
            Max value = 0.6f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotConfigureArmsHelper.Fling2LengthMinR">
            <summary>
            Min right arm length.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.3f.
            Max value = 0.6f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotConfigureArmsHelper.Fling2LengthMaxR">
            <summary>
            Max right arm length.
            </summary>
            <remarks>
            Default value = 0.6f.
            Min value = 0.3f.
            Max value = 0.6f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotConfigureArmsHelper.Bust">
            <summary>
            Has the character got a bust.
            If so then cupBust (move bust reach targets below bust) or bustElbowLift and cupSize (stop upperArm penetrating bust and move bust targets to surface of bust) are implemented.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotConfigureArmsHelper.BustElbowLift">
            <summary>
            Lift the elbows up this much extra to avoid upper arm penetrating the bust (when target hits spine2 or spine3).
            </summary>
            <remarks>
            Default value = 0.7f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotConfigureArmsHelper.CupSize">
            <summary>
            Amount reach target to bust (spine2) will be offset forward by.
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotConfigureArmsHelper.CupBust">
            <summary>
            All reach targets above or on the bust will cause a reach below the bust.
            (specifically moves spine3 and spine2 targets to spine1).
            BustElbowLift and cupSize are ignored.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="T:RDR2.NaturalMotion.SmartFallHelper">
            <summary>
            Clone of High Fall with a wider range of operating conditions.
            </summary>
        </member>
        <member name="M:RDR2.NaturalMotion.SmartFallHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the SmartFallHelper for sending a SmartFall <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the SmartFall <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
            <remarks>
            Clone of High Fall with a wider range of operating conditions.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.BodyStiffness">
            <summary>
            Stiffness of body.
            Value feeds through to bodyBalance (synced with defaults), to armsWindmill (14 for this value at default ), legs pedal, head look and roll down stairs directly.
            </summary>
            <remarks>
            Default value = 11.0f.
            Min value = 6.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.Bodydamping">
            <summary>
            The damping of the joints.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 3.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.Catchfalltime">
            <summary>
            The length of time before the impact that the character transitions to the landing.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.CrashOrLandCutOff">
            <summary>
            0.52angle is 0.868 dot//A threshold for deciding how far away from upright the character needs to be before bailing out (going into a foetal) instead of trying to land (keeping stretched out).
            NB: never does bailout if ignorWorldCollisions true.
            </summary>
            <remarks>
            Default value = 0.9f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
```
