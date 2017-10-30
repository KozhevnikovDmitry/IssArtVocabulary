using System;
using System.Linq;
using NUnit.Framework;
using Vocabulary.Server.Commands;
using static Vocabulary.Server.Resources.Resources;

namespace Vocabulary.Tests
{
    [TestFixture]
    public class DeleteCommandTests
    {
        [Test]
        public void Ctor_PassNull_ThrowsArgumentNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => new DeleteCommand(null));
        }

        [Test]
        public void Execute_PassNull_ThrowsArgumentNullException()
        {
            // Arrange
            var command = new DeleteCommand(VocabularyStubFactory.Stub());

            // Assert
            Assert.Throws<ArgumentNullException>(() => command.Execute(null));
        }

        [Test]
        public void Name_IsDelete()
        {
            // Arrange
            var command = new DeleteCommand(VocabularyStubFactory.Stub());

            // Assert
            Assert.AreEqual(command.Name, "delete");
        }


        [Test]
        public void Execute_PassEmptyParameters_ReturnWordIsNotSet()
        {
            // Arrange
            var command = new DeleteCommand(VocabularyStubFactory.Stub());

            // Act
            var result = command.Execute(new string[0]);

            // Assert
            Assert.AreEqual(result, Default.WordIsNotSet);
        }

        [Test]
        public void Execute_PassOnlyWordAndVocabularyReturnsTrue_ReturnsMeansAreNotSet()
        {
            // Arrange
            var word = "aaa";
            var command = new DeleteCommand(VocabularyStubFactory.Stub());

            // Act
            var result = command.Execute(new [] {word});

            // Assert
            Assert.AreEqual(result, Default.MeansAreNotSet);
        }


        [Test]
        public void Execute_PassWordWithMeansAndVocabularyReturnsTrue_ReturnMeansAreDeleted()
        {
            // Arrange
            var word = "aaa";
            var means = new[] {"bbb"};         
            var command = new DeleteCommand(VocabularyStubFactory.Delete(word, means, true));

            // Act
            var result = command.Execute(new[] { word }.Concat(means));

            // Assert
            Assert.AreEqual(result, Default.MeansAreDeleted);
        }

        [Test]
        public void Execute_PassWordWithMeansAndVocabularyReturnsFalse_ReturnNoSuchWordOrMean()
        {
            // Arrange
            var word = "aaa";
            var means = new[] { "bbb" };
            var command = new DeleteCommand(VocabularyStubFactory.Delete(word, means, false));

            // Act
            var result = command.Execute(new[] { word }.Concat(means));

            // Assert
            Assert.AreEqual(result, Default.NoSuchWordOrMean);
        }
    }
}