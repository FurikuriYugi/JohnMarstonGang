<!-- ScriptHookRDRNetAPI lines 28251-28500 of 30005 -->
```csharp
            <remarks>This is an alias for <see cref="P:RDR2.Entity.HasCollision"/></remarks>
        </member>
        <member name="P:RDR2.Entity.HasCollidedWithAnything">
            <summary>
            Gets or sets a value indicating whether this <see cref="T:RDR2.Entity"/> has recently collided with anything.
            </summary>
        </member>
        <member name="M:RDR2.Entity.SetNoCollision(RDR2.Entity,System.Boolean)">
            <summary>
            Sets the collision between this <see cref="T:RDR2.Entity"/> and another <see cref="T:RDR2.Entity"/>
            </summary>
            <param name="entity">The <see cref="T:RDR2.Entity"/> to set collision with</param>
            <param name="toggle">if set to <see langword="true" /> the 2 <see cref="T:RDR2.Entity"/>s wont collide with each other.</param>
        </member>
        <member name="M:RDR2.Entity.IsInArea(RDR2.Math.Vector3,RDR2.Math.Vector3)">
            <summary>
            Determines whether this <see cref="T:RDR2.Entity"/> is in a specified area
            </summary>
        </member>
        <member name="M:RDR2.Entity.IsInArea(RDR2.Math.Vector3,RDR2.Math.Vector3,System.Single)">
            <summary>
            Determines whether this <see cref="T:RDR2.Entity"/> is in a specified angled area
            </summary>
        </member>
        <member name="M:RDR2.Entity.IsInAngledArea(RDR2.Math.Vector3,RDR2.Math.Vector3,System.Single)">
            <summary>
            Determines whether this <see cref="T:RDR2.Entity"/> is in a specified angled area
            </summary>
        </member>
        <member name="M:RDR2.Entity.IsInRangeOf(RDR2.Math.Vector3,System.Single)">
            <summary>
            Determines whether this <see cref="T:RDR2.Entity"/> is in range of a specified position
            </summary>
        </member>
        <member name="M:RDR2.Entity.IsNearEntity(RDR2.Entity,RDR2.Math.Vector3)">
            <summary>
            Determines whether this <see cref="T:RDR2.Entity"/> is near a specified <see cref="T:RDR2.Entity"/>.
            </summary>
        </member>
        <member name="M:RDR2.Entity.IsAtEntity(RDR2.Entity,RDR2.Math.Vector3)">
            <summary>
            Determines whether this <see cref="T:RDR2.Entity"/> is near a specified <see cref="T:RDR2.Entity"/>.
            </summary>
            <remarks>This is an alias for <see cref="M:RDR2.Entity.IsNearEntity(RDR2.Entity,RDR2.Math.Vector3)"/></remarks>
        </member>
        <member name="M:RDR2.Entity.IsTouching(RDR2.Model)">
            <summary>
            Determines whether this <see cref="T:RDR2.Entity"/> is touching an <see cref="T:RDR2.Entity"/> with the <see cref="P:RDR2.Entity.Model"/> <paramref name="model"/>.
            </summary>
        </member>
        <member name="M:RDR2.Entity.IsTouching(RDR2.Entity)">
            <summary>
            Determines whether this <see cref="T:RDR2.Entity"/> is touching the <see cref="T:RDR2.Entity"/> <paramref name="entity"/>.
            </summary>
        </member>
        <member name="M:RDR2.Entity.AddBlip(RDR2.BlipType)">
            <summary>
            Creates a <see cref="T:RDR2.Blip"/> on this <see cref="T:RDR2.Entity"/>.
            </summary>
        </member>
        <member name="P:RDR2.Entity.GetBlip">
            <summary>
            Gets the <see cref="T:RDR2.Blip"/> on this <see cref="T:RDR2.Entity"/>.
            </summary>
        </member>
        <member name="P:RDR2.Entity.IsAttached">
            <summary>
            Determines whether this <see cref="T:RDR2.Entity"/> is attached to any other <see cref="T:RDR2.Entity"/>.
            </summary>
        </member>
        <member name="P:RDR2.Entity.IsAttachedToAnyObject">
            <summary>
            Determines whether this <see cref="T:RDR2.Entity"/> is attached to a <see cref="T:RDR2.Prop"/>.
            </summary>
        </member>
        <member name="P:RDR2.Entity.IsAttachedToAnyPed">
            <summary>
            Determines whether this <see cref="T:RDR2.Entity"/> is attached to a <see cref="T:RDR2.Ped"/>.
            </summary>
        </member>
        <member name="P:RDR2.Entity.IsAttachedToAnyVehicle">
            <summary>
            Determines whether this <see cref="T:RDR2.Entity"/> is attached to a <see cref="T:RDR2.Vehicle"/>.
            </summary>
        </member>
        <member name="M:RDR2.Entity.Detach">
            <summary>
            Detaches this <see cref="T:RDR2.Entity"/> from any <see cref="T:RDR2.Entity"/> it may be attached to.
            </summary>
        </member>
        <member name="M:RDR2.Entity.AttachTo(RDR2.Entity,System.Int32)">
            <summary>
            Attach this <see cref="T:RDR2.Entity"/> to another <see cref="T:RDR2.Entity"/>
            </summary>
        </member>
        <member name="M:RDR2.Entity.AttachTo(RDR2.Entity,System.Int32,RDR2.Math.Vector3,RDR2.Math.Vector3)">
            <summary>
            Attach this <see cref="T:RDR2.Entity"/> to another <see cref="T:RDR2.Entity"/>
            </summary>
        </member>
        <member name="M:RDR2.Entity.IsAttachedTo(RDR2.Entity)">
            <summary>
            Determines whether this <see cref="T:RDR2.Entity"/> is attached to the specified <see cref="T:RDR2.Entity"/>.
            </summary>
            <param name="entity">The <see cref="T:RDR2.Entity"/> to check if this <see cref="T:RDR2.Entity"/> is attached to.</param>
            <returns>
              <see langword="true" /> if this <see cref="T:RDR2.Entity"/> is attached to <paramref name="entity"/>; otherwise, <see langword="false" />.
            </returns>
        </member>
        <member name="M:RDR2.Entity.GetEntityAttachedTo">
            <summary>
            Gets the <see cref="T:RDR2.Entity"/> this <see cref="T:RDR2.Entity"/> is attached to.
            </summary>
        </member>
        <member name="M:RDR2.Entity.Delete">
            <summary>
            Instantly delete this <see cref="T:RDR2.Entity"/> from the game world.
            </summary>
        </member>
        <member name="M:RDR2.Entity.Exists">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Entity"/> exists in the game world.
            </summary>
        </member>
        <member name="M:RDR2.Entity.Exists(RDR2.Entity)">
            <summary>
            Gets a value indicating whether an <see cref="T:RDR2.Entity"/> is not <see langword="null"/>, and exists in the game world.
            </summary>
            <returns><see langword="true"/> if <see cref="T:RDR2.Entity"/> is not <see langword="null"/> and exists in the game world; otherwise, <see langword="false"/>.</returns>
        </member>
        <member name="P:RDR2.Model.Hash">
            <summary>
            Gets the hash for this <see cref="T:RDR2.Model"/>.
            </summary>
        </member>
        <member name="P:RDR2.Model.IsValid">
            <summary>
            Gets if this <see cref="T:RDR2.Model"/> is valid.
            </summary>
        </member>
        <member name="P:RDR2.Model.IsInCdImage">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Model"/> is in the CD image.
            </summary>
        </member>
        <member name="P:RDR2.Model.IsLoaded">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Model"/> is loaded so it can be spawned.
            </summary>
        </member>
        <member name="P:RDR2.Model.IsCollisionLoaded">
            <summary>
            Gets a value indicating whether the collision for this <see cref="T:RDR2.Model"/> is loaded.
            </summary>
        </member>
        <member name="P:RDR2.Model.IsPed">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Model"/> is a ped.
            </summary>
        </member>
        <member name="P:RDR2.Model.IsObject">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Model"/> is an object.
            </summary>
        </member>
        <member name="P:RDR2.Model.IsBoat">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Model"/> is a boat.
            </summary>
        </member>
        <member name="P:RDR2.Model.IsTrain">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Model"/> is a train.
            </summary>
        </member>
        <member name="P:RDR2.Model.IsVehicle">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Model"/> is vehicle.
            </summary>
        </member>
        <member name="P:RDR2.Model.IsDraftVehicle">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Model"/> is a draft vehicle.
            </summary>
        </member>
        <member name="P:RDR2.Model.IsHorse">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Model"/> is horse.
            </summary>
        </member>
        <member name="M:RDR2.Model.Request">
            <summary>
            Request this <see cref="T:RDR2.Model"/> to be loaded into memory. Remember to mark this <see cref="T:RDR2.Model"/> as no longer needed when you are done with this <see cref="T:RDR2.Model"/>.
            </summary>
        </member>
        <member name="M:RDR2.Model.Request(System.Int32)">
            <summary>
            Request this <see cref="T:RDR2.Model"/> to be loaded into memory. Remember to mark this <see cref="T:RDR2.Model"/> as no longer needed when you are done with this <see cref="T:RDR2.Model"/>.
            </summary>
        </member>
        <member name="P:RDR2.Model.Dimensions">
            <summary>
            Gets the dimensions of this <see cref="T:RDR2.Model"/>.
            </summary>
            <returns>
            rearBottomLeft is the minimum dimensions, which contains the rear bottom left relative offset from the origin of the model,
             frontTopRight is the maximum dimensions, which contains the front top right relative offset from the origin of the model.
            </returns>
        </member>
        <member name="P:RDR2.Ped.IsPlayer">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is a player.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsDeadOrDying">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is dead or dying.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsAimingFromCover">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is aiming while in cover.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsGoingIntoCover">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is going into cover.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsCarryingSomething">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is carrying something.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsReadToRender">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is ready to render.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsUsingActionMode">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is using an action mode.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsUsingScenario">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is using a scenario.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsInScenario">
```
