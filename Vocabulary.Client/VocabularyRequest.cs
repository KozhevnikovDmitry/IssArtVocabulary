using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using static Vocabulary.Client.Resources.Resources;

namespace Vocabulary.Client
{
    /// <summary>
    /// Запрос к серверу словаря
    /// </summary>
    internal sealed class VocabularyRequest : IDisposable
    {
        /// <summary>
        /// HTTP клиент
        /// </summary>
        private readonly HttpClient _http;

       /// <summary>
       /// Контент запроса - содердит команду для словаря
       /// </summary>
        private readonly StringContent _command;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="requestUrl">URL сервера словаря</param>
        /// <param name="command">Команда для словаря</param>
        /// <exception cref="ArgumentNullException">Значение аргумента Null</exception>
        public VocabularyRequest(Uri requestUrl, string command)
        {
            if (requestUrl == null) throw new ArgumentNullException(nameof(requestUrl));
            if (string.IsNullOrWhiteSpace(command)) throw new ArgumentNullException(nameof(command));
            _http = new HttpClient { BaseAddress = requestUrl };
            _command = new StringContent(command);
        }

        /// <summary>
        /// Отправляет команду на сервер
        /// </summary>
        /// <exception cref="ApplicationException">Ошибка запроса</exception>
        /// <returns>Возвращает результат работу команды</returns>
        public async Task<string> PostAsync()
        {
            try
            {
                var resp = await _http.PostAsync("", _command);
                if (resp.StatusCode != HttpStatusCode.OK)
                {
                    return string.Format(Default.ResponseErrorCode, resp.StatusCode);
                }

                var result = await resp.Content.ReadAsStringAsync();
                return result;
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException(Default.RequestError, ex);
            }
        }

        /// <summary>
        /// Освобождает ресурсы запроса
        /// </summary>
        public void Dispose()
        {
            _http.Dispose();
        }
    }
}