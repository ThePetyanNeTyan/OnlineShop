using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class Registration
    {

        [Required(ErrorMessage = "Введите Email")]
        [EmailAddress(ErrorMessage = "Некорректный электронный адрес")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите ваше имя")]
        [StringLength(20, MinimumLength = 1)]
        public string Name { get; set; }


        [Required(ErrorMessage = "Введите ваш возраст")]
        [Range(7, 100, ErrorMessage = "Диапазон возраста {1}-{2} ")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Введите ваш номер телефона")]
        [RegularExpression(@"^\+[7]\d{10}$", ErrorMessage = "Формат номера телефона +79xxxxxxxxx")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Возможный диапазон пароля {2}-{1}")]
        public string PasswordUser { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [Compare("PasswordUser", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        public string? ReturnUrl { get; set; }

    }
}
