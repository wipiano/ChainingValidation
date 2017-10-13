namespace ChainingValidation
{
    /// <summary>
    /// Validation result class
    /// </summary>
    /// <typeparam name="TSource">typeof source object</typeparam>
    /// <typeparam name="TDetail">typeof result detail</typeparam>
    public class ValidationResult<TSource, TDetail>
    {
        /// <summary>
        /// Source object
        /// </summary>
        public TSource Source { get; }
        
        /// <summary>
        /// detail
        /// </summary>
        public TDetail Detail { get; }
        
        /// <summary>
        /// validation result
        /// </summary>
        public bool IsValid { get; }

        internal ValidationResult(TSource source, TDetail detail, bool isValid)
        {
            this.Source = source;
            this.Detail = detail;
            this.IsValid = isValid;
        }
    }
}