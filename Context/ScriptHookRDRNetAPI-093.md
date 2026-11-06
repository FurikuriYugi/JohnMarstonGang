<!-- ScriptHookRDRNetAPI lines 23001-23250 of 30005 -->
```csharp
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 100.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunExtraHelper.PosDamp">
            <summary>
            Hand stabilization controller damping.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.RollDownStairsHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the RollDownStairsHelper for sending a RollDownStairs <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the RollDownStairs <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.RollDownStairsHelper.Stiffness">
            <summary>
            Effector Stiffness. Value feeds through to rollUp directly.
            </summary>
            <remarks>
            Default value = 11.0f.
            Min value = 6.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.RollDownStairsHelper.Damping">
            <summary>
            Effector  Damping.
            </summary>
            <remarks>
            Default value = 1.4f.
            Min value = 0.0f.
            Max value = 4.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.RollDownStairsHelper.Forcemag">
            <summary>
            Helper force strength.  Do not go above 1 for a rollDownStairs/roll along ground reaction.
            </summary>
            <remarks>
            Default value = 0.6f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.RollDownStairsHelper.M_useArmToSlowDown">
            <summary>
            The degree to which the character will try to stop a barrel roll with his arms.
            </summary>
            <remarks>
            Default value = -1.9f.
            Min value = -3.0f.
            Max value = 3.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.RollDownStairsHelper.UseZeroPose">
            <summary>
            Blends between a zeroPose and the Rollup, Faster the character is rotating the less the zeroPose.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.RollDownStairsHelper.SpinWhenInAir">
            <summary>
            Applied cheat forces to spin the character when in the air, the forces are 40% of the forces applied when touching the ground.  Be careful little bunny rabbits, the character could spin unnaturally in the air.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.RollDownStairsHelper.M_armReachAmount">
            <summary>
            How much the character reaches with his arms to brace against the ground.
            </summary>
            <remarks>
            Default value = 1.4f.
            Min value = 0.0f.
            Max value = 3.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.RollDownStairsHelper.M_legPush">
            <summary>
            Amount that the legs push outwards when tumbling.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.RollDownStairsHelper.TryToAvoidHeadButtingGround">
            <summary>
            Blends between a zeroPose and the Rollup, Faster the character is rotating the less the zeroPose.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.RollDownStairsHelper.ArmReachLength">
            <summary>
            The length that the arm reaches and so how much it straightens.
            </summary>
            <remarks>
            Default value = 0.4f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.RollDownStairsHelper.CustomRollDir">
            <summary>
            Pass in a custom direction in to have the character try and roll in that direction.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 1.0f).
            Min value = 1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.RollDownStairsHelper.UseCustomRollDir">
            <summary>
            Pass in true to use the customRollDir parameter.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.RollDownStairsHelper.StiffnessDecayTarget">
            <summary>
            The target linear velocity used to start the rolling.
            </summary>
            <remarks>
            Default value = 9.0f.
            Min value = 0.0f.
            Max value = 20.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.RollDownStairsHelper.StiffnessDecayTime">
            <summary>
            Time, in seconds, to decay stiffness down to the stiffnessDecayTarget value (or -1 to disable).
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.RollDownStairsHelper.AsymmetricalLegs">
            <summary>
            0 is no leg asymmetry in 'foetal' position.  greater than 0 a asymmetricalLegs-rand(30%), added/minus each joint of the legs in radians.  Random number changes about once every roll.  0.4 gives a lot of asymmetry.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.RollDownStairsHelper.ZAxisSpinReduction">
            <summary>
            Tries to reduce the spin around the z axis. Scale 0 - 1.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.RollDownStairsHelper.TargetLinearVelocityDecayTime">
            <summary>
            Time for the targetlinearVelocity to decay to zero.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.RollDownStairsHelper.TargetLinearVelocity">
            <summary>
            Helper torques are applied to match the spin of the character to the max of targetLinearVelocity and COMVelMag.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.RollDownStairsHelper.OnlyApplyHelperForces">
            <summary>
            Don't use rollup if true.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.RollDownStairsHelper.UseVelocityOfObjectBelow">
            <summary>
            Scale applied cheat forces/torques to (zero) if object underneath character has velocity greater than 1.f.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.RollDownStairsHelper.UseRelativeVelocity">
            <summary>
            UseVelocityOfObjectBelow uses a relative velocity of the character to the object underneath.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.RollDownStairsHelper.ApplyFoetalToLegs">
            <summary>
            If true, use rollup for upper body and a kind of foetal behavior for legs.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.RollDownStairsHelper.MovementLegsInFoetalPosition">
            <summary>
            Only used if applyFoetalToLegs = true : define the variation of angles for the joints of the legs.
            </summary>
            <remarks>
            Default value = 1.3f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.RollDownStairsHelper.MaxAngVelAroundFrontwardAxis">
            <summary>
            Only used if applyNewRollingCheatingTorques or applyHelPerTorqueToAlign defined to true : maximal angular velocity around frontward axis of the pelvis to apply cheating torques.
            </summary>
            <remarks>
            Default value = 2.0f.
            Min value = -1.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.RollDownStairsHelper.MinAngVel">
            <summary>
            Only used if applyNewRollingCheatingTorques or applyHelPerTorqueToAlign defined to true : minimal angular velocity of the roll to apply cheating torques.
            </summary>
```
