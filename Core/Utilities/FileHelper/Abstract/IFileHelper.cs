using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Core.Utilities.FileHelper.Abstract
{
    public interface IFileHelper
    {
        Task<IResult> Upload(IFormFile file, string folder);
        IResult Delete(string fileName, string folder);
    }
}