using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.EnterpriseServices;
using System.Linq;
using System.Web;

namespace SpacesForChildren.Models {

    [Table("Institution")]
    public class Institution : ApplicationUser {

        [Required(ErrorMessage = "Tipo de Instituição Obrigatório.")]
        [Display(Name = "Tipo de Instituição")]
        public EInstituitionType Type { get; set; }

        [Required(ErrorMessage = "Descrição da Instituição Obrigatório.")]
        [Display(Name = "Descrição da Instituição")]
        [StringLength(200, ErrorMessage = "A descrição não pode ser superior a 200 Carateres.")]
        public string Description { get; set; }

        public bool IsApproved { get; set; }

        public virtual ICollection<Service> Services { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<RequestInfo> Requests { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
    }
}