using Core.Utilities.FileHelper.Abstract;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Core.Utilities.FileHelper.Concrete
{
    public class FileHelper : IFileHelper
    {
        public IResult Delete(string fileName, string folder)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), folder, fileName);

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            return new SuccessResult();
        }

        public async Task<IDataResult<string>> Upload(IFormFile file, string folder)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string path = Path.Combine(Directory.GetCurrentDirectory(), folder, fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            };

            return new SuccessDataResult<string>(fileName);
        }
    }
}