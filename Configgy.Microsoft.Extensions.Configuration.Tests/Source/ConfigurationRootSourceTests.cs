using System.IO;
using System.Reflection;
using Configgy.Microsoft.Extensions.Configuration.Source;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.Extensions.Configuration;

namespace Configgy.Microsoft.Extensions.Configuration.Tests.Source
{
    [TestClass]
    public class ConfigurationRootSourceTests
    {
        [TestInitialize]
        public void Initialize()
        {
            // Current directory needs to be somewhere where the appsettings.json exists
            var path = typeof(TestConfig).GetTypeInfo().Assembly.Location;
            path = Path.GetDirectoryName(path);
            Directory.SetCurrentDirectory(path);
        }

        [TestMethod]
        public void Constructor_Works()
        {
            var source = new ConfigurationRootSource();
        }

        [TestMethod]
        public void Constructor_Takes_IConfigurationRoot()
        {
            var rootMock = new Mock<IConfigurationRoot>();

            var source = new ConfigurationRootSource(rootMock.Object);

            Assert.AreSame(rootMock.Object, source.ConfigurationRoot);
        }

        [TestMethod]
        public void Get_Returns_False_When_Value_Is_Null()
        {
            const string valueName = "value";

            var rootMock = new Mock<IConfigurationRoot>();

            var source = new ConfigurationRootSource(rootMock.Object);

            var result = source.Get(valueName, null, out string value);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Get_Returns_True_When_Value_Exists()
        {
            const string valueName = "value";
            const string expected = "something";

            var rootMock = new Mock<IConfigurationRoot>();
            rootMock.Setup(x => x[valueName])
                .Returns(expected);

            var source = new ConfigurationRootSource(rootMock.Object);

            var result = source.Get(valueName, null, out string value);

            Assert.IsTrue(result);
            Assert.AreEqual(expected, value);
            rootMock.VerifyAll();
        }

        [TestMethod]
        public void Get_Includes_Prefix_From_ConfigurationRootPrefixAttribute()
        {
            const string prefix = "prefix";
            const string valueName = "value";
            const string expected = "something";

            var sectionMock = new Mock<IConfigurationSection>();
            sectionMock.Setup(x => x[valueName])
                .Returns(expected);

            var rootMock = new Mock<IConfigurationRoot>();
            rootMock.Setup(x => x.GetSection(prefix))
                .Returns(sectionMock.Object);

            var propertyMock = new Mock<PropertyInfo>();
            var customAttributesMock = propertyMock.As<ICustomAttributeProvider>();
            customAttributesMock.Setup(x => x.GetCustomAttributes(true))
                .Returns(new object[] { new ConfigurationRootPrefixAttribute(prefix) });

            var source = new ConfigurationRootSource(rootMock.Object);

            var result = source.Get(valueName, propertyMock.Object, out string value);

            Assert.IsTrue(result);
            Assert.AreEqual(expected, value);
            rootMock.VerifyAll();
            sectionMock.VerifyAll();
            propertyMock.VerifyAll();
        }

        [TestMethod]
        public void Integration_Test_With_Prefix()
        {
            const string expected = "Value02";
            
            var config = new TestConfig();

            var result = config.Setting02;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Integration_Test_Without_Prefix()
        {
            const string expected = "Value01";

            var config = new TestConfig();

            var result = config.Setting01;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Integration_Test_With_Dotted_Prefix()
        {
            const string expected = "Value03";

            var config = new TestConfig();

            var result = config.Setting03;

            Assert.AreEqual(expected, result);
        }
    }
}
