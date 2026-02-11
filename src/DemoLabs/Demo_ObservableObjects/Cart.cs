using System.Collections.Specialized;
using System.ComponentModel;

namespace Demo_ObservableObjects
{
    internal class Cart
        : System.Collections.Specialized.INotifyCollectionChanged,
          System.ComponentModel.INotifyPropertyChanged
    {

        #region System.Collections.Specialized.INotifyCollectionChanged members

        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        #endregion



        #region System.ComponentModel.INotifyPropertyChanged members

        public event PropertyChangedEventHandler? PropertyChanged;
        
        #endregion



        public int OrderNo { get; private set; }

        private DateTime _orderDate;
        public DateTime OrderDate 
        { 
            get
            {
                return _orderDate;
            }
            set
            {
                _orderDate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs( propertyName: nameof(OrderDate) ));
            }
        }

        


        private List<OrderItem>? orderItems { get; set; }        // aggregated collection!

        // built-in Observable Collection
        // public System.Collections.ObjectModel.ObservableCollection<OrderItem> Items { get; private set; }


        private static int counter;

        static Cart()
        {
            counter = 0;
        }

        public Cart() 
        {
            OrderNo = ++counter;
            orderItems = null;          // late-instantiation pattern
        }


        public override string ToString()
        {
            string orderDetails = "";
            orderDetails += "Order Number: " + this.OrderNo + "\n";
            orderDetails += "Order Date  : " + this.OrderDate.ToString("dd-MMM-yyyy") + "\n";
            if (orderItems is not null)
            {
                foreach (var item in orderItems)
                {
                    orderDetails += item + "\n";        // implictly calls newItem.ToString()
                }
            }
            else
            {
                orderDetails += "(no items in cart)" + "\n";
            }
            return orderDetails;
        }


        public OrderItem? this[int findSlNo]
        {
            get
            {
                // Late-Instantiation Pattern - null check
                if(orderItems is null)
                {
                    return null;
                }

                OrderItem? foundItem = null;
                foreach (var item in orderItems)
                {
                    if(item.SlNo == findSlNo)
                    {
                        foundItem = item;
                        break;
                    }
                }

                return foundItem;
            }
        }


        public void Add(OrderItem newItem)
        {
            // Late-Instantiation Design Pattern (instantiate the first time it is needed)
            if( orderItems is null)
            {
                orderItems = new List<OrderItem>();
            }

            orderItems.Add(newItem);

            if(this.CollectionChanged is not null)
            {
                var e 
                    = new NotifyCollectionChangedEventArgs(
                        action: NotifyCollectionChangedAction.Add, 
                        changedItem: newItem);
                /*****************
                    // check if the event is "subscribed"
                    if(CollectionChanged is not null)
                    {
                        // raise the event
                        CollectionChanged(this, e);
                    }
                **************/
                CollectionChanged?.Invoke(this, e);             // raise the event
            }
        }


        public void Remove(OrderItem item)
        {
            // Late-Instantiation Design Pattern safe code
            if (orderItems is null)
            {
                return;
            }

            if (orderItems.Remove(item))
            {
                var e 
                    = new NotifyCollectionChangedEventArgs(
                        action: NotifyCollectionChangedAction.Remove, 
                        changedItem: item);
                CollectionChanged?.Invoke(this, e);
            }
        }


        public void Edit(int findSlNo, OrderItem updatedItem)
        {
            if (orderItems is null)
            {
                return;
            }

            /*********************************************************
            //---  Find the position of the newItem to replace (USING FOR LOOP)
            int index = -1;
            for (int i = 0; i < orderItems.Count; i++)
            {
                if(orderItems[i] is OrderItem item && orderItems[i].SlNo == findSlNo)
                {
                    index = i;
                    break;
                }
            }

            //---  Find the position of the newItem to replace (USING FOREACH LOOP)
            int index = -1;
            foreach (var item in orderItems) 
            {
                ++index;
                if (item.SlNo == findSlNo)
                {
                    break;
                }
            }
            ****************/
            
            //---  Find the position of the newItem to replace
            int index = orderItems.FindIndex(x => x.SlNo == findSlNo);       // LAMBDA Version


            if (index < 0)
            {
                throw new ArgumentException(message: "SlNo not found!");
            }

            var oldItem = orderItems[index];            // get the old newItem
            orderItems[index] = updatedItem;            // replace the newItem in the collection

            // NOTE: EDIT is treated as a REPLACE action
            var e
                = new NotifyCollectionChangedEventArgs(
                    action: NotifyCollectionChangedAction.Replace, 
                    newItem: updatedItem,   
                    oldItem: oldItem);
            CollectionChanged?.Invoke(this, e);
        }


        public void Clear()
        {
            if (orderItems is null)
            {
                return;
            }

            orderItems.Clear();
            orderItems = null;                  // to address the need of Late-Instantion Pattern

            var e
                = new NotifyCollectionChangedEventArgs(
                    action: NotifyCollectionChangedAction.Reset);

            CollectionChanged?.Invoke(this, e);
        }
    }
}
