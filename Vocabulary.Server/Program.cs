using System;
using System.Text;
using System.Threading.Tasks;
using static Vocabulary.Server.Resources.Resources;

namespace Vocabulary.Server
{
    class Program
    {
        /// <summary>
        /// Точка входа
        /// </summary>
        static void Main(string[] args)
        {
            try
            {
                Console.OutputEncoding = Encoding.UTF8;
                using (var server = new VocabularyServerFactory().Get("http://127.0.0.1:9020/"))
                {
                    var start = server.StartAsync();
                    Console.WriteLine(Default.ServerIsStarted);
                    Task.WaitAny(start, Task.Run(() => Console.ReadKey()));
                    if (start.IsFaulted && start.Exception != null)
                    {
                        throw start.Exception;
                    }
                    server.Stop();
                }
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine(ex.Message);
#if DEBUG
                Console.WriteLine(ex);
#endif
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(Default.UnexpectedError, ex.Message);
#if DEBUG
                Console.WriteLine(ex);
#endif
                Console.ReadKey();
            }
        }
    }

}
