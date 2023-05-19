using Dekanat.DekanatLib.StripedHashSet;
using Dekanat.DekanatLib.PhasedCuckooHashSet;
using Dekanat.DekanatLib;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
IExamSystem sys;

if (args.Length < 2)
    throw new Exception("Not enough arguments");

int size;
if (!int.TryParse(args[0], out size) || size <= 0)
    throw new Exception("Size must be possitive integer");

if (args[1] == "0")
    sys = new StripedHashSet(size);
else
    sys = new PhasedCuckooHashSet(size);

app.Run(async (context) =>
{
    var response = context.Response;
    var request = context.Request;
    var path = request.Path;

    (long, long) IdAndCourse()
    {
        var form = request.Form;

        return (long.Parse(form["id"].ToString()), long.Parse(form["course"].ToString()));
    }

    if (path == "/contains")
    {
        (var id, var course) = IdAndCourse();

        if (sys.Contains(id, course))
            await response.SendFileAsync("templates/yes.html");
        else
            await response.SendFileAsync("templates/no.html");
    }
    else if (path == "/add")
    {
        (var id, var course) = IdAndCourse();

        sys.Add(id, course);

        await response.SendFileAsync("templates/index.html");
    }
    else if (path == "/remove")
    {
        (var id, var course) = IdAndCourse();

        sys.Remove(id, course);

        await response.SendFileAsync("templates/index.html");
    }
    else if (path == "/count")
    {
        response.ContentType = "text/html; charset=utf-8";
        await response.WriteAsync($"<p><h>COUNT: {sys.Count()}</h></p><a href=\"/\">BACK TO INDEX</a>");
    }
    else
    {
        await response.SendFileAsync("templates/index.html");
    }
});

app.Run();