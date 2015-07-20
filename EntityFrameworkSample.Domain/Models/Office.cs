﻿namespace EntityFrameworkSample.Domain.Models
{
    public class Office : IEntity
    {
        public Office()
        {

        }

        public Office(string suiteNumber)
        {
            this.SuiteNumber = suiteNumber;
        }

        public int Id { get; set; }

        public string SuiteNumber { get; set; }

        public Company Company { get; set; }
    }
}
