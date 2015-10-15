using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExerciseTwo.Account;
using Logger.Infra;

namespace ExerciseTwo
{
    class Program
    {
        public delegate void Depos(double amount);
        static SuperList<IAccount> accounts = new SuperList<IAccount>();
        public static int AmountToDeposit;
        public static int AmountToWithdraw;
        static ILog log = Logger.Logger.Instance.Log;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Bank Accounts SuperList.");
           
            string line;
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine();
                PrintList();
                Console.WriteLine();
                Console.WriteLine("Choose an action from the following:");
                Console.WriteLine("a. Add an account");
                Console.WriteLine("b. Remove an Account");
                Console.WriteLine("c. Deposit money to an account or Everyone");
                Console.WriteLine("d. Withdraw money from an account");
                Console.WriteLine("e. Quit");
                line = Console.ReadLine();
                switch (line)
                {
                    case "a":
                        AddAccount();
                        break;
                    case "b":
                        RemoveAccount();
                        break;
                    case "c":
                        Deposit();
                        break;
                    case "d":
                        Withdraw();
                        break;
                    case "e":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("choose again");
                        break;
                }
            }


        }

        public static void AddAccount()
        {

            Console.WriteLine("choose account type:");
            Console.WriteLine("1. Simple Account");
            Console.WriteLine("2. VIP Account");
            int accountType = int.Parse(Console.ReadLine());
            Console.WriteLine("enter an ID number for the account");
            int id = int.Parse(Console.ReadLine());
            IAccount newAccount;
            switch (accountType)
            {
                case 1:
                    newAccount = new SimpleAccount(id);
                    break;
                case 2:
                    newAccount = new VipAccount(id);

                    break;
                default:
                    throw new Exception("Wrong Account Type");

            }
            if (accounts.Contains(newAccount))
            {
                Console.WriteLine("account already exists");
            }
            else
            {
                accounts.Add(newAccount);
            }


                string message;
                if (accountType == 1)
                {
                    message = "Simple Account added ID " + id;

                }
                else
                {
                    message = "VIP Account added ID: " + id;
                }

            try
            {
                log.WriteEntry(new LogEntry() { Message = message });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                Environment.Exit(1);
            }
        }

        public static void RemoveAccount()
        {
            Console.WriteLine("write the account's ID you want to remove");
            int id = int.Parse(Console.ReadLine());
            if (!accounts.Contains(new SimpleAccount(id)))
            {
                Console.WriteLine("account doesn't exist");
            }
            else
            {
                accounts.RemoveAt(accounts.IndexOf(new SimpleAccount(id)));
            }
            try
            {
                log.WriteEntry(new LogEntry() { Message = "Account ID: "+id+" was removed" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                Environment.Exit(1);
            }
        }

        public static void Deposit()
        {
            string message;
            Console.WriteLine("choose where to deposit the money:");
            Console.WriteLine("1. A specific account");
            Console.WriteLine("2. All accounts");
            int depositDestination = int.Parse(Console.ReadLine());

            switch (depositDestination)
            {
                case 1:
                    Console.WriteLine("enter an ID of the account you want to deposit to: ");
                    int id = int.Parse(Console.ReadLine());
                    int index = accounts.IndexOf(new SimpleAccount(id));
                    while (index == -1)
                    {
                        Console.WriteLine("ID not found");
                        id = int.Parse(Console.ReadLine());
                        index = accounts.IndexOf(new SimpleAccount(id));
                    }
                    Console.WriteLine("enter the amount of money you want to deposit: ");
                    AmountToDeposit = int.Parse(Console.ReadLine());
                    accounts.ActOnIndex(DepositToAccount, index);
                    message = AmountToDeposit + " $ transfered to account ID: " + id;
                    break;
                case 2:
                    Console.WriteLine("enter the amount of money you want to deposit: ");
                    AmountToDeposit = int.Parse(Console.ReadLine());
                    accounts.ActAllItems(DepositToAccount);
                    message = AmountToDeposit + " $ transfered to all accounts";
                    break;
                default:
                    throw new Exception("Wrong Account Type");
            }
            try
            {
                log.WriteEntry(new LogEntry() { Message = message });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                Environment.Exit(1);
            }
        }
        public static void Withdraw()
        {
            string message;
            Console.WriteLine("choose where to withdraw money from:");
            Console.WriteLine("1. A specific account");
            Console.WriteLine("2. All accounts");
            int withdrawDestination = int.Parse(Console.ReadLine());

            switch (withdrawDestination)
            {
                case 1:
                    Console.WriteLine("enter an ID of the account you want to withdraw from: ");
                    int id = int.Parse(Console.ReadLine());
                    int index = accounts.IndexOf(new SimpleAccount(id));
                    Console.WriteLine("enter the amount of money you want to withdraw: ");
                    AmountToWithdraw = int.Parse(Console.ReadLine());
                    accounts.ActOnIndex(WithdrawFromAccount, index);
                    message = AmountToWithdraw + " $ were taken from account ID: " + id;
                    break;
                case 2:
                    Console.WriteLine("enter the amount of money you want to withdraw: ");
                    AmountToWithdraw = int.Parse(Console.ReadLine());
                    accounts.ActAllItems(WithdrawFromAccount);
                    message = AmountToWithdraw + " $ were taken from all accounts";
                    break;
                default:
                    throw new Exception("Wrong Account Type");

            }
            try
            {
                log.WriteEntry(new LogEntry() { Message = message });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                Environment.Exit(1);
            }
        }

        public static IAccount DepositToAccount(IAccount account)
        {
            account.Deposit(AmountToDeposit);
            return null;
        }

        public static IAccount WithdrawFromAccount(IAccount account)
        {
            account.Withdraw(AmountToWithdraw);
            return null;
        }

        public static void PrintList()
        {
            foreach (IAccount acc in accounts)
            {
                Console.WriteLine(acc.ToString());
            }
        }
    }

}
