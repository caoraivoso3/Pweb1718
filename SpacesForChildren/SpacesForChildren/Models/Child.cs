using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SpacesForChildren.Models {
    public class Child {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome Obrigatório.")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Género Obrigatório.")]
        [Display(Name = "Género")]
        public EGender Gender { get; set; }

        [Required(ErrorMessage = "Data de Nascimento Obrigatória.")]
        [Display(Name = "Data de nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }

        [ForeignKey("Parent")]
        public string ParentId { get; set; }

        //[Required]
        [Display(Name = "Pai")]
        public virtual Parent Parent { get; set; }

        public virtual ICollection<Contract> Contracts { get; set; }
    }
}