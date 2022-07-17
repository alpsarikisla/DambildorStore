using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DambildorStore.Models
{
    public class Category
    {
        public int ID { get; set; }

        [Display(Name="Kategori Adı")]
        [Required(ErrorMessage ="Bu alan zorunludur")]
        [StringLength(maximumLength:50, ErrorMessage = "Bu alan en fazla 50 karakter olmalıdır")]
        public string Name { get; set; }

        [Display(Name = "Açıklama")]
        public string Description { get; set; }

        [Display(Name = "Durum")]
        public bool Status { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}