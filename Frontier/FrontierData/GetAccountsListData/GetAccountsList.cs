using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FrontierChallengeContracts.Enums;
using FrontierChallengeContracts.Models;

namespace GetAccountsListData
{
    public static class GetAccountsList
    {
        [FunctionName("GetAccountsList")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("A request has been made for the accounts list.");

            var accountsList = Get();
            var json = JsonConvert.SerializeObject(accountsList, Formatting.None);

            return accountsList != null
                ? (ActionResult)new OkObjectResult(json)
                : new BadRequestObjectResult("There was a problem servicing the accounts list request.");
        }

        public static IList<Account> Get()
        {
            // This would actually connect to a source for the this data and
            // pull back the list of accounts but, for the purposes of demonstration, 
            // just return a mocked list of accounts.
            return new List<Account>
            {
                Create(1, "Jackson", "Brown", "jackson.brown@easy-listening.com", "5555555555", 1210.44M, "12/1/2019 13:13:02", new TimeSpan(-2, 0, 0), AccountStatuses.Active),
                Create(2, "Joan", "Jett", "joan.jett@rockers-r-us.com", "5555555560", 832.11M, "10/13/2019 8:47:33", new TimeSpan(-4, 0, 0), AccountStatuses.Active),
                Create(3, "Justin", "Bieber", "justin.bieber@pop-goes-the-music.com", "5555555577", 5465.04M, "", null, AccountStatuses.Inactive),
                Create(4, "Mama", "Cass", "mama.cass@california-dreaming.com", "5555555522", 1155.11M, "2/22/1972 7:52:28", new TimeSpan(-3, 0, 0), AccountStatuses.Overdue),
                Create(5, "Mariah", "Carey", "diva.carey@bird-calls.com", "5555555582", 8796.34M, "3/31/2020 6:55:28", new TimeSpan(-1, 0, 0), AccountStatuses.Active),
                Create(6, "Bruno", "Mars", "bruno.mars@artist-anonymous.com", "5555555573", 3455.42M, "", null, AccountStatuses.Inactive),
                Create(7, "Alicia", "Keys", "alicia.keys@create-dat-music.com", "5555555543", 6446.54M, "7/12/2019 2:31:43", new TimeSpan(-1, 0, 0), AccountStatuses.Overdue),
            };
        }

        private static Account Create(
            int id,
            string firstName,
            string lastName,
            string email,
            string phoneNumber,
            decimal amountDue,
            string paymentDueDate,
            TimeSpan? offset,
            AccountStatuses accountStatus)
        {
            return new Account
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber,
                AmountDue = amountDue,
                PaymentDueDate = (string.IsNullOrWhiteSpace(paymentDueDate)) ? (DateTimeOffset?)null : new DateTimeOffset(Convert.ToDateTime(paymentDueDate), (TimeSpan)offset),
                AccountStatusId = (int)accountStatus
            };
        }
    }
}
