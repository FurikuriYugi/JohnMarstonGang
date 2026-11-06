<!-- ScriptHookRDRNetAPI lines 1501-1750 of 30005 -->
```csharp
            Reverses the direction of a given vector.
            </summary>
            <param name="vector">The vector to negate.</param>
            <returns>A vector facing in the opposite direction.</returns>
        </member>
        <member name="M:RDR2.Math.Vector3.op_Multiply(RDR2.Math.Vector3,System.Single)">
            <summary>
            Scales a vector by the given value.
            </summary>
            <param name="vector">The vector to scale.</param>
            <param name="scale">The amount by which to scale the vector.</param>
            <returns>The scaled vector.</returns>
        </member>
        <member name="M:RDR2.Math.Vector3.op_Multiply(System.Single,RDR2.Math.Vector3)">
            <summary>
            Scales a vector by the given value.
            </summary>
            <param name="vector">The vector to scale.</param>
            <param name="scale">The amount by which to scale the vector.</param>
            <returns>The scaled vector.</returns>
        </member>
        <member name="M:RDR2.Math.Vector3.op_Division(RDR2.Math.Vector3,System.Single)">
            <summary>
            Scales a vector by the given value.
            </summary>
            <param name="vector">The vector to scale.</param>
            <param name="scale">The amount by which to scale the vector.</param>
            <returns>The scaled vector.</returns>
        </member>
        <member name="M:RDR2.Math.Vector3.op_Equality(RDR2.Math.Vector3,RDR2.Math.Vector3)">
            <summary>
            Tests for equality between two objects.
            </summary>
            <param name="left">The first value to compare.</param>
            <param name="right">The second value to compare.</param>
            <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        </member>
        <member name="M:RDR2.Math.Vector3.op_Inequality(RDR2.Math.Vector3,RDR2.Math.Vector3)">
            <summary>
            Tests for inequality between two objects.
            </summary>
            <param name="left">The first value to compare.</param>
            <param name="right">The second value to compare.</param>
            <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        </member>
        <member name="M:RDR2.Math.Vector3.op_Implicit(RDR2.Math.Vector3)~RDR2.Math.Vector2">
            <summary>
            Converts a Vector3 to a Vector2 implicitly.
            </summary>
        </member>
        <member name="M:RDR2.Math.Vector3.ToArray">
            <summary>
            Converts the matrix to an array of floats.
            </summary>
        </member>
        <member name="M:RDR2.Math.Vector3.ToString">
            <summary>
            Converts the value of the object to its equivalent string representation.
            </summary>
            <returns>The string representation of the value of this instance.</returns>
        </member>
        <member name="M:RDR2.Math.Vector3.GetHashCode">
            <summary>
            Returns the hash code for this instance.
            </summary>
            <returns>A 32-bit signed integer hash code.</returns>
        </member>
        <member name="M:RDR2.Math.Vector3.Equals(System.Object)">
            <summary>
            Returns a value that indicates whether the current instance is equal to a specified object.
            </summary>
            <param name="obj">Object to make the comparison with.</param>
            <returns><c>true</c> if the current instance is equal to the specified object; <c>false</c> otherwise.</returns>
        </member>
        <member name="M:RDR2.Math.Vector3.Equals(RDR2.Math.Vector3)">
            <summary>
            Returns a value that indicates whether the current instance is equal to the specified object.
            </summary>
            <param name="other">Object to make the comparison with.</param>
            <returns><c>true</c> if the current instance is equal to the specified object; <c>false</c> otherwise.</returns>
        </member>
        <member name="M:RDR2.Math.Vector3.Equals(RDR2.Math.Vector3@,RDR2.Math.Vector3@)">
            <summary>
            Determines whether the specified object instances are considered equal.
            </summary>
            <param name="value1">The first value to compare.</param>
            <param name="value2">The second value to compare.</param>
            <returns><c>true</c> if <paramref name="value1"/> is the same instance as <paramref name="value2"/> or
            if both are <c>null</c> references or if <c>value1.Equals(value2)</c> returns <c>true</c>; otherwise, <c>false</c>.</returns>
        </member>
        <member name="M:RDR2.Native.BUILTIN.TIMERA">
            <summary>
            Counts up. Every 1000 is 1 real-time second. Use SETTIMERA(int value) to set the timer (e.g.: SETTIMERA(0)).
            </summary>
        </member>
        <member name="M:RDR2.Native.BUILTIN.TIMESTEP">
            <summary>
            Gets the current frame time.
            </summary>
        </member>
        <member name="M:RDR2.Native.BUILTIN.LOG10(System.Single)">
            <summary>
            Old name: _LOG10
            </summary>
        </member>
        <member name="M:RDR2.Native.BUILTIN.VMAG(System.Single,System.Single,System.Single)">
            <summary>
            Calculates the magnitude of a vector.
            </summary>
        </member>
        <member name="M:RDR2.Native.BUILTIN.VMAG2(System.Single,System.Single,System.Single)">
            <summary>
            Calculates the magnitude of a vector but does not perform Sqrt operations. (Its way faster)
            </summary>
        </member>
        <member name="M:RDR2.Native.BUILTIN.VDIST(System.Single,System.Single,System.Single,System.Single,System.Single,System.Single)">
            <summary>
            Calculates distance between vectors.
            The value returned will be in meters.
            </summary>
        </member>
        <member name="M:RDR2.Native.BUILTIN.VDIST2(System.Single,System.Single,System.Single,System.Single,System.Single,System.Single)">
            <summary>
            Calculates distance between vectors but does not perform Sqrt operations. (Its way faster)
            The value returned will be in RAGE units.
            </summary>
        </member>
        <member name="M:RDR2.Native.BUILTIN.FLOOR(System.Single)">
            <summary>
            Rounds a float value down to the next whole number
            </summary>
        </member>
        <member name="M:RDR2.Native.BUILTIN.CEIL(System.Single)">
            <summary>
            Rounds a float value up to the next whole number
            </summary>
        </member>
        <member name="M:RDR2.Native.BUILTIN.SET_THIS_THREAD_PRIORITY(System.Int32)">
            <summary>
            THREAD_PRIO_HIGHEST = 0
            THREAD_PRIO_NORMAL = 1
            THREAD_PRIO_LOWEST = 2
            THREAD_PRIO_MANUAL_UPDATE = 100
            </summary>
        </member>
        <member name="M:RDR2.Native.AICOVERPOINT._0x64340DC208D671D5(System.String)">
            <summary>
            coverLayer: see levels_0/levels/rdr3/coverlayers
            </summary>
        </member>
        <member name="M:RDR2.Native.AICOVERPOINT._0x7A1FDCF35EAA140F(System.String)">
            <summary>
            coverLayer: see levels_0/levels/rdr3/coverlayers
            </summary>
        </member>
        <member name="M:RDR2.Native.AICOVERPOINT._REQUEST_FLINCH_COVER_ANIM(System.Int32)">
            <summary>
            Makes ped flinch (if in cover) like they have been shot at
            </summary>
        </member>
        <member name="M:RDR2.Native.AICOVERPOINT._0x140B3CB1D424A945(System.Int32,System.UInt32)">
            <summary>
            weaponHash can also be -1
            </summary>
        </member>
        <member name="M:RDR2.Native.AICOVERPOINT._STOP_RUNNING_COVER_ANIMS(System.Int32)">
            <summary>
            Stops running cover anims and releases them
            _STOP_RENDERING_* - _STOP_SCRIPTED*
            </summary>
        </member>
        <member name="M:RDR2.Native.AICOVERPOINT._0x733077295AB51304(System.UInt64*)">
            <summary>
            args: f_0 = Volume Handle, f_2 = integer (-1, 32 used in R* Scripts)
            </summary>
        </member>
        <member name="M:RDR2.Native.AITRANSPORT.SET_PED_ON_TRANSPORT_SEAT(System.Int32,System.Int32,System.Int32,System.Int32)">
            <summary>
            seat: see CREATE_PED_INSIDE_VEHICLE
            </summary>
        </member>
        <member name="M:RDR2.Native.AITRANSPORT.SET_TRANSPORT_CONFIG_FLAG(System.Int32,System.Int32,System.Boolean)">
            <summary>
            flagId:
            enum eTransportConfigFlags
            {
            	TCF_NotConsideredForEntryByLocalPlayer,
            	TCF_0xB78D6624,
            	TCF_0xA9700425,
            	TCF_0x8D7E4641,
            	TCF_0xF24BAA1F,
            	TCF_0x63B77935,
            	TCF_NotConsideredForEntryByAllPlayers,
            	TCF_0xD17A2AFD,
            	TCF_0xD4E4FDD5,
            	TCF_0x8227C929,
            	TCF_0x812C1070,
            	TCF_0x0E1AB26F,
            	TCF_0xBF4EC863,
            	TCF_0x75660C36,
            	TCF_0xA2539E20,
            	TCF_0x9162C633,
            	TCF_DisableHonorModifiers,
            	TCF_0xF9E71CB6,
            	TCF_0x933ECD3F,
            	TCF_0x18513A34
            };
            https://github.com/femga/rdr3_discoveries/tree/master/AI/TRANSPORT_CONFIG_FLAGS
            </summary>
        </member>
        <member name="M:RDR2.Native.AITRANSPORT.GET_TRANSPORT_CONFIG_FLAG(System.Int32,System.Int32,System.Boolean)">
            <summary>
            flagId: see SET_TRANSPORT_CONFIG_FLAG
            </summary>
        </member>
        <member name="M:RDR2.Native.AITRANSPORT._GET_TRANSPORT_USAGE_FLAGS(System.Int32,System.Int32*)">
            <summary>
            See _SET_TRANSPORT_USAGE_FLAGS
            </summary>
        </member>
        <!-- Badly formed XML comment ignored for member "M:RDR2.Native.AITRANSPORT._SET_TRANSPORT_USAGE_FLAGS(System.Int32,System.Int32)" -->
        <member name="M:RDR2.Native.AITRANSPORT._IS_PED_ON_TRANSPORT_ENTITY(System.Int32,System.Int32)">
            <summary>
            Checks if ped is placed on target transportEntity
            </summary>
        </member>
        <member name="M:RDR2.Native.AITRANSPORT._IS_TRANSPORT_SEAT_FREE(System.Int32,System.Int32)">
            <summary>
            Called together with IS_VEHICLE_SEAT_FREE
            </summary>
        </member>
        <member name="M:RDR2.Native.AITRANSPORT._GET_PED_IN_TRANSPORT_SEAT(System.Int32,System.Int32)">
            <summary>
            seatIndex: see CREATE_PED_INSIDE_VEHICLE
            </summary>
        </member>
        <member name="M:RDR2.Native.AITRANSPORT._0x4248AB2EEB3C75AD(System.Int32,System.Int32,System.Boolean)">
            <summary>
            _SET_TRANSPORT_*
            </summary>
        </member>
        <member name="M:RDR2.Native.AITRANSPORT._0x5639FBEA922788DA(System.Int32)">
            <summary>
            _CLEAR_A*
            </summary>
        </member>
        <member name="M:RDR2.Native.AITRANSPORT._0x8C8371EDFAF014A0(System.Int32,System.Single)">
            <summary>
            _SET_TRANSPORT_*
            </summary>
```
