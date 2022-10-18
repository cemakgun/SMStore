namespace SMStore.WebUI.Models
{
    public class AdminLoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

// Class lar da data annotation yerine fluent validation ile veri doğrulaması yapabiliriz.
// Bunun için nuget pm'dan fluentvalidation asp.net core paketine yüklemeliyiz