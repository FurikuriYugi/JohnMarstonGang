<!-- ScriptHookRDRNetAPI lines 9501-9750 of 30005 -->
```csharp
        </member>
        <member name="M:RDR2.Native.PLAYER.SET_PLAYER_CONTROL(System.Int32,System.Boolean,System.Int32,System.Boolean)">
            <summary>
            flags: https://github.com/Halen84/RDR3-Native-Flags-And-Enums/tree/main/eSetPlayerControlFlags
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER.SET_POLICE_RADAR_BLIPS(System.Boolean)">
            <summary>
            If toggle is set to false:
             The police won't be shown on the (mini)map
            
            If toggle is set to true:
             The police will be shown on the (mini)map
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER.IS_PLAYER_PLAYING(System.Int32)">
            <summary>
            Checks whether the specified player has a Ped, the Ped is not dead, is not injured and is not arrested.
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._MODIFY_PLAYER_UI_PROMPT(System.Int32,System.Int32,System.Int32,System.Boolean)">
            <summary>
            Params: https://github.com/Halen84/RDR3-Native-Flags-And-Enums/tree/main/ePromptType
            promptType is mostly 34 (PP_TRACK_ANIMAL), promptMode = 0 (PP_MODE_BLOCK) in R* Scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._GET_PLAYER_UI_PROMPT_IS_DISABLED(System.Int32,System.Int32,System.Int32)">
            <summary>
            Returns false if PromptType is enabled
            Params: See 0x0751D461F06E41CE
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._MODIFY_PLAYER_UI_PROMPT_FOR_PED(System.Int32,System.Int32,System.Int32,System.Int32,System.Boolean)">
            <summary>
            Params: See 0x0751D461F06E41CE
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._GET_PLAYER_UI_PROMPT_FOR_PED_IS_ENABLED(System.Int32,System.Int32,System.Int32,System.Int32)">
            <summary>
            Returns true if PromptType is enabled for ped (mount)
            Params: See 0x0751D461F06E41CE
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER.SET_ALL_RANDOM_PEDS_FLEE(System.Int32,System.Boolean)">
            <summary>
            Sets whether all random peds will run away from player if they are agitated (threatened) (bool=true), or some peds can stand up for themselves (bool=false).
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._SET_DISABLE_PLAYER_WANTED_LEVEL(System.Int32,System.Boolean)">
            <summary>
            Disables the players ability to be wanted by lawmen
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._0x927861B2C08DBEA5(System.Int32)">
            <summary>
            _GET_A* - _GET_C*
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._IS_PLAYER_FREE_FOCUSING(System.Int32)">
            <summary>
            Checks if player is focused on any entity
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._0xBEA3A6E5F5F79A6F(System.UInt64,System.UInt64)">
            <summary>
            _GET_PLAYER_I*
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER.IS_PLAYER_FREE_AIMING(System.Int32)">
            <summary>
            Gets a value indicating whether the specified player is currently aiming freely.
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER.IS_PLAYER_FREE_AIMING_AT_ENTITY(System.Int32,System.Int32)">
            <summary>
            Gets a value indicating whether the specified player is currently aiming freely at the specified entity.
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._0x7AE93C45EC14A166(System.Int32,System.Int32*)">
            <summary>
            Only used in script function PROCESS_PED_INTERRUPT_DIALOGUE
            _GET_PLAYER_*
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER.SET_PLAYER_LOCKON_RANGE_OVERRIDE(System.Int32,System.Single)">
            <summary>
            Affects the range of auto aim target.
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER.SET_PLAYER_CAN_BE_HASSLED_BY_GANGS(System.Int32,System.Boolean)">
            <summary>
            Sets whether this player can be hassled by gangs.
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER.SET_PLAYER_CAN_USE_COVER(System.Int32,System.Boolean)">
            <summary>
            Sets whether this player can take cover.
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._0xACA45DDCEF6071C4(System.Int32,System.Boolean)">
            <summary>
            _SET_PLAYER_CAN_BE_* - _SET_PLAYER_CAN_USE_*
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._0xA0C683284DF027C7(System.Int32,System.Int32,System.Boolean)">
            <summary>
            Params: p1 is mostly 15, sometimes 1 in R* Scripts (Function: PLAYER_TOGGLE_PICK_UP_HATS)
            _SET_PLAYER_*
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER.GET_MAX_WANTED_LEVEL">
            <summary>
            Gets the maximum wanted level the player can get.
            Ranges from 0 to 5.
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._0x8591EE69CC3ED257(System.Int32,System.Boolean)">
            <summary>
            SET_PLAYER_S/T*
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER.GET_PLAYER_GROUP(System.Int32)">
            <summary>
            Returns the group ID the player is member of.
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._0xB331D8A73F9D2BDF(System.Int32,System.UInt64*)">
            <summary>
            _IS_PLAYER_I* - _IS_PLAYER_P*
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._ADD_PLAYER_AS_FOLLOW_TARGET(System.Int32,System.Int32,System.Single,System.Single,System.Int32,System.Int32,System.Boolean)">
            <summary>
            Used in script function: NET_AUTO_FOLLOW_UPDATE_LEADER_VALUES
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._0x2009F8AB7A5E9D6D(System.Int32)">
            <summary>
            _IS_PLAYER_F*
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._0xE7F8707269544B29(System.Int32,System.Int32)">
            <summary>
            _IS_PLAYER_A* - _IS_PLAYER_BE*
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER.IS_PLAYER_CONTROL_ON(System.Int32)">
            <summary>
            Returns whether the player can control himself.
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER.IS_PLAYER_CLIMBING(System.Int32)">
            <summary>
            Returns TRUE if the player ('s ped) is climbing at the moment.
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._0x621D1B289CAF5978(System.Int32)">
            <summary>
            _IS_PLAYER_S* - _IS_PLAYER_T*
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER.IS_PLAYER_BEING_ARRESTED(System.Int32,System.Boolean)">
            <summary>
            Return true while player is being arrested / busted.
            
            If atArresting is set to 1, this function will return 1 when player is being arrested (while player is putting his hand up, but still have control)
            
            If atArresting is set to 0, this function will return 1 only when the busted screen is shown.
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER.GET_PLAYER_INDEX">
            <summary>
            Returns the same as PLAYER_ID and NETWORK_PLAYER_ID_TO_INT
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER.INT_TO_PLAYERINDEX(System.Int32)">
            <summary>
            Simply returns whatever is passed to it (Regardless of whether the handle is valid or not).
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER.INT_TO_PARTICIPANTINDEX(System.Int32)">
            <summary>
            Simply returns whatever is passed to it (Regardless of whether the handle is valid or not).
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER.PLAYER_ID">
            <summary>
            This returns YOUR 'identity' as a Player type.
            
            Always returns 0 in story mode.
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER.PLAYER_PED_ID">
            <summary>
            Returns current player ped
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER.NETWORK_PLAYER_ID_TO_INT">
            <summary>
            Does exactly the same thing as PLAYER_ID()
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._0xC67A4910425F11F1(System.Int32,System.String)">
            <summary>
            _DISABLE_*(PLAYER_FORCED_INTERACTION_LOCKON?)
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER.SET_PLAYER_INVINCIBLE(System.Int32,System.Boolean)">
            <summary>
            Simply sets you as invincible (Health will not deplete).
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER.GET_PLAYER_INVINCIBLE(System.Int32)">
            <summary>
            Returns the player's invincibility status.
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER.SET_PLAYER_TARGETING_MODE(System.Int32)">
            <summary>
            Sets your targeting mode for when you're on foot.
            enum eTargetingMode
            {
            	TARGETING_MODE_INVALID = -1,
            	TARGETING_MODE_CAUSAL, (Wide)
            	TARGETING_MODE_NORMAL,
            	TARGETING_MODE_HARD, (Narrow)
            	TARGETING_MODE_EXPERT (Free Aim)
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._SET_PLAYER_IN_VEHICLE_TARGETING_MODE(System.Int32)">
            <summary>
            Sets your targeting mode for when you're in a vehicle (perhaps a mount/horse).
            see SET_PLAYER_TARGETING_MODE for eTargetingMode
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._0x72AD59F7B7FB6E24(System.Int32,System.Int32)">
            <summary>
            _HAS_PLAYER_D*
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._SET_PLAYER_DAMAGE_INFO_OVERRIDE(System.Int32,System.String)">
            <summary>
            damageInfo: STANDARD_PED_DAMAGE, STANDARD_FEMALE_PED_DAMAGE, STANDARD_PLAYER_PED_DAMAGE_MP, STANDARD_FEMALE_PLAYER_PED_DAMAGE_MP
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._0x1F488807BC8E0630(System.Int32)">
            <summary>
            _RESET_PLAYER_A* - _RESET_PLAYER_I*
            </summary>
```
