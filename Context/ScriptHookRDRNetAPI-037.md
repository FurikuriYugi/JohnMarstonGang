<!-- ScriptHookRDRNetAPI lines 9001-9250 of 30005 -->
```csharp
        <member name="M:RDR2.Native.PED._GET_NUM_COMPONENT_CATEGORIES_IN_PED(System.Int32)">
            <summary>
            Works similar to 0x90403E8107B60E81 (_GET_NUM_COMPONENTS_IN_PED) but is used to get category hashes instead
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._GET_PED_COMPONENT_CATEGORY_BY_INDEX(System.Int32,System.Int32)">
            <summary>
            Returns category hash that each ped component has. Hash examples: MASKS, HATS, HEADS, HORSE_MANES
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._UPDATE_PED_VARIATION(System.Int32,System.Boolean,System.Boolean,System.Boolean,System.Boolean,System.Boolean)">
            <summary>
            Update variation on ped, needed after first creation, or when component or texture/overlay is changed
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._EQUIP_META_PED_OUTFIT(System.Int32,System.UInt32)">
            <summary>
            Note: you have to update your ped's variation after calling (using 0xCC8CA3E88256E58F)
            
            Body Types:
            MPCREATOR_NEUTRAL
            MPCREATOR_SKINNY
            MPCREATOR_SKINNY_MUSCULAR
            MPCREATOR_HEAVY
            MPCREATOR_HEAVY_MUSCULAR
            
            eBodyWeightOutfit (pedattributes.ymt):
            -2045421226 (smallest)
            -1745814259
            -325933489
            -1065791927
            -844699484
            -1273449080
            927185840
            149872391
            399015098
            -644349862
            1745919061 (default)
            1004225511
            1278600348
            502499352
            -2093198664
            -1837436619
            1736416063
            2040610690
            -1173634986
            -867801909
            1960266524 (biggest)
            
            https://github.com/femga/rdr3_discoveries/blob/master/peds_customization/ped_outfits.lua
            
            Alt name: _EQUIP_META_PED_OUTFIT_COMPONENT
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x62FDF4E678E40CC6(System.Int32,System.UInt64)">
            <summary>
            Returns p1 value for 0x8E84119A23C16623
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x370A973252741AC4(System.Int32,System.Boolean)">
            <summary>
            _RESET_PED_*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._EQUIP_META_PED_OUTFIT_PRESET(System.Int32,System.Int32,System.Boolean)">
            <summary>
            Sets the outfit preset for the ped. The presetId is an index which determines its preset outfit. p2 is always false in the scripts.
            If p2 is true as player, then certain components like facial hair and hair will not be removed.
            Old name: _SET_PED_OUTFIT_PRESET
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._EQUIP_META_PED_OUTFIT_EXTRA(System.Int32,System.Int32,System.UInt64,System.UInt64)">
            <summary>
            Changes Multiplayer ped face and body type components, they can be stacked
            Params: p3 = 1
            Body shape for mp_male from 124 - 128, 110 - 115 for mp_female
            Face shape for mp_male from 110 - 123, 96 - 109 for mp_female
            Cloth type for mp_male from 0 - 109, 0 - 95 for mp_female
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._IS_META_PED_OUTFIT_EQUIPPED(System.Int32,System.UInt32)">
            <summary>
            Used in script function HORSE_IS_META_PED_OUTFIT_SADDLE_EQUIPPED
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x4EFC1F8FF1AD94DE(System.UInt64,System.UInt64,System.UInt64,System.UInt64,System.UInt64,System.UInt64)">
            <summary>
            Used in script function METAPED_CLOTHING__XML__APPLY_OUTFIT_TINTS_TO_PED
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._REQUEST_METAPED_PED(System.UInt32,System.Int32)">
            <summary>
            Returns requestId
            Params: p1 = 1 in R* Scripts (Used in SP only)
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._CREATE_METAPED_PED(System.Int32,System.Single,System.Single,System.Single,System.Single,System.Boolean,System.Boolean,System.Boolean,System.Boolean,System.Boolean)">
            <summary>
            Only used in SP scripts, for example odriscolls1: BOOLS: true, true, true, false, false
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._REQUEST_METAPED_OUTFIT(System.UInt32,System.UInt32)">
            <summary>
            https://github.com/femga/rdr3_discoveries/blob/master/clothes/metaped_outfits.lua
            Returns requestId, to be used with 0x74F512E29CB717E2
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x3FCBB5FCFD968698(System.UInt32,System.UInt32,System.UInt32,System.UInt32,System.UInt64)">
            <summary>
            Used for script function PROCESS_PLAYER_HAT_EVENT
            Returns requestId to be used with 0x13E7320C762F0477
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._REQUEST_METAPED_ASSET_BUNDLE(System.UInt32,System.Int32)">
            <summary>
            Returns requestId
            Params: p1 = 1 in R* Scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._CREATE_METAPED_ASSET(System.UInt32,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Boolean,System.Boolean,System.Boolean)">
            <summary>
            Creates prop from metaped asset bundle
            https://github.com/femga/rdr3_discoveries/blob/master/objects/metaped_asset_bundles_list.lua
            Creates a pickup-able metaped component. asset doesn't seems to be related to component hashes. Hash example : 0xD20354AB (https ://i.imgur.com/dzHkcDb.png)
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._CREATE_METAPED_OUTFIT_PED(System.Int32,System.Single,System.Single,System.Single,System.Single,System.Boolean,System.Boolean,System.Boolean,System.Boolean)">
            <summary>
            Creates metaped from ped outfit requestId. See _REQUEST_METAPED_OUTFIT
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._APPLY_PED_METAPED_OUTFIT(System.Int32,System.Int32,System.Boolean,System.Boolean)">
            <summary>
            https://github.com/femga/rdr3_discoveries/blob/master/clothes/metaped_outfits.lua
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._SET_METAPED_WEARINESS(System.Int32,System.Single)">
            <summary>
            Sets ped eye redness, weariness: 0.f to 1.f
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xF47D54B986F0A346(System.Int32,System.Int32)">
            <summary>
            Used in Script Function MOONSHINE_BAND_CLIENT_PATRON_UPDATE
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._GET_PELT_FROM_HORSE(System.Int32,System.Int32)">
            <summary>
            Returns peltId
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._CHANGE_PED_STAMINA(System.Int32,System.Single)">
            <summary>
            Alters entity's stamina by 'amount'. Can be negative (to drain stamina). float amount: -1000.0 - 1000.0
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._GET_PED_STAMINA_NORMALIZED(System.Int32)">
            <summary>
            Returns stamina normalizedValue / normalizedUnlockedMax
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x36513AFFC703C60D(System.UInt64)">
            <summary>
            _RESET_PED_*
            </summary>
        </member>
        <!-- Badly formed XML comment ignored for member "M:RDR2.Native.PED._RESTORE_PED_STAMINA(System.Int32,System.Single)" -->
        <member name="M:RDR2.Native.PED._0x5D4CD22A8C82A81A(System.Int32,System.Boolean)">
            <summary>
            Related to ped hat
            _SET_PED_LA* - _SET_PED_LE*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._CREATE_GRAVITY_WELL(System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Boolean)">
            <summary>
            Creates a handle to an instance of "CScriptResource_GravityWell", this system forces local ped to target specified position when moving, however player still can interrupt this.
            Can be useful to "point" player at some specific position.
            Only works while on-foot.
            
            _CREATE_[P-Z]
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._REMOVE_GRAVITY_WELL(System.Int32)">
            <summary>
            Removes gravity well by handle returned from 0x4F5EBE70081E5A20
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x7EE3A8660F38797E(System.Int32)">
            <summary>
            _IS_PED_H* - _IS_PED_I*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._REQUEST_TEXTURE(System.UInt32,System.UInt32,System.UInt32)">
            <summary>
            Creates a texture override data for ped and returns it's index.
            So you can replace any texture of any ped's component.
            Also, you can add overlays on it, such as aging, lipstick and more.
            Textures can be reused by multiple peds at once.
            You can keep only 32 textures at once(including other peds).
            
            https://github.com/femga/rdr3_discoveries/blob/master/clothes/change_overlays_script.lua
            materialHash: https://github.com/femga/rdr3_discoveries/blob/master/clothes/cloth_drawable_albedo_normal_material_TEMPORARY.lua
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._RELEASE_TEXTURE(System.Int32)">
            <summary>
            Removes a texture created by 0xC5E7204F322E49EB.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._UPDATE_PED_TEXTURE(System.Int32)">
            <summary>
            Should be called at least once for any new texture override.
            Otherwise component textures will be just black.
            Also needs to be called for updating any ped overlays to apply the changes.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._RESET_PED_TEXTURE(System.Int32)">
            <summary>
            Removes every texture layer but the base layer
            Clearing texture's data: setting params to default values, but keep overlays.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._ADD_TEXTURE_LAYER(System.Int32,System.UInt32,System.UInt32,System.UInt32,System.Int32,System.Single,System.Int32)">
            <summary>
            Creates ped overlay in texture override data and returns it's index.
            This index are used for further overlay editing.
            
            albedoHash: a hash of overlay's albedo texture
            colorType: a color type(from 0 to 2). 0 is used for overlays with RGB colors usually.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._SET_TEXTURE_LAYER_PALLETE(System.Int32,System.Int32,System.UInt32)">
            <summary>
            paletteHash: https://raw.githubusercontent.com/femga/rdr3_discoveries/master/clothes/cloth_color_palletes.lua
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._SET_TEXTURE_LAYER_TINT(System.Int32,System.Int32,System.Int32,System.Int32,System.Int32)">
            <summary>
            Seem color is not RGB or HSV
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._CLEAR_PED_TEXTURE(System.Int32)">
            <summary>
            Removes every texture layer
            Old Name: _RESET_PED_TEXTURE_2
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xC991EF46FE323867(System.Int32,System.UInt64)">
            <summary>
            Not implemented.
```
