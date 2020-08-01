using ARS.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARS.Models.Responses
{
    public class ARSServiceResponse<T> : ARSServiceResponse
    {
        public List<T> Result { get; set; }
    }

    public class ARSServiceResponse
    {
        public List<string> Errors { get; set; }

        public ServiceResponseTypes Type { get; set; }
    }
}
