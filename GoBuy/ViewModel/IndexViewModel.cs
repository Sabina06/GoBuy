using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoBuy.Models;
namespace GoBuy.ViewModel
{
    public class IndexViewModel
    {
        public IEnumerable<Product> product { get; set; }
        public IEnumerable<Member> member { get; set; }
        public IEnumerable<Category> category { get; set; }
    

    }
    
}