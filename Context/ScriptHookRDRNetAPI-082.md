<!-- ScriptHookRDRNetAPI lines 20251-20500 of 30005 -->
```csharp
            <summary>
            Stiffness of legs.
            </summary>
            <remarks>
            Default value = 6.0f.
            Min value = 4.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.CatchFallHelper.ArmsStiffness">
            <summary>
            Stiffness of arms.
            </summary>
            <remarks>
            Default value = 15.0f.
            Min value = 6.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.CatchFallHelper.BackwardsMinArmOffset">
            <summary>
            0 will prop arms up near his shoulders. -0.3 will place hands nearer his behind.
            </summary>
            <remarks>
            Default value = -0.3f.
            Min value = -1.0f.
            Max value = 0.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.CatchFallHelper.ForwardMaxArmOffset">
            <summary>
            0 will point arms down with angled body, 0.45 will point arms forward a bit to catch nearer the head.
            </summary>
            <remarks>
            Default value = 0.4f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.CatchFallHelper.ZAxisSpinReduction">
            <summary>
            Tries to reduce the spin around the Z axis. Scale 0 - 1.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.CatchFallHelper.ExtraSit">
            <summary>
            Scale extra-sit value 0..1. Setting to 0 helps with arched-back issues.  Set to 1 for a more alive-looking finish.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.CatchFallHelper.UseHeadLook">
            <summary>
            Toggle to use the head look in this behavior.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.CatchFallHelper.Mask">
            <summary>
            Two character body-masking value, bitwise joint mask or bitwise logic string of two character body-masking value  (see Active Pose notes for possible values).
            </summary>
            <remarks>
            Default value = "fb".
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.ElectrocuteHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the ElectrocuteHelper for sending a Electrocute <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the Electrocute <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.ElectrocuteHelper.StunMag">
            <summary>
            The magnitude of the reaction.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ElectrocuteHelper.InitialMult">
            <summary>
            InitialMult*stunMag = The magnitude of the 1st snap reaction (other multipliers are applied after this).
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 20.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ElectrocuteHelper.LargeMult">
            <summary>
            LargeMult*stunMag = The magnitude of a random large snap reaction (other multipliers are applied after this).
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 20.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ElectrocuteHelper.LargeMinTime">
            <summary>
            Min time to next large random snap (about 14 snaps with stunInterval = 0.07s).
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 200.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ElectrocuteHelper.LargeMaxTime">
            <summary>
            Max time to next large random snap (about 28 snaps with stunInterval = 0.07s).
            </summary>
            <remarks>
            Default value = 2.0f.
            Min value = 0.0f.
            Max value = 200.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ElectrocuteHelper.MovingMult">
            <summary>
            MovingMult*stunMag = The magnitude of the reaction if moving(comVelMag) faster than movingThresh.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 20.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ElectrocuteHelper.BalancingMult">
            <summary>
            BalancingMult*stunMag = The magnitude of the reaction if balancing = (not lying on the floor/ not upper body not collided) and not airborne.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 20.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ElectrocuteHelper.AirborneMult">
            <summary>
            AirborneMult*stunMag = The magnitude of the reaction if airborne.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 20.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ElectrocuteHelper.MovingThresh">
            <summary>
            If moving(comVelMag) faster than movingThresh then mvingMult applied to stunMag.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 20.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ElectrocuteHelper.StunInterval">
            <summary>
            Direction flips every stunInterval.
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ElectrocuteHelper.DirectionRandomness">
            <summary>
            The character vibrates in a prescribed way - Higher the value the more random this direction is.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ElectrocuteHelper.LeftArm">
            <summary>
            Vibrate the leftArm.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ElectrocuteHelper.RightArm">
            <summary>
            Vibrate the rightArm.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ElectrocuteHelper.LeftLeg">
            <summary>
            Vibrate the leftLeg.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ElectrocuteHelper.RightLeg">
            <summary>
            Vibrate the rightLeg.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ElectrocuteHelper.Spine">
            <summary>
            Vibrate the spine.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ElectrocuteHelper.Neck">
            <summary>
            Vibrate the neck.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ElectrocuteHelper.PhasedLegs">
            <summary>
            Legs are either in phase with each other or not.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ElectrocuteHelper.ApplyStiffness">
            <summary>
            Let electrocute apply a (higher generally) stiffness to the character whilst being vibrated.
```
