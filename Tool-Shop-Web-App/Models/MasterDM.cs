using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tool_Shop_Web_App.Models
{
    public class MasterDM
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool AllowShop { get; set; }
    }
}