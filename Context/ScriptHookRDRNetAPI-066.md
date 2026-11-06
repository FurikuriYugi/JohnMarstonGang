<!-- ScriptHookRDRNetAPI lines 16251-16500 of 30005 -->
```csharp
            Min time until changing direction.
            </summary>
            <remarks>
            Default value = 2.0f.
            Min value = 0.0f.
            Max value = 20.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ForceLeanRandomHelper.ChangeTimeMax">
            <summary>
            Maximum time until changing direction.
            </summary>
            <remarks>
            Default value = 4.0f.
            Min value = 0.0f.
            Max value = 20.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ForceLeanRandomHelper.BodyPart">
            <summary>
            Body part that the force is applied to.
            </summary>
            <remarks>
            Default value = 0.
            Min value = 0.
            Max value = 21.
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.ForceLeanToPositionHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the ForceLeanToPositionHelper for sending a ForceLeanToPosition <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the ForceLeanToPosition <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.ForceLeanToPositionHelper.LeanAmount">
            <summary>
            Amount of lean, 0 to about 0.5. -ve will move away from the target.
            </summary>
            <remarks>
            Default value = 0.2f.
            Min value = -0.5f.
            Max value = 0.5f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ForceLeanToPositionHelper.Pos">
            <summary>
            Position to head towards.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ForceLeanToPositionHelper.BodyPart">
            <summary>
            Body part that the force is applied to.
            </summary>
            <remarks>
            Default value = 0.
            Min value = 0.
            Max value = 21.
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.ForceLeanTowardsObjectHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the ForceLeanTowardsObjectHelper for sending a ForceLeanTowardsObject <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the ForceLeanTowardsObject <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.ForceLeanTowardsObjectHelper.LeanAmount">
            <summary>
            Amount of lean, 0 to about 0.5. -ve will move away from the target.
            </summary>
            <remarks>
            Default value = 0.2f.
            Min value = -0.5f.
            Max value = 0.5f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ForceLeanTowardsObjectHelper.Offset">
            <summary>
            Offset from instance position added when calculating position to lean to.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            Min value = -100.0f.
            Max value = 100.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ForceLeanTowardsObjectHelper.InstanceIndex">
            <summary>
            LevelIndex of object to move towards.
            </summary>
            <remarks>
            Default value = -1.
            Min value = -1.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ForceLeanTowardsObjectHelper.BoundIndex">
            <summary>
            BoundIndex of object to move towards (0 = just use instance coordinates).
            </summary>
            <remarks>
            Default value = 0.
            Min value = 0.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ForceLeanTowardsObjectHelper.BodyPart">
            <summary>
            Body part that the force is applied to.
            </summary>
            <remarks>
            Default value = 0.
            Min value = 0.
            Max value = 21.
            </remarks>
        </member>
        <member name="T:RDR2.NaturalMotion.SetStiffnessHelper">
            <summary>
            Use this message to manually set the body stiffness values -before using Active Pose to drive to an animated pose, for example.
            </summary>
        </member>
        <member name="M:RDR2.NaturalMotion.SetStiffnessHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the SetStiffnessHelper for sending a SetStiffness <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the SetStiffness <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
            <remarks>
            Use this message to manually set the body stiffness values -before using Active Pose to drive to an animated pose, for example.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetStiffnessHelper.BodyStiffness">
            <summary>
            Stiffness of whole character.
            </summary>
            <remarks>
            Default value = 12.0f.
            Min value = 2.0f.
            Max value = 20.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetStiffnessHelper.Damping">
            <summary>
            Damping amount, less is underdamped.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 3.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetStiffnessHelper.Mask">
            <summary>
            Two character body-masking value, bitwise joint mask or bitwise logic string of two character body-masking value  (see Active Pose notes for possible values).
            </summary>
            <remarks>
            Default value = "fb".
            </remarks>
        </member>
        <member name="T:RDR2.NaturalMotion.SetMuscleStiffnessHelper">
            <summary>
            Use this message to manually set the muscle stiffness values -before using Active Pose to drive to an animated pose, for example.
            </summary>
        </member>
        <member name="M:RDR2.NaturalMotion.SetMuscleStiffnessHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the SetMuscleStiffnessHelper for sending a SetMuscle stiffness <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the SetMuscle stiffness <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
            <remarks>
            Use this message to manually set the muscle stiffness values -before using Active Pose to drive to an animated pose, for example.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetMuscleStiffnessHelper.MuscleStiffness">
            <summary>
            Muscle stiffness of joint/s.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 20.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetMuscleStiffnessHelper.Mask">
            <summary>
            Two character body-masking value, bitwise joint mask or bitwise logic string of two character body-masking value  (see Active Pose notes for possible values).
            </summary>
            <remarks>
            Default value = "fb".
            </remarks>
        </member>
        <member name="T:RDR2.NaturalMotion.SetWeaponModeHelper">
            <summary>
            Use this message to set the character's weapon mode.  This is an alternativeto the setWeaponMode public function.
            </summary>
        </member>
        <member name="M:RDR2.NaturalMotion.SetWeaponModeHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the SetWeaponModeHelper for sending a SetWeaponMode <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the SetWeaponMode <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
            <remarks>
            Use this message to set the character's weapon mode.  This is an alternativeto the setWeaponMode public function.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetWeaponModeHelper.WeaponMode">
            <summary>
            </summary>
            <remarks>
            Default value = <see cref="F:RDR2.NaturalMotion.WeaponMode.PistolRight"/>.
            </remarks>.
        </member>
        <member name="T:RDR2.NaturalMotion.RegisterWeaponHelper">
            <summary>
            Use this message to register weapon.  This is an alternativeto the registerWeapon public function.
            </summary>
        </member>
        <member name="M:RDR2.NaturalMotion.RegisterWeaponHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the RegisterWeaponHelper for sending a RegisterWeapon <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the RegisterWeapon <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
            <remarks>
            Use this message to register weapon.  This is an alternativeto the registerWeapon public function.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.RegisterWeaponHelper.Hand">
            <summary>
            </summary>
            <remarks>
            Default value = <see cref="F:RDR2.NaturalMotion.Hand.Right"/>.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.RegisterWeaponHelper.LevelIndex">
            <summary>
            Level index of the weapon.
            </summary>
            <remarks>
            Default value = -1.
            Min value = -1.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.RegisterWeaponHelper.ConstraintHandle">
            <summary>
            Pointer to the hand-gun constraint handle.
            </summary>
            <remarks>
            Default value = -1.
            Min value = -1.
            </remarks>
        </member>
```
