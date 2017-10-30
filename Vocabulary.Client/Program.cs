using System;
using System.Text;
using static Vocabulary.Client.Resources.Resources;

namespace Vocabulary.Client
{
    class Program
    {
        /// <summary>
        /// Точка входа
        /// </summary>
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            try
            {
                var arguments = new ClientArgumentsParser().Parse(args);
                if (arguments.Correct)
                {
                    using (var request = new VocabularyRequest(arguments.ServerUrl, arguments.Command))
                    {
                        Console.WriteLine(request.PostAsync().Result);
                    }
                }
                else
                {
                    Console.WriteLine(arguments.Error);
                }
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine(ex.Message);
#if DEBUG
                Console.WriteLine(ex);
#endif
            }
            catch (Exception ex)
            {
                Console.WriteLine(Default.UnexpectedError, ex.Message);
#if DEBUG
                Console.WriteLine(ex);
#endif
            }
#if DEBUG
            Console.ReadKey();
#endif
        }
    }
}
