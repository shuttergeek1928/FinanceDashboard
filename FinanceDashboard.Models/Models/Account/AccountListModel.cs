﻿using System.ComponentModel.DataAnnotations;

namespace FinanceDashboard.Models.Account
{
    public class AccountListModel
    {
        [Required]
        public int AccountId { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }
    }
}
