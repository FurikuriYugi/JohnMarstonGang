<!-- ScriptHookRDRNetAPI lines 23251-23500 of 30005 -->
```csharp
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.RollDownStairsHelper.ApplyNewRollingCheatingTorques">
            <summary>
            If true will use the new way to apply cheating torques (like in fallOverWall), otherwise will use the old way.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.RollDownStairsHelper.MaxAngVel">
            <summary>
            Only used if applyNewRollingCheatingTorques defined to true : maximal angular velocity of the roll to apply cheating torque.
            </summary>
            <remarks>
            Default value = 5.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.RollDownStairsHelper.MagOfTorqueToRoll">
            <summary>
            Only used if applyNewRollingCheatingTorques defined to true : magnitude of the torque to roll down the stairs.
            </summary>
            <remarks>
            Default value = 50.0f.
            Min value = 0.0f.
            Max value = 500.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.RollDownStairsHelper.ApplyHelPerTorqueToAlign">
            <summary>
            Apply torque to align the body orthogonally to the direction of the roll.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.RollDownStairsHelper.DelayToAlignBody">
            <summary>
            Only used if applyHelPerTorqueToAlign defined to true : delay to start to apply torques.
            </summary>
            <remarks>
            Default value = 0.2f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.RollDownStairsHelper.MagOfTorqueToAlign">
            <summary>
            Only used if applyHelPerTorqueToAlign defined to true : magnitude of the torque to align orthogonally the body.
            </summary>
            <remarks>
            Default value = 50.0f.
            Min value = 0.0f.
            Max value = 500.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.RollDownStairsHelper.AirborneReduction">
            <summary>
            Ordinarily keep at 0.85.  Make this lower if you want spinning in the air.
            </summary>
            <remarks>
            Default value = 0.9f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.RollDownStairsHelper.ApplyMinMaxFriction">
            <summary>
            Pass-through to Roll Up. Controls whether or not behavior enforces min/max friction.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.RollDownStairsHelper.LimitSpinReduction">
            <summary>
            Scale zAxisSpinReduction back when rotating end-over-end (somersault) to give the body a chance to align with the axis of rotation.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.ShotHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the ShotHelper for sending a Shot <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the Shot <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.BodyStiffness">
            <summary>
            Stiffness of body. Feeds through to roll_up.
            </summary>
            <remarks>
            Default value = 11.0f.
            Min value = 6.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.SpineDamping">
            <summary>
            Stiffness of body. Feeds through to roll_up.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.1f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.ArmStiffness">
            <summary>
            Arm stiffness.
            </summary>
            <remarks>
            Default value = 10.0f.
            Min value = 6.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.InitialNeckStiffness">
            <summary>
            Initial stiffness of neck after being shot.
            </summary>
            <remarks>
            Default value = 14.0f.
            Min value = 3.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.InitialNeckDamping">
            <summary>
            Intial damping of neck after being shot.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.1f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.NeckStiffness">
            <summary>
            Stiffness of neck.
            </summary>
            <remarks>
            Default value = 14.0f.
            Min value = 3.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.NeckDamping">
            <summary>
            Damping of neck.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.1f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.KMultOnLoose">
            <summary>
            How much to add to upperbody stiffness dependent on looseness.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.KMult4Legs">
            <summary>
            How much to add to leg stiffnesses dependent on looseness.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.LoosenessAmount">
            <summary>
            How loose the character is made by a newBullet. Between 0 and 1.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.Looseness4Fall">
            <summary>
            How loose the character is made by a newBullet if falling.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.Looseness4Stagger">
            <summary>
            How loose the upperBody of the character is made by a newBullet if staggerFall is running (and not falling).  Note atm the neck ramp values are ignored in staggerFall.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.MinArmsLooseness">
            <summary>
            Minimum looseness to apply to the arms.
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.MinLegsLooseness">
            <summary>
            Minimum looseness to apply to the Legs.
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.GrabHoldTime">
            <summary>
            How long to hold for before returning to relaxed arm position.
            </summary>
            <remarks>
            Default value = 2.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHelper.SpineBlendExagCPain">
            <summary>
            True: spine is blended with zero pose, false: spine is blended with zero pose if not setting exag or cpain.
            </summary>
            <remarks>
            Default value = False.
```
