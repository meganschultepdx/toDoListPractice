using System;
using System.Collections.Generic;

namespace ToDoList.Models
{

  public class Program
  {
    public static void Main()
    {
      Console.WriteLine("Welcome, would you like to add a new item to your list? (Y/N)");
      string userResponse = Console.ReadLine();

      if (userResponse == "Y")
      {

      Console.WriteLine("Add new Item to your To-Do List:");

      string description = Console.ReadLine();

      Item newItem = new Item(description);
      List<Item> newList = new List<Item> {newItem};

      for (int i = 0; x == 0; i++)
      {
        Console.WriteLine("Would you like to add another item or view list? (Add/View/Q)");

        string answer = Console.ReadLine();

        if (answer == "Add")
        {
          Console.WriteLine("Add new Item to your To-Do List:");

          string description = Console.ReadLine();

          Item newItem = new Item(description);
          List<Item> newList = new List<Item> {newItem};
        }
        else if (answer == "View")
        {
          List<Item> result = Item.GetAll();
          Console.WriteLine("Here is your list:");
          foreach (Item thisItem in result)
          {
            Console.WriteLine(thisItem.GetDescription());
          }
        }
        else if (answer == "Q")
        {
          x =1;
          break;
        }
        else
        {
          Console.WriteLine("Please enter a valid choice.");
        }
      }

     }
     else if (userResponse == "N")
     {
       Console.WriteLine("Goodbye");
     }

      // Console.WriteLine("Add new Item to your To-Do List:");
      //
      // string description2 = Console.ReadLine();
      //
      // Item newItem2 = new Item(description2);
      // List<Item> newList = new List<Item> {newItem, newItem2};
      //
      // List<Item> result = Item.GetAll();
      // Console.WriteLine("Here is your list:");
      // foreach (Item thisItem in result)
      // {
      //   Console.WriteLine(thisItem.GetDescription());
      //
      // }
    }
  }
}
