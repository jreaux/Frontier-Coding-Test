﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontierChallengeContracts.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public decimal AmountDue { get; set; }
        public DateTimeOffset? PaymentDueDate { get; set; }
        public int AccountStatusId { get; set; }
    }
}
