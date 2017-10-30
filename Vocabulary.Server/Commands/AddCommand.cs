using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Vocabulary.Server.Resources.Resources;

namespace Vocabulary.Server.Commands
{
    /// <summary>
    /// Команда "Добавить слово и\или значения в словарь"
    /// </summary>
    internal sealed class AddCommand : IVocabularyCommand
    {
        /// <summary>
        /// Словарь
        /// </summary>
        private readonly IVocabulary _vocabulary;

        /// <summary>
        /// Имя команды
        /// </summary>
        public string Name => "add";

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="vocabulary">Словарь</param>
        /// <exception cref="ArgumentNullException">Значение словаря Null</exception>
        public AddCommand(IVocabulary vocabulary)
        {
            if (vocabulary == null) throw new ArgumentNullException(nameof(vocabulary));
            _vocabulary = vocabulary;
        }

        /// <summary>
        /// Добавляет слово и\или значения в словарь
        /// </summary>
        /// <param name="parameters">Первый элемент - слово, последующие - значения</param>
        /// <exception cref="ArgumentNullException">Значение параметров Null</exception>
        /// <returns>
        /// Возвращает сообщение о добавленном слове и\или добавленных значениях
        /// Если значения не заданы, возвращает соотвествующее сообщение
        /// Если новых значений добавлено не было, возвращает соответствующее сообщение
        /// </returns>
        public string Execute(IEnumerable<string> parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            var paramsArray = parameters as string[] ?? parameters.ToArray();
            if (paramsArray.Length < 1)
            {
                return Default.WordIsNotSet;
            }

            if (paramsArray.Length < 2)
            {
                return Default.MeansAreNotSet;
            }

            var added = _vocabulary.Add(paramsArray.FirstOrDefault(), paramsArray.Skip(1));
            if (!added.Any())
            {
                return Default.MeansAreAlreadyAdded;
            }

            var builder = new StringBuilder();
            var values = added.Aggregate(builder, (sb, res) => sb.Append(res).Append(", "))
                .Remove(builder.Length - 2, 2);

            return string.Format(Default.MeansAdded, values);
        }
    }
}