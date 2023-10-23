using WebDekanat.Containers;

IExamSystem examSystem = new LazySet();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapGet("/add/{studentID}/{courseID}", (long studentID, long courseID) => examSystem.Add(studentID, courseID) );
app.MapGet("/remove/{studentID}/{courseID}", (long studentID, long courseID) => examSystem.Remove(studentID, courseID) );
app.MapGet("/contains/{studentID}/{courseID}", (long studentID, long courseID) => examSystem.Contains(studentID, courseID) );
app.MapGet("/count", () => examSystem.Count);

app.Run();
