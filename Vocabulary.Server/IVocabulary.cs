using System.Collections.Generic;

namespace Vocabulary.Server
{
    /// <summary>
    /// Абстракция толкового словаря с возможностью редактирования и получения данных
    /// </summary>
    internal interface IVocabulary
    {
        /// <summary>
        /// Возвращает значения слова из словаря.
        /// </summary>
        /// <param name="word">Слово</param>
        string[] Get(string word);

        /// <summary>
        /// Добавляет слово в словарь.
        /// Если значения не заданы, слово не будет добавлено
        /// </summary>
        /// <param name="word">Слово</param>
        /// <param name="means">Значения слова</param>
        /// <returns>Добавленные значения</returns>
        string[] Add(string word, IEnumerable<string> means);
        

        /// <summary>
        /// Удаляет набор значений слова из словаря, но не само слово
        /// </summary>
        /// <param name="word">Слово</param>
        /// <param name="means">Значения, которые надо удалить</param>
        /// <returns>
        /// Возвращает true, значения были удалены из словаря.
        /// False, если такого слова или значений не было.
        /// </returns>
        bool Delete(string word, IEnumerable<string> means);
    }
}