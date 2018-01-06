using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SpacesForChildren.Models {

    [Table("Activities")]
    public class Activity {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Texto do Pedido Obrigatório.")]
        [Display(Name = "Texto")]
        [StringLength(200, ErrorMessage = "O texto não pode ser superior a 200 Carateres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Data da Atividade Obrigatória.")]
        [Display(Name = "Data Inicial do Contrato.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/YYYY}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }

        [Required(ErrorMessage = "Descrição da Atividade Obrigatória.")]
        [Display(Name = "Descrição")]
        [StringLength(200, ErrorMessage = "A descrição não pode ser superior a 200 Carateres.")]
        public string Description { get; set; }

        [ForeignKey("Institution")]
        public string InstitutionId { get; set; }

        [Required]
        public virtual Institution Institution { get; set; }

    }
}