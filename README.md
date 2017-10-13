# ChainingValidation
Lightweight method chaining for validation.

Current version is `v0.0.1`.

## SimpleValidator

`SimpleValidator<TSource>` is a validator class that returns `bool`.
You can't get detail of validation result from `SimpleValidator`.

Use `ChainingValidator.CreateSimple()` to create `SimpleValidator`.
Call `SimpleValidator.Validate()` to get result.

```csharp
// validate (0 <= source < 10)
var validator = ChainingValidator
    .CreateSimple<int>(source => source < 10)
    .Add(source => source >= 0);
    
validator.Validate(10); // == false (invalid)
validator.Validate(5);  // == true  (valid)
```


## Validator

`Validator<TSource, TDetail>` is a validator class that returns `ValidationResult`.
You can get detail of validation result.

Use `ChainingValidator.Create()` to create `Validator`.
Call `Validator.Validate()` to get result.

```csharp
// validate (0 <= source < 10)
var validator = ChainingValidator
    .Create<int, NumberDetail?>((int source) => source < 10, (NumberDetail?)NumberDetail.TooBig)
    .Add((source) => source >= 0, NumberDetail.TooSmall);
    
validator.Validate(10); // == {IsValid: false, Detail: NumberDetail.TooBig, Source: 10}
validator.Validate(-1); // == {IsValid: false, Detail: NumberDetail.TooSmall, Source: -1}
validator.Validate(5);  // == {IsValid: true, Detail: null, Source: 5}
```

```csharp
private enum NumberDetail
{
    TooBig,
    TooSmall,
}
```