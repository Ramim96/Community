using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Community.Models
{
    public class Search
    {
        [Display(Name = "Title")]
        [Required(ErrorMessage = "Provide the input")]
        [MinLength(1, ErrorMessage = "The text is too short")]
        public string Title { get; set; }
        public string Type { get; set; }
    }
}
