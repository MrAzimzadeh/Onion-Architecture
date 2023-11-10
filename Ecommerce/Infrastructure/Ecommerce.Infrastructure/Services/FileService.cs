// using System.Runtime.CompilerServices;
// using Ecommerce.Infrastructure.StaticService;
// using Ecomerce.Application.Services;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.AspNetCore.Http;
//
// namespace EEcomerce.InfrastructureServices;
//
// public class FileService : IFileService
// {
//     readonly IWebHostEnvironment _webHostEnvironment;
//
//     public FileService(IWebHostEnvironment webHostEnvironment)
//     {
//         _webHostEnvironment = webHostEnvironment;
//     }
//
//     public async Task<bool> CopyFileAsync(string path, IFormFile file)
//     {
//         try
//         {
//             await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None,
//                 1024 * 1024, useAsync: false);
//
//             await file.CopyToAsync(fileStream);
//             await fileStream.FlushAsync();
//             return true;
//         }
//         catch (Exception ex)
//         {
//             //todo log!
//             throw ex;
//         }
//     }
//
//     async Task<string> FileRenameAsync(string path, string fileName, bool first = true)
//     {
//         string newFileName = await Task.Run<string>(async () =>
//         {
//             string extension = Path.GetExtension(fileName);
//             string newFileName = string.Empty;
//             if (first)
//             {
//                 string oldName = Path.GetFileNameWithoutExtension(fileName);
//                 newFileName = $"{NameService.CharacterRegulatory(oldName)}{extension}";
//             }
//             else
//             {
//                 newFileName = fileName;
//                 int indexNo1 = newFileName.IndexOf("-");
//                 if (indexNo1 == -1)
//                     newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
//                 else
//                 {
//                     int lastIndex = 0;
//                     while (true)
//                     {
//                         lastIndex = indexNo1;
//                         indexNo1 = newFileName.IndexOf("-", indexNo1 + 1);
//                         if (indexNo1 == -1)
//                         {
//                             indexNo1 = lastIndex;
//                             break;
//                         }
//                     }
//
//                     int indexNo2 = newFileName.IndexOf(".");
//                     string fileNo = newFileName.Substring(indexNo1 + 1, indexNo2 - indexNo1 - 1);
//
//                     if (int.TryParse(fileNo, out int _fileNo))
//                     {
//                         _fileNo++;
//                         newFileName = newFileName.Remove(indexNo1 + 1, indexNo2 - indexNo1 - 1)
//                             .Insert(indexNo1 + 1, _fileNo.ToString());
//                     }
//                     else
//                         newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
//                 }
//             }
//
//             if (File.Exists($"{path}\\{newFileName}"))
//                 return await FileRenameAsync(path, newFileName, false);
//             else
//                 return newFileName;
//         });
//
//         return newFileName;
//     }
//
//     public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files)
//     {
//         string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);
//         if (!Directory.Exists(uploadPath))
//             Directory.CreateDirectory(uploadPath);
//
//         List<(string fileName, string path)> datas = new();
//         List<bool> results = new();
//         foreach (IFormFile file in files)
//         {
//             string fileNewName = await FileRenameAsync(uploadPath, file.FileName);
//
//             bool result = await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);
//             datas.Add((fileNewName, $"{path}\\{fileNewName}"));
//             results.Add(result);
//         }
//
//         if (results.TrueForAll(r => r.Equals(true)))
//             return datas;
//
//         return null;
//
//         //todo Eğer ki yukarıdaki if geçerli değilse burada dosyaların sunucuda yüklenirken hata alındığına dair uyarıcı bir exception oluşturulup fırlatılması gerekyior!
//     }
// }

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Ecomerce.Application.Services;
    using Ecomerce.Infrastructure.StaticService;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;

    namespace Ecomerce.Infrastructure.Services
    {
        public class FileService : IFileService

        {
            private readonly IWebHostEnvironment _webHostEnvironment;

            public FileService(IWebHostEnvironment webHostEnvironment)
            {
                _webHostEnvironment = webHostEnvironment;
            }


                public async Task<bool> CopyFileAsync(string path, IFormFile file)
                {
                    try
                    {
                        using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024,
                            useAsync: false);
                        await file.CopyToAsync(fileStream);
                        await fileStream.FlushAsync();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        throw ex;
                    }
                }

            public async Task<(string fileName, string path)> UploadAsync(string path, IFormFile file)
            {
                string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);
                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                string fileNewName = await GenerateNewFileNameAsync(uploadPath, file.FileName);

                bool result = await CopyFileAsync(Path.Combine(uploadPath, fileNewName), file);

                if (result)
                {
                    return (fileNewName, Path.Combine(path, fileNewName));
                }

                // todo IResult  
                throw new Exception("Error");
            }

            public (string fileName, string path) Upload(string path, IFormFile file)
            {
                string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);
                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                string fileNewName =
                    GenerateNewFileNameAsync(uploadPath, file.FileName).Result; // Eş zamanlı işlemi bekler.

                bool result = CopyFileAsync(Path.Combine(uploadPath, fileNewName), file)
                    .Result; // Eş zamanlı işlemi bekler.

                if (result)
                {
                    return (fileNewName, Path.Combine(path, fileNewName));
                }


                // todo Error (Iresult )
                throw new Exception("Error");
            }

            public async Task<List<(string fileName, string path)>> UploadRangeAsync(string path, IFormFileCollection files)
            {
                string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);
                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                List<(string fileName, string path)> datas = new List<(string fileName, string path)>();

                foreach (IFormFile file in files)
                {
                    string fileNewName = await GenerateNewFileNameAsync(uploadPath, file.FileName);

                    bool result = await CopyFileAsync(Path.Combine(uploadPath, fileNewName), file);

                    if (result)
                    {
                        datas.Add((fileNewName, Path.Combine(path, fileNewName)));
                    }
                    else
                    {
                        Console.WriteLine("Error");
                    }
                }

                return datas;
            }

            private async Task<string> GenerateNewFileNameAsync(string path, string fileName)
            {
                string extension = Path.GetExtension(fileName);
                string nameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                string baseFileName = NameService.CharacterRegulatory(nameWithoutExtension);
                string newFileName = $"{baseFileName}{extension}";

                int index = 2;

                while (File.Exists(Path.Combine(path, newFileName)))
                {
                    newFileName = $"{baseFileName}-{index++}{extension}";
                }

                return newFileName;
            }

            public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files)
            {
                string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);
                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                List<(string fileName, string path)> datas = new();
                List<bool> results = new();

                foreach (IFormFile file in files)
                {
                    string fileNewName = await GenerateNewFileNameAsync(uploadPath, file.FileName);
                    bool result = await CopyFileAsync(Path.Combine(uploadPath, fileNewName), file);
                    datas.Add((fileNewName, Path.Combine(path, fileNewName)));
                    results.Add(result);
                }

                if (results.All(r => r))
                    return datas;

                return null;
            }
        }
}