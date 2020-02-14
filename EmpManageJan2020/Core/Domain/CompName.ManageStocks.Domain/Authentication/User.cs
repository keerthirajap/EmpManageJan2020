namespace CompName.ManageStocks.Domain.Authentication
{
    using System;

    public class User
    {
        public long UserId { get; set; }

        public string UserName { get; set; }

        public short? UserTitleId { get; set; }

        public string UserTitleDesc { get; set; }

        public string FullName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailId { get; set; }

        public short? UserGenderId { get; set; }

        public string UserGenderDesc { get; set; }

        public string Password { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }

        public bool IsActive { get; set; }

        public bool IsLocked { get; set; }

        public DateTime CreatedOn { get; set; }

        public long CreatedBy { get; set; }

        public string CreatedByUserName { get; set; }

        public DateTime ModifiedOn { get; set; }

        public long ModifiedBy { get; set; }

        public string ModifiedByUserName { get; set; }
    }
}