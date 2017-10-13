using System;

namespace ChainingValidation.Validators
{
    public static class EqualsValidator
    {
        /// <summary>
        /// Add equality validator
        /// </summary>
        /// <param name="prev"></param>
        /// <param name="expected"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <returns></returns>
        public static SimpleValidator<TSource> Equals<TSource>(this SimpleValidator<TSource> prev, TSource expected)
        {
            return prev.Add(source => source.Equals(expected));
        }

        /// <summary>
        /// Add equality validator
        /// </summary>
        /// <param name="prev"></param>
        /// <param name="selector"></param>
        /// <param name="expected"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <returns></returns>
        public static SimpleValidator<TSource> Equals<TSource, TTarget>(this SimpleValidator<TSource> prev,
            Func<TSource, TTarget> selector, TTarget expected)
        {
            return prev.Add(source => selector(source).Equals(expected));
        }

        /// <summary>
        /// Add equality validator
        /// </summary>
        /// <param name="prev"></param>
        /// <param name="expected"></param>
        /// <param name="detail"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDetail"></typeparam>
        /// <returns></returns>
        public static Validator<TSource, TDetail> Equals<TSource, TDetail>(this Validator<TSource, TDetail> prev,
            TSource expected, TDetail detail)
        {
            return prev.Add(source => source.Equals(expected), detail);
        }

        /// <summary>
        /// Add equality validator
        /// </summary>
        /// <param name="prev"></param>
        /// <param name="selector"></param>
        /// <param name="expected"></param>
        /// <param name="detail"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <typeparam name="TDetail"></typeparam>
        /// <returns></returns>
        public static Validator<TSource, TDetail> Equals<TSource, TTarget, TDetail>(
            this Validator<TSource, TDetail> prev, Func<TSource, TTarget> selector, TTarget expected, TDetail detail)
        {
            return prev.Add(source => selector(source).Equals(expected), detail);
        }
    }
}