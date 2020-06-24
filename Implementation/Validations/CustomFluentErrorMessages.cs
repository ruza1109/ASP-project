using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Implementation.Validations
{
    public class CustomFluentErrorMessages : FluentValidation.Resources.LanguageManager
    {
        public CustomFluentErrorMessages()
        {
            AddTranslation("en", "NotEmptyValidator", "'{PropertyName}' field is required.");
            AddTranslation("en", "MinimumLengthValidator", "'{PropertyName}' field must have more than {MinLength} characters.");
            AddTranslation("en", "MaximumLengthValidator", "'{PropertyName}' field must have less than {MaxLength} characters.");
        }
    }
}
