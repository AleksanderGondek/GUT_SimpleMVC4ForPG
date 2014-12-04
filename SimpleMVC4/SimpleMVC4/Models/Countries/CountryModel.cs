using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleMVC4.Models.Countries
{
    [Table("Countries")]
    public class CountryModel
    {
        [Key]
        [Display(Name = "Country Id")]
        public int CountryId { get; set; }
        [Required]
        //[Index(IsUnique=true)]
        [Display(Name = "Country Name")]
        [RegularExpression(@"^[a-zA-Z.]{3,70}$", ErrorMessage = "Country should be at least 3 characters long and no more than 70 characters long")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Country Total Area")]
        [RegularExpression(@"^[0-9]\d{0,5}$", ErrorMessage = "Country total area should be a postive integer")]
        public int TotalArea { get; set; }
        [Required]
        [Display(Name = "Country Offical Language")]
        [MinLength(4)]
        public string OfficialLanguage { get; set; }

        public virtual ICollection<CountryModel> CountyModels { get; set; }
    }
}