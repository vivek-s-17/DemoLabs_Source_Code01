Console.WriteLine("Demo of INTERFACE SEGREGATION PRINCIPLE");


Demo_InterfaceSegregationPrinciple.Demo01.Crow crow = new();
crow.Eat(); 
crow.Fly();

Demo_InterfaceSegregationPrinciple.Demo01.Ostrich ostrich = new();
ostrich.Eat();
// ostrich.Fly();         // Compiler Error
// ( (Demo_InterfaceSegregationPrinciple.Demo01.IBirdWithFlight) ostrich ).Fly();  // runtime error
(ostrich as Demo_InterfaceSegregationPrinciple.Demo01.IBirdWithFlight)?.Fly();

// Demo_InterfaceSegregationPrinciple.Demo01.IBirdWithFlight obj
//    = new Demo_InterfaceSegregationPrinciple.Demo01.Ostrich();

