

namespace StudentManagement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class user
    {
        public int id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(50, MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z]+(\s+[a-zA-Z]+)*$", ErrorMessage = "Only Alphabets and space are allowed.")]
        public string firstname { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50, MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z]+(\s+[a-zA-Z]+)*$", ErrorMessage = "Only Alphabets and space are allowed.")]
        public string lastname { get; set; }

        [Required]
        [Display(Name = "User Name")]
        [StringLength(50, MinimumLength = 5)]
        public string username { get; set; }

        [StringLength(50, MinimumLength = 8)]
        [Required]
        [Display(Name = "Password")]
        public string password { get; set; }
    }
}
