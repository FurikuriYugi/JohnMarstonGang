<!-- ScriptHookRDRNetAPI lines 12501-12750 of 30005 -->
```csharp
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._GET_DEFAULT_UNARMED_WEAPON_HASH(System.Int32)">
            <summary>
            Returns the ped's default unarmed weapon hash as defined in CPedModelInfo (DefaultUnarmedWeapon).
            Falls back to WEAPON_UNARMED if the ped doesn't have a valid model info pointer, or 0 if the ped doesn't exist.
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._SET_AMMO_IN_TURRET(System.Int32,System.UInt32,System.Int32)">
            <summary>
            turretHash: WEAPON_TURRET_MAXIUM, WEAPON_TURRET_GATLING, WEAPON_TURRET_CANNON, WEAPON_TURRET_REVOLVING_CANNON
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._0x2387D6E9C6B478AA(System.Int32)">
            <summary>
            Returns true if ped is holstering/unholstering a weapon
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._GET_PED_WEAPON_IN_SLOT(System.Int32,System.UInt32)">
            <summary>
            slotHash is usually just the weaponHash name, but WEAPON_* is replaced with SLOT_*
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON.GIVE_DELAYED_WEAPON_TO_PED(System.Int32,System.UInt32,System.Int32,System.Boolean,System.UInt32)">
            <summary>
            addReason: see _ADD_AMMO_TO_PED
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON.REMOVE_WEAPON_FROM_PED(System.Int32,System.UInt32,System.Boolean,System.UInt32)">
            <summary>
            removeReason:
            enum eRemoveItemReason : Hash
            {
            	REMOVE_REASON_CLIENT_PURGED = 0x4A4E94DC,
            	REMOVE_REASON_COALESCE = 0x2ABE393E,
            	REMOVE_REASON_DEBUG = 0xA07362E6,
            	REMOVE_REASON_DEFAULT = 0xF77DE93D,
            	REMOVE_REASON_DELETE_CHARACTER = 0x20AFBDE9,
            	REMOVE_REASON_DROPPED = 0xEC7FB5D5,
            	REMOVE_REASON_DUPLICATE = 0x19047132,
            	REMOVE_REASON_GIFTED_INCORRECTLY = 0x9C4E3829,
            	REMOVE_REASON_GIVEN = 0xAD5377D4,
            	REMOVE_REASON_INSUFFICIENT_INVENTORY = 0x518D1AAE,
            	REMOVE_REASON_ITEM_DOES_NOT_EXIST = 0xEAD5D889,
            	REMOVE_REASON_LOADOUT = 0x1B94E3BA,
            	REMOVE_REASON_SET_AMOUNT = 0x19D5CFA5,
            	REMOVE_REASON_SOLD = 0x76C4B482,
            	REMOVE_REASON_USED = 0x2188E0A3,
            	REMOVE_REASON_USE_FAILED = 0x671F9EAD
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON.HIDE_PED_WEAPON_FOR_SCRIPTED_CUTSCENE(System.Int32,System.Boolean)">
            <summary>
            Hides the ped's weapon during a cutscene.
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._SET_PED_WEAPON_ATTACH_POINT_VISIBILITY(System.Int32,System.Int32,System.Boolean)">
            <summary>
            attachPoint: see SET_CURRENT_PED_WEAPON
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._0x2EBF70E1D8C06683(System.Int32,System.UInt32)">
            <summary>
            _SET_A* - _SET_B*
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._0x63B83A526329AFBC(System.UInt64)">
            <summary>
            Only used in R* Script fme_escaped_convicts, p0 = 0
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON.MAKE_PED_DROP_WEAPON(System.Int32,System.Boolean,System.Int32,System.Boolean,System.Boolean)">
            <summary>
            Old name: _DROP_CURRENT_PED_WEAPON
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._GET_PED_CURRENT_HELD_WEAPON(System.Int32)">
            <summary>
            Returns eCurrentHeldWeapon
            _GET_R* - _GET_T*
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._GET_WEAPON_NAME(System.UInt32)">
            <summary>
            Returns "WNS_INVALID" if the weapon is invalid/doesn't exist.
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._SEND_WEAPON_TO_INVENTORY(System.Int32,System.UInt32)">
            <summary>
            Appears to just send specified weapon to your horse holster without having to be close
            However, the weapon is not visible on the horse holster, but you can reach the weapon on the weapon wheel
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._0x14FF0C2545527F9B(System.Int32,System.UInt32,System.Int32)">
            <summary>
            Puts the gun visibly in your horse's holster without having to be close to the horse. Use 0xE9BD19F8121ADE3E before using this native
            _A* or _B*
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._GET_WEAPON_OBJECT_FROM_PED(System.Int32,System.Boolean)">
            <summary>
            Detaches the weapon from the ped and actually removes the ped's weapon
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._GET_PED_WEAPON_OBJECT(System.Int32,System.Boolean)">
            <summary>
            _GET_M* - _GET_PED_A*
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._GIVE_WEAPON_COMPONENT_TO_ENTITY(System.Int32,System.UInt32,System.UInt32,System.Boolean)">
            <summary>
            entity can be a ped or weapon object.
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._GET_WEAPON_GUN_SPINNING_WEAPON_EMOTE_TRICK_TYPE_HASH(System.UInt32,System.Int32)">
            <summary>
            Returns iSpinHash
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._SET_ACTIVE_GUN_SPINNING_EQUIP_KIT_EMOTE_TWIRL(System.Int32,System.UInt32)">
            <summary>
            emote hashes: KIT_EMOTE_TWIRL_GUN, KIT_EMOTE_TWIRL_GUN_LEFT_HOLSTER, KIT_EMOTE_TWIRL_GUN_DUAL, 0 (to unequip)
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._GET_PED_GUN_SPINNING_EQUIPPED_KIT_EMOTE_TWIRL(System.Int32)">
            <summary>
            Returns emote Hash
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._SET_ACTIVE_GUN_SPINNING_KIT_EMOTE_TWIRL(System.Int32,System.Int32,System.UInt32)">
            <summary>
            spinHash can be -1, 0 to disable
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._GET_PED_GUN_SPINNING_HASH_FROM_WEAPON_EMOTE_VARIATION(System.Int32,System.Int32)">
            <summary>
            Returns iSpinHash / iVariationSpin
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._GET_WEAPON_EMOTE_VARIATION(System.Int32,System.Int32)">
            <summary>
            Returns weaponEmoteVariation
            
            WEAPON_EMOTE_VARIATION_INVALID = -2,
            WEAPON_EMOTE_VARIATION_BASE,
            WEAPON_EMOTE_VARIATION_A,
            WEAPON_EMOTE_VARIATION_B,
            WEAPON_EMOTE_VARIATION_C,
            WEAPON_EMOTE_VARIATION_D,
            WEAPON_EMOTE_VARIATION_PREVIEW,
            WEAPON_EMOTE_NUM_VARIATIONS
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._SET_GUN_SPINNING_INVENTORY_SLOT_ID_ACTIVATE(System.Int32,System.Int32)">
            <summary>
            _STOP_* - _TEST_*
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._GET_CORRECT_KIT_EMOTE_TWIRL_GUN(System.Int32,System.UInt64*)">
            <summary>
            _GET_BEST_* - _GET_CLOSEST_*
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._GET_DEFAULT_PED_WEAPON_COLLECTION(System.UInt32)">
            <summary>
            Returns weaponCollection Hash
            Example: RE_POLICECHASE_MALES_01: Carbine Repeater + Knife, LO_AGRO_PED
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._0x9EEFD670F10656D7(System.UInt32,System.UInt32)">
            <summary>
            Returns weaponHash
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON.IS_WEAPON_A_GUN(System.UInt32)">
            <summary>
            Returns true if CWeaponInfoFlags::Flags::Gun is set.
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._GET_WEAPON_DAMAGE(System.Int32)">
            <summary>
            Related to weapon visual damage, not actual damage.
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._SET_WEAPON_DAMAGE(System.Int32,System.Single,System.Boolean)">
            <summary>
            Related to weapon visual damage, not actual damage.
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._SET_WEAPON_LEVEL_THRESHOLD(System.Int32,System.Single)">
            <summary>
            every other level will have the max value of (brokeLevel - threshold)
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON.GET_WEAPON_DEGRADATION(System.Int32)">
            <summary>
            0.0: good condition, 1.0: poor condition
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON.GET_WEAPON_PERMANENT_DEGRADATION(System.Int32)">
            <summary>
            Related to rust of weapons
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._GET_CURRENT_PED_WEAPON_AMMO_TYPE(System.Int32,System.Int32)">
            <summary>
            Returns ammoHash from weaponObject (Returned by 0x6CA484C9A7377E4F)
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._GET_CURRENT_AMMO_TYPE_FROM_GUID(System.Int32,System.UInt64*)">
            <summary>
            Returns ammoHash
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._GET_WEAPON_ATTACH_POINT(System.Int32,System.Int32)">
            <summary>
            Returns WeaponAttachPoint
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON.SET_CURRENT_PED_WEAPON_BY_GUID(System.Int32,System.UInt64*,System.Boolean,System.Boolean,System.Boolean,System.Boolean)">
            <summary>
            Equips a weapon from a weaponItem, similar to GIVE_WEAPON_TO_PED
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._GET_PLAYER_PED_QUICK_SWAP_WEAPON_BY_GUID(System.Int32,System.UInt64*,System.UInt64*)">
            <summary>
            Outputs cached guids
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._0x0DE0944ECCB3DF5D(System.UInt64)">
            <summary>
            _GET_D* - _GET_L*
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._0x46D42883E873C1D7(System.Int32)">
            <summary>
            _GET_NUM_*
            </summary>
        </member>
        <member name="M:RDR2.Native.WEAPON._0x65DC4AC5B96614CB(System.UInt32)">
            <summary>
            Returns attachPoint
            </summary>
        </member>
        <!-- Badly formed XML comment ignored for member "M:RDR2.Native.ZONE._GET_MAP_ZONE_AT_COORDS(System.Single,System.Single,System.Single,System.Int32)" -->
        <member name="M:RDR2.Native.ZONE._GET_WATER_MAP_ZONE_AT_COORDS(System.Single,System.Single,System.Single)">
            <summary>
            Returns the zone's name hash if its type matches one of the following:
            - LAKE
```
