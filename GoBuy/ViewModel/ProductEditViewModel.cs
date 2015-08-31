using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoBuy.Models;
using System.Web.Mvc;
namespace GoBuy.ViewModel
{
    public class ProductEditViewModel
    {
        //public int ProductId { get; set; }
        //public string Name { get; set; }
        //public string Price { get; set; }
        //public Nullable<int> CategoryId { get; set; }
        //public string ProductUrl { get; set; }
        
        //public virtual Category Category { get; set; }
        public Product Product { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}