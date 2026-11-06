<!-- ScriptHookRDRNetAPI lines 20501-20750 of 30005 -->
```csharp
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ElectrocuteHelper.UseTorques">
            <summary>
            Use torques to make vibration otherwise use a change in the parts angular velocity.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ElectrocuteHelper.HipType">
            <summary>
            Type of hip reaction 0=none, 1=side2side 2=steplike.
            </summary>
            <remarks>
            Default value = 2.
            Min value = 0.
            Max value = 2.
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.FallOverWallHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the FallOverWallHelper for sending a FallOverWall <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the FallOverWall <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.FallOverWallHelper.BodyStiffness">
            <summary>
            Stiffness of the body, roll up stiffness scales with this and defaults at this default value.
            </summary>
            <remarks>
            Default value = 9.0f.
            Min value = 6.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.FallOverWallHelper.Damping">
            <summary>
            Damping in the effectors.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 3.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.FallOverWallHelper.MagOfForce">
            <summary>
            Magnitude of the falloverWall helper force.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.FallOverWallHelper.MaxDistanceFromPelToHitPoint">
            <summary>
            The maximum distance away from the pelvis that hit points will be registered.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.FallOverWallHelper.MaxForceDist">
            <summary>
            Maximum distance between hitPoint and body part at which forces are applied to part.
            </summary>
            <remarks>
            Default value = 0.8f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.FallOverWallHelper.StepExclusionZone">
            <summary>
            Specifies extent of area in front of the wall in which balancer won't try to take another step.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.FallOverWallHelper.MinLegHeight">
            <summary>
            Minimum height of pelvis above feet at which fallOverWall is attempted.
            </summary>
            <remarks>
            Default value = 0.4f.
            Min value = 0.1f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.FallOverWallHelper.BodyTwist">
            <summary>
            Amount of twist to apply to the spine as the character tries to fling himself over the wall, provides more of a believable roll but increases the amount of lateral space the character needs to successfully flip.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.FallOverWallHelper.MaxTwist">
            <summary>
            Max angle the character can twist before twsit helper torques are turned off.
            </summary>
            <remarks>
            Default value = 3.1f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.FallOverWallHelper.FallOverWallEndA">
            <summary>
            One end of the wall to try to fall over.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.FallOverWallHelper.FallOverWallEndB">
            <summary>
            One end of the wall over which we are trying to fall over.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.FallOverWallHelper.ForceAngleAbort">
            <summary>
            The angle abort threshold.
            </summary>
            <remarks>
            Default value = -0.2f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.FallOverWallHelper.ForceTimeOut">
            <summary>
            The force time out.
            </summary>
            <remarks>
            Default value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.FallOverWallHelper.MoveArms">
            <summary>
            Lift the arms up if true.  Do nothing with the arms if false (eg when using catchfall arms or brace etc).
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.FallOverWallHelper.MoveLegs">
            <summary>
            Move the legs if true.  Do nothing with the legs if false (eg when using dynamicBalancer etc).
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.FallOverWallHelper.BendSpine">
            <summary>
            Bend spine to help falloverwall if true.  Do nothing with the spine if false.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.FallOverWallHelper.AngleDirWithWallNormal">
            <summary>
            Maximum angle in degrees (between the direction of the velocity of the COM and the wall normal) to start to apply forces and torques to fall over the wall.
            </summary>
            <remarks>
            Default value = 180.0f.
            Min value = 0.0f.
            Max value = 180.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.FallOverWallHelper.LeaningAngleThreshold">
            <summary>
            Maximum angle in degrees (between the vertical vector and a vector from pelvis to lower neck) to start to apply forces and torques to fall over the wall.
            </summary>
            <remarks>
            Default value = 180.0f.
            Min value = 0.0f.
            Max value = 180.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.FallOverWallHelper.MaxAngVel">
            <summary>
            If the angular velocity is higher than maxAngVel, the torques and forces are not applied.
            </summary>
            <remarks>
            Default value = 2.0f.
            Min value = -1.0f.
            Max value = 30.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.FallOverWallHelper.AdaptForcesToLowWall">
            <summary>
            Will reduce the magnitude of the forces applied to the character to help him to fall over wall.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.FallOverWallHelper.MaxWallHeight">
            <summary>
            Maximum height (from the lowest foot) to start to apply forces and torques to fall over the wall.
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.0f.
            Max value = 3.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.FallOverWallHelper.DistanceToSendSuccessMessage">
            <summary>
            Minimum distance between the pelvis and the wall to send the success message. If negative doesn't take this parameter into account when sending feedback.
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.0f.
            Max value = 3.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.FallOverWallHelper.RollingBackThr">
            <summary>
            Value of the angular velocity about the wallEgde above which the character is considered as rolling backwards i.e. goes in to fow_RollingBack state.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.FallOverWallHelper.RollingPotential">
            <summary>
            On impact with the wall if the rollingPotential(calculated from the characters linear velocity w.r.t the wall) is greater than this value the character will try to go over the wall otherwise it won't try (fow_Aborted).
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = -1.0f.
```
