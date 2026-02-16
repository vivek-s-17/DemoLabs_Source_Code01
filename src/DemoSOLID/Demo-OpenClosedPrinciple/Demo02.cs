namespace Demo_OpenClosedPrinciple.Demo02;

/// <remarks>
///     BENEFITS of Open-Closed Principle
///     - minimized risk of bugs
///     - increased reusability
///     - improved maintainability
///     - use of oops concepts effectively, which results in a more flexible and dynamic architecture, 
///       where behavior can be changed dynamically at runtime.
/// </remarks>

internal abstract class Invoice
{
    virtual internal double GetDiscount(double totalAmount)
    {
        return totalAmount;
    }
}


internal class ProposedInvoice : Invoice
{
    override internal double GetDiscount(double totalAmount)
    {
        return  base.GetDiscount(totalAmount) - 50;
    }
}

internal class FinalInvoice : Invoice
{
    override internal double GetDiscount(double totalAmount)
    {
        return base.GetDiscount(totalAmount) - 100;
    }
}
