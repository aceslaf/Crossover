using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrossoverTask.Web.Validators
{
    public class NullableGreatherThan : ValidationAttribute
    {
        private string OtherProperty { get; set; }
        private int Min { get; set; }
        public NullableGreatherThan(int min,string propertyName)
        {
            OtherProperty = propertyName;
            Min = min;
        }
        

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return null;
            }

            var actualValue = Double.Parse(value.ToString());
            //COmpare to other
            var otherProperty = validationContext.ObjectType.GetProperty(OtherProperty);
            if (otherProperty != null)
            {  
                var otherValueAsObject = otherProperty.GetValue(validationContext.ObjectInstance);
                if (otherValueAsObject != null)
                {
                    var otherActualValue = Double.Parse(otherValueAsObject.ToString());
                    if (actualValue <= otherActualValue)
                    {
                        string otherPropertyDisplayName;
                        var otherPropAtributes = otherProperty.GetCustomAttributes(typeof(DisplayAttribute), true);
                        otherPropertyDisplayName = otherPropAtributes.Cast<DisplayAttribute>().Single().Name;
                        return new ValidationResult(string.Format("{0} must be greather than {1}", validationContext.DisplayName, otherPropertyDisplayName));
                    }
                }                
            }

            //COmpare to Min
            if (actualValue <= Min)
            {
                return new ValidationResult(string.Format("{0} must be greather than {1}", validationContext.DisplayName, Min));
            }

            return null;
        }
    }
}