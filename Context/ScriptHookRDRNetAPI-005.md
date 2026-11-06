<!-- ScriptHookRDRNetAPI lines 1001-1250 of 30005 -->
```csharp
            Returns the reflection of a vector off a surface that has the specified normal.
            </summary>
            <param name="vector">The source vector.</param>
            <param name="normal">Normal of the surface.</param>
            <returns>The reflected vector.</returns>
            <remarks>Reflect only gives the direction of a reflection off a surface, it does not determine
            whether the original vector was close enough to the surface to hit it.</remarks>
        </member>
        <member name="M:RDR2.Math.Vector2.Minimize(RDR2.Math.Vector2,RDR2.Math.Vector2)">
            <summary>
            Returns a vector containing the smallest components of the specified vectors.
            </summary>
            <param name="value1">The first source vector.</param>
            <param name="value2">The second source vector.</param>
            <returns>A vector containing the smallest components of the source vectors.</returns>
        </member>
        <member name="M:RDR2.Math.Vector2.Maximize(RDR2.Math.Vector2,RDR2.Math.Vector2)">
            <summary>
            Returns a vector containing the largest components of the specified vectors.
            </summary>
            <param name="value1">The first source vector.</param>
            <param name="value2">The second source vector.</param>
            <returns>A vector containing the largest components of the source vectors.</returns>
        </member>
        <member name="M:RDR2.Math.Vector2.op_Addition(RDR2.Math.Vector2,RDR2.Math.Vector2)">
            <summary>
            Adds two vectors.
            </summary>
            <param name="left">The first vector to add.</param>
            <param name="right">The second vector to add.</param>
            <returns>The sum of the two vectors.</returns>
        </member>
        <member name="M:RDR2.Math.Vector2.op_Subtraction(RDR2.Math.Vector2,RDR2.Math.Vector2)">
            <summary>
            Subtracts two vectors.
            </summary>
            <param name="left">The first vector to subtract.</param>
            <param name="right">The second vector to subtract.</param>
            <returns>The difference of the two vectors.</returns>
        </member>
        <member name="M:RDR2.Math.Vector2.op_UnaryNegation(RDR2.Math.Vector2)">
            <summary>
            Reverses the direction of a given vector.
            </summary>
            <param name="value">The vector to negate.</param>
            <returns>A vector facing in the opposite direction.</returns>
        </member>
        <member name="M:RDR2.Math.Vector2.op_Multiply(RDR2.Math.Vector2,System.Single)">
            <summary>
            Scales a vector by the given value.
            </summary>
            <param name="vector">The vector to scale.</param>
            <param name="scale">The amount by which to scale the vector.</param>
            <returns>The scaled vector.</returns>
        </member>
        <member name="M:RDR2.Math.Vector2.op_Multiply(System.Single,RDR2.Math.Vector2)">
            <summary>
            Scales a vector by the given value.
            </summary>
            <param name="vector">The vector to scale.</param>
            <param name="scale">The amount by which to scale the vector.</param>
            <returns>The scaled vector.</returns>
        </member>
        <member name="M:RDR2.Math.Vector2.op_Division(RDR2.Math.Vector2,System.Single)">
            <summary>
            Scales a vector by the given value.
            </summary>
            <param name="vector">The vector to scale.</param>
            <param name="scale">The amount by which to scale the vector.</param>
            <returns>The scaled vector.</returns>
        </member>
        <member name="M:RDR2.Math.Vector2.op_Equality(RDR2.Math.Vector2,RDR2.Math.Vector2)">
            <summary>
            Tests for equality between two objects.
            </summary>
            <param name="left">The first value to compare.</param>
            <param name="right">The second value to compare.</param>
            <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        </member>
        <member name="M:RDR2.Math.Vector2.op_Inequality(RDR2.Math.Vector2,RDR2.Math.Vector2)">
            <summary>
            Tests for inequality between two objects.
            </summary>
            <param name="left">The first value to compare.</param>
            <param name="right">The second value to compare.</param>
            <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        </member>
        <member name="M:RDR2.Math.Vector2.op_Implicit(RDR2.Math.Vector2)~RDR2.Math.Vector3">
            <summary>
            Converts a Vector2 to a Vector3 implicitly.
            </summary>
        </member>
        <member name="M:RDR2.Math.Vector2.ToString">
            <summary>
            Converts the value of the object to its equivalent string representation.
            </summary>
            <returns>The string representation of the value of this instance.</returns>
        </member>
        <member name="M:RDR2.Math.Vector2.ToString(System.String)">
            <summary>
            Converts the value of the object to its equivalent string representation.
            </summary>
            <param name="format">The format.</param>
            <returns>The string representation of the value of this instance.</returns>
        </member>
        <member name="M:RDR2.Math.Vector2.GetHashCode">
            <summary>
            Returns the hash code for this instance.
            </summary>
            <returns>A 32-bit signed integer hash code.</returns>
        </member>
        <member name="M:RDR2.Math.Vector2.Equals(System.Object)">
            <summary>
            Returns a value that indicates whether the current instance is equal to a specified object.
            </summary>
            <param name="obj">Object to make the comparison with.</param>
            <returns><c>true</c> if the current instance is equal to the specified object; otherwise, <c>false</c>.</returns>
        </member>
        <member name="M:RDR2.Math.Vector2.Equals(RDR2.Math.Vector2)">
            <summary>
            Returns a value that indicates whether the current instance is equal to the specified object.
            </summary>
            <param name="other">Object to make the comparison with.</param>
            <returns><c>true</c> if the current instance is equal to the specified object; <c>false</c> otherwise.</returns>
        </member>
        <member name="M:RDR2.Math.Vector2.Equals(RDR2.Math.Vector2@,RDR2.Math.Vector2@)">
            <summary>
            Determines whether the specified object instances are considered equal.
            </summary>
            <param name="value1">The first value to compare.</param>
            <param name="value2">The second value to compare.</param>
            <returns><c>true</c> if <paramref name="value1"/> is the same instance as <paramref name="value2"/> or
            if both are <c>null</c> references or if <c>value1.Equals(value2)</c> returns <c>true</c>; otherwise, <c>false</c>.</returns>
        </member>
        <member name="F:RDR2.Math.Vector3.X">
            <summary>
            Gets or sets the X component of the vector.
            </summary>
            <value>The X component of the vector.</value>
        </member>
        <member name="F:RDR2.Math.Vector3.Y">
            <summary>
            Gets or sets the Y component of the vector.
            </summary>
            <value>The Y component of the vector.</value>
        </member>
        <member name="F:RDR2.Math.Vector3.Z">
            <summary>
            Gets or sets the Z component of the vector.
            </summary>
            <value>The Z component of the vector.</value>
        </member>
        <member name="M:RDR2.Math.Vector3.#ctor(System.Single,System.Single,System.Single)">
            <summary>
            Initializes a new instance of the <see cref="T:RDR2.Math.Vector3"/> class.
            </summary>
            <param name="x">Initial value for the X component of the vector.</param>
            <param name="y">Initial value for the Y component of the vector.</param>
            <param name="z">Initial value for the Z component of the vector.</param>
        </member>
        <member name="P:RDR2.Math.Vector3.Normalized">
            <summary>
            Returns this vector with a magnitude of 1.
            </summary>
        </member>
        <member name="P:RDR2.Math.Vector3.Zero">
            <summary>
            Returns a null vector. (0,0,0)
            </summary>
        </member>
        <member name="P:RDR2.Math.Vector3.WorldUp">
            <summary>
            Returns the world Up vector. (0,0,1)
            </summary>
        </member>
        <member name="P:RDR2.Math.Vector3.WorldDown">
            <summary>
            Returns the world Down vector. (0,0,-1)
            </summary>
        </member>
        <member name="P:RDR2.Math.Vector3.WorldNorth">
            <summary>
            Returns the world North vector. (0,1,0)
            </summary>
        </member>
        <member name="P:RDR2.Math.Vector3.WorldSouth">
            <summary>
            Returns the world South vector. (0,-1,0)
            </summary>
        </member>
        <member name="P:RDR2.Math.Vector3.WorldEast">
            <summary>
            Returns the world East vector. (1,0,0)
            </summary>
        </member>
        <member name="P:RDR2.Math.Vector3.WorldWest">
            <summary>
            Returns the world West vector. (-1,0,0)
            </summary>
        </member>
        <member name="P:RDR2.Math.Vector3.RelativeRight">
            <summary>
            Returns the relative Right vector. (1,0,0)
            </summary>
        </member>
        <member name="P:RDR2.Math.Vector3.RelativeLeft">
            <summary>
            Returns the relative Left vector. (-1,0,0)
            </summary>
        </member>
        <member name="P:RDR2.Math.Vector3.RelativeFront">
            <summary>
            Returns the relative Front vector. (0,1,0)
            </summary>
        </member>
        <member name="P:RDR2.Math.Vector3.RelativeBack">
            <summary>
            Returns the relative Back vector. (0,-1,0)
            </summary>
        </member>
        <member name="P:RDR2.Math.Vector3.RelativeTop">
            <summary>
            Returns the relative Top vector. (0,0,1)
            </summary>
        </member>
        <member name="P:RDR2.Math.Vector3.RelativeBottom">
            <summary>
            Returns the relative Bottom vector as used. (0,0,-1)
            </summary>
        </member>
        <member name="P:RDR2.Math.Vector3.Item(System.Int32)">
            <summary>
            Gets or sets the component at the specified index.
            </summary>
            <value>The value of the X, Y or Z component, depending on the index.</value>
            <param name="index">The index of the component to access. Use 0 for the X component, 1 for the Y component and 2 for the Z component.</param>
            <returns>The value of the component at the specified index.</returns>
            <exception cref="T:System.ArgumentOutOfRangeException">Thrown when the <paramref name="index"/> is out of the range [0, 2].</exception>
        </member>
        <member name="M:RDR2.Math.Vector3.Length">
            <summary>
            Calculates the length of the vector.
            </summary>
            <returns>The length of the vector.</returns>
        </member>
        <member name="M:RDR2.Math.Vector3.LengthSquared">
            <summary>
            Calculates the squared length of the vector.
            </summary>
            <returns>The squared length of the vector.</returns>
```
