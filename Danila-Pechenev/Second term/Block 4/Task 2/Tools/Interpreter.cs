﻿namespace Tools;

public class Interpreter
{
    private Dictionary<string, ICommand> Commands { get; set; }

    private static string ValidСharacters { get; set; } =
        "abcdefghijklmnopqrstuvwxyz" + 
        "ABCDEFGHIJKLMNOPQRSTUVWXYZ" + 
        "абвгдеёжзийклмнопрстуфхцчшщъыьэюя" + 
        "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ" + 
        "0123456789";

    private static string SpecialСharacters { get; set; } = " .!,:;@#№%\\^&?*()$-+=|/<>[]`_\'{}\"";

    public Interpreter(List<ICommand> listOfCommands)
    {
        Commands = new Dictionary<string, ICommand>();
        foreach (var command in listOfCommands)
        {
            Commands.Add(command.Name, command);
        }
    }

    public ResultCode ExecuteLine(string line, out string result)
    {
        result = "";
        line = line.Trim();
        var firstToken = line.Split(' ')[0];
        line = InsertVariablesAndEscapedCharacters(line);
        if (firstToken.Length != 0 && line.Length == 0)
        {
            result += AddLineToErrorList(ResultCode.CommandNotFound, "", false);
        }

        string[] parsedCommands = SplitByCommands(line);
        if (parsedCommands.Length == 0)
        {
            return ResultCode.Success;  // No commands
        }
        
        ResultCode code = ParseCommand(parsedCommands[0], out string lastResult);
        if (code == ResultCode.Exit)
        {
            return ResultCode.Exit;
        }

        result += AddLineToErrorList(code, lastResult);
        if (code != ResultCode.Success)
        {
            lastResult = "";
        }

        for (int i = 1; i < parsedCommands.Length; i++)
        {
            code = ParseCommand(parsedCommands[i], out lastResult, true, lastResult);
            if (code == ResultCode.Exit)
            {
                return ResultCode.Exit;
            }

            result += AddLineToErrorList(code, lastResult);
            if (code != ResultCode.Success)
            {
                lastResult = "";
            }
        }

        if (code == ResultCode.Success)
        {
            result += lastResult + Environment.NewLine;
        }

        result = (result.Length > 0) ? result.Remove(result.Length - Environment.NewLine.Length) : result;
        return ResultCode.Success;
    }

    public ResultCode ParseCommand(string line, out string result, bool stdin = false, string stdinLine = "")
    {
        result = "";
        line = line.Trim();
        var tokens = new List<string>(line.Split(' ').Where(x => !string.IsNullOrEmpty(x)));
        if (tokens.Count == 0)
        {
            return ResultCode.Success;
        }

        string firstToken = tokens[0];
        var parsedCommand = ParseArguments(firstToken);
        firstToken = (parsedCommand == null || parsedCommand.Count == 0) ? firstToken : parsedCommand[0];
        if (firstToken == "exit")
        {
            return ResultCode.Exit;
        }

        if (Commands.ContainsKey(firstToken))
        {
            tokens.RemoveAt(0);
            var argumentsLine = string.Join(' ', tokens.ToArray());
            var arguments = ParseArguments(argumentsLine);
            if ((!stdin || Commands[firstToken].RequiresArgumentsInStdinMode) && (arguments == null))
            {
                return ResultCode.UnexpectedSequence;
            }

            bool success = Commands[firstToken].Execute(arguments, out result, stdin, stdinLine);
            return success ? ResultCode.Success : ResultCode.CommandReturnedError;
        }
        else
        {
            bool added = false;
            bool run = false;
            if (line.Contains('='))
            {
                added = TryAddVariable(line);
            }

            if (added)
            {
                return ResultCode.Success;
            }
            else
            {
                run = TryUseProgramlauncher(line, out string text);
                if (run)
                {
                    result = text;
                    return ResultCode.Success;
                }
            }

            return line.Contains("=") ? ResultCode.UnexpectedSequence : ResultCode.CommandNotFound;
        }
    }

    public static List<string>? ParseArguments(string argumentsLine, bool insertVariablesAndEscapedCharacters = false)
    {
        if (insertVariablesAndEscapedCharacters)
        {
            argumentsLine = InsertVariablesAndEscapedCharacters(argumentsLine);
        }

        var arguments = new List<string>();
        bool doubleQuotationMarkZone = false;
        bool singleQuotationMarkZone = false;
        string currentArg = "";

        for (int i = 0; i < argumentsLine.Length; i++)
        {
            if (argumentsLine[i] == '\'')
            {
                if (doubleQuotationMarkZone)
                {
                    currentArg += "\'";
                }
                else
                {
                    singleQuotationMarkZone = !singleQuotationMarkZone;
                }
            }
            else if (argumentsLine[i] == '\"')
            {
                if (singleQuotationMarkZone)
                {
                    currentArg += "\"";
                }
                else
                {
                    doubleQuotationMarkZone = !doubleQuotationMarkZone;
                }
            }
            else if (argumentsLine[i] == ' ')
            {
                if (!singleQuotationMarkZone && !doubleQuotationMarkZone)
                {
                    arguments.Add(currentArg);
                    currentArg = "";
                }
                else
                {
                    currentArg += " ";
                }
            }
            else if (ValidСharacters.Contains(argumentsLine[i]) || SpecialСharacters.Contains(argumentsLine[i]))
            {
                currentArg += argumentsLine[i];
            }
            else
            {
                return null;
            }
        }

        if (currentArg.Length != 0)
        {
            if (!singleQuotationMarkZone && !doubleQuotationMarkZone)
            {
                arguments.Add(currentArg);
            }
            else
            {
                return null;
            }
        }

        return arguments;
    }

