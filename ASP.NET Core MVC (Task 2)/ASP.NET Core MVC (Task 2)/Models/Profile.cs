using System;

namespace ASP.NET_Core_MVC__Task_2_.Models
{
    public class Profile
    {
        public Guid Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public DateTime Birthday { get; init; }
    }
}
