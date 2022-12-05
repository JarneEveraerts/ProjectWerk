using System.Collections;
using System.Net.Mail;
using Ardalis.GuardClauses;
using Domain.Models;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using Padi.Vies;
using Domain.Models.DTOs;
using System.Xml.Linq;
using Domain.Repositories;

namespace Domain
{
    public class DomainController : IDomainController
    {
        #region Validation

        public bool IsEmailValid(string email)
        {
            try
            {
                MailAddress Email = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool IsBtwValid(string btwNumber)
        {
            var result = ViesManager.IsValid(btwNumber);
            return result.IsValid;
        }

        public bool IsLicensePlateValid(string licensePlate)
        {
            Regex regex = new Regex(@"^[0-9]?([A-Z]{3}[0-9]{3}|[0-9]{3}[A-Z]{3})$");
            Match match = regex.Match(licensePlate);
            return match.Success;
        }

        #endregion Validation

    }
}