using ExamSystem;
using Sets;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

ExamSystem.ExamSystem examSystem;

if (args.Length == 1 && args[0].ToLower().Equals("fine-grained"))
{
    examSystem = new(new FineGrainedSet<Credit>());
}
else
{
    examSystem = new(new OptimisticSet<Credit>());
}

app.MapPost("/api/add", (long studentId, long courseId) => examSystem.Add(studentId, courseId));
app.MapDelete("/api/remove", (long studentId, long courseId) => examSystem.Remove(studentId, courseId));
app.MapGet("/api/contains", (long studentId, long courseId) => examSystem.Contains(studentId, courseId));
app.MapGet("/api/count", () => examSystem.Count);

app.Run();