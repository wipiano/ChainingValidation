namespace ChainingValidation
{
    /// <summary>
    /// delegate to validate object
    /// </summary>
    public delegate (bool isValid, TDetail detail) ValidatorFunc<TSource, TDetail>(TSource source);
    
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
        private readonly ValidatorFunc<TSource, TDetail> _validator;

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
            (bool isValid, TDetail detail) = _validator(source);
            return new ValidationResult<TSource, TDetail>(source, detail, isValid);
        }

        /// <summary>
        /// Create a Validator
        /// </summary>
        /// <param name="prev">previous validator</param>
        /// <param name="validator">function to validate object</param>
        internal Validator(Validator<TSource, TDetail> prev, ValidatorFunc<TSource, TDetail> validator)
        {
            this.Prev = prev;
            _validator = validator;
        }
    }

    /// <summary>
    /// First Validator
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDetail"></typeparam>
    internal sealed class FirstValidator<TSource, TDetail> : Validator<TSource, TDetail>
    {
        internal FirstValidator(ValidatorFunc<TSource, TDetail> validator)
            :base (null, validator)
        {
        }

        public override ValidationResult<TSource, TDetail> Validate(TSource source)
        {
            return ValidateThis(source);
        }
    }
}