

namespace StudentManagement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class registration
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
        [DisplayName("Course")]
        public Nullable<int> course_id { get; set; }

        [Required]
        [DisplayName("Batch")]
        public Nullable<int> batch_id { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 10)]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile No.")]
        [Display(Name = "Tel No")]
        public Nullable<int> telno { get; set; }
        public string created_by { get; set; }
        public Nullable<System.DateTime> create_time { get; set; }
        public string updated_by { get; set; }
        public Nullable<System.DateTime> update_time { get; set; }
    
        public virtual batch batch { get; set; }
        public virtual course course { get; set; }
    }
}
