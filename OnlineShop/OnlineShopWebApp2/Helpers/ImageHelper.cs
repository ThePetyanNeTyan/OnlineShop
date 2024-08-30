using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Models.Abstract;

namespace OnlineShopWebApp.Helpers
{
    public class ImageHelper
    {
        private readonly IWebHostEnvironment appEnvironment;

        public ImageHelper(IWebHostEnvironment appEnvironment)
        {
            this.appEnvironment = appEnvironment;
        }


        public List<string> Load(LoadFile obj,string folder) 
        {
            if (obj.UploadedFiles != null)
            {
                string productImagesPath = Path.Combine(appEnvironment.WebRootPath + $"/Images/{folder}/");

                if (!Directory.Exists(productImagesPath))
                {
                    Directory.CreateDirectory(productImagesPath);
                }

                foreach(var file in  obj.UploadedFiles) 
                {
                    var fileName = Guid.NewGuid() + "." + file.FileName.Split('.').Last();

                    using (var fileStream = new FileStream(productImagesPath + fileName, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    obj.ImgPath.Add($"/Images/{folder}/" + fileName);
                }
            }

            return obj.ImgPath;
        }

        public List<string> Edit(LoadFile obj, string folder) 
        {
            if (obj.UploadedFiles != null)
            {
                string productImagesPath = Path.Combine(appEnvironment.WebRootPath + $"/Images/{folder}/");

                foreach (var file in obj.UploadedFiles)
                {
                    var fileName = Guid.NewGuid() + "." + file.FileName.Split('.').Last();

                    using (var fileStream = new FileStream(productImagesPath + fileName, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    obj.ImgPath.Add($"/Images/{folder}/" + fileName);
                }
            }
            return obj.ImgPath;
        }
    }
}
