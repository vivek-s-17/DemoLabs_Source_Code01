namespace Demo_LiskovSubstitutionPrinciple.Demo01;

internal class Bird
{
    internal virtual void Fly()
    {
        Console.WriteLine("{0} is flying", this.GetType().Name.ToUpper());
    }
}

internal class Ostrich : Bird
{
    internal override void Fly()
    {
        base.Fly();
    }
}
