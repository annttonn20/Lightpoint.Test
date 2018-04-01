using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lightpoin.Test.Web.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The field can not be empty")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The field can not be empty")]
        public string Description { get; set; }
    }
}
