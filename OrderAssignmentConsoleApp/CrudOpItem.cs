using OrderAssignmentConsoleApp.Entities;
using System;
using System.Linq;

namespace OrderAssignmentConsoleApp
{
    public class CrudOpItem
    {
        DemoDbContext DbContextFile;
        ItemMaster itemMaster;
        public CrudOpItem()
        {
            DbContextFile = new DemoDbContext();
        }
        private ItemMaster Input()
        {
            itemMaster = new ItemMaster();
            Console.WriteLine("Enter The Item name ");
            itemMaster.Name = Console.ReadLine();
            Console.WriteLine("Enter The Item Price");
            itemMaster.Price = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter The Item Quantity");
            itemMaster.Quantity = Convert.ToInt32(Console.ReadLine());
            return itemMaster;
        }
        private void AddItem()
        {
            ItemMaster item = Input();
            try
            {
                if (item.Quantity > 0 && item.Price > 0 && item.Name != "")
                {
                    if (!ItemAlreadyExistOrNot(item.Name))
                    {
                        DbContextFile.itemMasters.Add(item);
                        DbContextFile.SaveChanges();
                        Console.WriteLine("Item Added");
                    }
                    else
                        Console.WriteLine("This Item Already Exist");
                }
                else
                    Console.WriteLine(" Item name must have some char");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void DeleteItem()
        {
            try
            {
                Console.WriteLine("Enter the Item Name To be Delete");
                string itemName = Console.ReadLine();

                if (itemName == "")
                    Console.WriteLine("Enter the Valid Name It Can't Be Null");
                else
                {
                    if (ItemAlreadyExistOrNot(itemName))
                    {
                        var deleteitem = DbContextFile.itemMasters.Where(item => item.Name == itemName).FirstOrDefault();
                        DbContextFile.itemMasters.Remove(deleteitem);
                        DbContextFile.SaveChanges();
                        Console.WriteLine("Item Delete SUccessfully !.");
                    }
                    else
                        Console.WriteLine("Item Does Not Exist in Record");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void UpdateItem()
        {
            ItemMaster item = Input();

           

            Console.WriteLine("Enter The Item name That You Want to Update");
           string updateItemName = Console.ReadLine();
            if (updateItemName == "")
            {
                Console.WriteLine("Enter the Name can not be empty");
            }
            else
            {
                if (ItemAlreadyExistOrNot(updateItemName))
                {
                    if (item.Quantity > 0 && item.Price > 0 && item.Name == "")
                        Console.WriteLine("Enter the correct details");
                    else
                    {
                        var updateabout = DbContextFile.itemMasters.Where(x => x.Name == updateItemName).FirstOrDefault();
                        updateabout.Name = item.Name;
                        updateabout.Price = item.Price;
                        updateabout.Quantity = item.Quantity;
                        try
                        {
                            DbContextFile.itemMasters.Update(updateabout);
                            DbContextFile.SaveChanges();
                            Console.WriteLine("Item Update Successfully");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
                else
                    Console.WriteLine("Item Does Not Exist in Record...");
            }
        }
        private void ShowAllItem()
        {
            try
            {
                var showitem = DbContextFile.itemMasters.ToList();
                if (showitem == null)
                    Console.WriteLine("No Data Found...");
                else
                {
                    foreach (var item in showitem)
                    {
                        Console.WriteLine(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private bool ItemAlreadyExistOrNot(string ItemName)
        {
            var exist = DbContextFile.itemMasters.Where(item => item.Name == ItemName).FirstOrDefault();
            if (exist == null)
                return false;
            else
                return true;
        }
        public void Menu()
        {
        Menu:
            Console.WriteLine(" Press :1 \tAdd Item");
            Console.WriteLine(" Press :2 \tDelete Item");
            Console.WriteLine(" Press :3 \tupdate Item");
            Console.WriteLine(" Press :4 \tShow All Item");
            Console.WriteLine(" Press :5 \tExit");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    AddItem();
                    goto Menu;
                case 2:
                    DeleteItem();
                    goto Menu;
                case 3:
                    UpdateItem();
                    goto Menu;
                case 4:
                    ShowAllItem();
                    goto Menu;
                case 5:
                    break;
            }

        }
    }
}
