<!-- ScriptHookRDRNetAPI lines 25251-25500 of 30005 -->
```csharp
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.PdStrength">
            <summary>
            Strength of the controller to keep the character at angle aimAngleBase from vertical.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.PdDamping">
            <summary>
            Damping multiplier of the controller to keep the character at angle aimAngleBase from vertical.
            The actual damping is pdDamping*pdStrength*constant*angVel.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 5.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.ArmAngSpeed">
            <summary>
            Arm circling speed in armWindMillAdaptive.
            </summary>
            <remarks>
            Default value = 7.9f.
            Min value = 0.0f.
            Max value = 20.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.ArmAmplitude">
            <summary>
            In armWindMillAdaptive.
            </summary>
            <remarks>
            Default value = 2.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.ArmPhase">
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
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.ArmBendElbows">
            <summary>
            In armWindMillAdaptive bend the elbows as a function of armAngle.
            For stunt man true otherwise false.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.LegRadius">
            <summary>
            Radius of legs on pedal.
            </summary>
            <remarks>
            Default value = 0.4f.
            Min value = 0.0f.
            Max value = 0.5f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.LegAngSpeed">
            <summary>
            In pedal.
            </summary>
            <remarks>
            Default value = 7.9f.
            Min value = 0.0f.
            Max value = 15.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.LegAsymmetry">
            <summary>
            0.0 for stunt man.
            Random offset applied per leg to the angular speed to desynchronize the pedaling - set to 0 to disable, otherwise should be set to less than the angularSpeed value.
            </summary>
            <remarks>
            Default value = 4.0f.
            Min value = -10.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.Arms2LegsPhase">
            <summary>
            Phase angle between the arms and legs circling angle.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 6.5f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.Arms2LegsSync">
            <summary>
            Syncs the arms angle to what the leg angle is.
            </summary>
            <remarks>
            Default value = <see cref="F:RDR2.NaturalMotion.Synchroisation.AlwaysSynced"/>.
            All speed/direction parameters of armswindmill are overwritten if = <see cref="F:RDR2.NaturalMotion.Synchroisation.AlwaysSynced"/>.
            If <see cref="F:RDR2.NaturalMotion.Synchroisation.SyncedAtStart"/> and you want synced arms/legs then armAngSpeed=legAngSpeed, legAsymmetry = 0.0 (to stop randomizations of the leg cicle speed).
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.ArmsUp">
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
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.OrientateBodyToFallDirection">
            <summary>
            Toggle to orientate to fall direction.  i.e. orientate so that the character faces the horizontal velocity direction.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.OrientateTwist">
            <summary>
            If false don't worry about the twist angle of the character when orientating the character.
            If false this allows the twist axis of the character to be free (You can get a nice twisting highFall like the one in dieHard 4 when the car goes into the helicopter).
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.OrientateMax">
            <summary>
            DEVEL parameter - suggest you don't edit it.  Maximum torque the orientation controller can apply.
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
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.AlanRickman">
            <summary>
            If true then orientate the character to face the point from where it started falling.
            High fall like the one in "Die Hard" with Alan Rickman.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.FowardRoll">
            <summary>
            Try to execute a forward Roll on landing.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.UseZeroPose_withFowardRoll">
            <summary>
            Blend to a zero pose when forward roll is attempted.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.AimAngleBase">
            <summary>
            Angle from vertical the pdController is driving to (positive = forwards).
            </summary>
            <remarks>
            Default value = 0.2f.
            Min value = -3.1f.
            Max value = 3.1f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.FowardVelRotation">
            <summary>
            Scale to add/subtract from aimAngle based on forward speed (Internal).
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.FootVelCompScale">
            <summary>
            Scale to change to amount of vel that is added to the foot ik from the velocity (Internal).
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.SideD">
            <summary>
            Sideoffset for the feet during prepareForLanding. +ve = right.
            </summary>
            <remarks>
            Default value = 0.2f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.FowardOffsetOfLegIK">
            <summary>
            Forward offset for the feet during prepareForLanding.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.LegL">
            <summary>
            Leg Length for ik (Internal)//unused.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.CatchFallCutOff">
            <summary>
            0.5angle is 0.878 dot. Cutoff to go to the catchFall (internal) //mmmtodo do like crashOrLandCutOff.
            </summary>
            <remarks>
            Default value = 0.9f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.LegStrength">
```
