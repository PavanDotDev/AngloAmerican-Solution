using System;
using System.Collections.Generic;
using System.Linq;

namespace AngloAmerican.Account.Services
{
    /* TODO
        - Refactor the class and add Unit Tests
    */
    public class BalanceChecker : IBalanceChecker
    {
        public bool Process(int amount, Notification notification, ExternalApi eA, string lastName)
        {
            var emailTitle = "";
            if (DateTime.Now.Day < 15)
            {
                emailTitle = "<h1>Info about days till Middle of the month</h1>";
            }
            else
            {
                emailTitle = "<h1>Info about days till End of the month</h1>";
            }

            var emailBody = "<p>Body placeholder<p>";
            var m = emailTitle + "\n" + emailBody;

            if (amount < 10000)
            {
                notification.SendEmail(emailTitle, emailBody);
            }
            if (amount > 10000)
            {
                notification.SendMessage(m);
                return eA.CheckAccountBalance(amount, lastName);
            }

            return true;
        }
    }

    public class Notification
    {
        public void SendEmail(string title, string body)
        {
            // it sends an email
        }

        public void SendMessage(string message)
        {
            // it sends a message
        }
    }


    // TODO: Improve and make the code more readable.
    public class ExternalApi
    {
        private List<string> _names = new List<string> {"Rene", "Kirk", "Escarole"};

        // returns true if balance is valid
        public bool CheckAccountBalance(int amount, string lastName)
        {
            //bool isFalse = false;

            // if the person is in the list and balance is greater than 10,000 return false

            if (_names.Any(x => x == lastName && amount > 10000))
                return false;
            else
                return true;

            //foreach (var n in _names)
            //{
            //    isFalse = true;
            //    if (n == lastName)
            //    {
            //        if (amount > 10000)
            //        {
            //            return false;
            //        }
            //        else
            //        {
            //            return true;
            //        }
            //    }

            //    return isFalse;
            //}

            //return isFalse;
        }
    }
}