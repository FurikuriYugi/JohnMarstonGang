<!-- ScriptHookRDRNetAPI lines 21001-21250 of 30005 -->
```csharp
        <member name="P:RDR2.NaturalMotion.GrabHelper.FromEA">
            <summary>
            Use 2 point.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.GrabHelper.SurfaceGrab">
            <summary>
            Toggle surface grab on. Requires pos1,pos2,pos3 and pos4 to be specified.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.GrabHelper.InstanceIndex">
            <summary>
            LevelIndex of instance to grab (-1 = world coordinates).
            </summary>
            <remarks>
            Default value = -1.
            Min value = -1.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.GrabHelper.InstancePartIndex">
            <summary>
            BoundIndex of part on instance to grab (0 = just use instance coordinates).
            </summary>
            <remarks>
            Default value = 0.
            Min value = 0.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.GrabHelper.DontLetGo">
            <summary>
            Once a constraint is made, keep reaching with whatever hand is allowed - no matter what the angle/distance and whether or not the constraint has broken due to constraintForce  GT  grabStrength.  mmmtodo this is a badly named parameter.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.GrabHelper.BodyStiffness">
            <summary>
            Stiffness of upper body. Scales the arm grab such that the armStiffness is default when this is at default value.
            </summary>
            <remarks>
            Default value = 11.0f.
            Min value = 6.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.GrabHelper.ReachAngle">
            <summary>
            Angle from front at which the grab activates. If the point is outside this angle from front will not try to grab.
            </summary>
            <remarks>
            Default value = 2.8f.
            Min value = 0.0f.
            Max value = 3.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.GrabHelper.OneSideReachAngle">
            <summary>
            Angle at which we will only reach with one hand.
            </summary>
            <remarks>
            Default value = 1.4f.
            Min value = 0.0f.
            Max value = 3.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.GrabHelper.GrabDistance">
            <summary>
            Relative distance at which the grab starts.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 4.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.GrabHelper.Move2Radius">
            <summary>
            Relative distance (additional to grabDistance - doesn't try to move inside grabDistance)at which the grab tries to use the balancer to move to the grab point.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 14.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.GrabHelper.ArmStiffness">
            <summary>
            Stiffness of the arm.
            </summary>
            <remarks>
            Default value = 14.0f.
            Min value = 6.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.GrabHelper.MaxReachDistance">
            <summary>
            Distance to reach out towards the grab point.
            </summary>
            <remarks>
            Default value = 0.7f.
            Min value = 0.0f.
            Max value = 4.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.GrabHelper.OrientationConstraintScale">
            <summary>
            Scale torque used to rotate hands to face normals.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 4.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.GrabHelper.MaxWristAngle">
            <summary>
            When we are grabbing the max angle the wrist ccan be at before we break the grab.
            </summary>
            <remarks>
            Default value = 3.1f.
            Min value = 0.0f.
            Max value = 3.2f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.GrabHelper.UseHeadLookToTarget">
            <summary>
            If true, the character will look at targetForHeadLook after a hand grabs until the end of the behavior. (Before grabbing it looks at the grab target).
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.GrabHelper.LookAtGrab">
            <summary>
            If true, the character will look at the grab.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.GrabHelper.TargetForHeadLook">
            <summary>
            Only used if useHeadLookToTarget is true, the target in world space to look at.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.HeadLookHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the HeadLookHelper for sending a HeadLook <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the HeadLook <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.HeadLookHelper.Damping">
            <summary>
            Damping of the muscles.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 3.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HeadLookHelper.Stiffness">
            <summary>
            Stiffness of the muscles.
            </summary>
            <remarks>
            Default value = 10.0f.
            Min value = 6.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HeadLookHelper.InstanceIndex">
            <summary>
            LevelIndex of object to be looked at. Vel parameters are ignored if this is non -1.
            </summary>
            <remarks>
            Default value = -1.
            Min value = -1.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HeadLookHelper.Vel">
            <summary>
            The velocity of the point being looked at.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            Min value = -100.0f.
            Max value = 100.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HeadLookHelper.Pos">
            <summary>
            The point being looked at.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HeadLookHelper.AlwaysLook">
            <summary>
            Flag to force always to look.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HeadLookHelper.EyesHorizontal">
            <summary>
            Keep the eyes horizontal.  Use true for impact with cars.  Use false if you want better look at target accuracy when the character is on the floor or leaned over alot.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HeadLookHelper.AlwaysEyesHorizontal">
            <summary>
            Keep the eyes horizontal.  Use true for impact with cars.  Use false if you want better look at target accuracy when the character is on the floor or leaned over (when not leaned over the eyes are still kept horizontal if eyesHorizontal=true ) alot.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HeadLookHelper.KeepHeadAwayFromGround">
            <summary>
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HeadLookHelper.TwistSpine">
            <summary>
            Allow head look to twist spine.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.HighFallHelper.#ctor(RDR2.Ped)">
            <summary>
```
