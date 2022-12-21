using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.RegularExpressions;

// Overview of annotations: https://www.codeproject.com/Articles/1184173/DataAnnotations-in-Depth

namespace MVVM
{

    public class DataErrorInfoAttributeValidator
    {
        private readonly IDataErrorInfo _classInstance;
        private readonly Action<string> _updateErrorFucntion;
        private readonly Dictionary<string, PropertyValidator> _propertyValidations;

        public DataErrorInfoAttributeValidator(IDataErrorInfo classInstance, Action<string> updateErrorFunction)
        {
            _classInstance = classInstance;
            _updateErrorFucntion = updateErrorFunction;
            var validators = classInstance.GetType()
                            .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty)
                            .Select(i => new PropertyValidator(i, classInstance)).Where(j => j.Validators.Any());
            _propertyValidations = validators.ToDictionary(p => p.PropertyName, p => p);
        }

        public string Validate(string propertyName)
        {
            if (_propertyValidations.ContainsKey(propertyName))
            {
                var propertyValidation = _propertyValidations[propertyName];
                if (propertyValidation.CheckValidations())
                {
                    Errors = _propertyValidations.SelectMany(i => i.Value.ErrorMessages).ToArray();
                    _updateErrorFucntion?.Invoke(string.Join(Environment.NewLine, Errors));
                }
                return string.Join(Environment.NewLine, propertyValidation.ErrorMessages);
            }
            return string.Empty;
        }

        public IEnumerable<string> Errors { get; set; }

        private class PropertyValidator
        {
            private readonly PropertyInfo _propertyInfo;
            private readonly object _classInstance;
            public PropertyValidator(PropertyInfo propertyInfo, object classInstance)
            {
                _propertyInfo = propertyInfo;
                _classInstance = classInstance;
                Validators = propertyInfo.GetCustomAttributes().Where(i => i is ValidationAttribute).Cast<ValidationAttribute>().ToArray();
                Validators.Where(j => j is MethodValidator).Cast<MethodValidator>().ToList().ForEach(i => i.SetClassInstance(_classInstance));
                ErrorMessages = new string[0];
            }

            public bool CheckValidations()
            {
                var newValue = _propertyInfo.GetValue(_classInstance);

                var newErrorMessages = Validators.Where(i => !i.IsValid(newValue))
                        .Select(j => GetErrorMessage(_propertyInfo.Name, j))
                                .OrderBy(i => i).ToArray();
                var isChanged = !StringArraysEqual(newErrorMessages, ErrorMessages);
                ErrorMessages = newErrorMessages;
                return isChanged;
            }

            public string[] ErrorMessages { get; private set; }
            public string PropertyName => _propertyInfo.Name;
            public ValidationAttribute[] Validators { get; private set; }

            private static string GetErrorMessage(string name, ValidationAttribute validator)
            {
                if (!string.IsNullOrWhiteSpace(validator.ErrorMessage)) return validator.ErrorMessage;
                if (validator is RequiredAttribute) return
                        $"{FromCamelCase(name)} is required";
                if (validator is RangeUnitAttribute)
                    return $"{FromCamelCase(name)} should be in the range of {((RangeUnitAttribute)validator).Minimum} and {((RangeUnitAttribute)validator).Maximum} {((RangeUnitAttribute)validator).Units}";
                else if (validator is RangeAttribute) return
                        $"{FromCamelCase(name)} is not between the range of {((RangeAttribute)validator).Minimum} and {((RangeAttribute)validator).Maximum}";
                if (validator is MaxLengthAttribute) return
                    $"{name} text cannot be longer than {((MaxLengthAttribute)validator).Length} characters";
                throw new NotImplementedException(
                    $"Validator {validator.GetType().Name} does have default error message or specific error message for property {name}");
            }

            private bool StringArraysEqual(string[] str1, string[] str2)
            {
                if (str2.Length != str1.Length) return false;
                return !str1.Where((t, i) => t != str2[i]).Any();
            }

            private static string FromCamelCase(string value) =>
             Regex.Replace(value, @"(?<a>(?<!^)((?:[A-Z][a-z])|(?:(?<!^[A-Z]+)[A-Z0-9]+(?:(?=[A-Z][a-z])|$))|(?:[0-9]+)))", @" ${a}");
        }
    }
}