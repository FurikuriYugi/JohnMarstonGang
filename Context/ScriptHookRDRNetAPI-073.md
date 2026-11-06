<!-- ScriptHookRDRNetAPI lines 18001-18250 of 30005 -->
```csharp
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillHelper.ElbowStiffness">
            <summary>
            Stiffness applied to the elbows.
            </summary>
            <remarks>
            Default value = 12.0f.
            Min value = 1.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillHelper.ElbowDamping">
            <summary>
            Damping applied to the elbows.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillHelper.LeftElbowMin">
            <summary>
            Minimum left elbow bend.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 1.7f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillHelper.RightElbowMin">
            <summary>
            Minimum right elbow bend.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 1.7f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillHelper.PhaseOffset">
            <summary>
            Phase offset(degrees) when phase synchronization is turned on.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = -360.0f.
            Max value = 360.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillHelper.DragReduction">
            <summary>
            How much to compensate for movement of character/target.
            </summary>
            <remarks>
            Default value = 0.2f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillHelper.IKtwist">
            <summary>
            Angle of elbow around twist axis ?.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = -3.1f.
            Max value = 3.1f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillHelper.AngVelThreshold">
            <summary>
            Value of character angular speed above which adaptive arm motion starts.
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillHelper.AngVelGain">
            <summary>
            Multiplies angular speed of character to get speed of arms.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillHelper.MirrorMode">
            <summary>
            </summary>
            <remarks>
            Default value = <see cref="F:RDR2.NaturalMotion.MirrorMode.Mirrored"/>.
            If <see cref="F:RDR2.NaturalMotion.MirrorMode.Parallel"/> leftArm parameters are used.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillHelper.AdaptiveMode">
            <summary>
            </summary>
            <remarks>
            Default value = <see cref="F:RDR2.NaturalMotion.AdaptiveMode.NotAdaptive"/>.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillHelper.ForceSync">
            <summary>
            Toggles phase synchronization.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillHelper.UseLeft">
            <summary>
            Use the left arm.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillHelper.UseRight">
            <summary>
            Use the right arm.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillHelper.DisableOnImpact">
            <summary>
            If true, each arm will stop windmilling if it hits the ground.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.ArmsWindmillAdaptiveHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the ArmsWindmillAdaptiveHelper for sending a ArmsWindmillAdaptive <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the ArmsWindmillAdaptive <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillAdaptiveHelper.AngSpeed">
            <summary>
            Controls the speed of the windmilling.
            </summary>
            <remarks>
            Default value = 6.3f.
            Min value = 0.1f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillAdaptiveHelper.BodyStiffness">
            <summary>
            Controls how stiff the rest of the body is.
            </summary>
            <remarks>
            Default value = 11.0f.
            Min value = 6.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillAdaptiveHelper.Amplitude">
            <summary>
            Controls how large the motion is, higher values means the character waves his arms in a massive arc.
            </summary>
            <remarks>
            Default value = 0.6f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillAdaptiveHelper.Phase">
            <summary>
            Set to a non-zero value to desynchronize the left and right arms motion.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = -4.0f.
            Max value = 8.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillAdaptiveHelper.ArmStiffness">
            <summary>
            How stiff the arms are controls how pronounced the windmilling motion appears.
            </summary>
            <remarks>
            Default value = 14.1f.
            Min value = 6.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillAdaptiveHelper.LeftElbowAngle">
            <summary>
            If not negative then left arm will blend to this angle.
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.0f.
            Max value = 6.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillAdaptiveHelper.RightElbowAngle">
            <summary>
            If not negative then right arm will blend to this angle.
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.0f.
            Max value = 6.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillAdaptiveHelper.Lean1mult">
            <summary>
            0 arms go up and down at the side. 1 circles. 0..1 elipse.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillAdaptiveHelper.Lean1offset">
            <summary>
            0.f center of circle at side.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = -6.0f.
            Max value = 6.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillAdaptiveHelper.ElbowRate">
            <summary>
            Rate at which elbow tries to match *ElbowAngle.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 6.0f.
            </remarks>
        </member>
```
