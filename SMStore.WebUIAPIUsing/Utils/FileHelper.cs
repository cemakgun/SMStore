using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Drawing;

namespace SMStore.WebUIAPIUsing.Utils
{
    public class FileHelper
    {
        public static async Task<string> FileLoaderAsync(IFormFile formFile, string filePath = "/wwwroot/Img/")
        {
            string fileName = ""; // yüklenecek dosya adı için değişken oluşturduk.

            fileName = formFile.FileName; // oluşturduğumuz değişkene yüklenecek dosya adını aktardık

            string directory = Directory.GetCurrentDirectory() + filePath + fileName; // dosyanın yükleneceği dizin belirledik (GetCurrentDirectory metodu uygulamın çalıştığı fiziksel yolu getirir)

            using var stream = new FileStream(directory, FileMode.Create); // dosya yükleme için gerekli bir dosya akış nesnesi oluşturup sınıfa yükleme yapacağımız dizini(directory) ve yükleme tipimizi(yeni dosya oluşturma)

            await formFile.CopyToAsync(stream); // yukardaki ayarlarla dosyamızı asenkron bir şekilde sunucuya yükledik.

            return fileName; // bu metodun kullanılacağı yere yüklenen dosya adını geri gönderdik
        }
        public static bool FileRemover(string fileName, string filePath = "/wwwroot/Img/")
        {
          
            string directory = Directory.GetCurrentDirectory() + filePath + fileName;

            if (File.Exists(directory)) // File.Exists metodu C# ta var olan ve kendisine verilen adresteki dosya var mi yok mu kontrol ediyor
            {
                    File.Delete(directory); // file.delete metodu verilen adresteki dosyayı sunucudan siler.
                    return true;
            }
            return false;
        }

    }
}
