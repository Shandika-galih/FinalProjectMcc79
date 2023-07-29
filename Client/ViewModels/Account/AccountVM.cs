using System.ComponentModel.DataAnnotations;

namespace Client.ViewModels.Account
{
    public class AccountVM
    {
        public Guid Guid { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }  
    }
}
