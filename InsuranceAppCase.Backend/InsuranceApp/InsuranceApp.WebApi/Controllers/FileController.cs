using InsuranceApp.Business.Services.Interfaces;
using InsuranceApp.Core.Helpers.FileHelper;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace InsuranceApp.WebApi.Controllers
{
    public class FileController : ApiController
    {
        private readonly IFileHelper _fileService;

        public FileController(IFileHelper fileService)
        {
            _fileService = fileService;
        }

        [HttpPost]
        [System.Web.Http.Route("fileUpload")]
        public async Task<IHttpActionResult> UploadFile()
        {
            var httpRequest = HttpContext.Current.Request;

            if (httpRequest.Files.Count == 0)
            {
                return BadRequest("Dosya veya tür adı eksik.");
            }

            try
            {
                var file = httpRequest.Files["file"]; // form-data'da "file" key'i
                var typeName = httpRequest.Form["typeName"];

                var targetType = AppDomain.CurrentDomain.GetAssemblies()
    .SelectMany(a => a.GetTypes())
    .FirstOrDefault(t => t.Name.Equals(typeName, StringComparison.OrdinalIgnoreCase));

                if (targetType == null)
                {
                    return BadRequest("Belirtilen tür bulunamadı.");
                }

                var contentType = file.ContentType;
                var result = await _fileService.DeserializeAsync(file.InputStream, targetType, contentType);

                // İşlem başarılı, sonucu döndür
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                return InternalServerError(ex);
            }
        }


        public async Task<IHttpActionResult> AddJsonFile(HttpPostedFileBase file)
        {
            return Ok();
        }
    }
    public class FakeDataController:ApiController
    {
        private readonly IFakeDataService _fakeService;

        public FakeDataController(IFakeDataService fakeService)
        {
            _fakeService = fakeService;
        }

        [HttpPost]
        public async Task<IHttpActionResult> SetFakeDatas()
        {
            var result = await _fakeService.GenerateAsync();
            if (!result.Success)
            {

            }
            return Ok();
        }
    }
}
