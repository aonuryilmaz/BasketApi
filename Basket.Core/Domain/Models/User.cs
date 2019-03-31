using System;

namespace Basket.Core.Domain.Models
{
    public class User : Entity
    {
        public string Email { get; set; }
        public Guid BasketId { get; set; }

        protected User()
        {
        }

        public User(string email)
        {
            Email = email;
        }
    }
}