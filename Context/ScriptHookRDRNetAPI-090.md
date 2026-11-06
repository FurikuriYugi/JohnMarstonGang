<!-- ScriptHookRDRNetAPI lines 22251-22500 of 30005 -->
```csharp
            Stiffness of arm.
            </summary>
            <remarks>
            Default value = 15.0f.
            Min value = 6.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointArmHelper.ArmDampingRight">
            <summary>
            Damping value for arm used to point.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointArmHelper.InstanceIndexRight">
            <summary>
            Level index of thing to point at, or -1 for none. if -1, target is specified in world space, otherwise it is an offset from the object specified by this index.
            </summary>
            <remarks>
            Default value = -1.
            Min value = -1.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointArmHelper.PointSwingLimitRight">
            <summary>
            Swing limit.
            </summary>
            <remarks>
            Default value = 1.5f.
            Min value = 0.0f.
            Max value = 3.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointArmHelper.UseZeroPoseWhenNotPointingRight">
            <summary>
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.PointGunHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the PointGunHelper for sending a PointGun <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the PointGun <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.EnableRight">
            <summary>
            Allow right hand to point/support?.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.EnableLeft">
            <summary>
            Allow right hand to point/support?.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.LeftHandTarget">
            <summary>
            Target for the left Hand.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.LeftHandTargetIndex">
            <summary>
            Index of the object that the left hand target is specified in, -1 is world space.
            </summary>
            <remarks>
            Default value = -1.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.RightHandTarget">
            <summary>
            Target for the right Hand.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.RightHandTargetIndex">
            <summary>
            Index of the object that the right hand target is specified in, -1 is world space.
            </summary>
            <remarks>
            Default value = -1.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.LeadTarget">
            <summary>
            NB: Only Applied to single handed weapons (some more work is required to have this tech on two handed weapons). Amount to lead target based on target velocity relative to the chest.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.ArmStiffness">
            <summary>
            Stiffness of the arm.
            </summary>
            <remarks>
            Default value = 14.0f.
            Min value = 2.0f.
            Max value = 15.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.ArmStiffnessDetSupport">
            <summary>
            Stiffness of the arm on pointing arm when a support arm is detached from a two-handed weapon.
            </summary>
            <remarks>
            Default value = 8.0f.
            Min value = 2.0f.
            Max value = 15.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.ArmDamping">
            <summary>
            Damping.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.1f.
            Max value = 5.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.GravityOpposition">
            <summary>
            Amount of gravity opposition on pointing arm.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.GravOppDetachedSupport">
            <summary>
            Amount of gravity opposition on pointing arm when a support arm is detached from a two-handed weapon.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.MassMultDetachedSupport">
            <summary>
            Amount of mass of weapon taken into account by gravity opposition on pointing arm when a support arm is detached from a two-handed weapon.  The lower the value the more the character doesn't know about the weapon mass and therefore is more affected by it.
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.AllowShotLooseness">
            <summary>
            Allow shot to set a lower arm muscleStiffness than pointGun normally would.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.ClavicleBlend">
            <summary>
            How much of blend should come from incoming transforms 0(all IK) .. 1(all ITMs)   For pointing arms only.  (Support arm uses the IK solution as is for clavicles).
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.ElbowAttitude">
            <summary>
            Controls arm twist. (except in pistolIK).
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.SupportConstraint">
            <summary>
            Type of constraint between the support hand and gun.  0=no constraint, 1=hard distance constraint, 2=Force based constraint, 3=hard spherical constraint.
            </summary>
            <remarks>
            Default value = 1.
            Min value = 0.
            Max value = 3.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.ConstraintMinDistance">
            <summary>
            For supportConstraint = 1: Support hand constraint distance will be slowly reduced until it hits this value.  This is for stability and also allows the pointing arm to lead a little.  Don't set lower than NM_MIN_STABLE_DISTANCECONSTRAINT_DISTANCE 0.001f.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 0.1f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.MakeConstraintDistance">
            <summary>
            For supportConstraint = 1:  Minimum distance within which support hand constraint will be made.
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.0f.
            Max value = 3.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.ReduceConstraintLengthVel">
            <summary>
            For supportConstraint = 1:  Velocity at which to reduce the support hand constraint length.
            </summary>
            <remarks>
            Default value = 1.5f.
            Min value = 0.1f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.BreakingStrength">
            <summary>
            For supportConstraint = 1: strength of the supporting hands constraint (kg m/s), -1 to ignore/disable.
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.0f.
            Max value = 1000.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.BrokenSupportTime">
            <summary>
            Once constraint is broken then do not try to reconnect/support for this amount of time.
            </summary>
```
