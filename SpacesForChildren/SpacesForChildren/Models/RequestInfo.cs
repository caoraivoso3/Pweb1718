using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SpacesForChildren.Models {
    public class RequestInfo {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Titulo do Pedido Obrigatório.")]
        [Display(Name = "Titulo do Pedido")]
        [StringLength(40, ErrorMessage = "O nome não pode ser superior a 40 Carateres.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Texto do Pedido Obrigatório.")]
        [Display(Name = "Texto")]
        [StringLength(200, ErrorMessage = "O texto não pode ser superior a 200 Carateres.")]
        public string Text { get; set; }

        [Display(Name = "Respondido")]
        public bool IsAnswered {get; set; }

        [Display(Name = "Resposta do pedido")]
        [StringLength(200, ErrorMessage = "O resposta não pode ser superior a 200 Carateres.")]
        public string Answer { get; set; }

        //[Required(ErrorMessage = "Pai Obrigatória.")]
        [Display(Name = "Pai")]
        public virtual Parent Parent { get; set; }

        //[Required(ErrorMessage = "Instituição Obrigatória.")]
        [Display(Name = "Instituição")]
        public virtual Institution Institution { get; set; }

        [ForeignKey("Parent")]
        public string ParentId { get; set; }

        [ForeignKey("Institution")]
        public string InstitutionId { get; set; }
    }
}