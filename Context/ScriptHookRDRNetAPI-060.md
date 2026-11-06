<!-- ScriptHookRDRNetAPI lines 14751-15000 of 30005 -->
```csharp
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.ImpulseOneLegOn">
            <summary>
            Scale and/or clamp impulse if the character is contacting with one leg only and dynamicBalance is OK.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.RbRatio">
            <summary>
            0.0 no rigidBody response, 0.5 half partForce half rigidBody, 1.0 = no partForce full rigidBody.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.RbLowerShare">
            <summary>
            Rigid body response is shared between the upper and lower body (rbUpperShare = 1-rbLowerShare). RbLowerShare=0.5 gives upper and lower share scaled by mass.  i.e. if 70% ub mass and 30% lower mass then rbLowerShare=0.5 gives actualrbShare of 0.7ub and 0.3lb. rbLowerShare GT 0.5 scales the ub share down from 0.7 and the lb up from 0.3.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.RbMoment">
            <summary>
            0.0 only force, 0.5 = force and half the rigid body moment applied, 1.0 = force and full rigidBody moment.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.RbMaxTwistMomentArm">
            <summary>
            Maximum twist arm moment of bullet applied.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.RbMaxBroomMomentArm">
            <summary>
            Maximum broom((everything but the twist) arm moment of bullet applied.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.RbRatioAirborne">
            <summary>
            If Airborne: 0.0 no rigidBody response, 0.5 half partForce half rigidBody, 1.0 = no partForce full rigidBody.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.RbMomentAirborne">
            <summary>
            If Airborne: 0.0 only force, 0.5 = force and half the rigid body moment applied, 1.0 = force and full rigidBody moment.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.RbMaxTwistMomentArmAirborne">
            <summary>
            If Airborne: Maximum twist arm moment of bullet applied.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.RbMaxBroomMomentArmAirborne">
            <summary>
            If Airborne: Maximum broom((everything but the twist) arm moment of bullet applied.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.RbRatioOneLeg">
            <summary>
            If only one leg in contact: 0.0 no rigidBody response, 0.5 half partForce half rigidBody, 1.0 = no partForce full rigidBody.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.RbMomentOneLeg">
            <summary>
            If only one leg in contact: 0.0 only force, 0.5 = force and half the rigid body moment applied, 1.0 = force and full rigidBody moment.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.RbMaxTwistMomentArmOneLeg">
            <summary>
            If only one leg in contact: Maximum twist arm moment of bullet applied.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.RbMaxBroomMomentArmOneLeg">
            <summary>
            If only one leg in contact: Maximum broom((everything but the twist) arm moment of bullet applied.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.RbTwistAxis">
            <summary>
            </summary>
            <remarks>
            Default value = <see cref="F:RDR2.NaturalMotion.RbTwistAxis.WorldUp"/>.
            </remarks>.
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.RbPivot">
            <summary>
            If false pivot around COM always, if true change pivot depending on foot contact:  to feet center if both feet in contact, or foot position if 1 foot in contact or COM position if no feet in contact.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the ConfigureBulletsExtraHelper for sending a ConfigureBulletsExtra <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the ConfigureBulletsExtra <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.ImpulseSpreadOverParts">
            <summary>
            Spreads impulse across parts. Currently only for spine parts, not limbs.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.ImpulsePeriod">
            <summary>
            Duration that impulse is spread over (triangular shaped).
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.ImpulseTorqueScale">
            <summary>
            An impulse applied at a point on a body equivalent to an impulse at the center of the body and a torque.  This parameter scales the torque component. (The torque component seems to be excite the rage looseness bug which sends the character in a sometimes wildly different direction to an applied impulse).
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.LoosenessFix">
            <summary>
            Fix the rage looseness bug by applying only the impulse at the center of the body unless it is a spine part then apply the twist component only of the torque as well.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.ImpulseDelay">
            <summary>
            Time from hit before impulses are being applied.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.TorqueMode">
            <summary>
            </summary>
            <remarks>
            Default value = <see cref="F:RDR2.NaturalMotion.TorqueMode.Disabled"/>.
            If <see cref="F:RDR2.NaturalMotion.TorqueMode.Proportional"/> - proportional to character strength, can reduce impulse amount.
            If <see cref="F:RDR2.NaturalMotion.TorqueMode.Additive"/> - no reduction of impulse and not proportional to character strength.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.TorqueSpinMode">
            <summary>
            </summary>
            <remarks>
            Default value = <see cref="F:RDR2.NaturalMotion.TorqueSpinMode.FromImpulse"/>.
            If <see cref="F:RDR2.NaturalMotion.TorqueSpinMode.Flipping"/> a burst effect is achieved.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.TorqueFilterMode">
            <summary>
            </summary>
            <remarks>
            Default value = <see cref="F:RDR2.NaturalMotion.TorqueFilterMode.ApplyEveryBullet"/>.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.TorqueAlwaysSpine3">
            <summary>
            Always apply torques to spine3 instead of actual part hit.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.TorqueDelay">
            <summary>
            Time from hit before torques are being applied.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.TorquePeriod">
            <summary>
            Duration of torque.
```
