using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Vocabulary.Server.Resources.Resources;

namespace Vocabulary.Server.Commands
{
    /// <summary>
    /// Команда "Получить значения слова"
    /// Принимает слово, возвращает его значения из словаря
    /// </summary>
    internal sealed class GetCommand : IVocabularyCommand
    {
        /// <summary>
        /// Словарь
        /// </summary>
        private readonly IVocabulary _vocabulary;

        /// <summary>
        /// Имя команды
        /// </summary>
        public string Name => "get";

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="vocabulary">Словарь</param>
        /// <exception cref="ArgumentNullException">Значение аргумента Null</exception>
        public GetCommand(IVocabulary vocabulary)
        {
            if (vocabulary == null) throw new ArgumentNullException(nameof(vocabulary));
            _vocabulary = vocabulary;
        }

        /// <summary>
        /// Возвращает значения слова из словаря
        /// </summary>
        /// <param name="parameters">Слово. Принимается в расчёт только первый элемент перечисления</param>
        /// <exception cref="ArgumentNullException">Значение параметров Null</exception>
        /// <returns>
        /// Если слово отсутствует в словаре или слово не задано, возвращает соответствуюшее сообщение
        /// </returns>
        public string Execute(IEnumerable<string> parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            var paramArray = parameters as string[] ?? parameters.ToArray();
            if (paramArray.Any())
            {
                var get = _vocabulary.Get(paramArray[0]);
                if (get.Any())
                {
                    return get.Aggregate(new StringBuilder(), (sb, res) => sb.AppendLine(res))
                        .ToString();
                }
                else
                {
                    return Default.NoSuchWord;
                }
            }

            return Default.WordIsNotSet;
        }
    }
}