using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleMVC4.Models.Files
{
    [Table("Files")]
    public class FileModel
    {
        [Key]
        [Display(Name = "File Id")]
        public int FileId { get; set; }
        [Display(Name = "File Name")]
        public string FileName { get; set; }
        [Display(Name = "Content Type")]
        public string ContentType { get; set; }
        public byte[] FileBytes { get; set; }
    }
}