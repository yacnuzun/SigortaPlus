using System;
using System.IO;
using System.Threading.Tasks;

namespace InsuranceApp.Core.Helpers.FileHelper
{
    public interface IFileHelper
    {
        Task<object> DeserializeAsync(Stream file, Type targetType, string contentType);

    }
}
