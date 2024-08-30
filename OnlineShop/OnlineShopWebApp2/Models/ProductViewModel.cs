using OnlineShopWebApp.Models.Abstract;
using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class ProductViewModel : LoadFile
    {
     
        [Display(Name="Id")]
        public  Guid Id{ get;  set; }
        [Required(ErrorMessage = "Введите название продукта ")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Диапазон количество символов может быть от{2}-{1}")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите цену продукта ")]
        [Range(200,1000, ErrorMessage = "Цена продукта возможна в диапазоне {1}-{2}")]
        [Display(Name = "Цена")]
        public decimal Cost { get; set; }

        [Required(ErrorMessage = "Введите описание продукта ")]
        [StringLength(60 ,MinimumLength = 20, ErrorMessage = "Диапазон количество символов может быть от{2}-{1}")]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Введите значение бонуса продукта ")]
        [Range(20, 100, ErrorMessage="Диапазон бонуса может быть от{1}-{2}")]
        [Display(Name = "Бонус")]
        public int Bonus { get; set; }

        
     
    }
}
