using AthenaMFA.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthenaMFAAspnetExample.Models
{
    public class LoginResponseModel
    {
        public bool IsValid { get; set; }
        public bool MfaRequired { get; set; }
        public MfaApprovalRequestResponse Response { get; set; }
    }
}
