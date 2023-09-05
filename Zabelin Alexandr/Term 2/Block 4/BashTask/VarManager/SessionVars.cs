using System.Collections;

namespace VarManager
{
    public class SessionVars
    {
        private List<IVar> vars = new List<IVar>();


        public string GetValue(string key)
        {
            IVar? var = this.FindVar(key);

            if (var == null)
            {
                return "";
            }

            return var.Value;
        }

        public void SetValue(string key, string value)
        {
            IVar? var = this.FindVar(key);

            if (var == null)
            {
                IVar newVar = new Var(key, value);

                vars.Add(newVar);
            }
            else
            {
                var.Value = value;
            }
        }

        public string GetInterpolatedString(string initial)
        {
            string interpolatedString = initial;
            
            foreach (IVar var in vars)
            {
                interpolatedString = interpolatedString.Replace($"${var.Key}", var.Value);
            }

            return interpolatedString;
        }

        private IVar? FindVar(string key)
        {
            foreach (IVar var in this.vars)
            {
                if (var.Key == key)
                {
                    return var;
                }
            }

            return null;
        }
    }
}