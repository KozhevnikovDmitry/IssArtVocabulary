using System;
using NUnit.Framework;
using Vocabulary.Server;
using Vocabulary.Server.Commands;
using static Vocabulary.Server.Resources.Resources;

namespace Vocabulary.Tests
{
    [TestFixture]
    public class VocabularyRequestHandlerTests
    {
        [Test]
        public void Ctor_PassNull_ThrowsArgumentNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => new VocabularyRequestHandler(null));
        }

        [TestCase("get", "GET")]
        [TestCase("gEt", "GeT")]
        [TestCase(" get ", "gET   ")]
        public void Ctor_PassCommandsWithTheSameName_ThrowsApplicationException(string firstName, string secondName)
        {
            // Arrange
            var firstCommand = VocabularyCommandStubFactory.Name(firstName);
            var secondCommand = VocabularyCommandStubFactory.Name(firstName);
            
            // Assert
            var ex = Assert.Throws<ApplicationException>(
                () => new VocabularyRequestHandler(new[] {firstCommand, secondCommand}));

            Assert.AreEqual(ex.Message, Default.CommandNameDuplicated);
        }

        [TestCase("")]
        [TestCase("    ")]
        public void Handle_PassEmptyRequest(string request)
        {
            // Arrange
            var handler = new VocabularyRequestHandler(new IVocabularyCommand[0]);
            
            // Act
            var result = handler.Handle(request);

            // Assert
            Assert.AreEqual(result, Default.CommandIsNotSet);
        }


        [Test]
        public void Handle_PassNotContaiedCommandName_ReturnsNoSuchCommand()
        {
            // Arrange
            var handler = new VocabularyRequestHandler(
                new []
                {
                    VocabularyCommandStubFactory.Name("get")
                });

            // Act
            var result = handler.Handle("add");

            // Assert
            Assert.AreEqual(result, Default.NoSuchCommand);
        }


        [TestCase("get", "GET aaa")]
        [TestCase("gEt", "GeT   aaa  ")]
        [TestCase(" get ", "gET     aaa ")]
        public void Handle_PassCommandAndCommandReturnsResult_ReturnsCommandResult(string commandName, string request)
        {
            // Arrange
            var commandResults = "bbb ccc";
            var handler = new VocabularyRequestHandler(
                new[]
                {
                    VocabularyCommandStubFactory.NameAndExecute(commandName, new []{"aaa"}, commandResults)
                });

            // Act
            var result = handler.Handle(request);

            // Assert
            Assert.AreEqual(result, commandResults);
        }
    }

    
}
