using ExamLib;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add exam system

if (args.Length == 1)
{
    switch (args[0].ToLower())
    {
        case "core-cuckoo":
            builder.Services.AddScoped<IExamSystem, ExamSystem<CoreCockooHashSet<StudentPassedExam>>>();
            break;
        case "striped-cuckoo":
            builder.Services.AddScoped<IExamSystem, ExamSystem<StripedCuckooHashSet<StudentPassedExam>>>();
            break;
        case "default":
            builder.Services.AddScoped<IExamSystem, ExamSystem<StripedHashSet<StudentPassedExam>>>();
            break;
        default:
            builder.Services.AddScoped<IExamSystem, ExamSystem<StripedHashSet<StudentPassedExam>>>();
            break;
    }
}
else
{
    builder.Services.AddScoped<IExamSystem, ExamSystem<StripedHashSet<StudentPassedExam>>>();
}

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();