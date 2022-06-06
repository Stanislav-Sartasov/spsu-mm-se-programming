using System;
using System.Collections.Generic;

namespace Task_2
{
    public class BashSimulator
    {
        private Dictionary<string, string> variables;


        public BashSimulator()
        {
            variables = new Dictionary<string, string>();
        }

        public void SetVariable(string name, string value)
        {
            variables[name] = value;
        }

        public string InsertVariables(string initialStr)
        {
            string result = "";
            for (int i = 0; i < initialStr.Length; i++)
            {
                if (initialStr[i] != '$')
                {
                    result += initialStr[i];
                }

                else
                {
                    bool isNotVariable = true;
                    string variableName = "";
                    i++;

                    while (i != initialStr.Length && initialStr[i] != ' ' && initialStr[i] != '\"')
                    {
                        variableName += initialStr[i];
                        i++;
                    }

                    foreach (var variable in variables)
                    {
                        if (variable.Key == variableName)
                        {
                            result += variable.Value;
                            isNotVariable = false;
                            break;
                        }
                    }

                    if (isNotVariable)
                    {
                        result += "$" + variableName;
                    }

                    if (i < initialStr.Length && initialStr[i] == ' ')
                    {
                        result += ' ';
                    }
                }
            }
            return result;
        }
    }
}
