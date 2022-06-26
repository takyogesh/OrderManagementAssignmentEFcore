using OrderAssignmentConsoleApp;
using System;

namespace CustomerItemHandler
{
    class Program
    {
        static void Main(string[] args)
        {
        MainMenu:
            Console.WriteLine(" Press :1         Manage Item");
            Console.WriteLine(" Press :2         Manage Customres");
            Console.WriteLine(" Press :3         Close App");

            int switch_on = int.Parse(Console.ReadLine());
            switch (switch_on)
            {
                case 1:
                    CrudOpItem item = new CrudOpItem();
                    item.Menu();
                    goto MainMenu;
                case 2:
                    CrudOpCustomer customer = new CrudOpCustomer();
                    customer.Menu();
                    goto MainMenu;
                case 3:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Wrong Choise   ");
                    goto MainMenu;
            }

            Console.ReadLine();
        }
    }
}
