using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace InsuranceApp.Core.Helpers.FileHelper
{
    public class FileHelper : IFileHelper
    {
        public async Task<object> DeserializeAsync(Stream file, Type targetType, string contentType)
        {
            if (contentType.Equals("application/json", StringComparison.OrdinalIgnoreCase))
            {
                using (var reader = new StreamReader(file))
                {
                    var jsonContent = await reader.ReadToEndAsync();
                    return JsonConvert.DeserializeObject(jsonContent, targetType);
                }
            }
            else if (contentType.Equals("application/xml", StringComparison.OrdinalIgnoreCase) ||
                     contentType.Equals("text/xml", StringComparison.OrdinalIgnoreCase))
            {
                var serializer = new XmlSerializer(targetType);
                return serializer.Deserialize(file);
            }
            else
            {
                throw new NotSupportedException($"Content type '{contentType}' is not supported.");
            }
        }

    }
}
