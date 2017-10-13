using ChainingValidation.Validators;
using Xunit;

namespace ChainingValidation.Tests.Validators
{
    public class EqualsValidatorTest
    {
        [Fact]
        public void SimpleEqualsTest()
        {
            var expectedPerson = new Person(20, "Taro");
            var validator = ChainingValidator.CreateSimple<Person>()
                .AddEquals(expectedPerson);

            validator.Validate(new Person(20, "Taro")).IsTrue();
            validator.Validate(new Person(19, "Taro")).IsFalse();
            validator.Validate(new Person(20, "Taroo")).IsFalse();
        }
        
    }
}