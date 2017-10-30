using System;
using System.Linq;
using NUnit.Framework;

namespace Vocabulary.Tests
{
    [TestFixture]
    public class VocabularyTests
    {
        [Test]
        public void Get_PassNull_ThrowsArgumentNullException()
        {
            // Arrange
            var vocabulary = new Server.Vocabulary();

            // Assert
            Assert.Throws<ArgumentNullException>(() => vocabulary.Get(null));
        }


        [Test]
        public void Get_FromEmpty_ReturnsEmptyResuts()
        {
            // Arrange
            var vocabulary = new Server.Vocabulary();

            // Act
            var means = vocabulary.Get("aaa");

            // Assert
            Assert.IsEmpty(means);
        }


        [Test]
        public void Get_PassNotContainedWord_ReturnsEmptyResuts()
        {
            // Arrange
            var vocabulary = new Server.Vocabulary();
            vocabulary.Add("aaa", new[] {"bbb", "ccc"});

            // Act
            var means = vocabulary.Get("ddd");

            // Assert
            Assert.IsEmpty(means);
        }


        [TestCase("aaa", "AAA")]
        [TestCase("AAA", "Aaa")]
        [TestCase("  aAa  ", "     aaa ")]
        public void Get_PassContainedWord_ReturnsAddedMeans(string addWord, string getWord)
        {
            // Arrange
            var vocabulary = new Server.Vocabulary();
            var means = new[] {" bBb ", "  CcC"};
            vocabulary.Add(addWord, means);

            // Act
            var results = vocabulary.Get(getWord);

            // Assert
            Assert.That(results.SequenceEqual(means.Select(m => m.Trim().ToLower())));
        }


        [TestCase(null, new string[0])]
        [TestCase("", null)]
        public void Add_PassNull_ThrowsArgumentNullException(string word, string[] means)
        {
            // Arrange
            var vocabulary = new Server.Vocabulary();

            // Assert
            Assert.Throws<ArgumentNullException>(() => vocabulary.Add(word, means));
        }

        [Test]
        public void Add_PassEmptyMeans_GetReturnsEmpty()
        {
            // Arrange
            var word = "aaa";
            var vocabulary = new Server.Vocabulary();

            // Act
            vocabulary.Add(word, new string[0]);
            var get = vocabulary.Get(word);

            // Assert
            Assert.IsEmpty(get);
        }


        [TestCase("bbb")]
        [TestCase("BBB")]
        [TestCase("  bBb  ")]
        public void Add_PassWordAndMeans_ReturnWordAndMeans(string mean)
        {
            // Arrange
            var vocabulary = new Server.Vocabulary();
            var word = "aaa";
            var means = new[] { mean };

            // Act
            var added = vocabulary.Add(word, means);

            // Assert
            Assert.That(added.SequenceEqual(new []{mean.Trim().ToLower()}));
        }


        [Test]
        public void Add_PassNullMean_ReturnJustAWord()
        {
            // Arrange
            var vocabulary = new Server.Vocabulary();
            var word = "aaa";
            var means = new string[] { null, null };

            // Act
            var added = vocabulary.Add(word, means);

            // Assert
            Assert.IsEmpty(added);
        }


        [Test]
        public void Add_PassEqualsMeans_ReturnSingleEntryOfMean()
        {
            // Arrange
            var vocabulary = new Server.Vocabulary();
            var word = "aaa";
            var mean = "bbb";
            var means = new[] { mean.ToLower(), mean.ToUpper() };

            // Act
            var added = vocabulary.Add(word, means);

            // Assert
            Assert.That(added.SequenceEqual(new[] { mean }));
        }


        [TestCase("bbb", "BBB")]
        [TestCase("BBB", "bBb")]
        [TestCase("  BbB ", "   bbb")]
        public void Add_PassTheSameMeanTwice_ReturnsJusEmptyResult(string fisrtMean, string secondMean)
        {
            // Arrange
            var vocabulary = new Server.Vocabulary();
            var word = "aaa";

            // Act
            vocabulary.Add(word, new []{fisrtMean});
            var added = vocabulary.Add(word, new []{secondMean});

            // Assert
            Assert.IsEmpty(added);
        }


        [TestCase("aaa", "AAA")]
        [TestCase("AAA", "Aaa")]
        [TestCase("  aAa  ", "     aaa ")]
        public void Add_PassNewMeansForContainedWord_ReturnsJustNewMeans(string addWord, string updateWord)
        {
            // Arrange
            var vocabulary = new Server.Vocabulary();
            var fisrtMean = "bbb";
            var secondMean = "ccc";

            // Act
            vocabulary.Add(addWord, new[] { fisrtMean });
            var updated = vocabulary.Add(updateWord, new[] { secondMean });

            // Assert
            Assert.AreEqual(updated.Single(), secondMean);
        }


