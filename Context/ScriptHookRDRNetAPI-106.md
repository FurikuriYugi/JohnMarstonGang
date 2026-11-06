<!-- ScriptHookRDRNetAPI lines 26251-26500 of 30005 -->
```csharp
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.TeeterHelper.LeanAwayTime">
            <summary>
            Time-to-edge threshold to start leaning away from a potential fall.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.TeeterHelper.LeanAwayScale">
            <summary>
            Scales stay upright lean and hip pitch.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.TeeterHelper.TeeterTime">
            <summary>
            Time-to-edge threshold to start full-on teeter (more aggressive lean, drop-and-twist, etc).
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.UpperBodyFlinchHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the UpperBodyFlinchHelper for sending a UpperBodyFlinch <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the UpperBodyFlinch <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.UpperBodyFlinchHelper.HandDistanceLeftRight">
            <summary>
            Left-Right distance between the hands.
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.UpperBodyFlinchHelper.HandDistanceFrontBack">
            <summary>
            Front-Back distance between the hands.
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.UpperBodyFlinchHelper.HandDistanceVertical">
            <summary>
            Vertical distance between the hands.
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.UpperBodyFlinchHelper.BodyStiffness">
            <summary>
            Stiffness of body. Value carries over to head look, spine twist.
            </summary>
            <remarks>
            Default value = 11.0f.
            Min value = 6.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.UpperBodyFlinchHelper.BodyDamping">
            <summary>
            Damping value used for upper body.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.UpperBodyFlinchHelper.BackBendAmount">
            <summary>
            Amount to bend the back during the flinch.
            </summary>
            <remarks>
            Default value = -0.6f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.UpperBodyFlinchHelper.UseRightArm">
            <summary>
            Toggle to use the right arm.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.UpperBodyFlinchHelper.UseLeftArm">
            <summary>
            Toggle to Use the Left arm.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.UpperBodyFlinchHelper.NoiseScale">
            <summary>
            Amplitude of the perlin noise applied to the arms positions in the flinch to the front part of the behavior.
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.UpperBodyFlinchHelper.NewHit">
            <summary>
            Relaxes the character for 1 frame if set.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.UpperBodyFlinchHelper.ProtectHeadToggle">
            <summary>
            Always protect head.
            Note if false then character flinches if target is in front, protects head if target is behind.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.UpperBodyFlinchHelper.DontBraceHead">
            <summary>
            Don't protect head only brace from front. Turned on by bcr.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.UpperBodyFlinchHelper.ApplyStiffness">
            <summary>
            Turned of by bcr.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.UpperBodyFlinchHelper.HeadLookAwayFromTarget">
            <summary>
            Look away from target (unless protecting head then look between feet).
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.UpperBodyFlinchHelper.UseHeadLook">
            <summary>
            Use headlook.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.UpperBodyFlinchHelper.TurnTowards">
            <summary>
            Ve balancer turn Towards, negative balancer turn Away, 0 balancer won't turn.
            There is a 50% chance that the character will not turn even if this parameter is set to turn.
            </summary>
            <remarks>
            Default value = 1.
            Min value = -2.
            Max value = 2.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.UpperBodyFlinchHelper.Pos">
            <summary>
            Position in world-space of object to flinch from.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.YankedHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the YankedHelper for sending a Yanked <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the Yanked <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.YankedHelper.ArmStiffness">
            <summary>
            Stiffness of arms when upright.
            </summary>
            <remarks>
            Default value = 11.0f.
            Min value = 6.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.YankedHelper.ArmDamping">
            <summary>
            Sets damping value for the arms when upright.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.YankedHelper.SpineDamping">
            <summary>
            Spine damping when upright.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.YankedHelper.SpineStiffness">
            <summary>
            Spine stiffness when upright.
            </summary>
            <remarks>
            Default value = 10.0f.
            Min value = 6.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.YankedHelper.ArmStiffnessStart">
            <summary>
            Arm stiffness during the yanked timescale i.e. timeAtStartValues.
            </summary>
            <remarks>
            Default value = 3.0f.
            Min value = 0.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.YankedHelper.ArmDampingStart">
            <summary>
```
