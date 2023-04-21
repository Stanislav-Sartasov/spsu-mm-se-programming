namespace Bash.Core;

internal record CommandObject
{
	internal string Name { get; init; }
	
	internal List<string> Args { get; init; }
}