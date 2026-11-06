<!-- ScriptHookRDRNetAPI lines 7251-7500 of 30005 -->
```csharp
            <summary>
            The distance between these points, is the diagonal of a box (remember it's 3D).
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._GET_LAST_LED_MOUNT(System.Int32)">
            <summary>
            Returns last horse the ped was leading
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xED1C764997A86D5A(System.Int32,System.Int32)">
            <summary>
            Only used in R* Script nb_stalking_hunter
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xB8AB265426CFE6DD(System.Int32,System.Boolean)">
            <summary>
            _SET_HO*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xE8D1CCB9375C101B(System.Int32,System.Int32)">
            <summary>
            Returns dismounted timestamp
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._GET_HORSE_TAMING_STATE(System.Int32)">
            <summary>
            Returns an int based on enum eTamingState
            
            enum eTamingState
            {
            	ATS_INVALID = 0,
            	ATS_INACTIVE,
            	ATS_TARGET_DETECTED,
            	ATS_CALLED_OUT,
            	ATS_MOUNTABLE,
            	ATS_BEING_PATTED,
            	ATS_BREAKING_ACTIVE,
            	ATS_SPOOKED,
            	ATS_RETREATING,
            	ATS_FLEEING
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._SET_MOUNT_SECURITY_ENABLED(System.Int32,System.Boolean)">
            <summary>
            Note: this native was added in build 1232.40
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x9B65444C07B782BF(System.Int32,System.String)">
            <summary>
            Only used in SP R* Script winter1: p1 = Winter1Mount
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x8BE24D74D74C6E9B(System.Int32)">
            <summary>
            Used in Script Function NET_CAMP_DOG_CLIENT_HANDLE_ANIMAL_INTERACTION
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x77243ED4F7CAAA55(System.Int32)">
            <summary>
            _IS_I* - _IS_L*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.IS_PED_ON_VEHICLE(System.Int32,System.Boolean)">
            <summary>
            Gets a value indicating whether the specified ped is on top of any vehicle.
            
            Return 1 when ped is on vehicle.
            Return 0 when ped is not on a vehicle.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.IS_PED_SITTING_IN_VEHICLE(System.Int32,System.Int32)">
            <summary>
            Detect if ped is sitting in the specified vehicle
            [True/False]
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.IS_PED_SITTING_IN_ANY_VEHICLE(System.Int32)">
            <summary>
            Detect if ped is in any vehicle
            [True/False]
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x256EDD55C6BE1482(System.Int32)">
            <summary>
            _IS_PED_FL* - _IS_PED_FU*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._GET_PED_DAMAGE_CLEANLINESS(System.Int32)">
            <summary>
            enum ePedDamageCleanliness
            {
            	PED_DAMAGE_CLEANLINESS_POOR,
            	PED_DAMAGE_CLEANLINESS_GOOD,
            	PED_DAMAGE_CLEANLINESS_PERFECT
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._SET_PED_DAMAGE_CLEANLINESS(System.Int32,System.Int32)">
            <summary>
            damageCleanliness: see _GET_PED_DAMAGE_CLEANLINESS
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._GET_PED_DAMAGED(System.Int32)">
            <summary>
            Returns true if _GET_PED_DAMAGE_CLEANLINESS was ever lower than 2
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._SET_PLAYER_CURRENT_ANIMAL_DAMAGE_MODIFIER(System.Int32,System.Single,System.Int32,System.Int32)">
            <summary>
            Animal Skin Quality Modifier
            Params: p2 = 2, p3 = 3 in R* Scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._GET_PLAYER_CURRENT_ANIMAL_DAMAGE_MODIFIER(System.Int32)">
            <summary>
            Returns animal skin quality modifier
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xB29C553BA582D09E(System.UInt64*,System.UInt32,System.Int32,System.Int32)">
            <summary>
            Used in Script Function SATCHEL_COUNT_BREAKDOWN_COMPONENTS
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._COMPUTE_SATCHEL_ITEM_FOR_PED_CARCASS(System.UInt64*,System.Int32,System.Int32,System.Int32)">
            <summary>
            Related to dead animals items/loots
            Notice: skinningQuality is partially calculated using pedQuality
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._SET_TOTAL_PED_DAMAGE_FROM_AI(System.Int32,System.Single)">
            <summary>
            _SET_W(EAPON?)*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xA6D6F03095C88F59(System.Int32)">
            <summary>
            _DELETE_*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._GET_TOTAL_PED_DAMAGE_FROM_AI(System.Int32)">
            <summary>
            _GET_WA*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.SET_PED_TO_PLAYER_WEAPON_DAMAGE_MODIFIER(System.Int32,System.Single)">
            <summary>
            Old name: _SET_PED_DAMAGE_MODIFIER
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x763FA8A9D76EE3A7(System.Int32)">
            <summary>
            Used in Script Function NB_EVENT_OVERRIDE__HANDLE__EVENT_DAMAGE_ENTITY
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x3FDCC1F8C17E303E(System.Int32,System.Int32,System.Single)">
            <summary>
            Changes health bar around heart core icon
            INITIALISE_NEW_ROLE - Applying Super Jump buffs: p1 = 10, p2 = 0.0f
            INITIALISE_NEW_ROLE - Clearing up Super Jump buffs: p1 = 10, p2 = 1.0f
            _SET_D*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xE50C9816B3F22D8B(System.Int32,System.UInt32,System.Single)">
            <summary>
            _SET_D*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x7883AA809DF43D98(System.Int32,System.Single)">
            <summary>
            _SET_M*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xC5B78E41DCF8227C(System.Int32,System.Boolean)">
            <summary>
            _SET_H* - _SET_I*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x577C60BA06D0EA64(System.Int32)">
            <summary>
            _IS_PED_C* - _IS_PED_D*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.IS_PED_OPENING_DOOR(System.Int32)">
            <summary>
            Returns true if the ped is currently opening a door (CTaskOpenDoor).
            
            Old name: _IS_PED_OPENING_A_DOOR
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x5C6C7C70CA302801(System.Int32)">
            <summary>
            _IS_PED_IN*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xB91AB3BE7F655D49(System.Int32)">
            <summary>
            _IS_PED_J* - _IS_PED_L*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x028E7B3BBA0BD2FC(System.Int32)">
            <summary>
            _SET_ST* - _SET_SW*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xFA8C10DCE0706D43(System.Int32)">
            <summary>
            _HAS_PED_*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x2BA9D7BF629F920C(System.Int32)">
            <summary>
            Used in Script Function CHECK_IS_PLAYER_SEEN
            _GET_PED_*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x900CA00CE703E1E2(System.Int32)">
            <summary>
            Used in Script Function AGGRO_SHOULD_PED_HEAR_DEATH
            _GET_PED_*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x9AB33CB5834885B3(System.Int32,System.Single,System.Single,System.Single,System.Single)">
            <summary>
            _SET_PED_M*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x899DFA0009AC93DE(System.Int32,System.Single)">
            <summary>
            _SET_PED_O*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x3A5697B80FED5EBE(System.Int32,System.Single,System.Single,System.Single,System.Single)">
            <summary>
            _SET_PED_MO*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.SET_PED_STEALTH_MOVEMENT(System.Int32,System.Boolean,System.UInt64,System.UInt64)">
            <summary>
            Not implemented.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.GET_PED_STEALTH_MOVEMENT(System.Int32)">
            <summary>
            Returns whether the entity is in stealth mode
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.CREATE_GROUP(System.Int32)">
            <summary>
            Creates a new ped group.
```
