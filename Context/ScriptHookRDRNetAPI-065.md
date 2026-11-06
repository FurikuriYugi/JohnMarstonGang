<!-- ScriptHookRDRNetAPI lines 16001-16250 of 30005 -->
```csharp
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.LeanTowardsObjectHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the LeanTowardsObjectHelper for sending a LeanTowardsObject <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the LeanTowardsObject <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.LeanTowardsObjectHelper.LeanAmount">
            <summary>
            Amount of lean, 0 to about 0.5. -ve will move away from the target.
            </summary>
            <remarks>
            Default value = 0.2f.
            Min value = -0.5f.
            Max value = 0.5f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.LeanTowardsObjectHelper.Offset">
            <summary>
            Offset from instance position added when calculating position to lean to.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            Min value = -100.0f.
            Max value = 100.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.LeanTowardsObjectHelper.InstanceIndex">
            <summary>
            LevelIndex of object to lean towards.
            </summary>
            <remarks>
            Default value = -1.
            Min value = -1.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.LeanTowardsObjectHelper.BoundIndex">
            <summary>
            BoundIndex of object to lean towards (0 = just use instance coordinates).
            </summary>
            <remarks>
            Default value = 0.
            Min value = 0.
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.HipsLeanInDirectionHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the HipsLeanInDirectionHelper for sending a HipsLeanInDirection <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the HipsLeanInDirection <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.HipsLeanInDirectionHelper.LeanAmount">
            <summary>
            Amount of lean, 0 to about 0.5. -ve will move away from the target.
            </summary>
            <remarks>
            Default value = 0.2f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HipsLeanInDirectionHelper.Dir">
            <summary>
            Direction to lean in.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 1.0f).
            Min value = 0.0f.
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.HipsLeanRandomHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the HipsLeanRandomHelper for sending a HipsLeanRandom <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the HipsLeanRandom <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.HipsLeanRandomHelper.LeanAmountMin">
            <summary>
            Minimum amount of lean.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HipsLeanRandomHelper.LeanAmountMax">
            <summary>
            Maximum amount of lean.
            </summary>
            <remarks>
            Default value = 0.4f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HipsLeanRandomHelper.ChangeTimeMin">
            <summary>
            Min time until changing direction.
            </summary>
            <remarks>
            Default value = 2.0f.
            Min value = 0.0f.
            Max value = 20.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HipsLeanRandomHelper.ChangeTimeMax">
            <summary>
            Maximum time until changing direction.
            </summary>
            <remarks>
            Default value = 4.0f.
            Min value = 0.0f.
            Max value = 20.0f.
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.HipsLeanToPositionHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the HipsLeanToPositionHelper for sending a HipsLeanToPosition <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the HipsLeanToPosition <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.HipsLeanToPositionHelper.LeanAmount">
            <summary>
            Amount of lean, 0 to about 0.5. -ve will move away from the target.
            </summary>
            <remarks>
            Default value = 0.2f.
            Min value = -0.5f.
            Max value = 0.5f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HipsLeanToPositionHelper.Pos">
            <summary>
            Position to head towards.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.HipsLeanTowardsObjectHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the HipsLeanTowardsObjectHelper for sending a HipsLeanTowardsObject <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the HipsLeanTowardsObject <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.HipsLeanTowardsObjectHelper.LeanAmount">
            <summary>
            Amount of lean, 0 to about 0.5. -ve will move away from the target.
            </summary>
            <remarks>
            Default value = 0.2f.
            Min value = -0.5f.
            Max value = 0.5f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HipsLeanTowardsObjectHelper.Offset">
            <summary>
            Offset from instance position added when calculating position to lean to.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            Min value = -100.0f.
            Max value = 100.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HipsLeanTowardsObjectHelper.InstanceIndex">
            <summary>
            LevelIndex of object to lean hips towards.
            </summary>
            <remarks>
            Default value = -1.
            Min value = -1.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HipsLeanTowardsObjectHelper.BoundIndex">
            <summary>
            BoundIndex of object to lean hips towards (0 = just use instance coordinates).
            </summary>
            <remarks>
            Default value = 0.
            Min value = 0.
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.ForceLeanInDirectionHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the ForceLeanInDirectionHelper for sending a ForceLeanInDirection <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the ForceLeanInDirection <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.ForceLeanInDirectionHelper.LeanAmount">
            <summary>
            Amount of lean, 0 to about 0.5. -ve will move away from the target.
            </summary>
            <remarks>
            Default value = 0.2f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ForceLeanInDirectionHelper.Dir">
            <summary>
            Direction to lean in.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 1.0f).
            Min value = 0.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ForceLeanInDirectionHelper.BodyPart">
            <summary>
            Body part that the force is applied to.
            </summary>
            <remarks>
            Default value = 0.
            Min value = 0.
            Max value = 21.
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.ForceLeanRandomHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the ForceLeanRandomHelper for sending a ForceLeanRandom <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the ForceLeanRandom <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.ForceLeanRandomHelper.LeanAmountMin">
            <summary>
            Minimum amount of lean.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ForceLeanRandomHelper.LeanAmountMax">
            <summary>
            Maximum amount of lean.
            </summary>
            <remarks>
            Default value = 0.4f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ForceLeanRandomHelper.ChangeTimeMin">
            <summary>
```
