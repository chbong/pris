using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRIS.Models
{
    public class ApplicationViewModel
    {
        public int ApplicationId { get; set; }
        public System.DateTime ApplicationDate { get; set; }

        public Nullable<int> JobseekerId { get; set; }
        public Nullable<int> JobId { get; set; }

    }
}