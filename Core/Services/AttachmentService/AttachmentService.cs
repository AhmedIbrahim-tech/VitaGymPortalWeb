using Microsoft.AspNetCore.Http;

namespace Core.Services.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        public AttachmentService(IWebHostEnvironment webHost )
        {
            _webHost = webHost;
        }

        private readonly string[] _allowedExtensions = { ".jpg",".png",".jpeg"};
        private readonly long _maxFileSize = 5 * 1024 * 1024;
        private readonly IWebHostEnvironment _webHost;

        public string? Upload(string folderName, IFormFile file)
        {
            try
            {
                if (folderName == null || file == null || file.Length == 0) return null;

                if (file.Length > _maxFileSize) return null;

                var extension = Path.GetExtension(file.FileName).ToLower();
                if (!_allowedExtensions.Contains(extension)) return null;

                var folderPath = Path.Combine(_webHost.WebRootPath, "images", folderName);
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var fileName = Guid.NewGuid().ToString() + extension;

                var filePath = Path.Combine(folderPath, fileName);

                using var fileStream = new FileStream(filePath, FileMode.Create);
                file.CopyTo(fileStream);

                return fileName;
            }
            catch( Exception ex ) 
            {
                Console.WriteLine( "Failed To Create "+ ex.ToString() );
                return null;
            }
          
        }
        public bool Delete(string fileName, string folderName)
        {
           try
            {
                if(string.IsNullOrEmpty(fileName)|| string.IsNullOrEmpty(folderName))
                    return false;

                var filePath = Path.Combine(_webHost.WebRootPath, "images", folderName,fileName);
                if(File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }
                return false;

            }
            catch ( Exception ex )
            {
                Console.WriteLine( "Failed To Delete "+ ex.ToString() );
                return false;
            }
        }

    }
}
