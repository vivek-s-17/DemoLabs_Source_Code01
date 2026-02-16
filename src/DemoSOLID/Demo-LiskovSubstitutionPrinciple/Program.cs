Console.WriteLine("Demo of LISKOV SUBSTITUTION PRINCIPLE");

Demo01Test();
Console.WriteLine();
Demo02Test();


static void Demo01Test()
{
    Demo_LiskovSubstitutionPrinciple.Demo01.Bird bird 
        = new Demo_LiskovSubstitutionPrinciple.Demo01.Bird();
    bird.Fly();

    Demo_LiskovSubstitutionPrinciple.Demo01.Bird ostrich 
        = new Demo_LiskovSubstitutionPrinciple.Demo01.Ostrich();
    ostrich.Fly();
}


static void Demo02Test()
{
    Demo_LiskovSubstitutionPrinciple.Demo02.Crow crow = new();
    crow.Fly();

    Demo_LiskovSubstitutionPrinciple.Demo02.Ostrich ostrich = new();
    // ostrich.Fly();         // Compiler Error
}
