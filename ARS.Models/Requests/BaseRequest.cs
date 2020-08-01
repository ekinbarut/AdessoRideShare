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
        #region [ User identity ]

        public int UserId { get; set; }

        public string UserToken { get; set; }

        public string SessionId { get; set; }

        public string UserIP { get; set; }

        #endregion

        public string SearchKey { get; set; }

        public List<string> Includes { get; set; } = new List<string>();

        public DateTime TimeStamp { get; set; }

        public EntityStatus Status { get; set; }
    }
}
