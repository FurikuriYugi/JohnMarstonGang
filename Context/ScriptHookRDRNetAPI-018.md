<!-- ScriptHookRDRNetAPI lines 4251-4500 of 30005 -->
```csharp
        <member name="M:RDR2.Native.HUD._DISPLAY_TEXT(System.String,System.Single,System.Single)">
            <summary>
            nullsub, this native does nothing since build 1436, use _BG_DISPLAY_TEXT (0x16794E044C9EFB58) instead.
            </summary>
        </member>
        <member name="M:RDR2.Native.HUD._SET_TEXT_COLOR(System.Int32,System.Int32,System.Int32,System.Int32)">
            <summary>
            This native does nothing since build 1436, use _BG_SET_TEXT_COLOR (0x16FA5CE47F184F1E) instead.
            </summary>
        </member>
        <member name="M:RDR2.Native.HUD._0x2F7BB105144ACF30">
            <summary>
            nullsub, doesn't do anything
            </summary>
        </member>
        <member name="M:RDR2.Native.HUD._0xBFFF81E12A745A5F">
            <summary>
            nullsub, doesn't do anything
            </summary>
        </member>
        <member name="M:RDR2.Native.HUD.ALLOW_PAUSE_WHEN_NOT_IN_STATE_OF_PLAY_THIS_FRAME">
            <summary>
            Old name: _ALLOW_PAUSE_MENU_WHEN_DEAD_THIS_FRAME
            </summary>
        </member>
        <member name="M:RDR2.Native.HUD.IS_PAUSE_MENU_ACTIVE">
            <summary>
            Returns true when either Pause Menu, a Frontend Menu, Online Policies menu or Social Club menu is active.
            </summary>
        </member>
        <member name="M:RDR2.Native.HUD._UI_PROMPT_SET_PRIORITY(System.Int32,System.Int32)">
            <summary>
            priority: https://github.com/Halen84/RDR3-Native-Flags-And-Enums/tree/main/ePromptPriority
            </summary>
        </member>
        <member name="M:RDR2.Native.HUD._UI_PROMPT_HAS_STANDARD_MODE_COMPLETED(System.Int32,System.Int32)">
            <summary>
            Params: p1 is 0
            </summary>
        </member>
        <member name="M:RDR2.Native.HUD._UI_PROMPT_SET_HOLD_MODE(System.Int32,System.Int32)">
            <summary>
            Params: p2 is 304000 in R* SP Script coachrobberies
            </summary>
        </member>
        <member name="M:RDR2.Native.HUD._UI_PROMPT_SET_STANDARDIZED_HOLD_MODE(System.Int32,System.UInt32)">
            <summary>
            Hashes: SHORT_TIMED_EVENT_MP, SHORT_TIMED_EVENT, MEDIUM_TIMED_EVENT, LONG_TIMED_EVENT, RUSTLING_CALM_TIMING, PLAYER_FOCUS_TIMING, PLAYER_REACTION_TIMING
            </summary>
        </member>
        <member name="M:RDR2.Native.HUD._UI_PROMPT_SET_MASH_MANUAL_MODE_INCREASE_PER_PRESS(System.Int32,System.Single)">
            <summary>
            standard (prompt not held) rate: (1f / 128f)
            fast (prompt held) rate: (1f / 64f)
            punitive (been hit) rate: (1f / 128f)
            </summary>
        </member>
        <member name="M:RDR2.Native.HUD._UI_PROMPT_SET_MASH_MANUAL_MODE_DECAY_SPEED(System.Int32,System.Single)">
            <summary>
            standard (prompt not held) rate: 0.035f
            fast (prompt held) rate: 0.015f
            punitive (been hit) rate: 0.14f
            </summary>
        </member>
        <member name="M:RDR2.Native.HUD._UI_PROMPT_SET_ROTATE_MODE(System.Int32,System.Single,System.Boolean)">
            <summary>
            Used for controllers
            </summary>
        </member>
        <member name="M:RDR2.Native.HUD._UI_PROMPT_SET_GROUP(System.Int32,System.Int32,System.Int32)">
            <summary>
            tabIndex: specifies tab of prompt
            </summary>
        </member>
        <member name="M:RDR2.Native.HUD._UI_PROMPT_SET_ACTIVE_GROUP_THIS_FRAME(System.UInt32,System.String,System.Int32,System.Int32,System.Int32,System.Int32)">
            <summary>
            Note: you must use VAR_STRING for p1 if string is not part of text database
            tabAmount: specifies number of tabs in prompt group
            tabDefaultIndex: specifies starting index
            p3 if is set > 3 you can no longer press Q to change tab if there are more than one tab set in tabAmount
            </summary>
        </member>
        <member name="M:RDR2.Native.HUD._UI_PROMPT_SET_TRANSPORT_MODE(System.Int32,System.Int32)">
            <summary>
            TM_ANY = 0,
            TM_ON_FOOT,
            TM_IN_VEHICLE
            </summary>
        </member>
        <member name="M:RDR2.Native.HUD._UI_PROMPT_SET_ATTRIBUTE(System.Int32,System.Int32,System.Boolean)">
            <summary>
            attribute: https://github.com/Halen84/RDR3-Native-Flags-And-Enums/tree/main/eUIPromptAttribute
            </summary>
        </member>
        <member name="M:RDR2.Native.HUD._UI_PROMPT_SET_TYPE(System.Int32,System.Int32)">
            <summary>
            Params: type = mostly 0, 6 (net_mission_intro_story_gvo), 7 (fm_mission_controller), 14 (net_ugc_end_flow_transition_online), 15 (net_main_[tlg_]offline)
            </summary>
        </member>
        <member name="M:RDR2.Native.HUD._UI_PROMPT_CONTEXT_SET_VOLUME(System.Int32,System.Int32)">
            <summary>
            Attaches a Volume
            </summary>
        </member>
        <member name="M:RDR2.Native.HUD._UI_PROMPT_ENABLE_PROMPT_TYPE_THIS_FRAME(System.Int32)">
            <summary>
            https://github.com/femga/rdr3_discoveries/tree/master/graphics/HUD/prompts/prompt_types
            </summary>
        </member>
        <member name="M:RDR2.Native.HUD._UI_PROMPT_SET_REGISTER_HORIZONTAL_ORIENTATION">
            <summary>
            This returns an id that can be used with 0x6095358C4142932A.
            </summary>
        </member>
        <member name="M:RDR2.Native.HUD._UI_PROMPT_CLEAR_HORIZONTAL_ORIENTATION(System.Int32)">
            <summary>
            id is the return value from 0xD9459157EB22C895.
            </summary>
        </member>
        <member name="M:RDR2.Native.HUD._SET_MP_GAMER_TAG_VISIBILITY(System.Int32,System.Int32)">
            <summary>
            visibility:
            enum eUIGamertagVisibility
            {
            	UIGAMERTAGVISIBILITY_NONE,
            	UIGAMERTAGVISIBILITY_ICON,
            	UIGAMERTAGVISIBILITY_SIMPLE,
            	UIGAMERTAGVISIBILITY_COMPLEX
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.HUD._SET_MP_GAMER_TAG_TYPE(System.Int32,System.UInt32)">
            <summary>
            Found types: GENERIC_PLAYER, DEADDROP, HOTPROPERTY, MINIGAMES
            </summary>
        </member>
        <member name="M:RDR2.Native.HUD._SET_MP_GAMER_TAG_UNK_ALLOW_LOCALIZED(System.Int32,System.Boolean)">
            <summary>
            nullsub, doesn't do anything
            </summary>
        </member>
        <member name="M:RDR2.Native.HUD._SET_MP_GAMER_TAG_TOP_ICON(System.Int32,System.UInt32)">
            <summary>
            Found icons: https://pastebin.com/xx6rEgiG
            </summary>
        </member>
        <member name="M:RDR2.Native.HUD._SET_MP_GAMER_TAG_SECONDARY_ICON(System.Int32,System.UInt32)">
            <summary>
            Found icons: SPEAKER, THROPY
            </summary>
        </member>
        <member name="M:RDR2.Native.IK._INVERSE_KINEMATICS_SET_DISABLED_FOR_PED(System.Int32,System.Int32,System.Boolean)">
            <summary>
            Seems to disable IK on ped
            </summary>
        </member>
        <member name="M:RDR2.Native._NAMESPACE30.SET_MOUSE_CURSOR_THIS_FRAME">
            <summary>
            Shows the cursor on screen for one frame.
            
            Old name: _SET_MOUSE_CURSOR_ACTIVE_THIS_FRAME
            </summary>
        </member>
        <member name="M:RDR2.Native._NAMESPACE30.SET_MOUSE_CURSOR_STYLE(System.Int32)">
            <summary>
            Changes the mouse cursor's sprite.
            
            spriteId's: https://github.com/femga/rdr3_discoveries/tree/master/graphics/HUD/cursor_sprites#readme
            
            Old name: _SET_MOUSE_CURSOR_SPRITE
            </summary>
        </member>
        <member name="M:RDR2.Native._NAMESPACE30._0x0546B117BB17548B">
            <summary>
            Allows camera to be moved if middle mouse button is held while in first person
            Must be called every frame
            _SET*
            </summary>
        </member>
        <member name="M:RDR2.Native._NAMESPACE30._0x2B8B605F2A9E64BF">
            <summary>
            Returns true if player is moving mouse while cursor is active
            _PI* - _PO*
            </summary>
        </member>
        <member name="M:RDR2.Native._NAMESPACE30._0x61CAE9D1FD055E44">
            <summary>
            Returns true if player is holding LMB while cursor is active
            _PI* - _PO*
            </summary>
        </member>
        <member name="M:RDR2.Native._NAMESPACE30._0xF7F51A57349739F2">
            <summary>
            Returns true if player releases LMB if cursor is active
            _PI* - _PO*
            </summary>
        </member>
        <member name="M:RDR2.Native.INTERIOR.GET_ROOM_KEY_FROM_ENTITY(System.Int32)">
            <summary>
            Gets the room hash key from the room that the specified entity is in. Each room in every interior has a unique key. Returns 0 if the entity is outside.
            </summary>
        </member>
        <member name="M:RDR2.Native.INTERIOR.GET_KEY_FOR_ENTITY_IN_ROOM(System.Int32)">
            <summary>
            Seems to do the exact same as INTERIOR::GET_ROOM_KEY_FROM_ENTITY
            </summary>
        </member>
        <member name="M:RDR2.Native.INTERIOR.GET_INTERIOR_FROM_ENTITY(System.Int32)">
            <summary>
            Returns the handle of the interior that the entity is in. Returns 0 if outside.
            </summary>
        </member>
        <member name="M:RDR2.Native.INTERIOR.UNPIN_INTERIOR(System.Int32)">
            <summary>
            Does something similar to INTERIOR::DISABLE_INTERIOR.
            
            You don't fall through the floor but everything is invisible inside and looks the same as when INTERIOR::DISABLE_INTERIOR is used. Peds behaves normally inside. 
            </summary>
        </member>
        <member name="M:RDR2.Native.INTERIOR.SET_INTERIOR_IN_USE(System.Int32)">
            <summary>
            Actually returns void in IDA but the script header defines a BOOL return type
            </summary>
        </member>
        <member name="M:RDR2.Native.INTERIOR.GET_INTERIOR_AT_COORDS_WITH_TYPEHASH(System.Single,System.Single,System.Single,System.UInt32)">
            <summary>
            Hashed version of GET_INTERIOR_AT_COORDS_WITH_TYPE
            </summary>
        </member>
        <member name="M:RDR2.Native.INTERIOR.IS_COLLISION_MARKED_OUTSIDE(System.Single,System.Single,System.Single)">
            <summary>
            Returns true if the collision at the specified coords is marked as being outside (false if there's an interior)
            </summary>
        </member>
        <member name="M:RDR2.Native.INTERIOR.ACTIVATE_INTERIOR_ENTITY_SET(System.Int32,System.String,System.Int32)">
            <summary>
            https://github.com/femga/rdr3_discoveries/tree/master/interiors/interior_sets
            </summary>
        </member>
        <member name="M:RDR2.Native.INVENTORY._INVENTORY_GET_INVENTORY_ID_FROM_PED(System.Int32)">
            <summary>
            Returns a unique inventory ID for this ped.
            For the local player ped, it is an eInventories value.
            For other peds, it is the inventory address casted to unsigned int.
            
            enum eInventories
            {
            	INVENTORY_INVALID,
            	INVENTORY_SP_PLAYER,
            	INVENTORY_MP_PLAYER,
```
