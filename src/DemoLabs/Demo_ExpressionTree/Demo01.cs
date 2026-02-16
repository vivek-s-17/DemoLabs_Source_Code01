using System.Linq.Expressions;

namespace Demo_ExpressionTree;

/// <summary>
///     DEMO: Building Lambda Expression from Expression Tree
/// </summary>
internal static class Demo01
{
    internal static void RunThis ()
    {
        // int x = 2 + 15 / 3 - 1 * 7;
        //       = 2 +    5   - 1 * 7;
        //       = 2 +    5   -   7;
        //       =     7      -   7;
        //       = 0

        // int result = x + y + z;
        // LAMBDA: ( x, y, z ) => x + y + z;
        //                        ( x + y ) + z

        // Step 1:
        // Define parameters for the lambda expression.
        // These represent the three input numbers.
        ParameterExpression param1 = Expression.Parameter( typeof( int ), "x" );
        ParameterExpression param2 = Expression.Parameter( typeof( int ), "y" );
        ParameterExpression param3 = Expression.Parameter( typeof( int ), "z" );

        Console.WriteLine( "--- Created three parameter expressions" );
        Console.WriteLine( $"  Parameter 1: {param1} (Type: {param1.Type})" );
        Console.WriteLine( $"  Parameter 2: {param2} (Type: {param2.Type})" );
        Console.WriteLine( $"  Parameter 3: {param3} (Type: {param3.Type})" );
        Console.WriteLine();


        // Step 2:
        // To build the expression tree for adding three numbers,
        // define the expression to add the first two numbers (x + y)
        BinaryExpression firstAdd = Expression.Add( param1, param2 );

        Console.WriteLine( "--- Created first addition expression" );
        Console.WriteLine( $"  Expression: {firstAdd}" );
        Console.WriteLine( $"  Node Type: {firstAdd.NodeType}" );
        Console.WriteLine( $"  Left Operand: {firstAdd.Left}" );
        Console.WriteLine( $"  Right Operand: {firstAdd.Right}" );
        Console.WriteLine();

        // Then, define the expression to add the result to the third number: (x + y) + z
        BinaryExpression secondAdd = Expression.Add( firstAdd, param3 );

        Console.WriteLine( "--- Created second addition expression" );
        Console.WriteLine( $"  Expression: {secondAdd}" );
        Console.WriteLine( $"  Node Type: {secondAdd.NodeType}" );
        Console.WriteLine( $"  Left Operand: {secondAdd.Left}" );
        Console.WriteLine( $"  Right Operand: {secondAdd.Right}" );
        Console.WriteLine();



        // Step 3: Create the lambda expression from the expression tree
        // Func<int, int, int, int> represents a function that takes three INTs and returns an INT
        Expression<Func<int, int, int, int>> lambdaExpression 
            = Expression.Lambda<Func<int, int, int, int>>(
                secondAdd,
                param1,
                param2,
                param3
            );

        Console.WriteLine( "--- Created Lambda Expression:" );
        Console.WriteLine( $"  Full Expression: {lambdaExpression}" );
        Console.WriteLine( $"  Expression Type: {lambdaExpression.Type}" );
        Console.WriteLine( $"  Return Type: {lambdaExpression.ReturnType}" );
        Console.WriteLine( $"  Parameter Count: {lambdaExpression.Parameters.Count}" );
        Console.WriteLine();

        Console.WriteLine( "=== Expression Tree Structure:" );
        Console.WriteLine( $"   Node Type: {lambdaExpression.Body.NodeType}" );
        Console.WriteLine( $"   Body: {lambdaExpression.Body}" );
        Console.WriteLine( $"   Parameters: {string.Join( ", ", lambdaExpression.Parameters )}" );
        if ( lambdaExpression.Body is BinaryExpression binaryExpr )
        {
            Console.WriteLine( $"   Binary Expression Details:" );
            Console.WriteLine( $"      LHS: {binaryExpr.Left}" );
            Console.WriteLine( $"      RHS: {binaryExpr.Right}" );
            Console.WriteLine( $"      Operator: {binaryExpr.NodeType}" );
        }
        Console.WriteLine();


        // Step 4: Compile the expression tree into executable code
        Func<int, int, int, int> compiledLambda = lambdaExpression.Compile();

        Console.WriteLine( "--- Compiled the expression tree into a delegate" );
        Console.WriteLine( $"  Delegate Type: {compiledLambda.GetType()}" );
        Console.WriteLine();



        // NEXT STEP: Test the compiled lambda function
        Console.WriteLine( "\n=== Testing the Compiled Lambda ===\n" );

        int a = 5, b = 10, c = 15;
        int result = compiledLambda( a, b, c );
        Console.WriteLine( $"Input: x = {a}, y = {b}, z = {c}" );
        Console.WriteLine( $"Result: {a} + {b} + {c} = {result}" );
        Console.WriteLine();

    }

}
