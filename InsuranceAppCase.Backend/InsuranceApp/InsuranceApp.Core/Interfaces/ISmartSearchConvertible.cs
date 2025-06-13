using InsuranceApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApp.Core.DTO_s
{
    public interface ISmartSearchConvertible
    {
        BaseElasticSearchEntity ToSmartSearchEntity();
    }
}
