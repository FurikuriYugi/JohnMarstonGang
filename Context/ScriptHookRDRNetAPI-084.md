<!-- ScriptHookRDRNetAPI lines 20751-21000 of 30005 -->
```csharp
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.FallOverWallHelper.UseArmIK">
            <summary>
            Try to reach the wallEdge. To configure the IK : use limitAngleBack, limitAngleFront and limitAngleTotallyBack.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.FallOverWallHelper.ReachDistanceFromHitPoint">
            <summary>
            Distance from predicted hitpoint where each hands will try to reach the wall.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.FallOverWallHelper.MinReachDistanceFromHitPoint">
            <summary>
            Minimal distance from predicted hitpoint where each hands will try to reach the wall. Used if the hand target is outside the wall Edge.
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.FallOverWallHelper.AngleTotallyBack">
            <summary>
            Max angle in degrees (between 1.the vector between two hips and 2. WallEdge) to try to reach the wall just behind his pelvis with his arms when the character is back to the wall.
            </summary>
            <remarks>
            Default value = 15.0f.
            Min value = 0.0f.
            Max value = 180.0f.
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.GrabHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the GrabHelper for sending a Grab <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the Grab <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.GrabHelper.UseLeft">
            <summary>
            Flag to toggle use of left hand.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.GrabHelper.UseRight">
            <summary>
            Flag to toggle the use of the Right hand.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.GrabHelper.DropWeaponIfNecessary">
            <summary>
            If hasn't grabbed when weapon carrying hand is close to target, grab anyway.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.GrabHelper.DropWeaponDistance">
            <summary>
            Distance below which a weapon carrying hand will request weapon to be dropped.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.GrabHelper.GrabStrength">
            <summary>
            Strength in hands for grabbing (kg m/s), -1 to ignore/disable.
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.0f.
            Max value = 10000.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.GrabHelper.StickyHands">
            <summary>
            Strength of cheat force on hands to pull towards target and stick to target ("cleverHandIK" strength).
            </summary>
            <remarks>
            Default value = 4.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.GrabHelper.TurnToTarget">
            <summary>
            </summary>
            <remarks>
            Default value = <see cref="F:RDR2.NaturalMotion.TurnType.ToTarget"/>.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.GrabHelper.GrabHoldMaxTimer">
            <summary>
            Amount of time, in seconds, before grab automatically bails.
            </summary>
            <remarks>
            Default value = 100.0f.
            Min value = 0.0f.
            Max value = 1000.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.GrabHelper.PullUpTime">
            <summary>
            Time to reach the full pullup strength.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 4.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.GrabHelper.PullUpStrengthRight">
            <summary>
            Strength to pull up with the right arm. 0 = no pull up.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.GrabHelper.PullUpStrengthLeft">
            <summary>
            Strength to pull up with the left arm. 0 = no pull up.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.GrabHelper.Pos1">
            <summary>
            Grab pos1, right hand if not using line or surface grab.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.GrabHelper.Pos2">
            <summary>
            Grab pos2, left hand if not using line or surface grab.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.GrabHelper.Pos3">
            <summary>
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.GrabHelper.Pos4">
            <summary>
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.GrabHelper.NormalR">
            <summary>
            Normal for the right grab point.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.GrabHelper.NormalL">
            <summary>
            Normal for the left grab point.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.GrabHelper.NormalR2">
            <summary>
            Normal for the 2nd right grab point (if pointsX4grab=true).
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.GrabHelper.NormalL2">
            <summary>
            Normal for the 3rd left grab point (if pointsX4grab=true).
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.GrabHelper.HandsCollide">
            <summary>
            Hand collisions on when grabbing (false turns off hand collisions making grab more stable esp. To grab points slightly inside geometry).
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.GrabHelper.JustBrace">
            <summary>
            Flag to toggle between grabbing and bracing.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.GrabHelper.UseLineGrab">
            <summary>
            Use the line grab, Grab along the line (x-x2).
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.GrabHelper.PointsX4grab">
            <summary>
            Use 2 point.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
```
