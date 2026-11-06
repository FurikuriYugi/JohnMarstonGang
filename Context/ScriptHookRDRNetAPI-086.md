<!-- ScriptHookRDRNetAPI lines 21251-21500 of 30005 -->
```csharp
            Creates a new Instance of the HighFallHelper for sending a HighFall <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the HighFall <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.HighFallHelper.BodyStiffness">
            <summary>
            Stiffness of body. Value feeds through to bodyBalance (synced with defaults), to armsWindmill (14 for this value at default ), legs pedal, head look and roll down stairs directly.
            </summary>
            <remarks>
            Default value = 11.0f.
            Min value = 6.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HighFallHelper.Bodydamping">
            <summary>
            The damping of the joints.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 3.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HighFallHelper.Catchfalltime">
            <summary>
            The length of time before the impact that the character transitions to the landing.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HighFallHelper.CrashOrLandCutOff">
            <summary>
            0.52angle is 0.868 dot//A threshold for deciding how far away from upright the character needs to be before bailing out (going into a foetal) instead of trying to land (keeping stretched out).  NB: never does bailout if ignorWorldCollisions true.
            </summary>
            <remarks>
            Default value = 0.9f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HighFallHelper.PdStrength">
            <summary>
            Strength of the controller to keep the character at angle aimAngleBase from vertical.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HighFallHelper.PdDamping">
            <summary>
            Damping multiplier of the controller to keep the character at angle aimAngleBase from vertical.  The actual damping is pdDamping*pdStrength*constant*angVel.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 5.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HighFallHelper.ArmAngSpeed">
            <summary>
            Arm circling speed in armWindMillAdaptive.
            </summary>
            <remarks>
            Default value = 7.9f.
            Min value = 0.0f.
            Max value = 20.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HighFallHelper.ArmAmplitude">
            <summary>
            In armWindMillAdaptive.
            </summary>
            <remarks>
            Default value = 2.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HighFallHelper.ArmPhase">
            <summary>
            In armWindMillAdaptive 3.1 opposite for stuntman.
            1.0 old default. 0.0 in phase.
            </summary>
            <remarks>
            Default value = 3.1f.
            Min value = 0.0f.
            Max value = 6.3f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HighFallHelper.ArmBendElbows">
            <summary>
            In armWindMillAdaptive bend the elbows as a function of armAngle.
            For stunt man true otherwise false.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HighFallHelper.LegRadius">
            <summary>
            Radius of legs on pedal.
            </summary>
            <remarks>
            Default value = 0.4f.
            Min value = 0.0f.
            Max value = 0.5f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HighFallHelper.LegAngSpeed">
            <summary>
            In pedal.
            </summary>
            <remarks>
            Default value = 7.9f.
            Min value = 0.0f.
            Max value = 15.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HighFallHelper.LegAsymmetry">
            <summary>
            0.0 for stuntman.  Random offset applied per leg to the angular speed to desynchronize the pedaling - set to 0 to disable, otherwise should be set to less than the angularSpeed value.
            </summary>
            <remarks>
            Default value = 4.0f.
            Min value = -10.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HighFallHelper.Arms2LegsPhase">
            <summary>
            Phase angle between the arms and legs circling angle.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 6.5f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HighFallHelper.Arms2LegsSync">
            <summary>
            Syncs the arms angle to what the leg angle is.
            </summary>
            <remarks>
            Default value = <see cref="F:RDR2.NaturalMotion.Synchroisation.AlwaysSynced"/>.
            All speed/direction parameters of armswindmill are overwritten if = <see cref="F:RDR2.NaturalMotion.Synchroisation.AlwaysSynced"/>.
            If <see cref="F:RDR2.NaturalMotion.Synchroisation.SyncedAtStart"/> and you want synced arms/legs then armAngSpeed=legAngSpeed, legAsymmetry = 0.0 (to stop randomizations of the leg cicle speed).
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HighFallHelper.ArmsUp">
            <summary>
            Where to put the arms when preparing to land.
            Approx 1 = above head, 0 = head height, -1 = down.
            LT -2.0 use catchFall arms, LT -3.0 use prepare for landing pose if Agent is due to land vertically, feet first.
            </summary>
            <remarks>
            Default value = -3.1f.
            Min value = -4.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HighFallHelper.OrientateBodyToFallDirection">
            <summary>
            Toggle to orientate to fall direction.
            i.e. orientate so that the character faces the horizontal velocity direction.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HighFallHelper.OrientateTwist">
            <summary>
            If false don't worry about the twist angle of the character when orientating the character.
            If false this allows the twist axis of the character to be free (You can get a nice twisting highFall like the one in dieHard 4 when the car goes into the helicopter).
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HighFallHelper.OrientateMax">
            <summary>
            DEVEL parameter - suggest you don't edit it.
            Maximum torque the orientation controller can apply.
            If 0 then no helper torques will be used.
            300 will orientate the character softly for all but extreme angles away from aimAngleBase.
            If abs (current -aimAngleBase) is getting near 3.0 then this can be reduced to give a softer feel.
            </summary>
            <remarks>
            Default value = 300.0f.
            Min value = 0.0f.
            Max value = 2000.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HighFallHelper.AlanRickman">
            <summary>
            If true then orientate the character to face the point from where it started falling.
            High fall like the one in "Die Hard" with Alan Rickman.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HighFallHelper.FowardRoll">
            <summary>
            Try to execute a forward Roll on landing.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HighFallHelper.UseZeroPose_withFowardRoll">
            <summary>
            Blend to a zero pose when forward roll is attempted.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HighFallHelper.AimAngleBase">
            <summary>
            Angle from vertical the pdController is driving to (positive = forwards).
            </summary>
            <remarks>
            Default value = 0.2f.
            Min value = -3.1f.
            Max value = 3.1f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HighFallHelper.FowardVelRotation">
            <summary>
            Scale to add/subtract from aimAngle based on forward speed (Internal).
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HighFallHelper.FootVelCompScale">
            <summary>
            Scale to change to amount of vel that is added to the foot ik from the velocity (Internal).
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.0f.
```
