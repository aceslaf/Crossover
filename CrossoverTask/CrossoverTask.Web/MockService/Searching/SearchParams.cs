using CrossoverTask.Data.Entities;
using CrossoverTask.Web.Validators;
using System.ComponentModel.DataAnnotations;
namespace MockService.Searching
{
    
    public class SearchParams
    {
        public Gender? Gender { get; set; }
        
        [Range(0, 2147483647)]
        public int? AgeStart { get; set; }
        
        [NullableGreatherThan(0, "AgeStart")]
        public int? AgeEnd { get; set; }
        
        [Range(0, 2147483647)]
        public float? HeightStart { get; set; }
        
        [NullableGreatherThan(0, "HeightStart")]
        public float? HeightEnd { get; set; }
        
        [Range(0, 2147483647)]
        public float? WeightStart { get; set; }
        
        [NullableGreatherThan(0, "WeightStart")]
        public float? WeightEnd { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        [Range(1, 1000)]
        public int MaxNumberOfResults { get; set; }

        [Required]
        public string RecieverEmail { get; set; }

        public string SecretKey { get; set; }

    }
}
