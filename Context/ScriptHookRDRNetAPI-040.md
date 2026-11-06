<!-- ScriptHookRDRNetAPI lines 9751-10000 of 30005 -->
```csharp
        </member>
        <member name="M:RDR2.Native.PLAYER.SET_AIR_DRAG_MULTIPLIER_FOR_PLAYERS_VEHICLE(System.Int32,System.Single)">
            <summary>
            This can be between 1.0f - 50.0f
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER.SET_SWIM_MULTIPLIER_FOR_PLAYER(System.Int32,System.Single)">
            <summary>
            Swim speed multiplier.
            Multiplier goes up to 1.49f
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER.DISABLE_PLAYER_FIRING(System.Int32,System.Boolean)">
            <summary>
            Inhibits the player from using any method of combat including melee and firearms.
            
            NOTE: Only disables the firing for one frame
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._0xBBA140062B15A8AC(System.Int32)">
            <summary>
            Used in script function INIT_DEADEYE_SLOWDOWN
            _SPECIAL_ABILITY*
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._MODIFY_INFINITE_TRAIL_VISION(System.Int32,System.Boolean)">
            <summary>
            Toggle handles wether Deadeye and Eagleeye are infinite or not.
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._SPECIAL_ABILITY_RESTORE_BY_AMOUNT(System.Int32,System.Single,System.Int32,System.Int32,System.Int32)">
            <summary>
            Restores Deadeye by given amount.
            Params: p2, p3, p4 = 0, 0, 1 in R* Scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._0xFA437FA0738C370C(System.Int32,System.Single,System.Int32,System.Int32,System.Int32)">
            <summary>
            Params: p1, p2, p3, p4 = 1.f, 0, 0, 0 in R* Scripts
            _SPECIAL_ABILITY*
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._SPECIAL_ABILITY_RESTORE_OUTER_RING(System.Int32,System.Single)">
            <summary>
            Only used in R* SP Script short_update
            Restores Deadeye Outer Ring
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._GET_PLAYER_REQUIRED_DEAD_EYE_AMOUNT(System.Int32)">
            <summary>
            If player has less Dead Eye than required, Dead Eye cant be triggered.
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._SPECIAL_ABILITY_GET_AMOUNT_CACHED(System.Int32)">
            <summary>
            Returns Deadeye value from player
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._SPECIAL_ABILITY_DRAIN_BY_AMOUNT(System.Int32,System.Single,System.UInt64)">
            <summary>
            Drains Deadeye by given amount.
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._SPECIAL_ABILITY_START_RESTORE(System.Int32,System.Int32,System.Boolean)">
            <summary>
            Params: p1 = -1 in R* Scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._SET_SPECIAL_ABILITY_DURATION_COST(System.Int32,System.Single)">
            <summary>
            durationCost: per second
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._SET_SPECIAL_ABILITY_DISABLE_TIMER(System.Int32,System.Single)">
            <summary>
            Only used in R* SP Script short_update
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._0x4D1699543B1C023C(System.Int32,System.Single)">
            <summary>
            _SET_SPECIAL_ABILITY_*
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._SET_PLAYER_STAT_FLAG_HASH(System.Int32,System.UInt32)">
            <summary>
            _N*, _O* or _PE*
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._ENABLE_EAGLEEYE(System.Int32,System.Boolean)">
            <summary>
            (Un)lock Eagle Eye functionality
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._SECONDARY_SPECIAL_ABILITY_SET_ACTIVE(System.Int32)">
            <summary>
            Activates EagleEye, called together with 0x28A13BF6B05C3D83
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._SECONDARY_SPECIAL_ABILITY_SET_DISABLED(System.Int32,System.Boolean)">
            <summary>
            Deactivates EagleEye, called together with 0xC0B21F235C02139C
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER.STOP_PLAYER_TELEPORT">
            <summary>
            Disables the player's teleportation
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._SET_PLAYER_HEALTH_RECHARGE_TIME_MODIFIER(System.Int32,System.Single)">
            <summary>
            Setting player's Health recharge time to zero forces immediate health regen
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._SET_PED_ACTIVE_PLAYER_HORSE(System.Int32,System.Int32)">
            <summary>
            Seems to work similar to 0xD2CB0FB0FDCB473D
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._0x77B0B6D17A3AC9AA(System.UInt64,System.UInt64)">
            <summary>
            nullsub, doesn't do anything
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._0xDF93973251FB2CA5(System.Int32,System.Boolean)">
            <summary>
            _SET_PLAYER_*
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER.SET_PLAYER_WEAPON_DAMAGE_MODIFIER(System.Int32,System.Single)">
            <summary>
            This modifies the damage value of your weapon. Whether it is a multiplier or base damage is unknown.
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._0x818241B3EDA84191(System.Int32,System.Boolean)">
            <summary>
            _SET_PLAYER_DAMAGE_* - _SET_PLAYER_DEFENSE_*
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._SET_RECEIVED_HORSEBACK_DAMAGE_DECREASE(System.Int32,System.Single)">
            <summary>
            Decreases the damage the player receives while on horseback
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._SET_AI_PLAYER_DEFENSE_MODIFIER_AGAINST_AI(System.Int32,System.Single)">
            <summary>
            Sets Player's Defense against AI modifier
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._SET_PLAYER_DEFENSE_MODIFIER(System.Int32,System.Single,System.Single)">
            <summary>
            Sets stamina core drains peed using ranged damage scale and melee damage scale
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._SET_PLAYER_DEFENSE_TYPE_MODIFIER(System.Int32,System.Int32,System.Single)">
            <summary>
            bullet damage modifier: type = 4
            explosive damage Defense mod: type = 7
            fire damage Defense mod: type = 8, 15
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._SET_PLAYER_LASSO_DAMAGE_PER_SECOND(System.Int32,System.Single)">
            <summary>
            _SET_PLAYER_A* - _SET_PLAYER_C*
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER.IS_PLAYER_RIDING_TRAIN(System.Int32)">
            <summary>
            Returns true if the player is riding a train.
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._SET_DEADEYE_ABILITY_LEVEL(System.Int32,System.Int32)">
            <summary>
            Max level is 5.
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._SET_DEADEYE_ABILITY_DEPLETION_DELAY(System.Int32,System.Single)">
            <summary>
            Only used in R* SP Script short_update
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._0x570A13A4CA2799BB(System.Int32,System.Boolean)">
            <summary>
            Used in script function INIT_DEADEYE_SLOWDOWN
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._0x3ACAC8832E77BC93(System.Int32,System.Boolean)">
            <summary>
            Used in script function INIT_DEADEYE_SLOWDOWN
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._REGISTER_EAGLE_EYE_FOR_ENTITY(System.Int32,System.Int32,System.Boolean)">
            <summary>
            Used for setting up eagle eye for entity
            Params: p2 = re-register or not?
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._0xE5D3EB37ABC1EB03(System.Int32)">
            <summary>
            _CLEAR_FACIAL_* - _CLEAR_PED_BLOOD*
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._EAGLE_EYE_SET_COLOR(System.Int32,System.Boolean,System.UInt64*)">
            <summary>
            false: default eagleeye color
            true: green eagleeye color
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._0xF21C7A3F3FFBA629(System.Int32)">
            <summary>
            _CLEAR_FACIAL_* - _CLEAR_PED_BLOOD*
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._SET_LOCAL_PLAYER_PERSONA_ABILITY_FLAG(System.Int32,System.Boolean)">
            <summary>
            see personaabilities.meta
            enum ePersonaAbilityFlag
            {
            	PERSONA_CAN_AUTOESCAPE_FROM_LASSO,
            	PERSONA_HAT_BLOCKS_FIRST_HEADSHOT,
            	PERSONA_FULL_AUTO_FOR_ALL_WEAPONS,
            	PERSONA_MIGHT_LIVE_AFTER_DEADLY_DAMAGE,
            	PERSONA_IGNORE_AIM_BEFORE_FIRING_RESTRICTIONS,
            	PERSONA_DEADEYE_INSTANT_RELOAD,
            	PERSONA_USE_PHOSPHOROUS_ROUNDS,
            	PERSONA_CONT_DEADEYE_ON_TAKING_COVER,
            	PERSONA_CONT_DEADEYE_ON_RELOAD,
            	PERSONA_CONT_DEADEYE_ON_SHOOTING,
            	PERSONA_CONT_DEADEYE_ON_EXITING_AIM,
            	PERSONA_DISABLE_PLAYER_CANCELLING_DEADEYE,
            	PERSONA_CONT_DEADEYE_ON_RAGDOLL,
            	PERSONA_USE_EXPLOSIVE_ROUNDS,
            	PERSONA_EXIT_DEADEYE_ON_TAKING_DAMAGE,
            	PERSONA_CARRY_TWO_MONEYBAGS,
            	PERSONA_ABILITY_LONG_PICK_HERBS,
            	PERSONA_ABILITY_UNBREAKABLE_LASSO,
            	PERSONA_CONT_DEADEYE_ON_SPRINTING,
            	PERSONA_CANT_DEAL_HEADSHOTS,
            	PERSONA_HANGMAN,
            	PERSONA_ALLOW_DEADEYE_WITH_MELEE_WEAPONS,
            	PERSONA_ALLOW_DEADEYE_WHILE_UNARMED,
            	PERSONA_DISABLE_DEADEYE_PERFECT_ACCURACY,
            	PERSONA_CANT_DEAL_HEADSHOTS_TO_PLAYERS,
            	PERSONA_CANT_DEAL_CRITICAL_DAMAGE,
            	PERSONA_CANT_DEAL_CRITICAL_DAMAGE_TO_PLAYERS,
            	PERSONA_ALLOW_EAGLEEYE_IN_COMBAT,
            	PERSONA_CONT_EAGLEEYE_ON_SPRINT,
            	PERSONA_SUPPRESS_LENGENDARY_EAGLEEYE_TRAIL_COLOR
            };
            </summary>
        </member>
```
