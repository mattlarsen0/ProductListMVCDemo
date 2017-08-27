using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace ProductListMVCDemo.Objects.Products
{
    public abstract class ProductBase
    {
        [Key]
        public int ProductID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool Removed { get; set; }

        public virtual string GetAdditionalInformation()
        {
            return string.Empty;
        }
    }
}