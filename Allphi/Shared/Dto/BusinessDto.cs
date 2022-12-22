using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Shared.Dto
{
    public class BusinessDto : EntityDto
    {
        public string Name { get; set; }
        public string Btw { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public bool IsDeleted { get; set; }

        public BusinessDto(Business business)
        {
            Id = business.Id;
            Name = business.Name;
            Btw = business.Btw;
            Email = business.Email;
            Address = business.Address;
            Phone = business.Phone;
            IsDeleted = business.IsDeleted;
        }

        public BusinessDto()
        {
        }
    }
}