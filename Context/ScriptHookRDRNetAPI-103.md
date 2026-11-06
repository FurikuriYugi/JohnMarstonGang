<!-- ScriptHookRDRNetAPI lines 25501-25750 of 30005 -->
```csharp
            <summary>
            Strength of the legs at landing.
            </summary>
            <remarks>
            Default value = 12.0f.
            Min value = 6.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.Balance">
            <summary>
            If true have enough strength to balance.
            If false not enough strength in legs to balance (even though bodyBlance called).
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.IgnorWorldCollisions">
            <summary>
            Never go into bailout (foetal).
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.AdaptiveCircling">
            <summary>
            Stunt man type fall.  Arm and legs circling direction controlled by angmom and orientation.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.Hula">
            <summary>
            With stunt man type fall.  Hula reaction if can't see floor and not rotating fast.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.MaxSpeedForRecoverableFall">
            <summary>
            Character needs to be moving less than this speed to consider fall as a recoverable one.
            </summary>
            <remarks>
            Default value = 15.0f.
            Min value = 0.0f.
            Max value = 100.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.MinSpeedForBrace">
            <summary>
            Character needs to be moving at least this fast horizontally to start bracing for impact if there is an object along its trajectory.
            </summary>
            <remarks>
            Default value = 10.0f.
            Min value = 0.0f.
            Max value = 100.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.LandingNormal">
            <summary>
            Ray-cast normal doted with up direction has to be greater than this number to consider object flat enough to land on it.
            </summary>
            <remarks>
            Default value = 0.6f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.RdsForceMag">
            <summary>
            </summary>
            <remarks>
            Default value = 0.8f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.RdsTargetLinVeDecayTime">
            <summary>
            RDS: Time for the targetlinearVelocity to decay to zero.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.RdsTargetLinearVelocity">
            <summary>
            RDS: Helper torques are applied to match the spin of the character to the max of targetLinearVelocity and COMVelMag.
            -1 to use initial character velocity.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 30.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.RdsUseStartingFriction">
            <summary>
            Start Catch Fall/RDS state with specified friction.
            Catch fall will overwrite based on setFallingReaction.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.RdsStartingFriction">
            <summary>
            Catch Fall/RDS starting friction.
            Catch fall will overwrite based on setFallingReaction.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.RdsStartingFrictionMin">
            <summary>
            Catch Fall/RDS starting friction minimum.
            Catch fall will overwrite based on setFallingReaction.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.RdsForceVelThreshold">
            <summary>
            Velocity threshold under which RDS force mag will be applied.
            </summary>
            <remarks>
            Default value = 10.0f.
            Min value = 0.0f.
            Max value = 100.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.InitialState">
            <summary>
            Force initial state (used in vehicle bail out to start SF_CatchFall (6) earlier.
            </summary>
            <remarks>
            Default value = 0.
            Min value = 0.
            Max value = 7.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.ChangeExtremityFriction">
            <summary>
            Allow friction changes to be applied to the hands and feet.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.Teeter">
            <summary>
            Set up an immediate teeter in the direction of trave if initial state is SF_Balance.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.TeeterOffset">
            <summary>
            Offset the default Teeter edge in the direction of travel.
            Will need to be tweaked depending on how close to the real edge AI tends to trigger the behavior.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.StopRollingTime">
            <summary>
            Time in seconds before ped should start actively trying to stop rolling.
            </summary>
            <remarks>
            Default value = 2.0f.
            Min value = 0.0f.
            Max value = 100.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.ReboundScale">
            <summary>
            Scale for rebound assistance.
            0 = off,
            1 = very bouncy,
            2 = jbone crazy.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.ReboundMask">
            <summary>
            Part mask to apply rebound assistance.
            </summary>
            <remarks>
            Default value = uk.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.ForceHeadAvoid">
            <summary>
            Force head avoid to be active during Catch Fall even when character is not on the ground.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.CfZAxisSpinReduction">
            <summary>
            Pass-through parameter for Catch Fall spin reduction.  Increase to stop more spin. 0..1.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.SplatWhenStopped">
            <summary>
            Transition to splat state when com vel is below value, regardless of character health or fall velocity.
            Set to zero to disable.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 100.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.BlendHeadWhenStopped">
            <summary>
            Blend head to neutral pose com vel approaches zero.
            Linear between zero and value.
            Set to zero to disable.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 100.0f.
```
