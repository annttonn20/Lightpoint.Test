using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lightpoin.Test.Web.Models
{
    public class AddProductToStoreModel
    {
        [Required(ErrorMessage = "The field can not be empty")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "The field can not be empty")]
        public int StoreId { get; set; }
    }
}
