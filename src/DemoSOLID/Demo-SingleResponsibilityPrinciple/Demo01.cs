namespace Demo_SingleResponsibilityPrinciple.Demo01;


/// <remarks>
///     The class exhibits four responsibilities:
///     - add the invoice           (own transactional operations)
///     - delete the invoice        (own transactional operations)
///     - sending email             (additional responsibility)
///     - logging                   (additional responsibility)
///     If we wish to modify the logging functionality or sending email functionality, 
///     then we need to modify the Invoice class.
///      
///     Thus it voilates the Single Responsibility Principle due to the additional responsibilities.
/// </remarks>
internal class Invoice
{
    internal long Amount { get; set; }
    internal DateTime InvoiceDate { get; set; }

    internal void AddInvoice()
    {
        try
        {
            SaveInvoiceToDataStore();

            GenerateInvoiceDocument();

            SendInvoiceEmail();
        }
        catch (Exception ex)
        {
            // Error Logging
            System.IO.File.WriteAllText(@"c:\ErrorLog.txt", ex.ToString());
        }
    }

    internal void DeleteInvoice()
    {
        try
        {
            DeleteInvoiceInDataStore();
        }
        catch (Exception ex)
        {
            // Error Logging
            System.IO.File.WriteAllText(@"c:\ErrorLog.txt", ex.ToString());
        }
    }


    #region Helper Methods

    private void SaveInvoiceToDataStore()
    {
        Console.WriteLine("--- save the Invoice information to the database.");
    }

    private void GenerateInvoiceDocument()
    {
        Console.WriteLine("--- generate the Invoice PDF document.");
    }

    public void SendInvoiceEmail()
    {
        Console.WriteLine("--- send the confirmation email of the invoice");
    }

    private void DeleteInvoiceInDataStore()
    {
        Console.WriteLine("--- delete the existing Invoice information from the database.");
    }

    #endregion

}