using System;
using System.Net.Http;
using NUnit.Framework;
using Vocabulary.Client;

namespace Vocabulary.Tests
{
    [TestFixture]
    public class VocabularyRequestTests
    {
        [Test]
        public void Ctor_PassNullUrl_ThrowsArgumentNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => new VocabularyRequest(null, "add"));
        }

        [TestCase(null)]
        [TestCase("")]
        public void Ctor_PassNullCommand_ThrowsArgumentNullException(string command)
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => new VocabularyRequest(null, command));
        }

        [Test]
        public void Post_PassNotExistingUrls_ThrowsHandledException()
        {
            // Arrange
            var url = new Uri("http://0.0.0.0:80/");

            // Act
            using (var request = new VocabularyRequest(url, "add"))
            {
                // Assert
                var ex = Assert.Throws<AggregateException>(() => request.PostAsync().Wait());
                Assert.IsInstanceOf<ApplicationException>(ex.InnerException);
                Assert.IsInstanceOf<HttpRequestException>(ex.InnerException.InnerException);
            }
        }
    }
}
