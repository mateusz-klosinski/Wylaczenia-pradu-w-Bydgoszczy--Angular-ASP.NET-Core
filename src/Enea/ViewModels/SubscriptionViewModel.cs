using Enea.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enea.ViewModels
{
    public class SubscriptionViewModel
    {
        public ICollection<KeyWord> KeyWords { get; set; }

        [Required]
        public bool HasActiveSubscription { get; set; }

        public DateTime? LastNotificationSent { get; set; }
    }
}
