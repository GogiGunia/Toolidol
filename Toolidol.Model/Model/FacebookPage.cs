using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolidol.Model.Model
{
    public class FacebookPage
    {
        public int Id { get; set; } 
        public required string FacebookPageId { get; set; }
        public required string Name { get; set; }
        public required string EncryptedAccessToken { get; set; }

        // Foreign Key relationship to your User model
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
