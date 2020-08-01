using ARS.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARS.Models.Requests
{
    public class BaseRequest
    {
        public int UserId { get; set; }

        public string SearchKey { get; set; }

        public List<string> Includes { get; set; } = new List<string>();

        public EntityStatus Status { get; set; }
    }
}
