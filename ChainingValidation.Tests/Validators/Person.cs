using System;

namespace ChainingValidation.Tests.Validators
{
    public class Person
    {
        public int Age { get; }
        public string Name { get; }

        public Person(int age, string name)
        {
            this.Age = age;
            this.Name = name;
        }

        public override bool Equals(object obj)
        {
            return obj is Person p
                   && this.Age == p.Age
                   && this.Name == p.Name;
        }

        public override int GetHashCode()
        {
            return this.Age ^ this.Name.GetHashCode();
        }
    }
}