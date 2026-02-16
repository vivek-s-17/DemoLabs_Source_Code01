namespace Demo_OpenClosedPrinciple.Demo01;

internal enum InvoiceType
{
    FinalInvoice,
    ProposedInvoice
};


internal class Invoice
{

    /// <remarks>
    ///     Problems of not following Open Closed Principle:
    ///     - The problem here is that for each new InvoiceType, an appropriate IF..ELSE or SWITCH CASE 
    ///       would need to be added.  
    ///     - Tests would have to be run for the entire functionality, including for the old and new functionalities.
    ///     - It is the responsibility of the Developer to inform the QA (Quality Assurance) team about the changes 
    ///       in advance so that they can prepare for Regression Testing and new Feature Testing.
    ///     - If you are not following the Open-Closed Principle, it also breaks the Single Responsibility Principle, 
    ///       as the class or module will perform multiple responsibilities.
    ///    - Implementing all the functionalities in a single class makes maintenance very difficult.
    /// </remarks>
    internal double GetDiscount(double totalAmount, InvoiceType invoiceType)
    {
        double invoiceAmount = 0;

        switch(invoiceType)
        {
            case InvoiceType.ProposedInvoice:
                invoiceAmount = totalAmount - 50;
                break;
            case InvoiceType.FinalInvoice:
                invoiceAmount = totalAmount - 100;
                break;
            default:
                invoiceAmount = totalAmount;
                break;
        }

        return invoiceAmount;
    }
}
