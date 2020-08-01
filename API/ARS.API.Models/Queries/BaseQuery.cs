using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARS.API.Models.Queries
{
    public class BaseQuery
    {
        public List<string> Includes = new List<string>();

        public string SearchKey { get; set; }
    }
}
