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

        [Display(Name = "Nome do Serviço")]
        [Required(ErrorMessage = "Nome do Serviço Obrigatório.")]
        [StringLength(40, ErrorMessage = "O nome não pode ser superior a 40 Carateres.")]
        public string Name { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Descrição do Serviço Obrigatório.")]
        [StringLength(200, ErrorMessage = "A descrição não pode ter mais de 200 Carateres.")]
        public string Description { get; set; }

        [Display(Name = "Idade Mínima")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Idade tem de ser Numérica.")]
        [Required(ErrorMessage = "Idade Mínima Obrigatória.")]
        public int MinAgeYear { get; set; }

        [Display(Name = "Idade Máxima")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Idade tem de ser Numérica.")]
        [Required(ErrorMessage = "Idade Máxima Obrigatória.")]
        public int MaxAgeYear { get; set; }


        public virtual ICollection<Institution> Institutions { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
    }
}