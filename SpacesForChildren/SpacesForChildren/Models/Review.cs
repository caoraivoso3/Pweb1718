using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SpacesForChildren.Models {

    [Table("Review")]
    public class Review {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
                
        [Required]
        public string Text { get; set; }

        [Required]
        public int Ranking { get; set; }

        [Required]
        public virtual Institution Institution { get; set; }

        //[Required]
        //public Contract Contract { get; set; }

        [ForeignKey("Institution")]
        public string InstitutionId { get; set; }

        //[ForeignKey("Contract")]
        //public int ContractId { get; set; }

        public virtual ICollection<Contract> Contracts { get; set; }
    }
}