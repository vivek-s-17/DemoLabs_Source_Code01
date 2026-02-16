// C# 8.0: Demo of Default Interface Methods

using Demo_DefaultInterfaceMethods;

Car objCar = new Car("FORD 101");
objCar.ShowInfo();
(objCar as IVehicle).DisplayInfo();                 // interface method called explicitly
Console.WriteLine();

IVehicle objCar2 = new Car("Maruti 800");
objCar2.ShowInfo();
objCar2.DisplayInfo();                              // interface method called explicitly (object is typed)
Console.WriteLine();


Scooter objScooter = new Scooter("Vespa 420");
objScooter.ShowInfo();
objScooter.DisplayInfo();
Console.WriteLine();

// -------

// Exceutes the method in the class - not from the interface default method
// shows same behaviour as an Extension method to the interface would do!
(objScooter as IVehicle).DisplayInfo();
Console.WriteLine();


