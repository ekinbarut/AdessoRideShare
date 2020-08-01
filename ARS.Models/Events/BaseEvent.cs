using ARS.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARS.Models.Events
{
    public class BaseEvent
    {

        #region [ User identity ]

        public int UserId { get; set; }

        public string UserToken { get; set; }

        public string SessionId { get; set; }

        public string UserIP { get; set; }

        #endregion

        public string ThreadId { get; set; }

        public DateTime TimeStamp { get; set; }

        public EntityStatus Status { get; set; }

    }
}
