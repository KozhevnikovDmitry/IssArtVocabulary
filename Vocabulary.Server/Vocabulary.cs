using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Vocabulary.Server
{
    /// <summary>
    /// Толковый словарь в памяти
    /// </summary>
    internal sealed class Vocabulary : IVocabulary
    {
        /// <summary>
        /// Хранилище слово+значения
        /// </summary>
        private readonly ConcurrentDictionary<string, HashSet<string>> _storage;

        /// <summary>
        /// Ctor
        /// </summary>
        public Vocabulary()
        {
            _storage = new ConcurrentDictionary<string, HashSet<string>>();
        }

        /// <summary>
        /// Возвращает значения слова из словаря.
        /// </summary>
        /// <param name="word">Слово</param>
        /// <exception cref="ArgumentNullException">Значение слова Null</exception>
        public string[] Get(string word)
        {
            if (word == null) throw new ArgumentNullException(nameof(word));

            HashSet<string> result;
            if (_storage.TryGetValue(word.Trim().ToLower(), out result))
            {
                return result.ToArray();
            }

            return new string[0];
        }

        /// <summary>
        /// Добавляет слово в словарь.
        /// Если значения не заданы, слово не будет добавлено
        /// </summary>
        /// <param name="word">Слово</param>
        /// <param name="means">Значения слова</param>
        /// <exception cref="ArgumentNullException">Значение аргумента Null</exception>
        /// <returns>Добавленные значения</returns>
        public string[] Add(string word, IEnumerable<string> means)
        {
            if (word == null) throw new ArgumentNullException(nameof(word));
            if (means == null) throw new ArgumentNullException(nameof(means));

            string[] result = new string[0];

            var meansArray = NormalizeMeans(means);

            _storage.AddOrUpdate(word.Trim().ToLower(), k =>
                {
                    var meansValue = new HashSet<string>(meansArray);
                    result = meansValue.ToArray();
                    return meansValue;
                },
                (k, set) =>
                {
                    result = meansArray.Where(v => !set.Contains(v)).Distinct().ToArray();
                    return new HashSet<string>(set.Union(meansArray));
                });

            return result;
        }

        /// <summary>
        /// Удаляет набор значений слова из словаря.
        /// </summary>
        /// <param name="word">Слово</param>
        /// <param name="means">Значения, которые надо удалить</param>
        /// <exception cref="ArgumentNullException">Значение аргумента Null</exception>
        /// <returns>
        /// Возвращает true, значения были удалены из словаря.
        /// False, если такого слова или значений не было.
        /// </returns>
        public bool Delete(string word, IEnumerable<string> means)
        {
            if (word == null) throw new ArgumentNullException(nameof(word));
            if (means == null) throw new ArgumentNullException(nameof(means));
            
            var meansArray = NormalizeMeans(means);
            if (!meansArray.Any())
            {
                return false;
            }

            HashSet<string> curValue;
            while (_storage.TryGetValue(word.Trim().ToLower(), out curValue))
            {
                var newNewValue = new HashSet<string>(curValue.Except(meansArray));
                if (newNewValue.SetEquals(curValue))
                {
                    break;
                }

                if (_storage.TryUpdate(word.Trim().ToLower(), newNewValue, curValue))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Возвращает массив не-null значений слова в нижнем регистре без предшествующих и последующих пробелов.
        /// </summary>
        /// <param name="means">Значения слова</param>
        private string[] NormalizeMeans(IEnumerable<string> means)
        {
            var normalized = means.Where(m => m != null).Select(m => m.Trim().ToLower());
            var result = normalized as string[] ?? normalized.ToArray();
            return result;
        }
    }
}