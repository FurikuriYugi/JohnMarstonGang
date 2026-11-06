<!-- ScriptHookRDRNetAPI lines 7501-7750 of 30005 -->
```csharp
            Groups can contain up to 8 peds.
            
            The parameter is unused.
            
            Returns a handle to the created group, or 0 if a group couldn't be created.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.SET_PED_CAN_TELEPORT_TO_GROUP_LEADER(System.Int32,System.Int32,System.Boolean)">
            <summary>
            This only will teleport the ped to the group leader if the group leader teleports (sets coords).
            
            Only works in singleplayer
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.SET_GROUP_SEPARATION_RANGE(System.Int32,System.Single)">
            <summary>
            Sets the range at which members will automatically leave the group.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x89E59DBD15E21177(System.Int32,System.Int32)">
            <summary>
            _SET_C*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x7583A9D35248B83F(System.Int32)">
            <summary>
            _IS_PED_IN*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x2DD4E0E26DFAD97D(System.Int32,System.Int32,System.Single)">
            <summary>
            _IS_PED_M* - _IS_PED_O*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._GET_ACTIVE_DYNAMIC_SCENARIO_2(System.Int32)">
            <summary>
            Returns kneeling, sitting, squating, and sleeping scenario hashes
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._GET_ACTIVE_DYNAMIC_SCENARIO(System.Int32)">
            <summary>
            Returns kneeling, sitting, squating, and sleeping scenario hashes
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._GIVE_PED_SCENARIO_PROP_DYNAMIC(System.Int32,System.Int32,System.String,System.String,System.Boolean)">
            <summary>
            Only used in SP R* Script rcm_jack2
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._GET_PED_REGISTER_PROP(System.Int32,System.String,System.Boolean)">
            <summary>
            Params: p1 = p_banjo01x_XH_R_HAND00, mp006_p_dblcello01x_XH_R_HAND00, p_fiddle01x_PH_L_HAND, p_guitar01x_XH_R_HAND00, mp006_p_mnshn_washboard01x_XH_R_HAND00, p_bottleBeer01x_PH_R_HAND
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.GET_PED_SOURCE_OF_DEATH(System.Int32)">
            <summary>
            Returns the entity that killed the ped
            
            It is best to check if the Ped is dead before asking for its killer.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.GET_PED_CAUSE_OF_DEATH(System.Int32)">
            <summary>
            Returns the hash of the weapon/model/object that killed the ped.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.ADD_RELATIONSHIP_GROUP(System.String,System.UInt32*)">
            <summary>
            The hash of the created relationship group is output in the second parameter.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xDC91F22F09BC6C2F(System.UInt32,System.Boolean)">
            <summary>
            Used in Script Function MP_MAIN_OFFLINE__INITIALIZE_GAME
            _SET_RELATIONSHIP_*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x3ACCE14DFA6BA8C2(System.Int32,System.Int32,System.Single,System.Single,System.Single,System.Single,System.Int32)">
            <summary>
            Used in R* Script net_fetch (NET_FETCH_UPDATE_RECIPIENT_PROP_ILO_IN_COMBAT) and various SP Scripts
            Params: p1 = 4/5/6, p5 = 40.f/100.f, coords = Player ped
            Perhaps returns some distance (Clearing that the local player is able to use ILO while in combat because they are near the recipient but also near hated peds)
            _GET_NUM_M* - _GET_PEDS_J*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x40C9155AF8BC13F3(System.Int32)">
            <summary>
            _IS_PED_RE*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xF4860514AD354226(System.Int32,System.Single,System.Single,System.Single,System.Single,System.Int32*)">
            <summary>
            Only used in SP Scripts
            Returns count / index
            _C*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.IS_PED_RESPONDING_TO_EVENT(System.Int32,System.UInt32)">
            <summary>
            eventType: https://alloc8or.re/rdr3/doc/enums/eEventType.txt
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x5E9FAF6C513347B4(System.Int32,System.UInt32)">
            <summary>
            Only used in R* SP Scripts
            _GET_PED_IN*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x326F7951EF0D7F75(System.Int32,System.UInt32)">
            <summary>
            Only used in R* SP Script short_update
            _GET_TARGET_* - _GET_TRACKED*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xE76687023D8C8505(System.UInt32,System.Int32)">
            <summary>
            Used for AUDIO / ANIMSCENE (REFERENCE_REGIONAL_CHARACTER)
            Params: p1 = 0
            _GET_PED_IN*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xCB8F4C9343EBE240(System.Int32,System.UInt32,RDR2.Math.Vector3*)">
            <summary>
            Only used in R* SP Scripts
            _GET_PLAYER_W* - _GET_RANDOM_*
            </summary>
        </member>
        <!-- Badly formed XML comment ignored for member "M:RDR2.Native.PED.SET_PED_FIRING_PATTERN(System.Int32,System.UInt32)" -->
        <member name="M:RDR2.Native.PED._SET_PED_FIRING_PATTERN_2(System.Int32,System.UInt32)">
            <summary>
            Used in R* MP Script fm_mission_controller and various R* SP Scripts for ambush*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._SET_PED_FIRING_PATTERN_3(System.Int32,System.UInt32)">
            <summary>
            Only used in R* MP Script fm_mission_controller
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.SET_PED_SHOOT_RATE(System.Int32,System.Int32)">
            <summary>
            Params: shootRate = 0 - 1000
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x3C529A827998F9B3(System.Int32,System.Int32,System.Int32)">
            <summary>
            _SET_PED_TA* - _SET_PED_TO_*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x1F44B7E283C09EDE(System.Int32,System.Single,System.Int32)">
            <summary>
            Only used in SP R* Scripts
            Params: p2 = same as p2 of 0x3C529A827998F9B3
            _SET_PED_TA* - _SET_PED_TO_*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.SET_COMBAT_FLOAT(System.Int32,System.Int32,System.Single)">
            <summary>
            combatType can be between 0-14. See GET_COMBAT_FLOAT below for a list of possible parameters.
            https://github.com/femga/rdr3_discoveries/tree/master/AI/COMBAT_FLOATS
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x0455546F23FF08E4(System.Int32)">
            <summary>
            _DOES_GROUP_* - _DOES_N*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.GET_PED_GROUP_INDEX(System.Int32)">
            <summary>
            Returns the groupId of which the specified ped is a member of.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x917760CFE7A0E0F1(System.Int32)">
            <summary>
            _IS_PED_L* - _IS_PED_M*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.SET_GROUP_FORMATION(System.Int32,System.Int32)">
            <summary>
            eFormationType
            
            0: Default
            1: Circle Around Leader
            2: Alternative Circle Around Leader
            3: Line, with Leader at center
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xB05CC690CDE8A4A9(System.Int32,System.Single)">
            <summary>
            Used to set up bad guy groups in nb_kidnapped R* Script (MP_RE_KIDNAPPED): p1 = 4.f
            _SET_FORMATION_*
            </summary>
        </member>
        <!-- Badly formed XML comment ignored for member "M:RDR2.Native.PED._0x8AF8E647D6B2A649(System.Int32,System.Int32)" -->
        <member name="M:RDR2.Native.PED._0x87C2724A56F66020(System.Int32)">
            <summary>
            _CLEAR_PED_E* - _CLEAR_PED_L*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xD5BD1B5318A81994(System.Int32,System.Boolean)">
            <summary>
            _SET_FORMATION_*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x9BBEAF8B0C007F1E(System.Int32,System.Boolean)">
            <summary>
            _SET_PLAYER_CAN_B* - _SET_PLAYER_CAN_U*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xC99F104BDF8C7F5A(System.Int32,System.Boolean)">
            <summary>
            _SET_PLAYER_N* - _SET_PLAYER_S*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x02E741E19E39628C(System.Int32,System.Single)">
            <summary>
            _SET_PLAYER_SN* - _SET_PLAYER_STAMINA*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x97C475212B327666(System.Int32,System.Boolean)">
            <summary>
            _SET_SCENARIO_PED_* - _SET_SPAWNER_*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x154B7E841AC7412F(System.Int32,System.Boolean)">
            <summary>
            _SET_SCENARIO_PED_* - _SET_SPAWNER_*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x8AFCCC0F18D70018(System.Int32,System.Boolean)">
            <summary>
            _SET_FORMATION_*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xE1103300F3456DE7(System.Int32,System.Single,System.Single)">
            <summary>
            _SET_FORMATION_P*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xA8A95CECB1906EA2(System.Int32,System.Boolean)">
            <summary>
            _SET_ENABLE_B* - _SET_ENABLE_H*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xDDFAD4DEAA7FA362(System.Int32,System.Single,System.Single,System.Single,System.Single)">
            <summary>
            _SET_FORMATION_P*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x966DE09688A1DE39(System.Int32,System.Single,System.Single,System.Single,System.Single)">
            <summary>
```
