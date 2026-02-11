using System.Collections.Specialized;

namespace Demo_ObservableObjects
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Cart cart1 = new Cart()
            {
                OrderDate = DateTime.Now
            };

            // SHOW
            Console.WriteLine("--- Details of shopping cart #1 (after creating the cart!");
            Console.WriteLine(cart1);                    // implicitly calls cart1.ToString()
            Console.WriteLine();


            OrderItem item1 = new OrderItem()
            {
                SlNo = 1,
                ProductName = "Sugar",
                Price = 45M,
                Quantity = 5
            };
            cart1.Add(item1);

            // SHOW
            Console.WriteLine("--- Details of shopping cart #1 (after adding one item");
            Console.WriteLine(cart1);                    // implicitly calls cart1.ToString()
            Console.WriteLine();

            // -------------------------------------------------

            // Subscribe to the event INotifyCollectionChanged
            cart1.CollectionChanged
                += new NotifyCollectionChangedEventHandler(cart_CollectionChanged);

            // Subscribe to the event INotifyCollectionChanged for the ObservableCollection object
            // cart1.Items.CollectionChanged
            //    += new NotifyCollectionChangedEventHandler(cart_CollectionChanged);


            // -------------------------------------------------

            // ADD
            Console.WriteLine("Adding another item to Cart #1 (SlNo == 2)");
            cart1.Add(new OrderItem()
            {
                SlNo = 2,
                ProductName = "Wheat",
                Price = 30M,
                Quantity = 10
            });
            Console.WriteLine();

            // REPLACE
            Console.WriteLine("REPLACING an item in Cart #1 (SlNo == 1) with SlNo == 3");
            var editItem = new OrderItem()
            {
                SlNo = item1.SlNo,
                ProductName = item1.ProductName,
                Price = item1.Price,
                Quantity = item1.Quantity - 2                // changed from 5 to 3
            };
            cart1.Edit(
                findSlNo: item1.SlNo,                           // slno of the item to be replaced
                updatedItem: editItem);                     // updated item
            Console.WriteLine();

            // SHOW
            Console.WriteLine("--- Details of shopping cart #1");
            Console.WriteLine(cart1);                    // implicitly calls cart1.ToString()
            Console.WriteLine();

            // -------------------------------------------------

            // DELETE
            Console.WriteLine("REMOVE an item from Cart #1 (SlNo == 2)");
            OrderItem? itemToRemove = cart1[findSlNo: 2];
            if (itemToRemove is not null)
            {
                cart1.Remove(itemToRemove);
            }
            Console.WriteLine();

            // SHOW
            Console.WriteLine("--- Details of shopping cart #1");
            Console.WriteLine(cart1);                    // implicitly calls cart1.ToString()
            Console.WriteLine();

            // -------------------------------------------------

            // CLEAR
            Console.WriteLine("CLEAR out all items in the cart!");
            cart1.Clear();

            // SHOW
            Console.WriteLine("--- Details of shopping cart #1");
            Console.WriteLine(cart1);                    // implicitly calls cart1.ToString()
            Console.WriteLine();


            // Subscribe to the PropertyChanged event for the cart!
            cart1.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(cart1_PropertyChanged);


            Console.WriteLine("================changing the OrderDate of the Cart!");
            cart1.OrderDate = new DateTime(2000, 10, 28);
            Console.WriteLine();

            // SHOW
            Console.WriteLine("--- Details of shopping cart #1");
            Console.WriteLine(cart1);                    // implicitly calls cart1.ToString()
            Console.WriteLine();

        }


        private static void cart1_PropertyChanged(
            object? sender, 
            System.ComponentModel.PropertyChangedEventArgs e )
        {
            switch( e.PropertyName )
            {
                case nameof(Cart.OrderDate):            // if( e.PropertyName == "OrderDate" )
                    Console.WriteLine("ORDER DATE of the cart has changed!!!!!!!");
                    break;
                case nameof(Cart.OrderNo):
                    Console.WriteLine("ORDER NUMBER of the cart has changed!!!!!!!");
                    break;
            }
        }


        /// <summary>
        ///     Event Handler to support all actions performed on the collection!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        ///     Naming convention for the event handler: Object_EventName
        /// </remarks>
        private static void cart_CollectionChanged(
            object? sender, 
            NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    Console.WriteLine("--- The following item(s) were ADDED to the cart:");
                    if (e.NewItems is not null)
                    {
                        foreach (var item in e.NewItems)
                        {
                            Console.WriteLine(item);
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    Console.WriteLine("--- The following item(s) were REMOVED from the cart:");
                    if (e.OldItems is not null)
                    {
                        foreach (var item in e.OldItems)
                        {
                            Console.WriteLine(item);
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Replace:
                    Console.WriteLine("--- The following item(s) were REPLACED in the cart:");
                    if (e.OldItems is not null)
                    {
                        Console.WriteLine("------- OLD Items:");
                        foreach (var item in e.OldItems)
                        {
                            Console.WriteLine(item);
                        }
                    }
                    if (e.NewItems is not null)
                    {
                        Console.WriteLine("------- NEW Items:");
                        foreach (var item in e.NewItems)
                        {
                            Console.WriteLine(item);
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Reset:
                    Console.WriteLine("--- The cart was CLEARED of all items");
                    break;
            }

            Console.WriteLine();
        }
    }
}
