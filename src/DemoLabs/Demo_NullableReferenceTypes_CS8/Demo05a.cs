using System.ComponentModel;


namespace Demo_NullableReferenceTypes;


/// <remarks>
///     Another Example of Null Reference Types
///     -- C# 6.0 "Null-Coalescing Conditional Operator" ( ?. ) a.k.a. "Null-Safe Operator"
/// </remarks>
class Demo05a
{
    class Product
        : INotifyPropertyChanged
    {

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion


        private string _name = string.Empty;
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
                OnPropertyChanged( nameof( Name ) );
            }
        }

        // Old approach
        protected void OnPropertyChanged(string propname)
        {
            //// check if the event is subscribed
            //if (this.PropertyChanged is not null)
            //{
            //    // raise the event!
            //    this.PropertyChanged(this, new PropertyChangedEventArgs(propname));
            //}

            // C# 6 - null-safe operator used. 
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }

    }


    public static void Run()
    {
        Product objProduct = new Product();
        objProduct.PropertyChanged += new PropertyChangedEventHandler(Demo05a.NameChanged);
        objProduct.Name = "Wheat";
    }

    private static void NameChanged(object? sender, PropertyChangedEventArgs e)
    {
        Console.WriteLine($"{e.PropertyName} changed!");
    }

}
