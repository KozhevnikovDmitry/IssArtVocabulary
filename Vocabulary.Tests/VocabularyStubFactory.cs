using Moq;
using Vocabulary.Server;

namespace Vocabulary.Tests
{
    /// <summary>
    /// Фабрика заглушек интерфейса <see cref="IVocabulary"/>
    /// </summary>
    internal static class VocabularyStubFactory
    {
        /// <summary>
        /// Просто заглушка, которая ничего не делает
        /// </summary>
        public static IVocabulary Stub()
        {
            return Mock.Of<IVocabulary>();
        }

        /// <summary>
        /// Заглушка на получение значений слова
        /// </summary>
        /// <param name="word">Слово</param>
        /// <param name="means">Значения, которые вернёт словарь</param>
        /// <returns></returns>
        public static IVocabulary Get(string word, string[] means)
        {
            return Mock.Of<IVocabulary>(v => v.Get(word) == means);
        }

        /// <summary>
        /// Заглушка на добавление слов\значений
        /// </summary>
        /// <param name="word">Слово</param>
        /// <param name="means">Значения</param>
        /// <param name="addedMeans">Значения, которые словарь вернёт как добавленные</param>
        /// <returns></returns>
        public static IVocabulary Add(string word, string[] means, string[] addedMeans)
        {
            return Mock.Of<IVocabulary>(v => v.Add(word, means) == addedMeans);
        }
        

        /// <summary>
        /// Заглушка на удаление всех значений словаря
        /// </summary>
        /// <param name="word">Слово</param>
        /// <param name="means">Значения</param>
        /// <param name="deleted">Результат удаления, который вернёт словарь</param>
        /// <returns></returns>
        public static IVocabulary Delete(string word, string[] means, bool deleted)
        {
            return Mock.Of<IVocabulary>(v => v.Delete(word, means) == deleted);
        }
    }
}