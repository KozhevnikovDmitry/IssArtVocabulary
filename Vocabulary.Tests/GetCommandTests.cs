using System;
using NUnit.Framework;
using Vocabulary.Server.Commands;
using static Vocabulary.Server.Resources.Resources;

namespace Vocabulary.Tests
{
    [TestFixture]
    public class GetCommandTests
    {
        [Test]
        public void Ctor_PassNull_ThrowsArgumentNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => new GetCommand(null));
        }

        [Test]
        public void Execute_PassNull_ThrowsArgumentNullException()
        {
            // Arrange
            var command = new GetCommand(VocabularyStubFactory.Stub());

            // Assert
            Assert.Throws<ArgumentNullException>(() => command.Execute(null));
        }

        [Test]
        public void Name_IsGet()
        {
            // Arrange
            var command = new GetCommand(VocabularyStubFactory.Stub());
            
            // Assert
            Assert.AreEqual(command.Name, "get");
        }

        [Test]
        public void Execute_PassEmptyParameters_ReturnsWordOrMeansAreNotSet()
        {
            // Arrange
            var command = new GetCommand(VocabularyStubFactory.Stub());

            // Act
            var result = command.Execute(new string[0]);

            // Assert
            Assert.AreEqual(result, Default.WordIsNotSet);
        }

        [Test]
        public void Execute_PassWordAndVocabularyReturnEmptyResults_ReturnNoSuchWord()
        {
            // Arrange
            var word = "aaa";
            var command = new GetCommand(VocabularyStubFactory.Get(word, new string[0]));

            // Act
            var result = command.Execute(new [] { word });

            // Assert
            Assert.AreEqual(result, Default.NoSuchWord);
        }

        [Test]
        public void Execute_PassWordAndVocabularyReturnsMeans_ReturnMeansLineByLine()
        {
            // Arrange
            var word = "aaa";
            var means = new [] { "bbb", "ccc"};
            var command = new GetCommand(VocabularyStubFactory.Get(word, means));

            // Act
            var result = command.Execute(new[] { word });

            // Assert
            Assert.AreEqual(result, $"{means[0]}\r\n{means[1]}\r\n");
        }
    }
}
