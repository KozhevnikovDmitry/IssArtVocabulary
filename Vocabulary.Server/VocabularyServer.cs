using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Vocabulary.Server.Resources.Resources;

namespace Vocabulary.Server
{
    /// <summary>
    /// Асинхронный HTTP cервер для обработки запросов к словарю
    /// </summary>
    internal sealed class VocabularyServer : IDisposable
    {
        /// <summary>
        /// Обработчик запросов
        /// </summary>
        private readonly IVocabularyRequestHandler _vocabularyRequestHandler;

        /// <summary>
        /// URL, который прослушивает сервера
        /// </summary>
        private readonly string _url;

        /// <summary>
        /// HTTP слушатель
        /// </summary>
        private readonly HttpListener _listener;

        /// <summary>
        /// Токены отмены задач на обработку запросов
        /// </summary>
        private CancellationTokenSource _cancellationTokenSource;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="vocabularyRequestHandler">Обработчик запросов</param>
        /// <param name="url">URL, который будет прослушивать сервер</param>
        /// <exception cref="ArgumentException">URL имеет неверный формат</exception>
        /// <exception cref="ArgumentNullException">Значение аргумента Null</exception>
        public VocabularyServer(IVocabularyRequestHandler vocabularyRequestHandler, string url)
        {
            if (vocabularyRequestHandler == null) throw new ArgumentNullException(nameof(vocabularyRequestHandler));
            if (url == null) throw new ArgumentNullException(nameof(url));
            if(!Uri.IsWellFormedUriString(url, UriKind.Absolute)) throw new ArgumentException(Default.UrlIsInvalid, nameof(url), new UriFormatException(url));

            _vocabularyRequestHandler = vocabularyRequestHandler;
            _url = url;
            _listener = new HttpListener();
        }

        /// <summary>
        /// Стартует сервер
        /// </summary>
        /// <exception cref="ApplicationException">Ошикба работы сервера</exception>
        public async Task StartAsync()
        {
            try
            {
                if (_listener.IsListening)
                {
                    return;
                }

                _listener.Prefixes.Clear();
                _listener.Prefixes.Add(_url);
                _listener.Start();
                _cancellationTokenSource = new CancellationTokenSource();
                while (!_cancellationTokenSource.IsCancellationRequested)
                {
                    try
                    {
                        var context = await _listener.GetContextAsync();
                        await Task.Factory.StartNew(async () => await HandleRequestAsync(context),
                                _cancellationTokenSource.Token)
                            .ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException(Default.RequestError, ex);
                    }
                }
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(Default.StartServerError, ex);
            }

        }

        /// <summary>
        /// Обрабатывает запрос 
        /// </summary>
        /// <param name="context">Контекст запроса</param>
        /// <exception cref="ApplicationException">Ошибка обработки запроса</exception>
        /// <returns></returns>
        private async Task HandleRequestAsync(HttpListenerContext context)
        {
            try
            {
                using (var reader = new StreamReader(context.Request.InputStream))
                {
                    var body = reader.ReadToEnd();
                    var result = _vocabularyRequestHandler.Handle(body);
                    var bytes = Encoding.UTF8.GetBytes(result);
                    context.Response.StatusCode = 200;
                    context.Response.SendChunked = true;
                    await context.Response.OutputStream.WriteAsync(bytes, 0, bytes.Length);
                    context.Response.OutputStream.Close();
                }
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.OutputStream.Close();
                throw new ApplicationException(Default.HandleRequestError, ex);
            }
        }

        /// <summary>
        /// Останавливает сервер
        /// </summary>
        /// <exception cref="ApplicationException">Ошибка завершения работы сервера</exception>
        public void Stop()
        {
            try
            {
                if (!_listener.IsListening)
                {
                    return;
                }

                _cancellationTokenSource?.Cancel();
                _listener.Stop();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(Default.StopServerError, ex);
            }
        }

        /// <summary>
        /// Отпускает ресурсы сервера
        /// </summary>
        public void Dispose()
        {
            Stop();
            _listener.Close();
            _cancellationTokenSource?.Dispose();
        }
    }
}
