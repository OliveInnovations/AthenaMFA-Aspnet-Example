using AthenaMFA.Api.Integration;
using AthenaMFA.Api.Models;
using AthenaMFAAspnetExample.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AthenaMFAAspnetExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AthenaMFAClient _mfaClient;
        public HomeController(ILogger<HomeController> logger, AthenaMFAClient mfaClient)
        {
            _logger = logger;
            _mfaClient = mfaClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<LoginResponseModel> Login([FromForm] LoginModel login)
        {
            var validCredentials = true; //You usual logic to determine if the user is valid

            var response = new LoginResponseModel()
            {
                IsValid = validCredentials
            };

            if (response.IsValid)
            {
                var mfaCheck = await _mfaClient.RequestApproval(new MfaApprovalRequest
                {
                    email = login.Email,
                    respondonly = false //Set this to true if you never want to send push notifications
                });

                response.Response = mfaCheck;
                
                response.MfaRequired = mfaCheck.status != MfaResponseEnum.MfaAccountNotRegistered;

                if (!response.MfaRequired)
                {
                    //Login your user here
                }
            }

            return response;
        }

        [HttpPost]
        public async Task<MfaValidationResponse> LoginValidateMFA([FromBody] MfaCheckForApprovalRequest checkForApproval)
        {
            var result = await _mfaClient.ValidateApprovalResponse(checkForApproval);

            if(result.status == MfaResponseEnum.RequestApproved)
            {
                //Login your user here
            }

            return result;
        }
    }
}
