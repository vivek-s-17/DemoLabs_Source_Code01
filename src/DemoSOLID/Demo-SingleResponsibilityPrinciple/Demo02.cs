namespace Demo_SingleResponsibilityPrinciple.Demo02;

/// <remarks>
///     BENEFITS of Single Responsiblity Principle (SRP):
///     - easier to understand
///     - easier to modify
///     - easier to test
///     - increased reusability
///     - better organization of classes and modules
/// </remarks>


public interface ILogger
{
    void Info(string info);
    void Debug(string info);
    void Error(string message, Exception ex);
}


internal class Logger : ILogger
{
    internal Logger()
    {
        //TODO: write code for initialization like Creating the Log file with necesssary details
    }

    public void Info(string info)
    {
        //TODO: write code for logging Information events into the ErrorLog 
    }

    public void Debug(string info)
    {
        //TODO: write code for logging Debug events into the ErrorLog
    }

    public void Error(string message, Exception ex)
    {
        //TODO: write code for logging Error events into the ErrorLog 
    }

}


internal class MailSender
{
    internal required string EMailFrom { get; set; }
    internal required string EMailTo { get; set; }
    internal required string EMailSubject { get; set; }
    internal required string EMailBody { get; set; }

    internal void SendEmail()
    {
        //TODO: write code for sending the mail
    }
}

internal class Invoice
{
    internal long Amount { get; set; }
    internal DateTime InvoiceDate { get; set; }

    private readonly ILogger fileLogger;
    private MailSender? emailSender;


    internal Invoice()
    {
        fileLogger = new Logger();
    }


    internal void AddInvoice()
    {
        try
        {
            fileLogger.Info("Add Invoice method START @" + DateTime.Now);

            //TODO: write code for adding invoice information to the database

            // Once the Invoice has been added, send the email
            emailSender = new MailSender()
            {
                EMailFrom = "emailfrom@xyz.com",
                EMailTo = "emailto@xyz.com",
                EMailSubject = "Single Responsibility Princile",
                EMailBody = "A class should have only one reason to change"
            };
            emailSender.SendEmail();
        }
        catch (Exception ex)
        {
            fileLogger.Error("Error occurred while Generating Invoice", ex);
        }
        finally
        {
            fileLogger.Info("Add Invoice END @" + DateTime.Now);
        }

    }


    internal void DeleteInvoice()
    {
        try
        {
            fileLogger.Info("Delete Invoice START at @" + DateTime.Now);

            //TODO: write code for Deleting the already generated invoice

        }
        catch (Exception ex)
        {
            fileLogger.Error("Error Occurred while Deleting Invoice", ex);
        }
        finally
        {
            fileLogger.Info("Delete Invoice END @" + DateTime.Now);
        }
    }

}