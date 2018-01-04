using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpacesForChildren.Models {

    [Table("Contract")]
    public class Contract {

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Data Inicial do Contrato")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/YYYY}", ApplyFormatInEditMode = true)]
        public DateTime? InitialDate { get; set; }

        [Required]
        [Display(Name = "Data Final do Contrato")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/YYYY}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        public bool IsApproved { get; set; }

        public int TotalPrice { get; set; }

        [Required]
        public virtual Child Child { get; set; }

        [Required]
        public virtual Parent Parent { get; set; }

        [Required]
        public virtual Review Review { get; set; }

        [ForeignKey("Parent")]
        public string ParentId { get; set; }

        [ForeignKey("Child")]
        public int ChildId { get; set; }

        [ForeignKey("Review")]
        public int ReviewId { get; set; } 

        public virtual ICollection<Service> Services { get; set; }

    }
}