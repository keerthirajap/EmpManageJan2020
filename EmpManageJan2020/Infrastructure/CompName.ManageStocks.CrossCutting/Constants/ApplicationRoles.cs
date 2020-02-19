namespace CompName.ManageStocks.CrossCutting.Constants
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    [ExcludeFromCodeCoverage]
    public static class ApplicationRoles
    {
        public const string BasicUser = "BasicUser";

        public const string Administrator = "Administrator";

        public const string Manager = "Manager";

        public const string Approver = "Approver";
    }
}