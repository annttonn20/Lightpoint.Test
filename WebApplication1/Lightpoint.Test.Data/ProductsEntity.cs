using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lightpoint.Test.Data
{
    [Table(nameof(ProductsEntity))]
    public class ProductsEntity
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(nameof(Id))]
        public int Id { get; set; }

        [Required]
        [Column(nameof(Name))]
        public string Name { get; set; }

        [Required]
        [Column(nameof(Description))]
        public string Description { get; set; }


        [ForeignKey("ProductId")]
        public IEnumerable<ProductsAndStoresEntity> ProductStore { get; set; }

    }
}
