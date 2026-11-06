<!-- ScriptHookRDRNetAPI lines 18751-19000 of 30005 -->
```csharp
            Default value = 9.0f.
            Min value = 6.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.Elbow">
            <summary>
            How much the elbow swings based on the leg movement.
            </summary>
            <remarks>
            Default value = 0.9f.
            Min value = 0.0f.
            Max value = 4.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.Shoulder">
            <summary>
            How much the shoulder(lean1) swings based on the leg movement.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 4.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.ArmDamping">
            <summary>
            NB. WAS m_damping NeckDamping=1 ClaviclesDamping=1.
            </summary>
            <remarks>
            Default value = 0.7f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.UseHeadLook">
            <summary>
            Enable and provide a look-at target to make the character's head turn to face it while balancing.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.HeadLookPos">
            <summary>
            Position of thing to look at.
            </summary>
            <remarks>
            Default value = Vector3(0.0f, 0.0f, 0.0f).
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.HeadLookInstanceIndex">
            <summary>
            Level index of thing to look at.
            </summary>
            <remarks>
            Default value = -1.
            Min value = -1.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.SpineStiffness">
            <summary>
            </summary>
            <remarks>
            Default value = 10.0f.
            Min value = 6.0f.
            Max value = 16.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.SomersaultAngle">
            <summary>
            Multiplier of the somersault 'angle' (lean forward/back) for arms out (lean2).
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.SomersaultAngleThreshold">
            <summary>
            Amount of somersault 'angle' before m_somersaultAngle is used for ArmsOut. Unless drunk - DO NOT EXCEED 0.8.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.SideSomersaultAngle">
            <summary>
            Amount of side somersault 'angle' before sideSomersault is used for ArmsOut. Unless drunk - DO NOT EXCEED 0.8.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.SideSomersaultAngleThreshold">
            <summary>
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.BackwardsAutoTurn">
            <summary>
            Automatically turn around if moving backwards.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.TurnWithBumpRadius">
            <summary>
            0.9 is a sensible value.  If pusher within this distance then turn to get out of the way of the pusher.
            </summary>
            <remarks>
            Default value = -1.0f.
            Min value = -1.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.BackwardsArms">
            <summary>
            Bend elbows, relax shoulders and inhibit spine twist when moving backwards.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.BlendToZeroPose">
            <summary>
            Blend upper body to zero pose as the character comes to rest. If false blend to a stored pose.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.ArmsOutOnPush">
            <summary>
            Put arms out based on lean2 of legs, or angular velocity (lean or twist), or lean (front/back or side/side).
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.ArmsOutOnPushMultiplier">
            <summary>
            Arms out based on lean2 of the legs to simulate being pushed.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.ArmsOutOnPushTimeout">
            <summary>
            Number of seconds before turning off the armsOutOnPush response only for Arms out based on lean2 of the legs (NOT for the angle or angular velocity).
            </summary>
            <remarks>
            Default value = 1.1f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.ReturningToBalanceArmsOut">
            <summary>
            Range 0:1 0 = don't raise arms if returning to upright position, 0.x = 0.x*raise arms based on angvel and 'angle' settings, 1 = raise arms based on angvel and 'angle' settings.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.ArmsOutStraightenElbows">
            <summary>
            Multiplier for straightening the elbows based on the amount of arms out(lean2) 0 = dont straighten elbows. Otherwise straighten elbows proportionately to armsOut.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.ArmsOutMinLean2">
            <summary>
            Minimum desiredLean2 applied to shoulder (to stop arms going above shoulder height or not).
            </summary>
            <remarks>
            Default value = -9.9f.
            Min value = -10.0f.
            Max value = 0.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.SpineDamping">
            <summary>
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.UseBodyTurn">
            <summary>
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.ElbowAngleOnContact">
            <summary>
            On contact with upperbody the desired elbow angle is set to at least this value.
            </summary>
            <remarks>
            Default value = 1.9f.
            Min value = 0.0f.
            Max value = 3.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.BendElbowsTime">
            <summary>
            Time after contact (with Upper body) that the min m_elbowAngleOnContact is applied.
            </summary>
            <remarks>
            Default value = 0.3f.
            Min value = 0.0f.
            Max value = 2.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.BendElbowsGait">
            <summary>
            Minimum desired angle of elbow during non contact arm swing.
            </summary>
            <remarks>
            Default value = 0.7f.
            Min value = -3.0f.
            Max value = 3.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.BodyBalanceHelper.HipL2ArmL2">
            <summary>
            Mmmmdrunk = 0.2 multiplier of hip lean2 (star jump) to give shoulder lean2 (flapping).
            </summary>
```
