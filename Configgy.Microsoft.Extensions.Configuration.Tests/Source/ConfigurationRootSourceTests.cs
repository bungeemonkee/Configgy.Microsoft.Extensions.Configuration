using System;
using System.Collections.Generic;
using System.Text;
using Configgy.Microsoft.Extensions.Configuration.Source;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Configgy.Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration;

namespace Configgy.Microsoft.Extensions.Configuration.Tests.Source
{
    [TestClass]
    public class ConfigurationRootSourceTests
    {
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
        public void Get_Returns_False_When_Exception_Is_Thrown()
        {
            const string valueName = "value";

            var rootMock = new Mock<IConfigurationRoot>();
            rootMock.Setup(x => x[It.IsAny<string>()])
                .Throws<InvalidOperationException>();

            var source = new ConfigurationRootSource(rootMock.Object);

            var result = source.Get(valueName, null, out string value);

            Assert.IsFalse(result);
            rootMock.VerifyAll();
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
    }
}
