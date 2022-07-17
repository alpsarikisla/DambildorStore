using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DambildorStore.Models
{
    public class Product
    {
        public int ID { get; set; }

        public int Category_ID { get; set; }
        [ForeignKey("Category_ID")]
        public virtual Category Category { get; set; }

        public int Brand_ID { get; set; }
        [ForeignKey("Brand_ID")]
        public virtual Brand Brand { get; set; }

        [Display(Name = "Ürün Adı")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [StringLength(maximumLength: 100, ErrorMessage = "Bu alan en fazla 100 karkater olmalıdır")]
        public string Name { get; set; }

        [Display(Name = "Ürün Açıklama")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Stok")]
        public decimal Stock { get; set; }

        [Display(Name = "Fiyat")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Display(Name = "Liste Resim")]
        [StringLength(maximumLength: 100)]
        public string CoverImage { get; set; }

        [Display(Name = "Oluşturma Tarihi")]
        public DateTime CreationDay { get; set; }

        [Display(Name = "Satış Durum")]
        public bool SellStatus { get; set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}