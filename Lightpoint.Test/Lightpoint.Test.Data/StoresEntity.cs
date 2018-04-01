using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lightpoint.Test.Data
{
    [Table(nameof(StoresEntity))]
    public class StoresEntity
    {
        [Required]
        [Column(nameof(Id))]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(nameof(Name))]
        public string Name { get; set; }

        [Required]
        [Column(nameof(Address))]
        public string Address { get; set; }

        [Required]
        [Column(nameof(WorkingHours))]
        public string WorkingHours { get; set; }


        [ForeignKey("StoreId")]
        public ICollection<ProductsAndStoresEntity> StoreProduct { get; set; }
    }
}
