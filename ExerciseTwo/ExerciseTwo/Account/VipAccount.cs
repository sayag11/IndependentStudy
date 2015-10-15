using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace ExerciseTwo.Account
{
    class VipAccount : IAccount
    {
        private double balance;
        private int id;

        public double Balance
        {
            get
            {
                return balance;
            }

            set
            {
                balance = value;
            }
        }

        public VipAccount(int id)
        {
            balance = 0;
            this.id = id;
        }

        public int getID()
        {
            return this.id;
        }

        public void Deposit(double amount)
        {
            balance += amount;
        }
        public void Withdraw(double amount)
        {
            balance -= amount;
        }
        //public bool IsRich()
        //{
        //    return Balance >= 1000000;
        //}

        public override bool Equals(object x)
        {
            // Check for null values
            if (x == null )
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
