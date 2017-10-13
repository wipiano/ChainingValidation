using System;

namespace ChainingValidation
{
    /// <summary>
    /// Validator
    /// </summary>
    /// <typeparam name="TSource">typeof source object</typeparam>
    /// <typeparam name="TDetail">typeof result detail</typeparam>
    public class Validator<TSource, TDetail>
    {
        /// <summary>
        /// previous validator
        /// </summary>
        protected readonly Validator<TSource, TDetail> Prev;
        
        /// <summary>
        /// function to validate object
        /// </summary>
        private readonly Func<TSource, bool> _validator;

        /// <summary>
        /// validation result detail object that is returned when this validation is failed 
        /// </summary>
        private readonly TDetail _detail;

        /// <summary>
        /// Validate object
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public virtual ValidationResult<TSource, TDetail> Validate(TSource source)
        {
            var prevResult = this.Prev.Validate(source);

            return prevResult.IsValid ? ValidateThis(source) : prevResult;
        }
        
        /// <summary>
        /// Evaluate own
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected ValidationResult<TSource, TDetail> ValidateThis(TSource source)
        {
            bool isValid = _validator(source);
            TDetail detail = isValid ? default(TDetail) : _detail;
            return new ValidationResult<TSource, TDetail>(source, detail, isValid);
        }

        /// <summary>
        /// Create a Validator
        /// </summary>
        /// <param name="prev">previous validator</param>
        /// <param name="validator">function to validate object</param>
        /// <param name="detail">validation detail if failed</param>
        internal Validator(Validator<TSource, TDetail> prev, Func<TSource, bool> validator, TDetail detail)
        {
            this.Prev = prev;
            _validator = validator;
            _detail = detail;
        }
    }

    /// <summary>
    /// First Validator
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDetail"></typeparam>
    internal sealed class FirstValidator<TSource, TDetail> : Validator<TSource, TDetail>
    {
        internal FirstValidator(Func<TSource, bool> validator, TDetail detail)
            :base (null, validator, detail)
        {
        }

        public override ValidationResult<TSource, TDetail> Validate(TSource source)
        {
            return ValidateThis(source);
        }
    }
}