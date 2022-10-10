
using System.ComponentModel.DataAnnotations;

namespace SMStore.Entities
{
    public class Customer : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Adı"), Required(ErrorMessage = "{0} Alanı Boş Geçilemez!")]
        public string Name { get; set; }
        [Display(Name = "Soyadı"), Required(ErrorMessage = "{0} Alanı Boş Geçilemez!")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "{0} Alanı Boş Geçilemez!"), EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Telefon")]
        public string? Phone { get; set; }
        [Display(Name = "Şifre"), Required(ErrorMessage = "{0} Alanı Boş Geçilemez")]
        public string Password { get; set; }
        [Display(Name = "Aktif?")]
        public bool IsActive { get; set; }
        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
    }
}
