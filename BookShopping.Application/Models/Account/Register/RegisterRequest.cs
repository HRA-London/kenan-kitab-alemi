using System;
using System.ComponentModel.DataAnnotations;

namespace BookShopping.Application.Models.Account.Register
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Ad bos qala bilmez.")]
        [MaxLength(100, ErrorMessage = "Ad maximum 100 uzunlugunda ola biler")]
        [MinLength(3, ErrorMessage = "Ad minimum 3 uzunlugunda ola biler")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyad bos qala bilmez.")]
        [MaxLength(100, ErrorMessage = "Soyad maximum 100 uzunlugunda ola biler")]
        [MinLength(3, ErrorMessage = "Soyad minimum 3 uzunlugunda ola biler")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Email bos qala bilmez.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Sifre bos qala bilmez.")]
        [MinLength(6, ErrorMessage = "Sifre minimum 6 uzunlugunda ola biler")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Tekrar sifre bos qala bilmez.")]
        [Compare("Password", ErrorMessage = "Tekrar sifre , sifre ile eyni deyil")]
        public string ConfirmPassword { get; set; }
    }
}

