<!-- ScriptHookRDRNetAPI lines 24751-25000 of 30005 -->
```csharp
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHeadLookHelper.HeadLook">
            <summary>
            Position to look at with headlook flag.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHeadLookHelper.HeadLookAtWoundMinTimer">
            <summary>
            Min time to look at wound.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHeadLookHelper.HeadLookAtWoundMaxTimer">
            <summary>
            Max time to look at wound.
            </summary>
            <remarks>
            Default value = 0.8f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHeadLookHelper.HeadLookAtHeadPosMaxTimer">
            <summary>
            Min time to look headLook or if zero - forward or in velocity direction.
            </summary>
            <remarks>
            Default value = 1.7f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHeadLookHelper.HeadLookAtHeadPosMinTimer">
            <summary>
            Max time to look headLook or if zero - forward or in velocity direction.
            </summary>
            <remarks>
            Default value = 0.6f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="T:RDR2.NaturalMotion.ShotConfigureArmsHelper">
            <summary>
            Configure the arm reactions in shot.
            </summary>
        </member>
        <member name="M:RDR2.NaturalMotion.ShotConfigureArmsHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the ShotConfigureArmsHelper for sending a ShotConfigureArms <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the ShotConfigureArms <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
            <remarks>
            Configure the arm reactions in shot.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotConfigureArmsHelper.Brace">
            <summary>
            Blind brace with arms if appropriate.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotConfigureArmsHelper.PointGun">
            <summary>
            Point gun if appropriate.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotConfigureArmsHelper.UseArmsWindmill">
            <summary>
            ArmsWindmill if going backwards fast enough.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotConfigureArmsHelper.ReleaseWound">
            <summary>
            Release wound if going sideways/forward fast enough.
            0 = don't.
            1 = only if bracing.
            2 = any default arm reaction.
            </summary>
            <remarks>
            Default value = 1.
            Min value = 0.
            Max value = 2.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotConfigureArmsHelper.ReachFalling">
            <summary>
            Reach for wound when falling.
            0 = false,
            1 = true,
            2 = once per shot performance.
            </summary>
            <remarks>
            Default value = 0.
            Min value = 0.
            Max value = 2.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotConfigureArmsHelper.ReachFallingWithOneHand">
            <summary>
            Force character to reach for wound with only one hand when falling or fallen.
            0 = allow two-handed reach,
            1 = left only if two-handed possible,
            2 = right only if two-handed possible,
            3 = one handed but automatic (allows switching of hands).
            </summary>
            <remarks>
            Default value = 3.
            Min value = 0.
            Max value = 3.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotConfigureArmsHelper.ReachOnFloor">
            <summary>
            ReachForWound when on floor - 0 = false, 1 = true, 2 = once per shot performance.
            </summary>
            <remarks>
            Default value = 0.
            Min value = 0.
            Max value = 2.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotConfigureArmsHelper.AlwaysReachTime">
            <summary>
            Inhibit arms brace for this amount of time after reachForWound has begun.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotConfigureArmsHelper.AWSpeedMult">
            <summary>
            For armsWindmill, multiplier on character speed - increase of speed of circling is proportional to character speed (max speed of circliing increase = 1.5). Eg. lowering the value increases the range of velocity that the 0-1.5 is applied over.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotConfigureArmsHelper.AWRadiusMult">
            <summary>
            For armsWindmill, multiplier on character speed - increase of radii is proportional to character speed (max radius increase = 0.45).
            E.g. lowering the value increases the range of velocity that the 0-0.45 is applied over.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotConfigureArmsHelper.AWStiffnessAdd">
            <summary>
            For armsWindmill, added arm stiffness ranges from 0 to AWStiffnessAdd.
            </summary>
            <remarks>
            Default value = 4.0f.
            Min value = 0.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotConfigureArmsHelper.ReachWithOneHand">
            <summary>
            Force character to reach for wound with only one hand.
            0 = allow two-handed reach,
            1 = left only if two-handed possible,
            2 = right only if two-handed possible.
            </summary>
            <remarks>
            Default value = 0.
            Min value = 0.
            Max value = 2.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotConfigureArmsHelper.AllowLeftPistolRFW">
            <summary>
            Allow character to reach for wound with left hand if holding a pistol.
            It never will for a rifle.
            If pointGun is running this will only happen if the hand cannot point and pointGun:poseUnusedGunArm = false.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotConfigureArmsHelper.AllowRightPistolRFW">
            <summary>
            Allow character to reach for wound with right hand if holding a pistol.
            It never will for a rifle.
            If pointGun is running this will only happen if the hand cannot point and pointGun:poseUnusedGunArm = false.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotConfigureArmsHelper.RfwWithPistol">
            <summary>
            Override pointGun and reachForWound if desired if holding a pistol.
            It never will for a rifle.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotConfigureArmsHelper.Fling2">
            <summary>
            Type of reaction.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotConfigureArmsHelper.Fling2Left">
            <summary>
            Fling the left arm.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotConfigureArmsHelper.Fling2Right">
            <summary>
            Fling the right arm.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotConfigureArmsHelper.Fling2OverrideStagger">
            <summary>
            Override stagger arms even if staggerFall:m_upperBodyReaction = true.
            </summary>
            <remarks>
```
