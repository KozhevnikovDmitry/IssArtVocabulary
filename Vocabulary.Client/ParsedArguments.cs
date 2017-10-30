using System;

namespace Vocabulary.Client
{
    /// <summary>
    /// Результаты парсинга аргументов клиентского приложения словаря
    /// </summary>
    internal sealed class ParsedArguments
    {
        /// <summary>
        /// True, если аргументы корректные
        /// </summary>
        public bool Correct { get; }

        /// <summary>
        /// Текст ошибки
        /// </summary>
        public string Error { get; }

        /// <summary>
        /// Адрес сервера словаря
        /// </summary>
        public Uri ServerUrl { get; }

        /// <summary>
        /// Команда для словаря
        /// </summary>
        public string Command { get; }

        /// <summary>
        /// Корректные аргументы
        /// </summary>
        /// <param name="serverUrl">Адрес сервера</param>
        /// <param name="command">Команда для словаря</param>
        /// <exception cref="ArgumentNullException">Значение аргумента Null</exception>
        public ParsedArguments(Uri serverUrl, string command)
        {
            if (serverUrl == null) throw new ArgumentNullException(nameof(serverUrl));
            if (command == null) throw new ArgumentNullException(nameof(command));
            ServerUrl = serverUrl;
            Command = command;
            Correct = true;
            Error = string.Empty;
        }

        /// <summary>
        /// Некорректные аргументы
        /// </summary>
        /// <param name="error">Текст ошибки</param>
        /// <exception cref="ArgumentNullException">Значение ошикки Null</exception>
        public ParsedArguments(string error)
        {
            if (error == null) throw new ArgumentNullException(nameof(error));
            Correct = false;
            Error = error;
            Command = string.Empty;
            ServerUrl = null;
        }
    }
}