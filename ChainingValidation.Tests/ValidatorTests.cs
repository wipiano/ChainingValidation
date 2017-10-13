using Xunit;

namespace ChainingValidation.Tests
{
    public class ValidatorTests
    {
        [Fact]
        public void SingleValidationSuccessTest()
        {
            var validator = ChainingValidator
                .Create<int, NumberDetail?>(source => source < 10, NumberDetail.TooBig);

            // valid source
            var result = validator.Validate(1);
            result.Detail.IsNull();
            result.IsValid.IsTrue();
            result.Source.Is(1);
        }

        [Fact]
        public void SingleValidationFailedTest()
        {
            var validator = ChainingValidator
                .Create<int, NumberDetail?>(source => source < 10, NumberDetail.TooBig);
            
            // invalid source
            var result = validator.Validate(10);
            result.Detail.Is(NumberDetail.TooBig);
            result.IsValid.IsFalse();
            result.Source.Is(10);
        }
        
        [Fact]
        public void MultiValidationSuccessTest()
        {
            var validator = ChainingValidator
                .Create<int, NumberDetail?>((int source) => source < 10, (NumberDetail?)NumberDetail.TooBig)
                .Add((source) => source >= 0, NumberDetail.TooSmall);

            var result = validator.Validate(5);
            result.Detail.IsNull();
            result.IsValid.IsTrue();
            result.Source.Is(5);
        }

        [Fact]
        public void MultiValidationTooBigTest()
        {
            var validator = ChainingValidator
                .Create<int, NumberDetail?>((int source) => source < 10, (NumberDetail?)NumberDetail.TooBig)
                .Add((source) => source >= 0, NumberDetail.TooSmall);

            var result = validator.Validate(20);
            result.Detail.Is(NumberDetail.TooBig);
            result.IsValid.IsFalse();
            result.Source.Is(20);
        }
        
        [Fact]
        public void MultiValidationTooSmallTest()
        {
            var validator = ChainingValidator
                .Create<int, NumberDetail?>((int source) => source < 10, (NumberDetail?)NumberDetail.TooBig)
                .Add((source) => source >= 0, NumberDetail.TooSmall);

            var result = validator.Validate(-1);
            result.Detail.Is(NumberDetail.TooSmall);
            result.IsValid.IsFalse();
            result.Source.Is(-1);
        }

        private enum NumberDetail
        {
            TooBig,
            TooSmall,
        }
    }
}