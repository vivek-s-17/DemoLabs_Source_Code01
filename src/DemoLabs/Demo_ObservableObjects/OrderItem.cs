using System.ComponentModel;

namespace Demo_ObservableObjects
{
    internal class OrderItem
        : INotifyPropertyChanged
    {

        #region System.ComponenetModel.INotifyPropertyChanged members

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion


        public int SlNo { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public decimal ItemCost
        {
            get
            {
                return Price * Quantity;
            }
        }

        public override string ToString()
        {
            return string.Format("{0} {1,-15} {2,10:C} {3,5} {4,12:C}", 
                this.SlNo, this.ProductName, this.Price, this.Quantity, this.ItemCost);
        }

    }
}
