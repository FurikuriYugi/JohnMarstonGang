<!-- ScriptHookRDRNetAPI lines 14501-14750 of 30005 -->
```csharp
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.TorqueReductionPerTick">
            <summary>
            Ratio of torque for next tick (e.g. 1.0: not reducing over time, 0.9: each tick torque is reduced by 10%).
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.LiftGain">
            <summary>
            Amount of lift (directly multiplies torque axis to give lift force).
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.CounterImpulseDelay">
            <summary>
            Time after impulse is applied that counter impulse is applied.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.CounterImpulseMag">
            <summary>
            Amount of the original impulse that is countered.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.CounterAfterMagReached">
            <summary>
            Applies the counter impulse counterImpulseDelay(secs) after counterImpulseMag of the Impulse has been applied.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.DoCounterImpulse">
            <summary>
            Add a counter impulse to the pelvis.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.CounterImpulse2Hips">
            <summary>
            Amount of the counter impulse applied to hips - the rest is applied to the part originally hit.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.ImpulseNoBalMult">
            <summary>
            Amount to scale impulse by if the dynamicBalance is not OK.  1.0 means this functionality is not applied.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.ImpulseBalStabStart">
            <summary>
            100% LE Start to impulseBalStabMult*100% GT End. NB: Start LT End.
            </summary>
            <remarks>
            Default value = 3.0f.
            Min value = 0.0f.
            Max value = 100.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.ImpulseBalStabEnd">
            <summary>
            100% LE Start to impulseBalStabMult*100% GT End. NB: Start LT End.
            </summary>
            <remarks>
            Default value = 10.0f.
            Min value = 0.0f.
            Max value = 100.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.ImpulseBalStabMult">
            <summary>
            100% LE Start to impulseBalStabMult*100% GT End. NB: leaving this as 1.0 means this functionality is not applied and Start and End have no effect.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.ImpulseSpineAngStart">
            <summary>
            100% GE Start to impulseSpineAngMult*100% LT End. NB: Start GT End.  This the dot of hip2Head with up.
            </summary>
            <remarks>
            Default value = 0.7f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.ImpulseSpineAngEnd">
            <summary>
            100% GE Start to impulseSpineAngMult*100% LT End. NB: Start GT End.  This the dot of hip2Head with up.
            </summary>
            <remarks>
            Default value = 0.2f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.ImpulseSpineAngMult">
            <summary>
            100% GE Start to impulseSpineAngMult*100% LT End. NB: leaving this as 1.0 means this functionality is not applied and Start and End have no effect.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.ImpulseVelStart">
            <summary>
            100% LE Start to impulseVelMult*100% GT End. NB: Start LT End.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 100.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.ImpulseVelEnd">
            <summary>
            100% LE Start to impulseVelMult*100% GT End. NB: Start LT End.
            </summary>
            <remarks>
            Default value = 4.0f.
            Min value = 0.0f.
            Max value = 100.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.ImpulseVelMult">
            <summary>
            100% LE Start to impulseVelMult*100% GT End. NB: leaving this as 1.0 means this functionality is not applied and Start and End have no effect.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.ImpulseAirMult">
            <summary>
            Amount to scale impulse by if the character is airborne and dynamicBalance is OK and impulse is above impulseAirMultStart.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.ImpulseAirMultStart">
            <summary>
            If impulse is above this value scale it by impulseAirMult.
            </summary>
            <remarks>
            Default value = 100.0f.
            Min value = 0.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.ImpulseAirMax">
            <summary>
            Amount to clamp impulse to if character is airborne  and dynamicBalance is OK.
            </summary>
            <remarks>
            Default value = 100.0f.
            Min value = 0.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.ImpulseAirApplyAbove">
            <summary>
            If impulse is above this amount then do not scale/clamp just let it through as is - it's a shotgun or cannon.
            </summary>
            <remarks>
            Default value = 399.0f.
            Min value = 0.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.ImpulseAirOn">
            <summary>
            Scale and/or clamp impulse if the character is airborne and dynamicBalance is OK.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.ImpulseOneLegMult">
            <summary>
            Amount to scale impulse by if the character is contacting with one foot only and dynamicBalance is OK and impulse is above impulseAirMultStart.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.ImpulseOneLegMultStart">
            <summary>
            If impulse is above this value scale it by impulseOneLegMult.
            </summary>
            <remarks>
            Default value = 100.0f.
            Min value = 0.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.ImpulseOneLegMax">
            <summary>
            Amount to clamp impulse to if character is contacting with one foot only  and dynamicBalance is OK.
            </summary>
            <remarks>
            Default value = 100.0f.
            Min value = 0.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBulletsHelper.ImpulseOneLegApplyAbove">
            <summary>
            If impulse is above this amount then do not scale/clamp just let it through as is - it's a shotgun or cannon.
            </summary>
            <remarks>
            Default value = 399.0f.
            Min value = 0.0f.
            </remarks>
        </member>
```
