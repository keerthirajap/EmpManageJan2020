namespace CompName.ManageStocks.Domain.Geography
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class State
    {
        public long StateId { get; set; }

        public string StateName { get; set; }

        public long? CountryId { get; set; }
    }
}