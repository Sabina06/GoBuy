using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoBuy.Models;
namespace GoBuy.ViewModel
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string ProductUrl { get; set; }
        
        public virtual Category Category { get; set; }
    }
}