using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enea.Models
{
    public class EneaUser : IdentityUser
    {
        public ICollection<KeyWord> KeyWords { get; set; }
        public bool HasActiveSubscription { get; set; } = false;

        public DateTime? LastNotificationSent { get; set; } = null;
    }
}