        [Test]
        public void Add_PassEqualMeansForContainedWord_ReturnsSingleEntryOfMean()
        {
            // Arrange
            var vocabulary = new Server.Vocabulary();
            var word = "aaa";
            var fisrtMean = "bbb";
            var secondMean = "ccc";

            // Act
            vocabulary.Add(word, new[] { fisrtMean });
            var updated = vocabulary.Add(word, new[] { secondMean.ToLower(), secondMean.ToUpper() });

            // Assert
            Assert.AreEqual(updated.Single(), secondMean);
        }
        

        [TestCase(null, new string[0])]
        [TestCase("", null)]
        public void DeleteMeans_PassNull_ThrowsArgumentNullException(string word, string[] means)
        {
            // Arrange
            var vocabulary = new Server.Vocabulary();

            // Assert
            Assert.Throws<ArgumentNullException>(() => vocabulary.Delete(word, means));
        }


        [Test]
        public void DeleteMeans_PassEmptyMeans_ReturnsFalse()
        {
            // Arrange
            var vocabulary = new Server.Vocabulary();

            // Act
            var deleted = vocabulary.Delete("aaa", new string[0]);

            // Assert
            Assert.False(deleted);
        }


        [Test]
        public void DeleteMeans_PassNullMeans_ReturnsFalse()
        {
            // Arrange
            var vocabulary = new Server.Vocabulary();

            // Act
            var deleted = vocabulary.Delete("aaa", new string[] { null, null, null });

            // Assert
            Assert.False(deleted);
        }


        [Test]
        public void DeleteMeans_FromEmpty_ReturnsFalse()
        {
            // Arrange
            var vocabulary = new Server.Vocabulary();

            // Act
            var deleted = vocabulary.Delete("aaa", new[] { "bbb" });

            // Assert
            Assert.False(deleted);
        }


        [Test]
        public void DeleteMeans_NotContainedWord_ReturnsFalse()
        {
            // Arrange
            var vocabulary = new Server.Vocabulary();
            vocabulary.Add("aaa", new[] {"bbb"});

            // Act
            var deleted = vocabulary.Delete("ccc", new[] {"ddd"});

            // Assert
            Assert.False(deleted);
        }

        [TestCase("aaa", "AAA", "bbb", "BBB")]
        [TestCase("AAA", "Aaa", "BBB", "bBb")]
        [TestCase("  aAa  ", "     aaa ", "  BbB ", "   bbb")]
        public void DeleteMeans_PassContainedWordAndMean_ReturnsTrue(
            string addWord, 
            string deleteWord, 
            string addMean, 
            string deleteMean)
        {
            // Arrange
            var vocabulary = new Server.Vocabulary();
            vocabulary.Add(addWord, new[] { addMean });

            // Act
            var deleted = vocabulary.Delete(deleteWord, new[] { deleteMean });

            // Assert
           Assert.True(deleted);
        }


        [Test]
        public void DeleteMeans_PassContainedEqualMeans_ReturnsTrue()
        {
            // Arrange
            var vocabulary = new Server.Vocabulary();
            var word = "aaa";
            var mean = "bbb";
            vocabulary.Add(word, new[] { mean });

            // Act
            var deleted = vocabulary.Delete(word, new[] { mean.ToLower(), mean.ToUpper() });

            // Assert
            Assert.True(deleted);
        }


        [Test]
        public void DeleteMeans_PassContainedMeansTwice_ReturnsFalse()
        {
            // Arrange
            var vocabulary = new Server.Vocabulary();
            var word = "aaa";
            var mean = "bbb";
            vocabulary.Add(word, new[] { mean });

            // Act
            vocabulary.Delete(word, new[] { mean });
            var deleted = vocabulary.Delete(word, new[] { mean });

            // Assert
            Assert.False(deleted);
        }


        [Test]
        public void DeleteMeans_PassOneOfMeans_GetReturnsTheRestMean()
        {
            // Arrange
            var vocabulary = new Server.Vocabulary();
            var word = "aaa";
            var firstMean = "bbb";
            var secondMean = "ccc";
            vocabulary.Add(word, new[] { firstMean, secondMean });

            // Act
            vocabulary.Delete(word, new[] { firstMean });
            var get = vocabulary.Get(word);

            // Assert
            Assert.AreEqual(get.Single(), secondMean);
        }
    }
}