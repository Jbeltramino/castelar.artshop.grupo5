using System;
using System.Collections.Generic;
using System.Text;

namespace ArtShop.Data.Model
{
   public partial class Error: IdentityBase
    {
        public Error()
        {
            this.ErrorDate = DateTime.Now;
            this.ChangedOn = DateTime.Now;
            this.CreatedOn = DateTime.Now;
            this.CreatedBy = "monitor@artshop.com";
            this.ChangedBy = "monitor@artshop.com";
        }

        public string UserId { get; set; }
        public Nullable<System.DateTime> ErrorDate { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public string Exception { get; set; }
        public string Message { get; set; }
        public string Everything { get; set; }
        public string HttpReferer { get; set; }
        public string PathAndQuery { get; set; }
    }
}
