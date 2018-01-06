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

        [Required(ErrorMessage = "Titulo do Comentário Obrigatório.")]
        [Display(Name = "Titulo do Comentário")]
        [StringLength(40, ErrorMessage = "O nome não pode ser superior a 40 Carateres.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Texto Obrigatório.")]
        [Display(Name = "Texto")]
        [StringLength(200, ErrorMessage = "A descrição não pode ser superior a 200 Carateres.")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Avaliação Obrigatória.")]
        [Display(Name = "Avaliação")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Avaliação tem de ser Numérica.")]
        public int Ranking { get; set; }

        [Required(ErrorMessage = "Instituição Obrigatória.")]
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