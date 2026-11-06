<!-- ScriptHookRDRNetAPI lines 12751-13000 of 30005 -->
```csharp
            - RIVER
            - OIL_SPILL
            - SWAMP
            - OCEAN
            - CREEK
            - POND
            - GLACIER
            </summary>
        </member>
        <member name="M:RDR2.Native.COMPAPP._0xB6FD96420C0126A1(System.UInt32,System.Boolean)">
            <summary>
            nullsub, doesn't do anything
            </summary>
        </member>
        <member name="M:RDR2.Native.COMPAPP._0x74BCCEB233AD95B2(System.UInt32,System.Int32)">
            <summary>
            nullsub, doesn't do anything
            </summary>
        </member>
        <member name="M:RDR2.Native.COMPAPP._0x29C733459A9011EB(System.UInt32,System.String)">
            <summary>
            nullsub, doesn't do anything
            </summary>
        </member>
        <member name="M:RDR2.Native.COMPAPP._0x7AF1BB4504EA5ED9">
            <summary>
            Hardcoded to return false.
            </summary>
        </member>
        <member name="M:RDR2.Native.COMPAPP._0xCCB4635A071FB62D">
            <summary>
            nullsub, doesn't do anything
            </summary>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.ActivePose">
            <summary>
            Gets a ActivePose Helper class for sending ActivePose <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.ApplyImpulse">
            <summary>
            Gets a ApplyImpulse Helper class for sending ApplyImpulse <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.ApplyBulletImpulse">
            <summary>
            Gets a ApplyBulletImpulse Helper class for sending ApplyBulletImpulse <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.BodyRelax">
            <summary>
            Gets a BodyRelax Helper class for sending BodyRelax <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
            <remarks>
            Set the amount of relaxation across the whole body; Used to collapse the character into a rag-doll-like state.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.ConfigureBalance">
            <summary>
            Gets a ConfigureBalance Helper class for sending ConfigureBalance <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
            <remarks>
            This single message allows you to configure various parameters used on any behavior that uses the dynamic balance.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.ConfigureBalanceReset">
            <summary>
            Gets a ConfigureBalanceReset Helper class for sending ConfigureBalanceReset <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
            <remarks>
            reset the values configurable by the Configure Balance message to their defaults.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.ConfigureSelfAvoidance">
            <summary>
            Gets a ConfigureSelfAvoidance Helper class for sending ConfigureSelfAvoidance <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
            <remarks>
            this single message allows to configure self avoidance for the character.BBDD Self avoidance tech.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.ConfigureBullets">
            <summary>
            Gets a ConfigureBullets Helper class for sending ConfigureBullets <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.ConfigureBulletsExtra">
            <summary>
            Gets a ConfigureBulletsExtra Helper class for sending ConfigureBulletsExtra <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.ConfigureLimits">
            <summary>
            Gets a ConfigureLimits Helper class for sending ConfigureLimits <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
            <remarks>
            Enable/disable/edit character limits in real time.  This adjusts limits in RAGE-native space and will *not* reorient the joint.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.ConfigureSoftLimit">
            <summary>
            Gets a ConfigureSoftLimit Helper class for sending ConfigureSoftLimit <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.ConfigureShotInjuredArm">
            <summary>
            Gets a ConfigureShotInjuredArm Helper class for sending ConfigureShotInjuredArm <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
            <remarks>
            This single message allows you to configure the injured arm reaction during shot
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.ConfigureShotInjuredLeg">
            <summary>
            Gets a ConfigureShotInjuredLeg Helper class for sending ConfigureShotInjuredLeg <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
            <remarks>
            This single message allows you to configure the injured leg reaction during shot
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.DefineAttachedObject">
            <summary>
            Gets a DefineAttachedObject Helper class for sending DefineAttachedObject <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.ForceToBodyPart">
            <summary>
            Gets a ForceToBodyPart Helper class for sending ForceToBodyPart <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
            <remarks>
            Apply an impulse to a named body part
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.LeanInDirection">
            <summary>
            Gets a LeanInDirection Helper class for sending LeanInDirection <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.LeanRandom">
            <summary>
            Gets a LeanRandom Helper class for sending LeanRandom <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.LeanToPosition">
            <summary>
            Gets a LeanToPosition Helper class for sending LeanToPosition <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.LeanTowardsObject">
            <summary>
            Gets a LeanTowardsObject Helper class for sending LeanTowardsObject <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.HipsLeanInDirection">
            <summary>
            Gets a HipsLeanInDirection Helper class for sending HipsLeanInDirection <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.HipsLeanRandom">
            <summary>
            Gets a HipsLeanRandom Helper class for sending HipsLeanRandom <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.HipsLeanToPosition">
            <summary>
            Gets a HipsLeanToPosition Helper class for sending HipsLeanToPosition <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.HipsLeanTowardsObject">
            <summary>
            Gets a HipsLeanTowardsObject Helper class for sending HipsLeanTowardsObject <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.ForceLeanInDirection">
            <summary>
            Gets a ForceLeanInDirection Helper class for sending ForceLeanInDirection <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.ForceLeanRandom">
            <summary>
            Gets a ForceLeanRandom Helper class for sending ForceLeanRandom <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.ForceLeanToPosition">
            <summary>
            Gets a ForceLeanToPosition Helper class for sending ForceLeanToPosition <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.ForceLeanTowardsObject">
            <summary>
            Gets a ForceLeanTowardsObject Helper class for sending ForceLeanTowardsObject <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.SetStiffness">
            <summary>
            Gets a SetStiffness Helper class for sending SetStiffness <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
            <remarks>
            Use this message to manually set the body stiffness values -before using Active Pose to drive to an animated pose, for example.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.SetMuscleStiffness">
            <summary>
            Gets a SetMuscleStiffness Helper class for sending SetMuscleStiffness <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
            <remarks>
            Use this message to manually set the muscle stiffness values -before using Active Pose to drive to an animated pose, for example.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.SetWeaponMode">
            <summary>
            Gets a SetWeaponMode Helper class for sending SetWeaponMode <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
            <remarks>
            Use this message to set the character's weapon mode. This is an alternative to the setWeaponMode public function.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.RegisterWeapon">
            <summary>
            Gets a RegisterWeapon Helper class for sending RegisterWeapon <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
            <remarks>
            Use this message to register weapon. This is an alternative to the registerWeapon public function.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.ShotRelax">
            <summary>
            Gets a ShotRelax Helper class for sending ShotRelax <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.FireWeapon">
            <summary>
            Gets a FireWeapon Helper class for sending FireWeapon <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
            <remarks>
            One shot message apply a force to the hand as we fire the gun that should be in this hand
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.ConfigureConstraints">
            <summary>
            Gets a ConfigureConstraints Helper class for sending ConfigureConstraints <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
            <remarks>
            One shot to give state of constraints on character and response to constraints
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.StayUpright">
            <summary>
            Gets a StayUpright Helper class for sending StayUpright <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
```
