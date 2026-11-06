<!-- ScriptHookRDRNetAPI lines 21751-22000 of 30005 -->
```csharp
        </member>
        <member name="P:RDR2.NaturalMotion.DangleHelper.DoGrab">
            <summary>
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.DangleHelper.GrabFrequency">
            <summary>
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.OnFireHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the OnFireHelper for sending a OnFire <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the OnFire <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.OnFireHelper.StaggerTime">
            <summary>
            Max time for stumbling around before falling to ground.
            </summary>
            <remarks>
            Default value = 2.5f.
            Min value = 0.0f.
            Max value = 30.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.OnFireHelper.StaggerLeanRate">
            <summary>
            How quickly the character leans hips when staggering.
            </summary>
            <remarks>
            Default value = 0.9f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.OnFireHelper.StumbleMaxLeanBack">
            <summary>
            Max the character leans hips back when staggering.
            </summary>
            <remarks>
            Default value = 0.4f.
            Min value = 0.0f.
            Max value = 1.5f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.OnFireHelper.StumbleMaxLeanForward">
            <summary>
            Max the character leans hips forwards when staggering.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 1.5f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.OnFireHelper.ArmsWindmillWritheBlend">
            <summary>
            Blend armsWindmill with the bodyWrithe arms when character is upright.
            </summary>
            <remarks>
            Default value = 0.4f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.OnFireHelper.SpineStumbleWritheBlend">
            <summary>
            Blend spine stumble with the bodyWrithe spine when character is upright.
            </summary>
            <remarks>
            Default value = 0.7f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.OnFireHelper.LegsStumbleWritheBlend">
            <summary>
            Blend legs stumble with the bodyWrithe legs when character is upright.
            </summary>
            <remarks>
            Default value = 0.2f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.OnFireHelper.ArmsPoseWritheBlend">
            <summary>
            Blend the bodyWrithe arms with the current desired pose from on fire behavior when character is on the floor.
            </summary>
            <remarks>
            Default value = 0.7f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.OnFireHelper.SpinePoseWritheBlend">
            <summary>
            Blend the bodyWrithe back with the current desired pose from on fire behavior when character is on the floor.
            </summary>
            <remarks>
            Default value = 0.6f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.OnFireHelper.LegsPoseWritheBlend">
            <summary>
            Blend the bodyWrithe legs with the current desired pose from on fire behavior when character is on the floor.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.OnFireHelper.RollOverFlag">
            <summary>
            Flag to set bodyWrithe trying to rollOver.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.OnFireHelper.RollTorqueScale">
            <summary>
            Scale rolling torque that is applied to character spine by bodyWrithe. Torque magnitude is calculated with the following formula: m_rollOverDirection*rollOverPhase*rollTorqueScale.
            </summary>
            <remarks>
            Default value = 25.0f.
            Min value = 0.0f.
            Max value = 300.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.OnFireHelper.PredictTime">
            <summary>
            Character pose depends on character facing direction that is evaluated from its COMTM orientation.
            Set this value to 0 to use no orientation prediction i.e. current character COMTM orientation will be used to determine character facing direction and finally the pose bodyWrithe is blending to.
            Set this value to  GT  0 to predict character COMTM orientation this amount of time in seconds to the future.
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.OnFireHelper.MaxRollOverTime">
            <summary>
            Rolling torque is ramped down over time. At this time in seconds torque value converges to zero. Use this parameter to restrict time the character is rolling.
            </summary>
            <remarks>
            Default value = 8.0f.
            Min value = 0.0f.
            Max value = 60.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.OnFireHelper.RollOverRadius">
            <summary>
            Rolling torque is ramped down with distance measured from position where character hit the ground and started rolling. At this distance in meters torque value converges to zero. Use this parameter to restrict distance the character travels due to rolling.
            </summary>
            <remarks>
            Default value = 2.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.PedalLegsHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the PedalLegsHelper for sending a PedalLegs <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the PedalLegs <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.PedalLegsHelper.PedalLeftLeg">
            <summary>
            Pedal with this leg or not.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PedalLegsHelper.PedalRightLeg">
            <summary>
            Pedal with this leg or not.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PedalLegsHelper.BackPedal">
            <summary>
            Pedal forwards or backwards.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PedalLegsHelper.Radius">
            <summary>
            Base radius of pedal action.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PedalLegsHelper.AngularSpeed">
            <summary>
            Rate of pedaling. If adaptivePedal4Dragging is true then the legsAngularSpeed calculated to match the linear speed of the character can have a maximum value of angularSpeed (this max used to be hard coded to 13.0).
            </summary>
            <remarks>
            Default value = 10.0f.
            Min value = 0.0f.
            Max value = 100.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PedalLegsHelper.LegStiffness">
            <summary>
            Stiffness of legs.
            </summary>
            <remarks>
            Default value = 10.0f.
            Min value = 6.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PedalLegsHelper.PedalOffset">
            <summary>
            Move the center of the pedal for the left leg up by this amount, the right leg down by this amount.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PedalLegsHelper.RandomSeed">
            <summary>
            Random seed used to generate speed changes.
            </summary>
            <remarks>
            Default value = 100.
            Min value = 0.
```
