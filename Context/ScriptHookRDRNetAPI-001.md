<!-- ScriptHookRDRNetAPI lines 1-250 of 30005 -->
```csharp
<?xml version="1.0"?>
<doc>
    <assembly>
        <name>ScriptHookRDRNetAPI</name>
    </assembly>
    <members>
        <member name="T:RDR2.eEulerRotationOrder">
            <summary>
            Enums for the order in which to apply rotations in local space, just like how Rockstar Games define <c>EULER_ROT_ORDER</c>.
            </summary>
        </member>
        <member name="T:RDR2.Math.Matrix">
            <summary>
            Defines a 4x4 matrix.
            </summary>
        </member>
        <member name="F:RDR2.Math.Matrix.M11">
            <summary>
            Gets or sets the element of the matrix that exists in the first row and first column.
            </summary>
        </member>
        <member name="F:RDR2.Math.Matrix.M12">
            <summary>
            Gets or sets the element of the matrix that exists in the first row and second column.
            </summary>
        </member>
        <member name="F:RDR2.Math.Matrix.M13">
            <summary>
            Gets or sets the element of the matrix that exists in the first row and third column.
            </summary>
        </member>
        <member name="F:RDR2.Math.Matrix.M14">
            <summary>
            Gets or sets the element of the matrix that exists in the first row and fourth column.
            </summary>
        </member>
        <member name="F:RDR2.Math.Matrix.M21">
            <summary>
            Gets or sets the element of the matrix that exists in the second row and first column.
            </summary>
        </member>
        <member name="F:RDR2.Math.Matrix.M22">
            <summary>
            Gets or sets the element of the matrix that exists in the second row and second column.
            </summary>
        </member>
        <member name="F:RDR2.Math.Matrix.M23">
            <summary>
            Gets or sets the element of the matrix that exists in the second row and third column.
            </summary>
        </member>
        <member name="F:RDR2.Math.Matrix.M24">
            <summary>
            Gets or sets the element of the matrix that exists in the second row and fourth column.
            </summary>
        </member>
        <member name="F:RDR2.Math.Matrix.M31">
            <summary>
            Gets or sets the element of the matrix that exists in the third row and first column.
            </summary>
        </member>
        <member name="F:RDR2.Math.Matrix.M32">
            <summary>
            Gets or sets the element of the matrix that exists in the third row and second column.
            </summary>
        </member>
        <member name="F:RDR2.Math.Matrix.M33">
            <summary>
            Gets or sets the element of the matrix that exists in the third row and third column.
            </summary>
        </member>
        <member name="F:RDR2.Math.Matrix.M34">
            <summary>
            Gets or sets the element of the matrix that exists in the third row and fourth column.
            </summary>
        </member>
        <member name="F:RDR2.Math.Matrix.M41">
            <summary>
            Gets or sets the element of the matrix that exists in the fourth row and first column.
            </summary>
        </member>
        <member name="F:RDR2.Math.Matrix.M42">
            <summary>
            Gets or sets the element of the matrix that exists in the fourth row and second column.
            </summary>
        </member>
        <member name="F:RDR2.Math.Matrix.M43">
            <summary>
            Gets or sets the element of the matrix that exists in the fourth row and third column.
            </summary>
        </member>
        <member name="F:RDR2.Math.Matrix.M44">
            <summary>
            Gets or sets the element of the matrix that exists in the fourth row and fourth column.
            </summary>
        </member>
        <member name="M:RDR2.Math.Matrix.#ctor(System.Single[])">
            <summary>
            Initializes a new instance of the <see cref="T:RDR2.Math.Matrix"/> structure.
            </summary>
            <param name="values">The values to assign to the components of the matrix. This must be an array with sixteen elements.</param>
            <exception cref="T:System.ArgumentNullException">Thrown when <paramref name="values"/> is <c>null</c>.</exception>
            <exception cref="T:System.ArgumentOutOfRangeException">Thrown when <paramref name="values"/> contains more or less than sixteen elements.</exception>
        </member>
        <member name="P:RDR2.Math.Matrix.Identity">
            <summary>
            The identity <see cref="T:RDR2.Math.Matrix"/>.
            </summary>
        </member>
        <member name="P:RDR2.Math.Matrix.Item(System.Int32,System.Int32)">
            <summary>
            Gets or sets the component at the specified index.
            </summary>
            <value>The value of the matrix component, depending on the index.</value>
            <param name="row">The row of the matrix to access.</param>
            <param name="column">The column of the matrix to access.</param>
            <returns>The value of the component at the specified index.</returns>
            <exception cref="T:System.ArgumentOutOfRangeException">Thrown when the <paramref name="row"/> or <paramref name="column"/>is out of the range [0, 3].</exception>
        </member>
        <member name="M:RDR2.Math.Matrix.FromArray(System.Single[])">
            <summary>
            Converts the matrix to an array of floats.
            </summary>
        </member>
        <member name="P:RDR2.Math.Matrix.IsIdentity">
            <summary>
            Gets a value indicating whether this instance is an identity matrix.
            </summary>
            <value>
            <c>true</c> if this instance is an identity matrix; otherwise, <c>false</c>.
            </value>
        </member>
        <member name="P:RDR2.Math.Matrix.HasInverse">
            <summary>
            Gets a value indicating whether this instance has an inverse matrix.
            </summary>
        </member>
        <member name="M:RDR2.Math.Matrix.Determinant">
            <summary>
            Calculates the determinant of the matrix.
            </summary>
            <returns>The determinant of the matrix.</returns>
        </member>
        <member name="M:RDR2.Math.Matrix.Inverse">
            <summary>
            Inverts the matrix.
            </summary>
        </member>
        <member name="M:RDR2.Math.Matrix.Add(RDR2.Math.Matrix,RDR2.Math.Matrix)">
            <summary>
            Determines the sum of two matrices.
            </summary>
            <param name="left">The first matrix to add.</param>
            <param name="right">The second matrix to add.</param>
            <returns>The sum of the two matrices.</returns>
        </member>
        <member name="M:RDR2.Math.Matrix.Subtract(RDR2.Math.Matrix,RDR2.Math.Matrix)">
            <summary>
            Determines the difference between two matrices.
            </summary>
            <param name="left">The first matrix to subtract.</param>
            <param name="right">The second matrix to subtract.</param>
            <returns>The difference between the two matrices.</returns>
        </member>
        <member name="M:RDR2.Math.Matrix.Multiply(RDR2.Math.Matrix,RDR2.Math.Matrix)">
            <summary>
            Determines the product of two matrices.
            </summary>
            <param name="left">The first matrix to multiply.</param>
            <param name="right">The second matrix to multiply.</param>
            <returns>The product of the two matrices.</returns>
        </member>
        <member name="M:RDR2.Math.Matrix.Multiply(RDR2.Math.Matrix,System.Single)">
            <summary>
            Scales a matrix by the given value.
            </summary>
            <param name="left">The matrix to scale.</param>
            <param name="right">The amount by which to scale.</param>
            <returns>The scaled matrix.</returns>
        </member>
        <member name="M:RDR2.Math.Matrix.Divide(RDR2.Math.Matrix,RDR2.Math.Matrix)">
            <summary>
            Determines the quotient of two matrices.
            </summary>
            <param name="left">The first matrix to divide.</param>
            <param name="right">The second matrix to divide.</param>
            <returns>The quotient of the two matrices.</returns>
        </member>
        <member name="M:RDR2.Math.Matrix.Divide(RDR2.Math.Matrix,System.Single)">
            <summary>
            Scales a matrix by the given value.
            </summary>
            <param name="left">The matrix to scale.</param>
            <param name="right">The amount by which to scale.</param>
            <returns>The scaled matrix.</returns>
        </member>
        <member name="M:RDR2.Math.Matrix.Negate(RDR2.Math.Matrix)">
            <summary>
            Negates a matrix.
            </summary>
            <param name="matrix">The matrix to be negated.</param>
            <returns>The negated matrix.</returns>
        </member>
        <member name="M:RDR2.Math.Matrix.Inverse(RDR2.Math.Matrix)">
            <summary>
            Calculates the inverse of a matrix if it exists.
            </summary>
            <returns>The inverse of the matrix.</returns>
        </member>
        <member name="M:RDR2.Math.Matrix.Lerp(RDR2.Math.Matrix,RDR2.Math.Matrix,System.Single)">
            <summary>
            Performs a linear interpolation between two matrices.
            </summary>
            <param name="start">Start matrix.</param>
            <param name="end">End matrix.</param>
            <param name="amount">Value between 0 and 1 indicating the weight of <paramref name="end"/>.</param>
            <returns>The linear interpolation of the two matrices.</returns>
            <remarks>
            This method performs the linear interpolation based on the following formula.
            <code>start + (end - start) * amount</code>
            Passing <paramref name="amount"/> a value of 0 will cause <paramref name="start"/> to be returned; a value of 1 will cause <paramref name="end"/> to be returned.
            </remarks>
        </member>
        <member name="M:RDR2.Math.Matrix.RotationX(System.Single)">
            <summary>
            Creates a matrix that rotates around the x-axis.
            </summary>
            <param name="angle">Angle of rotation in radians. Angles are measured clockwise when looking along the rotation axis toward the origin.</param>
            <returns>The created rotation matrix.</returns>
        </member>
        <member name="M:RDR2.Math.Matrix.RotationY(System.Single)">
            <summary>
            Creates a matrix that rotates around the y-axis.
            </summary>
            <param name="angle">Angle of rotation in radians. Angles are measured clockwise when looking along the rotation axis toward the origin.</param>
            <returns>The created rotation matrix.</returns>
        </member>
        <member name="M:RDR2.Math.Matrix.RotationZ(System.Single)">
            <summary>
            Creates a matrix that rotates around the z-axis.
            </summary>
            <param name="angle">Angle of rotation in radians. Angles are measured clockwise when looking along the rotation axis toward the origin.</param>
            <returns>The created rotation matrix.</returns>
        </member>
        <member name="M:RDR2.Math.Matrix.RotationAxis(RDR2.Math.Vector3,System.Single)">
            <summary>
            Creates a matrix that rotates around an arbitrary axis.
            </summary>
            <param name="axis">The axis around which to rotate.</param>
            <param name="angle">Angle of rotation in radians. Angles are measured clockwise when looking along the rotation axis toward the origin.</param>
```
