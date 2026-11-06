<!-- ScriptHookRDRNetAPI lines 4001-4250 of 30005 -->
```csharp
        </member>
        <member name="M:RDR2.Native.GRAPHICS.IS_TVSHOW_CURRENTLY_PLAYING(System.UInt32)">
            <summary>
            Old name: _IS_TV_PLAYLIST_ITEM_PLAYING
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS.ENABLE_MOVIE_SUBTITLES(System.Boolean)">
            <summary>
            nullsub, doesn't do anything
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS.ANIMPOSTFX_PLAY(System.String)">
            <summary>
            https://github.com/femga/rdr3_discoveries/blob/master/graphics/animpostfx
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._0x26DD2FB0A88CC412(System.String,System.String,System.UInt64,System.UInt64)">
            <summary>
            effectName2, p2 and p3 are unused
            
            ANIMPOSTFX_*
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._ANIMPOSTFX_SET_STRENGTH(System.String,System.Single)">
            <summary>
            must be called after ANIMPOSTFX_PLAY, strength 0.0f - 1.0f
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._ANIMPOSTFX_SET_POTENCY(System.String,System.Int32,System.Single)">
            <summary>
            Health Core Effect Filter Potency: p1 = 1
            Stamina Core Effect Filter Potency: p1 = 2
            Multiple Core Effect Filter Potency: p1 = 3
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._0xB958D97A0DFAA0C2(System.String)">
            <summary>
            ANIMPOSTFX_*
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._0xA201A3D0AC087C37(System.String)">
            <summary>
            ANIMPOSTFX_*
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._0xFBF161FCFEC8589E(System.String,System.Int32,System.Boolean,System.Boolean*)">
            <summary>
            ANIMPOSTFX_*
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._ANIMPOSTFX_GET_STACKHASH(System.String)">
            <summary>
            Known effects: MP_Trans_SceneToPhoto
            MP_Trans_WinLose
            SpectateFilter
            MP_CharacterCreatorPhoto
            MP_Trans_PhotoToScene
            InterrogationHit
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._0x38D9D50F2085E9B3(System.UInt32)">
            <summary>
            ANIMPOSTFX_*
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._0xC76FC4C2FC5F4405(System.UInt32)">
            <summary>
            ANIMPOSTFX_*
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._0xE75CDDEBF618C8FF(System.UInt32)">
            <summary>
            ANIMPOSTFX_*
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._0x71845905BCCDE781(System.UInt32)">
            <summary>
            ANIMPOSTFX_*
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._0xFF584F097C17FA8F">
            <summary>
            Returns whether the 'killFX' setting is enabled.
            
            ANIMPOSTFX_*
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._0x3DA7A10583A4BEC0">
            <summary>
            ANIMPOSTFX_*
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._0xC37792A3F9C90771">
            <summary>
            Doesn't actually return anything.
            
            ANIMPOSTFX_*
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._0xC38B4952B728397A(System.Int32,System.Boolean)">
            <summary>
            Only used in guama1 R* SP Script while spawning the ship
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._0xFD05B1DDE83749FA(System.String)">
            <summary>
            R* Script spd_agnesdow1: p0 = SPD_AGNES_DOWD_01
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._0x402E1A61D2587FCD(System.UInt64,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single)">
            <summary>
            Only used in R* SP Script spd_agnesdown1
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._0x5C9C3A466B3296A8(System.UInt64)">
            <summary>
            Only used in R* SP Script spd_agnesdown1
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._SET_PROXY_INTERIOR_INDEX_ARTIFICIAL_LIGHTS_STATE(System.Int32,System.Boolean)">
            <summary>
            state: false disables artificial interior light sources for specific proxyInteriorIndex
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._GET_PROXY_INTERIOR_INDEX(System.Int32)">
            <summary>
            Returns proxyInteriorIndex
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._SET_CLOUD_POSITION(System.Single,System.Single,System.Single)">
            <summary>
            Only used in finale2, smuggler2, winter4
            _SET_CLOUD_A* - _SET_CLOUD_H*
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._0x94B261F1F35293E1(System.UInt64)">
            <summary>
            nullsub, doesn't do anything
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._0xAF4D239B8903FCBE">
            <summary>
            Used for script function RPG_GLOBAL_STATS__PRIVATE__DEACTIVATE_STAT_FLAG - Inspiration Aura unequip
            </summary>
        </member>
        <member name="M:RDR2.Native.GRAPHICS._SET_SNOW_COVERAGE_TYPE(System.Int32)">
            <summary>
            enum class eSnowCoverageType
            {
            	Primary,
            	Secondary,
            	Xmas,
            	XmasSecondary // since b1232
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.GANG._NETWORK_START_GANG(System.Boolean,System.Int32)">
            <summary>
            openStatus = true -> sets privacyType = 2 (PUBLIC_ADVERTISED)
            openStatus = false -> sets privacyType = 1 (INVITE_ONLY)
            
            campSize: NET_CAMP_SIZE_SMALLEST = 4, NET_CAMP_SIZE_LARGEST = 7
            </summary>
        </member>
        <member name="M:RDR2.Native.GANG._NETWORK_KICK_GANG_MEMBER(System.Int32,System.Int32)">
            <summary>
            banTimeSeconds is 120 in R* Scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.GANG._NETWORK_REQUEST_GANG_JOIN(System.UInt64)">
            <summary>
            Returns true if join succeeded, false if failed.
            </summary>
        </member>
        <member name="M:RDR2.Native.GANG._0xDA801F7F6A5278D3(System.Int32)">
            <summary>
            _NETWORK_GET_* or _NETWORK_IS_*
            </summary>
        </member>
        <member name="M:RDR2.Native.HUD._ENABLE_REDUCED_MENU_TIME_SCALE">
            <summary>
            Enables reduced time scale while menus such as weapon wheel and satchel are open.
            </summary>
        </member>
        <member name="M:RDR2.Native.HUD._DISABLE_REDUCED_MENU_TIME_SCALE">
            <summary>
            Disables reduced time scale while menus such as weapon wheel and satchel are open.
            </summary>
        </member>
        <member name="M:RDR2.Native.HUD._ENABLE_HUD_CONTEXT(System.UInt32)">
            <summary>
            https://github.com/femga/rdr3_discoveries/tree/master/graphics/HUD/hud_presets
            Old name: _HIDE_HUD_COMPONENT
            </summary>
        </member>
        <member name="M:RDR2.Native.HUD._DISABLE_HUD_CONTEXT(System.UInt32)">
            <summary>
            Old name: _DISPLAY_HUD_COMPONENT
            </summary>
        </member>
        <member name="M:RDR2.Native.HUD.BUSYSPINNER_OFF">
            <summary>
            Removes the loading prompt at the bottom right of the screen.
            </summary>
        </member>
        <member name="M:RDR2.Native.HUD.GET_CHARACTER_FROM_AUDIO_CONVERSATION_FILENAME(System.String,System.Int32,System.Int32)">
            <summary>
            Note: you must use VAR_STRING. Byte code very similar to TEXT_COMMAND_DISPLAY_TEXT in V
            Old name: _GET_TEXT_SUBSTRING
            </summary>
        </member>
        <member name="M:RDR2.Native.HUD.GET_FILENAME_FOR_AUDIO_CONVERSATION(System.String)">
            <summary>
            Gets a string literal from a label name.
            
            Old name: _GET_LABEL_TEXT
            </summary>
        </member>
        <member name="M:RDR2.Native.HUD.DOES_TEXT_LABEL_EXIST(System.String)">
            <summary>
            Checks if the passed gxt name exists in the game files.
            </summary>
        </member>
        <member name="M:RDR2.Native.HUD._GET_TEXT_SUBSTRING_2(System.String,System.Int32)">
            <summary>
            Similar to 0x9D7E12EC6A1EE4E5(GET_TEXT_SUBSTRING) but starts at the beginning of the string
            </summary>
        </member>
        <member name="M:RDR2.Native.HUD.GET_LENGTH_OF_LITERAL_STRING(System.String)">
            <summary>
            Returns the length of the string passed (much like strlen).
            </summary>
        </member>
        <member name="M:RDR2.Native.HUD.GET_STRING_FROM_HASH_KEY(System.UInt32)">
            <summary>
            Returns the label text given the hash.
            
            Old name: _GET_LABEL_TEXT_BY_HASH
            </summary>
        </member>
        <member name="M:RDR2.Native.HUD.DISPLAY_HUD(System.Boolean)">
            <summary>
            If Hud should be displayed
            </summary>
        </member>
        <member name="M:RDR2.Native.HUD._GET_COLOR_FROM_NAME(System.UInt32,System.Int32*,System.Int32*,System.Int32*,System.Int32*)">
            <summary>
            colorNameHash: https://alloc8or.re/rdr3/doc/enums/eColor.txt
            </summary>
        </member>
```
