using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tool_Shop_Web_App.Models
{
    public class ItemDM
    {
        public Guid Id { get; set; }
        public int SerNo { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool IsFreezed { get; set; }
        public byte[] Image { get; set; }

        public List<DropDown> Categories { get; set; }
    }

}