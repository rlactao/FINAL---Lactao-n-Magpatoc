using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Student.Account.BusinessLogic;

namespace FINAL___Lactao_n_Magpatoc.Models
{
    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = false)]
    public class ValidateLoginAttribute : CompareAttribute
    {
        public bool Valid { get; set; }

        public ValidateLoginAttribute(string otherProperty) : base(otherProperty)
        {
            
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Valid = value.ToString().Equals(this.OtherProperty);
            if (Valid)
                return ValidationResult.Success;
            else
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));

        }
    }
}
