namespace Demo_IOC;

internal class Process2b
{
    internal event StepHandler? Step2Event;


    // Inversion of Control (IoC) using Event mapped to the Delegate
    // the invoker of DoSomething controls when to invoke the method pointed to by the delegate
    internal void DoSomething()
    {
        this.Step1();

        Step2Event?.Invoke();           // raise the event.

        this.Step3();
    }


    // Inversion of Control (IoC) using Event mapped to the Delegate
    // the invoker of DoSomething controls when to invoke the method pointed to by the delegate
    internal void DoSomethingInParallel()
    {
		bool isEventSubscribed = Step2Event is not null;

        this.Step1();

		if( isEventSubscribed )
		{
			// raise the event in a different thread.
            System.Threading.Thread t = new Thread(() => Step2Event?.Invoke());
            t.Start();
		}

        this.Step3();

        if( isEventSubscribed )
        {
			// join the branch thread (running the event handler) back the branch thread
        	t.Join();
		}
    }


    private void Step1()
    {
        Console.WriteLine("-- step 1");
    }

    private void Step3()
    {
        Console.WriteLine("-- step 3");
    }

}
