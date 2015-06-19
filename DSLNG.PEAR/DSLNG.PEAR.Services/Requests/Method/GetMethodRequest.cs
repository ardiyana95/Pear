﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Requests.Method
{
    public class GetMethodRequest
    {
        public int Id { get; set; }
    }

    public class GetMethodsRequest { 
    }

    public class AddMethod {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Remark { get; set; }

        public bool IsActive { get; set; }
    }

    public class SaveMethod
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Remark { get; set; }

        public bool IsActive { get; set; }
    }
}
