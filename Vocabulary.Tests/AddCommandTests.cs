using System;
using System.Linq;
using NUnit.Framework;
using Vocabulary.Server.Commands;
using static Vocabulary.Server.Resources.Resources;

namespace Vocabulary.Tests
{
    [TestFixture]
    public class AddCommandTests
    {
        [Test]
        public void Ctor_PassNull_ThrowsArgumentNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => new AddCommand(null));
        }

        [Test]
        public void Execute_PassNull_ThrowsArgumentNullException()
        {
            // Arrange
            var command = new AddCommand(VocabularyStubFactory.Stub());

            // Assert
            Assert.Throws<ArgumentNullException>(() => command.Execute(null));
        }

        [Test]
        public void Name_IsAdd()
        {
            // Arrange
            var command = new AddCommand(VocabularyStubFactory.Stub());

            // Assert
            Assert.AreEqual(command.Name, "add");
        }

        [Test]
        public void Execute_PassEmptyParameters_ReturnWordIsNotSet()
        {
            // Arrange
            var command = new AddCommand(VocabularyStubFactory.Stub());

            // Act
            var result = command.Execute(new string[0]);

            // Assert
            Assert.AreEqual(result, Default.WordIsNotSet);
        }


        [Test]
        public void Execute_PassOnlyWordWithoutMeans_ReturnsMeansAreNotSet()
        {
            // Arrange
            var word = "aaa";
            var command = new AddCommand(VocabularyStubFactory.Stub());

            // Act
            var result = command.Execute(new[] { word });

            // Assert
            Assert.AreEqual(result, Default.MeansAreNotSet);
        }


        [Test]
        public void Execute_PassMeansAndVocabularyReturnsEmptyResults_ReturnsMeansAreAlreadyAdded()
        {
            // Arrange
            var word = "aaa";
            var means = new [] {"bbb", "ccc"};
            var command = new AddCommand(VocabularyStubFactory.Add(word, means, new string[0]));

            // Act
            var result = command.Execute(new []{word}.Concat(means));

            // Assert
            Assert.AreEqual(result, Default.MeansAreAlreadyAdded);
        }

        [Test]
        public void Execute_PassMeansAndVocabularyReturnAddedMeans_ReturnMeansAddedFormattedMessage()
        {
            // Arrange
            var word = "aaa";
            var means = new[] { "bbb", "ccc" };
            var addedMeans = new[] { "ddd", "eee" };
            var command = new AddCommand(VocabularyStubFactory.Add(word, means, addedMeans));

            // Act
            var result = command.Execute(new[] { word }.Concat(means));

            // Assert
            Assert.AreEqual(result, string.Format(Default.MeansAdded, $"{addedMeans[0]}, {addedMeans[1]}"));
        }
    }
}