using Xunit;

namespace ChainingValidation.Tests
{
    public class SimpleValidatorTest
    {
        [Fact]
        public void SingleValidationSuccessTest()
        {
            var validator = ChainingValidator.CreateSimple<int>(source => source < 10);

            // valid source
            validator.Validate(1).IsTrue();
        }

        [Fact]
        public void SingleValidationFailedTest()
        {
            var validator = ChainingValidator.CreateSimple<int>(source => source < 10);

            // invalid source
            validator.Validate(10).IsFalse();
        }

        [Fact]
        public void MultiValidationSuccessTest()
        {
            var validator = ChainingValidator
                .CreateSimple<int>(source => source < 10)
                .Add(source => source >= 0);

            validator.Validate(5).IsTrue();
        }

        [Fact]
        public void MultiValidationTooBigTest()
        {
            var validator = ChainingValidator
                .CreateSimple<int>(source => source < 10)
                .Add(source => source >= 0);

            validator.Validate(20).IsFalse();
        }
        
        [Fact]
        public void MultiValidationTooSmallTest()
        {
            var validator = ChainingValidator
                .CreateSimple<int>(source => source < 10)
                .Add(source => source >= 0);

            validator.Validate(-1).IsFalse();
        }
    }
}