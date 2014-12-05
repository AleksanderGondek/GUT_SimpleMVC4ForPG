using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

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
        [RegularExpression(@"^[A-Za-z0-9\s]{1,70}$", ErrorMessage = "Country should be at least 3 characters long and no more than 70 characters long")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Country Total Area")]
        [RegularExpression(@"^[0-9]\d{0,5}$", ErrorMessage = "Country total area should be a postive integer")]
        public int TotalArea { get; set; }
        [Required]
        [Display(Name = "Country Offical Language")]
        [MinLength(4, ErrorMessageResourceName="Error",ErrorMessageResourceType=typeof(SimpleMVC4.Resources.SimpleL10nMessages))]
        public string OfficialLanguage { get; set; }

        [NotMapped]
        public string[] SelectedCountries { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> AllCountries { get; set; }
        
        [Display(Name = "Country Neighbours")]
        public virtual ICollection<CountryModel> CountryModels { get; set; }
    }
}