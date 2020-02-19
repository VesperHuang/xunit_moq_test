using System.Collections.Generic;
using System.Linq;
using webapi.Models;

namespace webapi.Services{

    public interface IUserAccountService
    {
        UserAccount Get(int userAccountId);

        string GetFirstName(int userAccountId);

        List<UserAccount> GetAll();        
    }

    public class UserAccountService:IUserAccountService{

        public UserAccountService() { }

        public UserAccount Get(int userAccountId)
        {
            var userAccount = GetAllUserAccounts()
                .FirstOrDefault(x => x.Id == userAccountId);

            return userAccount;
        }

        public string GetFirstName(int userAccountId)
        {
            var result = GetAllUserAccounts()
                .Where(x => x.Id == userAccountId)
                .Select(x => x.FirstName)
                .FirstOrDefault();

            return result;
        }

        public List<UserAccount> GetAll()
        {
            return GetAllUserAccounts();
        }

        private List<UserAccount> GetAllUserAccounts()
        {
            return new List<UserAccount>()
            {
                new UserAccount
                {
                    Id = 123,
                    FirstName = "Simon",
                    Surname = "Gilbert",
                },
                new UserAccount
                {
                    Id = 456,
                    FirstName = "Alexander",
                    Surname = "Hill",
                },
            };
        }        
    }
}