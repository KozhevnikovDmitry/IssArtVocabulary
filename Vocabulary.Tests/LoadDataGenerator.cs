using System;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace Vocabulary.Tests
{
    /// <summary>
    /// Генератор тестовых данных для jmeter тест LoadTest.jmx
    /// </summary>
    [TestFixture(Ignore = "Генерирует файл commands.csv")]
    public class LoadDataGenerator
    {
        /// <summary>
        /// Число команд в генерации
        /// </summary>
        private const int CommandsTotal = 1000;
        
        /// <summary>
        /// Число аргументов команды
        /// </summary>
        private const int ArgsTotal = 5;

        /// <summary>
        /// Длина слов
        /// </summary>
        private const int WordLength = 3;

        /// <summary>
        /// Длина значений
        /// </summary>
        private const int MeanLength = 5;

        /// <summary>
        /// Имя выходного файла 
        /// </summary>
        private const string DataFile = "commands.csv";

        /// <summary>
        /// Генератор случайных чисел для составления слов
        /// </summary>
        private readonly Random _random;

        /// <summary>
        /// Доступные символы
        /// </summary>
        private readonly char[] _symbols = { 'a', 'b', 'c', 'd', 'e' };

        /// <summary>
        /// ctor
        /// </summary>
        public LoadDataGenerator()
        {
            _random = new Random(DateTime.Now.Millisecond);
        }

        /// <summary>
        /// Пишет в <see cref="DataFile"/> корректные команды add, get, delete по три в строке
        /// </summary>
        [Test]
        public void Generate()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < CommandsTotal; i++)
            {
                sb.Append($"add {GenerateArgs()},")
                    .Append($"get {GenerateArgs()},")
                    .Append($"delete {GenerateArgs()}")
                    .AppendLine();
            }

            File.AppendAllText(Path.Combine(Environment.CurrentDirectory,DataFile), sb.ToString());
        }

        /// <summary>
        /// Возвращает случайные аргументы команды
        /// </summary>
        private string GenerateArgs()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < ArgsTotal; i++)
            {
                sb.Append(GenerateWord(i == 0? WordLength : MeanLength)).Append(" ");
            }

            return sb.ToString().Trim();
        }

        /// <summary>
        /// Возвращает случайное слово
        /// </summary>
        /// <param name="len">Длина слова из доступных символов</param>
        private string GenerateWord(int len)
        {
            string word = string.Empty;
            for (int i = 0; i < len; i++)
            {
                word += _symbols[_random.Next(_symbols.Length - 1)];
            }
            return word;
        }
    }
}
