<!-- ScriptHookRDRNetAPI lines 19751-20000 of 30005 -->
```csharp
            </summary>
            <remarks>
            Default value = 12.0f.
            Min value = 6.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.GrabDontLetGo">
            <summary>
            Once a constraint is made, keep reaching with whatever hand is allowed.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.GrabStrength">
            <summary>
            Strength in hands for grabbing (kg m/s), -1 to ignore/disable.
            </summary>
            <remarks>
            Default value = 40.0f.
            Min value = -1.0f.
            Max value = 1000.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.GrabDistance">
            <summary>
            Relative distance at which the grab starts.
            </summary>
            <remarks>
            Default value = 2.0f.
            Min value = 0.0f.
            Max value = 4.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.GrabReachAngle">
            <summary>
            Angle from front at which the grab activates. If the point is outside this angle from front will not try to grab.
            </summary>
            <remarks>
            Default value = 1.5f.
            Min value = 0.0f.
            Max value = 3.2f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.GrabHoldTimer">
            <summary>
            Amount of time, in seconds, before grab automatically bails.
            </summary>
            <remarks>
            Default value = 2.5f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.MaxGrabCarVelocity">
            <summary>
            Don't try to grab a car moving above this speed mmmmtodo make this the relative velocity of car to character?.
            </summary>
            <remarks>
            Default value = 95.0f.
            Min value = 0.0f.
            Max value = 1000.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.LegStiffness">
            <summary>
            Balancer leg stiffness mmmmtodo remove this parameter and use configureBalance?.
            </summary>
            <remarks>
            Default value = 12.0f.
            Min value = 6.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.TimeToBackwardsBrace">
            <summary>
            Time before arm brace kicks in when hit from behind.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.Look">
            <summary>
            Position to look at, e.g. the driver.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.Pos">
            <summary>
            Location of the front part of the object to brace against. This should be the center of where his hands should meet the object.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.MinBraceTime">
            <summary>
            Minimum bracing time so the character doesn't look twitchy.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 3.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.HandsDelayMin">
            <summary>
            If bracing with 2 hands delay one hand by at least this amount of time to introduce some asymmetry.
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.0f.
            Max value = 3.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.HandsDelayMax">
            <summary>
            If bracing with 2 hands delay one hand by at most this amount of time to introduce some asymmetry.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 3.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.MoveAway">
            <summary>
            Move away from the car (if in reaching zone).
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.MoveAwayAmount">
            <summary>
            ForceLean away amount (-ve is lean towards).
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.MoveAwayLean">
            <summary>
            Lean away amount (-ve is lean towards).
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = -0.5f.
            Max value = 0.5f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.MoveSideways">
            <summary>
            Amount of sideways movement if at the front or back of the car to add to the move away from car.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.BbArms">
            <summary>
            Use bodyBalance arms for the default (non bracing) behavior if bodyBalance is active.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.NewBrace">
            <summary>
            Use the new brace prediction code.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.BraceOnImpact">
            <summary>
            If true then if a shin or thigh is in contact with the car then brace. NB: newBrace must be true.  For those situations where the car has pushed the ped backwards (at the same speed as the car) before the behavior has been started and so doesn't predict an impact.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.Roll2Velocity">
            <summary>
            When rollDownStairs is running use roll2Velocity to control the helper torques (this only attempts to roll to the chaarcter's velocity not some default linear velocity mag.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.RollType">
            <summary>
            0 = original/roll off/stay on car:  Roll with character velocity, 1 = //Gentle: roll off/stay on car = use relative velocity of character to car to roll against, 2 = //roll over car:  Roll against character velocity.  i.e. roll against any velocity picked up by hitting car, 3 = //Gentle: roll over car:  use relative velocity of character to car to roll with.
            </summary>
            <remarks>
            Default value = 3.
            Min value = 0.
            Max value = 3.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.SnapImpacts">
            <summary>
            Exaggerate impacts using snap.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.SnapImpact">
            <summary>
            Exaggeration amount of the initial impact (legs).  +ve fold with car impact (as if pushed at hips in the car velocity direction).  -ve fold away from car impact.
            </summary>
            <remarks>
            Default value = 7.0f.
            Min value = -20.0f.
            Max value = 20.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.SnapBonnet">
            <summary>
            Exaggeration amount of the secondary (torso) impact with bonnet. +ve fold with car impact (as if pushed at hips by the impact normal).  -ve fold away from car impact.
            </summary>
            <remarks>
            Default value = -7.0f.
            Min value = -20.0f.
            Max value = 20.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.SnapFloor">
            <summary>
            Exaggeration amount of the impact with the floor after falling off of car +ve fold with floor impact (as if pushed at hips in the impact normal direction).  -ve fold away from car impact.
            </summary>
            <remarks>
            Default value = 7.0f.
            Min value = -20.0f.
            Max value = 20.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BraceForImpactHelper.DampVel">
```
