<!-- ScriptHookRDRNetAPI lines 4501-4750 of 30005 -->
```csharp
            	INVENTORY_MP_MISSION,
            	INVENTORY_SECOND_SCREEN,
            	INVENTORY_SP_BACKUP,
            	INVENTORY_SP_SNAPSHOT,
            	INVENTORY_0xDE2AE452,
            	INVENTORY_0x399D9B3A,
            	INVENTORY_0x4BD43FA7,
            	INVENTORY_0x9529D251,
            	INVENTORY_0xA75776AC,
            	INVENTORY_MAX_ID = 11,
            	INVENTORY_IDS_COUNT
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.INVENTORY._0x112BCA290D2EB53C(System.Int32,System.UInt32,System.Int32*,System.Int32*,System.Int32*,System.Int32*,System.Int32*,System.Int32*)">
            <summary>
            Only used in R* SP Scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.INVENTORY._0x46743BBFEDBC859E(System.Int32,System.UInt32,System.Boolean)">
            <summary>
            eInventoryItem: CLOTHING_FANCY_SUIT, CLOTHING_GUNSLINGER_OUTFIT, etc.
            Only used in R* SP Scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.INVENTORY._INVENTORY_SET_INVENTORY_ITEM_IN_USE(System.Int32,System.UInt64*,System.Boolean)">
            <summary>
            Only works on CClothingItem
            </summary>
        </member>
        <member name="M:RDR2.Native.INVENTORY._INVENTORY_SET_INVENTORY_ITEM_HIDDEN(System.Int32,System.UInt64*,System.Boolean)">
            <summary>
            Used with CClothingItem
            </summary>
        </member>
        <member name="M:RDR2.Native.INVENTORY._INVENTORY_SET_INVENTORY_ITEM_HIDDEN_2(System.Int32,System.UInt64*,System.Boolean)">
            <summary>
            Used with CSatchelItem, R* Script usage: fisihing_core
            </summary>
        </member>
        <member name="M:RDR2.Native.INVENTORY._INVENTORY_IS_INVENTORY_ITEM_FLAG_ENABLED(System.UInt32,System.Int32)">
            <summary>
            flag: https://github.com/Halen84/RDR3-Native-Flags-And-Enums/tree/main/ItemDatabaseItemFlags
            2097152 (is item read?), 8388608 (is item sent/received/mailable?), 16777216 (is item consumable?)
            </summary>
        </member>
        <member name="M:RDR2.Native.INVENTORY._INVENTORY_GET_IS_INVENTORY_ITEM_SOUND_VALID(System.UInt32,System.Int32)">
            <summary>
            eSoundType:
            SI_SFX_INVALID = -1,
            SI_SFX_PURCHASE,
            SI_SFX_SELL,
            SI_SFX_USE
            </summary>
        </member>
        <member name="M:RDR2.Native.INVENTORY._INVENTORY_GET_INVENTORY_ITEM_SOUND(System.UInt32,System.Int32)">
            <summary>
            eSoundType
            Returns item Hash to be used with _IS_SCRIPTED_AUDIO_CUSTOM and _PLAY_SOUND_FROM_ITEM (p0)
            </summary>
        </member>
        <member name="M:RDR2.Native.INVENTORY._INVENTORY_GET_INVENTORY_ITEM_WEAPON_COPY_ID(System.Int32,System.UInt64*)">
            <summary>
            Returns CopyID
            </summary>
        </member>
        <member name="M:RDR2.Native.INVENTORY._INVENTORY_ARE_LOCAL_CHANGES_ALLOWED(System.Int32)">
            <summary>
            inventoryId: see _INVENTORY_GET_PED_INVENTORY_ID
            </summary>
        </member>
        <member name="M:RDR2.Native.INVENTORY._0x5D6182F3BCE1333B(System.Int32,System.UInt32)">
            <summary>
            removeReason: REMOVE_REASON_DEFAULT (eRemoveItemReason)
            Example: INVENTORY::_0x5D6182F3BCE1333B(1, joaat("REMOVE_REASON_DEFAULT")); -> clears weapon wheel
            Only used in R* SP Scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.INVENTORY._INVENTORY_ADD_ITEM_WITH_GUID(System.Int32,System.UInt64*,System.UInt64*,System.UInt32,System.UInt32,System.Int32,System.UInt32)">
            <summary>
            inventoryItemSlotHash: https://pastebin.com/P6fyr3vr
            </summary>
        </member>
        <member name="M:RDR2.Native.INVENTORY._INVENTORY_UPDATE_INVENTORY_ITEM(System.Int32,System.UInt64*,System.UInt64*,System.Int32)">
            <summary>
            Getter: _INVENTORY_GET_FULL_INVENTORY_ITEM_DATA
            </summary>
        </member>
        <member name="M:RDR2.Native.INVENTORY._INVENTORY_MOVE_INVENTORY_ITEM(System.Int32,System.UInt64*,System.UInt64*,System.UInt32,System.Int32,System.UInt64*)">
            <summary>
            guid1: old parent GUID
            guid2: new parent GUID
            guid3: new item GUID (out param)
            </summary>
        </member>
        <member name="M:RDR2.Native.INVENTORY._INVENTORY_CREATE_ITEM_COLLECTION(System.Int32,System.String,System.UInt32,System.Int32*)">
            <summary>
            filterName (collections): "ALL", "ALL WEAPONS", "ALL HORSES", "ALL COACHES"
            slotId: -1591664384
            p3: outCollectionSize (?)
            Returns collectionId
            </summary>
        </member>
        <!-- Badly formed XML comment ignored for member "M:RDR2.Native.INVENTORY._INVENTORY_GET_ITEM_FROM_COLLECTION_INDEX(System.Int32,System.Int32,System.UInt64*)" -->
        <member name="M:RDR2.Native.INVENTORY._INVENTORY_RELEASE_ITEM_COLLECTION(System.Int32)">
            <summary>
            Max num of collections is 5, so release your unused ones.
            </summary>
        </member>
        <member name="M:RDR2.Native.INVENTORY._0xD08685BA892DBFAB(System.Int32,System.UInt64*,System.Int32*,System.Int32*)">
            <summary>
            Params: p3 returns an int between 0 and 20 (?)
            Only used in R* SP Scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.INVENTORY._0x0349404A22736740(System.Boolean,System.Int32,System.UInt64*)">
            <summary>
            Params: p0 is only 0 or 1
            Only used in R* SP Scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.INVENTORY._INVENTORY_SET_INVENTORY_ITEM_WEATHER_EFFECTIVENESS(System.Int32,System.UInt64*,System.Int32)">
            <summary>
            OWE_INVALID = -1,
            OWE_GOOD_IN_HOT
            OWE_GOOD_IN_NONE
            OWE_GOOD_IN_COLD
            OWE_GOOD_IN_ALL
            </summary>
        </member>
        <member name="M:RDR2.Native.INVENTORY._INVENTORY_DISABLE_ITEM(System.Int32,System.UInt32,System.UInt32)">
            <summary>
            Example: (1, WEAPON_REVOLVER_CATTLEMAN, 0) - disables cattleman revolver on weapon wheel
            </summary>
        </member>
        <member name="M:RDR2.Native.INVENTORY._INVENTORY_IS_INVENTORY_ITEM_EQUIPPED(System.Int32,System.UInt32,System.Boolean)">
            <summary>
            Alternative Name: _INVENTORY_IS_ITEM_DISABLED
            </summary>
        </member>
        <member name="M:RDR2.Native.INVENTORY._GET_DEFAULT_ITEM_SLOT_INFO(System.UInt32,System.UInt32)">
            <summary>
            p1: WARDROBE, KIT_CAMP, CHARACTER, KIT_MOONSHINER_PROPERTY
            Returns slot hash
            </summary>
        </member>
        <member name="M:RDR2.Native.INVENTORY._0x9AC53CB6907B4428(System.UInt32,System.UInt64*,System.UInt64*)">
            <summary>
            p1 (out) and p2 (in) are both script arrays?
            item: can be a component item, see SHOP_CATALOG_BUILD_LIST_OF_WEAPON_COMPONENTS
            </summary>
        </member>
        <member name="M:RDR2.Native.INVENTORY._0x9D21B185ABC2DBC4(System.UInt64,System.String,System.Boolean,System.Boolean)">
            <summary>
            p0 is the return value of DATABINDING::_DATABINDING_ADD_DATA_CONTAINER_FROM_PATH
            p2, p3: 0
            Returns databindingEntryId
            </summary>
        </member>
        <member name="M:RDR2.Native.INVENTORY._0x75CFAC49301E134F(System.UInt32,System.Boolean,System.Boolean)">
            <summary>
            p1, p2: 0
            </summary>
        </member>
        <member name="M:RDR2.Native.INVENTORY._0x9D21B185ABC2DBC5(System.UInt64,System.String,System.Int32,System.Int32)">
            <summary>
            p2: 0
            p3: -1
            Returns databindingEntryId to be used with 0x75CFAC49301E134E (p0)
            </summary>
        </member>
        <member name="M:RDR2.Native.INVENTORY._0x75CFAC49301E134E(System.UInt64,System.Boolean,System.Int32)">
            <summary>
            p0: value returned by 0x9D21B185ABC2DBC5
            </summary>
        </member>
        <member name="M:RDR2.Native.INVENTORY._0x46DB71883EE9D5AF(System.UInt64,System.String,System.UInt64*,System.Int32)">
            <summary>
            Returns databindingEntryId to be used with 0x951847CEF3D829FF (p0)
            </summary>
        </member>
        <member name="M:RDR2.Native.INVENTORY._0x951847CEF3D829FF(System.UInt64,System.UInt64*,System.Int32)">
            <summary>
            p0: value returned by 0x46DB71883EE9D5AF
            </summary>
        </member>
        <member name="M:RDR2.Native.INVENTORY._0x6862E4D93F64CF01(System.Int32,System.UInt64*,System.UInt32,System.UInt64*)">
            <summary>
            Only used in R* SP Scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.INVENTORY._INVENTORY_ENABLE_MISSION_INVENTORY(System.Boolean)">
            <summary>
            This native has no functionality.
            </summary>
        </member>
        <member name="M:RDR2.Native.INVENTORY._0xE36D4A38D28D9CFB(System.Boolean)">
            <summary>
            Only used in R* SP Scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.INVENTORY._0x9B4E793B1CB6550A">
            <summary>
            Used in function SET_SHOP_BEING_ROBBED and many other shop related scripts and functions.
            INVENTORY_A*
            </summary>
        </member>
        <member name="M:RDR2.Native.INVENTORY._INVENTORY_DISABLE_WEAPONS(System.Int32,System.UInt64)">
            <summary>
            Params: p1 = 0
            </summary>
        </member>
        <member name="M:RDR2.Native.INVENTORY._0xE1F45A67A9F0DCBC(System.Int32)">
            <summary>
            Only used in R* SP Scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.INVENTORY._0x6968CE7AC32F6788(System.Int32)">
            <summary>
            Only used in R* SP Scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.INVENTORY._INVENTORY_CREATE_ITEM_COLLECTION_2(System.Int32*)">
            <summary>
            Returns collectionId
            </summary>
        </member>
        <member name="M:RDR2.Native.INVENTORY._INVENTORY_CREATE_SORTED_COLLECTION(System.Int32,System.Int32,System.Int32*)">
            <summary>
            p1: 32
            Returns collectionId
            </summary>
        </member>
        <member name="M:RDR2.Native.ITEMDATABASE.ITEMDATABASE_FILLOUT_ITEM_INFO(System.UInt32,System.UInt64*)">
            <summary>
            p0 can be a weapon hash, component item
            p1 is a struct containing WEAPON_MOD and WEAPON_DECORATION
            </summary>
        </member>
        <member name="M:RDR2.Native.ITEMDATABASE._0x74C3B1093728D263(System.UInt64,System.UInt64)">
            <summary>
            _ITEMDATABASE_FILLOUT_(A)* - _ITEMDATABASE_FILLOUT_(B)*
            </summary>
        </member>
        <member name="M:RDR2.Native.ITEMDATABASE._0xDEE7B3C76ED664BE(System.UInt32,System.UInt32)">
            <summary>
            Returns fillOutIndex (?)
            _ITEMDATABASE_GET_(A)* - _ITEMDATABASE_GET_(B)*
            </summary>
        </member>
```
