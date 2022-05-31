using Microsoft.VisualStudio.TestTools.UnitTesting;
using VarManager;

namespace VarManagerTests
{
    [TestClass]
    public class SessionVarsTests
    {
        [TestMethod]
        public void SetValueTest()
        {
            SessionVars sessionVars = new SessionVars();
            string key = "key";
            string value = "value";

            sessionVars.SetValue(key, value);

            Assert.AreEqual(value, sessionVars.GetValue(key));
        }

        [TestMethod]
        public void ResetValueTest()
        {
            SessionVars sessionVars = new SessionVars();
            string key = "key";
            string oldValue = "oldValue";
            string newValue = "newValue";

            sessionVars.SetValue(key, oldValue);
            sessionVars.SetValue(key, newValue);

            Assert.AreEqual(newValue, sessionVars.GetValue(key));
        }

        [TestMethod]
        public void GetInterpolatedStringOneVarTest()
        {
            SessionVars sessionVars = new SessionVars();
            string key = "counter";
            string value = "1027";
            string initialString = "Counter value is $counter";

            sessionVars.SetValue(key, value);
            string expected = "Counter value is 1027";
            string actual = sessionVars.GetInterpolatedString(initialString);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetInterpolatedStringManyVarsTest()
        {
            SessionVars sessionVars = new SessionVars();
            string[] keys = new string[] { "NAME", "SuRnAmE", "age", "citizenshiP" };
            string[] values = new string[] { "Bob", "Bobson", "29", "New Zealand" };
            string initialString = "NAME: $NAME, Surname: $SuRnAmE, age: $age, age:$age, citizenship: $citizenshiP";

            for (int i = 0; i < keys.Length; i++)
            {
                sessionVars.SetValue(keys[i], values[i]);
            }

            string expected = "NAME: Bob, Surname: Bobson, age: 29, age:29, citizenship: New Zealand";
            string actual = sessionVars.GetInterpolatedString(initialString);

            Assert.AreEqual(expected, actual);
        }
    }
}
