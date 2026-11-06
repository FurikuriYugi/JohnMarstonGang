<!-- ScriptHookRDRNetAPI lines 29251-29500 of 30005 -->
```csharp
            Gets whether a enabled <see cref="T:RDR2.eInputType"/> control is currently pressed.
            </summary>
        </member>
        <member name="M:RDR2.Game.IsControlJustPressed(RDR2.eInputType)">
            <summary>
            Gets whether a enabled <see cref="T:RDR2.eInputType"/> control was just pressed.
            </summary>
        </member>
        <member name="M:RDR2.Game.IsControlJustReleased(RDR2.eInputType)">
            <summary>
            Gets whether a enabled <see cref="T:RDR2.eInputType"/> control was just released.
            </summary>
        </member>
        <member name="M:RDR2.Game.IsControlReleased(RDR2.eInputType)">
            <summary>
            Gets whether a enabled <see cref="T:RDR2.eInputType"/> control is not pressed.
            </summary>
        </member>
        <member name="M:RDR2.Game.IsDisabledControlPressed(RDR2.eInputType)">
            <summary>
            Gets whether a disabled <see cref="T:RDR2.eInputType"/> control is currently pressed.
            </summary>
        </member>
        <member name="M:RDR2.Game.IsDisabledControlJustPressed(RDR2.eInputType)">
            <summary>
            Gets whether a disabled <see cref="T:RDR2.eInputType"/> control was just pressed.
            </summary>
        </member>
        <member name="M:RDR2.Game.IsDisabledControlJustReleased(RDR2.eInputType)">
            <summary>
            Gets whether a disabled <see cref="T:RDR2.eInputType"/> control was just released.
            </summary>
        </member>
        <member name="M:RDR2.Game.EnableControlThisFrame(RDR2.eInputType)">
            <summary>
            Enable a given <see cref="T:RDR2.eInputType"/> control for one frame
            </summary>
        </member>
        <member name="M:RDR2.Game.DisableControlThisFrame(RDR2.eInputType)">
            <summary>
            Disable a given <see cref="T:RDR2.eInputType"/> control for one frame
            </summary>
        </member>
        <member name="M:RDR2.Game.DisableAllControlsThisFrame">
            <summary>
            Disables all <see cref="T:RDR2.eInputType"/> controls this frame.
            </summary>
        </member>
        <member name="P:RDR2.Game.LastInputTime">
            <summary>
            Get how long ago since any input was given in milliseconds
            </summary>
        </member>
        <member name="P:RDR2.Game.IsUsingKeyboardAndMouse">
            <summary>
            Gets a value indicating whether the game is using keyboard and mouse input
            </summary>
        </member>
        <member name="M:RDR2.Game.Joaat(System.String)">
            <summary>
            Create a (case-insensitive) Jenkins-One-at-a-Time hash key
            </summary>
        </member>
        <member name="M:RDR2.Game.GetGXTEntry(System.String)">
            <summary>
            Get a GXT label entry
            </summary>
        </member>
        <member name="M:RDR2.Game.DoesGXTEntryExist(System.String)">
            <summary>
            Check whether the given GXT label entry exists
            </summary>
        </member>
        <member name="P:RDR2.GameplayCamera.FieldOfView">
            <summary>
            Gets the field of view of the <see cref="T:RDR2.GameplayCamera"/>.
            </summary>
        </member>
        <member name="P:RDR2.GameplayCamera.IsRendering">
            <summary>
            Gets a value indicating whether the <see cref="T:RDR2.GameplayCamera"/> is rendering.
            </summary>
        </member>
        <member name="P:RDR2.GameplayCamera.IsAimCamActive">
            <summary>
            Gets a value indicating whether the aiming camera is rendering.
            </summary>
        </member>
        <member name="P:RDR2.GameplayCamera.IsFirstPersonAimCamActive">
            <summary>
            Gets a value indicating whether the first person aiming camera is rendering.
            </summary>
        </member>
        <member name="P:RDR2.GameplayCamera.IsLookingBehind">
            <summary>
            Gets a value indicating whether the <see cref="T:RDR2.GameplayCamera"/> is looking behind.
            </summary>
        </member>
        <member name="P:RDR2.GameplayCamera.IsShaking">
            <summary>
            Gets a value indicating whether the <see cref="T:RDR2.GameplayCamera"/> is shaking.
            </summary>
        </member>
        <member name="P:RDR2.GameplayCamera.IsInFirstPerson">
            <summary>
            Gets a value indicating whether the <see cref="T:RDR2.GameplayCamera"/> is in first person.
            </summary>
        </member>
        <member name="P:RDR2.GameplayCamera.IsInThirdPerson">
            <summary>
            Gets a value indicating whether the <see cref="T:RDR2.GameplayCamera"/> is in third person.
            </summary>
        </member>
        <member name="P:RDR2.GameplayCamera.Zoom">
            <summary>
            Set the zoom of the <see cref="T:RDR2.GameplayCamera"/>.
            </summary>
        </member>
        <member name="P:RDR2.GameplayCamera.Position">
            <summary>
            Gets the position of the <see cref="T:RDR2.GameplayCamera"/>.
            </summary>
        </member>
        <member name="P:RDR2.GameplayCamera.Rotation">
            <summary>
            Gets the rotation of the <see cref="T:RDR2.GameplayCamera"/>.
            </summary>
        </member>
        <member name="P:RDR2.GameplayCamera.Direction">
            <summary>
            Gets the direction the <see cref="T:RDR2.GameplayCamera"/> is pointing in.
            </summary>
        </member>
        <member name="P:RDR2.GameplayCamera.ForwardVector">
            <summary>
            Gets the forward vector of the <see cref="T:RDR2.GameplayCamera"/>, see also <seealso cref="P:RDR2.GameplayCamera.Direction"/>.
            </summary>
        </member>
        <member name="M:RDR2.GameplayCamera.ClampYaw(System.Single,System.Single)">
            <summary>
            Clamps the yaw of the <see cref="T:RDR2.GameplayCamera"/>.
            </summary>
            <param name="min">The minimum yaw value.</param>
            <param name="max">The maximum yaw value.</param>
        </member>
        <member name="M:RDR2.GameplayCamera.ClampPitch(System.Single,System.Single)">
            <summary>
            Clamps the pitch of the <see cref="T:RDR2.GameplayCamera"/>.
            </summary>
            <param name="min">The minimum pitch value.</param>
            <param name="max">The maximum pitch value.</param>
        </member>
        <member name="P:RDR2.GameplayCamera.RelativePitch">
            <summary>
            Gets or sets the relative pitch of the <see cref="T:RDR2.GameplayCamera"/>.
            </summary>
        </member>
        <member name="P:RDR2.GameplayCamera.RelativeHeading">
            <summary>
            Gets or sets the relative heading of the <see cref="T:RDR2.GameplayCamera"/>.
            </summary>
        </member>
        <member name="M:RDR2.GlobalCollection.Get(System.Int32)">
            <summary>
            Returns an instance of <see cref="T:RDR2.Global"/> to a script global.
            </summary>
            <remarks>Make sure that you check the game version before accessing globals. ID's may differ between patches.</remarks>
            <param name="globalId">The script global index</param>
        </member>
        <member name="P:RDR2.Player.Name">
            <summary>
            Gets the Social Club name of this <see cref="T:RDR2.Player"/>.
            </summary>
        </member>
        <member name="P:RDR2.Player.IsDead">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Player"/> is dead.
            </summary>
        </member>
        <member name="P:RDR2.Player.IsAlive">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Player"/> is alive.
            </summary>
        </member>
        <member name="P:RDR2.Player.IsAiming">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Player"/> is aiming with a weapon.
            </summary>
        </member>
        <member name="P:RDR2.Player.IsClimbing">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Player"/> is climbing.
            </summary>
        </member>
        <member name="P:RDR2.Player.IsRidingTrain">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Player"/> is riding a train.
            </summary>
        </member>
        <member name="P:RDR2.Player.IsTargettingAnything">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Player"/> is targetting something.
            </summary>
        </member>
        <member name="P:RDR2.Player.IsFreeFocusing">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Player"/> is free focusing on something.
            </summary>
        </member>
        <member name="P:RDR2.Player.IsInDeadEye">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Player"/> is currently in Dead Eye.
            </summary>
        </member>
        <member name="P:RDR2.Player.IsInEagleEye">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Player"/> is currently in Eagle Eye.
            </summary>
        </member>
        <member name="P:RDR2.Player.IsWanted">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Player"/> is currently wanted by police/lawmen.
            </summary>
        </member>
        <member name="P:RDR2.Player.IsPlaying">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Player"/> is playing.
            </summary>
        </member>
        <member name="P:RDR2.Player.CanStartMission">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Player"/> can start a story mission.
            </summary>
        </member>
        <member name="P:RDR2.Player.LastVehicle">
            <summary>
            Gets the <see cref="T:RDR2.Vehicle"/> that this <see cref="T:RDR2.Player"/> last entered.
            </summary>
        </member>
        <member name="P:RDR2.Player.ActiveHorse">
            <summary>
            Gets the <see cref="T:RDR2.Player"/>'s currently active horse
            </summary>
        </member>
        <member name="P:RDR2.Player.IsControlOn">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Player"/> can control himself.
            </summary>
            <remarks>Use <see cref="M:RDR2.Player.SetPlayerControl(System.Boolean,RDR2.eSetPlayerControlFlags,System.Boolean)"/> to change this value.</remarks>
        </member>
```
