using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SMStore.Entities
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Adi"), Required(ErrorMessage = "{0} Alanı Boş Geçilemez!")]
        public string Name { get; set; }
        [Display(Name = "Açıklama")]
        public string? Description { get; set; }
        [Display(Name = "Resim")]
        public string? Image { get; set; }
        [Display(Name = "Aktif?")]
        public bool IsActive { get; set; }
        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        [Display(Name = "Fiyat")]
        public decimal Price { get; set; }
        [Display(Name = "Ürün Kodu")]
        public string? ProductCode { get; set; }
        [Display(Name = "Ürün Stok")]
        public int Stock { get; set; }
        [Display(Name = "Sıra No")]
        public int OrderNo { get; set; }
        [Display(Name = "Ana Sayfa")]
        public bool IsHome { get; set; } // ürün ana sayfadaki gözücek mi? admin den ekleyeceğim
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; } // direk foreign key olduğunu anlıyor.
        [Display(Name = "Kategori")]
        public virtual Category? Category { get; set; } // Product class i ile Category class ı arasında 1 e 1 bir ilişki olduğunu belirttik.
        [Display(Name = "Marka")]
        public int BrandId { get; set; } // direk foreign key olduğunu anlıyor.
        [Display(Name = "Marka")]
        public virtual Brand? Brand { get; set; }
    }
}
