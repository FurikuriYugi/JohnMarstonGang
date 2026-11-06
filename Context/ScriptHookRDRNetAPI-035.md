<!-- ScriptHookRDRNetAPI lines 8501-8750 of 30005 -->
```csharp
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._CLEAR_PED_COMBAT_STYLE(System.Int32,System.Int32)">
            <summary>
            Params: p1 = 1 in R* Scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._SET_PED_COMBAT_STYLE_MOD(System.Int32,System.UInt32,System.Single)">
            <summary>
            duration in seconds, -1.0 = forever
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x1FA132CBCD7CB239(System.Int32,System.UInt32)">
            <summary>
            _CLEAR_PED_COMBAT_*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x5BF0B9D9A8E227A0(System.Int32)">
            <summary>
            _IS_PED_B* - _IS_PED_C*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x642720D8D69328B6(System.Int32,System.UInt32)">
            <summary>
            _SET_PED_M*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x99DF2639DA76C1DC(System.Int32,System.Int32,System.Int32)">
            <summary>
            _CAN_PED_*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._SET_PED_BRAWLING_STYLE(System.Int32,System.UInt32)">
            <summary>
            brawlingStyle:
            enum eBrawlingStyle : Hash
            {
            	BS_AI = 0x802C604D,
            	BS_AI_BARBRAWL = 0x4FF5F0C7,
            	BS_AI_DEFENSIVE = 0xD888F2FD,
            	BS_AI_MOONSHINE_BARBRAWL = 0xA01B433A,
            	BS_ALLIGATOR = 0x7A5548ED,
            	BS_ALLIGATOR_LARGE = 0x368EC7CB,
            	BS_ALLY = 0x69C76C14,
            	BS_ANIMAL = 0xD777C754,
            	BS_BADGER = 0x7E7C3F53,
            	BS_BEAR = 0x0BC66E35,
            	BS_BEAVER = 0x4E313783,
            	BS_BOAR = 0x176A5831,
            	BS_BOUNTY_HUNTER = 0x3900654C,
            	BS_BRUISER = 0x4514DB61,
            	BS_BULL = 0x4E50C5D2,
            	BS_COUGAR = 0x9DAA7CCB,
            	BS_COW = 0xB0E91295,
            	BS_COYOTE = 0xA448EB69,
            	BS_DEER = 0xA781E6B3,
            	BS_DOG = 0x5A4155C4,
            	BS_ELK = 0x408697F0,
            	BS_FEMALE = 0x6A3BB2C2,
            	BS_FEMALE_STRONG = 0x4DAFDD84,
            	BS_GANGUP = 0xD0CECFF2,
            	BS_GOAT = 0x078E649F,
            	BS_HORSE = 0xF6B775F3,
            	BS_MICAH_FINALE = 0x1F0BB27A,
            	BS_MOOSE = 0x968917AB,
            	BS_MUSKRAT = 0x1EDC33AC,
            	BS_NO_MELEE = 0x25B5F931,
            	BS_PIG = 0x22EAD110,
            	BS_PLAYER = 0x78BAEF07,
            	BS_PLAYER_FINALE = 0xF9E77D2D,
            	BS_PLAYER_MOONSHINER = 0x687BF19F,
            	BS_PLAYER_WINTER1 = 0x3C6A802F,
            	BS_QUICK = 0xC4CABB1B,
            	BS_RACCOON = 0x505F8917,
            	BS_SHEEP = 0x6827CCCF,
            	BS_SNAKE = 0x82BEBC4B,
            	BS_TIMID = 0x431AEF77,
            	BS_WOLF = 0xA8F023D4
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x9D8DFE2DE9CB4DFC(System.Int32)">
            <summary>
            _RESET_PED_*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xC48AF420371C7407(System.Int32,System.UInt32)">
            <summary>
            _SET_PED_M*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._SET_PED_BEAT_MULTIPLIER(System.Int32,System.Single)">
            <summary>
            NET_FETCH_CLIENT_UPDATE_PED_FIGHT_PROFICIENCY: Changing parry multiplier for ped
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._SET_PED_ACTION_DISABLE_FLAG(System.Int32,System.Int32)">
            <summary>
            https://github.com/femga/rdr3_discoveries/tree/master/AI/COMBAT_ACTION_DISABLE_FLAGS
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x57F35552E771BE9D(System.Int32,System.Int32)">
            <summary>
            _SET_PED_M*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x7C10221CE718AA72(System.Int32,System.Int32)">
            <summary>
            _CLEAR_PED_M*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x0D3B1568917EBDA0(System.Int32,System.Int32)">
            <summary>
            _IS_PED_M*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._GET_PED_REMAINING_REVIVAL_TIME(System.Int32,System.Boolean)">
            <summary>
            normalized / non normalized
            0.0        / 1000.0         STARTED IN WRITHE STAGE
            1.0        / 0.0            END OF WRITHE, DEAD
            -1.0                        DEAD
            
            Returns some value from AI task 562 (unknown).
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x12EB4E31F092C9B3(System.Int32)">
            <summary>
            _GET_IS_PED_(BLEEDING_OUT?)*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._SET_PED_GRAPPLE_STYLE(System.Int32,System.UInt32)">
            <summary>
            Hashes: GS_DRAGGING, GS_FACE_TO_BACK, GS_FACE_TO_FACE, GS_FACE_TO_FACE_WALL, GS_MOUNTED
            </summary>
        </member>
        <!-- Badly formed XML comment ignored for member "M:RDR2.Native.PED.SET_PED_FLEE_ATTRIBUTES(System.Int32,System.Int32,System.Boolean)" -->
        <member name="M:RDR2.Native.PED._0xC2722B252C79E641(System.Int32,System.UInt64,System.UInt64,System.Boolean)">
            <summary>
            _FORCE_PED_*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x2DC0E8DCBD3546E9(System.Int32)">
            <summary>
            _IS_PED_D*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._ADD_SCENARIO_TRANSITION(System.Int32)">
            <summary>
            Forces transition now, called together with 0xD65FDC686A031C83
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._GIVE_PED_HASH_COMMAND(System.Int32,System.UInt32,System.Single)">
            <summary>
            Ped Command Hash are special commands, that can be activated to change conditional anim variations or trigger transitions between conditional anims.
            https://github.com/femga/rdr3_discoveries/blob/master/animations/scenarios
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x600BBDD29820370C(System.Int32)">
            <summary>
            Not implemented.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.IS_TRACKED_PED_VISIBLE(System.Int32)">
            <summary>
            Returns whether or not a ped is visible within your FOV, not this check auto's to false after a certain distance.
            Target needs to be tracked first, won't work otherwise.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._IS_METAPED_FISH(System.Int32)">
            <summary>
            Returns true if given ped is a fish.
            _IS_ME* - _IS_MO*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.CAN_PED_SEE_ENTITY(System.Int32,System.Int32,System.Boolean,System.Boolean)">
            <summary>
            Returns:
            0 - CTR_CANNOT_TARGET
            1 - CTR_CAN_TARGET
            2 - CTR_NOT_SURE_YET
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x0EA9EACBA3B01601(System.Int32,System.Int32,System.Boolean)">
            <summary>
            _CAN_PED_B* - _CAN_PED_I*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.GET_PED_BONE_INDEX(System.Int32,System.Int32)">
            <summary>
            no bone = -1
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xC5303F460A40D21D(System.Int32,System.Int32)">
            <summary>
            Returns boneIndex
            _GET_PED_N* - _GET_PED_R*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xE29D8CD66553DBAA(System.Int32)">
            <summary>
            _SET_PED_R* -_SET_PED_S*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._SET_PED_DIRT_CLEANED(System.Int32,System.Single,System.Int32,System.Boolean,System.Boolean)">
            <summary>
            Params: ped, 0f, -1, true, true in R* MP Scripts
            _SET_PED_DE* - _SET_PED_F*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xA2116C1E4ED85C24(System.Int32,System.Boolean)">
            <summary>
            _SET_PED_*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.FORCE_PED_MOTION_STATE(System.Int32,System.UInt32,System.Boolean,System.Int32,System.Boolean)">
            <summary>
            motionStateHash: https://github.com/Halen84/RDR3-Native-Flags-And-Enums/tree/main/FORCE_PED_MOTION_STATE
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.GET_PED_CURRENT_MOVE_BLEND_RATIO(System.Int32,System.Single*,System.Single*)">
            <summary>
            Old name: _GET_PED_CURRENT_MOVEMENT_SPEED
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x46BF2A810679D6E6(System.Int32,System.Single)">
            <summary>
            Returns vehicle (desired) speed
            _COMPUTE_(VEHICLE_SPEED_USING_BLEND_RATIO?)*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xCA95924C893A0C91(System.Int32,System.Single)">
            <summary>
            Returns vehicle speed
            _COMPUTE_*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._HORSE_AGITATE(System.Int32,System.Boolean)">
            <summary>
            _H* - _I*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.SET_PED_MOVE_RATE_OVERRIDE(System.Int32,System.Single)">
            <summary>
            Min: 0.0f
            Max: 1.15f
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.SET_PED_CAPSULE(System.Int32,System.Single)">
            <summary>
```
