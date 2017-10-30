using System.Collections.Generic;

namespace Vocabulary.Server.Commands
{
    /// <summary>
    /// Абстракция команды, выполняющей атомарную операцию над словарём.
    /// Например добавиь слово, удалить значения, получить значения.
    /// </summary>
    internal interface IVocabularyCommand
    {
        /// <summary>
        /// Уникальное имя команды.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Выполняет тело команды.
        /// </summary>
        /// <param name="parameters">Параметры команды</param>
        /// <returns>
        /// Возвращает данные из словаря или текстовое сообщение о результатах работы команды.
        /// </returns>
        string Execute(IEnumerable<string> parameters);
    }
}