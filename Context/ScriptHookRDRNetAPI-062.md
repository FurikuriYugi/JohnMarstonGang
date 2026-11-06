<!-- ScriptHookRDRNetAPI lines 15251-15500 of 30005 -->
```csharp
            If impulse is above this value scale it by impulseOneLegMult.
            </summary>
            <remarks>
            Default value = 100.0f.
            Min value = 0.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.ImpulseOneLegMax">
            <summary>
            Amount to clamp impulse to if character is contacting with one foot only  and dynamicBalance is OK.
            </summary>
            <remarks>
            Default value = 100.0f.
            Min value = 0.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.ImpulseOneLegApplyAbove">
            <summary>
            If impulse is above this amount then do not scale/clamp just let it through as is - it's a shotgun or cannon.
            </summary>
            <remarks>
            Default value = 399.0f.
            Min value = 0.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.ImpulseOneLegOn">
            <summary>
            Scale and/or clamp impulse if the character is contacting with one leg only and dynamicBalance is OK.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.RbRatio">
            <summary>
            0.0 no rigidBody response, 0.5 half partForce half rigidBody, 1.0 = no partForce full rigidBody.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.RbLowerShare">
            <summary>
            Rigid body response is shared between the upper and lower body (rbUpperShare = 1-rbLowerShare). RbLowerShare=0.5 gives upper and lower share scaled by mass.  i.e. if 70% ub mass and 30% lower mass then rbLowerShare=0.5 gives actualrbShare of 0.7ub and 0.3lb. rbLowerShare GT 0.5 scales the ub share down from 0.7 and the lb up from 0.3.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.RbMoment">
            <summary>
            0.0 only force, 0.5 = force and half the rigid body moment applied, 1.0 = force and full rigidBody moment.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.RbMaxTwistMomentArm">
            <summary>
            Maximum twist arm moment of bullet applied.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.RbMaxBroomMomentArm">
            <summary>
            Maximum broom((everything but the twist) arm moment of bullet applied.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.RbRatioAirborne">
            <summary>
            If Airborne: 0.0 no rigidBody response, 0.5 half partForce half rigidBody, 1.0 = no partForce full rigidBody.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.RbMomentAirborne">
            <summary>
            If Airborne: 0.0 only force, 0.5 = force and half the rigid body moment applied, 1.0 = force and full rigidBody moment.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.RbMaxTwistMomentArmAirborne">
            <summary>
            If Airborne: Maximum twist arm moment of bullet applied.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.RbMaxBroomMomentArmAirborne">
            <summary>
            If Airborne: Maximum broom((everything but the twist) arm moment of bullet applied.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.RbRatioOneLeg">
            <summary>
            If only one leg in contact: 0.0 no rigidBody response, 0.5 half partForce half rigidBody, 1.0 = no partForce full rigidBody.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.RbMomentOneLeg">
            <summary>
            If only one leg in contact: 0.0 only force, 0.5 = force and half the rigid body moment applied, 1.0 = force and full rigidBody moment.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.RbMaxTwistMomentArmOneLeg">
            <summary>
            If only one leg in contact: Maximum twist arm moment of bullet applied.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.RbMaxBroomMomentArmOneLeg">
            <summary>
            If only one leg in contact: Maximum broom((everything but the twist) arm moment of bullet applied.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.RbTwistAxis">
            <summary>
            </summary>
            <remarks>
            Default value = <see cref="F:RDR2.NaturalMotion.RbTwistAxis.WorldUp"/>.
            </remarks>.
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.RbPivot">
            <summary>
            If false pivot around COM always, if true change pivot depending on foot contact:  to feet center if both feet in contact, or foot position if 1 foot in contact or COM position if no feet in contact.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="T:RDR2.NaturalMotion.ConfigureLimitsHelper">
            <summary>
            Enable/disable/edit character limits in real time.  This adjusts limits in RAGE-native space and will *not* reorient the joint.
            </summary>
        </member>
        <member name="M:RDR2.NaturalMotion.ConfigureLimitsHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the ConfigureLimitsHelper for sending a ConfigureLimits <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the ConfigureLimits <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
            <remarks>
            Enable/disable/edit character limits in real time.  This adjusts limits in RAGE-native space and will *not* reorient the joint.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureLimitsHelper.Mask">
            <summary>
            Two character body-masking value, bitwise joint mask or bitwise logic string of two character body-masking value  for joint limits to configure. Ignored if index != -1.
            </summary>
            <remarks>
            Default value = "fb".
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureLimitsHelper.Enable">
            <summary>
            If false, disable (set all to PI, -PI) limits.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureLimitsHelper.ToDesired">
            <summary>
            If true, set limits to accommodate current desired angles.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureLimitsHelper.Restore">
            <summary>
            Return to cached defaults?.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureLimitsHelper.ToCurAnimation">
            <summary>
            If true, set limits to the current animated limits.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureLimitsHelper.Index">
            <summary>
            Index of effector to configure.  Set to -1 to use mask.
            </summary>
            <remarks>
            Default value = -1.
            Min value = -1.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureLimitsHelper.Lean1">
            <summary>
            Custom limit values to use if not setting limits to desired. Limits are RAGE-native, not NM-wrapper-native.
            </summary>
            <remarks>
            Default value = 1.6f.
            Min value = 0.0f.
            Max value = 3.1f.
            </remarks>
```
