<!-- ScriptHookRDRNetAPI lines 10251-10500 of 30005 -->
```csharp
            Same as GET_IS_LOADING_SCREEN_ACTIVE
            </summary>
        </member>
        <member name="M:RDR2.Native.SCRIPTS.BG_IS_EXITFLAG_SET">
            <summary>
            Returns true if GtaThread+0x77C is equal to 1.
            
            Old name: _BG_EXITED_BECAUSE_BACKGROUND_THREAD_STOPPED
            </summary>
        </member>
        <member name="M:RDR2.Native.SCRIPTS.BG_SET_EXITFLAG_RESPONSE">
            <summary>
            Sets bit 0 in GtaThread+0x784
            </summary>
        </member>
        <member name="M:RDR2.Native.SCRIPTS.BG_START_CONTEXT_HASH(System.UInt32)">
            <summary>
            Hashed version of BG_START_CONTEXT
            </summary>
        </member>
        <member name="M:RDR2.Native.SCRIPTS.BG_END_CONTEXT_HASH(System.UInt32)">
            <summary>
            Hashed version of BG_END_CONTEXT
            </summary>
        </member>
        <member name="M:RDR2.Native.SCRIPTS.BG_START_CONTEXT(System.String)">
            <summary>
            Inserts the given context into the background scripts context map.
            </summary>
        </member>
        <member name="M:RDR2.Native.SCRIPTS.BG_END_CONTEXT(System.String)">
            <summary>
            Deletes the given context from the background scripts context map.
            </summary>
        </member>
        <!-- Badly formed XML comment ignored for member "M:RDR2.Native.SCRIPTS._ACTIVATE_GOAL_CONTEXT(System.UInt32)" -->
        <member name="M:RDR2.Native.SCRIPTS._DEACTIVATE_GOAL_CONTEXT(System.UInt32)">
            <summary>
            goalContext: see _ACTIVATE_GOAL_CONTEXT
            </summary>
        </member>
        <member name="M:RDR2.Native.SCRIPTS._IS_GOAL_CONTEXT_ACTIVE(System.UInt32)">
            <summary>
            goalContext: see _ACTIVATE_GOAL_CONTEXT
            </summary>
        </member>
        <member name="M:RDR2.Native.SCRIPTS._NET_RPC_GUID_TO_STRING(System.UInt64*)">
            <summary>
            Returns "INVALID_NET_RPC_GUID" if netRpcGuid is invalid.
            </summary>
        </member>
        <member name="M:RDR2.Native.SCRIPTS.HAVE_ALL_CHILD_SCRIPTS_TERMINATED(System.Int32)">
            <summary>
            Waiting for child scripts to terminate / waiting for collapse of child scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.SCRIPTS.START_NEW_SCRIPT_WITH_ARGS(System.String,System.UInt64*,System.Int32,System.Int32)">
            <summary>
            return : script thread id, 0 if failed
            Pass pointer to struct of args in p1, size of struct goes into p2
            </summary>
        </member>
        <member name="M:RDR2.Native.SAVE.SAVEGAME_SAVE_SP(System.UInt32)">
            <summary>
            enum eSavegameType : Hash
            {
            	SAVEGAMETYPE_AMBIENT = 0x3CA4E1F8,
            	SAVEGAMETYPE_DEFAULT = 0xCB6ED080,
            	SAVEGAMETYPE_DELETE_CHAR = 0xCD35F947,
            	SAVEGAMETYPE_END_CREATE_NEWCHAR = 0x4C50A3CE,
            	SAVEGAMETYPE_END_MATCH = 0xE470ED50,
            	SAVEGAMETYPE_END_MISSION = 0x9A444E54,
            	SAVEGAMETYPE_END_SESSION = 0x6D23956C,
            	SAVEGAMETYPE_END_SHOPPING = 0xA311A6C4,
            	SAVEGAMETYPE_RANKUP = 0xE25F8017,
            	SAVEGAMETYPE_SCRIPT_MP_GLOBALS = 0xAFF30AD4,
            	SAVEGAMETYPE_SP_AUTOSAVE = 0xF4AE69EC,
            	SAVEGAMETYPE_SP_DEBUG = 0x6A8122FD,
            	SAVEGAMETYPE_SP_PROPERTY = 0xAE0AB38E
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.SAVE.SAVEGAME_SAVE_MP(System.UInt32)">
            <summary>
            See SAVEGAME_SAVE_SP
            </summary>
        </member>
        <member name="M:RDR2.Native.SAVE._SAVEGAME_GET_INT_2(System.UInt64*,System.String)">
            <summary>
            Does the exact same as 0x529B9CCD0972AF4E
            Commonly used with time/timestamps
            </summary>
        </member>
        <member name="M:RDR2.Native.SAVE._SAVEGAME_GET_INT_3(System.UInt64*,System.String)">
            <summary>
            Does the exact same as 0x529B9CCD0972AF4E
            Commonly used with enums and flags
            </summary>
        </member>
        <member name="M:RDR2.Native.SAVE._SAVEGAME_GET_BOOL(System.UInt64*,System.String)">
            <summary>
            Does the exact same as 0x529B9CCD0972AF4E
            </summary>
        </member>
        <member name="M:RDR2.Native.SHAPETEST.START_SHAPE_TEST_LOS_PROBE(System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Int32,System.Int32,System.Int32)">
            <summary>
            Asynchronously starts a line-of-sight (raycast) world probe shape test.
            
            Use the handle with 0x3D87450E15D98694 or 0x65287525D951F6BE until it returns 0 or 2.
            
            p8 is a bit mask with bits 1, 2 and/or 4, relating to collider types; 4 should usually be used.
            </summary>
        </member>
        <member name="M:RDR2.Native.SHAPETEST.START_EXPENSIVE_SYNCHRONOUS_SHAPE_TEST_LOS_PROBE(System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Int32,System.Int32,System.Int32)">
            <summary>
            Does the same as 0x7EE9F5D83DD4F90E, except blocking until the shape test completes.
            </summary>
        </member>
        <member name="M:RDR2.Native.SHAPETEST.START_SHAPE_TEST_MOUSE_CURSOR_LOS_PROBE(RDR2.Math.Vector3*,RDR2.Math.Vector3*,System.Int32,System.Int32,System.Int32)">
            <summary>
            Old name: _START_SHAPE_TEST_SURROUNDING_COORDS
            </summary>
        </member>
        <member name="M:RDR2.Native.SHAPETEST.GET_SHAPE_TEST_RESULT(System.Int32,System.Boolean*,RDR2.Math.Vector3*,RDR2.Math.Vector3*,System.Int32*)">
            <summary>
            Returns the result of a shape test: 0 if the handle is invalid, 1 if the shape test is still pending, or 2 if the shape test has completed, and the handle should be invalidated.
            
            When used with an asynchronous shape test, this native should be looped until returning 0 or 2, after which the handle is invalidated.
            
            enum eShapeTestStatus
            {
            	SHAPETEST_STATUS_NONEXISTENT,
            	SHAPETEST_STATUS_RESULTS_NOTREADY,
            	SHAPETEST_STATUS_RESULTS_READY
            };
            </summary>
        </member>
        <member name="M:RDR2.Native.SOCIALCLUB.SC_PROFANITY_CHECK_STRING(System.String,System.Int32*)">
            <summary>
            Starts a task to check an entered string for profanity on the ROS/Social Club services.
            </summary>
        </member>
        <member name="M:RDR2.Native.STATS.STAT_ID_IS_VALID(System.UInt64*)">
            <summary>
            struct StatId
            {
            	alignas(8) Hash BaseId;
            	alignas(8) Hash PermutationId;
            }
            </summary>
        </member>
        <member name="M:RDR2.Native.STATS.STAT_ID_SET_INT(System.UInt64*,System.Int32,System.Boolean)">
            <summary>
            statId: see STAT_ID_IS_VALID
            </summary>
        </member>
        <member name="M:RDR2.Native.STATS.STAT_ID_SET_FLOAT(System.UInt64*,System.Single,System.Boolean)">
            <summary>
            statId: see STAT_ID_IS_VALID
            </summary>
        </member>
        <member name="M:RDR2.Native.STATS.STAT_ID_SET_BOOL(System.UInt64*,System.Boolean,System.Boolean)">
            <summary>
            statId: see STAT_ID_IS_VALID
            </summary>
        </member>
        <member name="M:RDR2.Native.STATS.STAT_ID_SET_GXT_LABEL(System.UInt64*,System.String,System.Boolean)">
            <summary>
            statId: see STAT_ID_IS_VALID
            </summary>
        </member>
        <member name="M:RDR2.Native.STATS.STAT_ID_SET_DATE(System.UInt64*,System.UInt64*,System.Boolean)">
            <summary>
            statId: see STAT_ID_IS_VALID
            </summary>
        </member>
        <member name="M:RDR2.Native.STATS.STAT_ID_GET_INT(System.UInt64*,System.Int32*)">
            <summary>
            statId: see STAT_ID_IS_VALID
            </summary>
        </member>
        <member name="M:RDR2.Native.STATS.STAT_ID_GET_FLOAT(System.UInt64*,System.Single*)">
            <summary>
            statId: see STAT_ID_IS_VALID
            </summary>
        </member>
        <member name="M:RDR2.Native.STATS.STAT_ID_GET_BOOL(System.UInt64*,System.Boolean*)">
            <summary>
            statId: see STAT_ID_IS_VALID
            </summary>
        </member>
        <member name="M:RDR2.Native.STATS.STAT_ID_GET_DATE(System.UInt64*,System.UInt64*)">
            <summary>
            statId: see STAT_ID_IS_VALID
            </summary>
        </member>
        <member name="M:RDR2.Native.STATS._0x0FEE2561120F3333(System.UInt64*)">
            <summary>
            statId: see STAT_ID_IS_VALID
            </summary>
        </member>
        <member name="M:RDR2.Native.STATS._STAT_ID_INCREMENT_INT(System.UInt64*,System.Int32)">
            <summary>
            statId: see STAT_ID_IS_VALID
            </summary>
        </member>
        <member name="M:RDR2.Native.STATS._STAT_ID_INCREMENT_FLOAT(System.UInt64*,System.Single)">
            <summary>
            statId: see STAT_ID_IS_VALID
            </summary>
        </member>
        <member name="M:RDR2.Native.STATS._STAT_ID_DECREMENT_INT(System.UInt64*,System.Int32)">
            <summary>
            statId: see STAT_ID_IS_VALID
            </summary>
        </member>
        <member name="M:RDR2.Native.STATS._0x91A4F58E01ED5E4C(System.UInt64*,System.Int32)">
            <summary>
            statId: see STAT_ID_IS_VALID
            </summary>
        </member>
        <member name="M:RDR2.Native.STATS._0xE141F6B40B1E3683(System.UInt64*,System.Single)">
            <summary>
            statId: see STAT_ID_IS_VALID
            Only used in R* SP Scripts
            _STAT_ID_SET_*
            </summary>
        </member>
        <member name="M:RDR2.Native.STATS.STAT_ID_SET_TO_POSSE_ID(System.UInt64*)">
            <summary>
            statId: see STAT_ID_IS_VALID
            </summary>
        </member>
        <member name="M:RDR2.Native.STATS._STAT_CALCULATE_COOLDOWN(System.Int32)">
            <summary>
            Calculation: (value / 1000) / 60 % 60
            </summary>
        </member>
        <member name="M:RDR2.Native.STATS._STAT_PHEROMONE_COOLDOWN_LEGENDARY_ANIMAL(System.Int32,System.UInt64*)">
            <summary>
            statId: see STAT_ID_IS_VALID
            </summary>
        </member>
        <member name="M:RDR2.Native.STATS._0x8312F09C56149A8A(System.UInt32)">
            <summary>
            Only used in R* SP Scripts
            </summary>
        </member>
        <member name="M:RDR2.Native.STATS._0xA59590050F80FF2E(System.UInt64,System.Boolean,System.Boolean,System.Boolean)">
            <summary>
```
