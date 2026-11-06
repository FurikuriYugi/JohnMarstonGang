<!-- ScriptHookRDRNetAPI lines 13251-13500 of 30005 -->
```csharp
            </summary>
            <remarks>
            configure the shot from behind reaction
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.ShotInGuts">
            <summary>
            Gets a ShotInGuts Helper class for sending ShotInGuts <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
            <remarks>
            configure the shot in guts reaction
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.ShotHeadLook">
            <summary>
            Gets a ShotHeadLook Helper class for sending ShotHeadLook <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.ShotConfigureArms">
            <summary>
            Gets a ShotConfigureArms Helper class for sending ShotConfigureArms <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
            <remarks>
            configure the arm reactions in shot
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.SmartFall">
            <summary>
            Gets a SmartFall Helper class for sending SmartFall <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
            <remarks>
            Clone of High Fall with a wider range of operating conditions.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.StaggerFall">
            <summary>
            Gets a StaggerFall Helper class for sending StaggerFall <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.Teeter">
            <summary>
            Gets a Teeter Helper class for sending Teeter <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.UpperBodyFlinch">
            <summary>
            Gets a UpperBodyFlinch Helper class for sending UpperBodyFlinch <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
        </member>
        <member name="P:RDR2.NaturalMotion.Euphoria.Yanked">
            <summary>
            Gets a Yanked Helper class for sending Yanked <see cref="T:RDR2.NaturalMotion.Message"/> to this <see cref="T:RDR2.Ped"/>.
            </summary>
        </member>
        <member name="T:RDR2.NaturalMotion.Message">
            <summary>
            A base class for manually building a <see cref="T:RDR2.NaturalMotion.Message"/>.
            </summary>
        </member>
        <member name="M:RDR2.NaturalMotion.Message.#ctor(System.String)">
            <summary>
            Creates a class to manually build <see cref="T:RDR2.NaturalMotion.Message"/>s that can be sent to any <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="message">The name of the natural motion message.</param>
        </member>
        <member name="M:RDR2.NaturalMotion.Message.Abort(RDR2.Ped)">
            <summary>
            DO NOT USE DO NOT USE/>.
            </summary>
        </member>
        <member name="M:RDR2.NaturalMotion.Message.SendTo(RDR2.Ped)">
            <summary>
            Starts this Natural Motion behavior on the <see cref="T:RDR2.Ped"/> that will loop until manually aborted.
            </summary>
            <param name="target">The <see cref="T:RDR2.Ped"/> to send the <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="M:RDR2.NaturalMotion.Message.SendTo(RDR2.Ped,System.Int32)">
            <summary>
            Starts this Natural Motion behavior on the <see cref="T:RDR2.Ped"/> for a specified duration.
            </summary>
            <param name="target">The <see cref="T:RDR2.Ped"/> to send the <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
            <param name="duration">How long to apply the behavior for (-1 for looped).</param>
        </member>
        <member name="M:RDR2.NaturalMotion.Message.SetArgument(System.String,System.Boolean)">
            <summary>
            Sets a <see cref="T:RDR2.NaturalMotion.Message"/> argument to a <see cref="T:System.Boolean"/> value.
            </summary>
            <param name="message">The argument name.</param>
            <param name="value">The value to set the argument to.</param>
        </member>
        <member name="M:RDR2.NaturalMotion.Message.SetArgument(System.String,System.Int32)">
            <summary>
            Sets a <see cref="T:RDR2.NaturalMotion.Message"/> argument to a <see cref="T:System.Int32"/> value.
            </summary>
            <param name="message">The argument name.</param>
            <param name="value">The value to set the argument to.</param>
        </member>
        <member name="M:RDR2.NaturalMotion.Message.SetArgument(System.String,System.Single)">
            <summary>
            Sets a <see cref="T:RDR2.NaturalMotion.Message"/> argument to a <see cref="T:System.Single"/> value.
            </summary>
            <param name="message">The argument name.</param>
            <param name="value">The value to set the argument to.</param>
        </member>
        <member name="M:RDR2.NaturalMotion.Message.SetArgument(System.String,System.String)">
            <summary>
            Sets a <see cref="T:RDR2.NaturalMotion.Message"/> argument to a <see cref="T:System.String"/> value.
            </summary>
            <param name="message">The argument name.</param>
            <param name="value">The value to set the argument to.</param>
        </member>
        <member name="M:RDR2.NaturalMotion.Message.SetArgument(System.String,RDR2.Math.Vector3)">
            <summary>
            Sets a <see cref="T:RDR2.NaturalMotion.Message"/> argument to a <see cref="T:RDR2.Math.Vector3"/> value.
            </summary>
            <param name="message">The argument name.</param>
            <param name="value">The value to set the argument to.</param>
        </member>
        <member name="M:RDR2.NaturalMotion.Message.ResetArguments">
            <summary>
            Resets all arguments to their default values.
            </summary>
        </member>
        <member name="M:RDR2.NaturalMotion.Message.ToString">
            <summary>
            Returns the internal message name.
            </summary>
        </member>
        <member name="T:RDR2.NaturalMotion.CustomHelper">
            <summary>
            A helper class for building a <seealso cref="T:RDR2.NaturalMotion.Message" /> and sending it to a given <see cref="T:RDR2.Ped"/>.
            </summary>
        </member>
        <member name="M:RDR2.NaturalMotion.CustomHelper.#ctor(RDR2.Ped,System.String)">
            <summary>
            Creates a helper class for building Natural Motion messages to send to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="target">The <see cref="T:RDR2.Ped"/> that the message will be applied to.</param>
            <param name="message">The name of the natural motion message.</param>
        </member>
        <member name="M:RDR2.NaturalMotion.CustomHelper.Start">
            <summary>
            Starts this Natural Motion behavior on the <see cref="T:RDR2.Ped"/> that will loop until manually aborted.
            </summary>
        </member>
        <member name="M:RDR2.NaturalMotion.CustomHelper.Start(System.Int32)">
            <summary>
            Starts this Natural Motion behavior on the <see cref="T:RDR2.Ped"/> for a specified duration.
            </summary>
            <param name="duration">How long to apply the behavior for (-1 for looped).</param>
        </member>
        <member name="M:RDR2.NaturalMotion.CustomHelper.Stop">
            <summary>
            Stops this Natural Motion behavior on the <see cref="T:RDR2.Ped"/>.
            </summary>
        </member>
        <member name="M:RDR2.NaturalMotion.ActivePoseHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the ActivePoseHelper for sending a ActivePose <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the ActivePose <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.ActivePoseHelper.Mask">
            <summary>
            Two character body-masking value, bitwise joint mask or bitwise logic string of two character body-masking value  (see notes for explanation).
            </summary>
            <remarks>
            Default value = "fb".
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ActivePoseHelper.UseGravityCompensation">
            <summary>
            Apply gravity compensation.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ActivePoseHelper.AnimSource">
            <summary>
            Animation source.
            </summary>
        </member>
        <member name="M:RDR2.NaturalMotion.ApplyImpulseHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the ApplyImpulseHelper for sending a ApplyImpulse <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the ApplyImpulse <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.ApplyImpulseHelper.EqualizeAmount">
            <summary>
            0 means straight impulse, 1 means multiply by the mass (change in velocity).
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ApplyImpulseHelper.PartIndex">
            <summary>
            Index of part being hit. -1 apply impulse to COM.
            </summary>
            <remarks>
            Default value = 0.
            Min value = -1.
            Max value = 28.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ApplyImpulseHelper.Impulse">
            <summary>
            Impulse vector (impulse is change in momentum).
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            Min value = -4500.0f.
            Max value = 4500.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ApplyImpulseHelper.HitPoint">
            <summary>
            Optional point on part where hit. If not supplied then the impulse is applied at the part center.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ApplyImpulseHelper.LocalHitPointInfo">
            <summary>
            Hit point in local coordinates of body part.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ApplyImpulseHelper.LocalImpulseInfo">
            <summary>
            Impulse in local coordinates of body part.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ApplyImpulseHelper.AngularImpulse">
            <summary>
            Impulse should be considered an angular impulse.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
```
