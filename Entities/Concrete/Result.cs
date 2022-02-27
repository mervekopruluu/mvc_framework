using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities.Concrete
{
    public class Result
    {
        public bool Status { get; set; } = false;
        public string Message { get; set; }
        public int ReturnId { get; set; }
    }
}