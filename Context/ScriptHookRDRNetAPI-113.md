<!-- ScriptHookRDRNetAPI lines 28001-28250 of 30005 -->
```csharp
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Entity"/>'s position is frozen.
            </summary>
        </member>
        <member name="P:RDR2.Entity.IsOwnedByThisScript">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Entity"/> is owned by this script.
            </summary>
        </member>
        <member name="M:RDR2.Entity.RequestOwnership">
            <summary>
            Attempts to request script ownership of this <see cref="T:RDR2.Entity"/> if not already owned.
            </summary>
        </member>
        <member name="P:RDR2.Entity.IsStatic">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Entity"/> is static.
            </summary>
        </member>
        <member name="P:RDR2.Entity.IsDynamic">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Entity"/> is dynamic.
            </summary>
        </member>
        <member name="P:RDR2.Entity.IsInside">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Entity"/> is inside/in a interior.
            </summary>
        </member>
        <member name="P:RDR2.Entity.IsVisible">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Entity"/> is visible.
            </summary>
        </member>
        <member name="P:RDR2.Entity.CarriableState">
            <summary>
            Gets the <see cref="T:RDR2.eCarriableState"/> this <see cref="T:RDR2.Entity"/> is currently in.
            </summary>
        </member>
        <member name="P:RDR2.Entity.Model">
            <summary>
            Gets the model of the current <see cref="T:RDR2.Entity"/>.
            </summary>
        </member>
        <member name="P:RDR2.Entity.Alpha">
            <summary>
            Gets or sets how opaque this <see cref="T:RDR2.Entity"/> is.
            </summary>
            <value>
            0 for completely see through, 255 for fully opaque
            </value>
        </member>
        <member name="P:RDR2.Entity.Opacity">
            <summary>
            Gets or sets how opaque this <see cref="T:RDR2.Entity"/> is.
            </summary>
            <remarks>This is an alias for <see cref="P:RDR2.Entity.Alpha"/></remarks>
            <value>
            0 for completely see through, 255 for fully opaque
            </value>
        </member>
        <member name="P:RDR2.Entity.EntityType">
            <summary>
            Gets the type of the current <see cref="T:RDR2.Entity"/>.
            </summary>
        </member>
        <member name="P:RDR2.Entity.LodDistance">
            <summary>
            Gets or sets the level of detail distance of this <see cref="T:RDR2.Entity"/>.
            </summary>
        </member>
        <member name="P:RDR2.Entity.IsMissionEntity">
            <summary>
            Gets or sets a value indicating whether this <see cref="T:RDR2.Entity"/> is a mission entity.
            </summary>
        </member>
        <member name="P:RDR2.Entity.IsPersistent">
            <summary>
            Gets or sets a value indicating whether this <see cref="T:RDR2.Entity"/> is persistent.
            </summary>
        </member>
        <member name="P:RDR2.Entity.Health">
            <summary>
            Gets or sets the health of this <see cref="T:RDR2.Entity"/> as an <see cref="T:System.Int32"/>.
            </summary>
        </member>
        <member name="P:RDR2.Entity.MaxHealth">
            <summary>
            Gets or sets the maximum health of this <see cref="T:RDR2.Entity"/> as an <see cref="T:System.Int32"/>.
            </summary>
        </member>
        <member name="P:RDR2.Entity.Position">
            <summary>
            Gets or sets the position of this <see cref="T:RDR2.Entity"/>.
            If the <see cref="T:RDR2.Entity"/> is <see cref="T:RDR2.Ped"/> and the <see cref="T:RDR2.Ped"/> is in a <see cref="T:RDR2.Vehicle"/>, the <see cref="T:RDR2.Vehicle"/>'s position will be returned or changed.
            </summary>
        </member>
        <member name="P:RDR2.Entity.PositionNoOffset">
            <summary>
            Sets the position of this <see cref="T:RDR2.Entity"/> without any offset.
            </summary>
        </member>
        <member name="P:RDR2.Entity.Rotation">
            <summary>
            Gets or sets the rotation of this <see cref="T:RDR2.Entity"/>.
            </summary>
        </member>
        <member name="P:RDR2.Entity.Heading">
            <summary>
            Gets or sets the heading or "yaw" of this <see cref="T:RDR2.Entity"/>.
            </summary>
        </member>
        <member name="P:RDR2.Entity.SubmergedLevel">
            <summary>
            Gets a value indicating how submersed this <see cref="T:RDR2.Entity"/> is, 1.0f means the whole entity is submerged.
            </summary>
        </member>
        <member name="P:RDR2.Entity.HeightAboveGround">
            <summary>
            Gets how high above ground this <see cref="T:RDR2.Entity"/> is.
            </summary>
        </member>
        <member name="P:RDR2.Entity.Quaternion">
            <summary>
            Gets the quaternion of this <see cref="T:RDR2.Entity"/>.
            </summary>
        </member>
        <member name="P:RDR2.Entity.UpVector">
            <summary>
            Gets the vector that points above this <see cref="T:RDR2.Entity"/>.
            </summary>
        </member>
        <member name="P:RDR2.Entity.RightVector">
            <summary>
            Gets the vector that points to the right of this <see cref="T:RDR2.Entity"/>.
            </summary>
        </member>
        <member name="P:RDR2.Entity.ForwardVector">
            <summary>
            Gets the vector that points in front of this <see cref="T:RDR2.Entity"/>.
            </summary>
        </member>
        <member name="P:RDR2.Entity.Forward">
            <summary>
            Gets the vector that points in front of this <see cref="T:RDR2.Entity"/>.
            </summary>
            <remarks>This is an alias for <see cref="P:RDR2.Entity.ForwardVector"/></remarks>
        </member>
        <member name="M:RDR2.Entity.GetOffsetPosition(RDR2.Math.Vector3)">
            <summary>
            Gets the position in world coordinates of an offset relative this <see cref="T:RDR2.Entity"/>.
            </summary>
        </member>
        <member name="M:RDR2.Entity.GetPositionOffset(RDR2.Math.Vector3)">
            <summary>
            Gets the relative offset of this <see cref="T:RDR2.Entity"/> from a world coordinates position.
            </summary>
        </member>
        <member name="P:RDR2.Entity.Velocity">
            <summary>
            Gets or sets the velocity of this <see cref="T:RDR2.Entity"/>.
            </summary>
        </member>
        <member name="P:RDR2.Entity.Speed">
            <summary>
            Gets or sets this <see cref="T:RDR2.Entity"/>s speed.
            </summary>
            <value>
            The speed in m/s.
            </value>
        </member>
        <member name="P:RDR2.Entity.IsInvincible">
            <summary>
            Sets a value indicating whether this <see cref="T:RDR2.Entity"/> is invincible.
            </summary>
        </member>
        <member name="P:RDR2.Entity.IsOnlyDamagedByPlayer">
            <summary>
            Gets or sets a value indicating whether this <see cref="T:RDR2.Entity"/> can only be damaged by <see cref="T:RDR2.Player"/>s.
            </summary>
        </member>
        <member name="P:RDR2.Entity.CanBeDamaged">
            <summary>
            Gets or sets a value indicating whether this <see cref="T:RDR2.Entity"/> can be damaged.
            </summary>
        </member>
        <member name="P:RDR2.Entity.IsBulletProof">
            <summary>
            Gets or sets a value indicating whether this <see cref="T:RDR2.Entity"/> is bullet proof.
            </summary>
        </member>
        <member name="P:RDR2.Entity.IsFlameProof">
            <summary>
            Gets or sets a value indicating whether this <see cref="T:RDR2.Entity"/> is flame proof.
            </summary>
        </member>
        <member name="P:RDR2.Entity.IsExplosionProof">
            <summary>
            Gets or sets a value indicating whether this <see cref="T:RDR2.Entity"/> is explosion proof.
            </summary>
        </member>
        <member name="P:RDR2.Entity.IsCollisionProof">
            <summary>
            Gets or sets a value indicating whether this <see cref="T:RDR2.Entity"/> is collision proof.
            </summary>
        </member>
        <member name="P:RDR2.Entity.IsMeleeProof">
            <summary>
            Gets or sets a value indicating whether this <see cref="T:RDR2.Entity"/> is melee proof.
            </summary>
        </member>
        <member name="P:RDR2.Entity.IsSteamProof">
            <summary>
            Gets or sets a value indicating whether this <see cref="T:RDR2.Entity"/> is steam proof.
            </summary>
        </member>
        <member name="P:RDR2.Entity.IsSmokeProof">
            <summary>
            Gets or sets a value indicating whether this <see cref="T:RDR2.Entity"/> is smoke proof.
            </summary>
        </member>
        <member name="P:RDR2.Entity.IsHeadshotProof">
            <summary>
            Gets or sets a value indicating whether this <see cref="T:RDR2.Entity"/> is headshot proof.
            </summary>
        </member>
        <member name="P:RDR2.Entity.IsProjectileProof">
            <summary>
            Gets or sets a value indicating whether this <see cref="T:RDR2.Entity"/> is projectile proof.
            </summary>
        </member>
        <member name="M:RDR2.Entity.ClearProofs">
            <summary>
            Removes all proofs that are set on this <see cref="T:RDR2.Entity"/>
            </summary>
        </member>
        <member name="P:RDR2.Entity.ProofBits">
            <summary>
            Gets or sets the proofs currently set on this <see cref="T:RDR2.Entity"/> via bits
            </summary>
        </member>
        <member name="P:RDR2.Entity.HasCollision">
            <summary>
            Gets or sets a value indicating whether this <see cref="T:RDR2.Entity"/> has collision.
            </summary>
        </member>
        <member name="P:RDR2.Entity.IsCollisionEnabled">
            <summary>
            Gets or sets a value indicating whether this <see cref="T:RDR2.Entity"/> has collision.
            </summary>
```
