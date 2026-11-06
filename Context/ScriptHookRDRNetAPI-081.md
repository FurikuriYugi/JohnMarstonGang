<!-- ScriptHookRDRNetAPI lines 20001-20250 of 30005 -->
```csharp
            <summary>
            Damp out excessive spin and upward velocity when on car.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.DampSpin">
            <summary>
            Amount to damp spinning by (cartwheeling and somersaulting).
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 40.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.DampUpVel">
            <summary>
            Amount to damp upward velocity by to limit the amount of air above the car the character can get.
            </summary>
            <remarks>
            Default value = 10.0f.
            Min value = 0.0f.
            Max value = 40.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.DampSpinThresh">
            <summary>
            Angular velocity above which we start damping.
            </summary>
            <remarks>
            Default value = 4.0f.
            Min value = 0.0f.
            Max value = 20.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.DampUpVelThresh">
            <summary>
            Upward velocity above which we start damping.
            </summary>
            <remarks>
            Default value = 2.0f.
            Min value = 0.0f.
            Max value = 20.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.GsHelp">
            <summary>
            Enhance a glancing spin with the side of the car by modulating body friction.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.GsEndMin">
            <summary>
            ID for glancing spin. Min depth to be considered from either end (front/rear) of a car (-ve is inside the car area).
            </summary>
            <remarks>
            Default value = -0.1f.
            Min value = -10.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.GsSideMin">
            <summary>
            ID for glancing spin. Min depth to be considered on the side of a car (-ve is inside the car area).
            </summary>
            <remarks>
            Default value = -0.2f.
            Min value = -10.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.GsSideMax">
            <summary>
            ID for glancing spin. Max depth to be considered on the side of a car (+ve is outside the car area).
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = -10.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.GsUpness">
            <summary>
            ID for glancing spin. Character has to be more upright than this value for it to be considered on the side of a car. Fully upright = 1, upsideDown = -1.  Max Angle from upright is acos(gsUpness).
            </summary>
            <remarks>
            Default value = 0.9f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.GsCarVelMin">
            <summary>
            ID for glancing spin. Minimum car velocity.
            </summary>
            <remarks>
            Default value = 3.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.GsScale1Foot">
            <summary>
            Apply gsFricScale1 to the foot if colliding with car.  (Otherwise foot friction - with the ground - is determined by gsFricScale2 if it is in gsFricMask2).
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.GsFricScale1">
            <summary>
            Glancing spin help. Friction scale applied when to the side of the car.  e.g. make the character spin more by upping the friction against the car.
            </summary>
            <remarks>
            Default value = 8.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.GsFricMask1">
            <summary>
            Glancing spin help. Two character body-masking value, bitwise joint mask or bitwise logic string of two character body-masking value  (see notes for explanation). Note gsFricMask1 and gsFricMask2 are made independent by the code so you can have fb for gsFricMask1 but gsFricScale1 will not be applied to any body parts in gsFricMask2.
            </summary>
            <remarks>
            Default value = "fb".
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.GsFricScale2">
            <summary>
            Glancing spin help. Friction scale applied when to the side of the car.  e.g. make the character spin more by lowering the feet friction. You could also lower the wrist friction here to stop the car pulling along the hands i.e. gsFricMask2 = la|uw.
            </summary>
            <remarks>
            Default value = 0.2f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.GsFricMask2">
            <summary>
            Two character body-masking value, bitwise joint mask or bitwise logic string of two character body-masking value  (see notes for explanation). Note gsFricMask1 and gsFricMask2 are made independent by the code so you can have fb for gsFricMask1 but gsFricScale1 will not be applied to any body parts in gsFricMask2.
            </summary>
            <remarks>
            Default value = la.
            </remarks>
        </member>
        <member name="T:RDR2.NaturalMotion.BuoyancyHelper">
            <summary>
            Simple buoyancy model.  No character movement just fluid forces/torques added to parts.
            </summary>
        </member>
        <member name="M:RDR2.NaturalMotion.BuoyancyHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the BuoyancyHelper for sending a Buoyancy <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the Buoyancy <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
            <remarks>
            Simple buoyancy model.  No character movement just fluid forces/torques added to parts.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BuoyancyHelper.SurfacePoint">
            <summary>
            Arbitrary point on surface of water.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BuoyancyHelper.SurfaceNormal">
            <summary>
            Normal to surface of water.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 1.0f).
            Min value = 0.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BuoyancyHelper.Buoyancy">
            <summary>
            Buoyancy multiplier.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BuoyancyHelper.ChestBuoyancy">
            <summary>
            Buoyancy multiplier for spine2/3. Helps character float upright.
            </summary>
            <remarks>
            Default value = 8.0f.
            Min value = 0.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BuoyancyHelper.Damping">
            <summary>
            Damping for submerged parts.
            </summary>
            <remarks>
            Default value = 40.0f.
            Min value = 0.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BuoyancyHelper.Righting">
            <summary>
            Use righting torque to being character face-up in water?.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BuoyancyHelper.RightingStrength">
            <summary>
            Strength of righting torque.
            </summary>
            <remarks>
            Default value = 25.0f.
            Min value = 0.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BuoyancyHelper.RightingTime">
            <summary>
            How long to wait after chest hits water to begin righting torque.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.CatchFallHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the CatchFallHelper for sending a CatchFall <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the CatchFall <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.CatchFallHelper.TorsoStiffness">
            <summary>
            Stiffness of torso.
            </summary>
            <remarks>
            Default value = 9.0f.
            Min value = 6.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.CatchFallHelper.LegsStiffness">
```
