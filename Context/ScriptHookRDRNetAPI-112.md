<!-- ScriptHookRDRNetAPI lines 27751-28000 of 30005 -->
```csharp
        </member>
        <member name="P:RDR2.UI.Prompt.HasHoldModeCompleted">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.UI.Prompt"/>s hold mode has complete.
            </summary>
        </member>
        <member name="P:RDR2.UI.Prompt.HasMashModeCompleted">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.UI.Prompt"/>s mash mode has completed.
            </summary>
        </member>
        <member name="P:RDR2.UI.Prompt.HasMashModeFailed">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.UI.Prompt"/>s mash mode has failed.
            </summary>
        </member>
        <member name="P:RDR2.UI.Prompt.HasTimedEventCompleted">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.UI.Prompt"/>s timed event completed.
            </summary>
        </member>
        <member name="P:RDR2.UI.Prompt.HasTimedEventFailed">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.UI.Prompt"/>s timed event failed.
            </summary>
        </member>
        <member name="P:RDR2.UI.Prompt.HasCompleted">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.UI.Prompt"/>s <see cref="F:RDR2.UI.eUseContextMode.Press"/> or <see cref="F:RDR2.UI.eUseContextMode.Release"/> mode has been completed.
            </summary>
        </member>
        <member name="P:RDR2.UI.Prompt.Priority">
            <summary>
            Sets the <see cref="T:RDR2.UI.Prompt"/> priority level
            </summary>
        </member>
        <member name="P:RDR2.UI.Prompt.IsPulsing">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.UI.Prompt"/> is pulsing.
            </summary>
        </member>
        <member name="F:RDR2.UI.eUseContextMode.Rotate">
            <summary>
            Gamepad Only
            </summary>
        </member>
        <member name="P:RDR2.AnimScene.IsLoaded">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.AnimScene"/> has loaded.
            </summary>
        </member>
        <member name="P:RDR2.AnimScene.IsMetadataLoaded">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.AnimScene"/> has its metadata loaded.
            </summary>
        </member>
        <member name="P:RDR2.AnimScene.IsLoading">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.AnimScene"/> is loading.
            </summary>
        </member>
        <member name="P:RDR2.AnimScene.IsRunning">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.AnimScene"/> is running.
            </summary>
        </member>
        <member name="P:RDR2.AnimScene.IsFinished">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.AnimScene"/> is finished.
            </summary>
        </member>
        <member name="P:RDR2.AnimScene.IsExitingThisFrame">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.AnimScene"/> is currently exiting this frame.
            </summary>
        </member>
        <member name="P:RDR2.AnimScene.HasExited">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.AnimScene"/> has exited.
            </summary>
        </member>
        <member name="P:RDR2.AnimScene.IsAborted">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.AnimScene"/> is aborted.
            </summary>
        </member>
        <member name="P:RDR2.AnimScene.IsSkippable">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.AnimScene"/> can be skipped.
            </summary>
        </member>
        <member name="P:RDR2.AnimScene.CanBeSkipped">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.AnimScene"/> can be skipped.
            </summary>
            <remarks>This is an alias for <see cref="P:RDR2.AnimScene.IsSkippable"/></remarks>
        </member>
        <member name="P:RDR2.AnimScene.WasSkipped">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.AnimScene"/> was skipped.
            </summary>
        </member>
        <member name="P:RDR2.AnimScene.IsPaused">
            <summary>
            Gets or sets whether this <see cref="T:RDR2.AnimScene"/> is paused.
            </summary>
        </member>
        <member name="P:RDR2.AnimScene.Dictionary">
            <summary>
            Gets the dictionary hash of this <see cref="T:RDR2.AnimScene"/>
            </summary>
        </member>
        <member name="P:RDR2.AnimScene.Phase">
            <summary>
            Gets the phase this <see cref="T:RDR2.AnimScene"/> is at
            </summary>
        </member>
        <member name="P:RDR2.AnimScene.Duration">
            <summary>
            Gets the duration length of this <see cref="T:RDR2.AnimScene"/>
            </summary>
        </member>
        <member name="P:RDR2.AnimScene.Time">
            <summary>
            Gets the current time this <see cref="T:RDR2.AnimScene"/> is at
            </summary>
        </member>
        <member name="P:RDR2.AnimScene.Rate">
            <summary>
            Gets or sets this <see cref="T:RDR2.AnimScene"/>'s rate.
            </summary>
        </member>
        <member name="M:RDR2.AnimScene.Load">
            <summary>
            Load this <see cref="T:RDR2.AnimScene"/>
            </summary>
        </member>
        <member name="M:RDR2.AnimScene.Start">
            <summary>
            Start this <see cref="T:RDR2.AnimScene"/>
            </summary>
        </member>
        <member name="M:RDR2.AnimScene.Abort">
            <summary>
            Abort this <see cref="T:RDR2.AnimScene"/>
            </summary>
        </member>
        <member name="M:RDR2.AnimScene.Skip">
            <summary>
            Trigger a skip for this <see cref="T:RDR2.AnimScene"/>
            </summary>
        </member>
        <member name="M:RDR2.AnimScene.Exists">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.AnimScene"/> exists.
            </summary>
        </member>
        <member name="M:RDR2.AnimScene.Delete">
            <summary>
            Delete this <see cref="T:RDR2.AnimScene"/>
            </summary>
        </member>
        <member name="T:RDR2.eBlipSprite">
            <summary>
            The sprite icon to set the blip to. Here's a
            <see href="https://cdn.discordapp.com/attachments/450373719974477835/643937562091716638/unknown.png">reference image</see>
            in order from left-to-right by GlitchDetector, and each row indicates the different type.
            </summary>
        </member>
        <member name="M:RDR2.Entity.FromHandle(System.Int32)">
            <summary>
            Creates a new instance of an <see cref="T:RDR2.Entity"/> from the given handle.
            </summary>
            <param name="handle">The entity handle.</param>
            <returns>Returns a <see cref="T:RDR2.Ped"/> if this handle corresponds to a Ped.
            Returns a <see cref="T:RDR2.Vehicle"/> if this handle corresponds to a Vehicle.
            Returns a <see cref="T:RDR2.Prop"/> if this handle corresponds to a Prop.
            Returns <see langword="null"/> otherwise.</returns>
        </member>
        <member name="P:RDR2.Entity.IsDead">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Entity"/> is dead.
            </summary>
        </member>
        <member name="P:RDR2.Entity.IsAlive">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Entity"/> is alive.
            </summary>
        </member>
        <member name="P:RDR2.Entity.IsPed">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Entity"/> is a ped.
            </summary>
        </member>
        <member name="P:RDR2.Entity.IsObject">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Entity"/> is an object.
            </summary>
        </member>
        <member name="P:RDR2.Entity.IsVehicle">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Entity"/> is a vehicle.
            </summary>
        </member>
        <member name="P:RDR2.Entity.IsAnimal">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Entity"/> is an animal.
            </summary>
        </member>
        <member name="P:RDR2.Entity.IsOccluded">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Entity"/> is occluded.
            </summary>
        </member>
        <member name="P:RDR2.Entity.IsOnFire">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Entity"/> is on fire.
            </summary>
        </member>
        <member name="P:RDR2.Entity.IsOnScreen">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Entity"/> is currently on screen.
            </summary>
        </member>
        <member name="P:RDR2.Entity.IsUpright">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Entity"/> is in the upright position.
            </summary>
        </member>
        <member name="P:RDR2.Entity.IsUpsideDown">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Entity"/> is upsidedown.
            </summary>
        </member>
        <member name="P:RDR2.Entity.IsInAir">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Entity"/> is in the air.
            </summary>
        </member>
        <member name="P:RDR2.Entity.IsInWater">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Entity"/> is in water.
            </summary>
        </member>
        <member name="P:RDR2.Entity.IsUnderwater">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Entity"/> is  underwater.
            </summary>
        </member>
        <member name="P:RDR2.Entity.IsFrozenInPlace">
```
