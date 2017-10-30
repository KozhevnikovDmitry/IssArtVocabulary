using System;
using System.Collections.Generic;
using System.Linq;
using static Vocabulary.Server.Resources.Resources;

namespace Vocabulary.Server.Commands
{
    /// <summary>
    /// Команда "Удалить слово и\или значения из словаря"
    /// </summary>
    internal sealed class DeleteCommand : IVocabularyCommand
    {
        /// <summary>
        /// Словарь
        /// </summary>
        private readonly IVocabulary _vocabulary;

        /// <summary>
        /// Имя команды
        /// </summary>
        public string Name => "delete";

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="vocabulary">Словарь</param>
        /// <exception cref="ArgumentNullException">Значение аргумента Null</exception>
        public DeleteCommand(IVocabulary vocabulary)
        {
            if (vocabulary == null) throw new ArgumentNullException(nameof(vocabulary));
            _vocabulary = vocabulary;
        }

        /// <summary>
        /// Удаляет значения слова из словаря
        /// </summary>
        /// <param name="parameters">Первый элемент - слово, последующие - значения, которые необходимо удалить</param>
        /// <exception cref="ArgumentNullException">Значение параметров Null</exception>
        /// <returns>True, если значения слова были удалены</returns>
        public string Execute(IEnumerable<string> parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            var paramsArray = parameters as string[] ?? parameters.ToArray();
            if (!paramsArray.Any())
            {
                return Default.WordIsNotSet;
            }
            
            if (paramsArray.Length == 1)
            {
                return Default.MeansAreNotSet;
            }

            bool deleted = _vocabulary.Delete(paramsArray[0], paramsArray.Skip(1));
            return deleted ? Default.MeansAreDeleted : Default.NoSuchWordOrMean;
        }
    }
}