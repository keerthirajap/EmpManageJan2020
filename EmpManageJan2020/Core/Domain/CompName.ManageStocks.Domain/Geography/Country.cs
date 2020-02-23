namespace CompName.ManageStocks.Domain.Geography
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Country
    {
        public long CountryId { get; set; }

        public string ShortName { get; set; }

        public string CountryName { get; set; }

        public int? CountryPhoneCode { get; set; }
    }
}