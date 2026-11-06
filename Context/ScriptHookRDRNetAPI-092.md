<!-- ScriptHookRDRNetAPI lines 22751-23000 of 30005 -->
```csharp
            Enable new ik for pistol pointing.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.UseSpineTwist">
            <summary>
            Use spine twist to orient chest?.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.UseTurnToTarget">
            <summary>
            Turn balancer to help gun point at target.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.UseHeadLook">
            <summary>
            Use head look to drive head?.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.ErrorThreshold">
            <summary>
            Angular difference between pointing direction and target direction above which feedback will be generated.
            </summary>
            <remarks>
            Default value = 0.4f.
            Min value = 0.0f.
            Max value = 3.1f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.FireWeaponRelaxTime">
            <summary>
            Duration of arms relax following firing weapon.  NB:This is clamped (0,5) in pointGun.
            </summary>
            <remarks>
            Default value = 0.4f.
            Min value = 0.0f.
            Max value = 5.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.FireWeaponRelaxAmount">
            <summary>
            Relax multiplier following firing weapon. Recovers over relaxTime.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.1f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.FireWeaponRelaxDistance">
            <summary>
            Range of motion for ik-based recoil.
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.0f.
            Max value = 0.3f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.UseIncomingTransforms">
            <summary>
            Use the incoming transforms to inform the pointGun of the primaryWeaponDistance, poleVector for the arm.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.MeasureParentOffset">
            <summary>
            If useIncomingTransforms = true and measureParentOffset=true then measure the Pointing-from offset from parent effector, using itms - this should point the barrel of the gun to the target.  This is added to the rightHandParentOffset. NB NOT used if rightHandParentEffector LT 0.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.LeftHandParentOffset">
            <summary>
            Pointing-from offset from parent effector, expressed in spine3's frame, x = back/forward, y = right/left, z = up/down.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.LeftHandParentEffector">
            <summary>
            1 = Use leftShoulder. Effector from which the left hand pointing originates. Ie, point from this part to the target. -1 causes default offset for active weapon mode to be applied.
            </summary>
            <remarks>
            Default value = -1.
            Min value = -1.
            Max value = 21.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.RightHandParentOffset">
            <summary>
            Pointing-from offset from parent effector, expressed in spine3's frame, x = back/forward, y = right/left, z = up/down. This is added to the measured one if useIncomingTransforms=true and measureParentOffset=true.  NB NOT used if rightHandParentEffector LT 0.  Pistol(0,0,0) Rifle(0.0032, 0.0, -0.0).
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.RightHandParentEffector">
            <summary>
            1 = Use rightShoulder.. Effector from which the right hand pointing originates. Ie, point from this part to the target. -1 causes default offset for active weapon mode to be applied.
            </summary>
            <remarks>
            Default value = -1.
            Min value = -1.
            Max value = 21.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.PrimaryHandWeaponDistance">
            <summary>
            Distance from the shoulder to hold the weapon. If -1 and useIncomingTransforms then weaponDistance is read from ITMs. WeaponDistance=primaryHandWeaponDistance clamped [0.2f:m_maxArmReach=0.65] if useIncomingTransforms = false. pistol 0.60383, rifle 0.336.
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.ConstrainRifle">
            <summary>
            Use hard constraint to keep rifle stock against shoulder?.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.RifleConstraintMinDistance">
            <summary>
            Rifle constraint distance. Deliberately kept large to create a flat constraint surface where rifle meets the shoulder.
            </summary>
            <remarks>
            Default value = 0.2f.
            Min value = 0.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.DisableArmCollisions">
            <summary>
            Disable collisions between right hand/forearm and the torso/legs.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.DisableRifleCollisions">
            <summary>
            Disable collisions between right hand/forearm and spine3/spine2 if in rifle mode.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="T:RDR2.NaturalMotion.PointGunExtraHelper">
            <summary>
            Seldom set parameters for pointGun - just to keep number of parameters in any message less than or equal to 64.
            </summary>
        </member>
        <member name="M:RDR2.NaturalMotion.PointGunExtraHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the PointGunExtraHelper for sending a PointGunExtra <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the PointGunExtra <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
            <remarks>
            Seldom set parameters for pointGun - just to keep number of parameters in any message less than or equal to 64.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunExtraHelper.ConstraintStrength">
            <summary>
            For supportConstraint = 2: force constraint strength of the supporting hands - it gets shaky at about 4.0.
            </summary>
            <remarks>
            Default value = 2.0f.
            Min value = 0.0f.
            Max value = 5.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunExtraHelper.ConstraintThresh">
            <summary>
            For supportConstraint = 2:  Like makeConstraintDistance. Force starts acting when the hands are  LT  3.0*thresh apart but is maximum strength  LT  thresh. For comparison: 0.1 is used for reachForWound in shot, 0.25 is used in grab.
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunExtraHelper.WeaponMask">
            <summary>
            Currently unused - no intoWorldTest. RAGE bit mask to exclude weapons from ray probe - currently defaults to MP3 weapon flag.
            </summary>
            <remarks>
            Default value = 1024.
            Min value = 0.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunExtraHelper.TimeWarpActive">
            <summary>
            Is timeWarpActive enabled?.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunExtraHelper.TimeWarpStrengthScale">
            <summary>
            Scale for arm and helper strength when timewarp is enabled. 1 = normal compensation.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.1f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunExtraHelper.OriStiff">
            <summary>
            Hand stabilization controller stiffness.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 100.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunExtraHelper.OriDamp">
            <summary>
            Hand stabilization controller damping.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunExtraHelper.PosStiff">
            <summary>
            Hand stabilization controller stiffness.
            </summary>
```
