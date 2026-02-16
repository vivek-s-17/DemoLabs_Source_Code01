namespace Demo_NullableReferenceTypes;


class Demo03
{
    class Order
    {
    }

    class Customer
    {
        public List<Order>? Orders { get; set; }
    }


    public static void Run()
    {
        List<Customer>? customers = new List<Customer>();

        // Old approach
        int customerCount = customers != null ? customers.Count : default;
        Customer? firstCustomer 
            = customers != null && customers.Count > 0 ? customers[0] : null;
        int firstCustomerOrderCount =
            (customers != null && customers.Count > 0 && customers[0].Orders != null)
            ? customers[0].Orders?.Count ?? 0
            : default;

        // C# 6.0 approach - using "Null-Coalescing Conditional Operator" (?.)
        //                   also known as "Null-Safe Operator" (?.)
        int? customerCount2 = customers?.Count;
        Customer? firstCustomer2 = customers?.Count > 0 ? customers?[0] : null;
        int? firstCustomerOrderCount2 = customers?.Count > 0 ? customers?[0].Orders?.Count : default;
    }
}
