using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace PRIS.Data
{
    public class States
    {
        public List<SelectListItem> StateName { get; set; }
        public int[] StateId { get; set; }
    }
}