using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Demo_NullableReferenceTypes;


/// <remarks>
///     Another Example of Null Reference Types
///     -- C# 6.0 "Null-Coalescing Conditional Operator" ( ?. ) a.k.a. "Null-Safe Operator"
/// </remarks>
class Demo05b
{
    
    class Product
        : INotifyPropertyChanged
    {

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion


        private string? _Name;
        public string? Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                // OLD APPROACH
                //if (this._Name != value) { 
                //    this._Name = value;
                //    this.OnPropertyChanged( nameof(Name) );
                //}

                // The C# 5.0 approach
                // The SetField method centralizes the logic.
                bool changed = this.SetField(ref this._Name, value);
                if( changed )
                {
                    // Add the audit log entry, or do additional logic.
                }
            }
        }


        // C# 5 - CallMemberName used to identify the property's name.
        //        Needs default value for the attributed parameter.
        protected bool SetField<T>(
            ref T field, 
            T value, 
            [CallerMemberName] string? propertyName = null)
        {
            // Check if the value actually changed
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;   // FALSE to indicate change was unsuccessful.
            }

            // Update the backing field since the value has changed
            field = value;

            // Raise the Property Changed notification
            // C# 6 - null-safe operator used. 
            //        No need to check for event listeners.
            //        If there are no listeners, this will be a noop
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            return true;        // TRUE to indicate change was successful.
        }


    }



    public static void Run()
    {
        Product objProduct = new Product() { Name = "Rice" };

        Console.WriteLine($"Name of the Product: {objProduct.Name}");
        Console.WriteLine();

        // subscribe to property change notifications
        objProduct.PropertyChanged
            += (sender, e) =>
            {
                Product? objprod = sender as Product;
                Console.WriteLine($"{e.PropertyName} changed for the Product {objprod?.Name?.ToUpper()}!");
            };

        Console.WriteLine("-- Changing the name of the product now!");
        objProduct.Name = "Wheat";
        Console.WriteLine($"Name of the Product: {objProduct.Name}");
        Console.WriteLine();
    }

}
