<!-- ScriptHookRDRNetAPI lines 19251-19500 of 30005 -->
```csharp
        <member name="P:RDR2.NaturalMotion.BodyFoetalHelper.Stiffness">
            <summary>
            The stiffness of the body determines how fast the character moves into the position, and how well that they hold it.
            </summary>
            <remarks>
            Default value = 9.0f.
            Min value = 6.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyFoetalHelper.DampingFactor">
            <summary>
            Sets damping value for the character joints.
            </summary>
            <remarks>
            Default value = 1.4f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyFoetalHelper.Asymmetry">
            <summary>
            A value between 0-1 that controls how asymmetric the results are by varying stiffness across the body.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyFoetalHelper.RandomSeed">
            <summary>
            Random seed used to generate asymmetry values.
            </summary>
            <remarks>
            Default value = 100.
            Min value = 0.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyFoetalHelper.BackTwist">
            <summary>
            Amount of random back twist to add.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyFoetalHelper.Mask">
            <summary>
            Two character body-masking value, bitwise joint mask or bitwise logic string of two character body-masking value  (see Active Pose notes for possible values).
            </summary>
            <remarks>
            Default value = "fb".
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.BodyRollUpHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the BodyRollUpHelper for sending a BodyRollUp <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the BodyRollUp <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyRollUpHelper.Stiffness">
            <summary>
            Stiffness of whole body.
            </summary>
            <remarks>
            Default value = 10.0f.
            Min value = 6.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyRollUpHelper.UseArmToSlowDown">
            <summary>
            The degree to which the character will try to stop a barrel roll with his arms.
            </summary>
            <remarks>
            Default value = 1.3f.
            Min value = -2.0f.
            Max value = 3.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyRollUpHelper.ArmReachAmount">
            <summary>
            The likeliness of the character reaching for the ground with its arms.
            </summary>
            <remarks>
            Default value = 1.4f.
            Min value = 0.0f.
            Max value = 3.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyRollUpHelper.Mask">
            <summary>
            Two character body-masking value, bitwise joint mask or bitwise logic string of two character body-masking value  (see Active Pose notes for possible values).
            </summary>
            <remarks>
            Default value = "fb".
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyRollUpHelper.LegPush">
            <summary>
            Used to keep rolling down slope, 1 is full (kicks legs out when pointing upwards).
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = -1.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyRollUpHelper.AsymmetricalLegs">
            <summary>
            0 is no leg asymmetry in 'foetal' position.  greater than 0 a asymmetricalLegs-rand(30%), added/minus each joint of the legs in radians.  Random number changes about once every roll.  0.4 gives a lot of asymmetry.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = -2.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyRollUpHelper.NoRollTimeBeforeSuccess">
            <summary>
            Time that roll velocity has to be lower than rollVelForSuccess, before success message is sent.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyRollUpHelper.RollVelForSuccess">
            <summary>
            Lower threshold for roll velocity at which success message can be sent.
            </summary>
            <remarks>
            Default value = 0.2f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyRollUpHelper.RollVelLinearContribution">
            <summary>
            Contribution of linear COM velocity to roll Velocity (if 0, roll velocity equal to COM angular velocity).
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyRollUpHelper.VelocityScale">
            <summary>
            Scales perceived body velocity.  The higher this value gets, the more quickly the velocity measure saturates, resulting in a tighter roll at slower speeds. (NB: Set to 1 to match earlier behavior).
            </summary>
            <remarks>
            Default value = 0.2f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyRollUpHelper.VelocityOffset">
            <summary>
            Offsets perceived body velocity.  Increase to create larger "dead zone" around zero velocity where character will be less rolled. (NB: Reset to 0 to match earlier behavior).
            </summary>
            <remarks>
            Default value = 2.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyRollUpHelper.ApplyMinMaxFriction">
            <summary>
            Controls whether or not behavior enforces min/max friction.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.BodyWritheHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the BodyWritheHelper for sending a BodyWrithe <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the BodyWrithe <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyWritheHelper.ArmStiffness">
            <summary>
            </summary>
            <remarks>
            Default value = 13.0f.
            Min value = 6.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyWritheHelper.BackStiffness">
            <summary>
            </summary>
            <remarks>
            Default value = 13.0f.
            Min value = 6.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyWritheHelper.LegStiffness">
            <summary>
            The stiffness of the character will determine how 'determined' a writhe this is - high values will make him thrash about wildly.
            </summary>
            <remarks>
            Default value = 13.0f.
            Min value = 6.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyWritheHelper.ArmDamping">
            <summary>
            Damping amount, less is underdamped.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 3.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyWritheHelper.BackDamping">
            <summary>
            Damping amount, less is underdamped.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 3.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyWritheHelper.LegDamping">
            <summary>
            Damping amount, less is underdamped.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 3.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyWritheHelper.ArmPeriod">
            <summary>
            Controls how fast the writhe is executed, smaller values make faster motions.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
```
