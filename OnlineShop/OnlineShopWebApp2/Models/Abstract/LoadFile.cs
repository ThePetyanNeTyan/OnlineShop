using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models.Abstract
{
    public abstract class LoadFile
    {
        [Required(ErrorMessage = "Добавьте изображение продукта ")]
        [Display(Name = "Изображение")]
        public List<IFormFile> UploadedFiles { get; set; } = new();

        public List<string>? ImgPath { get; set; }= new();
    }
}
