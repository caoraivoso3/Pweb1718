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

        [Required(ErrorMessage = "Titulo do Pedido Obrigatório.")]
        [Display(Name = "Titulo")]
        [StringLength(200, ErrorMessage = "O texto não pode ser superior a 200 Carateres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Data da Atividade Obrigatória.")]
        [Display(Name = "Data da Atividade")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }

        [Required(ErrorMessage = "Descrição da Atividade Obrigatória.")]
        [Display(Name = "Descrição")]
        [StringLength(200, ErrorMessage = "A descrição não pode ser superior a 200 Carateres.")]
        public string Description { get; set; }

        [ForeignKey("Institution")]
        public string InstitutionId { get; set; }

        //[Required(ErrorMessage = "Instituição Obrigatória.")]
        [Display(Name = "Instituição")]
        public virtual Institution Institution { get; set; }

    }
}