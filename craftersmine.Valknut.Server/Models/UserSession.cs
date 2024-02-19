using System.ComponentModel.DataAnnotations.Schema;
using Guid = System.Guid;

namespace craftersmine.Valknut.Server.Models
{
    public class UserSession
    {
        [NotMapped]
        public Guid ClientToken
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ClientTokenDb))
                    return Guid.Empty;

                return Guid.Parse(ClientTokenDb);
            }
        }

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
        
        [Column("clientToken")]
        public string ClientTokenDb { get; set; }
        [Column("sessionId")]
        public string SessionId { get; set; }
        [Column("serverId")]
        public string ServerId { get; set; }
        [Column("uuid")]
        public string UuidDb { get; set; }
    }
}
