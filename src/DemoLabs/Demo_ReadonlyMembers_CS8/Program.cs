// C# 8.0: Demo of Readonly Members

using Demo_ReadonlyMembers;

MyStruct objStruct = new MyStruct { Name = "Struct object" };
objStruct.Display( "hello world" );

MyClass objClass = new MyClass { Name = "Class object" };
objClass.Display();
