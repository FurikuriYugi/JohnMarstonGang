<!-- ScriptHookRDRNetAPI lines 16751-17000 of 30005 -->
```csharp
            The sink rate (higher for a faster drop).
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StayUprightHelper.LastStandHorizDamping">
            <summary>
            Higher values for more damping.
            </summary>
            <remarks>
            Default value = 0.4f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StayUprightHelper.LastStandMaxTime">
            <summary>
            Max time allowed in last stand mode.
            </summary>
            <remarks>
            Default value = 0.4f.
            Min value = 0.0f.
            Max value = 5.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StayUprightHelper.TurnTowardsBullets">
            <summary>
            Use cheat torques to face the direction of bullets if not facing too far away.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StayUprightHelper.VelocityBased">
            <summary>
            Make strength of constraint function of COM velocity.  Uses -1 for forceDamping if the damping is positive.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StayUprightHelper.TorqueOnlyInAir">
            <summary>
            Only apply torque based constraint when airBorne.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StayUprightHelper.ForceStrength">
            <summary>
            Strength of constraint.
            </summary>
            <remarks>
            Default value = 3.0f.
            Min value = 0.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StayUprightHelper.ForceDamping">
            <summary>
            Damping in constraint: -1 makes it scale automagically with forceStrength.  Other negative values will scale this automagic damping.
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.0f.
            Max value = 50.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StayUprightHelper.ForceFeetMult">
            <summary>
            Multiplier to the force applied to the feet.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StayUprightHelper.ForceSpine3Share">
            <summary>
            Share of pelvis force applied to spine3.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StayUprightHelper.ForceLeanReduction">
            <summary>
            How much the character lean is taken into account when reducing the force.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StayUprightHelper.ForceInAirShare">
            <summary>
            Share of the feet force to the airborne foot.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StayUprightHelper.ForceMin">
            <summary>
            When min and max are greater than 0 the constraint strength is determined from character strength, scaled into the range given by min and max.
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StayUprightHelper.ForceMax">
            <summary>
            See above.
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StayUprightHelper.ForceSaturationVel">
            <summary>
            When in velocityBased mode, the COM velocity at which constraint reaches maximum strength (forceStrength).
            </summary>
            <remarks>
            Default value = 4.0f.
            Min value = 0.1f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StayUprightHelper.ForceThresholdVel">
            <summary>
            When in velocityBased mode, the COM velocity above which constraint starts applying forces.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 5.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StayUprightHelper.TorqueStrength">
            <summary>
            Strength of torque based constraint.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StayUprightHelper.TorqueDamping">
            <summary>
            Damping of torque based constraint.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StayUprightHelper.TorqueSaturationVel">
            <summary>
            When in velocityBased mode, the COM velocity at which constraint reaches maximum strength (torqueStrength).
            </summary>
            <remarks>
            Default value = 4.0f.
            Min value = 0.1f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StayUprightHelper.TorqueThresholdVel">
            <summary>
            When in velocityBased mode, the COM velocity above which constraint starts applying torques.
            </summary>
            <remarks>
            Default value = 2.5f.
            Min value = 0.0f.
            Max value = 5.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StayUprightHelper.SupportPosition">
            <summary>
            Distance the foot is behind Com projection that is still considered able to generate the support for the upright constraint.
            </summary>
            <remarks>
            Default value = 2.0f.
            Min value = -2.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StayUprightHelper.NoSupportForceMult">
            <summary>
            Still apply this fraction of the upright constaint force if the foot is not in a position (defined by supportPosition) to generate the support for the upright constraint.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StayUprightHelper.StepUpHelp">
            <summary>
            Strength of cheat force applied upwards to spine3 to help the character up steps/slopes.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StayUprightHelper.StayUpAcc">
            <summary>
            How much the cheat force takes into account the acceleration of moving platforms.
            </summary>
            <remarks>
            Default value = 0.7f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StayUprightHelper.StayUpAccMax">
            <summary>
            The maximum floorAcceleration (of a moving platform) that the cheat force takes into account.
            </summary>
            <remarks>
            Default value = 5.0f.
            Min value = 0.0f.
            Max value = 15.0f.
            </remarks>
        </member>
        <member name="T:RDR2.NaturalMotion.StopAllBehavioursHelper">
            <summary>
            Send this message to immediately stop all behaviors from executing.
            </summary>
        </member>
        <member name="M:RDR2.NaturalMotion.StopAllBehavioursHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the StopAllBehaviorsHelper for sending a StopAllBehaviors <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
```
