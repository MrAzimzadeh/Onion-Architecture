using System.Text.RegularExpressions;

namespace Ecomerce.Infrastructure.Services.Storage
{
    public class Storage
    {
        // Fayl adını dəyişdirmək üçün istifadə olunan metod
        protected delegate bool HasFile(string pathOrContainerName, string fileName);

        // Fayl adını dəyişdirmək üçün istifadə olunan üsullar
        protected async Task<string> FileRenameAsync(string pathOrContainerName, string fileName, HasFile hasFileMethod, bool first = true)
        {
            
            // Path - C:\Users\azimz\OneDrive\Desktop\onion_architecture\Ecommerce\Presentation\Ecommerce.API\wwwroot\{pathOrContainerName}
            // FileName - 3415007.jpg
            // hasFileMethod - HasFile
            string newFileName = await Task.Run(() =>
            {
                // Faylın uzantısını 
                // extension - .jpg
                string extension = Path.GetExtension(fileName);
                // Faylın adını almaq
                // baseFileName - (FileName)
                string baseFileName = Path.GetFileNameWithoutExtension(fileName);

                int fileNo = 2;

                if (!first)
                {
                    // Fayl adındakı rəqəmi çıxartmaq üçün 
                    Match match = Regex.Match(baseFileName, @"-(\d+)$");

                    // match.Groups[1].Value = 2
                    // parsedFileNo = 2
                    if (match.Success && int.TryParse(match.Groups[1].Value, out int parsedFileNo))
                    {
                        fileNo = parsedFileNo + 1;
                    }
                }

                // Yeni fayl  
                // newFileNameWithoutExtension - 3415007-2
                string newFileNameWithoutExtension = $"{baseFileName}-{fileNo}";
                // newFileNameWithExtension - 3415007-2.jpg
                string newFileNameWithExtension = $"{newFileNameWithoutExtension}{extension}";

                // Əgər yeni fayl adı artıq mövcuddursa adı yenidən təyin etmə prosesini təkrarla (Recursive fun    ction)
                if (hasFileMethod(pathOrContainerName, newFileNameWithExtension))
                {
                    // pathOrContainerName - C:\Users\azimz\OneDrive\Desktop\onion_architecture\Ecommerce\Presentation\Ecommerce.API\wwwroot\{pathOrContainerName}
                    // newFileNameWithExtension - 3415007-2.jpg  
                    // hasFileMethod - HasFile
                    return FileRenameAsync(pathOrContainerName, newFileNameWithExtension, hasFileMethod, false).Result;
                }

                return newFileNameWithExtension;
            });

            return newFileName;
        }
    }
}
