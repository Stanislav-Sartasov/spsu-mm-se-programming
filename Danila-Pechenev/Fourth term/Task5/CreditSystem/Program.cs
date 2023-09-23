using CreditSystem;
using DataStructures;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ICreditSystem, CreditSystem.CreditSystem>();
builder.Services.AddSingleton<DataStructures.ISet<Credit>, OptimisticSet<Credit>>();
builder.Services.AddControllers();

var app = builder.Build();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();