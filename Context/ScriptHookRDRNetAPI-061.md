<!-- ScriptHookRDRNetAPI lines 15001-15250 of 30005 -->
```csharp
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.TorqueGain">
            <summary>
            Multiplies impulse magnitude to arrive at torque that is applied.
            </summary>
            <remarks>
            Default value = 4.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.TorqueCutoff">
            <summary>
            Minimum ratio of impulse that remains after converting to torque (if in strength-proportional mode).
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.TorqueReductionPerTick">
            <summary>
            Ratio of torque for next tick (e.g. 1.0: not reducing over time, 0.9: each tick torque is reduced by 10%).
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.LiftGain">
            <summary>
            Amount of lift (directly multiplies torque axis to give lift force).
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.CounterImpulseDelay">
            <summary>
            Time after impulse is applied that counter impulse is applied.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.CounterImpulseMag">
            <summary>
            Amount of the original impulse that is countered.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.CounterAfterMagReached">
            <summary>
            Applies the counter impulse counterImpulseDelay(secs) after counterImpulseMag of the Impulse has been applied.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.DoCounterImpulse">
            <summary>
            Add a counter impulse to the pelvis.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.CounterImpulse2Hips">
            <summary>
            Amount of the counter impulse applied to hips - the rest is applied to the part originally hit.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.ImpulseNoBalMult">
            <summary>
            Amount to scale impulse by if the dynamicBalance is not OK.  1.0 means this functionality is not applied.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.ImpulseBalStabStart">
            <summary>
            100% LE Start to impulseBalStabMult*100% GT End. NB: Start LT End.
            </summary>
            <remarks>
            Default value = 3.0f.
            Min value = 0.0f.
            Max value = 100.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.ImpulseBalStabEnd">
            <summary>
            100% LE Start to impulseBalStabMult*100% GT End. NB: Start LT End.
            </summary>
            <remarks>
            Default value = 10.0f.
            Min value = 0.0f.
            Max value = 100.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.ImpulseBalStabMult">
            <summary>
            100% LE Start to impulseBalStabMult*100% GT End. NB: leaving this as 1.0 means this functionality is not applied and Start and End have no effect.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.ImpulseSpineAngStart">
            <summary>
            100% GE Start to impulseSpineAngMult*100% LT End. NB: Start GT End.  This the dot of hip2Head with up.
            </summary>
            <remarks>
            Default value = 0.7f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.ImpulseSpineAngEnd">
            <summary>
            100% GE Start to impulseSpineAngMult*100% LT End. NB: Start GT End.  This the dot of hip2Head with up.
            </summary>
            <remarks>
            Default value = 0.2f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.ImpulseSpineAngMult">
            <summary>
            100% GE Start to impulseSpineAngMult*100% LT End. NB: leaving this as 1.0 means this functionality is not applied and Start and End have no effect.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.ImpulseVelStart">
            <summary>
            100% LE Start to impulseVelMult*100% GT End. NB: Start LT End.
            </summary>
            <remarks>
            Default value = 4.0f.
            Min value = 0.0f.
            Max value = 100.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.ImpulseVelEnd">
            <summary>
            100% LE Start to impulseVelMult*100% GT End. NB: Start LT End.
            </summary>
            <remarks>
            Default value = 4.0f.
            Min value = 0.0f.
            Max value = 100.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.ImpulseVelMult">
            <summary>
            100% LE Start to impulseVelMult*100% GT End. NB: leaving this as 1.0 means this functionality is not applied and Start and End have no effect.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.ImpulseAirMult">
            <summary>
            Amount to scale impulse by if the character is airborne and dynamicBalance is OK and impulse is above impulseAirMultStart.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.ImpulseAirMultStart">
            <summary>
            If impulse is above this value scale it by impulseAirMult.
            </summary>
            <remarks>
            Default value = 100.0f.
            Min value = 0.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.ImpulseAirMax">
            <summary>
            Amount to clamp impulse to if character is airborne  and dynamicBalance is OK.
            </summary>
            <remarks>
            Default value = 100.0f.
            Min value = 0.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.ImpulseAirApplyAbove">
            <summary>
            If impulse is above this amount then do not scale/clamp just let it through as is - it's a shotgun or cannon.
            </summary>
            <remarks>
            Default value = 399.0f.
            Min value = 0.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.ImpulseAirOn">
            <summary>
            Scale and/or clamp impulse if the character is airborne and dynamicBalance is OK.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.ImpulseOneLegMult">
            <summary>
            Amount to scale impulse by if the character is contacting with one foot only and dynamicBalance is OK and impulse is above impulseAirMultStart.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsExtraHelper.ImpulseOneLegMultStart">
            <summary>
```
