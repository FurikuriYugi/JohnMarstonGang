<!-- ScriptHookRDRNetAPI lines 28501-28750 of 30005 -->
```csharp
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is in a scenario.
            </summary>
            <remarks>This is an alias for <see cref="P:RDR2.Ped.IsUsingScenario"/></remarks>
        </member>
        <member name="P:RDR2.Ped.IsChild">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is a child.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsCowering">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is cowering.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsDrunk">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is drunk.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsIntimidated">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is intimidated.
            </summary>
        </member>
        <member name="M:RDR2.Ped.Clone">
            <summary>
            Spawn an identical clone of this <see cref="T:RDR2.Ped"/>.
            </summary>
        </member>
        <member name="M:RDR2.Ped.Kill">
            <summary>
            Instantly kill this <see cref="T:RDR2.Ped"/>.
            </summary>
        </member>
        <member name="M:RDR2.Ped.Resurrect">
            <summary>
            Resurrects this <see cref="T:RDR2.Ped"/> from death.
            </summary>
        </member>
        <member name="M:RDR2.Ped.Revive">
            <summary>
            Revives this <see cref="T:RDR2.Ped"/> from death.
            </summary>
            <remarks>This is an alias for <see cref="M:RDR2.Ped.Resurrect"/></remarks>
        </member>
        <member name="P:RDR2.Ped.IsHuman">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is human.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsCuffed">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is handcuffed.
            </summary>
        </member>
        <member name="P:RDR2.Ped.Gender">
            <summary>
            Gets the gender of this <see cref="T:RDR2.Ped"/>.
            </summary>
        </member>
        <member name="P:RDR2.Ped.Scale">
            <summary>
            Sets the scale of this <see cref="T:RDR2.Ped"/>.
            </summary>
        </member>
        <member name="P:RDR2.Ped.Sweat">
            <summary>
            Sets how much sweat should be rendered on this <see cref="T:RDR2.Ped"/>.
            </summary>
        </member>
        <member name="P:RDR2.Ped.WetnessHeight">
            <summary>
            Sets how high up on this <see cref="T:RDR2.Ped"/>s body water should be visible.
            </summary>
            <value>
            The height ranges from 0.0f to 1.99f, 0.0f being no water visible, 1.99f being covered in water.
            </value>
        </member>
        <member name="M:RDR2.Ped.Clean">
            <summary>
            Clean this <see cref="T:RDR2.Ped"/> from dirt, mud, snow, water, etc
            </summary>
        </member>
        <member name="M:RDR2.Ped.RandomizeOutfit">
            <summary>
            Randomize this <see cref="T:RDR2.Ped"/> outfit
            </summary>
        </member>
        <member name="M:RDR2.Ped.UpdateVariation">
            <summary>
            Update variation on this <see cref="T:RDR2.Ped"/>, needed after first creation, or when component or texture/overlay is changed.
            </summary>
        </member>
        <member name="M:RDR2.Ped.EquipOutfitComponent(System.UInt32)">
            <summary>
            Equip a specific outfit component on this <see cref="T:RDR2.Ped"/>.
            Must call <see cref="M:RDR2.Ped.UpdateVariation"/> to see changes.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsBeingArrested">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is being arrested.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsExitingScenario">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is exiting/leaving a scenario.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsGettingUp">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is getting up from the ground.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsWrithing">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is writhing.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsRunning">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is running.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsSprinting">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is sprinting.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsStandingStill">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is standing still.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsWalking">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is walking.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsAimingInTheAir">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is aiming in the air.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsArrestingAPed">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is arresting another <see cref="T:RDR2.Ped"/>
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsLeadingHorse">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is leading a horse.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsClimbing">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is climbing.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsDiving">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is diving.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsJumping">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is jumping.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsProne">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is prone.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsSitting">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is sitting.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsStopped">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is stopped.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsSwimming">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is swimming.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsSwimmingUnderwater">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is swimming underwater.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsClimbingLadder">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is climbing a ladder.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsVaulting">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is vaulting over something.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsSliding">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is sliding.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsOpeningDoor">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is opening a door.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsIdle">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is idle.
            </summary>
            <remarks>This is an alias for <see cref="P:RDR2.Ped.IsStandingStill"/></remarks>
        </member>
        <member name="M:RDR2.Ped.IsHeadTrackingEntity(RDR2.Entity)">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is headtracking an <see cref="T:RDR2.Entity"/>.
            </summary>
        </member>
        <member name="P:RDR2.Ped.AlwaysKeepTask">
            <summary>
            Set a value indicating whether this <see cref="T:RDR2.Ped"/> should keep scripted tasks.
            </summary>
        </member>
        <member name="P:RDR2.Ped.BlockPermanentEvents">
            <summary>
            Set a value indicating whether this <see cref="T:RDR2.Ped"/> should block non temporary events.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsRagdoll">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is ragdolling.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsFalling">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is falling.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsFallingOver">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is falling over.
            </summary>
```
