using System;
using System.Collections.Generic;
using System.Linq;
using Vocabulary.Server.Commands;
using static Vocabulary.Server.Resources.Resources;

namespace Vocabulary.Server
{
    /// <summary>
    /// Обработчик запросов к словарю
    /// </summary>
    internal sealed class VocabularyRequestHandler : IVocabularyRequestHandler
    {
        /// <summary>
        /// Допустимые команды словаря
        /// </summary>
        private readonly Dictionary<string, IVocabularyCommand> _commands;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="commands">Допустимые команды словаря</param>
        /// <exception cref="ArgumentNullException">Значение команд Null</exception>
        /// <exception cref="ApplicationException">Имена команд дублируются</exception>
        public VocabularyRequestHandler(IEnumerable<IVocabularyCommand> commands)
        {
            if (commands == null) throw new ArgumentNullException(nameof(commands));
            try
            {
                _commands = commands.ToDictionary(c => c.Name.Trim().ToLower(), c => c);
            }
            catch (ArgumentException ex)
            {
                throw new ApplicationException(Default.CommandNameDuplicated, ex);
            }
        }


        /// <summary>
        /// Обрабатывает запрос и возвращает сообщение с даннными из словаря или другое сообщение о результатах. 
        /// </summary>
        /// <param name="request">Текст запроса</param>
        /// <returns>
        /// Текст с результатами запроса.
        /// </returns>
        public string Handle(string request)
        {
            if (string.IsNullOrWhiteSpace(request))
            {
                return Default.CommandIsNotSet;
            }

            var entries = request.Trim().Split(' ').Where(e => !string.IsNullOrWhiteSpace(e)).ToArray();
            var commandName = entries[0].Trim().ToLower();
            if (_commands.ContainsKey(commandName))
            {
                return _commands[commandName].Execute(entries.Skip(1));
            }

            return Default.NoSuchCommand;
        }
    }
}