using System;
using System.Linq;
using System.Text;
using static Vocabulary.Client.Resources.Resources;

namespace Vocabulary.Client
{
    /// <summary>
    /// Парсер аргументов клиентского приложения словаря
    /// </summary>
    internal sealed class ClientArgumentsParser
    {
        /// <summary>
        /// Проверяет корректность аргументов, возвращает адрес сервера и команду
        /// </summary>
        /// <param name="args">Сырые аргументы приложения</param>
        /// <returns>
        /// Если аргументы некорректные, возвращает текст ошибки
        /// </returns>
        public ParsedArguments Parse(string[] args)
        {
            if (args == null || !args.Any())
            {
                return new ParsedArguments(Default.UrlIsNotSet);
            }

            if (args.Length == 1)
            {
                return new ParsedArguments(Default.PortIsNotSet);
            }

            if (args.Length == 2)
            {
                return new ParsedArguments(Default.CommandIsNotSet);
            }

            var uri = GetHttpUrl(args[0], args[1]);
            if (!Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            {
                return new ParsedArguments(Default.UrlIsInvalid);
            }

            return new ParsedArguments(new Uri(uri), GetCommand(args));
        }

        /// <summary>
        /// Возвращает http url 
        /// </summary>
        /// <param name="host">Имя хоста</param>
        /// <param name="port">Порт</param>
        /// <returns></returns>
        private string GetHttpUrl(string host, string port) => $"http://{host}:{port}/";

        /// <summary>
        /// Возвращает текст команды к серверу словаря
        /// </summary>
        /// <param name="args">Аргументы приложения</param>
        private string GetCommand(string[] args) => args.Skip(2)
            .Aggregate(new StringBuilder(), (sb, cur) => sb.Append(cur).Append(" "))
            .ToString().TrimEnd();
    }
}
