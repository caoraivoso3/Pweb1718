using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SpacesForChildren.Models {

    [Table("Parent")]
    public class Parent : ApplicationUser{

        [Required(ErrorMessage = "Género Obrigatório.")]
        [Display(Name = "Género")]
        public EGender Gender { get; set; }

        public virtual ICollection<Child> Childrens { get; set; }
        public virtual ICollection<RequestInfo> Requests { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
    }
}