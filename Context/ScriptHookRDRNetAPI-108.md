<!-- ScriptHookRDRNetAPI lines 26751-27000 of 30005 -->
```csharp
            <summary>
            Wriggle relaxes for a minimum of minRelaxPeriod (if it is negative it is a multiplier on the time previously spent wriggling).
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = -5.0f.
            Max value = 5.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.YankedHelper.MaxRelaxPeriod">
            <summary>
            Wriggle relaxes for a maximum of maxRelaxPeriod (if it is negative it is a multiplier on the time previously spent wriggling).
            </summary>
            <remarks>
            Default value = 1.5f.
            Min value = -5.0f.
            Max value = 5.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.YankedHelper.RollHelp">
            <summary>
            Amount of cheat torque applied to turn the character over.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.YankedHelper.GroundLegStiffness">
            <summary>
            Leg Stiffness when on the ground.
            </summary>
            <remarks>
            Default value = 11.0f.
            Min value = 0.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.YankedHelper.GroundArmStiffness">
            <summary>
            Arm Stiffness when on the ground.
            </summary>
            <remarks>
            Default value = 11.0f.
            Min value = 0.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.YankedHelper.GroundSpineStiffness">
            <summary>
            Spine Stiffness when on the ground.
            </summary>
            <remarks>
            Default value = 14.0f.
            Min value = 0.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.YankedHelper.GroundLegDamping">
            <summary>
            Leg Damping when on the ground.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.YankedHelper.GroundArmDamping">
            <summary>
            Arm Damping when on the ground.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.YankedHelper.GroundSpineDamping">
            <summary>
            Spine Damping when on the ground.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.YankedHelper.GroundFriction">
            <summary>
            Friction multiplier on body parts when on ground.
            Character can look too slidy with groundFriction = 1.
            Higher values give a more jerky reaction but this seems timestep dependent especially for dragged by the feet.
            </summary>
            <remarks>
            Default value = 8.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="M:RDR2.UI.ContainerElement.#ctor">
            <summary>
            Initializes a new instance of the <see cref="T:RDR2.UI.ContainerElement"/> class used for grouping items on screen.
            </summary>
        </member>
        <member name="M:RDR2.UI.ContainerElement.#ctor(System.Drawing.PointF,System.Drawing.SizeF)">
            <summary>
            Initializes a new instance of the <see cref="T:RDR2.UI.ContainerElement"/> class used for grouping items on screen.
            </summary>
            <param name="position">Set the <see cref="P:RDR2.UI.ContainerElement.Position"/> on screen where to draw the <see cref="T:RDR2.UI.ContainerElement"/>.</param>
            <param name="size">Set the <see cref="P:RDR2.UI.ContainerElement.Size"/> of the <see cref="T:RDR2.UI.ContainerElement"/>.</param>
        </member>
        <member name="M:RDR2.UI.ContainerElement.#ctor(System.Drawing.PointF,System.Drawing.SizeF,System.Drawing.Color)">
            <summary>
            Initializes a new instance of the <see cref="T:RDR2.UI.ContainerElement"/> class used for grouping items on screen.
            </summary>
            <param name="position">Set the <see cref="P:RDR2.UI.ContainerElement.Position"/> on screen where to draw the <see cref="T:RDR2.UI.ContainerElement"/>.</param>
            <param name="size">Set the <see cref="P:RDR2.UI.ContainerElement.Size"/> of the <see cref="T:RDR2.UI.ContainerElement"/>.</param>
            <param name="color">Set the <see cref="P:RDR2.UI.ContainerElement.Color"/> used to draw the <see cref="T:RDR2.UI.ContainerElement"/>.</param>
        </member>
        <member name="M:RDR2.UI.ContainerElement.#ctor(System.Drawing.PointF,System.Drawing.SizeF,System.Drawing.Color,System.Boolean)">
            <summary>
            Initializes a new instance of the <see cref="T:RDR2.UI.ContainerElement"/> class used for grouping items on screen.
            </summary>
            <param name="position">Set the <see cref="P:RDR2.UI.ContainerElement.Position"/> on screen where to draw the <see cref="T:RDR2.UI.ContainerElement"/>.</param>
            <param name="size">Set the <see cref="P:RDR2.UI.ContainerElement.Size"/> of the <see cref="T:RDR2.UI.ContainerElement"/>.</param>
            <param name="color">Set the <see cref="P:RDR2.UI.ContainerElement.Color"/> used to draw the <see cref="T:RDR2.UI.ContainerElement"/>.</param>
            <param name="centered">Position the <see cref="T:RDR2.UI.ContainerElement"/> based on its center instead of top left corner, see also <seealso cref="P:RDR2.UI.ContainerElement.Centered"/>.</param>
        </member>
        <member name="P:RDR2.UI.ContainerElement.Enabled">
            <summary>
            Gets or sets a value indicating whether this <see cref="T:RDR2.UI.ContainerElement"/> will be drawn.
            </summary>
            <value>
              <c>true</c> if enabled; otherwise, <c>false</c>.
            </value>
        </member>
        <member name="P:RDR2.UI.ContainerElement.Color">
            <summary>
            Gets or sets the color of this <see cref="T:RDR2.UI.ContainerElement"/>.
            </summary>
            <value>
            The color.
            </value>
        </member>
        <member name="P:RDR2.UI.ContainerElement.Position">
            <summary>
            Gets or sets the position of this <see cref="T:RDR2.UI.ContainerElement"/>.
            </summary>
            <value>
            The position scaled on a 1280*720 pixel base.
            </value>
            <remarks>
            If ScaledDraw is called, the position will be scaled by the width returned in <see cref="P:RDR2.UI.Screen.ScaledWidth"/>.
            </remarks>
        </member>
        <member name="P:RDR2.UI.ContainerElement.Size">
            <summary>
            Gets or sets the size to draw the <see cref="T:RDR2.UI.ContainerElement"/>
            </summary>
            <value>
            The size on a 1280*720 pixel base
            </value>
            <remarks>
            If ScaledDraw is called, the size will be scaled by the width returned in <see cref="P:RDR2.UI.Screen.ScaledWidth"/>.
            </remarks>
        </member>
        <member name="P:RDR2.UI.ContainerElement.Centered">
            <summary>
            Gets or sets a value indicating whether this <see cref="T:RDR2.UI.ContainerElement"/> should be positioned based on its center or top left corner
            </summary>
            <value>
              <c>true</c> if centered; otherwise, <c>false</c>.
            </value>
        </member>
        <member name="P:RDR2.UI.ContainerElement.Items">
            <summary>
            The <see cref="T:RDR2.UI.IElement"/>s Contained inside this <see cref="T:RDR2.UI.ContainerElement"/>
            </summary>
        </member>
        <member name="M:RDR2.UI.ContainerElement.Draw">
            <summary>
            Draws this <see cref="T:RDR2.UI.ContainerElement" /> this frame.
            </summary>
        </member>
        <member name="M:RDR2.UI.ContainerElement.Draw(System.Drawing.SizeF)">
            <summary>
            Draws this <see cref="T:RDR2.UI.ContainerElement" /> this frame at the specified offset.
            </summary>
            <param name="offset">The offset to shift the draw position of this <see cref="T:RDR2.UI.ContainerElement" /> using a 1280*720 pixel base.</param>
        </member>
        <member name="M:RDR2.UI.ContainerElement.ScaledDraw">
            <summary>
            Draws this <see cref="T:RDR2.UI.ContainerElement" /> this frame using the width returned in <see cref="P:RDR2.UI.Screen.ScaledWidth" />.
            </summary>
        </member>
        <member name="M:RDR2.UI.ContainerElement.ScaledDraw(System.Drawing.SizeF)">
            <summary>
            Draws this <see cref="T:RDR2.UI.ContainerElement" /> this frame at the specified offset using the width returned in <see cref="P:RDR2.UI.Screen.ScaledWidth" />.
            </summary>
            <param name="offset">The offset to shift the draw position of this <see cref="T:RDR2.UI.ContainerElement" /> using a <see cref="P:RDR2.UI.Screen.ScaledWidth" />*720 pixel base.</param>
        </member>
        <member name="T:RDR2.UI.CustomSprite">
            <summary>
            A sprite element using a custom image texture.
            </summary>
        </member>
        <member name="M:RDR2.UI.CustomSprite.#ctor(System.String,System.Drawing.SizeF,System.Drawing.PointF)">
            <summary>
            Initializes a new instance of the <see cref="T:RDR2.UI.CustomSprite"/> class used for drawing external textures on the screen.
            </summary>
            <param name="filename">Full path to location of the <see cref="T:RDR2.UI.CustomSprite"/> on the disc.</param>
            <param name="size">Set the <see cref="P:RDR2.UI.CustomSprite.Size"/> of the <see cref="T:RDR2.UI.CustomSprite"/>.</param>
            <param name="position">Set the <see cref="P:RDR2.UI.CustomSprite.Position"/> on screen where to draw the <see cref="T:RDR2.UI.CustomSprite"/>.</param>
            <exception cref="T:System.IO.FileNotFoundException">Thrown if the specified file doesn't exist</exception>
        </member>
        <member name="M:RDR2.UI.CustomSprite.#ctor(System.String,System.Drawing.SizeF,System.Drawing.PointF,System.Drawing.Color)">
            <summary>
            Initializes a new instance of the <see cref="T:RDR2.UI.CustomSprite"/> class used for drawing external textures on the screen.
            </summary>
            <param name="filename">Full path to location of the <see cref="T:RDR2.UI.CustomSprite"/> on the disc.</param>
            <param name="size">Set the <see cref="P:RDR2.UI.CustomSprite.Size"/> of the <see cref="T:RDR2.UI.CustomSprite"/>.</param>
            <param name="position">Set the <see cref="P:RDR2.UI.CustomSprite.Position"/> on screen where to draw the <see cref="T:RDR2.UI.CustomSprite"/>.</param>
            <param name="color">Set the <see cref="P:RDR2.UI.CustomSprite.Color"/> used to draw the <see cref="T:RDR2.UI.CustomSprite"/>.</param>
            <exception cref="T:System.IO.FileNotFoundException">Thrown if the specified file doesn't exist</exception>
        </member>
        <member name="M:RDR2.UI.CustomSprite.#ctor(System.String,System.Drawing.SizeF,System.Drawing.PointF,System.Drawing.Color,System.Single)">
            <summary>
            Initializes a new instance of the <see cref="T:RDR2.UI.CustomSprite"/> class used for drawing external textures on the screen.
            </summary>
            <param name="filename">Full path to location of the <see cref="T:RDR2.UI.CustomSprite"/> on the disc.</param>
            <param name="size">Set the <see cref="P:RDR2.UI.CustomSprite.Size"/> of the <see cref="T:RDR2.UI.CustomSprite"/>.</param>
            <param name="position">Set the <see cref="P:RDR2.UI.CustomSprite.Position"/> on screen where to draw the <see cref="T:RDR2.UI.CustomSprite"/>.</param>
            <param name="color">Set the <see cref="P:RDR2.UI.CustomSprite.Color"/> used to draw the <see cref="T:RDR2.UI.CustomSprite"/>.</param>
            <param name="rotation">Set the rotation to draw the sprite, measured in degrees, see also <seealso cref="P:RDR2.UI.CustomSprite.Rotation"/>.</param>
            <exception cref="T:System.IO.FileNotFoundException">Thrown if the specified file doesn't exist</exception>
        </member>
        <member name="M:RDR2.UI.CustomSprite.#ctor(System.String,System.Drawing.SizeF,System.Drawing.PointF,System.Drawing.Color,System.Single,System.Boolean)">
            <summary>
            Initializes a new instance of the <see cref="T:RDR2.UI.CustomSprite"/> class used for drawing external textures on the screen.
            </summary>
            <param name="filename">Full path to location of the <see cref="T:RDR2.UI.CustomSprite"/> on the disc.</param>
            <param name="size">Set the <see cref="P:RDR2.UI.CustomSprite.Size"/> of the <see cref="T:RDR2.UI.CustomSprite"/>.</param>
            <param name="position">Set the <see cref="P:RDR2.UI.CustomSprite.Position"/> on screen where to draw the <see cref="T:RDR2.UI.CustomSprite"/>.</param>
            <param name="color">Set the <see cref="P:RDR2.UI.CustomSprite.Color"/> used to draw the <see cref="T:RDR2.UI.CustomSprite"/>.</param>
            <param name="rotation">Set the rotation to draw the sprite, measured in degrees, see also <seealso cref="P:RDR2.UI.CustomSprite.Rotation"/>.</param>
            <param name="centered">Position the <see cref="T:RDR2.UI.CustomSprite"/> based on its center instead of top left corner, see also <seealso cref="P:RDR2.UI.CustomSprite.Centered"/>.</param>
            <exception cref="T:System.IO.FileNotFoundException">Thrown if the specified file doesn't exist</exception>
        </member>
```
