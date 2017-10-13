using System;

namespace ChainingValidation
{
    public static class ChainingValidator
    {
        /// <summary>
        /// Create empty Validator
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDetail"></typeparam>
        /// <returns></returns>
        public static Validator<TSource, TDetail> Create<TSource, TDetail>()
            => Create<TSource, TDetail>(source => true, default(TDetail));
        
        /// <summary>
        /// Create a Validator
        /// </summary>
        /// <param name="validator"></param>
        /// <returns></returns>
        public static Validator<TSource, TDetail> Create<TSource, TDetail>(Func<TSource, bool> validator, TDetail detail)
        {
            return new FirstValidator<TSource, TDetail>(validator, detail);
        }

        /// <summary>
        /// Create a empty SimpleValidator
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <returns></returns>
        public static SimpleValidator<TSource> CreateSimple<TSource>()
            => CreateSimple<TSource>(source => true);
        
        /// <summary>
        /// Create a simple validator
        /// </summary>
        /// <param name="validator"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <returns></returns>
        public static SimpleValidator<TSource> CreateSimple<TSource>(Func<TSource, bool> validator)
        {
            return new FirstSimpleValidator<TSource>(validator);
        }
        
        /// <summary>
        /// Add validator
        /// </summary>
        /// <param name="prev">previous validator</param>
        /// <param name="validator">validator</param>
        /// <typeparam name="TSource">type of source</typeparam>
        /// <typeparam name="TDetail">type of validation result</typeparam>
        /// <returns>new validator</returns>
        public static Validator<TSource, TDetail> Add<TSource, TDetail>(this Validator<TSource, TDetail> prev, Func<TSource, bool> validator, TDetail detail)
        {
            return new Validator<TSource, TDetail>(prev, validator, detail);
        }

        /// <summary>
        /// Add validator
        /// </summary>
        /// <param name="prev"></param>
        /// <param name="validator"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <returns></returns>
        public static SimpleValidator<TSource> Add<TSource>(this SimpleValidator<TSource> prev,
            Func<TSource, bool> validator)
        {
            return new SimpleValidator<TSource>(prev, validator);
        }
    }
}