﻿using ARS.Common.Models;
using ARS.Models.Models;
using ARS.Models.Responses;
using ARS.Service.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARS.Service.Base
{
    public class CountriesServiceBase : EFServiceBase<Countries, CountriesDAO, ARSDBContext>
    {
       
    }
}
