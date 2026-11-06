<!-- ScriptHookRDRNetAPI lines 29001-29250 of 30005 -->
```csharp
        </member>
        <member name="P:RDR2.Vehicle.Speed">
            <summary>
            Gets or sets this <see cref="T:RDR2.Vehicle"/>s speed.
            </summary>
        </member>
        <member name="M:RDR2.Vehicle.Repair">
            <summary>
            Repair all damage to this <see cref="T:RDR2.Vehicle"/> instantaneously.
            </summary>
        </member>
        <member name="M:RDR2.Vehicle.Explode">
            <summary>
            Explode this <see cref="T:RDR2.Vehicle"/> instantaneously.
            </summary>
        </member>
        <member name="P:RDR2.Vehicle.Driver">
            <summary>
            Gets the driver of this <see cref="T:RDR2.Vehicle"/>
            </summary>
        </member>
        <member name="M:RDR2.Vehicle.GetPedInSeat(RDR2.eVehicleSeat)">
            <summary>
            Gets the <see cref="T:RDR2.Ped"/> thats in the specified <see cref="T:RDR2.eVehicleSeat"/>
            </summary>
        </member>
        <member name="P:RDR2.Vehicle.Occupants">
            <summary>
            Returns an <see cref="T:System.Array"/> of all occupants in this <see cref="T:RDR2.Vehicle"/>, INCLUDING the driver
            </summary>
        </member>
        <member name="P:RDR2.Vehicle.Passengers">
            <summary>
            Returns an <see cref="T:System.Array"/> of all passengers in this <see cref="T:RDR2.Vehicle"/>, EXCLUDING the driver
            </summary>
        </member>
        <member name="P:RDR2.Vehicle.PassengerCount">
            <summary>
            Gets the current number of passengers in this <see cref="T:RDR2.Vehicle"/>
            </summary>
        </member>
        <member name="P:RDR2.Vehicle.PassengerSeats">
            <summary>
            Gets the max number of passengers this <see cref="T:RDR2.Vehicle"/> can have
            </summary>
        </member>
        <member name="P:RDR2.Game.FrameTime">
            <summary>
            Get the interval in seconds from the last frame to the current one
            </summary>
        </member>
        <member name="P:RDR2.Game.DeltaTime">
            <summary>
            Get the interval in seconds from the last frame to the current one
            </summary>
            <remarks>This is an alias for <see cref="P:RDR2.Game.FrameTime"/></remarks>
        </member>
        <member name="P:RDR2.Game.FPS">
            <summary>
            Gets the current frame rate in frames per second.
            </summary>
        </member>
        <member name="P:RDR2.Game.GameTime">
            <summary>
            Gets how many milliseconds the game has been open in this session
            </summary>
        </member>
        <member name="P:RDR2.Game.FrameCount">
            <summary>
            Gets the total number of frames that have been rendered in this session.
            </summary>
        </member>
        <member name="P:RDR2.Game.ScreenResolution">
            <summary>
            Gets the game window screen resolution
            </summary>
        </member>
        <member name="P:RDR2.Game.Player">
            <summary>
            Gets the main <see cref="T:RDR2.Player"/> that you are controlling.
            </summary>
        </member>
        <member name="P:RDR2.Game.PlayerBlip">
            <summary>
            Gets the blip of the <see cref="T:RDR2.Player"/> that you are controlling.
            </summary>
        </member>
        <member name="P:RDR2.Game.Version">
            <summary>
            Gets the version of the game.
            </summary>
        </member>
        <member name="P:RDR2.Game.Globals">
            <summary>
            Get the games script globals as a collection
            </summary>
        </member>
        <member name="M:RDR2.Game.GetGlobalPtr(System.Int32)">
            <summary>
            Gets an instance of a <see cref="T:RDR2.Global"/> that holds data to a script global.
            </summary>
            <remarks>Make sure that you check the game version before accessing globals. ID's may differ between patches.</remarks>
            <param name="globalId">The script global index</param>
            <returns>An instance to a script <see cref="T:RDR2.Global"/></returns>
        </member>
        <member name="P:RDR2.Game.IsCutsceneActive">
            <summary>
            Gets a value indicating whether a cutscene is currently playing.
            </summary>
        </member>
        <member name="P:RDR2.Game.InCutscene">
            <summary>
            Gets a value indicating whether a cutscene is currently playing.
            </summary>
            <remarks>This is an alias for <see cref="P:RDR2.Game.IsCutsceneActive"/></remarks>
        </member>
        <member name="P:RDR2.Game.IsPaused">
            <summary>
            Gets or sets a value indicating whether the pause menu is active.
            </summary>
        </member>
        <member name="M:RDR2.Game.Pause(System.Boolean)">
            <summary>
            Pause/resume the game.
            </summary>
        </member>
        <member name="M:RDR2.Game.PauseClock(System.Boolean)">
            <summary>
            Pause/resume the in-game clock
            </summary>
        </member>
        <member name="P:RDR2.Game.IsLoading">
            <summary>
            Gets a value indicating whether there is a loading screen being displayed.
            </summary>
        </member>
        <member name="P:RDR2.Game.IsScreenFadedIn">
            <summary>
            Gets a value indicating whether the screen is currently faded in
            </summary>
        </member>
        <member name="P:RDR2.Game.IsScreenFadedOut">
            <summary>
            Gets a value indicating whether the screen is currently faded out
            </summary>
        </member>
        <member name="P:RDR2.Game.IsScreenFadingIn">
            <summary>
            Gets a value indicating whether the screen is currently fading in
            </summary>
        </member>
        <member name="P:RDR2.Game.IsScreenFadingOut">
            <summary>
            Gets a value indicating whether the screen is currently fading out
            </summary>
        </member>
        <member name="M:RDR2.Game.FadeScreenIn(System.Int32)">
            <summary>
            Fade the screen in
            </summary>
        </member>
        <member name="M:RDR2.Game.FadeScreenOut(System.Int32)">
            <summary>
            Fade the screen out
            </summary>
        </member>
        <member name="P:RDR2.Game.IsWaypointActive">
            <summary>
            Gets a value indicating whether there is a waypoint set on the map.
            </summary>
        </member>
        <member name="P:RDR2.Game.TimeScale">
            <summary>
            Gets the time scale of the game.
            </summary>
        </member>
        <member name="P:RDR2.Game.RadarZoom">
            <summary>
            Set the zoom on the minimap/radar
            </summary>
        </member>
        <member name="P:RDR2.Game.MinimapZoom">
            <summary>
            Set the zoom on the minimap/radar
            </summary>
            <remarks>This is an alias for <see cref="P:RDR2.Game.RadarZoom"/></remarks>
        </member>
        <member name="P:RDR2.Game.IsMissionActive">
            <summary>
            Gets or sets a value informing the engine if a mission is in progress.
            </summary>
        </member>
        <member name="P:RDR2.Game.InMission">
            <summary>
            Gets or sets a value informing the engine if a mission is in progress.
            </summary>
            <remarks>This is an alias for <see cref="P:RDR2.Game.IsMissionActive"/></remarks>
        </member>
        <member name="P:RDR2.Game.ShowsPoliceBlipsOnRadar">
            <summary>
            Set whether police/lawmen blips should appear on the radar/minimap.
            </summary>
        </member>
        <member name="M:RDR2.Game.GetUserInput(System.Int32)">
            <summary>
            Creates an onscreen keyboard for the user to input text.
            </summary>
        </member>
        <member name="M:RDR2.Game.GetUserInput(System.String,System.Int32)">
            <summary>
            Creates an onscreen keyboard for the user to input text.
            </summary>
        </member>
        <member name="M:RDR2.Game.GetUserInput(System.String,System.String,System.Int32)">
            <summary>
            Creates an onscreen keyboard for the user to input text.
            </summary>
        </member>
        <member name="P:RDR2.Game.LastInputMethod">
            <summary>
            Gets whether the last input was made with a GamePad or keyboard and mouse.
            </summary>
        </member>
        <member name="M:RDR2.Game.GetControlValue(RDR2.eInputType)">
            <summary>
            Gets an analog value of a <see cref="T:RDR2.eInputType"/> input.
            </summary>
        </member>
        <member name="M:RDR2.Game.GetControlNormal(RDR2.eInputType)">
            <summary>
            Gets an analog value of a <see cref="T:RDR2.eInputType"/> input between -1.0f and 1.0f.
            </summary>
        </member>
        <member name="M:RDR2.Game.GetDisabledControlNormal(RDR2.eInputType)">
            <summary>
            Gets an analog value of a disabled <see cref="T:RDR2.eInputType"/> input between -1.0f and 1.0f.
            </summary>
        </member>
        <member name="M:RDR2.Game.SetControlNormal(RDR2.eInputType,System.Single)">
            <summary>
            Override a <see cref="T:RDR2.eInputType"/> by giving it a user-defined value for this frame.
            </summary>
        </member>
        <member name="M:RDR2.Game.IsControlEnabled(RDR2.eInputType)">
            <summary>
            Gets whether a <see cref="T:RDR2.eInputType"/> control is currently enabled.
            </summary>
        </member>
        <member name="M:RDR2.Game.IsControlPressed(RDR2.eInputType)">
            <summary>
```
