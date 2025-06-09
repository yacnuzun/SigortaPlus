using InsuranceApp.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApp.Core.DTO_s
{
    //Kullanıcı, yaş, cinsiyet ve hastalık geçmişine
    public class OfferRequestDto:IDto
    {
        public string FullName { get; set; }
        public int Age { get; set; }
        public bool Gender { get; set; }
    }
}
