<!-- ScriptHookRDRNetAPI lines 21501-21750 of 30005 -->
```csharp
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HighFallHelper.SideD">
            <summary>
            Side offset for the feet during prepareForLanding. +ve = right.
            </summary>
            <remarks>
            Default value = 0.2f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HighFallHelper.FowardOffsetOfLegIK">
            <summary>
            Forward offset for the feet during prepareForLanding.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HighFallHelper.LegL">
            <summary>
            Leg Length for ik (Internal)//unused.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HighFallHelper.CatchFallCutOff">
            <summary>
            0.5angle is 0.878 dot. Cutoff to go to the catchFall ( internal) //mmmtodo do like crashOrLandCutOff.
            </summary>
            <remarks>
            Default value = 0.9f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HighFallHelper.LegStrength">
            <summary>
            Strength of the legs at landing.
            </summary>
            <remarks>
            Default value = 12.0f.
            Min value = 6.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HighFallHelper.Balance">
            <summary>
            If true have enough strength to balance.  If false not enough strength in legs to balance (even though bodyBlance called).
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HighFallHelper.IgnorWorldCollisions">
            <summary>
            Never go into bailout (foetal).
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HighFallHelper.AdaptiveCircling">
            <summary>
            Stunt man type fall.
            Arm and legs circling direction controlled by angmom and orientation.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HighFallHelper.Hula">
            <summary>
            With stunt man type fall.
            Hula reaction if can't see floor and not rotating fast.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HighFallHelper.MaxSpeedForRecoverableFall">
            <summary>
            Character needs to be moving less than this speed to consider fall as a recoverable one.
            </summary>
            <remarks>
            Default value = 15.0f.
            Min value = 0.0f.
            Max value = 100.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HighFallHelper.MinSpeedForBrace">
            <summary>
            Character needs to be moving at least this fast horizontally to start bracing for impact if there is an object along its trajectory.
            </summary>
            <remarks>
            Default value = 10.0f.
            Min value = 0.0f.
            Max value = 100.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.HighFallHelper.LandingNormal">
            <summary>
            Ray-cast normal doted with up direction has to be greater than this number to consider object flat enough to land on it.
            </summary>
            <remarks>
            Default value = 0.6f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.IncomingTransformsHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the IncomingTransformsHelper for sending a IncomingTransforms <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the IncomingTransforms <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="T:RDR2.NaturalMotion.InjuredOnGroundHelper">
            <summary>
            InjuredOnGround.
            </summary>
        </member>
        <member name="M:RDR2.NaturalMotion.InjuredOnGroundHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the InjuredOnGroundHelper for sending a InjuredOnGround <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the InjuredOnGround <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
            <remarks>
            InjuredOnGround.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.InjuredOnGroundHelper.NumInjuries">
            <summary>
            </summary>
            <remarks>
            Default value = 0.
            Min value = 0.
            Max value = 2.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.InjuredOnGroundHelper.Injury1Component">
            <summary>
            </summary>
            <remarks>
            Default value = 0.
            Min value = 0.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.InjuredOnGroundHelper.Injury2Component">
            <summary>
            </summary>
            <remarks>
            Default value = 0.
            Min value = 0.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.InjuredOnGroundHelper.Injury1LocalPosition">
            <summary>
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.InjuredOnGroundHelper.Injury2LocalPosition">
            <summary>
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.InjuredOnGroundHelper.Injury1LocalNormal">
            <summary>
            </summary>
            <remarks>
            Default value = Vector3(1.0f, 0.0f, 0.0f).
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.InjuredOnGroundHelper.Injury2LocalNormal">
            <summary>
            </summary>
            <remarks>
            Default value = Vector3(1.0f, 0.0f, 0.0f).
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.InjuredOnGroundHelper.AttackerPos">
            <summary>
            </summary>
            <remarks>
            Default value = Vector3(1.0f, 0.0f, 0.0f).
            Min value = 0.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.InjuredOnGroundHelper.DontReachWithLeft">
            <summary>
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.InjuredOnGroundHelper.DontReachWithRight">
            <summary>
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.InjuredOnGroundHelper.StrongRollForce">
            <summary>
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="T:RDR2.NaturalMotion.CarriedHelper">
            <summary>
            Carried.
            </summary>
        </member>
        <member name="M:RDR2.NaturalMotion.CarriedHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the CarriedHelper for sending a Carried <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the Carried <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
            <remarks>
            Carried.
            </remarks>
        </member>
        <member name="T:RDR2.NaturalMotion.DangleHelper">
            <summary>
            Dangle.
            </summary>
        </member>
        <member name="M:RDR2.NaturalMotion.DangleHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the DangleHelper for sending a Dangle <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the Dangle <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
            <remarks>
            Dangle.
            </remarks>
```
