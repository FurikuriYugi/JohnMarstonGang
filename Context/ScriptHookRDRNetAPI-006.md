<!-- ScriptHookRDRNetAPI lines 1251-1500 of 30005 -->
```csharp
        </member>
        <member name="M:RDR2.Math.Vector3.Normalize">
            <summary>
            Converts the vector into a unit vector.
            </summary>
        </member>
        <member name="M:RDR2.Math.Vector3.DistanceTo(RDR2.Math.Vector3)">
            <summary>
            Calculates the distance between two vectors.
            </summary>
            <param name="position">The second vector to calculate the distance to.</param>
            <returns>The distance to the other vector.</returns>
        </member>
        <member name="M:RDR2.Math.Vector3.DistanceToSquared(RDR2.Math.Vector3)">
            <summary>
            Calculates the squared distance between two vectors.
            </summary>
            <param name="position">The second vector to calculate the distance to.</param>
            <returns>The distance to the other vector.</returns>
        </member>
        <member name="M:RDR2.Math.Vector3.DistanceTo2D(RDR2.Math.Vector3)">
            <summary>
            Calculates the distance between two vectors, ignoring the Z-component.
            </summary>
            <param name="position">The second vector to calculate the distance to.</param>
            <returns>The distance to the other vector.</returns>
        </member>
        <member name="M:RDR2.Math.Vector3.DistanceToSquared2D(RDR2.Math.Vector3)">
            <summary>
            Calculates the squared distance between two vectors, ignoring the Z-component.
            </summary>
            <param name="position">The second vector to calculate the squared distance to.</param>
            <returns>The distance to the other vector.</returns>
        </member>
        <member name="M:RDR2.Math.Vector3.Distance(RDR2.Math.Vector3,RDR2.Math.Vector3)">
            <summary>
            Calculates the distance between two vectors.
            </summary>
            <param name="position1">The first vector to calculate the distance to the second vector.</param>
            <param name="position2">The second vector to calculate the distance to the first vector.</param>
            <returns>The distance between the two vectors.</returns>
        </member>
        <member name="M:RDR2.Math.Vector3.DistanceSquared(RDR2.Math.Vector3,RDR2.Math.Vector3)">
            <summary>
            Calculates the squared distance between two vectors.
            </summary>
            <param name="position1">The first vector to calculate the squared distance to the second vector.</param>
            <param name="position2">The second vector to calculate the squared distance to the first vector.</param>
            <returns>The squared distance between the two vectors.</returns>
        </member>
        <member name="M:RDR2.Math.Vector3.Distance2D(RDR2.Math.Vector3,RDR2.Math.Vector3)">
            <summary>
            Calculates the distance between two vectors, ignoring the Z-component.
            </summary>
            <param name="position1">The first vector to calculate the distance to the second vector.</param>
            <param name="position2">The second vector to calculate the distance to the first vector.</param>
            <returns>The distance between the two vectors.</returns>
        </member>
        <member name="M:RDR2.Math.Vector3.DistanceSquared2D(RDR2.Math.Vector3,RDR2.Math.Vector3)">
            <summary>
            Calculates the squared distance between two vectors, ignoring the Z-component.
            </summary>
            <param name="position1">The first vector to calculate the squared distance to the second vector.</param>
            <param name="position2">The second vector to calculate the squared distance to the first vector.</param>
            <returns>The squared distance between the two vectors.</returns>
        </member>
        <member name="M:RDR2.Math.Vector3.Angle(RDR2.Math.Vector3,RDR2.Math.Vector3)">
            <summary>
            Returns the angle in degrees between from and to.
            The angle returned is always the acute angle between the two vectors.
            </summary>
        </member>
        <member name="M:RDR2.Math.Vector3.SignedAngle(RDR2.Math.Vector3,RDR2.Math.Vector3,RDR2.Math.Vector3)">
            <summary>
            Returns the signed angle in degrees between from and to.
            </summary>
        </member>
        <member name="M:RDR2.Math.Vector3.ToHeading">
            <summary>
            Converts a vector to a heading.
            </summary>
        </member>
        <member name="M:RDR2.Math.Vector3.Around(System.Single)">
            <summary>
            Creates a random vector inside the circle around this position.
            </summary>
        </member>
        <member name="M:RDR2.Math.Vector3.RandomXY">
            <summary>
            Returns a new normalized vector with random X and Y components.
            </summary>
        </member>
        <member name="M:RDR2.Math.Vector3.RandomXYZ">
            <summary>
            Returns a new normalized vector with random X, Y and Z components.
            </summary>
        </member>
        <member name="M:RDR2.Math.Vector3.Add(RDR2.Math.Vector3,RDR2.Math.Vector3)">
            <summary>
            Adds two vectors.
            </summary>
            <param name="left">The first vector to add.</param>
            <param name="right">The second vector to add.</param>
            <returns>The sum of the two vectors.</returns>
        </member>
        <member name="M:RDR2.Math.Vector3.Subtract(RDR2.Math.Vector3,RDR2.Math.Vector3)">
            <summary>
            Subtracts two vectors.
            </summary>
            <param name="left">The first vector to subtract.</param>
            <param name="right">The second vector to subtract.</param>
            <returns>The difference of the two vectors.</returns>
        </member>
        <member name="M:RDR2.Math.Vector3.Multiply(RDR2.Math.Vector3,System.Single)">
            <summary>
            Scales a vector by the given value.
            </summary>
            <param name="value">The vector to scale.</param>
            <param name="scale">The amount by which to scale the vector.</param>
            <returns>The scaled vector.</returns>
        </member>
        <member name="M:RDR2.Math.Vector3.Modulate(RDR2.Math.Vector3,RDR2.Math.Vector3)">
            <summary>
            Modulates a vector by another.
            </summary>
            <param name="left">The first vector to modulate.</param>
            <param name="right">The second vector to modulate.</param>
            <returns>The multiplied vector.</returns>
        </member>
        <member name="M:RDR2.Math.Vector3.Divide(RDR2.Math.Vector3,System.Single)">
            <summary>
            Scales a vector by the given value.
            </summary>
            <param name="value">The vector to scale.</param>
            <param name="scale">The amount by which to scale the vector.</param>
            <returns>The scaled vector.</returns>
        </member>
        <member name="M:RDR2.Math.Vector3.Negate(RDR2.Math.Vector3)">
            <summary>
            Reverses the direction of a given vector.
            </summary>
            <param name="value">The vector to negate.</param>
            <returns>A vector facing in the opposite direction.</returns>
        </member>
        <member name="M:RDR2.Math.Vector3.Clamp(RDR2.Math.Vector3,RDR2.Math.Vector3,RDR2.Math.Vector3)">
            <summary>
            Restricts a value to be within a specified range.
            </summary>
            <param name="value">The value to clamp.</param>
            <param name="min">The minimum value.</param>
            <param name="max">The maximum value.</param>
            <returns>The clamped value.</returns>
        </member>
        <member name="M:RDR2.Math.Vector3.Lerp(RDR2.Math.Vector3,RDR2.Math.Vector3,System.Single)">
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
        <member name="M:RDR2.Math.Vector3.Normalize(RDR2.Math.Vector3)">
            <summary>
            Converts the vector into a unit vector.
            </summary>
            <param name="vector">The vector to normalize.</param>
            <returns>The normalized vector.</returns>
        </member>
        <member name="M:RDR2.Math.Vector3.Dot(RDR2.Math.Vector3,RDR2.Math.Vector3)">
            <summary>
            Calculates the dot product of two vectors.
            </summary>
            <param name="left">First source vector.</param>
            <param name="right">Second source vector.</param>
            <returns>The dot product of the two vectors.</returns>
        </member>
        <member name="M:RDR2.Math.Vector3.Cross(RDR2.Math.Vector3,RDR2.Math.Vector3)">
            <summary>
            Calculates the cross product of two vectors.
            </summary>
            <param name="left">First source vector.</param>
            <param name="right">Second source vector.</param>
            <returns>The cross product of the two vectors.</returns>
        </member>
        <member name="M:RDR2.Math.Vector3.Project(RDR2.Math.Vector3,RDR2.Math.Vector3)">
            <summary>
            Projects a vector onto another vector.
            </summary>
            <param name="vector">The vector to project.</param>
            <param name="onNormal">Vector to project onto, does not assume it is normalized.</param>
            <returns>The projected vector.</returns>
        </member>
        <member name="M:RDR2.Math.Vector3.ProjectOnPlane(RDR2.Math.Vector3,RDR2.Math.Vector3)">
            <summary>
            Projects a vector onto a plane defined by a normal orthogonal to the plane.
            </summary>
            <param name="vector">The vector to project.</param>
            <param name="planeNormal">Normal of the plane,  does not assume it is normalized.</param>
            <returns>The Projection of vector onto plane.</returns>
        </member>
        <member name="M:RDR2.Math.Vector3.Reflect(RDR2.Math.Vector3,RDR2.Math.Vector3)">
            <summary>
            Returns the reflection of a vector off a surface that has the specified normal.
            </summary>
            <param name="vector">The vector to project onto the plane.</param>
            <param name="normal">Normal of the surface.</param>
            <returns>The reflected vector.</returns>
            <remarks>Reflect only gives the direction of a reflection off a surface, it does not determine
            whether the original vector was close enough to the surface to hit it.</remarks>
        </member>
        <member name="M:RDR2.Math.Vector3.Minimize(RDR2.Math.Vector3,RDR2.Math.Vector3)">
            <summary>
            Returns a vector containing the smallest components of the specified vectors.
            </summary>
            <param name="value1">The first source vector.</param>
            <param name="value2">The second source vector.</param>
            <returns>A vector containing the smallest components of the source vectors.</returns>
        </member>
        <member name="M:RDR2.Math.Vector3.Maximize(RDR2.Math.Vector3,RDR2.Math.Vector3)">
            <summary>
            Returns a vector containing the largest components of the specified vectors.
            </summary>
            <param name="value1">The first source vector.</param>
            <param name="value2">The second source vector.</param>
            <returns>A vector containing the largest components of the source vectors.</returns>
        </member>
        <member name="M:RDR2.Math.Vector3.op_Addition(RDR2.Math.Vector3,RDR2.Math.Vector3)">
            <summary>
            Adds two vectors.
            </summary>
            <param name="left">The first vector to add.</param>
            <param name="right">The second vector to add.</param>
            <returns>The sum of the two vectors.</returns>
        </member>
        <member name="M:RDR2.Math.Vector3.op_Subtraction(RDR2.Math.Vector3,RDR2.Math.Vector3)">
            <summary>
            Subtracts two vectors.
            </summary>
            <param name="left">The first vector to subtract.</param>
            <param name="right">The second vector to subtract.</param>
            <returns>The difference of the two vectors.</returns>
        </member>
        <member name="M:RDR2.Math.Vector3.op_UnaryNegation(RDR2.Math.Vector3)">
            <summary>
```
