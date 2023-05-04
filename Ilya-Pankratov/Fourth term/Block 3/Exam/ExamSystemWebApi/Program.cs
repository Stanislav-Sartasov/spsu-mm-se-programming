using ExamLib;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add exam system

if (args.Length == 1)
{
    if (args[0].ToLower() == "cuckoo")
    {
        builder.Services.AddScoped<IExamSystem, CuckooExamSystem>();
    }
    else
    {
        builder.Services.AddScoped<IExamSystem, DefaultExamSystem>();
    }
}
else
{
    builder.Services.AddScoped<IExamSystem, DefaultExamSystem>();
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