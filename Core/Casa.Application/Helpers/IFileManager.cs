using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Helpers
{
    public interface IFileManager
    {
        string UploadDocuments(IFormFile file, string folderPath);
        string? GetDocumentsFile(string fileName, string folderPath);
        byte[]? GetDocumentsInByte(string fileName, string folderPath);

        string UploadDocumentsBase64ToFile(string base64String, string folderPath);
    }

    public class FileManager : IFileManager
    {
        private readonly IHostingEnvironment _environment;
        public FileManager(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        private string SaveFileToPath(string folderPath, IFormFile postedFile)
        {
            string fileName = $"{Guid.NewGuid()}{new FileInfo(postedFile.FileName).Extension}";
            string fileSaveLocation = $"{folderPath}{fileName}";

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            using (Stream fileStream = new FileStream(fileSaveLocation, FileMode.Create))
            {
                postedFile.CopyTo(fileStream);
            }

            return fileName;
        }

        public byte[]? GetFormatFileFromPath(string fileNameWithExtention)
        {
            byte[]? result = null;
            string imageWithFullPath = $"{_environment.ContentRootPath}\\FormatFiles\\{fileNameWithExtention}";

            if (File.Exists(imageWithFullPath))
            {
                result = File.ReadAllBytes(imageWithFullPath);
            }
            return result;
        }

        public string UploadDocumentsBase64ToFile(string base64String, string folderPath)
        {
            string sFileName = string.Empty;
            try
            {
                string fileName = $"{Guid.NewGuid()}" + ".jpg";
                string fileDirectory = $"{_environment.ContentRootPath}" + folderPath;
                string fileSavePath = $"{_environment.ContentRootPath}" + folderPath + fileName;

                if (!Directory.Exists(fileDirectory))
                {
                    Directory.CreateDirectory(fileDirectory);
                }

                var byteData = Convert.FromBase64String(base64String);
                File.WriteAllBytes(fileSavePath, byteData);

                sFileName = fileName;
            }
            catch (Exception ex)
            {
            }

            return sFileName;
        }

        #region Upload

        public string UploadDocuments(IFormFile file, string folderPath)
        {
            //string folderPath = $"{_environment.ContentRootPath}\\Uploads\\Documents\\";

            string folderSavePath = $"{_environment.ContentRootPath}" + folderPath;
            string fileName = SaveFileToPath(folderSavePath, file);
            return fileName;
        }

        public string? GetDocumentsFile(string fileName, string folderPath)
        {
            //string fileWithFullPath = "\\Uploads\\Documents\\" + fileName;
            string fileWithFullPath = folderPath + fileName;
            return fileWithFullPath;
        }

        public byte[]? GetDocumentsInByte(string fileName, string folderPath)
        {
            byte[]? result = null;
            //  string fileWithFullPath = $"{_environment.ContentRootPath}\\Uploads\\Documents\\{fileName}";
            string fileWithFullPath = $"{_environment.ContentRootPath}" + folderPath + fileName;

            if (File.Exists(fileWithFullPath))
            {
                result = File.ReadAllBytes(fileWithFullPath);
            }

            return result;
        }

        #endregion
    }
}
