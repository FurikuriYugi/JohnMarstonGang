<!-- ScriptHookRDRNetAPI lines 19001-19250 of 30005 -->
```csharp
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.ShoulderL2">
            <summary>
            Mmmmdrunk = 0.7 shoulder lean2 offset.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = -3.0f.
            Max value = 3.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.ShoulderL1">
            <summary>
            Mmmmdrunk 1.1 shoulder lean1 offset (+ve frankenstein).
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = -1.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.ShoulderTwist">
            <summary>
            Mmmmdrunk = 0.0 shoulder twist.
            </summary>
            <remarks>
            Default value = -0.4f.
            Min value = -3.0f.
            Max value = 3.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.HeadLookAtVelProb">
            <summary>
            Probability [0-1] that headLook will be looking in the direction of velocity when stepping.
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.TurnOffProb">
            <summary>
            Weighted probability that turn will be off. This is one of six turn type weights.
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.Turn2VelProb">
            <summary>
            Weighted probability of turning towards velocity. This is one of six turn type weights.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.TurnAwayProb">
            <summary>
            Weighted probability of turning away from headLook target. This is one of six turn type weights.
            </summary>
            <remarks>
            Default value = 0.2f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.TurnLeftProb">
            <summary>
            Weighted probability of turning left. This is one of six turn type weights.
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.TurnRightProb">
            <summary>
            Weighted probability of turning right. This is one of six turn type weights.
            </summary>
            <remarks>
            Default value = 0.1f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.Turn2TargetProb">
            <summary>
            Weighted probability of turning towards headLook target. This is one of six turn type weights.
            </summary>
            <remarks>
            Default value = 0.2f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.AngVelMultiplier">
            <summary>
            Somersault, twist, sideSomersault) multiplier of the angular velocity  for arms out (lean2) (somersault, twist, sideSomersault).
            </summary>
            <remarks>
            Default value = Vector3(4.0f, 1.0f, 4.0f).
            Min value = 0.0f.
            Max value = 20.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.AngVelThreshold">
            <summary>
            Somersault, twist, sideSomersault) threshold above which angVel is used for arms out (lean2) Unless drunk - DO NOT EXCEED 7.0 for each component.
            </summary>
            <remarks>
            Default value = Vector3(1.2f, 3.0f, 1.2f).
            Min value = 0.0f.
            Max value = 40.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.BraceDistance">
            <summary>
            If -ve then do not brace.  distance from object at which to raise hands to brace 0.5 good if newBrace=true - otherwise 0.65.
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.TargetPredictionTime">
            <summary>
            Time expected to get arms up from idle.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.ReachAbsorbtionTime">
            <summary>
            Larger values and he absorbs the impact more.
            </summary>
            <remarks>
            Default value = 0.2f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.BraceStiffness">
            <summary>
            Stiffness of character. Catch_fall stiffness scales with this too, with its defaults at this values default.
            </summary>
            <remarks>
            Default value = 12.0f.
            Min value = 6.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.MinBraceTime">
            <summary>
            Minimum bracing time so the character doesn't look twitchy.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 3.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.TimeToBackwardsBrace">
            <summary>
            Time before arm brace kicks in when hit from behind.
            </summary>
            <remarks>
            Default value = 0.5f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.HandsDelayMin">
            <summary>
            If bracing with 2 hands delay one hand by at least this amount of time to introduce some asymmetry.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 3.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.HandsDelayMax">
            <summary>
            If bracing with 2 hands delay one hand by at most this amount of time to introduce some asymmetry.
            </summary>
            <remarks>
            Default value = 0.7f.
            Min value = 0.0f.
            Max value = 3.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.BraceOffset">
            <summary>
            BraceTarget is global headLookPos plus braceOffset m in the up direction.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = -2.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.MoveRadius">
            <summary>
            If -ve don't move away from pusher unless moveWhenBracing is true and braceDistance  GT  0.0f.  if the pusher is closer than moveRadius then move away from it.
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.MoveAmount">
            <summary>
            Amount of leanForce applied away from pusher.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.MoveWhenBracing">
            <summary>
            Only move away from pusher when bracing against pusher.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="M:RDR2.NaturalMotion.BodyFoetalHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the BodyFoetalHelper for sending a BodyFoetal <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the BodyFoetal <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
        </member>
```
