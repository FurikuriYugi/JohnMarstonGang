<!-- ScriptHookRDRNetAPI lines 19501-19750 of 30005 -->
```csharp
            Max value = 4.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyWritheHelper.BackPeriod">
            <summary>
            Controls how fast the writhe is executed, smaller values make faster motions.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 4.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyWritheHelper.LegPeriod">
            <summary>
            Controls how fast the writhe is executed, smaller values make faster motions.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 4.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyWritheHelper.Mask">
            <summary>
            Two character body-masking value, bitwise joint mask or bitwise logic string of two character body-masking value  (see Active Pose notes for possible values).
            </summary>
            <remarks>
            Default value = "fb".
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyWritheHelper.ArmAmplitude">
            <summary>
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 3.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyWritheHelper.BackAmplitude">
            <summary>
            Scales the amount of writhe. 0 = no writhe.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 3.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyWritheHelper.LegAmplitude">
            <summary>
            Scales the amount of writhe. 0 = no writhe.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 3.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyWritheHelper.ElbowAmplitude">
            <summary>
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 3.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyWritheHelper.KneeAmplitude">
            <summary>
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 3.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyWritheHelper.RollOverFlag">
            <summary>
            Flag to set trying to rollOver.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyWritheHelper.BlendArms">
            <summary>
            Blend the writhe arms with the current desired arms (0=don't apply any writhe, 1=only writhe).
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyWritheHelper.BlendBack">
            <summary>
            Blend the writhe spine and neck with the current desired (0=don't apply any writhe, 1=only writhe).
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyWritheHelper.BlendLegs">
            <summary>
            Blend the writhe legs with the current desired legs (0=don't apply any writhe, 1=only writhe).
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyWritheHelper.ApplyStiffness">
            <summary>
            Use writhe stiffnesses if true. If false don't set any stiffnesses.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyWritheHelper.OnFire">
            <summary>
            Extra shoulderBlend. Rolling:one way only, maxRollOverTime, rollOverRadius, doesn't reduce arm stiffness to help rolling. No shoulder twist.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyWritheHelper.ShoulderLean1">
            <summary>
            Blend writhe shoulder desired lean1 with this angle in RAD. Note that onFire has to be set to true for this parameter to take any effect.
            </summary>
            <remarks>
            Default value = 0.7f.
            Min value = 0.0f.
            Max value = 6.3f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyWritheHelper.ShoulderLean2">
            <summary>
            Blend writhe shoulder desired lean2 with this angle in RAD. Note that onFire has to be set to true for this parameter to take any effect.
            </summary>
            <remarks>
            Default value = 0.4f.
            Min value = 0.0f.
            Max value = 6.3f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyWritheHelper.Lean1BlendFactor">
            <summary>
            Shoulder desired lean1 with shoulderLean1 angle blend factor. Set it to 0 to use original shoulder withe desired lean1 angle for shoulders. Note that onFire has to be set to true for this parameter to take any effect.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyWritheHelper.Lean2BlendFactor">
            <summary>
            Shoulder desired lean2 with shoulderLean2 angle blend factor. Set it to 0 to use original shoulder withe desired lean2 angle for shoulders. Note that onFire has to be set to true for this parameter to take any effect.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyWritheHelper.RollTorqueScale">
            <summary>
            Scale rolling torque that is applied to character spine.
            </summary>
            <remarks>
            Default value = 150.0f.
            Min value = 0.0f.
            Max value = 300.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyWritheHelper.MaxRollOverTime">
            <summary>
            Rolling torque is ramped down over time. At this time in seconds torque value converges to zero. Use this parameter to restrict time the character is rolling. Note that onFire has to be set to true for this parameter to take any effect.
            </summary>
            <remarks>
            Default value = 8.0f.
            Min value = 0.0f.
            Max value = 60.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyWritheHelper.RollOverRadius">
            <summary>
            Rolling torque is ramped down with distance measured from position where character hit the ground and started rolling. At this distance in meters torque value converges to zero. Use this parameter to restrict distance the character travels due to rolling. Note that onFire has to be set to true for this parameter to take any effect.
            </summary>
            <remarks>
            Default value = 2.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.BraceForImpactHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the BraceForImpactHelper for sending a BraceForImpact <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the BraceForImpact <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.BraceDistance">
            <summary>
            Distance from object at which to raise hands to brace 0.5 good if newBrace=true - otherwise 0.65.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.TargetPredictionTime">
            <summary>
            Time epected to get arms up from idle.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.ReachAbsorbtionTime">
            <summary>
            Larger values and he absorbs the impact more.
            </summary>
            <remarks>
            Default value = 0.2f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.InstanceIndex">
            <summary>
            LevelIndex of object to brace.
            </summary>
            <remarks>
            Default value = -1.
            Min value = -1.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.BodyStiffness">
            <summary>
            Stiffness of character. Catch_fall stiffness scales with this too, with its defaults at this values default.
```
