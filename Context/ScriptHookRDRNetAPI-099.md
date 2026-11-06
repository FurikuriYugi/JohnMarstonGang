<!-- ScriptHookRDRNetAPI lines 24501-24750 of 30005 -->
```csharp
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotFallToKneesHelper.FtkReleasePointGun">
            <summary>
            Override the pointGun when knees hit.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotFallToKneesHelper.FtkFailMustCollide">
            <summary>
            The upper body of the character must be colliding and other failure conditions met to fail.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="T:RDR2.NaturalMotion.ShotFromBehindHelper">
            <summary>
            Configure the shot from behind reaction.
            </summary>
        </member>
        <member name="M:RDR2.NaturalMotion.ShotFromBehindHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the ShotFromBehindHelper for sending a ShotFromBehind <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the ShotFromBehind <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
            <remarks>
            Configure the shot from behind reaction.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotFromBehindHelper.ShotFromBehind">
            <summary>
            Type of reaction.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotFromBehindHelper.SfbSpineAmount">
            <summary>
            SpineBend.
            </summary>
            <remarks>
            Default value = 4.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotFromBehindHelper.SfbNeckAmount">
            <summary>
            Neck Bend.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotFromBehindHelper.SfbHipAmount">
            <summary>
            Hip Pitch.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotFromBehindHelper.SfbKneeAmount">
            <summary>
            Knee bend.
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotFromBehindHelper.SfbPeriod">
            <summary>
            ShotFromBehind reaction period after being shot.
            </summary>
            <remarks>
            Default value = 0.7f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotFromBehindHelper.SfbForceBalancePeriod">
            <summary>
            Amount of time not taking a step.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotFromBehindHelper.SfbArmsOnset">
            <summary>
            Amount of time before applying spread out arms pose.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotFromBehindHelper.SfbKneesOnset">
            <summary>
            Amount of time before bending knees a bit.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotFromBehindHelper.SfbNoiseGain">
            <summary>
            Controls additional independent randomized bending of left/right elbows.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotFromBehindHelper.SfbIgnoreFail">
            <summary>
            0 = balancer fails as normal,
            1 = ignore backArchedBack and leanedTooFarBack balancer failures,
            2 = ignore backArchedBack balancer failure only,
            3 = ignore leanedTooFarBack balancer failure only.
            </summary>
            <remarks>
            Default value = 0.
            Min value = 0.
            Max value = 3.
            </remarks>
        </member>
        <member name="T:RDR2.NaturalMotion.ShotInGutsHelper">
            <summary>
            Configure the shot in guts reaction.
            </summary>
        </member>
        <member name="M:RDR2.NaturalMotion.ShotInGutsHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the ShotInGutsHelper for sending a ShotInGuts <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the ShotInGuts <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
            <remarks>
            Configure the shot in guts reaction.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotInGutsHelper.ShotInGuts">
            <summary>
            Type of reaction.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotInGutsHelper.SigSpineAmount">
            <summary>
            SpineBend.
            </summary>
            <remarks>
            Default value = 2.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotInGutsHelper.SigNeckAmount">
            <summary>
            Neck Bend.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotInGutsHelper.SigHipAmount">
            <summary>
            Hip Pitch.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotInGutsHelper.SigKneeAmount">
            <summary>
            Knee bend.
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotInGutsHelper.SigPeriod">
            <summary>
            Active time after being shot.
            </summary>
            <remarks>
            Default value = 2.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotInGutsHelper.SigForceBalancePeriod">
            <summary>
            Amount of time not taking a step.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotInGutsHelper.SigKneesOnset">
            <summary>
            Amount of time not taking a step.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.ShotHeadLookHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the ShotHeadLookHelper for sending a ShotHeadLook <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the ShotHeadLook <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotHeadLookHelper.UseHeadLook">
            <summary>
            Use head look.
            Default: looks at provided target or if this is zero - looks forward or in velocity direction.
            If reachForWound is enabled, switches between looking at the wound and at the default target.
            </summary>
            <remarks>
            Default value = False.
```
