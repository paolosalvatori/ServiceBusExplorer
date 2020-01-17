#region Using Directives

using ServiceBusExplorer.Utilities.Helpers;
using NUnit.Framework;

#endregion

namespace ServiceBusExplorer.Tests.Helpers
{
    [TestFixture]
    public class ConnectionStringHelperTests
    {
        [Test]
        public void IsEntitySpecific_NoEntityPath_ReturnsFalse()
        {
            string connectionString = "Endpoint=sb://some-namespace.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=ejfklwejfwlkejfwfflwkejfklwef";

            Assert.IsFalse(ConnectionStringHelper.IsEntitySpecific(connectionString));
        }

        [Test]
        public void IsEntitySpecific_EntityPathInTheEnd_ReturnsTrue()
        {
            string connectionString = "Endpoint=sb://some-namespace.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=ejfklwejfwlkejfwfflwkejfklwef;EntityPath=loremipsum";

            Assert.IsTrue(ConnectionStringHelper.IsEntitySpecific(connectionString));
        }

        [Test]
        public void IsEntitySpecific_EntityPathInTheMiddle_ReturnsFalse()
        {
            string connectionString = "Endpoint=sb://some-namespace.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;EntityPath=loremipsum;SharedAccessKey=ejfklwejfwlkejfwfflwkejfklwef";

            Assert.IsTrue(ConnectionStringHelper.IsEntitySpecific(connectionString));
        }

        [Test]
        public void IsEntitySpecific_EntityPathInTheBeginning_ReturnsFalse()
        {
            string connectionString = "EntityPath=loremipsum;Endpoint=sb://some-namespace.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=ejfklwejfwlkejfwfflwkejfklwef";

            Assert.IsTrue(ConnectionStringHelper.IsEntitySpecific(connectionString));
        }
    }
}
