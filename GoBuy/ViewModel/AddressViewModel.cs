using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoBuy.Models;
namespace GoBuy.ViewModel
{
    public class AddressViewModel
    {
        public IEnumerable<Country> country { get; set; }
        public IEnumerable<City> city { get; set; }
        public virtual Country Country { get; set; }
        public virtual City City { get; set; }
    }
}