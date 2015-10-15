using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseTwo.Account
{
    internal class SimpleAccount : IAccount
    {
        private double balance;
        private int id;

        public double Balance
        {
            get { return balance; }

            set { balance = value; }
        }

        public SimpleAccount(int id)
        {
            Balance = 0;
            this.id = id;
        }

        public int getID()
        {
            return this.id;
        }

        public void Deposit(double amount)
        {
            Balance += amount;
        }

        public void Withdraw(double amount)
        {
            if (Balance - amount < 0)
            {
                Console.WriteLine("Withdraw failed. Simple Account can't overdraft");
            }
            else
            {
                Balance = -amount;
            }
        }

        //public bool IsRich()
        //{
        //    return Balance >= 1000000;
        //}

        public override bool Equals(object x)
        {
            // Check for null values
            if (x == null)
                return false;
            try
            {
                IAccount accountX = (IAccount) x;
                return accountX.getID() == this.getID();
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine("Types Doesn't match");
                return false;
            }
        }

        public override string ToString()
        {
            return "Account ID: " + id + " Balance: " + balance;
        }
    }
}