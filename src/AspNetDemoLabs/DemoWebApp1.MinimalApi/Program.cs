using System.Text;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// app.MapGet("/", () => "Hello World!");

app.MapGet("/", () =>
{
    StringBuilder sb = new StringBuilder();
    sb.AppendLine("<html>");
    sb.AppendLine("<head>");
    sb.AppendLine("</head>");
    sb.AppendLine("<body>");
    sb.AppendLine("<h1>Hello world from Minimal API</h1>");
    sb.AppendLine("</body>");
    sb.AppendLine("</html>");

    return sb.ToString();
});

app.MapGet("/data", () =>
{
    return new int[] { 1, 2, 3, 4, 5 };
});

app.MapGet("/employees", () =>
{
    List<Employee> employees = new List<Employee>()
    {
        new Employee() { Id = 1, Name = "First Employee" }
        , new Employee() { Id = 2, Name = "Second Employee" }
        , new Employee() { Id = 3, Name = "Third Employee" }
    };

    return employees;
});

app.Run();




class Employee
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
