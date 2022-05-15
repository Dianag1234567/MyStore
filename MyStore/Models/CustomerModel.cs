using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Models
{
    public class CustomerModel
    {
        public int Custid { get; set; }
        [Required]
        public string Companyname { get; set; }
        [Required]
        public string Contactname { get; set; }
        public string Contacttitle { get; set; }
        [Required]
        [MinLength(4)]
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Postalcode { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        public string Fax { get; set; }
    }
}
