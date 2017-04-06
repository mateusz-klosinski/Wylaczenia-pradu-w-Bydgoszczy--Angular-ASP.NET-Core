using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enea.ViewModels
{
    public class SettingsViewModel
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string OldPassword { get; set; }
        public string Password { get; set; }
    }
}
