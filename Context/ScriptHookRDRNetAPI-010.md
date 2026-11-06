<!-- ScriptHookRDRNetAPI lines 2251-2500 of 30005 -->
```csharp
            https://github.com/femga/rdr3_discoveries/blob/master/audio/music_events/music_events.lua
            </summary>
        </member>
        <member name="M:RDR2.Native.AUDIO.SET_PED_WALLA_DENSITY(System.Single,System.Single)">
            <summary>
            https://en.m.wikipedia.org/wiki/Walla
            </summary>
        </member>
        <member name="M:RDR2.Native.AUDIO.SET_AUDIO_FLAG(System.String,System.Boolean)">
            <summary>
            https://github.com/femga/rdr3_discoveries/tree/master/audio/audio_flags
            </summary>
        </member>
        <member name="M:RDR2.Native.AUDIO._GET_LOADED_STREAM_ID_FROM_CREATION(System.String,System.String)">
            <summary>
            Creates stream and returns streamId handle to be used with PLAY_STREAM_* natives
            https://github.com/femga/rdr3_discoveries/tree/master/audio/create_stream
            </summary>
        </member>
        <member name="M:RDR2.Native.BOUNTY._BOUNTY_REQUEST_SELF_REPORT_CRIME(System.UInt64*,System.UInt32,System.Boolean)">
            <summary>
            crimeType: see _REPORT_CRIME
            </summary>
        </member>
        <member name="M:RDR2.Native.BOUNTY._0x86EC5F83867C4B70(System.UInt64*)">
            <summary>
            _BOUNTY_C* or _BOUNTY_GET_*
            </summary>
        </member>
        <member name="M:RDR2.Native.BOUNTY._0x27D3A0E1FE090A43(System.UInt64*)">
            <summary>
            _BOUNTY_IS_* or _BOUNTY_REQUEST_*
            </summary>
        </member>
        <member name="M:RDR2.Native.BOUNTY._0xF8BCC5ECA33AC9C1">
            <summary>
            _BOUNTY_GET_*
            </summary>
        </member>
        <member name="M:RDR2.Native.BOUNTY._0xD6A67E2FF373D0E3(System.Int32)">
            <summary>
            _BOUNTY_GET_*
            </summary>
        </member>
        <member name="M:RDR2.Native.BRAIN.REGISTER_OBJECT_SCRIPT_BRAIN(System.String,System.UInt32,System.Int32,System.Single,System.Int32,System.Int32)">
            <summary>
            Registers a script for any object with a specific model hash.
            </summary>
        </member>
        <member name="M:RDR2.Native.BRAIN._START_PRELOADED_SCRIPT_BRAIN(System.Int32,System.String,System.Int32,System.Boolean)">
            <summary>
            Returns threadId
            </summary>
        </member>
        <member name="M:RDR2.Native.BRAIN._START_SCRIPT_BRAIN(System.Int32,System.String,System.Int32,System.UInt64*,System.Int32,System.Boolean)">
            <summary>
            Returns threadId
            </summary>
        </member>
        <member name="M:RDR2.Native.BRAIN._0xA6AC35DB4A7957A8(System.Int32)">
            <summary>
            Common flags: 250, 99999
            _SET_SCRIPT_BRAIN*
            </summary>
        </member>
        <member name="M:RDR2.Native.BRAIN._0x4AA5EA1EDFB25786(System.Int32)">
            <summary>
            Called with flag 0 before 0xA6AC35DB4A7957A8 in net_entity_brain
            _SET_SCRIPT_BRAIN*
            </summary>
        </member>
        <member name="M:RDR2.Native.BRAIN.REACTIVATE_ALL_OBJECT_BRAINS_THAT_ARE_WAITING_TILL_OUT_OF_RANGE">
            <summary>
            Called before starting a new thread_monitor script thread in startup_mp/startup_tlg
            Alternative name _REGISTER_SCRIPT_BRAIN
            
            Old name: _PREPARE_SCRIPT_BRAIN
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM.RENDER_SCRIPT_CAMS(System.Boolean,System.Boolean,System.Int32,System.Boolean,System.Boolean,System.Int32)">
            <summary>
            ease - smooth transition between the camera's positions
            easeTime - Time in milliseconds for the transition to happen
            
            If you have created a script (rendering) camera, and want to go back to the 
            character (gameplay) camera, call this native with render set to FALSE.
            Setting ease to TRUE will smooth the transition.
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM.STOP_RENDERING_SCRIPT_CAMS_USING_CATCH_UP(System.Boolean,System.Single,System.Int32,System.Boolean,System.Boolean,System.Boolean)">
            <summary>
            This native makes the gameplay camera zoom into first person/third person with a special effect.
            blendBackSmoothingType: https://github.com/Halen84/RDR3-Native-Flags-And-Enums/tree/main/eBlendBackSmoothing
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM.DESTROY_CAM(System.Int32,System.Boolean)">
            <summary>
            BOOL param indicates whether the cam should be destroyed if it belongs to the calling script.
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM.DESTROY_ALL_CAMS(System.Boolean)">
            <summary>
            BOOL param indicates whether the cam should be destroyed if it belongs to the calling script.
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM.DOES_CAM_EXIST(System.Int32)">
            <summary>
            Returns whether or not the passed camera handle exists.
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM.SET_CAM_ACTIVE(System.Int32,System.Boolean)">
            <summary>
            Set camera as active/inactive.
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM.IS_CAM_ACTIVE(System.Int32)">
            <summary>
            Returns whether or not the passed camera handle is active.
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM.GET_CAM_ROT(System.Int32,System.Int32)">
            <summary>
            rotationOrder: https://github.com/Halen84/RDR3-Native-Flags-And-Enums/tree/main/eEulerRotationOrder
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM.SET_CAM_COORD(System.Int32,System.Single,System.Single,System.Single)">
            <summary>
            Sets the position of the cam.
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM.SET_CAM_ROT(System.Int32,System.Single,System.Single,System.Single,System.Int32)">
            <summary>
            Sets the rotation of the cam.
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM.SET_CAM_FOV(System.Int32,System.Single)">
            <summary>
            Sets the field of view of the cam.
            
            Min: 1.0f
            Max: 130.0f
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM.ATTACH_CAM_TO_ENTITY(System.Int32,System.Int32,System.Single,System.Single,System.Single,System.Boolean)">
            <summary>
            Last param determines if its relative to the Entity
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM.ATTACH_CAM_TO_PED_BONE(System.Int32,System.Int32,System.Int32,System.Single,System.Single,System.Single,System.Boolean)">
            <summary>
            boneIndex: https://github.com/femga/rdr3_discoveries/tree/master/boneNames
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM.SET_CAM_AFFECTS_AIMING(System.Int32,System.Boolean)">
            <summary>
            Allows you to aim and shoot at the direction the camera is facing.
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM.ADD_CAM_SPLINE_NODE(System.Int32,System.Single,System.Single,System.Single,System.Single,System.Single,System.Single,System.Int32,System.Int32,System.Int32)">
            <summary>
            p7 (length) determines the length of the spline, affects camera path and duration of transition between previous node and this one
            
            p8 big values ~100 will slow down the camera movement before reaching this node
            
            p9 != 0 seems to override the rotation/pitch (bool?)
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM.GET_CAM_SPLINE_PHASE(System.Int32)">
            <summary>
            Can use this with SET_CAM_SPLINE_PHASE to set the float it this native returns.
            
            (returns 1.0f when no nodes has been added, reached end of non existing spline)
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM._IS_ANIM_SCENE_CAM_ACTIVE">
            <summary>
            Only used in R* Script camera_photomode
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM.DO_SCREEN_FADE_IN(System.Int32)">
            <summary>
            Fades the screen in.
            
            duration: The time the fade should take, in milliseconds.
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM.DO_SCREEN_FADE_OUT(System.Int32)">
            <summary>
            Fades the screen out.
            
            duration: The time the fade should take, in milliseconds.
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM.HAS_LETTER_BOX">
            <summary>
            More info: https://en.wikipedia.org/wiki/Letterboxing_(filming)
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM._REQUEST_LETTER_BOX_NOW(System.Boolean,System.Boolean)">
            <summary>
            Creates Cinematic Black Bars (at top and bottom)
            Disable instantly: false/false, Enable instantly: true/true
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM.GET_LETTER_BOX_RATIO">
            <summary>
            More info: see HAS_LETTER_BOX
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM.SET_GAMEPLAY_CAM_RELATIVE_HEADING(System.Single,System.Single)">
            <summary>
            Sets the camera position relative to heading in float from -360 to +360.
            
            Heading is always 0 in aiming camera.
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM.SET_GAMEPLAY_CAM_RELATIVE_PITCH(System.Single,System.Single)">
            <summary>
            Sets the camera pitch.
            
            Parameters:
            x = pitches the camera on the x axis.
            Value2 = always seems to be hex 0x3F800000 (1.000000 float).
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM._IS_IN_FULL_FIRST_PERSON_MODE">
            <summary>
            Returns true if player is in first person
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM._0xC3E9E5D4F413B773(System.String,System.Single)">
            <summary>
            shakeName: REINFORCED_LASSO_STRUGGLE_SHAKE
            _RES*
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM._0x0060B31968E60E41(System.String)">
            <summary>
            shakeNames in script_rel: CORRECTOR_SHAKE, MINIGAME_BOUNTY_SHAKE, POV_DRUNK_SHAKE, DRUNK_SHAKE, MINIGAME_TRAIN_SHAKE
            _IS_GAMEPLAY_*
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM.SET_GAMEPLAY_CAM_SHAKE_AMPLITUDE(System.Single)">
            <summary>
            Sets the amplitude for the gameplay (i.e. 3rd or 1st) camera to shake.
            </summary>
        </member>
        <member name="M:RDR2.Native.CAM._0xFEFDDC6E8FDF8A75(System.String,System.Single)">
            <summary>
            _SET_GAMEPLAY_P* - _SET_GAMEPLAY_V*
```
