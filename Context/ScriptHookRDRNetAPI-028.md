<!-- ScriptHookRDRNetAPI lines 6751-7000 of 30005 -->
```csharp
        <member name="M:RDR2.Native.NETWORK._NETWORK_GET_ROS_TITLE_NAME">
            <summary>
            Returns CGameConfig->ConfigOnlineServices->RosTitleName (see gameconfig.xml)
            </summary>
        </member>
        <member name="M:RDR2.Native.OBJECT.DELETE_OBJECT(System.Int32*)">
            <summary>
            Deletes the specified object, then sets the handle pointed to by the pointer to NULL.
            </summary>
        </member>
        <member name="M:RDR2.Native.OBJECT._SET_OBJECT_TARGETTABLE_FOCUS(System.Int32,System.Boolean,System.Boolean)">
            <summary>
            When p1 and p2 are true you can focus on the object (similar to when you focus a ped)
            </summary>
        </member>
        <member name="M:RDR2.Native.OBJECT.GET_CLOSEST_OBJECT_OF_TYPE(System.Single,System.Single,System.Single,System.Single,System.UInt32,System.Boolean,System.Boolean,System.Boolean)">
            <summary>
            isMission - if true doesn't return mission objects
            </summary>
        </member>
        <member name="M:RDR2.Native.OBJECT.GET_OFFSET_FROM_COORD_AND_HEADING_IN_WORLD_COORDS(System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single)">
            <summary>
            Old name: _GET_OBJECT_OFFSET_FROM_COORDS
            </summary>
        </member>
        <member name="M:RDR2.Native.OBJECT._ADD_DOOR_TO_SYSTEM_NEW(System.UInt32,System.Boolean,System.Boolean,System.Boolean,System.Int32,System.Int32,System.Boolean)">
            <summary>
            Registers a door, hashes: https://github.com/femga/rdr3_discoveries/tree/master/doorHashes
            </summary>
        </member>
        <member name="M:RDR2.Native.OBJECT.DOOR_SYSTEM_SET_DOOR_STATE(System.UInt32,System.Int32)">
            <summary>
            Door lock states:
            enum eDoorState
            {
            	DOORSTATE_INVALID = -1,
            	DOORSTATE_UNLOCKED,
            	DOORSTATE_LOCKED_UNBREAKABLE,
            	DOORSTATE_LOCKED_BREAKABLE,
            	DOORSTATE_HOLD_OPEN_POSITIVE,
            	DOORSTATE_HOLD_OPEN_NEGATIVE
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.OBJECT.DOOR_SYSTEM_SET_OPEN_RATIO(System.UInt32,System.Single,System.Boolean)">
            <summary>
            Sets the ajar angle of a door.
            Ranges from -1.0 to 1.0, and 0.0 is closed / default.
            </summary>
        </member>
        <member name="M:RDR2.Native.OBJECT._IS_DOOR_REGISTERED_WITH_OWNER(System.UInt32)">
            <summary>
            Returns true if door is alredy registered with owner
            </summary>
        </member>
        <member name="M:RDR2.Native.OBJECT._0x0943113E02322164(System.Int32,System.Int32)">
            <summary>
            Params: p1 = 23 in R* Scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.OBJECT.SET_OBJECT_PHYSICS_PARAMS(System.Int32,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single)">
            <summary>
            Adjust the physics parameters of a prop, or otherwise known as "object". This is useful for simulated gravity.
            
            Other parameters seem to be unknown.
            
            p2: seems to be weight and gravity related. Higher value makes the obj fall faster. Very sensitive?
            p3: seems similar to p2
            p4: makes obj fall slower the higher the value
            p5: similar to p4
            </summary>
        </member>
        <member name="M:RDR2.Native.OBJECT._SET_LIGHT_TRANSLUCENCY_FOR_OBJECT(System.Int32,System.Single)">
            <summary>
            Params: value = 0.0 - 586.67 (?)
            </summary>
        </member>
        <member name="M:RDR2.Native.OBJECT.CREATE_PICKUP(System.UInt32,System.Single,System.Single,System.Single,System.Int32,System.Int32,System.Boolean,System.UInt32,System.Int32,System.Single,System.UInt64)">
            <summary>
            https://github.com/Halen84/RDR3-Native-Flags-And-Enums/tree/main/Placement%20Flags
            https://github.com/femga/rdr3_discoveries/blob/master/objects/pickup_list.lua
            </summary>
        </member>
        <member name="M:RDR2.Native.OBJECT.CREATE_PICKUP_ROTATE(System.UInt32,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Int32,System.Int32,System.Int32,System.Boolean,System.UInt32,System.Int32,System.Single,System.UInt64)">
            <summary>
            flags: see CREATE_PICKUP
            </summary>
        </member>
        <member name="M:RDR2.Native.OBJECT.CREATE_AMBIENT_PICKUP(System.UInt32,System.Single,System.Single,System.Single,System.Int32,System.Int32,System.UInt32,System.Boolean,System.Boolean,System.Int32,System.Single)">
            <summary>
            flags: see CREATE_PICKUP
            </summary>
        </member>
        <member name="M:RDR2.Native.OBJECT._SET_NETWORK_PICKUP_USABLE_FOR_PLAYER(System.Int32,System.UInt32,System.Boolean)">
            <summary>
            Params: p2 controls whether to make pickups usable/collectable or not in networked games
            </summary>
        </member>
        <member name="M:RDR2.Native.OBJECT.SET_LOCAL_PLAYER_PERMITTED_TO_COLLECT_PICKUPS_WITH_MODEL(System.UInt32,System.Boolean)">
            <summary>
            Maximum amount of pickup models that can be disallowed is 10.
            
            Old name: _SET_LOCAL_PLAYER_CAN_USE_PICKUPS_WITH_THIS_MODEL
            </summary>
        </member>
        <member name="M:RDR2.Native.OBJECT._0x1F5E07E14A86FAFC(System.Boolean)">
            <summary>
            _SET_A(MBIENT_PICKUP_?)*
            </summary>
        </member>
        <member name="M:RDR2.Native.OBJECT.SET_OBJECT_TINT_INDEX(System.Int32,System.Int32)">
            <summary>
            Alt name: _SET_OBJECT_TINT
            
            Old name: _SET_OBJECT_TEXTURE_VARIATION
            </summary>
        </member>
        <member name="M:RDR2.Native.OBJECT._GET_OBJECT_LIGHT_INTENSITY(System.Int32)">
            <summary>
            Returns float value to be used with _SET_LIGHT_INTENSITY_FOR_OBJECT
            </summary>
        </member>
        <member name="M:RDR2.Native.OBJECT._SET_OBJECT_BURN_LEVEL(System.Int32,System.Single,System.Boolean)">
            <summary>
            Seems to mostly have effect on wood-made objects https://imgur.com/a/32oQvOn
            </summary>
        </member>
        <member name="M:RDR2.Native.OBJECT._SET_OBJECT_BURN_SPEED(System.Int32,System.Single,System.Single)">
            <summary>
            p2 is usually the same as speed parameter
            </summary>
        </member>
        <member name="M:RDR2.Native.OBJECT.CONVERT_OLD_PICKUP_TYPE_TO_NEW(System.UInt32)">
            <summary>
            Old name: _GET_PICKUP_HASH
            </summary>
        </member>
        <member name="M:RDR2.Native.OBJECT.ONLY_CLEAN_UP_OBJECT_WHEN_OUT_OF_RANGE(System.Int32)">
            <summary>
            Old name: _MARK_OBJECT_FOR_DELETION
            </summary>
        </member>
        <member name="M:RDR2.Native.OBJECT._0xCAAF2BCCFEF37F77(System.Int32,System.UInt64)">
            <summary>
            _SET_OBJECT_*
            </summary>
        </member>
        <member name="M:RDR2.Native.OBJECT._MAKE_ITEM_CARRIABLE(System.Int32)">
            <summary>
            _PRE* or _Q* or _RE*
            </summary>
        </member>
        <member name="M:RDR2.Native.OBJECT._SET_AUTO_JUMPABLE_BY_HORSE(System.Int32,System.Boolean)">
            <summary>
            Sets object as auto-jumpable by horse.
            </summary>
        </member>
        <member name="M:RDR2.Native.OBJECT._SET_NOT_JUMPABLE_BY_HORSE(System.Int32,System.Boolean)">
            <summary>
            Sets object as not jumpable by horse.
            </summary>
        </member>
        <member name="M:RDR2.Native.PAD._SET_CONTROL_CONTEXT(System.Int32,System.UInt32)">
            <summary>
            Sets the current control context. Must be called every frame.
            
            context: https://alloc8or.re/rdr3/doc/misc/input_contexts.txt
            For more information, see common:/data/control/settings.meta
            https://github.com/femga/rdr3_discoveries/tree/master/Controls
            </summary>
        </member>
        <member name="M:RDR2.Native.PAD.SET_CONTROL_VALUE_NEXT_FRAME(System.Int32,System.UInt32,System.Single)">
            <summary>
            This is for simulating player input.
            value is a float value from 0 - 1
            
            control: see IS_CONTROL_ENABLED
            
            Old name: _SET_CONTROL_NORMAL
            </summary>
        </member>
        <member name="M:RDR2.Native.PAD.GET_CONTROL_HOW_LONG_AGO(System.Int32)">
            <summary>
            Returns time in ms since last input.
            </summary>
        </member>
        <member name="M:RDR2.Native.PAD.IS_USING_KEYBOARD_AND_MOUSE(System.Int32)">
            <summary>
            padIndex is not used
            
            Old name: _IS_USING_KEYBOARD
            </summary>
        </member>
        <member name="M:RDR2.Native.PAD.SET_CONTROL_LIGHT_EFFECT_COLOR(System.Int32,System.Int32,System.Int32,System.Int32)">
            <summary>
            nullsub, doesn't do anything
            
            Old name: _SET_CONTROL_GROUP_COLOR
            </summary>
        </member>
        <member name="M:RDR2.Native.PAD.CLEAR_CONTROL_LIGHT_EFFECT(System.Int32)">
            <summary>
            nullsub, doesn't do anything
            </summary>
        </member>
        <member name="M:RDR2.Native.PAD.SET_CONTROL_LIGHT_EFFECT_FLASHING_COLOR(System.Int32,System.Int32,System.Int32,System.Int32)">
            <summary>
            nullsub, doesn't do anything
            </summary>
        </member>
        <member name="M:RDR2.Native.PAD.SET_CONTROL_SHAKE(System.Int32,System.Int32,System.Int32)">
            <summary>
            Old name: SET_PAD_SHAKE
            </summary>
        </member>
        <member name="M:RDR2.Native.PAD.SET_CONTROL_TRIGGER_SHAKE(System.Int32,System.Int32,System.Int32,System.Int32,System.Int32)">
            <summary>
            nullsub, doesn't do anything
            </summary>
        </member>
        <member name="M:RDR2.Native.PAD.STOP_CONTROL_SHAKE(System.Int32)">
            <summary>
            Old name: STOP_PAD_SHAKE
            </summary>
        </member>
        <member name="M:RDR2.Native.PAD.SET_CONTROL_SHAKE_SUPPRESSED_ID(System.Int32,System.Int32)">
            <summary>
            Old name: SET_PAD_SHAKE_SUPPRESSED_ID
            </summary>
        </member>
        <member name="M:RDR2.Native.PAD.CLEAR_CONTROL_SHAKE_SUPPRESSED_ID(System.Int32)">
            <summary>
            Old name: _CLEAR_SUPPRESSED_PAD_RUMBLE
            </summary>
        </member>
        <member name="M:RDR2.Native.PAD._0x5F217BC1190503D8(System.String,System.Single)">
            <summary>
            rumbleCurve: common_0/data/rumblecurves.meta
            </summary>
        </member>
        <member name="M:RDR2.Native.PAD._0x1252C029FC8EBB4D">
            <summary>
            _IS_R* - _IS_S*
            </summary>
        </member>
        <member name="M:RDR2.Native.PATHFIND.IS_VEHICLE_NODE_ID_VALID(System.Int32)">
            <summary>
            Returns true if the id is non zero.
            </summary>
        </member>
```
