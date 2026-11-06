<!-- ScriptHookRDRNetAPI lines 29751-30000 of 30005 -->
```csharp
        <member name="M:RDR2.ScriptSettings.SetValue``1(System.String,System.String,``0)">
            <summary>
            Sets a value in this <see cref="T:RDR2.ScriptSettings"/>.
            </summary>
            <param name="section">The section where the value is.</param>
            <param name="name">The name of the key the value is saved at.</param>
            <param name="value">The value to set the key to.</param>
        </member>
        <member name="M:RDR2.ScriptSettings.GetAllValues``1(System.String,System.String)">
            <summary>
            Reads all the values at a specified key and section from this <see cref="T:RDR2.ScriptSettings"/>.
            </summary>
            <param name="section">The section where the value is.</param>
            <param name="name">The name of the key the values are saved at.</param>
        </member>
        <member name="P:RDR2.Volume.Position">
            <summary>
            Gets or sets the position of this <see cref="T:RDR2.Volume"/>.
            </summary>
        </member>
        <member name="P:RDR2.Volume.Rotation">
            <summary>
            Gets or sets the rotation of this <see cref="T:RDR2.Volume"/>.
            </summary>
        </member>
        <member name="P:RDR2.Volume.Scale">
            <summary>
            Gets or sets the scale of this <see cref="T:RDR2.Volume"/>.
            </summary>
        </member>
        <member name="P:RDR2.Volume.IsAggregateVolume">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Volume"/> is aggregate.
            </summary>
        </member>
        <member name="P:RDR2.Volume.RelationshipGroup">
            <summary>
            Gets or sets the relationship group hash of this <see cref="T:RDR2.Volume"/>.
            </summary>
        </member>
        <member name="P:RDR2.Weapon.IsValid">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Weapon"/> is valid.
            </summary>
        </member>
        <member name="P:RDR2.Weapon.IsGun">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Weapon"/> has flag CWeaponInfoFlags::Flags::Gun set.
            </summary>
        </member>
        <member name="P:RDR2.Weapon.IsBow">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Weapon"/> is a bow.
            </summary>
        </member>
        <member name="P:RDR2.Weapon.IsMelee">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Weapon"/> is a melee weapon.
            </summary>
        </member>
        <member name="P:RDR2.Weapon.IsPistol">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Weapon"/> is a pistol.
            </summary>
        </member>
        <member name="P:RDR2.Weapon.IsRepeater">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Weapon"/> is repeater.
            </summary>
        </member>
        <member name="P:RDR2.Weapon.IsRifle">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Weapon"/> is rifle.
            </summary>
        </member>
        <member name="P:RDR2.Weapon.IsSniper">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Weapon"/> is a sniper.
            </summary>
        </member>
        <member name="P:RDR2.Weapon.IsShotgun">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Weapon"/> is shotgun.
            </summary>
        </member>
        <member name="P:RDR2.Weapon.IsKit">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Weapon"/> is a kit weapon.
            </summary>
        </member>
        <member name="P:RDR2.Weapon.IsOneHanded">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Weapon"/> is one handed.
            </summary>
        </member>
        <member name="P:RDR2.Weapon.IsTwoHanded">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Weapon"/> is two handed.
            </summary>
        </member>
        <member name="P:RDR2.Weapon.IsThrowable">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Weapon"/> is throwable.
            </summary>
        </member>
        <member name="P:RDR2.Weapon.ClipSize">
            <summary>
            Gets the amount of ammo this <see cref="T:RDR2.Weapon"/> can hold in its clip.
            </summary>
        </member>
        <member name="P:RDR2.Weapon.CanBeTwirled">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Weapon"/> can be twirled.
            </summary>
        </member>
        <member name="P:RDR2.Weapon.Model">
            <summary>
            Get the <see cref="T:RDR2.Model"/> of this <see cref="T:RDR2.Weapon"/>
            </summary>
        </member>
        <member name="P:RDR2.Weapon.Group">
            <summary>
            Get the <see cref="T:RDR2.eWeaponGroup"/> of this <see cref="T:RDR2.Weapon"/>
            </summary>
        </member>
        <member name="P:RDR2.Weapon.SlotHash">
            <summary>
            Get the slot hash of this <see cref="T:RDR2.Weapon"/>
            </summary>
        </member>
        <member name="P:RDR2.Weapon.Name">
            <summary>
            Get the name of this <see cref="T:RDR2.Weapon"/>
            </summary>
        </member>
        <member name="P:RDR2.Weapon.PedHasThisWeapon">
            <summary>
            Gets a value indicating whether the <see cref="P:RDR2.Weapon.Owner"/> has this <see cref="T:RDR2.Weapon"/>.
            </summary>
        </member>
        <member name="P:RDR2.Weapon.IsBeingCarried">
            <summary>
            Gets a value indicating whether the <see cref="P:RDR2.Weapon.Owner"/> is carrying this <see cref="T:RDR2.Weapon"/>.
            </summary>
        </member>
        <member name="P:RDR2.Weapon.DefaultAmmoType">
            <summary>
            Gets the <see cref="T:RDR2.eAmmoType"/> that this weapon uses by default.
            </summary>
        </member>
        <member name="P:RDR2.Weapon.CurrentAmmoType">
            <summary>
            Gets the <see cref="T:RDR2.eAmmoType"/> that this weapon currently using.
            </summary>
        </member>
        <member name="P:RDR2.Weapon.InfiniteAmmo">
            <summary>
            Sets this <see cref="T:RDR2.Weapon"/> to have infinite ammo.
            </summary>
        </member>
        <member name="P:RDR2.Weapon.Ammo">
            <summary>
            Get or set the total amount of ammo that this <see cref="T:RDR2.Weapon"/> has.
            </summary>
        </member>
        <member name="P:RDR2.Weapon.AmmoInClip">
            <summary>
            Get or set the amount of ammo that this <see cref="T:RDR2.Weapon"/> has in its clip.
            </summary>
        </member>
        <member name="P:RDR2.Weapon.MaxAmmo">
            <summary>
            Get the max amount of ammo that this <see cref="T:RDR2.Weapon"/> has.
            </summary>
        </member>
        <member name="M:RDR2.World.GetAllPeds">
            <summary>
            Gets all of the <see cref="T:RDR2.Ped"/>s currently spawned/loaded in the game world
            </summary>
            <returns>An <see cref="T:System.Array"/> of all <see cref="T:RDR2.Ped"/>s found</returns>
            <remarks>Note: This function can return <see cref="M:System.Array.Empty``1"/> if the internal call to worldGetAllPeds() failed</remarks>
        </member>
        <member name="M:RDR2.World.GetAllVehicles">
            <summary>
            Gets all of the <see cref="T:RDR2.Vehicle"/>s currently spawned/loaded in the game world
            </summary>
            <returns>An <see cref="T:System.Array"/> of all <see cref="T:RDR2.Vehicle"/>s found</returns>
            <remarks>Note: This function can return <see cref="M:System.Array.Empty``1"/> if the internal call to worldGetAllVehicles() failed</remarks>
        </member>
        <member name="M:RDR2.World.GetAllObjects">
            <summary>
            Gets all of the <see cref="T:RDR2.Prop"/>s currently spawned/loaded in the game world
            </summary>
            <returns>An <see cref="T:System.Array"/> of all <see cref="T:RDR2.Prop"/>s found</returns>
            <remarks>Note: This function can return <see cref="M:System.Array.Empty``1"/> if the internal call to worldGetAllObjects() failed</remarks>
        </member>
        <member name="M:RDR2.World.CreateProp(RDR2.Model,RDR2.Math.Vector3,System.Boolean,System.Boolean)">
            <summary>
            Spawns a <see cref="T:RDR2.Prop"/> of the given <see cref="T:RDR2.Model"/> at the position specified.
            </summary>
            <param name="model">The <see cref="T:RDR2.Model"/> of the <see cref="T:RDR2.Prop"/>.</param>
            <param name="position">The position to spawn the <see cref="T:RDR2.Prop"/> at.</param>
            <param name="dynamic">if set to <c>true</c> the <see cref="T:RDR2.Prop"/> will have physics; otherwise, it will be static.</param>
            <param name="placeOnGround">if set to <c>true</c> place the prop on the ground nearest to the <paramref name="position"/>.</param>
            <remarks>returns <c>null</c> if the <see cref="T:RDR2.Prop"/> could not be spawned</remarks>
        </member>
        <member name="M:RDR2.World.CreateProp(RDR2.Model,RDR2.Math.Vector3,RDR2.Math.Vector3,System.Boolean,System.Boolean)">
            <summary>
            Spawns a <see cref="T:RDR2.Prop"/> of the given <see cref="T:RDR2.Model"/> at the position specified.
            </summary>
            <param name="model">The <see cref="T:RDR2.Model"/> of the <see cref="T:RDR2.Prop"/>.</param>
            <param name="position">The position to spawn the <see cref="T:RDR2.Prop"/> at.</param>
            <param name="rotation">The rotation of the <see cref="T:RDR2.Prop"/>.</param>
            <param name="dynamic">if set to <c>true</c> the <see cref="T:RDR2.Prop"/> will have physics; otherwise, it will be static.</param>
            <param name="placeOnGround">if set to <c>true</c> place the prop on the ground nearest to the <paramref name="position"/>.</param>
            <remarks>returns <c>null</c> if the <see cref="T:RDR2.Prop"/> could not be spawned</remarks>
        </member>
        <member name="M:RDR2.World.CreatePropNoOffset(RDR2.Model,RDR2.Math.Vector3,System.Boolean)">
            <summary>
            Spawns a <see cref="T:RDR2.Prop"/> of the given <see cref="T:RDR2.Model"/> at the position specified without any offset.
            </summary>
            <param name="model">The <see cref="T:RDR2.Model"/> of the <see cref="T:RDR2.Prop"/>.</param>
            <param name="position">The position to spawn the <see cref="T:RDR2.Prop"/> at.</param>
            <param name="dynamic">if set to <c>true</c> the <see cref="T:RDR2.Prop"/> will have physics; otherwise, it will be static.</param>
            <remarks>returns <c>null</c> if the <see cref="T:RDR2.Prop"/> could not be spawned</remarks>
        </member>
        <member name="M:RDR2.World.CreatePropNoOffset(RDR2.Model,RDR2.Math.Vector3,RDR2.Math.Vector3,System.Boolean)">
            <summary>
            Spawns a <see cref="T:RDR2.Prop"/> of the given <see cref="T:RDR2.Model"/> at the position specified without any offset.
            </summary>
            <param name="model">The <see cref="T:RDR2.Model"/> of the <see cref="T:RDR2.Prop"/>.</param>
            <param name="position">The position to spawn the <see cref="T:RDR2.Prop"/> at.</param>
            <param name="rotation">The rotation of the <see cref="T:RDR2.Prop"/>.</param>
            <param name="dynamic">if set to <c>true</c> the <see cref="T:RDR2.Prop"/> will have physics; otherwise, it will be static.</param>
            <remarks>returns <c>null</c> if the <see cref="T:RDR2.Prop"/> could not be spawned</remarks>
        </member>
        <member name="M:RDR2.World.DrawMarker(RDR2.MarkerType,RDR2.Math.Vector3,RDR2.Math.Vector3,RDR2.Math.Vector3,RDR2.Math.Vector3,System.Drawing.Color,System.Boolean,System.Boolean,System.Boolean,System.String,System.String,System.Boolean)">
            <summary>
            Draws a marker in the world, this needs to be done on a per frame basis
            </summary>
            <param name="type">The type of marker.</param>
            <param name="pos">The position of the marker.</param>
            <param name="dir">The direction the marker points in.</param>
            <param name="rot">The rotation of the marker.</param>
            <param name="scale">The amount to scale the marker by.</param>
            <param name="color">The color of the marker.</param>
            <param name="bobUpAndDown">if set to <c>true</c> the marker will bob up and down.</param>
            <param name="faceCamera">if set to <c>true</c> the marker will always face the camera, regardless of its rotation.</param>
            <param name="rotateY">if set to <c>true</c> rotates only on the y axis(heading).</param>
            <param name="textueDict">Name of texture dictionary to load the texture from, leave null for no texture in the marker.</param>
```
