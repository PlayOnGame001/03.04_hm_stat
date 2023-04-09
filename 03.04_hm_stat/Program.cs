using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03._04_hm_stat
{
    abstract class State
    {
        public Profile profile;
        public double balance;
        public double LowerLimit;
        public double UpperLimit;
        public double GetBalance()
        {
            return balance;
        }
        public void SetBalance()
        {
            Console.Write("Enter balance: ");
            string b = Console.ReadLine();
            balance = Convert.ToDouble(b);
        }
        public abstract void Deposit();
        public abstract void Withdraw();
    }
    class Profile
    {
        public string owner { get; set; }
        public State state;
        public Profile(string owner)
        {
            this.owner = owner;
        }
        public void SetState(State state)
        {
            this.state = state;
        }
        public void Deposit()
        {
            Console.Write("Enter amount profile: ");
            string n = Console.ReadLine();
            state.balance += Convert.ToDouble(n);
            Console.WriteLine("Success Profile...");
        }
        public void GetBalance()
        {
            Console.WriteLine($"Balance: {state.balance}");
        }
        public void Withdraw()
        {
            Console.Write("Enter amount to download: ");
            string s = Console.ReadLine();
            state.balance -= Convert.ToDouble(s);
        }
    }
    class Basic_card : State
    {
        public Basic_card(double balance, Profile account)
        {
            this.balance = balance;
            this.profile = account;
            Initialize();
        }
        public void StateChange()
        {
            if (balance > UpperLimit)
            {
                profile.SetState(new Selver_card(balance, profile));
            }
        }
        public void Initialize()
        {
            LowerLimit = -100.0;
            UpperLimit = 0.0;
        }
        public override void Deposit()
        {
            Console.Write("Enter amount profile: ");
            string n = Console.ReadLine();
            balance += Convert.ToDouble(n);
            Console.WriteLine("Success Profile...");
        }
        public override void Withdraw()
        {
            Console.Write("Enter amount to download: ");
            string s = Console.ReadLine();
            balance -= Convert.ToDouble(s);
        }
    }
    class Selver_card : State
    {
        public Selver_card(double balance, Profile account)
        {
            this.balance = balance;
            this.profile = account;
            Initialize();
        }
        public void StateChange()
        {
            if (balance < LowerLimit)
            {
                profile.SetState(new Basic_card(balance, profile));
            }
            else if (balance > UpperLimit)
            {
                profile.SetState(new Gold_card(balance, profile));
            }
        }
        public void Initialize()
        {
            LowerLimit = 0.0;
            UpperLimit = 1000.0;
        }
        public override void Deposit()
        {
            Console.Write("Enter the replenishment amount: ");
            string n = Console.ReadLine();
            balance += Convert.ToDouble(n);
            Console.WriteLine("Refill completed successfully...");
        }
        public override void Withdraw()
        {
            Console.Write("Enter amount to withdraw: ");
            string s = Console.ReadLine();
            balance -= Convert.ToDouble(s);
        }
    }
    class Gold_card : State
    {
        public Gold_card(double balance, Profile account)
        {
            this.balance = balance;
            this.profile = account;
            Initialize();
        }
        public void StateChange()
        {
            if (balance < 0.0)
            {
                profile.SetState(new Basic_card(balance, profile));
            }
            else if (balance < LowerLimit)
            {
                profile.SetState(new Selver_card(balance, profile));
            }
        }
        public void Initialize()
        {
            LowerLimit = 1000.0;
            UpperLimit = 100000000.0;
        }
        public override void Deposit()
        {
            Console.Write("Enter the replenishment amount: ");
            string n = Console.ReadLine();
            balance += Convert.ToDouble(n);
            Console.WriteLine("Refill completed successfully...");
        }
        public override void Withdraw()
        {
            Console.Write("Enter amount to withdraw: ");
            string s = Console.ReadLine();
            balance -= Convert.ToDouble(s);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Profile profile = new Profile("bank");
            profile.SetState(new Selver_card(0.0, profile));
            profile.Deposit();
        }
    }
}