namespace CompName.ManageStocks.Domain.Geography
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class City
    {
        public long CityId { get; set; }

        public string CityName { get; set; }

        public long? StateId { get; set; }
    }
}