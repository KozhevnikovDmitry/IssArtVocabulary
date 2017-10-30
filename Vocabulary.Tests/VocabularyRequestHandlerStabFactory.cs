using System;
using Moq;
using Vocabulary.Server;

namespace Vocabulary.Tests
{
    /// <summary>
    /// Фабрика заглушек для интерфейса <see cref="IVocabularyRequestHandler"/>
    /// </summary>
    internal static class VocabularyRequestHandlerStabFactory
    {
        /// <summary>
        /// Просто заглушка
        /// </summary>
        /// <returns></returns>
        public static IVocabularyRequestHandler Stub()
        {
            return Mock.Of<IVocabularyRequestHandler>();
        }

        /// <summary>
        /// Заглушка с обработкой запроса
        /// </summary>
        /// <param name="request">Запрос</param>
        /// <param name="result">Результат, который вернут обработчик</param>
        public static IVocabularyRequestHandler Handle(string request, string result)
        {
            return Mock.Of<IVocabularyRequestHandler>(v => v.Handle(request) == result);
        }

        /// <summary>
        /// Заглушка, которая бросает исключение при обработке запроса
        /// </summary>
        public static IVocabularyRequestHandler ExceptionHandle()
        {
            var mock = new Mock<IVocabularyRequestHandler>();
            mock.Setup(h => h.Handle(It.IsAny<string>())).Throws<Exception>();
            return mock.Object;
        }
    }
}