namespace Vocabulary.Server
{
    /// <summary>
    /// Абстракция обработчика запросов к словарю
    /// </summary>
    internal interface IVocabularyRequestHandler
    {
        /// <summary>
        /// Обрабатывает запрос и возвращает сообщение с даннными из словаря или другое сообщение о результатах. 
        /// </summary>
        /// <param name="request">Текст запроса</param>
        /// <returns>
        /// Текст с результатами запроса.
        /// </returns>
        string Handle(string request);
    }
}