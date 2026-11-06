<!-- ScriptHookRDRNetAPI lines 12251-12500 of 30005 -->
```csharp
        <member name="M:RDR2.Native.VOLUME._0x128FC3A893BF853A(System.UInt64)">
            <summary>
            nullsub, doesn't do anything
            </summary>
        </member>
        <member name="M:RDR2.Native.VOLUME._GET_VOLUME_RELATIONSHIP(System.Int32)">
            <summary>
            Returns relationshipGroup Hash
            </summary>
        </member>
        <member name="M:RDR2.Native.WATER.DISABLE_WATER_LOOKUP">
            <summary>
            Must be called every frame to take full effect.
            </summary>
        </member>
        <member name="M:RDR2.Native.WATER.GET_WATER_HEIGHT(System.Single,System.Single,System.Single,System.Single*)">
            <summary>
            Checks against a global variable that is set by _SET_WORLD_WATER_TYPE. If that is set to one it will fail. Likely not the only issue but part of it.
            </summary>
        </member>
        <member name="M:RDR2.Native.WATER.TEST_PROBE_AGAINST_ALL_WATER(System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Int32,System.Single*)">
            <summary>
            enum eScriptWaterTestResult
            {
            	SCRIPT_WATER_TEST_RESULT_NONE,
            	SCRIPT_WATER_TEST_RESULT_WATER,
            	SCRIPT_WATER_TEST_RESULT_BLOCKED,
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.WATER.TEST_VERTICAL_PROBE_AGAINST_ALL_WATER(System.Single,System.Single,System.Single,System.Int32,System.Single*)">
            <summary>
            Checks against a global variable that is set by _SET_WORLD_WATER_TYPE. If it's set to 1 (Guarma) it will fail.
            
            See TEST_PROBE_AGAINST_ALL_WATER.
            </summary>
        </member>
        <member name="M:RDR2.Native.WATER.REMOVE_EXTRA_CALMING_QUAD(System.Int32)">
            <summary>
            Only used in rcm_crackpot1 R* Script: p0 = 0
            </summary>
        </member>
        <member name="M:RDR2.Native.WATER._0x09A1C7DFDCE54FBC(System.Int32)">
            <summary>
            Called together with REMOVE_EXTRA_CALMING_QUAD in rcm_crackpot1 R* Script: p0 = 0
            _REMOVE_*
            </summary>
        </member>
        <member name="M:RDR2.Native.WATER._0xF0FBF193F1F5C0EA(System.Int32)">
            <summary>
            Only used in fishing_core R* Script
            </summary>
        </member>
        <member name="M:RDR2.Native.WATER._0xA33F5069B0CB89B8">
            <summary>
            Only used in fishing_core R* Script
            </summary>
        </member>
        <member name="M:RDR2.Native.WATER._0xB34A6009A0DB80B8(System.Int32)">
            <summary>
            Used in bounty1, fanale3, sean1 R* Scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.WATER._SET_OCEAN_GUARMA_WATER_QUADRANT(System.Single,System.Single,System.Int32,System.Single,System.Single,System.Single,System.Single,System.Single,System.Int32)">
            <summary>
            Only used in R* Script guama1
            </summary>
        </member>
        <member name="M:RDR2.Native.WATER._0xC63540AEF8384769">
            <summary>
            Only used in guama1 / guama3 R* Script
            _REQUEST_* or _RESET_*
            </summary>
        </member>
        <member name="M:RDR2.Native.WATER._SET_WORLD_WATER_TYPE(System.Int32)">
            <summary>
            0 = World
            1 = Guarma
            </summary>
        </member>
        <member name="M:RDR2.Native.WATER._0x0DCEC6A92E497E17(System.Int32,System.Int32)">
            <summary>
            Only used in native_son1 R* Script: p1 = 1
            </summary>
        </member>
        <member name="M:RDR2.Native.WATER._0xE8126623008372AA">
            <summary>
            Only used in fussar1 / train_robbery2 R* Script
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._ADD_AMMO_TO_PED(System.Int32,System.UInt32,System.Int32,System.UInt32)">
            <summary>
            addReason:
            enum eAddItemReason : Hash
            {
            	ADD_REASON_AWARDS = 0xB784AD1E,
            	ADD_REASON_CREATE_CHARACTER = 0xE2C4FF71,
            	ADD_REASON_DEBUG = 0x5C05C64D,
            	ADD_REASON_DEFAULT = 0x2CD419DC,
            	ADD_REASON_GET_INVENTORY = 0xD8188685,
            	ADD_REASON_INCENTIVE = 0x8ADC2E95,
            	ADD_REASON_LOADOUT = 0xCA3454E6,
            	ADD_REASON_LOAD_SAVEGAME = 0x56212906,
            	ADD_REASON_LOOTED = 0xCA806A55,
            	ADD_REASON_MELEE = 0x7B9BDCE7,
            	ADD_REASON_MP_MISSION = 0xEC0E0194,
            	ADD_REASON_NOTIFICATION = 0xC56292D2,
            	ADD_REASON_PICKUP = 0x1A770E22,
            	ADD_REASON_PURCHASED = 0x4A6726C9,
            	ADD_REASON_SET_AMOUNT = 0x4504731E,
            	ADD_REASON_SYNCING = 0x8D4B4FF4,
            	ADD_REASON_USE_FAILED = 0xD385B670
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._ADD_AMMO_TO_PED_BY_TYPE(System.Int32,System.UInt32,System.Int32,System.UInt32)">
            <summary>
            addReason: see _ADD_AMMO_TO_PED
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._REMOVE_AMMO_FROM_PED(System.Int32,System.UInt32,System.Int32,System.UInt32)">
            <summary>
            removeReason must be REMOVE_REASON_USED, REMOVE_REASON_GIVEN, REMOVE_REASON_DROPPED or REMOVE_REASON_DEBUG, unless amount is -1
            
            removeReason: see REMOVE_WEAPON_FROM_PED
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._REMOVE_AMMO_FROM_PED_BY_TYPE(System.Int32,System.UInt32,System.Int32,System.UInt32)">
            <summary>
            removeReason must be REMOVE_REASON_USED, REMOVE_REASON_GIVEN, REMOVE_REASON_DROPPED or REMOVE_REASON_DEBUG, unless amount is -1
            
            removeReason: see REMOVE_WEAPON_FROM_PED
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._0x8A779706DA5CA3DD(System.Int32,System.Boolean,System.Int32)">
            <summary>
            Only used in R* SP Scripts native_son2, native_son3 and smuggler2
            Params: p2 = -1
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._0x9409C62504A8F9E9(System.Int32,System.Boolean)">
            <summary>
            Only used in R* SP Script guama3
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON.GET_PED_AMMO_TYPE_FROM_WEAPON(System.Int32,System.UInt32)">
            <summary>
            Returns the current ammo type of the specified ped's specified weapon.
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._GET_WEAPONTYPE_MODEL(System.UInt32)">
            <summary>
            Gets the model hash from the weapon hash.
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._GET_WEAPONTYPE_SLOT(System.UInt32)">
            <summary>
            Returns hash where WEAPON_ is replaced with SLOT_
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._0xF8204EF17410BF43(System.UInt32,System.Single,System.Single,System.UInt64)">
            <summary>
            Returns weaponHash
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._IS_WEAPON_KIT(System.UInt32)">
            <summary>
            Returns true when the weapon passed is either a lasso, the camera or the binoculars
            _IS_WEAPON_M* - _IS_WEAPON_P*
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._IS_WEAPON_KIT_2(System.UInt32)">
            <summary>
            Returns true when the weapon passed is either the fishingrod, a lasso, the camera or the binoculars
            _IS_WEAPON_M* - _IS_WEAPON_P*
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON.GIVE_WEAPON_TO_PED(System.Int32,System.UInt32,System.Int32,System.Boolean,System.Boolean,System.Int32,System.Boolean,System.Single,System.Single,System.UInt32,System.Boolean,System.Single,System.Boolean)">
            <summary>
            Gives the ped the weapon.
            List: https://github.com/femga/rdr3_discoveries/blob/master/weapons/weapons.lua
            
            Params: p7 is 0.5f, and p8 is 1.0f. p11 and p12 are both 0 in R* Scripts
            attachPoint: see SET_CURRENT_PED_WEAPON
            addReason: see _ADD_AMMO_TO_PED
            permanentDegradation: default 0.5, any higher than 0 it will automatically make the weapon worn, you can also adjust the value to change the weapons maximum cleanliness
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._HIDE_PED_WEAPONS(System.Int32,System.Int32,System.Boolean)">
            <summary>
            Unequip current weapon and set current weapon to WEAPON_UNARMED.
            p0 usually 2 in R* scripts. Doesn't seem to have any effect if changed....
            immediately: if true it will instantly switch to unarmed
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON.SET_CURRENT_PED_WEAPON(System.Int32,System.UInt32,System.Boolean,System.Int32,System.Boolean,System.Boolean)">
            <summary>
            attachPoint:
            enum eWeaponAttachPoint
            {
            	WEAPON_ATTACH_POINT_INVALID = -1,
            	WEAPON_ATTACH_POINT_HAND_PRIMARY = 0,
            	WEAPON_ATTACH_POINT_HAND_SECONDARY = 1,
            	WEAPON_ATTACH_POINT_PISTOL_R = 2,
            	MAX_HAND_WEAPON_ATTACH_POINTS = 2,
            	WEAPON_ATTACH_POINT_PISTOL_L = 3,
            	WEAPON_ATTACH_POINT_KNIFE = 4,
            	WEAPON_ATTACH_POINT_LASSO = 5,
            	WEAPON_ATTACH_POINT_THROWER = 6,
            	WEAPON_ATTACH_POINT_BOW = 7,
            	WEAPON_ATTACH_POINT_BOW_ALTERNATE = 8,
            	WEAPON_ATTACH_POINT_RIFLE = 9,
            	WEAPON_ATTACH_POINT_RIFLE_ALTERNATE = 10,
            	WEAPON_ATTACH_POINT_LANTERN = 11,
            	WEAPON_ATTACH_POINT_TEMP_LANTERN = 12,
            	WEAPON_ATTACH_POINT_MELEE = 13,
            	MAX_SYNCED_WEAPON_ATTACH_POINTS = 13,
            	WEAPON_ATTACH_POINT_HIP = 14,
            	WEAPON_ATTACH_POINT_BOOT = 15,
            	WEAPON_ATTACH_POINT_BACK = 16,
            	WEAPON_ATTACH_POINT_FRONT = 17,
            	WEAPON_ATTACH_POINT_SHOULDERSLING = 18,
            	WEAPON_ATTACH_POINT_LEFTBREAST = 19,
            	WEAPON_ATTACH_POINT_RIGHTBREAST = 20,
            	WEAPON_ATTACH_POINT_LEFTARMPIT = 21,
            	WEAPON_ATTACH_POINT_RIGHTARMPIT = 22,
            	WEAPON_ATTACH_POINT_LEFTARMPIT_RIFLE = 23,
            	WEAPON_ATTACH_POINT_SATCHEL = 24,
            	WEAPON_ATTACH_POINT_LEFTARMPIT_BOW = 25,
            	WEAPON_ATTACH_POINT_RIGHT_HAND_EXTRA = 26,
            	WEAPON_ATTACH_POINT_LEFT_HAND_EXTRA = 27,
            	WEAPON_ATTACH_POINT_RIGHT_HAND_AUX = 28,
            	MAX_WEAPON_ATTACH_POINTS = 29
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON.GET_CURRENT_PED_WEAPON(System.Int32,System.UInt32*,System.Boolean,System.Int32,System.Boolean)">
            <summary>
            attachPoint: see SET_CURRENT_PED_WEAPON
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON.GET_CURRENT_PED_WEAPON_ENTITY_INDEX(System.Int32,System.Int32)">
            <summary>
            Returns weaponObject, attachPoint: see SET_CURRENT_PED_WEAPON
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._GET_BEST_PED_WEAPON_IN_GROUP(System.Int32,System.UInt32,System.Boolean,System.Boolean)">
            <summary>
            If near your horse when called, weapons stored on your horse will be considered
            Returns weaponHash
```
