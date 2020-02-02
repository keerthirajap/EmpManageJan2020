namespace EmpManage.WebAppMVC.Models
{
    using System;

    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public DateTime RequestTime { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(this.RequestId);
    }
}