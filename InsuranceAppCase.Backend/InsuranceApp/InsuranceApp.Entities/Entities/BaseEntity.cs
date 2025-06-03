﻿using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApp.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
    }

}
