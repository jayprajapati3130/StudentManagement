
namespace StudentManagement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class course
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public course()
        {
            this.registrations = new HashSet<registration>();
        }

        [Required]
        [Display(Name = "Course Name")]
        public int id { get; set; }

        [Required]
        [Display(Name = "Course Name")]
        public string course1 { get; set; }

        [Required]
        [Display(Name = "Duration")]
        public Nullable<int> duration { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<registration> registrations { get; set; }
    }
}
