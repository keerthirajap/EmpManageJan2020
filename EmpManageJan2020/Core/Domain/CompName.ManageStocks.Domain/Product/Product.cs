namespace CompName.ManageStocks.Domain.Product
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Product
    {
        public long ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public decimal? ProductPrice { get; set; }

        public byte[] ProductImage { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedOn { get; set; }

        public long CreatedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public long ModifiedBy { get; set; }
    }
}