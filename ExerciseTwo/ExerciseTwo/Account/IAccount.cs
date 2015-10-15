using System;
using System.Collections;
using System.Collections.Generic;

namespace ExerciseTwo.Account
{
    interface IAccount
    {
        int getID();
        void Deposit(double amount);
        void Withdraw(double amount);
     //   bool IsRich();

    }
}
