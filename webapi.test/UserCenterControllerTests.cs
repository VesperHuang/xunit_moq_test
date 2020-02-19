using System.Collections.Generic;
using System.Linq;

using Moq;

using Xunit;

using webapi.Models;
using webapi.Services;
using webapi.Controllers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace webapi.test{

    public class UserCenterControllerTest{

        [Fact]
        public void get_users_account()
        {

            var expectedUserAccountCount = 2;

            var mockUserAccountService = new Mock<IUserAccountService>();

            mockUserAccountService.Setup(x => x.GetAll())
                .Returns(GetTestUserAccounts());

            // Inject
            var userCenterController = new UserCenterController(mockUserAccountService.Object);

            // Act
            var result = userCenterController.get_users_account();

            // Assert
            ResponseBase response = Assert.IsType<ResponseBase>(JsonConvert.DeserializeObject<ResponseBase>(result));

            List<UserAccount> users = new List<UserAccount>();
            foreach (var item in response.List_Data)
            {   
                UserAccount user =  JsonConvert.DeserializeObject<UserAccount>(item.ToString());
                users.Add(user);
            }
            Assert.Equal(expectedUserAccountCount, users.Count());
        }

        [Fact]
        public void Account_View_Result_One()
        {
            // Setup
            var expectedUserAccountId = 123;

            var mockUserAccountService = new Mock<IUserAccountService>();

            mockUserAccountService.Setup(x => x.Get(expectedUserAccountId))
                .Returns(GetTestUserAccountOne());

            // Inject
            var userCenterController = new UserCenterController(mockUserAccountService.Object);

            // Act            
            var result = userCenterController.get_user_by_id(expectedUserAccountId);
            ResponseBase response = Assert.IsType<ResponseBase>(JsonConvert.DeserializeObject<ResponseBase>(result));
            UserAccount user = JsonConvert.DeserializeObject<UserAccount>(response.Data.ToString());

            // Assert            
            Assert.Equal(expectedUserAccountId, user.Id);
        }

        [Fact]
        public void Account_View_Result_Two()
        {
            // Setup
            var expectedUserAccountId = 456;

            var mockUserAccountService = new Mock<IUserAccountService>();

            mockUserAccountService.Setup(x => x.Get(expectedUserAccountId))
                .Returns(GetTestUserAccountTwo());

            // Inject
            var userCenterController = new UserCenterController(mockUserAccountService.Object);

            // Act            
            var result = userCenterController.get_user_by_id(expectedUserAccountId);
            ResponseBase response = Assert.IsType<ResponseBase>(JsonConvert.DeserializeObject<ResponseBase>(result));
            UserAccount user = JsonConvert.DeserializeObject<UserAccount>(response.Data.ToString());

            // Assert            
            Assert.Equal(expectedUserAccountId, user.Id);
        }

        [Fact]
        public void Get_First_Name_Result()
        {
            // Setup
            var userAccountId = 123;
            var userAccountFirstName = "Simon";

            var mockUserAccountService = new Mock<IUserAccountService>();

            mockUserAccountService.Setup(x => x.GetFirstName(userAccountId))
                .Returns(userAccountFirstName);

            // Inject
            var userCenterController = new UserCenterController(mockUserAccountService.Object);

            // Act
            var result = userCenterController.get_user_fist_name(userAccountId);
            ResponseBase response = Assert.IsType<ResponseBase>(JsonConvert.DeserializeObject<ResponseBase>(result));

            // Assert
            Assert.Equal(userAccountFirstName, response.Data);
        }    

        private List<UserAccount> GetTestUserAccounts()
        {
            return new List<UserAccount>()
            {
                GetTestUserAccountOne(),
                GetTestUserAccountTwo(),
            };
        }

        private UserAccount GetTestUserAccountOne()
        {
            return new UserAccount
            {
                Id = 123,
                FirstName = "Simon",
                Surname = "Gilbert",
            };
        }

        private UserAccount GetTestUserAccountTwo()
        {
            return new UserAccount
            {
                Id = 456,
                FirstName = "Alexander",
                Surname = "Hill",
            };
        }

    }
}