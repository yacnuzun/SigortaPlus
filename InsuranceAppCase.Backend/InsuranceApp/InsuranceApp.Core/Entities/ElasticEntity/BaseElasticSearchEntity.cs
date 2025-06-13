using System;

namespace InsuranceApp.Core.Entities
{
    public class BaseElasticSearchEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
    }

}
