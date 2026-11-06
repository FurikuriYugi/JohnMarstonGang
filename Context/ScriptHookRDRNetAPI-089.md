<!-- ScriptHookRDRNetAPI lines 22001-22250 of 30005 -->
```csharp
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PedalLegsHelper.SpeedAsymmetry">
            <summary>
            Random offset applied per leg to the angular speed to desynchronize the pedaling - set to 0 to disable, otherwise should be set to less than the angularSpeed value.
            </summary>
            <remarks>
            Default value = 8.0f.
            Min value = -10.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PedalLegsHelper.AdaptivePedal4Dragging">
            <summary>
            Will pedal in the direction of travel (if backPedal = false, against travel if backPedal = true) and with an angular velocity relative to speed upto a maximum of 13(rads/sec).  Use when being dragged by a car.  Overrides angularSpeed.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PedalLegsHelper.AngSpeedMultiplier4Dragging">
            <summary>
            NewAngularSpeed = Clamp(angSpeedMultiplier4Dragging * linear_speed/pedalRadius, 0.0, angularSpeed).
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 100.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PedalLegsHelper.RadiusVariance">
            <summary>
            0-1 value used to add variance to the radius value while pedalling, to desynchonize the legs' movement and provide some variety.
            </summary>
            <remarks>
            Default value = 0.4f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PedalLegsHelper.LegAngleVariance">
            <summary>
            0-1 value used to vary the angle of the legs from the hips during the pedal.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PedalLegsHelper.CentreSideways">
            <summary>
            Move the center of the pedal for both legs sideways (+ve = right).  NB: not applied to hula.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PedalLegsHelper.CentreForwards">
            <summary>
            Move the center of the pedal for both legs forward (or backward -ve).
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PedalLegsHelper.CentreUp">
            <summary>
            Move the center of the pedal for both legs up (or down -ve).
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PedalLegsHelper.Ellipse">
            <summary>
            Turn the circle into an ellipse.  Ellipse has horizontal radius a and vertical radius b.  If ellipse is +ve then a=radius*ellipse and b=radius.  If ellipse is -ve then a=radius and b = radius*ellipse.  0.0 = vertical line of length 2*radius, 0.0:1.0 circle squashed horizontally (vertical radius = radius), 1.0=circle.  -0.001 = horizontal line of length 2*radius, -0.0:-1.0 circle squashed vertically (horizontal radius = radius), -1.0 = circle.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PedalLegsHelper.DragReduction">
            <summary>
            How much to account for the target moving through space rather than being static.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PedalLegsHelper.Spread">
            <summary>
            Spread legs.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PedalLegsHelper.Hula">
            <summary>
            If true circle the legs in a hula motion.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="T:RDR2.NaturalMotion.PointArmHelper">
            <summary>
            BEHAVIOURS REFERENCED: AnimPose - allows animPose to override body parts: Arms (useLeftArm, useRightArm).
            </summary>
        </member>
        <member name="M:RDR2.NaturalMotion.PointArmHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the PointArmHelper for sending a PointArm <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the PointArm <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
            <remarks>
            BEHAVIOURS REFERENCED: AnimPose - allows animPose to override body parts: Arms (useLeftArm, useRightArm).
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointArmHelper.TargetLeft">
            <summary>
            Point to point to (in world space).
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointArmHelper.TwistLeft">
            <summary>
            Twist of the arm around point direction.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointArmHelper.ArmStraightnessLeft">
            <summary>
            Values less than 1 can give the arm a more bent look.
            </summary>
            <remarks>
            Default value = 0.8f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointArmHelper.UseLeftArm">
            <summary>
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointArmHelper.ArmStiffnessLeft">
            <summary>
            Stiffness of arm.
            </summary>
            <remarks>
            Default value = 15.0f.
            Min value = 6.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointArmHelper.ArmDampingLeft">
            <summary>
            Damping value for arm used to point.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointArmHelper.InstanceIndexLeft">
            <summary>
            Level index of thing to point at, or -1 for none. if -1, target is specified in world space, otherwise it is an offset from the object specified by this index.
            </summary>
            <remarks>
            Default value = -1.
            Min value = -1.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointArmHelper.PointSwingLimitLeft">
            <summary>
            Swing limit.
            </summary>
            <remarks>
            Default value = 1.5f.
            Min value = 0.0f.
            Max value = 3.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointArmHelper.UseZeroPoseWhenNotPointingLeft">
            <summary>
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointArmHelper.TargetRight">
            <summary>
            Point to point to (in world space).
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointArmHelper.TwistRight">
            <summary>
            Twist of the arm around point direction.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointArmHelper.ArmStraightnessRight">
            <summary>
            Values less than 1 can give the arm a more bent look.
            </summary>
            <remarks>
            Default value = 0.8f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointArmHelper.UseRightArm">
            <summary>
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointArmHelper.ArmStiffnessRight">
            <summary>
```
