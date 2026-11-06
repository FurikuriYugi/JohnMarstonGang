<!-- ScriptHookRDRNetAPI lines 18501-18750 of 30005 -->
```csharp
            </summary>
            <remarks>
            Default value = 0.
            Min value = 0.
            Max value = 1.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.TimeToForward">
            <summary>
            Time after a forwards impact before forwardMode is called (leave sometime for a rebound or brace - the min of 0.1 is to ensure fallOverWall can start although it probably needs only 1or2 frames for the probes to return).
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.1f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.ReboundForce">
            <summary>
            If forwards impact only: cheat force to try to get the character away from the wall.  3 is a good value.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.BraceWall">
            <summary>
            Brace against wall if forwards impact(at the moment only if bodyBalance is running/in charge of arms).
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.IgnoreColVolumeBelow">
            <summary>
            Collisions with non-fixed objects with volume below this will not set this behavior off.
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = -1.0f.
            Max value = 1000.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.FallOverWallDrape">
            <summary>
            Use fallOverWall as the main drape reaction.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.FallOverHighWalls">
            <summary>
            Trigger fall over wall if hit up to spine2 else only if hit up to spine1.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.Snap">
            <summary>
            Add a Snap to when you hit a wall to emphasize the hit.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.SnapMag">
            <summary>
            The magnitude of the snap reaction.
            </summary>
            <remarks>
            Default value = -0.6f.
            Min value = -10.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.SnapDirectionRandomness">
            <summary>
            The character snaps in a prescribed way (decided by bullet direction) - Higher the value the more random this direction is.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.SnapLeftArm">
            <summary>
            Snap the leftArm.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.SnapRightArm">
            <summary>
            Snap the rightArm.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.SnapLeftLeg">
            <summary>
            Snap the leftLeg.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.SnapRightLeg">
            <summary>
            Snap the rightLeg.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.SnapSpine">
            <summary>
            Snap the spine.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.SnapNeck">
            <summary>
            Snap the neck.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.SnapPhasedLegs">
            <summary>
            Legs are either in phase with each other or not.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.SnapHipType">
            <summary>
            Type of hip reaction 0=none, 1=side2side 2=steplike.
            </summary>
            <remarks>
            Default value = 0.
            Min value = 0.
            Max value = 2.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.UnSnapInterval">
            <summary>
            Interval before applying reverse snap.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 100.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.UnSnapRatio">
            <summary>
            The magnitude of the reverse snap.
            </summary>
            <remarks>
            Default value = 0.7f.
            Min value = 0.0f.
            Max value = 100.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.SnapUseTorques">
            <summary>
            Use torques to make the snap otherwise use a change in the parts angular velocity.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.ImpactWeaknessZeroDuration">
            <summary>
            Duration for which the character's upper body stays at minimum stiffness (not quite zero).
            </summary>
            <remarks>
            Default value = 0.2f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.ImpactWeaknessRampDuration">
            <summary>
            Duration of the ramp to bring the character's upper body stiffness back to normal levels.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.ImpactLoosenessAmount">
            <summary>
            How loose the character is on impact. Between 0 and 1.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.ObjectBehindVictim">
            <summary>
            Detected an object behind a shot victim in the direction of a bullet?.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.ObjectBehindVictimPos">
            <summary>
            The intersection pos of a detected object behind a shot victim in the direction of a bullet.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BalancerCollisionsReactionHelper.ObjectBehindVictimNormal">
            <summary>
            The normal of a detected object behind a shot victim in the direction of a bullet.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.BodyBalanceHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the BodyBalanceHelper for sending a BodyBalance <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the BodyBalance <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.ArmStiffness">
            <summary>
            NB. WAS m_bodyStiffness ClaviclesStiffness=9.0f.
            </summary>
            <remarks>
```