    private static string InsertVariablesAndEscapedCharacters(string line)
    {
        string result = "";
        bool doubleQuotationMarkZone = false;
        bool singleQuotationMarkZone = false;
        bool shielding = false;
        bool variableNameStarted = false;
        string currentVariableName = "";
        string currentArg = "";

        for (int i = 0; i < line.Length; i++)
        {
            if (!shielding)
            {
                if (variableNameStarted && !ValidСharacters.Contains(line[i]))
                {
                    var variableValue = Runtime.GetVariable(currentVariableName);
                    if (variableValue != null)
                    {
                        currentArg += variableValue;
                    }

                    variableNameStarted = false;
                    currentVariableName = "";
                }

                if (line[i] == '\'')
                {
                    if (!doubleQuotationMarkZone)
                    {
                        singleQuotationMarkZone = !singleQuotationMarkZone;
                    }

                    currentArg += line[i];
                }
                else if (line[i] == '\"')
                {
                    if (!singleQuotationMarkZone)
                    {
                        doubleQuotationMarkZone = !doubleQuotationMarkZone;
                    }

                    currentArg += line[i];
                }
                else if (line[i] == '$')
                {
                    if (singleQuotationMarkZone)
                    {
                        currentArg += line[i];
                    }
                    else
                    {
                        variableNameStarted = true;
                    }
                }
                else if (line[i] == ' ')
                {
                    if (!singleQuotationMarkZone && !doubleQuotationMarkZone)
                    {
                        result += currentArg + " ";
                        currentArg = "";
                    }
                    else
                    {
                        currentArg += " ";
                    }
                }
                else if (line[i] == '\u005C')
                {
                    if (singleQuotationMarkZone)
                    {
                        currentArg += "\u005C";
                    }
                    else
                    {
                        shielding = true;
                    }
                }
                else if (ValidСharacters.Contains(line[i]))
                {
                    if (variableNameStarted)
                    {
                        currentVariableName += line[i];
                    }
                    else
                    {
                        currentArg += line[i];
                    }
                }
                else
                {
                    currentArg += line[i];
                }
            }
            else
            {
                if (line[i] == ' ')
                {
                    currentArg += "\" \"";
                }
                else
                {
                    currentArg += line[i];
                }

                shielding = false;
            }
        }

        if (variableNameStarted)
        {
            var variableValue = Runtime.GetVariable(currentVariableName);
            if (variableValue != null)
            {
                currentArg += variableValue;
            }
        }

        if (currentArg.Length != 0)
        {
            result += currentArg;
        }

        return result;
    }
    
    private static string AddLineToErrorList(ResultCode code, string resultOfCommand, bool addNewLine = true)
    {
        if (code == ResultCode.CommandNotFound)  // command not found
        {
            if (addNewLine)
            {
                return "command not found" + Environment.NewLine;
            }

            return "command not found";
        }

        if (code == ResultCode.UnexpectedSequence)  // unexpected sequence
        {
            if (addNewLine)
            {
                return "unexpected sequence" + Environment.NewLine;
            }

            return "unexpected sequence";
        }

        if (code == ResultCode.CommandReturnedError)  // command returned error message
        {
            if (addNewLine)
            {
                return resultOfCommand + Environment.NewLine;
            }

            return resultOfCommand;
        }

        return "";
    }

    private static string[] SplitByCommands(string line)
    {
        var commands = new List<string>();
        bool doubleQuotationMarkZone = false;
        bool singleQuotationMarkZone = false;
        string currentCommand = ""; 

        for (int i = 0; i < line.Length; i++)
        {
            if (line[i] == '|')
            {
                if (!singleQuotationMarkZone && !doubleQuotationMarkZone)
                {
                    commands.Add(currentCommand);
                    currentCommand = "";
                    continue;
                }
            }
            else if (line[i] == '\"')
            {
                doubleQuotationMarkZone = !doubleQuotationMarkZone;
            }
            else if (line[i] == '\'')
            {
                singleQuotationMarkZone = !singleQuotationMarkZone;
            }

            currentCommand += line[i];
        }

        if (currentCommand.Length > 0)
        {
            commands.Add(currentCommand);
        }

        return commands.ToArray();
    }

    private static bool TryAddVariable(string line)
    {
        string[] nameAndArgumentsLine = line.Split('=', 2);
        if (nameAndArgumentsLine.Length < 2)
        {
            return false;
        }

        string name = nameAndArgumentsLine[0];
        string argumentsLine = nameAndArgumentsLine[1];
        string? value = nameAndArgumentsLine[1] != "" ? ParseArguments(argumentsLine)?[0] : "";
        
        if (value != null)
        {
            if (ValidateName(name) && ValidateName(value, true, true))
            {
                Runtime.AddVariable(name, value);
                return true;
            }
        }

        return false;
    }

    private static bool TryUseProgramlauncher(string line, out string text)
    {
        string[] nameAndArgumentsLine = line.Split(' ', 2).Where(x => !string.IsNullOrEmpty(x)).ToArray();
        string name = nameAndArgumentsLine[0];
        string? argumentsLine = null;
        if (nameAndArgumentsLine.Length > 1)
        {
            argumentsLine = nameAndArgumentsLine[1];
        }

        return Programlauncher.TryLaunchProgram(name, argumentsLine, out text);
    }

    private static bool ValidateName(string name, bool CanBeDigitTheFirst = false, bool CanBeSpecialСharacters = false)
    {
        if (name.Length == 0)
        {
            return false;
        }

        if (!CanBeDigitTheFirst && char.IsDigit(name[0]))
        {
            return false;
        }

        var available = ValidСharacters;
        if (CanBeSpecialСharacters)
        {
            available += SpecialСharacters;
        }

        foreach (var s in name)
        {
            if (!available.Contains(s))
            {
                return false;
            }
        }

        return true;
    }
}
