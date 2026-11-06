<!-- ScriptHookRDRNetAPI lines 15751-16000 of 30005 -->
```csharp
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureShotInjuredLegHelper.LegForceStep">
            <summary>
            Force a step to be taken whether pushed out of balance or not.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureShotInjuredLegHelper.LegLimpBend">
            <summary>
            Bend the legs via the balancer by this amount if stepping on the injured leg.
            0.2 seems a good default.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureShotInjuredLegHelper.LegLiftTime">
            <summary>
            Leg lift duration (reaction to being shot in leg).
            (Lifting happens when not stepping with other leg).
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureShotInjuredLegHelper.LegInjury">
            <summary>
            Leg injury - leg strength is reduced.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureShotInjuredLegHelper.LegInjuryHipPitch">
            <summary>
            Leg injury bend forwards amount when not lifting leg.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureShotInjuredLegHelper.LegInjuryLiftHipPitch">
            <summary>
            Leg injury bend forwards amount when lifting leg.
            (Lifting happens when not stepping with other leg).
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureShotInjuredLegHelper.LegInjurySpineBend">
            <summary>
            Leg injury bend forwards amount when not lifting leg.
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureShotInjuredLegHelper.LegInjuryLiftSpineBend">
            <summary>
            Leg injury bend forwards amount when lifting leg.
            (Lifting happens when not stepping with other leg).
            </summary>
            <remarks>
            Default value = 0.2f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.DefineAttachedObjectHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the DefineAttachedObjectHelper for sending a DefineAttachedObject <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the DefineAttachedObject <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.DefineAttachedObjectHelper.PartIndex">
            <summary>
            Index of part to attach to.
            </summary>
            <remarks>
            Default value = -1.
            Min value = -1.
            Max value = 21.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.DefineAttachedObjectHelper.ObjectMass">
            <summary>
            Mass of the attached object.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.DefineAttachedObjectHelper.WorldPos">
            <summary>
            World position of attached object's center of mass. Must be updated each frame.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            </remarks>
        </member>
        <member name="T:RDR2.NaturalMotion.ForceToBodyPartHelper">
            <summary>
            Apply an impulse to a named body part.
            </summary>
        </member>
        <member name="M:RDR2.NaturalMotion.ForceToBodyPartHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the ForceToBodyPartHelper for sending a ForceToBodyPart <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the ForceToBodyPart <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
            <remarks>
            Apply an impulse to a named body part.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ForceToBodyPartHelper.PartIndex">
            <summary>
            Part or link or bound index.
            </summary>
            <remarks>
            Default value = 0.
            Min value = 0.
            Max value = 28.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ForceToBodyPartHelper.Force">
            <summary>
            Force to apply.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, -50.0f, 0.0f).
            Min value = -100000.0f.
            Max value = 100000.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ForceToBodyPartHelper.ForceDefinedInPartSpace">
            <summary>
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.LeanInDirectionHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the LeanInDirectionHelper for sending a LeanInDirection <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the LeanInDirection <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.LeanInDirectionHelper.LeanAmount">
            <summary>
            Amount of lean, 0 to about 0.5. -ve will move away from the target.
            </summary>
            <remarks>
            Default value = 0.2f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.LeanInDirectionHelper.Dir">
            <summary>
            Direction to lean in.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 1.0f).
            Min value = 0.0f.
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.LeanRandomHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the LeanRandomHelper for sending a LeanRandom <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the LeanRandom <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.LeanRandomHelper.LeanAmountMin">
            <summary>
            Minimum amount of lean.
            </summary>
            <remarks>
            Default value = 0.2f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.LeanRandomHelper.LeanAmountMax">
            <summary>
            Maximum amount of lean.
            </summary>
            <remarks>
            Default value = 0.2f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.LeanRandomHelper.ChangeTimeMin">
            <summary>
            Minimum time until changing direction.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 20.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.LeanRandomHelper.ChangeTimeMax">
            <summary>
            Maximum time until changing direction.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 20.0f.
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.LeanToPositionHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the LeanToPositionHelper for sending a LeanToPosition <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the LeanToPosition <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.LeanToPositionHelper.LeanAmount">
            <summary>
            Amount of lean, 0 to about 0.5. -ve will move away from the target.
            </summary>
            <remarks>
            Default value = 0.2f.
            Min value = -0.5f.
            Max value = 0.5f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.LeanToPositionHelper.Pos">
            <summary>
            Position to head towards.
            </summary>
```
