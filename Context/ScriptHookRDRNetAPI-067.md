<!-- ScriptHookRDRNetAPI lines 16501-16750 of 30005 -->
```csharp
        <member name="P:RDR2.NaturalMotion.RegisterWeaponHelper.GunToHandA">
            <summary>
            A vector of the gunToHand matrix.  The gunToHandMatrix is the desired gunToHandMatrix in the aimingPose. (The gunToHandMatrix when pointGun starts can be different so will be blended to this desired one).
            </summary>
            <remarks>
            Default value = Vector3(1.0f, 0.0f, 0.0f).
            Min value = 0.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.RegisterWeaponHelper.GunToHandB">
            <summary>
            B vector of the gunToHand matrix.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 1.0f, 0.0f).
            Min value = 0.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.RegisterWeaponHelper.GunToHandC">
            <summary>
            C vector of the gunToHand matrix.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 1.0f).
            Min value = 0.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.RegisterWeaponHelper.GunToHandD">
            <summary>
            D vector of the gunToHand matrix.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            Min value = 0.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.RegisterWeaponHelper.GunToMuzzleInGun">
            <summary>
            Gun center to muzzle expressed in gun co-ordinates.  To get the line of sight/barrel of the gun. Assumption: the muzzle direction is always along the same primary axis of the gun.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.RegisterWeaponHelper.GunToButtInGun">
            <summary>
            Gun center to butt expressed in gun co-ordinates.  The gun pivots around this point when aiming.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.ShotRelaxHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the ShotRelaxHelper for sending a ShotRelax <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the ShotRelax <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotRelaxHelper.RelaxPeriodUpper">
            <summary>
            Time over which to relax to full relaxation for upper body.
            </summary>
            <remarks>
            Default value = 2.0f.
            Min value = 0.0f.
            Max value = 40.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotRelaxHelper.RelaxPeriodLower">
            <summary>
            Time over which to relax to full relaxation for lower body.
            </summary>
            <remarks>
            Default value = 0.4f.
            Min value = 0.0f.
            Max value = 40.0f.
            </remarks>
        </member>
        <member name="T:RDR2.NaturalMotion.FireWeaponHelper">
            <summary>
            One shot message apply a force to the hand as we fire the gun that should be in this hand.
            </summary>
        </member>
        <member name="M:RDR2.NaturalMotion.FireWeaponHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the FireWeaponHelper for sending a FireWeapon <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the FireWeapon <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
            <remarks>
            One shot message apply a force to the hand as we fire the gun that should be in this hand.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.FireWeaponHelper.FiredWeaponStrength">
            <summary>
            The force of the gun.
            </summary>
            <remarks>
            Default value = 1000.0f.
            Min value = 0.0f.
            Max value = 10000.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.FireWeaponHelper.GunHandEnum">
            <summary>
            Which hand is the gun in.
            </summary>
            <remarks>
            Default value = <see cref="F:RDR2.NaturalMotion.Hand.Left"/>.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.FireWeaponHelper.ApplyFireGunForceAtClavicle">
            <summary>
            Should we apply some of the force at the shoulder. Force double handed weapons (Ak47 etc).
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.FireWeaponHelper.InhibitTime">
            <summary>
            Minimum time before next fire impulse.
            </summary>
            <remarks>
            Default value = 0.4f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.FireWeaponHelper.Direction">
            <summary>
            Direction of impulse in gun frame.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.FireWeaponHelper.Split">
            <summary>
            Split force between hand and clavicle when applyFireGunForceAtClavicle is true. 1 = all hand, 0 = all clavicle.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="T:RDR2.NaturalMotion.ConfigureConstraintsHelper">
            <summary>
            One shot to give state of constraints on character and response to constraints.
            </summary>
        </member>
        <member name="M:RDR2.NaturalMotion.ConfigureConstraintsHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the ConfigureConstraintsHelper for sending a ConfigureConstraints <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the ConfigureConstraints <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
            <remarks>
            One shot to give state of constraints on character and response to constraints.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureConstraintsHelper.HandCuffs">
            <summary>
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureConstraintsHelper.HandCuffsBehindBack">
            <summary>
            Not implemented.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureConstraintsHelper.LegCuffs">
            <summary>
            Not implemented.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureConstraintsHelper.RightDominant">
            <summary>
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureConstraintsHelper.PassiveMode">
            <summary>
            0 setCurrent, 1= IK to dominant, (2=pointGunLikeIK //not implemented).
            </summary>
            <remarks>
            Default value = 0.
            Min value = 0.
            Max value = 5.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureConstraintsHelper.BespokeBehavior">
            <summary>
            Not implemented.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureConstraintsHelper.Blend2ZeroPose">
            <summary>
            Blend Arms to zero pose.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.StayUprightHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the StayUprightHelper for sending a StayUpright <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the StayUpright <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.StayUprightHelper.UseForces">
            <summary>
            Enable force based constraint.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StayUprightHelper.UseTorques">
            <summary>
            Enable torque based constraint.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StayUprightHelper.LastStandMode">
            <summary>
            Uses position/orientation control on the spine and drifts in the direction of bullets.  This ignores all other stayUpright settings.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StayUprightHelper.LastStandSinkRate">
            <summary>
```
