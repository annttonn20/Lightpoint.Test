using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lightpoint.Test.Data
{
    public class ProductsAndStoresEntity
    {
        [Key]
        [Column(nameof(StoreId))]
        [Required]
        public int StoreId { get; set; }

        [ForeignKey(nameof(StoreId))]
        public StoresEntity Store { get; set; }

        [Required]
        [Key]
        [Column(nameof(ProductId))]
        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public ProductsEntity Product{ get; set; }
    }
}
