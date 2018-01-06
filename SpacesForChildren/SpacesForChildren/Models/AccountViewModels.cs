using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpacesForChildren.Models
{
    public enum EGender {
        Masculino,
        Feminino
    }

    public enum EInstituitionType {
        Public,
        Private,
        IPSS
    }

    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Nome obrigatório.")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Morada obrigatória.")]
        [Display(Name = "Morada")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Cidade obrigatória.")]
        [Display(Name = "Cidade")]
        public string City { get; set; }

        [Required(ErrorMessage = "Nif obrigatório.")]       
        [Display(Name = "NIF")]
        public int NIF { get; set; }

        [Required(ErrorMessage = "Telefone obrigatório.")]
        [Display(Name = "Telefone")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$", ErrorMessage = "Formato de telefone inserido invalido.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email obrigatório.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Palavra-Passe obrigatória.")]
        [StringLength(100, ErrorMessage = "A {0} tem de ter pelo menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Palavra-Passe")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirma a Palavra-Passe")]
        [Compare("Password", ErrorMessage = "As palvras-passe não correspondem")]
        public string ConfirmPassword { get; set; }

        //Account type
        public string Profile { get; set; }


        //Institution Fields
        //[Required(ErrorMessage = "Tipo de Instituição obrigatória.")]
        [Display(Name = "Tipo de Instituição")]
        public EInstituitionType Type { get; set; }

        //[Required(ErrorMessage = "Descrição obrigatória.")]
        [Display(Name = "Descrição")]
        public string Description { get; set; }

        //Parent Fields
        //[Required(ErrorMessage = "Género obrigatório.")]
        [Display(Name = "Género")]
        public EGender Gender { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
