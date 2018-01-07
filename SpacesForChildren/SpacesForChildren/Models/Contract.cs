﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace SpacesForChildren.Models {

    [Table("Contract")]
    public class Contract {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Data Inicial do Contrato Obrigatória.")]
        [Display(Name = "Data Inicial do Contrato.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? InitialDate { get; set; }

        [Required(ErrorMessage = "Data Final do Contrato Obrigatória.")]
        [Display(Name = "Data Final do Contrato.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "Preço dos Serviços Obrigatório.")]
        [Display(Name = "Preço dos Serviços")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Avaliação tem de ser Numérica.")]
        public int TotalPrice;

        [Display(Name = "Aprovação do Cliente")]
        public EApprovation Approvation { get; set; }    

        //[Required(ErrorMessage = "Obrigatório adicionar Filho.")]
        [Display(Name = "Filho")]
        public virtual Child Child { get; set; }

        //[Required(ErrorMessage = "Obrigatório adicionar Pai.")]
        [Display(Name = "Pai")]
        public virtual Parent Parent { get; set; }

        [Display(Name = "Comentário")]
        public virtual Review Review { get; set; }

        [Display(Name = "Instituição")]
        public virtual Institution Institution { get; set; }

        [ForeignKey("Institution")]
        [Display(Name = "Instituição")]
        public string InstitutionId { get; set; }

        [ForeignKey("Parent")]
        [Display(Name = "Pai")]
        public string ParentId { get; set; }

        [ForeignKey("Child")]
        public int ChildId { get; set; }

        [ForeignKey("Review")]
        [Display(Name = "Comentário")]
        public int ReviewId { get; set; }

        public virtual ICollection<Service> Services { get; set; }

    }
}