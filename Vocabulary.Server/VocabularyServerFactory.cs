using Vocabulary.Server.Commands;

namespace Vocabulary.Server
{
    /// <summary>
    /// Фабрика экземпляров <see cref="VocabularyServer"/>
    /// </summary>
    internal sealed class VocabularyServerFactory
    {
        /// <summary>
        /// Билдит и возвращает сервер словаря.
        /// </summary>
        /// <param name="url">URL, который слушает сервер</param>
        public VocabularyServer Get(string url)
        {
            var storage = new Vocabulary();
            var server = new VocabularyServer(
                new VocabularyRequestHandler(
                    new IVocabularyCommand[]
                    {
                        new AddCommand(storage),
                        new GetCommand(storage),
                        new DeleteCommand(storage)
                    }),
                url);

            return server;
        }
    }
}