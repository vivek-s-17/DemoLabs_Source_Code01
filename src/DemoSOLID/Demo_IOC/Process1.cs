namespace Demo_IOC;

internal class Process1
{

    // Demo of Control
    // the method controls what to call, and how to call it.
    internal void DoSomething()
    {
        this.Step1();
        this.Step2();
        this.Step3();
    }

    private void Step1()
    {
        Console.WriteLine("-- step 1");
    }

    private void Step2()
    {
        Console.WriteLine("-- step 2");
    }

    private void Step3()
    {
        Console.WriteLine("-- step 3");
    }
}
