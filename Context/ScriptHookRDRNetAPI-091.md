<!-- ScriptHookRDRNetAPI lines 22501-22750 of 30005 -->
```csharp
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 5.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.BrokenToSideProb">
            <summary>
            Probability that the when a constraint is broken that during brokenSupportTime a side pose will be selected.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.ConnectAfter">
            <summary>
            If gunArm has been controlled by other behaviors for this time when it could have been pointing but couldn't due to pointing only allowed if connected, change gunArm pose to something that could connect for connectFor seconds.
            </summary>
            <remarks>
            Default value = 0.7f.
            Min value = 0.0f.
            Max value = 5.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.ConnectFor">
            <summary>
            Time to try to reconnect for.
            </summary>
            <remarks>
            Default value = 0.6f.
            Min value = 0.0f.
            Max value = 5.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.OneHandedPointing">
            <summary>
            0 = don't allow, 1= allow for kPistol(two handed pistol) only, 2 = allow for kRifle only, 3 = allow for kPistol and kRifle. Allow one handed pointing - no constraint if cant be supported .  If not allowed then gunHand does not try to point at target if it cannot be supported - the constraint will be controlled by always support.
            </summary>
            <remarks>
            Default value = 1.
            Min value = 0.
            Max value = 3.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.AlwaysSupport">
            <summary>
            Support a non pointing gunHand i.e. if in zero pose (constrain as well  if constraint possible).
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.PoseUnusedGunArm">
            <summary>
            Apply neutral pose when a gun arm isn't in use.  NB: at the moment Rifle hand is always controlled by pointGun.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.PoseUnusedSupportArm">
            <summary>
            Apply neutral pose when a support arm isn't in use.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.PoseUnusedOtherArm">
            <summary>
            Apply neutral pose to the non-gun arm (otherwise it is always under the control of other behaviors or not set). If the non-gun hand is a supporting hand it is not controlled by this parameter but by poseUnusedSupportArm.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.MaxAngleAcross">
            <summary>
            Max aiming angle(deg) sideways across body midline measured from chest forward that the character will try to point.
            </summary>
            <remarks>
            Default value = 90.0f.
            Min value = 0.0f.
            Max value = 180.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.MaxAngleAway">
            <summary>
            Max aiming angle(deg) sideways away from body midline measured from chest forward that the character will try to point.
            </summary>
            <remarks>
            Default value = 90.0f.
            Min value = 0.0f.
            Max value = 180.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.FallingLimits">
            <summary>
            0= don't apply limits.  1=apply the limits below only when the character is falling.  2 =  always apply these limits (instead of applying maxAngleAcross and maxAngleAway which only limits the horizontal angle but implicity limits the updown (the limit shape is a vertical hinge).
            </summary>
            <remarks>
            Default value = 0.
            Min value = 0.
            Max value = 2.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.AcrossLimit">
            <summary>
            Max aiming angle(deg) sideways across body midline measured from chest forward that the character will try to point.  i.e. for rightHanded gun this is the angle left of the midline.
            </summary>
            <remarks>
            Default value = 90.0f.
            Min value = 0.0f.
            Max value = 180.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.AwayLimit">
            <summary>
            Max aiming angle(deg) sideways away from body midline measured from chest forward that the character will try to point.  i.e. for rightHanded gun this is the angle right of the midline.
            </summary>
            <remarks>
            Default value = 90.0f.
            Min value = 0.0f.
            Max value = 180.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.UpLimit">
            <summary>
            Max aiming angle(deg) upwards from body midline measured from chest forward that the character will try to point.
            </summary>
            <remarks>
            Default value = 90.0f.
            Min value = 0.0f.
            Max value = 180.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.DownLimit">
            <summary>
            Max aiming angle(deg) downwards from body midline measured from chest forward that the character will try to point.
            </summary>
            <remarks>
            Default value = 45.0f.
            Min value = 0.0f.
            Max value = 180.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.RifleFall">
            <summary>
            Pose the rifle hand to reduce complications with collisions. 0 = false, 1 = always when falling, 2 = when falling except if falling backwards.
            </summary>
            <remarks>
            Default value = 0.
            Min value = 0.
            Max value = 2.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.FallingSupport">
            <summary>
            Allow supporting of a rifle(or two handed pistol) when falling. 0 = false, 1 = support if allowed, 2 = support until constraint not active (don't allow support to restart), 3 = support until constraint not effective (support hand to support distance must be less than 0.15 - don't allow support to restart).
            </summary>
            <remarks>
            Default value = 1.
            Min value = 0.
            Max value = 3.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.FallingTypeSupport">
            <summary>
            What is considered a fall by fallingSupport). Apply fallingSupport 0=never(will support if allowed), 1 = falling, 2 = falling except if falling backwards, 3 = falling and collided, 4 = falling and collided except if falling backwards, 5 = falling except if falling backwards until collided.
            </summary>
            <remarks>
            Default value = 0.
            Min value = 0.
            Max value = 5.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.PistolNeutralType">
            <summary>
            0 = byFace, 1=acrossFront, 2=bySide.  NB: bySide is not connectible so be careful if combined with kPistol and oneHandedPointing = 0 or 2.
            </summary>
            <remarks>
            Default value = 0.
            Min value = 0.
            Max value = 2.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.NeutralPoint4Pistols">
            <summary>
            NOT IMPLEMENTED YET KEEP=false - use pointing for neutral targets in pistol modes.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.NeutralPoint4Rifle">
            <summary>
            Use pointing for neutral targets in rifle mode.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.CheckNeutralPoint">
            <summary>
            Check the neutral pointing is pointable, if it isn't then choose a neutral pose instead.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.Point2Side">
            <summary>
            Side, up, back) side is left for left arm, right for right arm mmmmtodo.
            </summary>
            <remarks>
            Default value = Vector3(5.0f, -5.0f, -2.0f).
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.Add2WeaponDistSide">
            <summary>
            Add to weaponDistance for point2Side neutral pointing (to straighten the arm).
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = -1.0f.
            Max value = 1000.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.Point2Connect">
            <summary>
            Side, up, back) side is left for left arm, right for rght arm mmmmtodo.
            </summary>
            <remarks>
            Default value = Vector3(-1.0f, -0.9f, -0.2f).
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.Add2WeaponDistConnect">
            <summary>
            Add to weaponDistance for point2Connect neutral pointing (to straighten the arm).
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = -1.0f.
            Max value = 1000.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.PointGunHelper.UsePistolIK">
            <summary>
```
