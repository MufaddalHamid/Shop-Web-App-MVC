using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tool_Shop_Web_App.Models
{
    public class CartDM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string WhatsappNumber { get; set; }
        public bool Whatsapp { get; set; }
        public bool Promotional { get; set; }
    }

}