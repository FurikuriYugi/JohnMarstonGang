<!-- ScriptHookRDRNetAPI lines 25751-26000 of 30005 -->
```csharp
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SmartFallHelper.SpreadLegs">
            <summary>
            Spread legs amount for pedal during fall.
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.StaggerFallHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the StaggerFallHelper for sending a StaggerFall <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the StaggerFall <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.ArmStiffness">
            <summary>
            Stiffness of arms. Catch_fall's stiffness scales with this value, but has default values when this is default.
            </summary>
            <remarks>
            Default value = 12.0f.
            Min value = 0.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.ArmDamping">
            <summary>
            Sets damping value for the arms.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.SpineDamping">
            <summary>
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.SpineStiffness">
            <summary>
            </summary>
            <remarks>
            Default value = 10.0f.
            Min value = 0.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.ArmStiffnessStart">
            <summary>
            ArmStiffness during the yanked timescale i.e. timeAtStartValues.
            </summary>
            <remarks>
            Default value = 3.0f.
            Min value = 0.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.ArmDampingStart">
            <summary>
            ArmDamping during the yanked timescale i.e. timeAtStartValues.
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.SpineDampingStart">
            <summary>
            SpineDamping during the yanked timescale i.e. timeAtStartValues.
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.SpineStiffnessStart">
            <summary>
            SpineStiffness during the yanked timescale i.e. timeAtStartValues.
            </summary>
            <remarks>
            Default value = 3.0f.
            Min value = 0.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.TimeAtStartValues">
            <summary>
            Time spent with Start values for arms and spine stiffness and damping i.e. for whiplash effect.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.RampTimeFromStartValues">
            <summary>
            Time spent ramping from Start to end values for arms and spine stiffness and damping i.e. for whiplash effect (occurs after timeAtStartValues).
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.StaggerStepProb">
            <summary>
            Probability per step of time spent in a stagger step.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.StepsTillStartEnd">
            <summary>
            Steps taken before lowerBodyStiffness starts ramping down by perStepReduction1.
            </summary>
            <remarks>
            Default value = 2.
            Min value = 0.
            Max value = 100.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.TimeStartEnd">
            <summary>
            Time from start of behavior before lowerBodyStiffness starts ramping down for rampTimeToEndValues to endValues.
            </summary>
            <remarks>
            Default value = 100.0f.
            Min value = 0.0f.
            Max value = 100.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.RampTimeToEndValues">
            <summary>
            Time spent ramping from lowerBodyStiffness to lowerBodyStiffnessEnd.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.LowerBodyStiffness">
            <summary>
            LowerBodyStiffness should be 12.
            </summary>
            <remarks>
            Default value = 13.0f.
            Min value = 0.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.LowerBodyStiffnessEnd">
            <summary>
            LowerBodyStiffness at end.
            </summary>
            <remarks>
            Default value = 8.0f.
            Min value = 0.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.PredictionTime">
            <summary>
            Amount of time (seconds) into the future that the character tries to step to.
            Bigger values try to recover with fewer, bigger steps.
            Smaller values recover with smaller steps, and generally recover less.
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.PerStepReduction1">
            <summary>
            LowerBody stiffness will be reduced every step to make the character fallover.
            </summary>
            <remarks>
            Default value = 0.7f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.LeanInDirRate">
            <summary>
            LeanInDirection will be increased from 0 to leanInDirMax linearly at this rate.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.LeanInDirMaxF">
            <summary>
            Max of leanInDirection magnitude when going forwards.
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.LeanInDirMaxB">
            <summary>
            Max of leanInDirection magnitude when going backwards.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.LeanHipsMaxF">
            <summary>
            Max of leanInDirectionHips magnitude when going forwards.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.LeanHipsMaxB">
            <summary>
            Max of leanInDirectionHips magnitude when going backwards.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.Lean2multF">
            <summary>
```
