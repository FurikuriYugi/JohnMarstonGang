<!-- ScriptHookRDRNetAPI lines 24001-24250 of 30005 -->
```csharp
        <member name="P:RDR2.NaturalMotion.ShotSnapHelper.SnapDirectionRandomness">
            <summary>
            The character snaps in a prescribed way (decided by bullet direction) - Higher the value the more random this direction is.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotSnapHelper.SnapLeftArm">
            <summary>
            Snap the leftArm.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotSnapHelper.SnapRightArm">
            <summary>
            Snap the rightArm.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotSnapHelper.SnapLeftLeg">
            <summary>
            Snap the leftLeg.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotSnapHelper.SnapRightLeg">
            <summary>
            Snap the rightLeg.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotSnapHelper.SnapSpine">
            <summary>
            Snap the spine.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotSnapHelper.SnapNeck">
            <summary>
            Snap the neck.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotSnapHelper.SnapPhasedLegs">
            <summary>
            Legs are either in phase with each other or not.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotSnapHelper.SnapHipType">
            <summary>
            Type of hip reaction 0=none, 1=side2side 2=steplike.
            </summary>
            <remarks>
            Default value = 0.
            Min value = 0.
            Max value = 2.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotSnapHelper.SnapUseBulletDir">
            <summary>
            Legs are either in phase with each other or not.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotSnapHelper.SnapHitPart">
            <summary>
            Snap only around the wounded part//mmmmtodo check whether bodyPart doesn't have to be remembered for unSnap.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotSnapHelper.UnSnapInterval">
            <summary>
            Interval before applying reverse snap.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 100.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotSnapHelper.UnSnapRatio">
            <summary>
            The magnitude of the reverse snap.
            </summary>
            <remarks>
            Default value = 0.7f.
            Min value = 0.0f.
            Max value = 100.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotSnapHelper.SnapUseTorques">
            <summary>
            Use torques to make the snap otherwise use a change in the parts angular velocity.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="T:RDR2.NaturalMotion.ShotShockSpinHelper">
            <summary>
            Configure the shockSpin effect in shot.  Spin/Lift the character using cheat torques/forces.
            </summary>
        </member>
        <member name="M:RDR2.NaturalMotion.ShotShockSpinHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the ShotShockSpinHelper for sending a ShotShockSpin <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the ShotShockSpin <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
            <remarks>
            Configure the shockSpin effect in shot.  Spin/Lift the character using cheat torques/forces.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotShockSpinHelper.AddShockSpin">
            <summary>
            If enabled, add a short 'shock' of torque to the character's spine to exaggerate bullet impact.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotShockSpinHelper.RandomizeShockSpinDirection">
            <summary>
            For use with close-range shotgun blasts, or similar.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotShockSpinHelper.AlwaysAddShockSpin">
            <summary>
            If true, apply the shock spin no matter which body component was hit. Otherwise only apply if the spine or clavicles get hit.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotShockSpinHelper.ShockSpinMin">
            <summary>
            Minimum amount of torque to add if using shock-spin feature.
            </summary>
            <remarks>
            Default value = 50.0f.
            Min value = 0.0f.
            Max value = 1000.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotShockSpinHelper.ShockSpinMax">
            <summary>
            Maximum amount of torque to add if using shock-spin feature.
            </summary>
            <remarks>
            Default value = 90.0f.
            Min value = 0.0f.
            Max value = 1000.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotShockSpinHelper.ShockSpinLiftForceMult">
            <summary>
            If greater than 0, apply a force to lift the character up while the torque is applied, trying to produce a dramatic spun/twist shotgun-to-the-chest effect. This is a scale of the torque applied, so 8.0 or so would give a reasonable amount of lift.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotShockSpinHelper.ShockSpinDecayMult">
            <summary>
            Multiplier used when decaying torque spin over time.
            </summary>
            <remarks>
            Default value = 4.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotShockSpinHelper.ShockSpinScalePerComponent">
            <summary>
            Torque applied is scaled by this amount across the spine components - spine2 recieving the full amount, then 3 and 1 and finally 0. Each time, this value is used to scale it down. 0.5 means half the torque each time.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotShockSpinHelper.ShockSpinMaxTwistVel">
            <summary>
            Shock spin ends when twist velocity is greater than this value (try 6.0).  If set to -1 does not stop.
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.0f.
            Max value = 200.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotShockSpinHelper.ShockSpinScaleByLeverArm">
            <summary>
            Shock spin scales by lever arm of bullet i.e. bullet impact point to center line.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotShockSpinHelper.ShockSpinAirMult">
            <summary>
            ShockSpin's torque is multipied by this value when both the character's feet are not in contact.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotShockSpinHelper.ShockSpin1FootMult">
            <summary>
            ShockSpin's torque is multipied by this value when the one of the character's feet are not in contact.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotShockSpinHelper.ShockSpinFootGripMult">
            <summary>
            ShockSpin scales the torques applied to the feet by footSlipCompensation.
            </summary>
```
