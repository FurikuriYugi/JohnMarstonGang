<!-- ScriptHookRDRNetAPI lines 29501-29750 of 30005 -->
```csharp
        <member name="P:RDR2.Player.Character">
            <summary>
            Gets the <see cref="T:RDR2.Ped"/> this <see cref="T:RDR2.Player"/> is controlling.
            </summary>
        </member>
        <member name="P:RDR2.Player.Ped">
            <summary>
            Gets the <see cref="T:RDR2.Ped"/> this <see cref="T:RDR2.Player"/> is controlling.
            </summary>
            <remarks>This is an alias for <see cref="P:RDR2.Player.Character"/></remarks>
        </member>
        <member name="P:RDR2.Player.Money">
            <summary>
            Gets or sets how much money this <see cref="T:RDR2.Player"/> has.
            </summary>
        </member>
        <member name="P:RDR2.Player.Cash">
            <summary>
            Gets or sets how much money this <see cref="T:RDR2.Player"/> has.
            </summary>
            <remarks>This is an alias for <see cref="P:RDR2.Player.Money"/></remarks>
        </member>
        <member name="P:RDR2.Player.Bounty">
            <summary>
            Gets or sets this <see cref="T:RDR2.Player"/>'s bounty amount.
            </summary>
        </member>
        <member name="P:RDR2.Player.Invincible">
            <summary>
            Gets or sets a value indicating whether this <see cref="T:RDR2.Player"/> is invincible.
            </summary>
        </member>
        <member name="P:RDR2.Player.IgnoredByEveryone">
            <summary>
            Sets a value indicating whether this <see cref="T:RDR2.Player"/> is ignored by everyone.
            </summary>
            <remarks>This should be set every frame (if possible) to take full effect</remarks>
        </member>
        <member name="P:RDR2.Player.CanUseCover">
            <summary>
            Sets a value indicating whether this <see cref="T:RDR2.Player"/> can use cover.
            </summary>
        </member>
        <member name="M:RDR2.Player.SetPlayerControl(System.Boolean,RDR2.eSetPlayerControlFlags,System.Boolean)">
            <summary>
            Set whether this <see cref="T:RDR2.Player"/> can control himself or not.
            </summary>
            <param name="bEnabled">Whether control is enabled or not.</param>
            <param name="iFlags">Flags that affect how player control is handled</param>
            <param name="bPreventHeadingChange">Whether the player can still rotate his <see cref="P:RDR2.Player.Character"/>.</param>
        </member>
        <member name="M:RDR2.Player.ChangeModel(RDR2.Model)">
            <summary>
            Attempts to change the <see cref="T:RDR2.Model"/> of this <see cref="T:RDR2.Player"/>.
            </summary>
            <param name="model">The <see cref="T:RDR2.Model"/> to change this <see cref="T:RDR2.Player"/> to.</param>
            <returns><see langword="true" /> if the change was successful; otherwise, <see langword="false" />.</returns>
        </member>
        <member name="M:RDR2.Player.IsAimingAtEntity(RDR2.Entity)">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Player"/> is aiming at the passed <see cref="T:RDR2.Entity"/>.
            </summary>
        </member>
        <member name="M:RDR2.Player.GetFreeAimEntity">
            <summary>
            Gets the <see cref="T:RDR2.Entity"/> that this <see cref="T:RDR2.Player"/> is aiming at.
            </summary>
        </member>
        <member name="M:RDR2.Player.GetInteractionLockonTargetEntity">
            <summary>
            Gets the <see cref="T:RDR2.Entity"/> that this <see cref="T:RDR2.Player"/> is locking onto via interaction.
            </summary>
        </member>
        <member name="M:RDR2.Player.DisableFiringThisFrame(System.Boolean)">
            <summary>
            Prevents this <see cref="T:RDR2.Player"/> from firing for one frame.
            </summary>
        </member>
        <member name="M:RDR2.Player.SetSuperJumpThisFrame">
            <summary>
            Allows this <see cref="T:RDR2.Player"/> jump really high for one frame.
            </summary>
        </member>
        <member name="T:RDR2.ISpatial">
            <summary>
            An object with position and rotation information.
            </summary>
        </member>
        <member name="T:RDR2.IExistable">
            <summary>
            An object that can exist in the world.
            </summary>
        </member>
        <member name="T:RDR2.IDeletable">
            <summary>
            An object that can be deleted from the world.
            </summary>
        </member>
        <member name="T:RDR2.PoolObject">
            <summary>
            An object that resides in one of the available object pools.
            </summary>
        </member>
        <member name="P:RDR2.PoolObject.Handle">
            <summary>
            The handle of the object.
            </summary>
        </member>
        <member name="P:RDR2.PoolObject.NativeValue">
            <summary>
            The handle of the object translated to a native value.
            </summary>
        </member>
        <member name="T:RDR2.Script">
            <summary>
            A base class for all user scripts to inherit.
            Only scripts that inherit directly from this class and have a default (parameterless) public constructor will be detected and started.
            </summary>
        </member>
        <member name="E:RDR2.Script.Tick">
            <summary>
            An event that is raised every tick of the script.
            Put code that needs to be looped each frame in here.
            </summary>
        </member>
        <member name="E:RDR2.Script.Aborted">
            <summary>
            An event that is raised when this <see cref="T:RDR2.Script"/> gets aborted for any reason.
            This should be used for cleaning up anything created during this <see cref="T:RDR2.Script"/>.
            </summary>
        </member>
        <member name="E:RDR2.Script.KeyUp">
            <summary>
            An event that is raised when a key is lifted.
            The <see cref="T:System.Windows.Forms.KeyEventArgs"/> contains the key that was lifted.
            </summary>
        </member>
        <member name="E:RDR2.Script.KeyDown">
            <summary>
            An event that is raised when a key is first pressed.
            The <see cref="T:System.Windows.Forms.KeyEventArgs"/> contains the key that was pressed.
            </summary>
        </member>
        <member name="P:RDR2.Script.Name">
            <summary>
            Gets the name of this <see cref="T:RDR2.Script"/>.
            </summary>
        </member>
        <member name="P:RDR2.Script.FileName">
            <summary>
            Gets the file name of this <see cref="T:RDR2.Script"/>.
            </summary>
        </member>
        <member name="P:RDR2.Script.BaseDirectory">
            <summary>
            Gets the Directory where this <see cref="T:RDR2.Script"/> is stored.
            </summary>
        </member>
        <member name="P:RDR2.Script.IsPaused">
            <summary>
            Checks if this <see cref="T:RDR2.Script"/> is paused.
            </summary>
        </member>
        <member name="P:RDR2.Script.IsRunning">
            <summary>
            Checks if this <see cref="T:RDR2.Script"/> is running.
            </summary>
        </member>
        <member name="P:RDR2.Script.IsExecuting">
            <summary>
            Checks if this <see cref="T:RDR2.Script"/> is executing.
            </summary>
        </member>
        <member name="P:RDR2.Script.Settings">
            <summary>
            Gets an INI file associated with this <see cref="T:RDR2.Script"/>.
            The File will be in the same location as this <see cref="T:RDR2.Script"/> but with an extension of ".ini".
            Use this to save and load settings for this <see cref="T:RDR2.Script"/>.
            </summary>
        </member>
        <member name="P:RDR2.Script.Interval">
            <summary>
            Gets or sets the interval in ms between <see cref="E:RDR2.Script.Tick"/> for this <see cref="T:RDR2.Script"/>.
            Default value is 0 meaning the event will execute once each frame.
            </summary>
        </member>
        <member name="M:RDR2.Script.ToString">
            <summary>
            Returns a string that represents this <see cref="T:RDR2.Script"/>.
            </summary>
        </member>
        <member name="M:RDR2.Script.GetRelativeFilePath(System.String)">
            <summary>
            Gets the full file path for a file relative to this <see cref="T:RDR2.Script"/>.
            e.g: <c>GetRelativeFilePath("ScriptFiles\texture1.png")</c> may return <c>"C:\Program Files\Rockstar Games\Red Dead Redemption 2\scripts\ScriptFiles\texture1.png"</c>.
            </summary>
            <param name="filePath">The file path relative to the location of this <see cref="T:RDR2.Script"/>.</param>
        </member>
        <member name="M:RDR2.Script.Abort">
            <summary>
            Aborts execution of this <see cref="T:RDR2.Script"/>.
            </summary>
        </member>
        <member name="M:RDR2.Script.Pause">
            <summary>
            Pause execution of this <see cref="T:RDR2.Script"/>.
            </summary>
        </member>
        <member name="M:RDR2.Script.Resume">
            <summary>
            Starts execution of this <see cref="T:RDR2.Script"/> after it has been Paused.
            </summary>
        </member>
        <member name="M:RDR2.Script.Wait(System.Int32)">
            <summary>
            Pauses execution of the <see cref="T:RDR2.Script"/> for a specific amount of time.
            Must be called inside the main script loop (the <see cref="E:RDR2.Script.Tick"/> event or any sub methods called from it).
            </summary>
            <param name="ms">The time in milliseconds to pause for.</param>
        </member>
        <member name="M:RDR2.Script.Yield">
            <summary>
            Yields the execution of the script for 1 frame.
            </summary>
        </member>
        <member name="M:RDR2.Script.InstantiateScript``1">
            <summary>
            Spawns a new <see cref="T:RDR2.Script"/> instance of the specified type.
            </summary>
        </member>
        <member name="M:RDR2.ScriptSettings.Load(System.String)">
            <summary>
            Loads a <see cref="T:RDR2.ScriptSettings"/> from the specified file.
            </summary>
            <param name="filename">The filename to load the settings from.</param>
        </member>
        <member name="M:RDR2.ScriptSettings.Save">
            <summary>
            Saves this <see cref="T:RDR2.ScriptSettings"/> to file.
            </summary>
            <returns><see langword="true" /> if the file saved successfully; otherwise, <see langword="false" /></returns>
        </member>
        <member name="M:RDR2.ScriptSettings.GetValue``1(System.String,System.String,``0)">
            <summary>
            Reads a value from this <see cref="T:RDR2.ScriptSettings"/>.
            </summary>
            <param name="section">The section where the value is.</param>
            <param name="name">The name of the key the value is saved at.</param>
            <param name="defaultvalue">The fall-back value if the key doesn't exist or casting to type <typeparamref name="T"/> fails.</param>
        </member>
```
