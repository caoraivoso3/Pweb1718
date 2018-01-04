using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.EnterpriseServices;
using System.Linq;
using System.Web;

namespace SpacesForChildren.Models {

    public enum EInstituitionType {
        Public,
        Private,
        IPSS
    };

    [Table("Institution")]
    public class Institution : ApplicationUser {
        public Institution() {
            //Services = new HashSet<Service>();
        }


        [Required]
        public EInstituitionType Type { get; set; }

        [Required]
        public string Acronym { get; set; }

        [Required]
        public string Description { get; set; }

        public bool IsApproved { get; set; }

        [Required]
        public virtual Local Local { get; set; }

        [ForeignKey("Local")]
        public virtual int LocalId { get; set; }


        public virtual ICollection<Service> Services { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<RequestInfo> Requests { get; set; }
    }
}