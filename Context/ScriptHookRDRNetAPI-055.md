<!-- ScriptHookRDRNetAPI lines 13501-13750 of 30005 -->
```csharp
        </member>
        <member name="M:RDR2.NaturalMotion.ApplyBulletImpulseHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the ApplyBulletImpulseHelper for sending a ApplyBulletImpulse <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the ApplyBulletImpulse <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.ApplyBulletImpulseHelper.EqualizeAmount">
            <summary>
            0 means straight impulse, 1 means multiply by the mass (change in velocity).
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ApplyBulletImpulseHelper.PartIndex">
            <summary>
            Index of part being hit.
            </summary>
            <remarks>
            Default value = 0.
            Min value = 0.
            Max value = 28.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ApplyBulletImpulseHelper.Impulse">
            <summary>
            Impulse vector (impulse is change in momentum).
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            Min value = -1000.0f.
            Max value = 1000.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ApplyBulletImpulseHelper.HitPoint">
            <summary>
            Optional point on part where hit.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ApplyBulletImpulseHelper.LocalHitPointInfo">
            <summary>
            True = hitPoint is in local coordinates of bodyPart, false = hit point is in world coordinates.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ApplyBulletImpulseHelper.ExtraShare">
            <summary>
            If not 0.0 then have an extra bullet applied to spine0 (approximates the COM).  Uses setup from configureBulletsExtra.  0-1 shared 0.0 = no extra bullet, 0.5 = impulse split equally between extra and bullet,  1.0 only extra bullet.  LT 0.0 then bullet + scaled extra bullet.  Eg.-0.5 = bullet + 0.5 impulse extra bullet.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = -2.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="T:RDR2.NaturalMotion.BodyRelaxHelper">
            <summary>
            Set the amount of relaxation across the whole body; Used to collapse the character into a rag-doll-like state.
            </summary>
        </member>
        <member name="M:RDR2.NaturalMotion.BodyRelaxHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the BodyRelaxHelper for sending a BodyRelax <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the BodyRelax <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
            <remarks>
            Set the amount of relaxation across the whole body; Used to collapse the character into a rag-doll-like state.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyRelaxHelper.Relaxation">
            <summary>
            How relaxed the body becomes, in percentage relaxed. 100 being totally rag-dolled, 0 being very stiff and rigid.
            </summary>
            <remarks>
            Default value = 50.0f.
            Min value = 0.0f.
            Max value = 100.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyRelaxHelper.Damping">
            <summary>
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyRelaxHelper.Mask">
            <summary>
            Two character body-masking value, bitwise joint mask or bitwise logic string of two character body-masking value  (see Active Pose notes for possible values).
            </summary>
            <remarks>
            Default value = "fb.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyRelaxHelper.HoldPose">
            <summary>
            Automatically hold the current pose as the character relaxes - can be used to avoid relaxing into a t-pose.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyRelaxHelper.DisableJointDriving">
            <summary>
            Sets the drive state to free - this reduces drifting on the ground.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="T:RDR2.NaturalMotion.ConfigureBalanceHelper">
            <summary>
            This single message allows you to configure various parameters used on any behavior that uses the dynamic balance.
            </summary>
        </member>
        <member name="M:RDR2.NaturalMotion.ConfigureBalanceHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the ConfigureBalanceHelper for sending a ConfigureBalance <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the ConfigureBalance <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
            <remarks>
            This single message allows you to configure various parameters used on any behavior that uses the dynamic balance.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.StepHeight">
            <summary>
            Maximum height that character steps vertically (above 0.2 is high ... But OK underwater).
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.0f.
            Max value = 0.4f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.StepHeightInc4Step">
            <summary>
            Added to stepHeight if going up steps.
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.0f.
            Max value = 0.4f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.LegsApartRestep">
            <summary>
            If the legs end up more than (legsApartRestep + hipwidth) apart even though balanced, take another step.
            </summary>
            <remarks>
            Default value = 0.2f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.LegsTogetherRestep">
            <summary>
            Mmmm0.1 for drunk if the legs end up less than (hipwidth - legsTogetherRestep) apart even though balanced, take another step.  A value of 1 will turn off this feature and the max value is hipWidth = 0.23f by default but is model dependent.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.LegsApartMax">
            <summary>
            FRICTION WORKAROUND: if the legs end up more than (legsApartMax + hipwidth) apart when balanced, adjust the feet positions to slide back so they are legsApartMax + hipwidth apart.  Needs to be less than legsApartRestep to see any effect.
            </summary>
            <remarks>
            Default value = 2.0f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.TaperKneeStrength">
            <summary>
            Does the knee strength reduce with angle.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.LegStiffness">
            <summary>
            Stiffness of legs.
            </summary>
            <remarks>
            Default value = 12.0f.
            Min value = 6.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.LeftLegSwingDamping">
            <summary>
            Damping of left leg during swing phase (mmmmDrunk used 1.25 to slow legs movement).
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.2f.
            Max value = 4.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.RightLegSwingDamping">
            <summary>
            Damping of right leg during swing phase (mmmmDrunk used 1.25 to slow legs movement).
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.2f.
            Max value = 4.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.OpposeGravityLegs">
            <summary>
            Gravity opposition applied to hips and knees.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 4.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.OpposeGravityAnkles">
            <summary>
            Gravity opposition applied to ankles.  General balancer likes 1.0.  StaggerFall likes 0.1.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 4.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.ConfigureBalanceHelper.LeanAcc">
            <summary>
            Multiplier on the floorAcceleration added to the lean.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
```
