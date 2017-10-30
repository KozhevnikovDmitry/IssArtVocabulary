using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Vocabulary.Client;
using Vocabulary.Server;

namespace Vocabulary.Tests
{
    /// <summary>
    /// Интеграционные тесты клиент+сервер
    /// </summary>
    [TestFixture]
    public class IntegrationTests
    {
        /// <summary>
        /// URL сервера
        /// </summary>
        private const string Url = "http://127.0.0.1:9020/";

        /// <summary>
        /// Выполняет команды к серверу, возвращает последний ответ сервера
        /// </summary>
        /// <param name="commands">Команды</param>
        /// <returns></returns>
        private async Task<string> ExecuteCommandsAsync(string[] commands)
        {
            // Arrange
            string response = string.Empty;
            using (var server = new VocabularyServerFactory().Get(Url))
            {
                await Task.Factory.StartNew(async () => await server.StartAsync()).ConfigureAwait(false);
                foreach (var command in commands)
                {
                    using (var request = new VocabularyRequest(new Uri(Url), command))
                    {
                        // Act
                        response = await request.PostAsync();
                    }
                }
                server.Stop();
            }

            return response;
        }

        [TestCase(new[] { "add aaa bbb ccc", "get aaa"}, "bbb\r\nccc\r\n")]
        [TestCase(new[] { "add aaa bbb", "add aaa ccc", "get aaa" }, "bbb\r\nccc\r\n")]
        [TestCase(new[] { "add aaa bbb ccc", "add aaa bbb ccc", "get aaa" }, "bbb\r\nccc\r\n")]
        [TestCase(new[] { "add aaa bbb ccc bbb ccc", "get aaa" }, "bbb\r\nccc\r\n")]
        [TestCase(new[] { "add aaa bbb ccc", "delete aaa bbb", "get aaa" }, "ccc\r\n")]
        [TestCase(new[] { "add aaa bbb ccc", "delete aaa ddd", "get aaa" }, "bbb\r\nccc\r\n")]
        public async Task Request_AddDeleteAndThenGetCommands_AssureResults(string[] commands, string result)
        {
            // Act
            var response = await ExecuteCommandsAsync(commands);

            // Assert
            Assert.AreEqual(response, result);
        }
    }
}
