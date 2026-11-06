<!-- ScriptHookRDRNetAPI lines 5001-5250 of 30005 -->
```csharp
            	LAW_REGION_WAPITI = 0x7A976E02
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW._0x5E6F375CA101C108(System.Int32,System.Boolean)">
            <summary>
            Only used in R* SP Scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW._0xEDFC6C1FD1C964F5(System.Int32,System.UInt32,System.Int32,System.Single,System.Single,System.Boolean,System.Single,System.Single,System.UInt64)">
            <summary>
            _SET_C* - _SET_D*
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW._0x318F0F9A4426CFA2(System.Int32,System.UInt64*)">
            <summary>
            Only used in R* SP Script av_amb_camp_robbery
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW._0xD7494DED50C6EF52(System.Int32,System.UInt32,System.Int32)">
            <summary>
            Only used in R* SP Scripts
            Params: p2 either 1 or 2
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW._0xDA1A9ADC4E3D4B16(System.Int32,System.Boolean,System.Boolean)">
            <summary>
            Only used in R* SP Scripts
            Params: p1 = true, p2 = false
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW._0xECE3C34B270428D5">
            <summary>
            Only used in rcm_homerob00 R* Script
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW._ARE_LAW_PEDS_ENABLED_FOR_TRAIN">
            <summary>
            Only used in trainrobbery_ambient R* Script
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW._0x82F11E1296996574(System.Int32)">
            <summary>
            Only used in rcm_gunslinger1_1 R* Script: p0 = 0
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW._0xC0DF161950FB101E(System.Int32)">
            <summary>
            Only used in rcm_serial_killer1 R* Script
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW._0xE9EB79CBF9C0F58A(System.Int32)">
            <summary>
            Returns p1 value for 0xE4D6E45F491A66CB
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW._0x390710D2DAFA6BFF(System.Int32,System.Boolean)">
            <summary>
            _CLEAR*
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW._GET_CRIME_BOUNTY_AMOUNT_BY_TYPE(System.UInt32)">
            <summary>
            Returns bounty (increment) value
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW._0xDAEFDFDB2AEECE37(System.UInt32,System.UInt64)">
            <summary>
            crimeType: see _REPORT_CRIME
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW._GET_HUD_PLAYER_CRIME_TYPE(System.Int32)">
            <summary>
            See _REPORT_CRIME
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW._0x0BDFEBCF40A5F7E3(System.UInt32)">
            <summary>
            Only used in net_fetch R* Script
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW._SET_CUSTOM_LAW_DISPATCH_RESPONSE(System.UInt32)">
            <summary>
            Note: This native is only used in multiplayer scripts
            dispatchResponseHash: see update1/common/data/dispatchresponses/..
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW._CREATE_LAW_DISPATCH_RESPONSE_FOR_COORDS(System.Single,System.Single,System.Single,System.UInt32)">
            <summary>
            dispatchResponseHash: see common/data/dispatchresponses/..
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW._SET_BOUNTY_HUNTER_PURSUIT_CLEARED">
            <summary>
            Force clears local player's wanted level
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW._0xBD944A3D36E992DE">
            <summary>
            Called together with REPORT_POLICE_SPOTTED_PLAYER
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW._0x9C8A2BF37E966464(System.Int32,System.Int32)">
            <summary>
            Only used in act_bankrobbery01 R* Script
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW._0x7351DA734F989F4E(System.Int32)">
            <summary>
            Only used in shoprobberies
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW._GET_BOUNTY_HUNTER_GLOBAL_COOLDOWN(System.UInt32)">
            <summary>
            p0 is always BOUNTYHUNTERSGLOBALCOOLDOWN in R* scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW._SET_BOUNTY_HUNTER_GLOBAL_COOLDOWN(System.UInt32,System.Int32)">
            <summary>
            p0 is always BOUNTYHUNTERSGLOBALCOOLDOWN in R* scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW._0x2001687F9562FD9D(System.UInt64)">
            <summary>
            Only used in resapwn_dump_body R* Script
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW._PAUSE_BOUNTY_HUNTER_COOLDOWN(System.UInt32,System.Boolean,System.UInt64)">
            <summary>
            p0 is always BOUNTYHUNTERSGLOBALCOOLDOWN in R* scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW._0xE9AC8466ABE484BB(System.Boolean,System.UInt64)">
            <summary>
            Only used in R* SP Scripts
            Params: p1 = 0
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW._0xE94B5E938619712E">
            <summary>
            Seems to disable lawmen guarding behaviors (like during a region lockdown). Must be called every frame.
            Only used in R* SP Scripts, mostly used in train_fast_travel_core
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW._0x7FC667F6DDFBCDCC(System.Int32)">
            <summary>
            Only used in R* Script long_update
            Returns a value thats being subtracted from GET_GAME_TIMER
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW._SET_PED_LAW_BEHAVIOUR(System.Int32,System.Int32)">
            <summary>
            behaviour: https://github.com/Halen84/RDR3-Native-Flags-And-Enums/tree/main/CLawBehavior__Flags
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW.SET_LAW_SENSE_RANGE_MODIFIER(System.Int32,System.Single)">
            <summary>
            Default range is 1.0f
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW._0x7EF2A2FE38D74456(System.Int32,System.Boolean)">
            <summary>
            _SET_DISPATCH_*
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW._0xC7DC5A0A7DF608CB(System.Int32)">
            <summary>
            _GET_DISPATCH_*
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW._0x9772395CC73E8D1F(System.Int32,System.String)">
            <summary>
            Only used in loanshark_miner1 R* Script: name = ANNESBURG_MINES
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW._0x0DBACA9C38C9A686(System.String)">
            <summary>
            Only used in sisikapenitentiary R* Script: name = SISIKA
            _IS_G* or _IS_H*
            </summary>
        </member>
        <member name="M:RDR2.Native.LAW._CREATE_GUARD_ZONE_FOR_ENTITY(System.String,System.Int32,System.Single,System.Single,System.Single)">
            <summary>
            Returns true when investigation creation was successful
            </summary>
        </member>
        <member name="M:RDR2.Native.LOCALIZATION.LOCALIZATION_GET_SYSTEM_LANGUAGE">
            <summary>
            Same return values as GET_CURRENT_LANGUAGE
            </summary>
        </member>
        <member name="M:RDR2.Native.LOCALIZATION.GET_CURRENT_LANGUAGE">
            <summary>
            0 = american (en-US)
            1 = french (fr-FR)
            2 = german (de-DE)
            3 = italian (it-IT)
            4 = spanish (es-ES)
            5 = brazilian (pt-BR)
            6 = polish (pl-PL)
            7 = russian (ru-RU)
            8 = korean (ko-KR)
            9 = chinesetrad (zh-TW)
            10 = japanese (ja-JP)
            11 = mexican (es-MX)
            12 = chinesesimp (zh-CN)
            </summary>
        </member>
        <member name="M:RDR2.Native.LOCALIZATION._0x45D50415E4D885FF">
            <summary>
            Returns true if the current language is american, french, german, italian, spanish, brazilian or mexican.
            _DOES_*
            </summary>
        </member>
        <member name="M:RDR2.Native.LOCALIZATION.LOCALIZATION_GET_SYSTEM_DATE_TYPE">
            <summary>
            0 = DATE_FORMAT_DMY
            1 = DATE_FORMAT_MDY
            2 = DATE_FORMAT_YMD
            
            Old name: _LOCALIZATION_GET_SYSTEM_DATE_FORMAT
            </summary>
        </member>
        <member name="M:RDR2.Native.MAP.GET_BLIP_FROM_ENTITY(System.Int32)">
            <summary>
            Returns the Blip handle of given Entity.
            </summary>
        </member>
        <member name="M:RDR2.Native.MAP.BLIP_ADD_FOR_COORDS(System.UInt32,System.Single,System.Single,System.Single)">
            <summary>
            https://github.com/femga/rdr3_discoveries/tree/master/useful_info_from_rpfs/textures/blips
            https://github.com/femga/rdr3_discoveries/tree/master/useful_info_from_rpfs/textures/blips_mp
            </summary>
        </member>
        <member name="M:RDR2.Native.MAP._BLIP_SET_STYLE(System.Int32,System.UInt32)">
            <summary>
            https://github.com/femga/rdr3_discoveries/tree/master/useful_info_from_rpfs/blip_styles
            Removes any existing modifiers and sets the style.
            </summary>
        </member>
        <member name="M:RDR2.Native.MAP.BLIP_ADD_MODIFIER(System.Int32,System.UInt32)">
            <summary>
            https://alloc8or.re/rdr3/doc/enums/eBlipModifier.txt
            https://github.com/femga/rdr3_discoveries/tree/master/useful_info_from_rpfs/blip_modifiers
            
            Old name: _BLIP_SET_MODIFIER
            </summary>
        </member>
        <member name="M:RDR2.Native.MAP.BLIP_REMOVE_MODIFIER(System.Int32,System.UInt32)">
            <summary>
```
