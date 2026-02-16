// DI = Dependency Injection
using Demo_DI;

Console.WriteLine("--- demo of IoC using Interface");
IStepActivity objGold = new GoldVersionProcess();
IStepActivity objSilver = new SilverVerisionProcess();

Process4 p3a = new Process4(objGold);
p3a.DoSomething();
p3a.DoSomethingElse();
p3a.DoDifferent();
Console.WriteLine();


Process4 p3b = new Process4(objSilver);
p3b.DoSomething();
p3b.DoDifferent();
Console.WriteLine();