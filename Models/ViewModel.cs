using System.ComponentModel.DataAnnotations;

namespace InfoTrackAdCount.Models
{
    public class ViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "* Part numbers must be between 3 and 50 character in length.")]
        [Display(Name = "Search Keyword :")]
        public string Keyword { get; set; }

        [Required]
        [Display(Name = "Count URL :")]
        public string Url { get; set; }

        public string Result { get; set; }
    }
}