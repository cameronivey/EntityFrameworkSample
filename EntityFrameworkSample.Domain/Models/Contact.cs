using System;

namespace EntityFrameworkSample.Domain.Models
{
    public class Contact : IEntity
    {
        public Contact(string name)
        {
            Name = name;
        }

        public Contact(string name, DateTime hireDate)
        {
            Name = name;
            HireDate = hireDate;
        }
        
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime HireDate { get; set; }
    }
}
