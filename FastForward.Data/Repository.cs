using System;
using FastForward.Models;

namespace FastForward.Data
{
    public class Repository : IRepository
    {
        public Application GetApplication()
        {
            return new Application
            {
                Name = "Fast Forward",
                Version = "0.1",
                LastUpdate = new DateTime(2015, 2, 10)
            };
        }
    }
}