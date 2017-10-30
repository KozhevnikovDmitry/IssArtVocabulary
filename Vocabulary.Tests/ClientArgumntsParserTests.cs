using System;
using NUnit.Framework;
using Vocabulary.Client;
using Vocabulary.Client.Resources;

namespace Vocabulary.Tests
{
    [TestFixture]
    public class ClientArgumentsParserTests
    {
        [Test]
        public void Parse_PassNullArgs_ReturnsUrlIsNotSet()
        {
            // Arrange
            var parser= new ClientArgumentsParser();
            
            // Act
            var args = parser.Parse(null);

            // Assert
            Assert.False(args.Correct);
            Assert.AreEqual(args.Error, Resources.Default.UrlIsNotSet);
        }

        [Test]
        public void Parse_PassEmptyArgs_ReturnsUrlIsNotSet()
        {
            // Arrange
            var parser = new ClientArgumentsParser();

            // Act
            var args = parser.Parse(new string[0]);

            // Assert
            Assert.False(args.Correct);
            Assert.AreEqual(args.Error, Resources.Default.UrlIsNotSet);
        }



        [Test]
        public void Parse_PassOnlyHost_ReturnsPortIsNotSet()
        {
            // Arrange
            var parser = new ClientArgumentsParser();

            // Act
            var args = parser.Parse(new [] {"localhost"});

            // Assert
            Assert.False(args.Correct);
            Assert.AreEqual(args.Error, Resources.Default.PortIsNotSet);
        }

        [Test]
        public void Parse_PassOnlyHostAndPort_ReturnsCommandIsNotSet()
        {
            // Arrange
            var parser = new ClientArgumentsParser();

            // Act
            var args = parser.Parse(new[] { "localhost", "80" });

            // Assert
            Assert.False(args.Correct);
            Assert.AreEqual(args.Error, Resources.Default.CommandIsNotSet);
        }
        
        [TestCase("#$%^&", "80")]
        [TestCase("localhost", "80000")]
        [TestCase("localhost", "-1")]
        [TestCase("localhost", "port")]
        public void Parse_PassInvalidHostOrPort_ReturnsUrlIsInvalid(string host, string port)
        {
            // Arrange
            var parser = new ClientArgumentsParser();

            // Act
            var args = parser.Parse(new[] { host, port, "add" });

            // Assert
            Assert.False(args.Correct);
            Assert.AreEqual(args.Error, Resources.Default.UrlIsInvalid);
        }

        [Test]
        public void Parse_PassCorrectHostAndPortAndCommand_ReturnsCorrectParserArguments()
        {
            // Arrange
            var parser = new ClientArgumentsParser();

            // Act
            var args = parser.Parse(new[] { "localhost", "9000", "add", "aaa", "bbb"});

            // Assert
            Assert.True(args.Correct);
            Assert.IsEmpty(args.Error);
            Assert.AreEqual(args.ServerUrl, new Uri("http://localhost:9000/"));
            Assert.AreEqual(args.Command, "add aaa bbb");
        }
    }
}
