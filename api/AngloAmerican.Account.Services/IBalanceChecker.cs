using System;
using System.Collections.Generic;
using System.Text;

namespace AngloAmerican.Account.Services
{
    public interface IBalanceChecker
    {
        bool Process(int amount, Notification notification, ExternalApi eA, string lastName);
    }
}
