<!-- ScriptHookRDRNetAPI lines 251-500 of 30005 -->
```csharp
            <returns>The created rotation matrix.</returns>
        </member>
        <member name="M:RDR2.Math.Matrix.RotationQuaternion(RDR2.Math.Quaternion)">
            <summary>
            Creates a rotation matrix from a rotation.
            </summary>
            <param name="rotation">The quaternion to use to build the matrix.</param>
            <returns>The created rotation matrix.</returns>
        </member>
        <member name="M:RDR2.Math.Matrix.RotationYawPitchRoll(System.Single,System.Single,System.Single)">
            <summary>
            Creates a rotation matrix with a specified yaw, pitch, and roll.
            </summary>
            <param name="yaw">Yaw around the y-axis, in radians.</param>
            <param name="pitch">Pitch around the x-axis, in radians.</param>
            <param name="roll">Roll around the z-axis, in radians.</param>
            <returns>The created rotation matrix.</returns>
        </member>
        <member name="M:RDR2.Math.Matrix.Scaling(System.Single,System.Single,System.Single)">
            <summary>
            Creates a matrix that scales along the x-axis, y-axis, and y-axis.
            </summary>
            <param name="x">Scaling factor that is applied along the x-axis.</param>
            <param name="y">Scaling factor that is applied along the y-axis.</param>
            <param name="z">Scaling factor that is applied along the z-axis.</param>
            <returns>The created scaling matrix.</returns>
        </member>
        <member name="M:RDR2.Math.Matrix.Scaling(RDR2.Math.Vector3)">
            <summary>
            Creates a matrix that scales along the x-axis, y-axis, and y-axis.
            </summary>
            <param name="scale">Scaling factor for all three axes.</param>
            <returns>The created scaling matrix.</returns>
        </member>
        <member name="M:RDR2.Math.Matrix.Translation(System.Single,System.Single,System.Single)">
            <summary>
            Creates a translation matrix using the specified offsets.
            </summary>
            <param name="x">X-coordinate offset.</param>
            <param name="y">Y-coordinate offset.</param>
            <param name="z">Z-coordinate offset.</param>
            <returns>The created translation matrix.</returns>
        </member>
        <member name="M:RDR2.Math.Matrix.Translation(RDR2.Math.Vector3)">
            <summary>
            Creates a translation matrix using the specified offsets.
            </summary>
            <param name="amount">The offset for all three coordinate planes.</param>
            <returns>The created translation matrix.</returns>
        </member>
        <member name="M:RDR2.Math.Matrix.Transpose(RDR2.Math.Matrix)">
            <summary>
            Calculates the transpose of the specified matrix.
            </summary>
            <param name="matrix">The matrix whose transpose is to be calculated.</param>
            <returns>The transpose of the specified matrix.</returns>
        </member>
        <member name="M:RDR2.Math.Matrix.op_UnaryNegation(RDR2.Math.Matrix)">
            <summary>
            Negates a matrix.
            </summary>
            <param name="matrix">The matrix to negate.</param>
            <returns>The negated matrix.</returns>
        </member>
        <member name="M:RDR2.Math.Matrix.op_Addition(RDR2.Math.Matrix,RDR2.Math.Matrix)">
            <summary>
            Adds two matrices.
            </summary>
            <param name="left">The first matrix to add.</param>
            <param name="right">The second matrix to add.</param>
            <returns>The sum of the two matrices.</returns>
        </member>
        <member name="M:RDR2.Math.Matrix.op_Subtraction(RDR2.Math.Matrix,RDR2.Math.Matrix)">
            <summary>
            Subtracts two matrices.
            </summary>
            <param name="left">The first matrix to subtract.</param>
            <param name="right">The second matrix to subtract.</param>
            <returns>The difference between the two matrices.</returns>
        </member>
        <member name="M:RDR2.Math.Matrix.op_Division(RDR2.Math.Matrix,RDR2.Math.Matrix)">
            <summary>
            Divides two matrices.
            </summary>
            <param name="left">The first matrix to divide.</param>
            <param name="right">The second matrix to divide.</param>
            <returns>The quotient of the two matrices.</returns>
        </member>
        <member name="M:RDR2.Math.Matrix.op_Division(RDR2.Math.Matrix,System.Single)">
            <summary>
            Scales a matrix by a given value.
            </summary>
            <param name="left">The matrix to scale.</param>
            <param name="right">The amount by which to scale.</param>
            <returns>The scaled matrix.</returns>
        </member>
        <member name="M:RDR2.Math.Matrix.op_Multiply(RDR2.Math.Matrix,RDR2.Math.Matrix)">
            <summary>
            Multiplies two matrices.
            </summary>
            <param name="left">The first matrix to multiply.</param>
            <param name="right">The second matrix to multiply.</param>
            <returns>The product of the two matrices.</returns>
        </member>
        <member name="M:RDR2.Math.Matrix.op_Multiply(RDR2.Math.Matrix,System.Single)">
            <summary>
            Scales a matrix by a given value.
            </summary>
            <param name="left">The matrix to scale.</param>
            <param name="right">The amount by which to scale.</param>
            <returns>The scaled matrix.</returns>
        </member>
        <member name="M:RDR2.Math.Matrix.op_Multiply(System.Single,RDR2.Math.Matrix)">
            <summary>
            Scales a matrix by a given value.
            </summary>
            <param name="right">The matrix to scale.</param>
            <param name="left">The amount by which to scale.</param>
            <returns>The scaled matrix.</returns>
        </member>
        <member name="M:RDR2.Math.Matrix.op_Equality(RDR2.Math.Matrix,RDR2.Math.Matrix)">
            <summary>
            Tests for equality between two objects.
            </summary>
            <param name="left">The first value to compare.</param>
            <param name="right">The second value to compare.</param>
            <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        </member>
        <member name="M:RDR2.Math.Matrix.op_Inequality(RDR2.Math.Matrix,RDR2.Math.Matrix)">
            <summary>
            Tests for inequality between two objects.
            </summary>
            <param name="left">The first value to compare.</param>
            <param name="right">The second value to compare.</param>
            <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        </member>
        <member name="M:RDR2.Math.Matrix.ToArray">
            <summary>
            Converts the matrix to an array of floats.
            </summary>
        </member>
        <member name="M:RDR2.Math.Matrix.ToString">
            <summary>
            Converts the value of the object to its equivalent string representation.
            </summary>
            <returns>The string representation of the value of this instance.</returns>
        </member>
        <member name="M:RDR2.Math.Matrix.GetHashCode">
            <summary>
            Returns the hash code for this instance.
            </summary>
            <returns>A 32-bit signed integer hash code.</returns>
        </member>
        <member name="M:RDR2.Math.Matrix.Equals(System.Object)">
            <summary>
            Returns a value that indicates whether the current instance is equal to a specified object.
            </summary>
            <param name="obj">Object to make the comparison with.</param>
            <returns><c>true</c> if the current instance is equal to the specified object; <c>false</c> otherwise.</returns>
        </member>
        <member name="M:RDR2.Math.Matrix.Equals(RDR2.Math.Matrix)">
            <summary>
            Returns a value that indicates whether the current instance is equal to the specified object.
            </summary>
            <param name="other">Object to make the comparison with.</param>
            <returns><c>true</c> if the current instance is equal to the specified object; <c>false</c> otherwise.</returns>
        </member>
        <member name="M:RDR2.Math.Matrix.Equals(RDR2.Math.Matrix@,RDR2.Math.Matrix@)">
            <summary>
            Determines whether the specified object instances are considered equal.
            </summary>
            <param name="value1"></param>
            <param name="value2"></param>
            <returns><c>true</c> if <paramref name="value1"/> is the same instance as <paramref name="value2"/> or
            if both are <c>null</c> references or if <c>value1.Equals(value2)</c> returns <c>true</c>; otherwise, <c>false</c>.</returns>
        </member>
        <member name="F:RDR2.Math.Quaternion.X">
            <summary>
            Gets or sets the X component of the quaternion.
            </summary>
            <value>The X component of the quaternion.</value>
        </member>
        <member name="F:RDR2.Math.Quaternion.Y">
            <summary>
            Gets or sets the Y component of the quaternion.
            </summary>
            <value>The Y component of the quaternion.</value>
        </member>
        <member name="F:RDR2.Math.Quaternion.Z">
            <summary>
            Gets or sets the Z component of the quaternion.
            </summary>
            <value>The Z component of the quaternion.</value>
        </member>
        <member name="F:RDR2.Math.Quaternion.W">
            <summary>
            Gets or sets the W component of the quaternion.
            </summary>
            <value>The W component of the quaternion.</value>
        </member>
        <member name="M:RDR2.Math.Quaternion.#ctor(System.Single,System.Single,System.Single,System.Single)">
            <summary>
            Initializes a new instance of the <see cref="T:RDR2.Math.Quaternion"/> structure.
            </summary>
            <param name="x">The X component of the quaternion.</param>
            <param name="y">The Y component of the quaternion.</param>
            <param name="z">The Z component of the quaternion.</param>
            <param name="w">The W component of the quaternion.</param>
        </member>
        <member name="M:RDR2.Math.Quaternion.#ctor(RDR2.Math.Vector3,System.Single)">
            <summary>
            Initializes a new instance of the <see cref="T:RDR2.Math.Quaternion"/> structure.
            </summary>
            <param name="axis">The axis of rotation.</param>
            <param name="angle">The angle of rotation in radians.</param>
        </member>
        <member name="P:RDR2.Math.Quaternion.Identity">
            <summary>
            The identity <see cref="T:RDR2.Math.Quaternion"/> (0, 0, 0, 1).
            </summary>
        </member>
        <member name="P:RDR2.Math.Quaternion.Axis">
            <summary>
            Gets the axis components of the quaternion.
            </summary>
        </member>
        <member name="P:RDR2.Math.Quaternion.Angle">
            <summary>
            Gets the angle of the quaternion.
            </summary>
        </member>
        <member name="M:RDR2.Math.Quaternion.Length">
            <summary>
            Calculates the length of the quaternion.
            </summary>
            <returns>The length of the quaternion.</returns>
        </member>
        <member name="M:RDR2.Math.Quaternion.LengthSquared">
            <summary>
            Calculates the squared length of the quaternion.
            </summary>
            <returns>The squared length of the quaternion.</returns>
        </member>
        <member name="M:RDR2.Math.Quaternion.Normalize">
            <summary>
            Converts the quaternion into a unit quaternion.
            </summary>
        </member>
        <member name="M:RDR2.Math.Quaternion.Conjugate">
            <summary>
```
