﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Responses.User
{
    public class ResetPasswordResponse : BaseResponse
    {
        public string Token { get; set; }
    }
}
