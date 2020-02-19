using System.Security.Cryptography.X509Certificates;
using System;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using webapi.Models;
using webapi.Models.Enums;
using webapi.Services;

namespace webapi.Controllers{
    
    [ApiController]
    public class UserCenterController:ControllerBase{

        private readonly IUserAccountService _userAccountService;
        public UserCenterController(IUserAccountService userAccountService){
            _userAccountService = userAccountService;
        }

        [HttpGet("/api/v1/users/get_users")]
        public string get_users_account(){
            ResponseBase result = new ResponseBase();

            try{
                var userAccounts = _userAccountService.GetAll();
                var users = userAccounts.Select(userAccount => new UserAccount
                {
                    Id = userAccount.Id,
                    FirstName = userAccount.FirstName,
                    Surname = userAccount.Surname,
                });
                result.ReturnCode = ReturnCodes.Succeed;
                result.List_Data = users;

                return JsonConvert.SerializeObject(result);
            }
            catch(Exception ex){
                Console.Write(ex.Message.ToString());

                result.ReturnCode = ReturnCodes.Faild;
                result.ErrorMessage = "internal server error";
                return JsonConvert.SerializeObject(result);
            }
        }

        [HttpGet("/api/v1/users/get_user_by_id")]
        public string get_user_by_id(int id){
            ResponseBase result = new ResponseBase();

            try
            {
                var userAccount = _userAccountService.Get(id);

                var user = new UserAccount
                {
                    Id = userAccount.Id,
                    FirstName = userAccount.FirstName,
                    Surname = userAccount.Surname,
                };
                result.ReturnCode = ReturnCodes.Succeed;
                result.Data = user;                

                return JsonConvert.SerializeObject(result);                
            }
            catch(Exception ex){
                Console.Write(ex.Message.ToString());

                result.ReturnCode = ReturnCodes.Faild;
                result.ErrorMessage = "internal server error";
                return JsonConvert.SerializeObject(result);
            }            
        }

        [HttpGet("/api/v1/users/get_user_fist_name")]
        public string get_user_fist_name(int userAccountId)
        {
            ResponseBase result = new ResponseBase();            
            try
            {
                var user = _userAccountService.GetFirstName(userAccountId);
                
                result.ReturnCode = ReturnCodes.Succeed;
                result.Data = user;                

                return JsonConvert.SerializeObject(result);     
            }
            catch(Exception ex){
                Console.Write(ex.Message.ToString());

                result.ReturnCode = ReturnCodes.Faild;
                result.ErrorMessage = "internal server error";
                return JsonConvert.SerializeObject(result);
            }              
        }
    }
}