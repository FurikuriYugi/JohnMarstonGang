<!-- ScriptHookRDRNetAPI lines 501-750 of 30005 -->
```csharp
            Conjugates the quaternion.
            </summary>
        </member>
        <member name="M:RDR2.Math.Quaternion.Invert">
            <summary>
            Conjugates and renormalizes the quaternion.
            </summary>
        </member>
        <member name="M:RDR2.Math.Quaternion.Negate(RDR2.Math.Quaternion)">
            <summary>
            Reverses the direction of a given quaternion.
            </summary>
            <param name="quaternion">The quaternion to negate.</param>
            <returns>A quaternion facing in the opposite direction.</returns>
        </member>
        <member name="M:RDR2.Math.Quaternion.Add(RDR2.Math.Quaternion,RDR2.Math.Quaternion)">
            <summary>
            Adds two quaternions.
            </summary>
            <param name="left">The first quaternion to add.</param>
            <param name="right">The second quaternion to add.</param>
            <returns>The sum of the two quaternions.</returns>
        </member>
        <member name="M:RDR2.Math.Quaternion.Subtract(RDR2.Math.Quaternion,RDR2.Math.Quaternion)">
            <summary>
            Subtracts two quaternions.
            </summary>
            <param name="left">The first quaternion to subtract.</param>
            <param name="right">The second quaternion to subtract.</param>
            <returns>The difference of the two quaternions.</returns>
        </member>
        <member name="M:RDR2.Math.Quaternion.Multiply(RDR2.Math.Quaternion,RDR2.Math.Quaternion)">
            <summary>
            Modulates a quaternion by another.
            </summary>
            <param name="left">The first quaternion to modulate.</param>
            <param name="right">The second quaternion to modulate.</param>
            <returns>The modulated quaternion.</returns>
        </member>
        <member name="M:RDR2.Math.Quaternion.Multiply(RDR2.Math.Quaternion,System.Single)">
            <summary>
            Scales a quaternion by the given value.
            </summary>
            <param name="quaternion">The quaternion to scale.</param>
            <param name="scale">The amount by which to scale the quaternion.</param>
            <returns>The scaled quaternion.</returns>
        </member>
        <member name="M:RDR2.Math.Quaternion.Divide(RDR2.Math.Quaternion,RDR2.Math.Quaternion)">
            <summary>
            Divides a quaternion by another.
            </summary>
            <param name="left">The first quaternion to divide.</param>
            <param name="right">The second quaternion to divide.</param>
            <returns>The divided quaternion.</returns>
        </member>
        <member name="M:RDR2.Math.Quaternion.Normalize(RDR2.Math.Quaternion)">
            <summary>
            Converts the quaternion into a unit quaternion.
            </summary>
            <param name="quaternion">The quaternion to normalize.</param>
            <returns>The normalized quaternion.</returns>
        </member>
        <member name="M:RDR2.Math.Quaternion.Conjugate(RDR2.Math.Quaternion)">
            <summary>
            Creates the conjugate of a specified Quaternion.
            </summary>
            <param name="value">The Quaternion of which to return the conjugate.</param>
            <returns>A new Quaternion that is the conjugate of the specified one.</returns>
        </member>
        <member name="M:RDR2.Math.Quaternion.Invert(RDR2.Math.Quaternion)">
            <summary>
            Conjugates and renormalizes the quaternion.
            </summary>
            <param name="quaternion">The quaternion to conjugate and re-normalize.</param>
            <returns>The conjugated and renormalized quaternion.</returns>
        </member>
        <member name="M:RDR2.Math.Quaternion.Dot(RDR2.Math.Quaternion,RDR2.Math.Quaternion)">
            <summary>
            Calculates the dot product of two quaternions.
            </summary>
            <param name="left">First source quaternion.</param>
            <param name="right">Second source quaternion.</param>
            <returns>The dot product of the two quaternions.</returns>
        </member>
        <member name="M:RDR2.Math.Quaternion.Lerp(RDR2.Math.Quaternion,RDR2.Math.Quaternion,System.Single)">
            <summary>
            Performs a linear interpolation between two quaternion.
            </summary>
            <param name="start">Start quaternion.</param>
            <param name="end">End quaternion.</param>
            <param name="amount">Value between 0 and 1 indicating the weight of <paramref name="end"/>.</param>
            <returns>The linear interpolation of the two quaternions.</returns>
            <remarks>
            This method performs the linear interpolation based on the following formula.
            <code>start + (end - start) * amount</code>
            Passing <paramref name="amount"/> a value of 0 will cause <paramref name="start"/> to be returned; a value of 1 will cause <paramref name="end"/> to be returned.
            </remarks>
        </member>
        <member name="M:RDR2.Math.Quaternion.Slerp(RDR2.Math.Quaternion,RDR2.Math.Quaternion,System.Single)">
            <summary>
            Interpolates between two quaternions, using spherical linear interpolation..
            </summary>
            <param name="start">Start quaternion.</param>
            <param name="end">End quaternion.</param>
            <param name="amount">Value between 0 and 1 indicating the weight of <paramref name="end"/>.</param>
            <returns>The spherical linear interpolation of the two quaternions.</returns>
        </member>
        <member name="M:RDR2.Math.Quaternion.SlerpUnclamped(RDR2.Math.Quaternion,RDR2.Math.Quaternion,System.Single)">
            <summary>
            Interpolates between two quaternions, using spherical linear interpolation. The parameter /t/ is not clamped.
            </summary>
            <param name="a"></param>
            <param name="b"></param>
            <param name="t"></param>
        </member>
        <member name="M:RDR2.Math.Quaternion.FromToRotation(RDR2.Math.Vector3,RDR2.Math.Vector3)">
            <summary>
            Creates a rotation which rotates from fromDirection to toDirection.
            </summary>
        </member>
        <member name="M:RDR2.Math.Quaternion.RotateTowards(RDR2.Math.Quaternion,RDR2.Math.Quaternion,System.Single)">
            <summary>
            Rotates a rotation from towards to.
            </summary>
            <param name="from">From Quaternion.</param>
            <param name="to">To Quaternion.</param>
            <param name ="maxDegreesDelta"></param>
        </member>
        <member name="M:RDR2.Math.Quaternion.AngleBetween(RDR2.Math.Quaternion,RDR2.Math.Quaternion)">
            <summary>
            Returns the angle in degrees between two rotations a and b.
            </summary>
            <param name="a">The first quaternion to calculate angle.</param>
            <param name="b">The second quaternion to calculate angle.</param>
            <returns>The angle in degrees between two rotations a and b.</returns>
        </member>
        <member name="M:RDR2.Math.Quaternion.Euler(System.Single,System.Single,System.Single)">
            <summary>
            Returns a rotation that rotates z degrees around the z axis, x degrees around the x axis, and y degrees around the y axis (in that order).
            </summary>
            <param name="zaxis">Z degrees.</param>
            <param name ="xaxis">X degrees.</param>
            <param name ="yaxis">Y degrees.</param>
        </member>
        <member name="M:RDR2.Math.Quaternion.Euler(RDR2.Math.Vector3)">
            <summary>
            Returns a rotation that rotates z degrees around the z axis, x degrees around the x axis, and y degrees around the y axis (in that order).
            </summary>
            <param name="euler">Euler angles in degrees. euler.X = around X axis, euler.Y = around Y axis, euler.Z = around Z axis</param>
        </member>
        <member name="M:RDR2.Math.Quaternion.RotationAxis(RDR2.Math.Vector3,System.Single)">
            <summary>
            Creates a quaternion given a rotation and an axis.
            </summary>
            <param name="axis">The axis of rotation.</param>
            <param name="angle">The angle of rotation in radians.</param>
            <returns>The newly created quaternion.</returns>
        </member>
        <member name="M:RDR2.Math.Quaternion.RotationMatrix(RDR2.Math.Matrix)">
            <summary>
            Creates a quaternion given a rotation matrix.
            </summary>
            <param name="matrix">The rotation matrix.</param>
            <returns>The newly created quaternion.</returns>
        </member>
        <member name="M:RDR2.Math.Quaternion.RotationYawPitchRoll(System.Single,System.Single,System.Single)">
            <summary>
            Creates a Quaternion from the given yaw, pitch, and roll, in radians.
            </summary>
            <param name="yaw">The yaw angle, in radians, around the Z-axis.</param>
            <param name="pitch">The pitch angle, in radians, around the X-axis.</param>
            <param name="roll">The roll angle, in radians, around the Y-axis.</param>
            <returns>The newly created quaternion.</returns>
        </member>
        <member name="M:RDR2.Math.Quaternion.op_UnaryNegation(RDR2.Math.Quaternion)">
            <summary>
            Reverses the direction of a given quaternion.
            </summary>
            <param name="quaternion">The quaternion to negate.</param>
            <returns>A quaternion facing in the opposite direction.</returns>
        </member>
        <member name="M:RDR2.Math.Quaternion.op_Addition(RDR2.Math.Quaternion,RDR2.Math.Quaternion)">
            <summary>
            Adds two quaternions.
            </summary>
            <param name="left">The first quaternion to add.</param>
            <param name="right">The second quaternion to add.</param>
            <returns>The sum of the two quaternions.</returns>
        </member>
        <member name="M:RDR2.Math.Quaternion.op_Subtraction(RDR2.Math.Quaternion,RDR2.Math.Quaternion)">
            <summary>
            Subtracts two quaternions.
            </summary>
            <param name="left">The first quaternion to subtract.</param>
            <param name="right">The second quaternion to subtract.</param>
            <returns>The difference of the two quaternions.</returns>
        </member>
        <member name="M:RDR2.Math.Quaternion.op_Multiply(RDR2.Math.Quaternion,RDR2.Math.Quaternion)">
            <summary>
            Multiplies a quaternion by another.
            </summary>
            <param name="left">The first quaternion to multiply.</param>
            <param name="right">The second quaternion to multiply.</param>
            <returns>The multiplied quaternion.</returns>
        </member>
        <member name="M:RDR2.Math.Quaternion.op_Multiply(RDR2.Math.Quaternion,System.Single)">
            <summary>
            Scales a quaternion by the given value.
            </summary>
            <param name="quaternion">The quaternion to scale.</param>
            <param name="scale">The amount by which to scale the quaternion.</param>
            <returns>The scaled quaternion.</returns>
        </member>
        <member name="M:RDR2.Math.Quaternion.op_Multiply(System.Single,RDR2.Math.Quaternion)">
            <summary>
            Scales a quaternion by the given value.
            </summary>
            <param name="quaternion">The quaternion to scale.</param>
            <param name="scale">The amount by which to scale the quaternion.</param>
            <returns>The scaled quaternion.</returns>
        </member>
        <member name="M:RDR2.Math.Quaternion.op_Division(RDR2.Math.Quaternion,RDR2.Math.Quaternion)">
            <summary>
            Divides a Quaternion by another Quaternion.
            </summary>
            <param name="left">The source Quaternion.</param>
            <param name="right">The divisor.</param>
            <returns>The result of the division.</returns>
        </member>
        <member name="M:RDR2.Math.Quaternion.op_Equality(RDR2.Math.Quaternion,RDR2.Math.Quaternion)">
            <summary>
            Tests for equality between two objects.
            </summary>
            <param name="left">The first value to compare.</param>
            <param name="right">The second value to compare.</param>
            <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        </member>
        <member name="M:RDR2.Math.Quaternion.op_Inequality(RDR2.Math.Quaternion,RDR2.Math.Quaternion)">
            <summary>
            Tests for inequality between two objects.
            </summary>
            <param name="left">The first value to compare.</param>
            <param name="right">The second value to compare.</param>
            <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        </member>
        <member name="M:RDR2.Math.Quaternion.ToString">
            <summary>
            Converts the value of the object to its equivalent string representation.
            </summary>
            <returns>The string representation of the value of this instance.</returns>
```
