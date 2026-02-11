namespace Demo_Partial
{
    // NOTE: Partial class cannot span across assemblies!
    public partial class Employee
    {
        public int Id { get; set; }

        #region IDisposable members
        public void Dispose()
        {

        }
        #endregion
    }
}
