var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/dupa", () => "Hello World!");

app.Run();
