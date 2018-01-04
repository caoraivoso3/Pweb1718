using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SpacesForChildren.Models {
    public class Service {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int MinAgeYear { get; set; }

        [Required]
        public int MaxAgeYear { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public virtual Institution Institution { get; set; }

        [ForeignKey("Institution")]
        public string InstitutionId { get; set; }

        public virtual ICollection<Contract> Contracts { get; set; }
    }
}