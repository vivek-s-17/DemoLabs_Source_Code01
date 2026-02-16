// C# 8.0: Demo of Ranges and Indexes


// Demo of Index
// Console.WriteLine( "======================== Demo 01" );
// Demo01();

// Demo of Range - using LINQ 
// Console.WriteLine("======================== Demo 02");
// Demo02();

// Demo of Range - using C# 8.0
Console.WriteLine( "======================== Demo 03" );
Demo03();


// Demo of Index
static void Demo01 ()
{
    string[] numbers = { 
                                    // index from start     index from end
                "one",              // [0]                  [^5]
                "two",              // [1]                  [^4]
                "three",            // [2]                  [^3]
                "four",             // [3]                  [^2]
                "five"              // [4]                  [^1]
            };

    Console.WriteLine( $"First number = {numbers[0]}" );
    Console.WriteLine();

    string lastItem;

    // Previously
    lastItem = numbers[numbers.Length - 1];
    Console.WriteLine( $"Last number = {lastItem}" );

    // Now...
    lastItem = numbers[^1];                              // "hat" operator 
    Console.WriteLine( $"Last number = {lastItem}" );

    Console.WriteLine();
}


// Demo of Range - using LINQ 
static void Demo02 ()
{
    string[] numbers = { "one", "two", "three", "four", "five" };

    // Previously, using LINQ Statement
    var query = (from item in numbers
                select item)
                .Skip(2)
                .Take(3);
    Console.WriteLine(string.Join(", ", query));
    Console.WriteLine();

    // OR using LINQ Lambda expression version
    IEnumerable<string> items3to5b = numbers.Skip(2).Take(3);
    Console.WriteLine(string.Join(", ", items3to5b));
    Console.WriteLine();

    // OR
    List<string> items3to5c = numbers.ToList().GetRange( 2, 3 );
    Console.WriteLine( string.Join( ", ", items3to5c ) );
    Console.WriteLine();

    Console.WriteLine();
}



// Demo of Range - using C# 8.0
static void Demo03 ()
{
    string[] numbers = { "one", "two", "three", "four", "five"
                         , "six", "seven", "eight", "nine", "ten" };

    Console.WriteLine( "---- 2..5" );
    var items2to5 = numbers[2..5];      // Range
    Console.WriteLine( string.Join( ", ", items2to5 ) );
    Console.WriteLine();

    Console.WriteLine();

    Console.WriteLine( "---- 3..7" );
    var range = 3..7;                   // System.Range declared as a variable
    var items = numbers[range];
    Console.WriteLine( string.Join( ", ", items ) );
    Console.WriteLine();

    Console.WriteLine();

    Console.WriteLine( "---- All OR [0..^0]" );
    range = Range.All;                  // same as [0..^0]
    Console.WriteLine( string.Join( ", ", numbers[range] ) );
    Console.WriteLine();

    Console.WriteLine();

    Console.WriteLine( "---- EndAt(5) OR [0..5] OR  <listName>[..5]" );
    range = Range.EndAt( 5 );             // first 5 items == same as [1..5]
    var first4numbers = numbers[..5];
    Console.WriteLine( string.Join( ", ", numbers[range] ) );
    Console.WriteLine();

    Console.WriteLine();

    Console.WriteLine( "---- StartAt(7) OR [7..10] OR <listName>[7..]" );
    range = Range.StartAt( 7 );           // last 3 items == same as [7..10]
    Console.WriteLine( string.Join( ", ", numbers[range] ) );
    Console.WriteLine();

    Console.WriteLine();

    Console.WriteLine( "---- [^7..^1]" );
    items = numbers[^7..^1];
    Console.WriteLine( string.Join( ", ", items ) );
    Console.WriteLine();

    Console.WriteLine();

    Console.WriteLine( "---- [..3]" );
    // range = [..3];                           // gives compiler error.  needs GetEnumerator() implementation!
    items = numbers[..3];                          
    Console.WriteLine( string.Join( ", ", items ) );
    Console.WriteLine();

    Console.WriteLine();

    Console.WriteLine( "---- [4..]" );
    // range = [4..];                           // gives compiler error.  needs GetEnumerator() implementation!
    items = numbers[4..];
    Console.WriteLine( string.Join( ", ", items ) );
    Console.WriteLine();

    Console.WriteLine();

    Console.WriteLine( "---- [..]" );
    // range = [..];                           // gives compiler error.  needs GetEnumerator() implementation!
    items = numbers[..];
    Console.WriteLine( string.Join( ", ", items ) );
    Console.WriteLine();

    Console.WriteLine();
}
