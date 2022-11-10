namespace Commands;

public class CommandOutput
{
    public bool IsBashCommand { get; }
    public bool Empty { get; private set; }
    public int ArgumentNumber { get; private set; }
    public IEnumerable<Record> Message { get; private set; }
    
    public static CommandOutput EmptyBashCommand => new CommandOutput(true);
    public static CommandOutput EmptyNotBashCommand => new CommandOutput(false);
    
    public CommandOutput(bool bashCommandFlag, int argumentNumber, Record record)
    {
        IsBashCommand = bashCommandFlag;
        Empty = false;
        ArgumentNumber = 1;
        Message = new List<Record> { record };
    }

    public CommandOutput(bool bashCommandFlag, Record record) : this(bashCommandFlag, 1, record)
    {
        
    }
    
    private CommandOutput(bool bashCommandFlag)
    {
        IsBashCommand = bashCommandFlag;
        Empty = true;
        ArgumentNumber = 0;
        Message = new List<Record>();
    }

    public void AddRecord(Record record)
    {
        if (Empty) Empty = false;
        ArgumentNumber++; 
        Message = Message.Append(record);
    }
}