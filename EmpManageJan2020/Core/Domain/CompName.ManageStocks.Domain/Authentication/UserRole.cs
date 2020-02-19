namespace CompName.ManageStocks.Domain.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class UserRole
    {
        public int UserRoleId { get; set; }

        public int UserId { get; set; }

        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public bool? IsActive { get; set; }

        public DateTime CreatedOn { get; set; }

        public long? CreatedBy { get; set; }

        public string CreatedByUserName { get; set; }

        public DateTime ModifiedOn { get; set; }

        public long? ModifiedBy { get; set; }

        public string ModifiedByUserName { get; set; }
    }
}