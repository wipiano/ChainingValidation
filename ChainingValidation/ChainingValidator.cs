using System;

namespace ChainingValidation
{
    public static class ChainingValidator
    {
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
    }
}