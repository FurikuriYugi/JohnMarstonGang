<!-- ScriptHookRDRNetAPI lines 27251-27500 of 30005 -->
```csharp
        </member>
        <member name="M:RDR2.UI.IElement.Draw">
            <summary>
            Draws this <see cref="T:RDR2.UI.IElement"/> this frame.
            </summary>
        </member>
        <member name="M:RDR2.UI.IElement.Draw(System.Drawing.SizeF)">
            <summary>
            Draws this <see cref="T:RDR2.UI.IElement"/> this frame at the specified offset.
            </summary>
            <param name="offset">The offset to shift the draw position of this <see cref="T:RDR2.UI.IElement"/> using a 1280*720 pixel base.</param>
        </member>
        <member name="M:RDR2.UI.IElement.ScaledDraw">
            <summary>
            Draws this <see cref="T:RDR2.UI.IElement"/> this frame using the width returned in <see cref="P:RDR2.UI.Screen.ScaledWidth"/>.
            </summary>
        </member>
        <member name="M:RDR2.UI.IElement.ScaledDraw(System.Drawing.SizeF)">
            <summary>
            Draws this <see cref="T:RDR2.UI.IElement"/> this frame at the specified offset using the width returned in <see cref="P:RDR2.UI.Screen.ScaledWidth"/>.
            </summary>
            <param name="offset">The offset to shift the draw position of this <see cref="T:RDR2.UI.IElement"/> using a <see cref="P:RDR2.UI.Screen.ScaledWidth"/>*720 pixel base.</param>
        </member>
        <member name="P:RDR2.UI.ISpriteElement.Size">
            <summary>
            Gets or sets the size to draw the <see cref="T:RDR2.UI.ISpriteElement"/>
            </summary>
            <value>
            The size on a 1280*720 pixel base
            </value>
            <remarks>
            If ScaledDraw is called, the size will be scaled by the width returned in <see cref="P:RDR2.UI.Screen.ScaledWidth"/>.
            </remarks>
        </member>
        <member name="P:RDR2.UI.ISpriteElement.Rotation">
            <summary>
            Gets or sets the rotation to draw thie <see cref="T:RDR2.UI.ISpriteElement"/>.
            </summary>
            <value>
            The rotation measured in degrees, clockwise increasing, 0.0 at vertical
            </value>
        </member>
        <member name="T:RDR2.UI.Screen">
            <summary>
            Methods to handle UI actions that affect the whole screen.
            </summary>
        </member>
        <member name="F:RDR2.UI.Screen.Width">
            <summary>
            The base width of the screen used for all UI Calculations, unless ScaledDraw is used
            </summary>
        </member>
        <member name="F:RDR2.UI.Screen.Height">
            <summary>
            The base height of the screen used for all UI Calculations
            </summary>
        </member>
        <member name="P:RDR2.UI.Screen.Resolution">
            <summary>
            Gets the actual screen resolution the game is being rendered at
            </summary>
        </member>
        <member name="P:RDR2.UI.Screen.AspectRatio">
            <summary>
            Gets the current screen aspect ratio
            </summary>
        </member>
        <member name="P:RDR2.UI.Screen.ScaledWidth">
            <summary>
            Gets the screen width scaled against a 720pixel height base.
            </summary>
        </member>
        <member name="P:RDR2.UI.Screen.IsFadedIn">
            <summary>
            Gets a value indicating whether the screen is faded in.
            </summary>
            <value>
            <c>true</c> if the screen is faded in; otherwise, <c>false</c>.
            </value>
        </member>
        <member name="P:RDR2.UI.Screen.IsFadedOut">
            <summary>
            Gets a value indicating whether the screen is faded out.
            </summary>
            <value>
            <c>true</c> if the screen is faded out; otherwise, <c>false</c>.
            </value>
        </member>
        <member name="P:RDR2.UI.Screen.IsFadingIn">
            <summary>
            Gets a value indicating whether the screen is fading in.
            </summary>
            <value>
            <c>true</c> if the screen is fading in; otherwise, <c>false</c>.
            </value>
        </member>
        <member name="P:RDR2.UI.Screen.IsFadingOut">
            <summary>
            Gets a value indicating whether the screen is fading out.
            </summary>
            <value>
            <c>true</c> if the screen is fading out; otherwise, <c>false</c>.
            </value>
        </member>
        <member name="M:RDR2.UI.Screen.FadeIn(System.Int32)">
            <summary>
            Fades the screen in over a specific time, useful for transitioning
            </summary>
            <param name="duration">The time for the fade in to take</param>
        </member>
        <member name="M:RDR2.UI.Screen.FadeOut(System.Int32)">
            <summary>
            Fades the screen out over a specific time, useful for transitioning
            </summary>
            <param name="duration">The time for the fade out to take</param>
        </member>
        <member name="M:RDR2.UI.Screen.IsEffectActive(System.String)">
            <summary>
            Gets a value indicating whether the specific screen effect is running.
            </summary>
            <param name="effectName">The effect to check.</param>
            <returns><c>true</c> if the screen effect is active; otherwise, <c>false</c>.</returns>
        </member>
        <member name="M:RDR2.UI.Screen.PlayEffect(System.String)">
            <summary>
            Plays a post processing effect on screen
            </summary>
            <param name="effectName">The effect to play</param>
        </member>
        <member name="M:RDR2.UI.Screen.StopEffect(System.String)">
            <summary>
            Stops a running post processing effect on screen
            </summary>
            <param name="effectName">The effect to stop</param>
        </member>
        <member name="M:RDR2.UI.Screen.ClearEffect(System.String)">
            <summary>
            Stops a running post processing effect on screen
            </summary>
            <param name="effectName">The effect to stop</param>
        </member>
        <member name="M:RDR2.UI.Screen.StopAllEffects">
            <summary>
            Stops all currently running effects.
            </summary>
        </member>
        <member name="M:RDR2.UI.Screen.PrintSubtitle(System.String)">
            <summary>
            Shows a subtitle at the bottom of the screen for a given time
            </summary>
            <param name="message">The message to display.</param>
        </member>
        <member name="M:RDR2.UI.Screen.PrintSubtitle(System.String[])">
            <summary>
            Shows a subtitle at the bottom of the screen for a given time
            </summary>
            <param name="message">The message to display.</param>
        </member>
        <member name="M:RDR2.UI.Screen.DisplaySubtitle(System.String)">
            <summary>
            Shows a subtitle at the bottom of the screen for a given time.
            <remarks>This is an alias for <see cref="M:RDR2.UI.Screen.PrintSubtitle(System.String)"/></remarks>
            </summary>
            <param name="message">The message to display.</param>
        </member>
        <member name="M:RDR2.UI.Screen.DisplaySubtitle(System.String[])">
            <summary>
            Shows a subtitle at the bottom of the screen for a given time.
            <remarks>This is an alias for <see cref="M:RDR2.UI.Screen.PrintSubtitle(System.String[])"/></remarks>
            </summary>
            <param name="message">The message to display.</param>
        </member>
        <member name="M:RDR2.UI.Screen.WorldToScreen(RDR2.Math.Vector3,System.Boolean)">
            <summary>
            Translates a point in WorldSpace to its given Coordinates on the <see cref="T:RDR2.UI.Screen"/>
            </summary>
            <param name="position">The position in the World.</param>
            <param name="scaleWidth">if set to <c>true</c> Returns the screen position scaled by <see cref="P:RDR2.UI.Screen.ScaledWidth"/>; otherwise, returns the screen position scaled by <see cref="F:RDR2.UI.Screen.Width"/>.</param>
            <returns></returns>
        </member>
        <member name="T:RDR2.UI.Sprite">
            <summary>
            A sprite element using a built-in texture.
            </summary>
        </member>
        <member name="M:RDR2.UI.Sprite.#ctor(System.String,System.String,System.Drawing.SizeF,System.Drawing.PointF)">
            <summary>
            Initializes a new instance of the <see cref="T:RDR2.UI.Sprite"/> class used for drawing in game textures on the screen.
            </summary>
            <param name="textureDict">The Texture dictionary where the <see cref="T:RDR2.UI.Sprite"/> is stored (the *.ytd file).</param>
            <param name="textureName">Name of the <see cref="T:RDR2.UI.Sprite"/> inside the Texture dictionary.</param>
            <param name="size">Set the <see cref="P:RDR2.UI.Sprite.Size"/> of the <see cref="T:RDR2.UI.Sprite"/>.</param>
            <param name="position">Set the <see cref="P:RDR2.UI.Sprite.Position"/> on screen where to draw the <see cref="T:RDR2.UI.Sprite"/>.</param>
        </member>
        <member name="M:RDR2.UI.Sprite.#ctor(System.String,System.String,System.Drawing.SizeF,System.Drawing.PointF,System.Drawing.Color)">
            <summary>
            Initializes a new instance of the <see cref="T:RDR2.UI.Sprite"/> class used for drawing in game textures on the screen.
            </summary>
            <param name="textureDict">The Texture dictionary where the <see cref="T:RDR2.UI.Sprite"/> is stored (the *.ytd file).</param>
            <param name="textureName">Name of the <see cref="T:RDR2.UI.Sprite"/> inside the Texture dictionary.</param>
            <param name="size">Set the <see cref="P:RDR2.UI.Sprite.Size"/> of the <see cref="T:RDR2.UI.Sprite"/>.</param>
            <param name="position">Set the <see cref="P:RDR2.UI.Sprite.Position"/> on screen where to draw the <see cref="T:RDR2.UI.Sprite"/>.</param>
            <param name="color">Set the <see cref="P:RDR2.UI.Sprite.Color"/> used to draw the <see cref="T:RDR2.UI.Sprite"/>.</param>
        </member>
        <member name="M:RDR2.UI.Sprite.#ctor(System.String,System.String,System.Drawing.SizeF,System.Drawing.PointF,System.Drawing.Color,System.Single)">
            <summary>
            Initializes a new instance of the <see cref="T:RDR2.UI.Sprite"/> class used for drawing in game textures on the screen.
            </summary>
            <param name="textureDict">The Texture dictionary where the <see cref="T:RDR2.UI.Sprite"/> is stored (the *.ytd file).</param>
            <param name="textureName">Name of the <see cref="T:RDR2.UI.Sprite"/> inside the Texture dictionary.</param>
            <param name="size">Set the <see cref="P:RDR2.UI.Sprite.Size"/> of the <see cref="T:RDR2.UI.Sprite"/>.</param>
            <param name="position">Set the <see cref="P:RDR2.UI.Sprite.Position"/> on screen where to draw the <see cref="T:RDR2.UI.Sprite"/>.</param>
            <param name="color">Set the <see cref="P:RDR2.UI.Sprite.Color"/> used to draw the <see cref="T:RDR2.UI.Sprite"/>.</param>
            <param name="rotation">Set the rotation to draw the sprite, measured in degrees, see also <seealso cref="P:RDR2.UI.Sprite.Rotation"/>.</param>
        </member>
        <member name="M:RDR2.UI.Sprite.#ctor(System.String,System.String,System.Drawing.SizeF,System.Drawing.PointF,System.Drawing.Color,System.Single,System.Boolean)">
            <summary>
            Initializes a new instance of the <see cref="T:RDR2.UI.Sprite"/> class used for drawing in game textures on the screen.
            </summary>
            <param name="textureDict">The Texture dictionary where the <see cref="T:RDR2.UI.Sprite"/> is stored (the *.ytd file).</param>
            <param name="textureName">Name of the <see cref="T:RDR2.UI.Sprite"/> inside the Texture dictionary.</param>
            <param name="size">Set the <see cref="P:RDR2.UI.Sprite.Size"/> of the <see cref="T:RDR2.UI.Sprite"/>.</param>
            <param name="position">Set the <see cref="P:RDR2.UI.Sprite.Position"/> on screen where to draw the <see cref="T:RDR2.UI.Sprite"/>.</param>
            <param name="color">Set the <see cref="P:RDR2.UI.Sprite.Color"/> used to draw the <see cref="T:RDR2.UI.Sprite"/>.</param>
            <param name="rotation">Set the rotation to draw the sprite, measured in degrees, see also <seealso cref="P:RDR2.UI.Sprite.Rotation"/>.</param>
            <param name="centered">Position the <see cref="T:RDR2.UI.Sprite"/> based on its center instead of top left corner, see also <seealso cref="P:RDR2.UI.Sprite.Centered"/>.</param>
        </member>
        <member name="P:RDR2.UI.Sprite.Enabled">
            <summary>
            Gets or sets a value indicating whether this <see cref="T:RDR2.UI.Sprite" /> will be drawn.
            </summary>
            <value>
              <c>true</c> if enabled; otherwise, <c>false</c>.
            </value>
        </member>
        <member name="P:RDR2.UI.Sprite.Color">
            <summary>
            Gets or sets the color of this <see cref="T:RDR2.UI.Sprite" />.
            </summary>
            <value>
            The color.
            </value>
        </member>
        <member name="P:RDR2.UI.Sprite.Position">
            <summary>
            Gets or sets the position of this <see cref="T:RDR2.UI.Sprite" />.
            </summary>
            <value>
            The position scaled on a 1280*720 pixel base.
            </value>
```
