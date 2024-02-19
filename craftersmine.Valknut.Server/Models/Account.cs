using System.ComponentModel.DataAnnotations.Schema;

namespace craftersmine.Valknut.Server.Models
{
    public class Account
    {
        [NotMapped]
        public Guid Uuid
        {
            get
            {
                if (string.IsNullOrWhiteSpace(UuidDb))
                    return Guid.Empty;

                return Guid.Parse(UuidDb);
            }
        }
        [Column("uuid")]
        public string UuidDb { get; set; }
        [Column("username")]
        public string Username { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("password")]
        public string EncryptedPassword { get; set; }
    }
}
