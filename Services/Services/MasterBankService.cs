using System.Collections.Generic;
using System;
using System.Linq;
using BankSimulator.Models;

namespace Services.Services
{
    public class MasterBankService
    {
        public static List<Bank> Banks { get; set; }

        static MasterBankService()
        {
            Banks = new List<Bank>();
        }

        public static string CreateBank(Bank bank)
        {
            try
            {
                bank.Id = bank.Name.Substring(0, 3) + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year;
                Banks.Add(bank);
                return bank.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Bank GetBank(string id)
        {
            try
            {
                Bank bank = Banks.FirstOrDefault(a => a.Id == id);
                return bank;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
