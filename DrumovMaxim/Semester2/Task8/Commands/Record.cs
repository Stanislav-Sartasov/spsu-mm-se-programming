namespace Commands;

public class Record
{
    public string Message { get; private set; }
    public bool ErrorOccured { get; private set; }
    public string ErrorMessage { get; private set; }
    
    public static Record NoSuchDirectory(string fileName) => new Record(String.Empty, true, $"{fileName}: such directory does not exist");
    public static Record NoSuchFile(string fileName) => new Record(String.Empty, true, $"{fileName}: such file does not exist");
    public static Record SuccessEmpty => new Record(String.Empty, false, "No error");
    public static Record Empty => new Record(String.Empty,false, "Empty");
    public static Record ManyArguments => new Record(String.Empty,true, "Too many arguments");
    public static Record NoArguments => new Record(String.Empty,true, "No arguments");  

    public Record(string message, bool errorOccured, string errorMessage)
    {
        Message = message;
        ErrorOccured = errorOccured;
        ErrorMessage = errorMessage;
    }
}