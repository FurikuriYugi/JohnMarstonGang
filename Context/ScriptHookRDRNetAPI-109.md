<!-- ScriptHookRDRNetAPI lines 27001-27250 of 30005 -->
```csharp
        <member name="P:RDR2.UI.CustomSprite.Enabled">
            <summary>
            Gets or sets a value indicating whether this <see cref="T:RDR2.UI.CustomSprite" /> will be drawn.
            </summary>
            <value>
              <c>true</c> if enabled; otherwise, <c>false</c>.
            </value>
        </member>
        <member name="P:RDR2.UI.CustomSprite.Color">
            <summary>
            Gets or sets the color of this <see cref="T:RDR2.UI.CustomSprite" />.
            </summary>
            <value>
            The color.
            </value>
        </member>
        <member name="P:RDR2.UI.CustomSprite.Position">
            <summary>
            Gets or sets the position of this <see cref="T:RDR2.UI.CustomSprite" />.
            </summary>
            <value>
            The position scaled on a 1280*720 pixel base.
            </value>
            <remarks>
            If ScaledDraw is called, the position will be scaled by the width returned in <see cref="P:RDR2.UI.Screen.ScaledWidth" />.
            </remarks>
        </member>
        <member name="P:RDR2.UI.CustomSprite.Size">
            <summary>
            Gets or sets the size to draw the <see cref="T:RDR2.UI.CustomSprite" />
            </summary>
            <value>
            The size on a 1280*720 pixel base
            </value>
            <remarks>
            If ScaledDraw is called, the size will be scaled by the width returned in <see cref="P:RDR2.UI.Screen.ScaledWidth" />.
            </remarks>
        </member>
        <member name="P:RDR2.UI.CustomSprite.Rotation">
            <summary>
            Gets or sets the rotation to draw thie <see cref="T:RDR2.UI.CustomSprite" />.
            </summary>
            <value>
            The rotation measured in degrees, clockwise increasing, 0.0 at vertical
            </value>
        </member>
        <member name="P:RDR2.UI.CustomSprite.Centered">
            <summary>
            Gets or sets a value indicating whether this <see cref="T:RDR2.UI.CustomSprite"/> should be positioned based on its center or top left corner
            </summary>
            <value>
              <c>true</c> if centered; otherwise, <c>false</c>.
            </value>
        </member>
        <member name="M:RDR2.UI.CustomSprite.Draw">
            <summary>
            Draws this <see cref="T:RDR2.UI.CustomSprite" />.
            </summary>
        </member>
        <member name="M:RDR2.UI.CustomSprite.Draw(System.Drawing.SizeF)">
            <summary>
            Draws the <see cref="T:RDR2.UI.CustomSprite" /> at the specified offset.
            </summary>
            <param name="offset">The offset.</param>
        </member>
        <member name="M:RDR2.UI.CustomSprite.ScaledDraw">
            <summary>
            Draws this <see cref="T:RDR2.UI.CustomSprite" /> using the width returned in <see cref="P:RDR2.UI.Screen.ScaledWidth" />.
            </summary>
        </member>
        <member name="M:RDR2.UI.CustomSprite.ScaledDraw(System.Drawing.SizeF)">
            <summary>
            Draws the <see cref="T:RDR2.UI.CustomSprite" /> at the specified offset using the width returned in <see cref="P:RDR2.UI.Screen.ScaledWidth" />.
            </summary>
            <param name="offset">The offset.</param>
        </member>
        <member name="T:RDR2.UI.Font">
            <summary>
            An enumeration of fonts the game supports.
            </summary>
        </member>
        <member name="F:RDR2.UI.Font.HapnaSlabSerifDemiBold">
            <summary>
            Body
            </summary>
        </member>
        <member name="F:RDR2.UI.Font.DroidSerifPro">
            <summary>
            Body1
            </summary>
        </member>
        <member name="F:RDR2.UI.Font.RDRGothica">
            <summary>
            Catalog1
            </summary>
        </member>
        <member name="F:RDR2.UI.Font.RDRCatalogueNumbers">
            <summary>
            Catalog2
            </summary>
        </member>
        <member name="F:RDR2.UI.Font.ArialDEBUG">
            <summary>
            Catalog3
            </summary>
        </member>
        <member name="F:RDR2.UI.Font.RDRCatalogueBold">
            <summary>
            Catalog4
            </summary>
        </member>
        <member name="F:RDR2.UI.Font.CabritoNormDemi">
            <summary>
            Catalog5
            </summary>
        </member>
        <member name="F:RDR2.UI.Font.RDRChalkHand">
            <summary>
            Chalk
            </summary>
        </member>
        <member name="F:RDR2.UI.Font.HelveticaNeueLT47LightCn">
            <summary>
            Debug_Bold
            </summary>
        </member>
        <member name="F:RDR2.UI.Font.RDRLinoNumbers">
            <summary>
            FixedWidthNumbers
            </summary>
        </member>
        <member name="F:RDR2.UI.Font.Redemption">
            <summary>
            Font5
            </summary>
        </member>
        <member name="F:RDR2.UI.Font.RockstarGamertagCond">
            <summary>
            Gamername
            </summary>
        </member>
        <member name="F:RDR2.UI.Font.DreamerScript">
            <summary>
            Handwritten
            </summary>
        </member>
        <member name="F:RDR2.UI.Font.RDRLedgerHand">
            <summary>
            Ledger
            </summary>
        </member>
        <member name="F:RDR2.UI.Font.RockstarTag">
            <summary>
            RockstarTAG
            </summary>
        </member>
        <member name="F:RDR2.UI.Font.Arial">
            <summary>
            Social_Club_Cond_Bold
            </summary>
        </member>
        <member name="F:RDR2.UI.Font.RDRLino">
            <summary>
            Title
            </summary>
        </member>
        <member name="F:RDR2.UI.Font.RDRTurri">
            <summary>
            WantedPostersGeneric
            </summary>
        </member>
        <member name="T:RDR2.UI.Hud">
            <summary>
            Methods to manipulate the HUD (heads-up-display) of the game.
            </summary>
        </member>
        <member name="M:RDR2.UI.Hud.EnableHudContext(RDR2.UI.eHudContext)">
            <summary>
            Enables a <see cref="T:RDR2.UI.eHudContext"/>
            </summary>
            <param name="component">The <see cref="T:RDR2.UI.eHudContext"/> to enable</param>
        </member>
        <member name="M:RDR2.UI.Hud.DisableHudContext(RDR2.UI.eHudContext)">
            <summary>
            Disables a <see cref="T:RDR2.UI.eHudContext"/>
            </summary>
            <param name="component">The <see cref="T:RDR2.UI.eHudContext"/> to disable</param>
        </member>
        <member name="M:RDR2.UI.Hud.ShowCursorThisFrame">
            <summary>
            Shows the mouse cursor this frame.
            </summary>
        </member>
        <member name="P:RDR2.UI.Hud.CursorSprite">
            <summary>
            Gets or sets the sprite the cursor should used when drawn
            </summary>
        </member>
        <member name="P:RDR2.UI.Hud.IsVisible">
            <summary>
            Gets or sets a value indicating whether any HUD components should be rendered.
            </summary>
        </member>
        <member name="P:RDR2.UI.Hud.IsRadarVisible">
            <summary>
            Gets or sets a value indicating whether the radar is visible.
            </summary>
        </member>
        <member name="P:RDR2.UI.Hud.RadarZoom">
            <summary>
            Sets how far the minimap should be zoomed in.
            </summary>
            <value>
            The radar zoom; accepts values from 0 to 200.
            </value>
        </member>
        <member name="P:RDR2.UI.IElement.Enabled">
            <summary>
            Gets or sets a value indicating whether this <see cref="T:RDR2.UI.IElement"/> will be drawn.
            </summary>
            <value>
              <c>true</c> if enabled; otherwise, <c>false</c>.
            </value>
        </member>
        <member name="P:RDR2.UI.IElement.Color">
            <summary>
            Gets or sets the color of this <see cref="T:RDR2.UI.IElement"/>.
            </summary>
            <value>
            The color.
            </value>
        </member>
        <member name="P:RDR2.UI.IElement.Position">
            <summary>
            Gets or sets the position of this <see cref="T:RDR2.UI.IElement"/>.
            </summary>
            <value>
            The position scaled on a 1280*720 pixel base.
            </value>
            <remarks>
            If ScaledDraw is called, the position will be scaled by the width returned in <see cref="P:RDR2.UI.Screen.ScaledWidth"/>.
            </remarks>
        </member>
        <member name="P:RDR2.UI.IElement.Centered">
            <summary>
            Gets or sets a value indicating whether this <see cref="T:RDR2.UI.IElement"/> should be positioned based on its center or top left corner
            </summary>
            <value>
              <c>true</c> if centered; otherwise, <c>false</c>.
            </value>
```
