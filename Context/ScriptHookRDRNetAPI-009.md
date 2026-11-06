<!-- ScriptHookRDRNetAPI lines 2001-2250 of 30005 -->
```csharp
        <member name="M:RDR2.Native.ATTRIBUTE.GET_MAX_ATTRIBUTE_POINTS(System.Int32,System.Int32)">
            <summary>
            attributeIndex: see SET_ATTRIBUTE_BASE_RANK
            </summary>
        </member>
        <member name="M:RDR2.Native.ATTRIBUTE._SET_ATTRIBUTE_CORE_VALUE(System.Int32,System.Int32,System.Int32)">
            <summary>
            coreIndex:
            enum eAttributeCore
            {
            	ATTRIBUTE_CORE_HEALTH,
            	ATTRIBUTE_CORE_STAMINA,
            	ATTRIBUTE_CORE_DEADEYE
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.ATTRIBUTE._GET_ATTRIBUTE_CORE_VALUE(System.Int32,System.Int32)">
            <summary>
            Gets the ped's core value on a scale of 0 to 100.
            coreIndex: see _SET_ATTRIBUTE_CORE_VALUE
            </summary>
        </member>
        <member name="M:RDR2.Native.ATTRIBUTE.GET_DEFAULT_ATTRIBUTE_POINTS_NEEDED_FOR_RANK(System.UInt32,System.Int32,System.Int32)">
            <summary>
            attributeIndex: see SET_ATTRIBUTE_BASE_RANK
            </summary>
        </member>
        <member name="M:RDR2.Native.ATTRIBUTE.ENABLE_ATTRIBUTE_OVERPOWER(System.Int32,System.Int32,System.Single,System.Boolean)">
            <summary>
            attributeIndex: see SET_ATTRIBUTE_BASE_RANK
            
            Old name: _SET_ATTRIBUTE_OVERPOWER_VALUE
            </summary>
        </member>
        <member name="M:RDR2.Native.ATTRIBUTE._ENABLE_ATTRIBUTE_CORE_OVERPOWER(System.Int32,System.Int32,System.Single,System.Boolean)">
            <summary>
            coreIndex: see _SET_ATTRIBUTE_CORE_VALUE
            
            Previously incorrectly named ENABLE_ATTRIBUTE_OVERPOWER
            </summary>
        </member>
        <member name="M:RDR2.Native.ATTRIBUTE.DISABLE_ATTRIBUTE_OVERPOWER(System.Int32,System.Int32)">
            <summary>
            attributeIndex: see SET_ATTRIBUTE_BASE_RANK
            </summary>
        </member>
        <member name="M:RDR2.Native.ATTRIBUTE._IS_ATTRIBUTE_OVERPOWERED(System.Int32,System.Int32)">
            <summary>
            attributeIndex: see SET_ATTRIBUTE_BASE_RANK
            </summary>
        </member>
        <member name="M:RDR2.Native.ATTRIBUTE._START_ITEM_PREVIEW(System.UInt64,System.Int32)">
            <summary>
            Params: p1 is related to satchel_category
            </summary>
        </member>
        <member name="M:RDR2.Native.ATTRIBUTE._SET_STATUS_EFFECT_CORE_ICON(System.Int32)">
            <summary>
            Displays status effects on core icons (includes warnings).
            
            enum eUiRpgStatusEffect
            {
            	STATUS_NONE,
            	STATUS_COLD,
            	STATUS_HOT,
            	STATUS_OVERFED,
            	STATUS_DIRTY,
            	STATUS_SNAKE_VENOM,
            	STATUS_ARROW_WOUNDED,
            	STATUS_ARROW_DRAINED,
            	STATUS_ARROW_DISORIENTED,
            	STATUS_ARROW_TRACKED,
            	STATUS_ARROW_CONFUSION,
            	STATUS_UNDERWEIGHT,
            	STATUS_OVERWEIGHT,
            	STATUS_SICK_1,
            	STATUS_SICK_2,
            	STATUS_PREDATOR_INVULNERABLE
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.ATTRIBUTE._SET_STATUS_EFFECT_PERIODIC_ICON(System.Int32)">
            <summary>
            Starts core periodic icon.
            statusEffectType: see 0xA4D3A1C008F250DF
            </summary>
        </member>
        <member name="M:RDR2.Native.ATTRIBUTE._STOP_STATUS_EFFECT_PERIODIC_ICON(System.Int32)">
            <summary>
            Stops periodic icon.
            statusEffectType: see 0xA4D3A1C008F250DF
            </summary>
        </member>
        <member name="M:RDR2.Native.AUDIO._0x7455CD705F7E933E">
            <summary>
            _AUDIO_IS_* - _AUDIO_TRIGGER*
            </summary>
        </member>
        <member name="M:RDR2.Native.AUDIO._0x79F9C57B8D0DFE90(System.String,System.Int32)">
            <summary>
            Only used in R* SP Script cv_mus_shared
            </summary>
        </member>
        <member name="M:RDR2.Native.AUDIO._0xD0730C1FA40348D9(System.String)">
            <summary>
            _IS_SCRIPTED_CONVERSATION_*
            </summary>
        </member>
        <member name="M:RDR2.Native.AUDIO.REGISTER_SCRIPT_WITH_AUDIO(System.Int32)">
            <summary>
            nullsub, doesn't do anything
            </summary>
        </member>
        <member name="M:RDR2.Native.AUDIO.UNREGISTER_SCRIPT_WITH_AUDIO">
            <summary>
            nullsub, doesn't do anything
            </summary>
        </member>
        <member name="M:RDR2.Native.AUDIO.PLAY_SOUND_FRONTEND(System.String,System.String,System.Boolean,System.UInt64)">
            <summary>
            https://github.com/femga/rdr3_discoveries/tree/master/audio/frontend_soundsets
            </summary>
        </member>
        <member name="M:RDR2.Native.AUDIO._PLAY_SOUND_FROM_ITEM(System.UInt32,System.UInt32,System.UInt64)">
            <summary>
            item: value returned from 0x2E1CDC1FF3B8473E
            soundSet: HUD_SHOP_SOUNDSET, COMPANIONS_ROBBERIES_SOUNDSET
            </summary>
        </member>
        <member name="M:RDR2.Native.AUDIO._0x580D71DFE0088E34(System.String,System.String)">
            <summary>
            _IS_SOUND_RUNNING(?)
            </summary>
        </member>
        <member name="M:RDR2.Native.AUDIO._IS_SCRIPTED_AUDIO_CUSTOM(System.UInt32,System.UInt32)">
            <summary>
            item: FUSE, value returned from 0x2E1CDC1FF3B8473E
            soundSet: HUD_SHOP_SOUNDSET, COMPANIONS_ROBBERIES_SOUNDSET
            </summary>
        </member>
        <member name="M:RDR2.Native.AUDIO._SET_WHISTLE_CONFIG_FOR_PED(System.Int32,System.String,System.Single)">
            <summary>
            whistleConfig: Ped.WhistlePitch (0.0 - 1.0), Ped.WhistleClarity (0.0 - 1.0), Ped.WhistleShape (0.0 - 10.0)
            </summary>
        </member>
        <member name="M:RDR2.Native.AUDIO._SET_SOUND_RELATIONSHIP_ON_PED(System.Int32,System.String,System.String)">
            <summary>
            p1: Entity.Relationship
            p2: Player, Enemy, Teammate, Neutral
            </summary>
        </member>
        <member name="M:RDR2.Native.AUDIO._PLAY_SOUND_FROM_ENTITY_WITH_SET(System.Int32,System.String,System.Int32,System.String,System.Boolean,System.UInt64)">
            <summary>
            Params: p5 seems to be always 0
            </summary>
        </member>
        <member name="M:RDR2.Native.AUDIO._PLAY_SOUND_FROM_POSITION_WITH_ID(System.Int32,System.String,System.Single,System.Single,System.Single,System.String,System.Boolean,System.Int32,System.Boolean)">
            <summary>
            Starts Audio Loop
            _PLAY_SOUND_FROM_ENTITY* - _PLAY_SOUND_FRONTEND*
            </summary>
        </member>
        <member name="M:RDR2.Native.AUDIO._UPDATE_SOUND_POSITION(System.Int32,System.Single,System.Single,System.Single)">
            <summary>
            Only used in R* SP Scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.AUDIO.PREPARE_SOUNDSET(System.String,System.Boolean)">
            <summary>
            https://github.com/femga/rdr3_discoveries/tree/master/audio/soundsets
            </summary>
        </member>
        <!-- Badly formed XML comment ignored for member "M:RDR2.Native.AUDIO.PLAY_PED_AMBIENT_SPEECH_NATIVE(System.Int32,System.UInt64*)" -->
        <member name="M:RDR2.Native.AUDIO.PLAY_AMBIENT_SPEECH_FROM_POSITION_NATIVE(System.Single,System.Single,System.Single,System.UInt64*)">
            <summary>
            Old name: _PLAY_AMBIENT_SPEECH_AT_COORDS
            </summary>
        </member>
        <member name="M:RDR2.Native.AUDIO._0x0D7FD6A55FD63AEF(System.Int32,System.Int32,System.Boolean)">
            <summary>
            Related to audio events of some sort
            </summary>
        </member>
        <member name="M:RDR2.Native.AUDIO.PLAY_PAIN(System.Int32,System.Int32,System.Single,System.Boolean,System.Boolean)">
            <summary>
            Valid pain IDs: 0..12
            </summary>
        </member>
        <member name="M:RDR2.Native.AUDIO._0xF092B6030D6FD49C(System.Int32,System.String)">
            <summary>
            Name: ROPE_SETTINGS_DEFAULT
            </summary>
        </member>
        <member name="M:RDR2.Native.AUDIO._SET_VOFX_PED_VOICE(System.Int32,System.UInt32)">
            <summary>
            Hashes: VOFX_PLAYER_MALE01, VOFX_PLAYER_MALE02, VOFX_PLAYER_MALE03, VOFX_PLAYER_FEMALE01, VOFX_PLAYER_FEMALE02, VOFX_PLAYER_FEMALE03
            </summary>
        </member>
        <member name="M:RDR2.Native.AUDIO.DOES_CONTEXT_EXIST_FOR_THIS_PED(System.Int32,System.String,System.Boolean)">
            <summary>
            Checks if the ped can play the speech or has the speech file, last parameter is usually false.
            </summary>
        </member>
        <member name="M:RDR2.Native.AUDIO._0x864A842B86993851(System.Int32)">
            <summary>
            Not implemented.
            </summary>
        </member>
        <member name="M:RDR2.Native.AUDIO.SET_ANIMAL_MOOD(System.Int32,System.Int32)">
            <summary>
            Not implemented.
            </summary>
        </member>
        <member name="M:RDR2.Native.AUDIO.IS_HORN_ACTIVE(System.Int32)">
            <summary>
            Checks whether the horn of a vehicle is currently played.
            </summary>
        </member>
        <member name="M:RDR2.Native.AUDIO.FORCE_USE_AUDIO_GAME_OBJECT(System.Int32,System.String)">
            <summary>
            Old name: _FORCE_VEHICLE_ENGINE_AUDIO
            </summary>
        </member>
        <member name="M:RDR2.Native.AUDIO.SET_GPS_ACTIVE(System.Boolean)">
            <summary>
            nullsub, doesn't do anything
            </summary>
        </member>
        <member name="M:RDR2.Native.AUDIO._0x6AB944DF68B512D3(System.UInt64)">
            <summary>
            _STOP_AUDIO_*
            </summary>
        </member>
        <member name="M:RDR2.Native.AUDIO.AUDIO_IS_MUSIC_PLAYING">
            <summary>
            Old name: AUDIO_IS_SCRIPTED_MUSIC_PLAYING
            </summary>
        </member>
        <member name="M:RDR2.Native.AUDIO._0xBE28DB99556FF8D9(System.Int32)">
            <summary>
            Checks for MOONSHINE_BAND
            </summary>
        </member>
        <member name="M:RDR2.Native.AUDIO._0xB93A769B8B726950(System.Int32,System.UInt32)">
            <summary>
            Used in Script Function NET_CAMP_CLIENT_UPDATE_PED_ROLE_STATE_SHOP: hash exists! Playing hash
            </summary>
        </member>
        <member name="M:RDR2.Native.AUDIO.TRIGGER_MUSIC_EVENT(System.String)">
            <summary>
```
