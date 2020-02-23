namespace CompName.ManageStocks.WebAppMVC.Models.Geography
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CountryViewModel
    {
        public long CountryId { get; set; }

        public string ShortName { get; set; }

        public string CountryName { get; set; }

        public int? CountryPhoneCode { get; set; }
    }
}