using System;
using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;
using Vocabulary.Client;
using Vocabulary.Server;
using Vocabulary.Client.Resources;

namespace Vocabulary.Tests
{
    [TestFixture]
    public class VocabularyServerTests
    {
        /// <summary>
        /// URL сервера
        /// </summary>
        private const string Url = "http://127.0.0.1:9020/";

        [Test]
        public void Ctor_PassNullHandler_ThrowsArgumentNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => new VocabularyServer(null, string.Empty));
        }

        [Test]
        public void Ctor_PassNullUrl_ThrowsArgumentNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => 
                new VocabularyServer(
                    VocabularyRequestHandlerStabFactory.Stub(), null));
        }

        [Test]
        public void Ctor_PassIncorrectUrl_ThrowsArgumentException()
        {
            // Assert
            Assert.Throws<ArgumentException>(() =>
                new VocabularyServer(
                    VocabularyRequestHandlerStabFactory.Stub(), "@#$$%%^&"));
        }

        [Test]
        public void Start_DoItTwice_ThrowsApplicationException()
        {
            // Arrange
            using (var fisrtServer = new VocabularyServer(VocabularyRequestHandlerStabFactory.Stub(), Url))
            {
                var start = fisrtServer.StartAsync();
                using (var secondServer = new VocabularyServer(VocabularyRequestHandlerStabFactory.Stub(), Url))
                {
                    // Assert
                    var ex = Assert.Throws<AggregateException>(() => secondServer.StartAsync().Wait());
                    Assert.IsInstanceOf<ApplicationException>(ex.InnerException);
                    Assert.IsInstanceOf<HttpListenerException>(ex.InnerException.InnerException);
                }
                fisrtServer.Stop();
            }
        }

        [Test]
        public async Task ProcessRequest_HandlerReturnResult_ReceiveThatResult()
        {
            // Arrange
            var command = "aaa";
            var result = "bbb";
            using (var server = new VocabularyServer(
                        VocabularyRequestHandlerStabFactory.Handle(command, result), Url))
            {
                var start = server.StartAsync();
                using (var request = new VocabularyRequest(new Uri(Url), command))
                {
                    // Act
                    var responce = await request.PostAsync();

                    // Assert
                    Assert.AreEqual(responce, result);
                }

                server.Stop();
            }
        }

        [Test]
        public async Task ProcessRequest_HandlerThorwsException_ReceiveInternalServerError()
        {
            // Arrange
            using (var server = new VocabularyServer(VocabularyRequestHandlerStabFactory.ExceptionHandle(), Url))
            {
                var start = server.StartAsync();
                using (var request = new VocabularyRequest(new Uri(Url), "add"))
                {
                    // Act
                    var responce = await request.PostAsync();

                    // Assert
                    Assert.AreEqual(responce, string.Format(Resources.Default.ResponseErrorCode, "InternalServerError"));
                }

                server.Stop();
            }
        }
    }
}
