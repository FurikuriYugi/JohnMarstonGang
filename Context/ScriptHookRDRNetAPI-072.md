<!-- ScriptHookRDRNetAPI lines 17751-18000 of 30005 -->
```csharp
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.AnimPoseHelper.DampingRightLeg">
            <summary>
            Damping applied to right leg (applied after stiffness). If stiffness -ve then not applied (use current setting).
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.AnimPoseHelper.GravCompLeftArm">
            <summary>
            Gravity compensation applied to left arm (applied after gravityCompensation). If -ve then not applied (use current setting).
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.0f.
            Max value = 14.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.AnimPoseHelper.GravCompRightArm">
            <summary>
            Gravity compensation applied to right arm (applied after gravityCompensation). If -ve then not applied (use current setting).
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.0f.
            Max value = 14.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.AnimPoseHelper.GravCompSpine">
            <summary>
            Gravity compensation applied to spine (applied after gravityCompensation). If -ve then not applied (use current setting).
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.0f.
            Max value = 14.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.AnimPoseHelper.GravCompLeftLeg">
            <summary>
            Gravity compensation applied to left leg (applied after gravityCompensation). If -ve then not applied (use current setting).
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.0f.
            Max value = 14.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.AnimPoseHelper.GravCompRightLeg">
            <summary>
            Gravity compensation applied to right leg (applied after gravityCompensation). If -ve then not applied (use current setting).
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.0f.
            Max value = 14.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.AnimPoseHelper.ConnectedLeftHand">
            <summary>
            Is the left hand constrained to the world/ an object: -1=auto decide by impact info, 0=no, 1=part fully constrained (not implemented:, 2=part point constraint, 3=line constraint).
            </summary>
            <remarks>
            Default value = 0.
            Min value = -1.
            Max value = 2.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.AnimPoseHelper.ConnectedRightHand">
            <summary>
            Is the right hand constrained to the world/ an object: -1=auto decide by impact info, 0=no, 1=part fully constrained (not implemented:, 2=part point constraint, 3=line constraint).
            </summary>
            <remarks>
            Default value = 0.
            Min value = -1.
            Max value = 2.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.AnimPoseHelper.ConnectedLeftFoot">
            <summary>
            Is the left foot constrained to the world/ an object: -2=do not set in animpose (e.g. let the balancer decide), -1=auto decide by impact info, 0=no, 1=part fully constrained (not implemented:, 2=part point constraint, 3=line constraint).
            </summary>
            <remarks>
            Default value = -2.
            Min value = -2.
            Max value = 2.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.AnimPoseHelper.ConnectedRightFoot">
            <summary>
            Is the right foot constrained to the world/ an object: -2=do not set in animpose (e.g. let the balancer decide),-1=auto decide by impact info, 0=no, 1=part fully constrained (not implemented:, 2=part point constraint, 3=line constraint).
            </summary>
            <remarks>
            Default value = -2.
            Min value = -2.
            Max value = 2.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.AnimPoseHelper.AnimSource">
            <summary>
            </summary>
        </member>
        <member name="P:RDR2.NaturalMotion.AnimPoseHelper.DampenSideMotionInstanceIndex">
            <summary>
            LevelIndex of object to dampen side motion relative to. -1 means not used.
            </summary>
            <remarks>
            Default value = -1.
            Min value = -1.
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.ArmsWindmillHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the ArmsWindmillHelper for sending a ArmsWindmill <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the ArmsWindmill <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillHelper.LeftPartID">
            <summary>
            ID of part that the circle uses as local space for positioning.
            </summary>
            <remarks>
            Default value = 10.
            Min value = 0.
            Max value = 21.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillHelper.LeftRadius1">
            <summary>
            Radius for first axis of ellipse.
            </summary>
            <remarks>
            Default value = 0.8f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillHelper.LeftRadius2">
            <summary>
            Radius for second axis of ellipse.
            </summary>
            <remarks>
            Default value = 0.8f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillHelper.LeftSpeed">
            <summary>
            Speed of target around the circle.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = -2.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillHelper.LeftNormal">
            <summary>
            Euler Angles orientation of circle in space of part with part ID.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.2f, 0.2f).
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillHelper.LeftCentre">
            <summary>
            Centre of circle in the space of partID.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.5f, -0.1f).
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillHelper.RightPartID">
            <summary>
            ID of part that the circle uses as local space for positioning.
            </summary>
            <remarks>
            Default value = 10.
            Min value = 0.
            Max value = 21.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillHelper.RightRadius1">
            <summary>
            Radius for first axis of ellipse.
            </summary>
            <remarks>
            Default value = 0.8f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillHelper.RightRadius2">
            <summary>
            Radius for second axis of ellipse.
            </summary>
            <remarks>
            Default value = 0.8f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillHelper.RightSpeed">
            <summary>
            Speed of target around the circle.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = -2.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillHelper.RightNormal">
            <summary>
            Euler Angles orientation of circle in space of part with part ID.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, -0.2f, -0.2f).
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillHelper.RightCentre">
            <summary>
            Centre of circle in the space of partID.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, -0.5f, -0.1f).
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillHelper.ShoulderStiffness">
            <summary>
            Stiffness applied to the shoulders.
            </summary>
            <remarks>
            Default value = 12.0f.
            Min value = 1.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ArmsWindmillHelper.ShoulderDamping">
            <summary>
            Damping applied to the shoulders.
```
