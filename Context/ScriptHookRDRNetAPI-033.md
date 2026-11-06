<!-- ScriptHookRDRNetAPI lines 8001-8250 of 30005 -->
```csharp
            murfree
            old_female
            primate
            rally
            waiter
            war_veteran
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._CLEAR_PED_DESIRED_LOCO_FOR_MODEL(System.Int32)">
            <summary>
            Clears locomotion archetype
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._SET_PED_DESIRED_LOCO_MOTION_TYPE(System.Int32,System.String)">
            <summary>
            Sets peds motion type
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._REQUEST_PED_GETUP_ANIMATION(System.Int32,System.String)">
            <summary>
            Known get up animation types: REAR, FRONT
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x16F798A05BB9E3B5(System.Int32)">
            <summary>
            _PED_COWER_M*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._PED_EMOTIONAL_PRESET_LOCO_MOTION(System.Int32,System.String,System.Int32,System.Int32,System.Int32)">
            <summary>
            target: 0 affects everyone
            duration: -1 indefinite
            flag: always 4 in R* Scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x32CCAD8A981B53D3(System.Int32)">
            <summary>
            _STOP_(?)*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._SET_PED_DRUNKNESS(System.Int32,System.Boolean,System.Single)">
            <summary>
            SOBER = 0.0f, SLIGHTLY_DRUNK = 0.25f, MODERATELY_DRUNK = 0.5f, VERY_DRUNK = 1.0f
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._GET_PED_DRUNKNESS(System.Int32)">
            <summary>
            Returns ped drunk level
            _H* or _I*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xC17A94CC8FC3C61A(System.Int32,System.Int32,System.Single,System.Single,System.Single)">
            <summary>
            _SET_PED_P* - _SET_PED_R*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x134775B093AD5C38(System.Int32)">
            <summary>
            _GET_PED_M*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._GET_PED_MODEL_SIZE_FROM_HASH(System.UInt32)">
            <summary>
            PS_SMALL = 0,
            PS_MEDIUM,
            PS_MEDIUM_LARGE,
            PS_LARGE,
            PS_EXTRA_LARGE
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.REGISTER_HATED_TARGETS_AROUND_PED(System.Int32,System.Single)">
            <summary>
            Based on TASK_COMBAT_HATED_TARGETS_AROUND_PED, the parameters are likely similar (PedHandle, and area to attack in).
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.GET_CLOSEST_PED(System.Single,System.Single,System.Single,System.Single,System.Boolean,System.Boolean,System.Int32*,System.Boolean,System.Boolean,System.Boolean,System.Int32)">
            <summary>
            Gets the closest ped in a radius.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.SET_PED_TO_RAGDOLL(System.Int32,System.Int32,System.Int32,System.Int32,System.Boolean,System.Boolean,System.String)">
            <summary>
            Params: p6 = DraggedByCart in R* SP Script marston8
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.SET_PED_RAGDOLL_ON_COLLISION(System.Int32,System.Boolean,System.Boolean)">
            <summary>
            Causes Ped to ragdoll on collision with any object (e.g Running into trashcan). If applied to player you will sometimes trip on the sidewalk.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.IS_PED_RAGDOLL(System.Int32)">
            <summary>
            If the ped handle passed through the parenthesis is in a ragdoll state this will return true.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x8CB2553C559102C1(System.Int32,System.Int32,System.Boolean)">
            <summary>
            _SET_PED_T* - SET_PED_U*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xFD3C31A2E45671E7(System.Int32,System.Int32)">
            <summary>
            _DISABLE_A* - _DISABLE_C*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x3AEC4A410ECAF30D(System.Int32)">
            <summary>
            _IS_PED_R*
            </summary>
        </member>
        <!-- Badly formed XML comment ignored for member "M:RDR2.Native.PED.SET_RAGDOLL_BLOCKING_FLAGS(System.Int32,System.Int32)" -->
        <member name="M:RDR2.Native.PED.CLEAR_RAGDOLL_BLOCKING_FLAGS(System.Int32,System.Int32)">
            <summary>
            flags: see SET_RAGDOLL_BLOCKING_FLAGS
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._SET_PED_DEFENSIVE_AREA_TO_ANGLED_AREA(System.Int32,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.UInt64,System.Boolean,System.Boolean,System.Int32,System.Boolean)">
            <summary>
            _SET_PED_(A?)*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.REMOVE_PED_DEFENSIVE_AREA(System.Int32,System.Boolean)">
            <summary>
            Ped will no longer get angry when you stay near him.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x4EC4EA2F72B36358(System.Int32,System.Boolean)">
            <summary>
            _SET_PED_A*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xCF0B19806473D324(System.Int32,System.Single,System.Single,System.Single)">
            <summary>
            _SET_PED_COMBAT_*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xB4B7C92FCE7347B7(System.Int32)">
            <summary>
            _RESET_PED_C*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.RESURRECT_PED(System.Int32)">
            <summary>
            This function will simply bring the dead ped back to life.
            
            Before calling this function, you may want to declare the position, where your Resurrected ped to be spawn at because theres a chance the ped will fall through the map
            
            Also, disabling any assigned task immediately helped in the number of scenarios, where If you want peds to perform certain decided tasks.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.SET_PED_NAME_DEBUG(System.Int32,System.String)">
            <summary>
            nullsub, doesn't do anything
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x7020839C7302D8AC(System.Int32)">
            <summary>
            _HAS_*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xE1AADD0055D76603(System.Int32,System.Int32,System.Int32,System.Int32,System.Single,System.Single,System.Single,System.Single,System.Boolean,System.Boolean,System.Int32)">
            <summary>
            _C*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x5A1A929C8B729B4A(System.Int32)">
            <summary>
            _C*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x97A38B65EBDA3D50(System.Int32,System.Boolean)">
            <summary>
            _SET_PED_D*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x06A10B4D7F50B0C3(System.Int32)">
            <summary>
            _GET_PED_D*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xFEA6126C34DF2532(System.Int32,System.Boolean)">
            <summary>
            METAPED_PLAYER_COMPONENTS_SET_META_TYPE_TO_BE_BYPASSED: Setting visibility
            _SET_M* - _SET_P*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xA967D6A8ED2D713B(System.Int32,System.Boolean)">
            <summary>
            _SET_PED_P* - _SET_PED_R*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x735662994E60A710(System.Int32,System.Boolean)">
            <summary>
            _SET_PED_F*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._SET_PED_ACTIVATE_WOUND_EFFECT(System.Int32,System.Int32,System.Int32,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single)">
            <summary>
            bloodFountainPressure: visible effect from 0.0 till 20.0
            yaw: visible effect from -3.0 till 3.0
            bloodFountainDirection: 1.0 left side, -1.0 right side
            bloodFountainPulse: from 0.1 (low) till 1.0 (fast)
            make blood fountain from your stomach: _SET_PED_ACTIVATE_WOUND_EFFECT(ped, unk, 2, 14411, 0.0, 0.1, 0.0, 0.0, 3.0, -1.0, 1.0)
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._UPDATE_PED_WOUND_EFFECT(System.Int32,System.Single)">
            <summary>
            Params: 0.0f to remove wound effects
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.APPLY_PED_DAMAGE_PACK(System.Int32,System.String,System.Single,System.Single)">
            <summary>
            https://github.com/femga/rdr3_discoveries/blob/master/peds_customization/ped_decals.lua
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xD8544F6260F5F01E(System.Int32,System.Int32)">
            <summary>
            METAPED_PLAYER_COMPONENTS_SET_META_TYPE_TO_BE_BYPASSED: Setting visibility
            p1 is mostly 10
            _CLEAR_PED_N* - _CLEAR_PED_W*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xEB8886E1065654CD(System.Int32,System.Int32,System.String,System.Single)">
            <summary>
            Washing player's face/hands now
            _FA* - _FI*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.CLEAR_PED_WETNESS(System.Int32)">
            <summary>
            It clears the wetness of the selected Ped/Player. Clothes have to be wet to notice the difference.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.SET_PED_WETNESS_HEIGHT(System.Int32,System.Single)">
            <summary>
            It adds the wetness level to the player clothing/outfit. As if player just got out from water surface.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xF9CFF5BB70E8A2CB(System.Int32,System.Single)">
            <summary>
            _SET_PED_WETNESS*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.SET_PED_WETNESS_ENABLED_THIS_FRAME(System.Int32)">
            <summary>
            combined with PED::SET_PED_WETNESS_HEIGHT(), this native makes the ped drenched in water up to the height specified in the other function
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xA7A806677F8DE138(System.Int32)">
            <summary>
```
