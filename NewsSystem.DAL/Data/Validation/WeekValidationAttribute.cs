using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsSystem.DAL.Data.Validation
{
    public class WeekValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && value is DateTime dateValue)
            {
                // Perform your custom validation logic
                if (dateValue.Day >= DateTime.Now.Day && dateValue.Day <= DateTime.Now.Day+7)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Date must be between today and a week from today.");
                }
            }

            return ValidationResult.Success; // Return success for null or non-string values
        }
    }
}
