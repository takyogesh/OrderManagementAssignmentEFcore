using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace OrderAssignmentConsoleApp
{
    public class CrudOpCustomer
    {
        Customers customers;
        DemoDbContext dbContextFile;
        public CrudOpCustomer()
        {
            dbContextFile = new DemoDbContext();
        }
        private Customers TakeInput()
        {
            customers = new Customers();
            Console.WriteLine("Enter the First Name");
            customers.FirstName = Console.ReadLine();
            Console.WriteLine("Enter The Last Name");
            customers.LastName = Console.ReadLine();
            Console.WriteLine("Enter the Phone Number");
            customers.PhoneNumber = Console.ReadLine();
            if (customers.FirstName != "" && customers.LastName != "" && customers.PhoneNumber.ToString().Length == 10 && customers.Email != "")
            {
            EnterValidmail:
                Console.WriteLine("Enter the Email");

                customers.Email = Console.ReadLine();
                if (!MailValidate(customers.Email))
                {
                    Console.WriteLine("Enter Valid Email Address");
                    goto EnterValidmail;
                }
            }
            return customers;
        }
        #region AddCustomer
        public void AddCustomers()
        {
            Customers customer=TakeInput();
            try
            {
                if (!CustomerExistOrNot(customers.Email))
                {
                    dbContextFile.customers.Add(customer);
                    dbContextFile.SaveChanges();
                    Console.WriteLine("Customer Add Successfully ");
                    MailSend(customer.Email, customer.FirstName, customer.LastName);
                }
                else
                    Console.WriteLine("User Already Exist Use Another   ");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion
        #region DeleteCustomer
        public void DeleteCustomers()
        {
            Console.WriteLine("Enter The Valid Eamil That Customer You Want to delete");
            customers.Email = Console.ReadLine();
            if (MailValidate(customers.Email))
            {
                if (CustomerExistOrNot(customers.Email))
                {
                    var deleteCustomer = dbContextFile.customers.Where(cust => cust.Email == customers.Email).FirstOrDefault();
                    dbContextFile.customers.Remove(deleteCustomer);
                    dbContextFile.SaveChanges();
                    Console.WriteLine("Customer Delete Successfully..");
                }
                else
                    Console.WriteLine(" Email Does not exist in the record");
            }
            else
                Console.WriteLine("Enter the Valid Email");
        }
        #endregion
        #region UpdateCustomer
        public void UpdateCustomers()
        {

            Console.WriteLine("Enter the Email tobe Updated");
            string Email = Console.ReadLine();
            Console.WriteLine("Enter the new updated data");

            Customers customer = TakeInput();
            if (MailValidate(customers.Email))
            {
                if (CustomerExistOrNot(customers.Email))
                {
                    TakeInput();
                    try
                    {
                        var updateCustomer = dbContextFile.customers.Where(cust => cust.Email == Email).FirstOrDefault();
                        updateCustomer.FirstName = customer.FirstName;
                        updateCustomer.LastName = customer.LastName;
                        updateCustomer.Email=customer.Email;
                        updateCustomer.PhoneNumber=customer.PhoneNumber;
                        dbContextFile.customers.Update(updateCustomer);
                        dbContextFile.SaveChanges();
                        Console.WriteLine(" Update data Successfully");
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.Message);
                    }
                }
                else
                    Console.WriteLine("Customer Does not Exist with this Email Address\n Enter The data again");
            }
            else
                Console.WriteLine("Please Enter The Valid Email");
        }
        #endregion
        #region List of All Customer
        public void ListOfAllCustomer()
        {
            var dt = dbContextFile.customers.ToList();
            try
            {
                if (dt != null)
                {
                    //print data
                    foreach (var cust in dt)
                    {
                        Console.WriteLine(cust);
                    }
                }
                else
                    Console.WriteLine("No Data Found...");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion
        private bool CustomerExistOrNot(string mail)
        {
            var customer = dbContextFile.customers.Where(cus => cus.Email == mail).FirstOrDefault();
            if (customer != null)
                return true;
            else
                return false;
        }
        public void MailSend(string to, string firstname, string lastname)
        {
            string from = "ytak989@gmail.com";
            string password = "jmhxccwbsjppybiz";
            string subject = "Welcome Dear Customer";
            string body = "<h1>Dear, " + firstname + " " + lastname + "</h1> \n Thanks for registering with us";
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(from);
                message.To.Add(new MailAddress(to));
                message.Subject = subject;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = body;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(from, password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public bool MailValidate(string email)
        {
            Regex eml = new Regex(@"^[a-zA-Z]+[._]{0,1}[0-9a-zA-Z]+[@][a-zA-Z]+[.][a-zA-Z]{2,3}([.]+[a-zA-Z]{2,3}){0,1}");
            Match m = eml.Match(email);
            if (m.Success)
                return true;
            else
                return false;
        }
        public void Menu()
        {
        MainMenu:
            Console.WriteLine("Press :1 \tTo Add Customer");
            Console.WriteLine("Press :2 \tTo Delete Customer");
            Console.WriteLine("Press :3 \tTo update Customer");
            Console.WriteLine("Press :4 \tTo Show All Customer");
            Console.WriteLine("Press :5 \tTo Exit    ");
            int choise = Convert.ToInt32(Console.ReadLine());
            switch (choise)
            {
                case 1:
                    AddCustomers();
                    goto MainMenu;
                case 2:
                    DeleteCustomers();
                    goto MainMenu;
                case 3:
                    UpdateCustomers();
                    goto MainMenu;
                case 4:
                    ListOfAllCustomer();
                    goto MainMenu;
                case 5:
                    break;
            }
        }
    }
}
