using Microsoft.VisualStudio.TestTools.UnitTesting;
using DateTimeManager;
using System.ComponentModel;

namespace DateTimeManagerTests
{
    [TestClass]
    public class DateAndTimeTests
    {
        [TestMethod]
        public void isEventEnvokedTest()
        {
            DateAndTime dateTime = new DateAndTime();
            string? changedPropertyName = null;
            dateTime.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                changedPropertyName = e.PropertyName;
            };

            dateTime.UpdateTime();

            Assert.AreEqual("Date", changedPropertyName);
        }
    }
}