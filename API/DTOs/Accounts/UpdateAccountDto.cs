namespace API.DTOs.Accounts
{
    public class UpdateAccountDto
    {
        public Guid Guid { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
