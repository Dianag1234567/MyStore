using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Models
{
    public class ProductModel
    {
        //only relevant data
        public int Productid { get; set; }
        [Required]
        [MinLength(4)]
        public string Productname { get; set; }
        public int Supplierid { get; set; }
        public int Categoryid { get; set; }
        [Required]
        public decimal? Unitprice { get; set; }
        public bool Discontinued { get; set; }
    }
}
