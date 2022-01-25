using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PayrollServer.Extensions
{
    public static class FormFileExtensions
    {
        public static async Task<byte[]> GetBytes(this IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                if(formFile == null)
                {
                    return null;
                }
                await formFile.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}