using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DambildorStore.Models
{
    public class ProductImage
    {
        public int ID { get; set; }

        public int Product_ID { get; set; }

        [ForeignKey("Product_ID")]
        public virtual Product Product { get; set; }

        public string ImagePath { get; set; }
    }
}