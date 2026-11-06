<!-- ScriptHookRDRNetAPI lines 17251-17500 of 30005 -->
```csharp
            <summary>
            When bodyStrength is less than successStrength send a success feedback - DO NOT GO OUTSIDE MIN/MAX PARAMETER VALUES OTHERWISE NO SUCCESS FEEDBACK WILL BE SENT.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.3f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="T:RDR2.NaturalMotion.SetCharacterUnderwaterHelper">
            <summary>
            Sets viscosity applied to damping limbs.
            </summary>
        </member>
        <member name="M:RDR2.NaturalMotion.SetCharacterUnderwaterHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the SetCharacterUnderwaterHelper for sending a SetCharacterUnderwater <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the SetCharacterUnderwater <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
            <remarks>
            Sets viscosity applied to damping limbs.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetCharacterUnderwaterHelper.Underwater">
            <summary>
            Is character underwater?.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetCharacterUnderwaterHelper.Viscosity">
            <summary>
            Viscosity applied to character's parts.
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.0f.
            Max value = 100.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetCharacterUnderwaterHelper.GravityFactor">
            <summary>
            Gravity factor applied to character.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = -10.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetCharacterUnderwaterHelper.Stroke">
            <summary>
            Swimming force applied to character as a function of handVelocity and footVelocity.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = -1000.0f.
            Max value = 1000.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetCharacterUnderwaterHelper.LinearStroke">
            <summary>
            Swimming force (linearStroke=true,False) = (f(v),f(v*v)).
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="T:RDR2.NaturalMotion.SetCharacterCollisionsHelper">
            <summary>
            SetCharacterCollisions:.
            </summary>
        </member>
        <member name="M:RDR2.NaturalMotion.SetCharacterCollisionsHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the SetCharacterCollisionsHelper for sending a SetCharacterCollisions <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the SetCharacterCollisions <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
            <remarks>
            SetCharacterCollisions:.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetCharacterCollisionsHelper.Spin">
            <summary>
            Sliding friction turned into spin 80.0 (used in demo videos) good for rest of default params below.  If 0.0 then no collision enhancement.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 100.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetCharacterCollisionsHelper.MaxVelocity">
            <summary>
            Torque = spin*(relative velocity) up to this maximum for relative velocity.
            </summary>
            <remarks>
            Default value = 8.0f.
            Min value = 0.0f.
            Max value = 100.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetCharacterCollisionsHelper.ApplyToAll">
            <summary>
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetCharacterCollisionsHelper.ApplyToSpine">
            <summary>
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetCharacterCollisionsHelper.ApplyToThighs">
            <summary>
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetCharacterCollisionsHelper.ApplyToClavicles">
            <summary>
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetCharacterCollisionsHelper.ApplyToUpperArms">
            <summary>
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetCharacterCollisionsHelper.FootSlip">
            <summary>
            Allow foot slipping if collided.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetCharacterCollisionsHelper.VehicleClass">
            <summary>
            ClassType of the object against which to enhance the collision.  All character vehicle interaction (e.g. braceForImpact glancing spins) relies on this value so EDIT WISELY. If it is used for things other than vehicles then NM should be informed.
            </summary>
            <remarks>
            Default value = 15.
            Min value = 0.
            Max value = 100.
            </remarks>
        </member>
        <member name="T:RDR2.NaturalMotion.SetCharacterDampingHelper">
            <summary>
            Damp out cartwheeling and somersaulting above a certain threshold.
            </summary>
        </member>
        <member name="M:RDR2.NaturalMotion.SetCharacterDampingHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the SetCharacterDampingHelper for sending a SetCharacterDamping <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the SetCharacterDamping <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
            <remarks>
            Damp out cartwheeling and somersaulting above a certain threshold.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetCharacterDampingHelper.SomersaultThresh">
            <summary>
            Somersault AngularMomentum measure above which we start damping - try 34.0.  Falling over straight backwards gives 54 on hitting ground.
            </summary>
            <remarks>
            Default value = 34.0f.
            Min value = 0.0f.
            Max value = 200.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetCharacterDampingHelper.SomersaultDamp">
            <summary>
            Amount to damp somersaulting by (spinning around left/right axis) - try 0.45.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetCharacterDampingHelper.CartwheelThresh">
            <summary>
            Cartwheel AngularMomentum measure above which we start damping - try 27.0.
            </summary>
            <remarks>
            Default value = 27.0f.
            Min value = 0.0f.
            Max value = 200.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetCharacterDampingHelper.CartwheelDamp">
            <summary>
            Amount to damp somersaulting by (spinning around front/back axis) - try 0.8.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetCharacterDampingHelper.VehicleCollisionTime">
            <summary>
            Time after impact with a vehicle to apply characterDamping. -ve values mean always apply whether collided with vehicle or not. =0.0 never apply. =timestep apply for only that frame.  A typical roll from being hit by a car lasts about 4secs.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = -1.0f.
            Max value = 1000.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetCharacterDampingHelper.V2">
            <summary>
            If true damping is proportional to Angular momentum squared.  If false proportional to Angular momentum.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="T:RDR2.NaturalMotion.SetFrictionScaleHelper">
            <summary>
            SetFrictionScale:.
            </summary>
        </member>
        <member name="M:RDR2.NaturalMotion.SetFrictionScaleHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the SetFrictionScaleHelper for sending a SetFrictionScale <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the SetFrictionScale <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
            <remarks>
            SetFrictionScale:.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetFrictionScaleHelper.Scale">
            <summary>
            Friction scale to be applied to parts in mask.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 10.0f.
```
