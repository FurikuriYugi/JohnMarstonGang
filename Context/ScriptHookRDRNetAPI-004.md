<!-- ScriptHookRDRNetAPI lines 751-1000 of 30005 -->
```csharp
        </member>
        <member name="M:RDR2.Math.Quaternion.ToString(System.String)">
            <summary>
            Converts the value of the object to its equivalent string representation.
            </summary>
            <param name="format">The format.</param>
            <returns>The string representation of the value of this instance.</returns>
        </member>
        <member name="M:RDR2.Math.Quaternion.GetHashCode">
            <summary>
            Returns the hash code for this instance.
            </summary>
            <returns>A 32-bit signed integer hash code.</returns>
        </member>
        <member name="M:RDR2.Math.Quaternion.Equals(System.Object)">
            <summary>
            Returns a value that indicates whether the current instance is equal to a specified object.
            </summary>
            <param name="obj">Object to make the comparison with.</param>
            <returns><c>true</c> if the current instance is equal to the specified object; <c>false</c> otherwise.</returns>
        </member>
        <member name="M:RDR2.Math.Quaternion.Equals(RDR2.Math.Quaternion)">
            <summary>
            Returns a value that indicates whether the current instance is equal to the specified object.
            </summary>
            <param name="other">Object to make the comparison with.</param>
            <returns><c>true</c> if the current instance is equal to the specified object; <c>false</c> otherwise.</returns>
        </member>
        <member name="M:RDR2.Math.Quaternion.Equals(RDR2.Math.Quaternion@,RDR2.Math.Quaternion@)">
            <summary>
            Determines whether the specified object instances are considered equal.
            </summary>
            <param name="value1"></param>
            <param name="value2"></param>
            <returns><c>true</c> if <paramref name="value1"/> is the same instance as <paramref name="value2"/> or
            if both are <c>null</c> references or if <c>value1.Equals(value2)</c> returns <c>true</c>; otherwise, <c>false</c>.</returns>
        </member>
        <member name="F:RDR2.Math.Vector2.X">
            <summary>
            Gets or sets the X component of the vector.
            </summary>
            <value>The X component of the vector.</value>
        </member>
        <member name="F:RDR2.Math.Vector2.Y">
            <summary>
            Gets or sets the Y component of the vector.
            </summary>
            <value>The Y component of the vector.</value>
        </member>
        <member name="M:RDR2.Math.Vector2.#ctor(System.Single,System.Single)">
            <summary>
            Initializes a new instance of the <see cref="T:RDR2.Math.Vector2"/> class.
            </summary>
            <param name="x">Initial value for the X component of the vector.</param>
            <param name="y">Initial value for the Y component of the vector.</param>
        </member>
        <member name="P:RDR2.Math.Vector2.Normalized">
            <summary>
            Returns this vector with a magnitude of 1.
            </summary>
        </member>
        <member name="P:RDR2.Math.Vector2.Zero">
            <summary>
            Returns a null vector. (0,0)
            </summary>
        </member>
        <member name="P:RDR2.Math.Vector2.Up">
            <summary>
            Returns the up vector. (0,1)
            </summary>
        </member>
        <member name="P:RDR2.Math.Vector2.Down">
            <summary>
            Returns the down vector. (0,-1)
            </summary>
        </member>
        <member name="P:RDR2.Math.Vector2.Right">
            <summary>
            Returns the right vector. (1,0)
            </summary>
        </member>
        <member name="P:RDR2.Math.Vector2.Left">
            <summary>
            Returns the left vector. (-1,0)
            </summary>
        </member>
        <member name="P:RDR2.Math.Vector2.Item(System.Int32)">
            <summary>
            Gets or sets the component at the specified index.
            </summary>
            <value>The value of the X or Y component, depending on the index.</value>
            <param name="index">The index of the component to access. Use 0 for the X component and 1 for the Y component.</param>
            <returns>The value of the component at the specified index.</returns>
            <exception cref="T:System.ArgumentOutOfRangeException">Thrown when the <paramref name="index"/> is out of the range [0, 1].</exception>
        </member>
        <member name="M:RDR2.Math.Vector2.Length">
            <summary>
            Calculates the length of the vector.
            </summary>
            <returns>The length of the vector.</returns>
        </member>
        <member name="M:RDR2.Math.Vector2.LengthSquared">
            <summary>
            Calculates the squared length of the vector.
            </summary>
            <returns>The squared length of the vector.</returns>
        </member>
        <member name="M:RDR2.Math.Vector2.Normalize">
            <summary>
            Converts the vector into a unit vector.
            </summary>
        </member>
        <member name="M:RDR2.Math.Vector2.DistanceTo(RDR2.Math.Vector2)">
            <summary>
            Calculates the distance between two vectors.
            </summary>
            <param name="position">The second vector to calculate the distance to.</param>
            <returns>The distance to the other vector.</returns>
        </member>
        <member name="M:RDR2.Math.Vector2.DistanceToSquared(RDR2.Math.Vector2)">
            <summary>
            Calculates the squared distance between two vectors.
            </summary>
            <param name="position">The second vector to calculate the squared distance to.</param>
            <returns>The squared distance to the other vector.</returns>
        </member>
        <member name="M:RDR2.Math.Vector2.Distance(RDR2.Math.Vector2,RDR2.Math.Vector2)">
            <summary>
            Calculates the distance between two vectors.
            </summary>
            <param name="position1">The first vector to calculate the distance to the second vector.</param>
            <param name="position2">The second vector to calculate the distance to the first vector.</param>
            <returns>The distance between the two vectors.</returns>
        </member>
        <member name="M:RDR2.Math.Vector2.DistanceSquared(RDR2.Math.Vector2,RDR2.Math.Vector2)">
            <summary>
            Calculates the squared distance between two vectors.
            </summary>
            <param name="position1">The first vector to calculate the squared distance to the second vector.</param>
            <param name="position2">The second vector to calculate the squared distance to the first vector.</param>
            <returns>The squared distance between the two vectors.</returns>
        </member>
        <member name="M:RDR2.Math.Vector2.Angle(RDR2.Math.Vector2,RDR2.Math.Vector2)">
            <summary>
            Returns the angle in degrees between from and to.
            The angle returned is always the acute angle between the two vectors.
            </summary>
        </member>
        <member name="M:RDR2.Math.Vector2.SignedAngle(RDR2.Math.Vector2,RDR2.Math.Vector2)">
            <summary>
            Returns the signed angle in degrees between from and to.
            </summary>
        </member>
        <member name="M:RDR2.Math.Vector2.ToHeading">
            <summary>
            Converts a vector to a heading.
            </summary>
        </member>
        <member name="M:RDR2.Math.Vector2.RandomXY">
            <summary>
            Returns a new normalized vector with random X and Y components.
            </summary>
        </member>
        <member name="M:RDR2.Math.Vector2.Add(RDR2.Math.Vector2,RDR2.Math.Vector2)">
            <summary>
            Adds two vectors.
            </summary>
            <param name="left">The first vector to add.</param>
            <param name="right">The second vector to add.</param>
            <returns>The sum of the two vectors.</returns>
        </member>
        <member name="M:RDR2.Math.Vector2.Subtract(RDR2.Math.Vector2,RDR2.Math.Vector2)">
            <summary>
            Subtracts two vectors.
            </summary>
            <param name="left">The first vector to subtract.</param>
            <param name="right">The second vector to subtract.</param>
            <returns>The difference of the two vectors.</returns>
        </member>
        <member name="M:RDR2.Math.Vector2.Multiply(RDR2.Math.Vector2,System.Single)">
            <summary>
            Scales a vector by the given value.
            </summary>
            <param name="value">The vector to scale.</param>
            <param name="scale">The amount by which to scale the vector.</param>
            <returns>The scaled vector.</returns>
        </member>
        <member name="M:RDR2.Math.Vector2.Modulate(RDR2.Math.Vector2,RDR2.Math.Vector2)">
            <summary>
            Modulates a vector by another.
            </summary>
            <param name="left">The first vector to modulate.</param>
            <param name="right">The second vector to modulate.</param>
            <returns>The modulated vector.</returns>
        </member>
        <member name="M:RDR2.Math.Vector2.Divide(RDR2.Math.Vector2,System.Single)">
            <summary>
            Scales a vector by the given value.
            </summary>
            <param name="value">The vector to scale.</param>
            <param name="scale">The amount by which to scale the vector.</param>
            <returns>The scaled vector.</returns>
        </member>
        <member name="M:RDR2.Math.Vector2.Negate(RDR2.Math.Vector2)">
            <summary>
            Reverses the direction of a given vector.
            </summary>
            <param name="value">The vector to negate.</param>
            <returns>A vector facing in the opposite direction.</returns>
        </member>
        <member name="M:RDR2.Math.Vector2.Clamp(RDR2.Math.Vector2,RDR2.Math.Vector2,RDR2.Math.Vector2)">
            <summary>
            Restricts a value to be within a specified range.
            </summary>
            <param name="value">The value to clamp.</param>
            <param name="min">The minimum value.</param>
            <param name="max">The maximum value.</param>
            <returns>The clamped value.</returns>
        </member>
        <member name="M:RDR2.Math.Vector2.Lerp(RDR2.Math.Vector2,RDR2.Math.Vector2,System.Single)">
            <summary>
            Performs a linear interpolation between two vectors.
            </summary>
            <param name="start">Start vector.</param>
            <param name="end">End vector.</param>
            <param name="amount">Value between 0 and 1 indicating the weight of <paramref name="end"/>.</param>
            <returns>The linear interpolation of the two vectors.</returns>
            <remarks>
            This method performs the linear interpolation based on the following formula.
            <code>start + (end - start) * amount</code>
            Passing <paramref name="amount"/> a value of 0 will cause <paramref name="start"/> to be returned; a value of 1 will cause <paramref name="end"/> to be returned.
            </remarks>
        </member>
        <member name="M:RDR2.Math.Vector2.Normalize(RDR2.Math.Vector2)">
            <summary>
            Converts the vector into a unit vector.
            </summary>
            <param name="vector">The vector to normalize.</param>
            <returns>The normalized vector.</returns>
        </member>
        <member name="M:RDR2.Math.Vector2.Dot(RDR2.Math.Vector2,RDR2.Math.Vector2)">
            <summary>
            Calculates the dot product of two vectors.
            </summary>
            <param name="left">First source vector.</param>
            <param name="right">Second source vector.</param>
            <returns>The dot product of the two vectors.</returns>
        </member>
        <member name="M:RDR2.Math.Vector2.Reflect(RDR2.Math.Vector2,RDR2.Math.Vector2)">
            <summary>
```
