<!-- ScriptHookRDRNetAPI lines 27501-27750 of 30005 -->
```csharp
            <remarks>
            If ScaledDraw is called, the position will be scaled by the width returned in <see cref="P:RDR2.UI.Screen.ScaledWidth" />.
            </remarks>
        </member>
        <member name="P:RDR2.UI.Sprite.Size">
            <summary>
            Gets or sets the size to draw the <see cref="T:RDR2.UI.Sprite" />
            </summary>
            <value>
            The size on a 1280*720 pixel base
            </value>
            <remarks>
            If ScaledDraw is called, the size will be scaled by the width returned in <see cref="P:RDR2.UI.Screen.ScaledWidth" />.
            </remarks>
        </member>
        <member name="P:RDR2.UI.Sprite.Rotation">
            <summary>
            Gets or sets the rotation to draw thie <see cref="T:RDR2.UI.Sprite" />.
            </summary>
            <value>
            The rotation measured in degrees, clockwise increasing, 0.0 at vertical
            </value>
        </member>
        <member name="P:RDR2.UI.Sprite.Centered">
            <summary>
            Gets or sets a value indicating whether this <see cref="T:RDR2.UI.Sprite"/> should be positioned based on its center or top left corner
            </summary>
            <value>
              <c>true</c> if centered; otherwise, <c>false</c>.
            </value>
        </member>
        <member name="M:RDR2.UI.Sprite.Draw">
            <summary>
            Draws this <see cref="T:RDR2.UI.Sprite" />.
            </summary>
        </member>
        <member name="M:RDR2.UI.Sprite.Draw(System.Drawing.SizeF)">
            <summary>
            Draws the <see cref="T:RDR2.UI.Sprite" /> at the specified offset.
            </summary>
            <param name="offset">The offset.</param>
        </member>
        <member name="M:RDR2.UI.Sprite.ScaledDraw">
            <summary>
            Draws this <see cref="T:RDR2.UI.Sprite" /> using the width returned in <see cref="P:RDR2.UI.Screen.ScaledWidth" />.
            </summary>
        </member>
        <member name="M:RDR2.UI.Sprite.ScaledDraw(System.Drawing.SizeF)">
            <summary>
            Draws the <see cref="T:RDR2.UI.Sprite" /> at the specified offset using the width returned in <see cref="P:RDR2.UI.Screen.ScaledWidth" />.
            </summary>
            <param name="offset">The offset.</param>
        </member>
        <member name="M:RDR2.UI.TextElement.#ctor(System.String,System.Drawing.PointF,System.Single)">
            <summary>
            Initializes a new instance of the <see cref="T:RDR2.UI.TextElement"/> class used for drawing text on the screen.
            </summary>
            <param name="caption">The <see cref="T:RDR2.UI.TextElement"/> to draw.</param>
            <param name="position">Set the <see cref="P:RDR2.UI.TextElement.Position"/> on screen where to draw the <see cref="T:RDR2.UI.TextElement"/>.</param>
            <param name="scale">Sets a <see cref="P:RDR2.UI.TextElement.Scale"/> used to increase of decrease the size of the <see cref="T:RDR2.UI.TextElement"/>, for no scaling use 1.0f.</param>
        </member>
        <member name="M:RDR2.UI.TextElement.#ctor(System.String,System.Drawing.PointF,System.Single,System.Drawing.Color)">
            <summary>
            Initializes a new instance of the <see cref="T:RDR2.UI.TextElement"/> class used for drawing text on the screen.
            </summary>
            <param name="caption">The <see cref="T:RDR2.UI.TextElement"/> to draw.</param>
            <param name="position">Set the <see cref="P:RDR2.UI.TextElement.Position"/> on screen where to draw the <see cref="T:RDR2.UI.TextElement"/>.</param>
            <param name="scale">Sets a <see cref="P:RDR2.UI.TextElement.Scale"/> used to increase of decrease the size of the <see cref="T:RDR2.UI.TextElement"/>, for no scaling use 1.0f.</param>
            <param name="color">Set the <see cref="P:RDR2.UI.TextElement.Color"/> used to draw the <see cref="T:RDR2.UI.TextElement"/>.</param>
        </member>
        <member name="M:RDR2.UI.TextElement.#ctor(System.String,System.Drawing.PointF,System.Single,System.Drawing.Color,RDR2.UI.Alignment)">
            <summary>
            Initializes a new instance of the <see cref="T:RDR2.UI.TextElement"/> class used for drawing text on the screen.
            </summary>
            <param name="caption">The <see cref="T:RDR2.UI.TextElement"/> to draw.</param>
            <param name="position">Set the <see cref="P:RDR2.UI.TextElement.Position"/> on screen where to draw the <see cref="T:RDR2.UI.TextElement"/>.</param>
            <param name="scale">Sets a <see cref="P:RDR2.UI.TextElement.Scale"/> used to increase of decrease the size of the <see cref="T:RDR2.UI.TextElement"/>, for no scaling use 1.0f.</param>
            <param name="color">Set the <see cref="P:RDR2.UI.TextElement.Color"/> used to draw the <see cref="T:RDR2.UI.TextElement"/>.</param>
            <param name="alignment">Sets the <see cref="P:RDR2.UI.TextElement.Alignment"/> used when drawing the text, <see cref="F:RDR2.UI.Alignment.Left"/>,<see cref="F:RDR2.UI.Alignment.Center"/> or <see cref="F:RDR2.UI.Alignment.Right"/>.</param>
        </member>
        <member name="M:RDR2.UI.TextElement.#ctor(System.String,System.Drawing.PointF,System.Single,System.Drawing.Color,RDR2.UI.Alignment,System.Boolean,System.Boolean)">
            <summary>
            Initializes a new instance of the <see cref="T:RDR2.UI.TextElement"/> class used for drawing text on the screen.
            </summary>
            <param name="caption">The <see cref="T:RDR2.UI.TextElement"/> to draw.</param>
            <param name="position">Set the <see cref="P:RDR2.UI.TextElement.Position"/> on screen where to draw the <see cref="T:RDR2.UI.TextElement"/>.</param>
            <param name="scale">Sets a <see cref="P:RDR2.UI.TextElement.Scale"/> used to increase of decrease the size of the <see cref="T:RDR2.UI.TextElement"/>, for no scaling use 1.0f.</param>
            <param name="color">Set the <see cref="P:RDR2.UI.TextElement.Color"/> used to draw the <see cref="T:RDR2.UI.TextElement"/>.</param>
            <param name="alignment">Sets the <see cref="P:RDR2.UI.TextElement.Alignment"/> used when drawing the text, <see cref="F:RDR2.UI.Alignment.Left"/>,<see cref="F:RDR2.UI.Alignment.Center"/> or <see cref="F:RDR2.UI.Alignment.Right"/>.</param>
            <param name="shadow">Sets whether or not to draw the <see cref="T:RDR2.UI.TextElement"/> with a <see cref="P:RDR2.UI.TextElement.Shadow"/> effect.</param>
            <param name="outline">Sets whether or not to draw the <see cref="T:RDR2.UI.TextElement"/> with an <see cref="P:RDR2.UI.TextElement.Outline"/> around the letters.</param>
        </member>
        <member name="M:RDR2.UI.TextElement.#ctor(System.String,System.Drawing.PointF,System.Single,System.Drawing.Color,RDR2.UI.Alignment,System.Boolean,System.Boolean,System.Single)">
            <summary>
            Initializes a new instance of the <see cref="T:RDR2.UI.TextElement"/> class used for drawing text on the screen.
            </summary>
            <param name="caption">The <see cref="T:RDR2.UI.TextElement"/> to draw.</param>
            <param name="position">Set the <see cref="P:RDR2.UI.TextElement.Position"/> on screen where to draw the <see cref="T:RDR2.UI.TextElement"/>.</param>
            <param name="scale">Sets a <see cref="P:RDR2.UI.TextElement.Scale"/> used to increase of decrease the size of the <see cref="T:RDR2.UI.TextElement"/>, for no scaling use 1.0f.</param>
            <param name="color">Set the <see cref="P:RDR2.UI.TextElement.Color"/> used to draw the <see cref="T:RDR2.UI.TextElement"/>.</param>
            <param name="alignment">Sets the <see cref="P:RDR2.UI.TextElement.Alignment"/> used when drawing the text, <see cref="F:RDR2.UI.Alignment.Left"/>,<see cref="F:RDR2.UI.Alignment.Center"/> or <see cref="F:RDR2.UI.Alignment.Right"/>.</param>
            <param name="shadow">Sets whether or not to draw the <see cref="T:RDR2.UI.TextElement"/> with a <see cref="P:RDR2.UI.TextElement.Shadow"/> effect.</param>
            <param name="outline">Sets whether or not to draw the <see cref="T:RDR2.UI.TextElement"/> with an <see cref="P:RDR2.UI.TextElement.Outline"/> around the letters.</param>
            <param name="wrapWidth">Sets how many horizontal pixel to draw before wrapping the <see cref="T:RDR2.UI.TextElement"/> on the next line down.</param>
        </member>
        <member name="P:RDR2.UI.TextElement.Enabled">
            <summary>
            Gets or sets a value indicating whether this <see cref="T:RDR2.UI.TextElement" /> will be drawn.
            </summary>
            <value>
              <c>true</c> if enabled; otherwise, <c>false</c>.
            </value>
        </member>
        <member name="P:RDR2.UI.TextElement.Color">
            <summary>
            Gets or sets the color of this <see cref="T:RDR2.UI.TextElement" />.
            </summary>
            <value>
            The color.
            </value>
        </member>
        <member name="P:RDR2.UI.TextElement.Position">
            <summary>
            Gets or sets the position of this <see cref="T:RDR2.UI.TextElement" />.
            </summary>
            <value>
            The position scaled on a 1280*720 pixel base.
            </value>
            <remarks>
            If ScaledDraw is called, the position will be scaled by the width returned in <see cref="P:RDR2.UI.Screen.ScaledWidth" />.
            </remarks>
        </member>
        <member name="P:RDR2.UI.TextElement.Scale">
            <summary>
            Gets or sets the scale of this <see cref="T:RDR2.UI.TextElement"/>.
            </summary>
            <value>
            The scale usually a value between ~0.5 and 3.0, Default = 1.0
            </value>
        </member>
        <member name="P:RDR2.UI.TextElement.Caption">
            <summary>
            Gets or sets the text to draw in this <see cref="T:RDR2.UI.TextElement"/>.
            </summary>
            <value>
            The caption.
            </value>
        </member>
        <member name="P:RDR2.UI.TextElement.Alignment">
            <summary>
            Gets or sets the alignment of this <see cref="T:RDR2.UI.TextElement"/>.
            </summary>
            <value>
            The alignment:<c>Left</c>, <c>Center</c>, <c>Right</c> Justify
            </value>
        </member>
        <member name="P:RDR2.UI.TextElement.Shadow">
            <summary>
            Gets or sets a value indicating whether this <see cref="T:RDR2.UI.TextElement"/> is drawn with a shadow effect.
            </summary>
            <value>
              <c>true</c> if shadow; otherwise, <c>false</c>.
            </value>
        </member>
        <member name="P:RDR2.UI.TextElement.Outline">
            <summary>
            Gets or sets a value indicating whether this <see cref="T:RDR2.UI.TextElement"/> is drawn with an outline.
            </summary>
            <value>
              <c>true</c> if outline; otherwise, <c>false</c>.
            </value>
        </member>
        <member name="P:RDR2.UI.TextElement.WrapWidth">
            <summary>
            Gets or sets the maximum size of the <see cref="T:RDR2.UI.TextElement"/> before it wraps to a new line.
            </summary>
            <value>
            The width of the <see cref="T:RDR2.UI.TextElement"/>.
            </value>
        </member>
        <member name="P:RDR2.UI.TextElement.Centered">
            <summary>
            Gets or sets a value indicating whether the alignment of this <see cref="T:RDR2.UI.TextElement" /> is centered.
            See <see cref="P:RDR2.UI.TextElement.Alignment"/>
            </summary>
            <value>
              <c>true</c> if centered; otherwise, <c>false</c>.
            </value>
        </member>
        <member name="M:RDR2.UI.TextElement.Draw">
            <summary>
            Draws the <see cref="T:RDR2.UI.TextElement" /> this frame.
            </summary>
        </member>
        <member name="M:RDR2.UI.TextElement.Draw(System.Drawing.SizeF)">
            <summary>
            Draws the <see cref="T:RDR2.UI.TextElement" /> this frame at the specified offset.
            </summary>
            <param name="offset">The offset to shift the draw position of this <see cref="T:RDR2.UI.TextElement" /> using a 1280*720 pixel base.</param>
        </member>
        <member name="M:RDR2.UI.TextElement.ScaledDraw">
            <summary>
            Draws the <see cref="T:RDR2.UI.TextElement" /> this frame using the width returned in <see cref="P:RDR2.UI.Screen.ScaledWidth" />.
            </summary>
        </member>
        <member name="M:RDR2.UI.TextElement.ScaledDraw(System.Drawing.SizeF)">
            <summary>
            Draws the <see cref="T:RDR2.UI.TextElement" /> this frame at the specified offset using the width returned in <see cref="P:RDR2.UI.Screen.ScaledWidth" />.
            </summary>
            <param name="offset">The offset to shift the draw position of this <see cref="T:RDR2.UI.TextElement" /> using a <see cref="P:RDR2.UI.Screen.ScaledWidth" />*720 pixel base.</param>
        </member>
        <member name="M:RDR2.UI.Prompt.CreatePrompt(RDR2.eInputType,RDR2.UI.eUseContextMode,System.String,System.Int32,System.Int32,System.Int32,System.Int32,RDR2.UI.PromptTimingEvent)">
            <summary>
            Create a interactable HUD prompt.
            </summary>
        </member>
        <member name="P:RDR2.UI.Prompt.Text">
            <summary>
            Gets or sets the text on this <see cref="T:RDR2.UI.Prompt"/>.
            </summary>
        </member>
        <member name="P:RDR2.UI.Prompt.Visible">
            <summary>
            Gets or sets the visibility on this <see cref="T:RDR2.UI.Prompt"/>.
            </summary>
        </member>
        <member name="P:RDR2.UI.Prompt.Enabled">
            <summary>
            Gets or sets if this <see cref="T:RDR2.UI.Prompt"/> is enabled.
            </summary>
        </member>
        <member name="P:RDR2.UI.Prompt.IsPressed">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.UI.Prompt"/> is pressed.
            </summary>
        </member>
        <member name="P:RDR2.UI.Prompt.IsJustPressed">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.UI.Prompt"/> is just pressed.
            </summary>
        </member>
        <member name="P:RDR2.UI.Prompt.IsReleased">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.UI.Prompt"/> is released.
            </summary>
        </member>
        <member name="P:RDR2.UI.Prompt.IsJustReleased">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.UI.Prompt"/> is just pressed.
            </summary>
```
