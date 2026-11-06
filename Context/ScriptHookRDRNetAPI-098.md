<!-- ScriptHookRDRNetAPI lines 24251-24500 of 30005 -->
```csharp
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotShockSpinHelper.BracedSideSpinMult">
            <summary>
            If shot on a side with a forward foot and both feet are on the ground and balanced, increase the shockspin to compensate for the balancer naturally resisting spin to that side.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 1.0f.
            Max value = 5.0f.
            </remarks>
        </member>
        <member name="T:RDR2.NaturalMotion.ShotFallToKneesHelper">
            <summary>
            Configure the fall to knees shot.
            </summary>
        </member>
        <member name="M:RDR2.NaturalMotion.ShotFallToKneesHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the ShotFallToKneesHelper for sending a ShotFallToKnees <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the ShotFallToKnees <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
            <remarks>
            Configure the fall to knees shot.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotFallToKneesHelper.FallToKnees">
            <summary>
            Type of reaction.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotFallToKneesHelper.FtkAlwaysChangeFall">
            <summary>
            Always change fall behavior.  If false only change when falling forward.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotFallToKneesHelper.FtkBalanceTime">
            <summary>
            How long the balancer runs for before fallToKnees starts.
            </summary>
            <remarks>
            Default value = 0.7f.
            Min value = 0.0f.
            Max value = 5.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotFallToKneesHelper.FtkHelperForce">
            <summary>
            Hip helper force magnitude - to help character lean over balance point of line between toes.
            </summary>
            <remarks>
            Default value = 200.0f.
            Min value = 0.0f.
            Max value = 2000.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotFallToKneesHelper.FtkHelperForceOnSpine">
            <summary>
            Helper force applied to spine3 as well.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotFallToKneesHelper.FtkLeanHelp">
            <summary>
            Help balancer lean amount - to help character lean over balance point of line between toes.
            Half of this is also applied as hipLean.
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.0f.
            Max value = 0.3f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotFallToKneesHelper.FtkSpineBend">
            <summary>
            Bend applied to spine when falling from knees. (+ve forward - try -0.1) (only if rds called).
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = -0.2f.
            Max value = 0.3f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotFallToKneesHelper.FtkStiffSpine">
            <summary>
            Stiffen spine when falling from knees (only if rds called).
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotFallToKneesHelper.FtkImpactLooseness">
            <summary>
            Looseness (muscleStiffness = 1.01f - m_parameters.ftkImpactLooseness) applied to upperBody on knee impacts.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotFallToKneesHelper.FtkImpactLoosenessTime">
            <summary>
            Time that looseness is applied after knee impacts.
            </summary>
            <remarks>
            Default value = 0.2f.
            Min value = -0.1f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotFallToKneesHelper.FtkBendRate">
            <summary>
            Rate at which the legs are bent to go from standing to on knees.
            </summary>
            <remarks>
            Default value = 0.7f.
            Min value = 0.0f.
            Max value = 4.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotFallToKneesHelper.FtkHipBlend">
            <summary>
            Blend from current hip to balancing on knees hip angle.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotFallToKneesHelper.FtkLungeProb">
            <summary>
            Probability that a lunge reaction will be allowed.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotFallToKneesHelper.FtkKneeSpin">
            <summary>
            When on knees allow some spinning of the character.
            If false then the balancers' footSlipCompensation remains on and tends to keep the character facing the same way as when it was balancing.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotFallToKneesHelper.FtkFricMult">
            <summary>
            Multiplier on the reduction of friction for the feet based on angle away from horizontal - helps the character fall to knees quicker.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 5.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotFallToKneesHelper.FtkHipAngleFall">
            <summary>
            Apply this hip angle when the character starts to fall backwards when on knees.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotFallToKneesHelper.FtkPitchForwards">
            <summary>
            Hip pitch applied (+ve forward, -ve backwards) if character is falling forwards on way down to it's knees.
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = -0.5f.
            Max value = 0.5f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotFallToKneesHelper.FtkPitchBackwards">
            <summary>
            Hip pitch applied (+ve forward, -ve backwards) if character is falling backwards on way down to it's knees.
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = -0.5f.
            Max value = 0.5f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotFallToKneesHelper.FtkFallBelowStab">
            <summary>
            Balancer instability below which the character starts to bend legs even if it isn't going to fall on to it's knees (i.e. if going backwards).
            0.3 almost ensures a fall to knees but means the character will keep stepping backward until it slows down enough.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 15.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotFallToKneesHelper.FtkBalanceAbortThreshold">
            <summary>
            When the character gives up and goes into a fall.
            </summary>
            <remarks>
            Default value = 2.0f.
            Min value = 0.0f.
            Max value = 4.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotFallToKneesHelper.FtkOnKneesArmType">
            <summary>
            Type of arm response when on knees falling forward 0=useFallArms (from RollDownstairs or catchFall), 1= armsIn, 2=armsOut.
            </summary>
            <remarks>
            Default value = 2.
            Min value = 0.
            Max value = 2.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotFallToKneesHelper.FtkReleaseReachForWound">
            <summary>
            Release the reachForWound this amount of time after the knees have hit.
            If LT 0.0 then keep reaching for wound regardless of fall/onground state.
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.0f.
            Max value = 5.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ShotFallToKneesHelper.FtkReachForWound">
            <summary>
            True = Keep reaching for wound regardless of fall/onground state.
            false = respect the shotConfigureArms params: reachFalling, reachFallingWithOneHand, reachOnFloor.
            </summary>
            <remarks>
```
