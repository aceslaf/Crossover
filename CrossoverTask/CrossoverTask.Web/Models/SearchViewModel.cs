using CrossoverTask.Data.Entities;
using CrossoverTask.Web.Validators;
using Foolproof;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CrossoverTask.Web.Models
{
    public class SearchViewModel
    {
        [Display(Name = "Gender")]
        public Gender? Gender { get; set; }

        [Range(0, 2147483647)]
        [Display(Name = "Minimal age")]
        public int? MinAge { get; set; }

        [NullableGreatherThan(0,"MinAge")]
        [Display(Name = "Maximal age")]
        public int? MaxAge { get; set; }

        [Range(0, 2147483647)]
        [Display(Name = "Minimal weight")]
        public float? MinWeight { get; set; }

        [NullableGreatherThan(0, "MinWeight")]
        [Display(Name = "Maximal weight")]
        public float? MaxWeight { get; set; }

        [Range(0, 2147483647)]
        [Display(Name = "Minimal height")]
        public float? MinHeight { get; set; }

        [NullableGreatherThan(0, "MinHeight")]
        [Display(Name = "Maximal height")]
        public float? MaxHeight { get; set; }

        [Display(Name = "Convict's first name")]
        public string FirstName { get; set; }

        [Display(Name = "Convict's last name")]
        public string LastName { get; set; }

        [Range(1,1000)]
        [Display(Name = "Maximal number of results")]
        public int MaxResults { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Recipient email")]
        public string Email { get; set; }
        public bool HasAtLeastOneCriteria
        {
            get
            {
                List<object> criteriaProperties = new List<object>();
                criteriaProperties.Add(MinAge);
                criteriaProperties.Add(MaxAge);
                criteriaProperties.Add(MinWeight);
                criteriaProperties.Add(MaxWeight);
                criteriaProperties.Add(MinHeight);
                criteriaProperties.Add(MaxHeight);
                criteriaProperties.Add(FirstName);
                criteriaProperties.Add(LastName);
                criteriaProperties.Add(Gender);
                return criteriaProperties.Any(o => o != null);
            } 
        }
    }
}