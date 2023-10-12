using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Core.Domain.Tests
{
    public static class TestHelper
    {
        public static IEnumerable<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model);

            Validator.TryValidateObject(model, validationContext, validationResults, true);

            return validationResults;
        }
    }
}
