<!-- ScriptHookRDRNetAPI lines 9251-9500 of 30005 -->
```csharp
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.SET_HORSE_AVOIDANCE_LEVEL(System.Int32,System.Int32)">
            <summary>
            -1 - HORSE_ASSIST__NO_CHANGE
             0 - HORSE_ASSIST__MANUAL
             1 - HORSE_ASSIST__SEMIASSIST
             2 - HORSE_ASSIST__FULLASSIST
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._SET_PED_USE_HORSE_MAP_COLLISION(System.Int32,System.Boolean)">
            <summary>
            Doesn't actually return anything.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.GET_META_PED_ASSET_GUIDS(System.Int32,System.Int32,System.UInt32*,System.UInt32*,System.UInt32*,System.UInt32*)">
            <summary>
            This is a way to get what drawables a ped has equipped
            Example: you are able to tell if the ped has the drawable PLAYER_ZERO_HAT_017 attached
            Note: this works with non shop components, direct .ydd files.
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._SET_PED_BLACKBOARD_INT(System.Int32,System.String,System.Int32,System.Int32)">
            <summary>
            https://github.com/femga/rdr3_discoveries/tree/master/AI/BLACKBOARDS
            Blackboard natives allow you to apply and check certain data to/for peds.
            Blackboard bools, floats and strings are subdivided into 6 sections: "all", "animation", "any", "code", "global" and "script"
            Most changes are only visible for "script" blackboards, some "script" blackboards change ped motions
            "removeTimer" is self-removal timer, can be "-1" so your data will not be removed by the game (forever); 100 = 1 second
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._SET_PED_BLACKBOARD_HASH(System.Int32,System.String,System.String,System.Int32)">
            <summary>
            p1:
            BodyPartChained
            OverloadMostInjuredBodyPart
            
            p2:
            LeftLeg
            Legs
            RightArm
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._GET_PED_BLACKBOARD_BOOL(System.Int32,System.String)">
            <summary>
            Can be used to get a peds foliage active status: variableName = FoliageActive
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._GET_PED_BLACKBOARD_FLOAT(System.Int32,System.String)">
            <summary>
            Can be used to get a peds foliage raw height: variableName = FoliageHeight
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._SET_TANK_ATTRIBUTE_SIZE(System.Int32,System.Int32,System.Single)">
            <summary>
            Size will be permanent
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._IS_USING_SLIPSTREAM(System.Int32)">
            <summary>
            _IS_TRACKED_* - IS_V*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x10F96086123B939F(System.Int32,System.Int32,System.Single)">
            <summary>
            NB_CUSTOM_CLIENT_ON_CREATE_COMPLETE - set legendary to not avoid prey
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._REMOVE_SHOP_ITEM_FROM_PED(System.Int32,System.UInt32,System.Int32,System.Boolean)">
            <summary>
            Directly removes a shop item component from a ped
            Params: p2 and p3 are always 0
            </summary>
        </member>
        <member name="M:RDR2.Native.PED.REMOVE_SHOP_ITEM_FROM_PED_BY_CATEGORY(System.Int32,System.UInt32,System.Int32,System.Boolean)">
            <summary>
            Params: p2, p3 usually 0 in R* Scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xAAB86462966168CE(System.Int32,System.Boolean)">
            <summary>
            Related to _0x704C908E9C405136 for component loading
            _S*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._REFRESH_META_PED_SHOP_ITEMS(System.Int32,System.Int32)">
            <summary>
            p1 is always 1
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xD103F6DBB5442BE8(System.Int32,System.Int32)">
            <summary>
            Params: p1 either a 1 or 0, so perhaps BOOL
            _SET_PED_A*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._HAS_PED_TAKEN_GORE_DAMAGE(System.Int32,System.Int32)">
            <summary>
            limb: 3 = Left Hand, 4 = Left Arm, 6 = Right Hand, 7 = Right Arm, 9 = Left Foot, 10 = Left Leg, 12 = Right Foot, 13 = Right Leg, 37 = Head
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x704C908E9C405136(System.Int32)">
            <summary>
            _CLEAR*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._SET_PED_QUALITY(System.Int32,System.Int32)">
            <summary>
            quality: see _GET_PED_QUALITY
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._GET_PED_QUALITY(System.Int32)">
            <summary>
            Returns Ped Quality to be used to calculate Skinning Quality
            
            enum ePedQuality
            {
            	PQ_INVALID = -1,
            	PQ_LOW,
            	PQ_MEDIUM,
            	PQ_HIGH,
            	PQ_MAX
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._SET_TOTAL_PED_DAMAGE_FALLOFF_BONUS(System.Int32,System.Single)">
            <summary>
            _SET_W(EAPON?)*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0x6507AC3BD7C99009(System.Single,System.Single,System.Single,System.Single)">
            <summary>
            _IS_N* - _IS_P*
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._GET_PLAYER_PED_WATER_DEPTH(System.Int32)">
            <summary>
            Returns how deep the water is below the ped (if in water)
            -1.0f = Not in water
            10.0f = Max water depth
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._SET_PED_TRAIL_EFFECT(System.Int32,System.Boolean,System.Single)">
            <summary>
            duration in seconds
            </summary>
        </member>
        <member name="M:RDR2.Native.PED._0xEBAAC9A750E7563B(System.Int32)">
            <summary>
            If returned true: PROCESS_RESIZING_TRACKING_BOUNDS_VOLUME - Scaling UP the bounds due to tracking
            If returned false: PROCESS_RESIZING_TRACKING_BOUNDS_VOLUME - Scaling DOWN the bounds due to tracking
            _IS_PED_T* - _IS_PED_U*
            </summary>
        </member>
        <member name="M:RDR2.Native.PERSISTENCE._0xB03140014ACA6C40(System.UInt64,System.UInt64)">
            <summary>
            nullsub, doesn't do anything
            </summary>
        </member>
        <member name="M:RDR2.Native.PERSISTENCE._PERSISTENCE_REFRESH_TOWN_VOLUME(System.Int32)">
            <summary>
            Only used in R* script long_update.ysc in script function REFRESH_CLOSEST_TOWN
            </summary>
        </member>
        <member name="M:RDR2.Native.PERSISTENCE._0x66DAA3A9274E8E82">
            <summary>
            _PERSISTENCE_*
            </summary>
        </member>
        <member name="M:RDR2.Native.PHYSICS.ADD_ROPE(System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Int32,System.Single,System.Single,System.Single,System.Boolean,System.Boolean,System.Boolean,System.Single,System.Boolean,System.UInt64*,System.Boolean)">
            <summary>
            There are 19 types of rope, from type = 0 to type = 18
            Rope definitions are stored in ropedata.xml
            Rope types 0, 15 and 18 have proper physics for hanging objects (taut, do not sag, small to medium diameter, good aspect for a rope)
            </summary>
        </member>
        <member name="M:RDR2.Native.PHYSICS._BREAK_ROPE(System.Int32*,System.Int32*,System.Int32*,System.Single,System.Single,System.Single,System.Int32)">
            <summary>
            ropeTop returns top half of rope, ropeBottom returns bottom half of rope
            </summary>
        </member>
        <member name="M:RDR2.Native.PHYSICS.ATTACH_ENTITIES_TO_ROPE(System.Int32,System.Int32,System.Int32,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Int32,System.Int32,System.String,System.String,System.Boolean,System.Int32,System.Int32,System.Int32,System.Int32,System.Boolean,System.Boolean)">
            <summary>
            Attaches entity 1 to entity 2.
            If you use a boneName (p12/p13) make sure boneId (p15/p16) is set to -1.
            </summary>
        </member>
        <member name="M:RDR2.Native.PHYSICS._ATTACH_ENTITIES_TO_ROPE_2(System.Int32,System.Int32,System.Int32,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.String,System.String)">
            <summary>
            Attaches a rope to two entities: binds two bones from two entities; one entity can be an object, i.e. a suspension point, the other an NPC bone
            </summary>
        </member>
        <member name="M:RDR2.Native.PHYSICS._CREATE_ROPE_WINDING_ABILITY(System.Int32,System.String,System.String,System.Single,System.Boolean)">
            <summary>
            Combining this with ADD_ROPE enables winding
            p1: mostly empty (0)
            ropeModelType: RB_L_Wrist02, RB_R_Wrist02, ropeAttach, noose01x_Rope_03, SKEL_Neck0, SKEL_L_FOOT, SKEL_Neck1, Root_s_meatbit_Chunck_Xlarge01x
            _CREATE_*
            </summary>
        </member>
        <member name="M:RDR2.Native.PHYSICS.ROPE_FORCE_LENGTH(System.Int32,System.Single)">
            <summary>
            Forces a rope to a certain length.
            </summary>
        </member>
        <member name="M:RDR2.Native.PHYSICS._0x8D59079C37C21D78(System.Int32,System.Single)">
            <summary>
            _ROPE_SET_*
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._0x325434C68358D282(System.Boolean)">
            <summary>
            Only used in script function UPDATE_PLAYER_JUST_DIED_STATE
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER.GET_PLAYER_PED_SCRIPT_INDEX(System.Int32)">
            <summary>
            Does the same like PLAYER::GET_PLAYER_PED
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER.SET_PLAYER_MODEL(System.Int32,System.UInt32,System.Boolean)">
            <summary>
            Make sure to request the model first and wait until it has loaded.
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER._NETWORK_HAS_PLAYER_VALID_PED(System.Int32)">
            <summary>
            Returns true if the given player has a valid ped.
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER.GET_PLAYER_TEAM(System.Int32)">
            <summary>
            Gets the player's team.
            Returns -1 in singleplayer.
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER.SET_PLAYER_TEAM(System.Int32,System.Int32,System.Boolean)">
            <summary>
            Sets the player's team.
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER.SET_PLAYER_WANTED_LEVEL(System.Int32,System.Int32,System.Boolean)">
            <summary>
            nullsub, doesn't do anything
            </summary>
        </member>
        <member name="M:RDR2.Native.PLAYER.CLEAR_PLAYER_WANTED_LEVEL(System.Int32)">
            <summary>
            nullsub, doesn't do anything
            </summary>
```
