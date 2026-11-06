<!-- ScriptHookRDRNetAPI lines 17501-17750 of 30005 -->
```csharp
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetFrictionScaleHelper.GlobalMin">
            <summary>
            Character-wide minimum impact friction. Affects all parts (not just those in mask).
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1000000.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetFrictionScaleHelper.GlobalMax">
            <summary>
            Character-wide maximum impact friction. Affects all parts (not just those in mask).
            </summary>
            <remarks>
            Default value = 999999.0f.
            Min value = 0.0f.
            Max value = 1000000.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetFrictionScaleHelper.Mask">
            <summary>
            Two character body-masking value, bitwise joint mask or bitwise logic string of two character body-masking value  (see Active Pose notes for possible values).
            </summary>
            <remarks>
            Default value = "fb".
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.AnimPoseHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the AnimPoseHelper for sending a AnimPose <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the AnimPose <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.AnimPoseHelper.MuscleStiffness">
            <summary>
            Muscle stiffness of masked joints. -values mean don't apply (just use defaults or ones applied by behaviors - safer if you are going to return to a behavior).
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.1f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.AnimPoseHelper.Stiffness">
            <summary>
            Stiffness of masked joints. -ve values mean don't apply stiffness or damping (just use defaults or ones applied by behaviors).  If you are using animpose fullbody on its own then this gives the opprtunity to use setStffness and setMuscle stiffness messages to set up the character's muscles. Mmmmtodo get rid of this -ve.
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.1f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.AnimPoseHelper.Damping">
            <summary>
            Damping of masked joints.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.AnimPoseHelper.EffectorMask">
            <summary>
            Two character body-masking value, bitwise joint mask or bitwise logic string of two character body-masking value  (see notes for explanation).
            </summary>
            <remarks>
            Default value = "ub".
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.AnimPoseHelper.OverideHeadlook">
            <summary>
            Overide Headlook behavior (if animPose includes the head).
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.AnimPoseHelper.OveridePointArm">
            <summary>
            Overide PointArm behavior (if animPose includes the arm/arms).
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.AnimPoseHelper.OveridePointGun">
            <summary>
            Overide PointGun behavior (if animPose includes the arm/arms)//mmmmtodo not used at moment.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.AnimPoseHelper.UseZMPGravityCompensation">
            <summary>
            If true then modify gravity compensation based on stance (can reduce gravity compensation to zero if cofm is outside of balance area).
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.AnimPoseHelper.GravityCompensation">
            <summary>
            Gravity compensation applied to joints in the effectorMask. If -ve then not applied (use current setting).
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.0f.
            Max value = 14.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.AnimPoseHelper.MuscleStiffnessLeftArm">
            <summary>
            Muscle stiffness applied to left arm (applied after stiffness). If -ve then not applied (use current setting).
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.AnimPoseHelper.MuscleStiffnessRightArm">
            <summary>
            Muscle stiffness applied to right arm (applied after stiffness). If -ve then not applied (use current setting).
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.AnimPoseHelper.MuscleStiffnessSpine">
            <summary>
            Muscle stiffness applied to spine (applied after stiffness). If -ve then not applied (use current setting).
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.AnimPoseHelper.MuscleStiffnessLeftLeg">
            <summary>
            Muscle stiffness applied to left leg (applied after stiffness). If -ve then not applied (use current setting).
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.AnimPoseHelper.MuscleStiffnessRightLeg">
            <summary>
            Muscle stiffness applied to right leg (applied after stiffness). If -ve then not applied (use current setting).
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.AnimPoseHelper.StiffnessLeftArm">
            <summary>
            Stiffness  applied to left arm (applied after stiffness). If -ve then not applied (use current setting).
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.AnimPoseHelper.StiffnessRightArm">
            <summary>
            Stiffness applied to right arm (applied after stiffness). If -ve then not applied (use current setting).
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.AnimPoseHelper.StiffnessSpine">
            <summary>
            Stiffness applied to spine (applied after stiffness). If -ve then not applied (use current setting).
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.AnimPoseHelper.StiffnessLeftLeg">
            <summary>
            Stiffness applied to left leg (applied after stiffness). If -ve then not applied (use current setting).
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.AnimPoseHelper.StiffnessRightLeg">
            <summary>
            Stiffness applied to right leg (applied after stiffness). If -ve then not applied (use current setting).
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.AnimPoseHelper.DampingLeftArm">
            <summary>
            Damping applied to left arm (applied after stiffness). If stiffness -ve then not applied (use current setting).
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.AnimPoseHelper.DampingRightArm">
            <summary>
            Damping applied to right arm (applied after stiffness). If stiffness -ve then not applied (use current setting).
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.AnimPoseHelper.DampingSpine">
            <summary>
            Damping applied to spine (applied after stiffness). If stiffness-ve then not applied (use current setting).
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.AnimPoseHelper.DampingLeftLeg">
            <summary>
            Damping applied to left leg (applied after stiffness). If stiffness-ve then not applied (use current setting).
            </summary>
```
