using Moq;
using Vocabulary.Server.Commands;

namespace Vocabulary.Tests
{
    /// <summary>
    /// Фабрика заглушек интерфейса <see cref="IVocabularyCommand"/>
    /// </summary>
    internal static class VocabularyCommandStubFactory
    {
        /// <summary>
        /// Заглушка команды с именем
        /// </summary>
        /// <param name="name">Имя, которое должна иметь команда</param>
        public static IVocabularyCommand Name(string name)
        {
            return Mock.Of<IVocabularyCommand>(c => c.Name == name);
        }

        /// <summary>
        /// Заглушка команды
        /// </summary>
        /// <param name="name">Имя, которое должна иметь команда</param>
        /// <param name="parameters">Параметры, которые принимает команда</param>
        /// <param name="result">Результаты, которые вернёт команда</param>
        /// <returns></returns>
        public static IVocabularyCommand NameAndExecute(string name, string[] parameters, string result)
        {
            return Mock.Of<IVocabularyCommand>(c => c.Execute(parameters) == result && c.Name == name);
        }
    }
}