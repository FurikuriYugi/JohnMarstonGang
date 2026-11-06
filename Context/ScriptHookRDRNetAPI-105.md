<!-- ScriptHookRDRNetAPI lines 26001-26250 of 30005 -->
```csharp
            Lean of spine to side in side velocity direction when going forwards.
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -5.0f.
            Max value = 5.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.Lean2multB">
            <summary>
            Lean of spine to side in side velocity direction when going backwards.
            </summary>
            <remarks>
            Default value = -2.0f.
            Min value = -5.0f.
            Max value = 5.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.PushOffDist">
            <summary>
            Amount stance foot is behind com in the direction of velocity before the leg tries to pushOff to increase momentum.
            Increase to lower the probability of the pushOff making the character bouncy.
            </summary>
            <remarks>
            Default value = 0.2f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.MaxPushoffVel">
            <summary>
            Stance leg will only pushOff to increase momentum if the vertical hip velocity is less than this value. 0.4 seems like a good value.
            The higher it is the less this functionality is applied.
            If it is very low or negative this can stop the pushOff altogether.
            </summary>
            <remarks>
            Default value = 20.0f.
            Min value = -20.0f.
            Max value = 20.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.HipBendMult">
            <summary>
            HipBend scaled with velocity.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = -10.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.AlwaysBendForwards">
            <summary>
            Bend forwards at the hip (hipBendMult) whether moving backwards or forwards.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.SpineBendMult">
            <summary>
            Spine bend scaled with velocity.
            </summary>
            <remarks>
            Default value = 0.4f.
            Min value = -10.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.UseHeadLook">
            <summary>
            Enable and provide a look-at target to make the character's head turn to face it while balancing, balancer default is 0.2.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.HeadLookPos">
            <summary>
            Position of thing to look at.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.HeadLookInstanceIndex">
            <summary>
            Level index of thing to look at.
            </summary>
            <remarks>
            Default value = -1.
            Min value = -1.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.HeadLookAtVelProb">
            <summary>
            Probability [0-1] that headLook will be looking in the direction of velocity when stepping.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.TurnOffProb">
            <summary>
            Weighted probability that turn will be off.
            This is one of six turn type weights.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.Turn2TargetProb">
            <summary>
            Weighted probability of turning towards headLook target.
            This is one of six turn type weights.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.Turn2VelProb">
            <summary>
            Weighted probability of turning towards velocity.
            This is one of six turn type weights.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.TurnAwayProb">
            <summary>
            Weighted probability of turning away from headLook target.
            This is one of six turn type weights.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.TurnLeftProb">
            <summary>
            Weighted probability of turning left.
            This is one of six turn type weights.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.TurnRightProb">
            <summary>
            Weighted probability of turning right.
            This is one of six turn type weights.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.UseBodyTurn">
            <summary>
            Enable and provide a positive bodyTurnTimeout and provide a look-at target to make the character turn to face it while balancing.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.StaggerFallHelper.UpperBodyReaction">
            <summary>
            Enable upper body reaction i.e. blindBrace and armswindmill.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.TeeterHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the TeeterHelper for sending a Teeter <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the Teeter <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
        <member name="P:RDR2.NaturalMotion.TeeterHelper.EdgeLeft">
            <summary>
            Defines the left edge point (left of character facing edge).
            </summary>
            <remarks>
            Default value = Vector3(39.5f, 38.9f, 21.1f).
            Min value = 0.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.TeeterHelper.EdgeRight">
            <summary>
            Defines the right edge point (right of character facing edge).
            </summary>
            <remarks>
            Default value = Vector3(39.5f, 39.9f, 21.1f).
            Min value = 0.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.TeeterHelper.UseExclusionZone">
            <summary>
            Stop stepping across the line defined by edgeLeft and edgeRight.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.TeeterHelper.UseHeadLook">
            <summary>
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.TeeterHelper.CallHighFall">
            <summary>
            Call highFall if fallen over the edge.
            If false just call blended writhe (to go over the top of the fall behavior of the underlying behavior e.g. bodyBalance).
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.TeeterHelper.LeanAway">
            <summary>
            Lean away from the edge based on velocity towards the edge (if closer than 2m from edge).
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.TeeterHelper.PreTeeterTime">
            <summary>
            Time-to-edge threshold to start pre-teeter (windmilling, etc).
            </summary>
            <remarks>
            Default value = 2.0f.
            Min value = 0.0f.
            Max value = 10.0f.
```
