<!-- ScriptHookRDRNetAPI lines 17001-17250 of 30005 -->
```csharp
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the StopAllBehaviors <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
            <remarks>
            Send this message to immediately stop all behaviors from executing.
            </remarks>
        </member>
        <member name="T:RDR2.NaturalMotion.SetCharacterStrengthHelper">
            <summary>
            Sets character's strength on the dead-granny-to-healthy-terminator scale: [0..1].
            </summary>
        </member>
        <member name="M:RDR2.NaturalMotion.SetCharacterStrengthHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the SetCharacterStrengthHelper for sending a SetCharacterStrength <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the SetCharacterStrength <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
            <remarks>
            Sets character's strength on the dead-granny-to-healthy-terminator scale: [0..1].
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetCharacterStrengthHelper.CharacterStrength">
            <summary>
            Strength of character.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="T:RDR2.NaturalMotion.SetCharacterHealthHelper">
            <summary>
            Sets character's health on the dead-to-alive scale: [0..1].
            </summary>
        </member>
        <member name="M:RDR2.NaturalMotion.SetCharacterHealthHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the SetCharacterHealthHelper for sending a SetCharacterHealth <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the SetCharacterHealth <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
            <remarks>
            Sets character's health on the dead-to-alive scale: [0..1].
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetCharacterHealthHelper.CharacterHealth">
            <summary>
            Health of character.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="T:RDR2.NaturalMotion.SetFallingReactionHelper">
            <summary>
            Sets the type of reaction if catchFall is called.
            </summary>
        </member>
        <member name="M:RDR2.NaturalMotion.SetFallingReactionHelper.#ctor(RDR2.Ped)">
            <summary>
            Creates a new Instance of the SetFallingReactionHelper for sending a SetFallingReaction <see cref="T:RDR2.NaturalMotion.Message"/> to a given <see cref="T:RDR2.Ped"/>.
            </summary>
            <param name="ped">The <see cref="T:RDR2.Ped"/> to send the SetFallingReaction <see cref="T:RDR2.NaturalMotion.Message"/> to.</param>
            <remarks>
            Sets the type of reaction if catchFall is called.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetFallingReactionHelper.HandsAndKnees">
            <summary>
            Set to true to get handsAndKnees catchFall if catchFall called. If true allows the dynBalancer to stay on during the catchfall and modifies the catch fall to give a more alive looking performance (hands and knees for front landing or sitting up for back landing).
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetFallingReactionHelper.CallRDS">
            <summary>
            If true catchFall will call rollDownstairs if comVel GT comVelRDSThresh - prevents excessive sliding in catchFall.  Was previously only true for handsAndKnees.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetFallingReactionHelper.ComVelRDSThresh">
            <summary>
            ComVel above which rollDownstairs will start - prevents excessive sliding in catchFall.
            </summary>
            <remarks>
            Default value = 2.0f.
            Min value = 0.0f.
            Max value = 20.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetFallingReactionHelper.ResistRolling">
            <summary>
            For rds catchFall only: True to resist rolling motion (rolling motion is set off by ub contact and a sliding velocity), false to allow more of a continuous rolling  (rolling motion is set off at a sliding velocity).
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetFallingReactionHelper.ArmReduceSpeed">
            <summary>
            Strength is reduced in the catchFall when the arms contact the ground.  0.2 is good for handsAndKnees.  2.5 is good for normal catchFall, anything lower than 1.0 for normal catchFall may lead to bad catchFall poses.
            </summary>
            <remarks>
            Default value = 2.5f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetFallingReactionHelper.ReachLengthMultiplier">
            <summary>
            Reach length multiplier that scales characters arm topological length, value in range from (0, 1 GT  where 1.0 means reach length is maximum.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.3f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetFallingReactionHelper.InhibitRollingTime">
            <summary>
            Time after hitting ground that the catchFall can call rds.
            </summary>
            <remarks>
            Default value = 0.2f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetFallingReactionHelper.ChangeFrictionTime">
            <summary>
            Time after hitting ground that the catchFall can change the friction of parts to inhibit sliding.
            </summary>
            <remarks>
            Default value = 0.2f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetFallingReactionHelper.GroundFriction">
            <summary>
            8.0 was used on yanked) Friction multiplier on body parts when on ground.  Character can look too slidy with groundFriction = 1.  Higher values give a more jerky reaction but this seems timestep dependent especially for dragged by the feet.
            </summary>
            <remarks>
            Default value = 1.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetFallingReactionHelper.FrictionMin">
            <summary>
            Min Friction of an impact with a body part (not head, hands or feet) - to increase friction of slippy environment to get character to roll better.  Applied in catchFall and rollUp(rollDownStairs).
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 10.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetFallingReactionHelper.FrictionMax">
            <summary>
            Max Friction of an impact with a body part (not head, hands or feet) - to increase friction of slippy environment to get character to roll better.  Applied in catchFall and rollUp(rollDownStairs).
            </summary>
            <remarks>
            Default value = 9999.0f.
            Min value = 0.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetFallingReactionHelper.StopOnSlopes">
            <summary>
            Apply tactics to help stop on slopes.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetFallingReactionHelper.StopManual">
            <summary>
            Override slope value to manually force stopping on flat ground.  Encourages character to come to rest face down or face up.
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetFallingReactionHelper.StoppedStrengthDecay">
            <summary>
            Speed at which strength reduces when stopped.
            </summary>
            <remarks>
            Default value = 5.0f.
            Min value = 0.0f.
            Max value = 20.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetFallingReactionHelper.SpineLean1Offset">
            <summary>
            Bias spine post towards hunched (away from arched).
            </summary>
            <remarks>
            Default value = 0.0f.
            Min value = 0.0f.
            Max value = 1.0f.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetFallingReactionHelper.RiflePose">
            <summary>
            Hold rifle in a safe position to reduce complications with collision.  Only applied if holding a rifle.
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetFallingReactionHelper.HkHeadAvoid">
            <summary>
            Enable head ground avoidance when handsAndKnees is true.
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetFallingReactionHelper.AntiPropClav">
            <summary>
            Discourage the character getting stuck propped up by elbows when falling backwards - by inhibiting backwards moving clavicles (keeps the arms slightly wider).
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetFallingReactionHelper.AntiPropWeak">
            <summary>
            Discourage the character getting stuck propped up by elbows when falling backwards - by weakening the arms as soon they hit the floor.  (Also stops the hands lifting up when flat on back).
            </summary>
            <remarks>
            Default value = False.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetFallingReactionHelper.HeadAsWeakAsArms">
            <summary>
            Head weakens as arms weaken. If false and antiPropWeak when falls onto back doesn't loosen neck so early (matches bodyStrength instead).
            </summary>
            <remarks>
            Default value = True.
            </remarks>
        </member>
        <member name="P:RDR2.NaturalMotion.SetFallingReactionHelper.SuccessStrength">
```
