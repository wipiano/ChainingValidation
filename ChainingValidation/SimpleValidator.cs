using System;

namespace ChainingValidation
{
    /// <summary>
    /// Validator without detail result
    /// </summary>
    public class SimpleValidator<TSource>
    {
        /// <summary>
        /// previous validator
        /// </summary>
        protected SimpleValidator<TSource> Prev;
        
        /// <summary>
        /// function for validation
        /// </summary>
        protected readonly Func<TSource, bool> Validator;

        public virtual bool Validate(TSource source)
        {
            return this.Prev.Validate(source) && this.Validator(source);
        }

        internal SimpleValidator(SimpleValidator<TSource> prev, Func<TSource, bool> validator)
        {
            this.Prev = prev;
            this.Validator = validator;
        }
    }

    public sealed class FirstSimpleValidator<TSource> : SimpleValidator<TSource>
    {
        internal FirstSimpleValidator(Func<TSource, bool> validator)
            : base(null, validator)
        {
        }

        public override bool Validate(TSource source)
        {
            return this.Validator(source);
        }
    }
}