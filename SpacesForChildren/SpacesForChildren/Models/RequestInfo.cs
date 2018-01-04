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

        [Required]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }

        public bool IsAnswered {get; set; }

        public string Answer { get; set; }

        [Required]
        public virtual Parent Parent { get; set; }

        [Required]
        public virtual Institution Institution { get; set; }

        [ForeignKey("Parent")]
        public string ParentId { get; set; }

        [ForeignKey("Institution")]
        public string InstitutionId { get; set; }
    }
}