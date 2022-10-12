using System.ComponentModel.DataAnnotations;

namespace SMStore.Entities
{
    public class Brand : IEntity 
    {
        public int Id { get; set; }
        [Display(Name = "Adı"), Required(ErrorMessage = "{0} Alanı Boş Geçilemez!")]
        public string Name { get; set; }
        [Display(Name = "Açıklama")]
        public string? Description { get; set; }
        [Display(Name = "Logo")]
        public string? Image { get; set; }
        [Display(Name = "Aktif?")]
        public bool IsActive { get; set; }
        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public virtual ICollection<Product>? Products { get; set; } // Brand ile Product arasında 1 e çok ilişki kurduk.
        public Brand()
        {
            Products = new List<Product>();
        }
    }
}
