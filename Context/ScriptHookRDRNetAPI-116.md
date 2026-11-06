<!-- ScriptHookRDRNetAPI lines 28751-29000 of 30005 -->
```csharp
        </member>
        <member name="P:RDR2.Ped.CanRagdoll">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is ragdolling.
            </summary>
        </member>
        <member name="M:RDR2.Ped.StopRagdoll">
            <summary>
            Set this <see cref="T:RDR2.Ped"/> to stop ragdolling.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsLassoed">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is being lassoed.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsShooting">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is shooting.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsReloading">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is reloading.
            </summary>
        </member>
        <member name="P:RDR2.Ped.Accuracy">
            <summary>
            Gets or sets how accurate this <see cref="T:RDR2.Ped"/>s shooting ability is.
             The higher the value of this property is, the more likely it is that this <see cref="T:RDR2.Ped"/> will shoot at exactly where they are aiming at.
            </summary>
            <value>
            The accuracy from 0 to 100, 0 being very inaccurate, which means this <see cref="T:RDR2.Ped"/> cannot shoot at exactly where they are aiming at,
             100 being perfectly accurate.
            </value>
        </member>
        <member name="P:RDR2.Ped.ShootRate">
            <summary>
            Sets the rate this <see cref="T:RDR2.Ped"/> will shoot at.
            </summary>
            <value>
            The shoot rate from 0 to 1000. 100 is the default value.
            </value>
        </member>
        <member name="P:RDR2.Ped.IsInBoat">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is in a boat.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsInTrain">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is in a train.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsInVehicle">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is in a vehicle.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsJacking">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is jacking another <see cref="T:RDR2.Ped"/>.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsOnFoot">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is on foot.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsOnHorse">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is on a horse.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsOnMount">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is on a mount.
            </summary>
            <remarks>This is an alias for <see cref="P:RDR2.Ped.IsOnHorse"/></remarks>
        </member>
        <member name="P:RDR2.Ped.IsSittingInVehicle">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is sitting in a vehicle.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsEnteringVehicle">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is getting into a vehicle.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsGettingIntoAVehicle">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is getting into a vehicle.
            </summary>
            <remarks>This is an alias for <see cref="P:RDR2.Ped.IsEnteringVehicle"/></remarks>
        </member>
        <member name="P:RDR2.Ped.IsEnteringTransport">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is entering transport.
            </summary>
        </member>
        <member name="P:RDR2.Ped.CurrentMount">
            <summary>
            Gets the mount that this <see cref="T:RDR2.Ped"/> is currently on.
            </summary>
        </member>
        <member name="M:RDR2.Ped.IsInThisVehicle(RDR2.Vehicle)">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is in a specific vehicle.
            </summary>
        </member>
        <member name="M:RDR2.Ped.IsSittingInThisVehicle(RDR2.Vehicle)">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is sitting in a specific vehicle.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsBeingHogtied">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is being hogtied.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsBeingJacked">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is being jacked.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsBeingStealthKilled">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is being stealth killed.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsBeingStunned">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is being stunned.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsFleeing">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is fleeing.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsHogtied">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is hogtied.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsHogtying">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is hogtying.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsInCombat">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is in combat.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsInMeleeCombat">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is melee combat.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsRespondingToThreat">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is responding to a threat.
            </summary>
        </member>
        <member name="P:RDR2.Ped.CanWrithe">
            <summary>
            Gets or sets a value indicating whether this <see cref="T:RDR2.Ped"/> can writhe.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsIncapacitated">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is incapacitated.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsInjured">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is injured.
            </summary>
        </member>
        <member name="P:RDR2.Ped.IsFatallyInjured">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Ped"/> is fatally injured.
            </summary>
        </member>
        <member name="P:RDR2.Ped.DropsWeaponsOnDeath">
            <summary>
            Set this <see cref="T:RDR2.Ped"/> is drop his weapons on death.
            </summary>
        </member>
        <member name="P:RDR2.Ped.Voice">
            <summary>
            Set the voice this <see cref="T:RDR2.Ped"/> has
            </summary>
        </member>
        <member name="M:RDR2.Ped.PlaySpeech(System.String,System.String,RDR2.eSpeechParams,System.Int32)">
            <summary>
            Play a speech on this <see cref="T:RDR2.Ped"/>
            </summary>
        </member>
        <member name="P:RDR2.RelationshipGroup.Hash">
            <summary>
            Gets the hash for this <see cref="T:RDR2.RelationshipGroup"/>.
            </summary>
        </member>
        <member name="P:RDR2.RelationshipGroup.NativeValue">
            <summary>
            Gets the native representation of this <see cref="T:RDR2.RelationshipGroup"/>.
            </summary>
        </member>
        <member name="P:RDR2.Vehicle.IsDraftVehicle">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Vehicle"/> is xxx.
            </summary>
        </member>
        <member name="P:RDR2.Vehicle.IsOnAllWheels">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Vehicle"/> is on all of its wheels.
            </summary>
        </member>
        <member name="P:RDR2.Vehicle.IsStopped">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Vehicle"/> is stopped.
            </summary>
        </member>
        <member name="P:RDR2.Vehicle.IsWrecked">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Vehicle"/> is wrecked.
            </summary>
        </member>
        <member name="P:RDR2.Vehicle.IsOnFire">
            <summary>
            Gets a value indicating whether this <see cref="T:RDR2.Vehicle"/> is on fire.
            </summary>
        </member>
        <member name="P:RDR2.Vehicle.IsStolen">
            <summary>
            Sets a value indicating whether this <see cref="T:RDR2.Vehicle"/> is stolen.
            </summary>
        </member>
        <member name="P:RDR2.Vehicle.ProvidesCover">
            <summary>
            Sets a value indicating whether this <see cref="T:RDR2.Vehicle"/> can provide cover.
            </summary>
        </member>
        <member name="P:RDR2.Vehicle.BodyHealth">
            <summary>
            Gets or sets this <see cref="T:RDR2.Vehicle"/>s body health.
            </summary>
```
