using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace ToDoList.Models
{
  public class Item
  {
    private string _description;
    // private int _id;


    public Item (string description)
    {
      _description = description;

      // _id = _instances.Count;
    }

    public string GetDescription()
    {
      return _description;
    }

    public void SetDescription(string newDescription)
    {
      _description = newDescription;
    }

    public static List<Item> GetAll()
    {
      //instantiate and return new empty list meant to hold Items. Item objects from DB will go here.
      List<Item> allItems = new List<Item> {};
      //caal the Connection() method in Database.cs
      MySqlConnection conn = DB.Connection();
      //open database connection object
      conn.Open();
      //create a new instance of on of the special objects that are SQL Commands. create new sqlCommand object named cmd
      //CreateCommand is build in method. as MySqlCommand at end creates an expression that casts cmd into a MySqlCommand object, specifies MySqlCommand over other types of sql databases
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      //actual text of our SQL Commands
      cmd.CommandText = @"SELECT * From items;";
      //create Data Reader Object, repsonsible for reading data returned by DB
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader
      //must call build in .Read() method to make rdr do Something
      while(rdr.Read())
      {
        //call methods on rdr to retrieve Item information from DB
        int itemId = rdr.GetInt32(0);
        string itemDescription = rdr.GetString(1);
        //once data collected we can use it to instantiate new Item objects and add them to out allItems list. must reconstruct data into c# object with constructor
        Item newItem = new Item(itemDescription, itemId);
        allItems.Add(newItem);
      }

      //must close DB connection when done
      conn.Close();
      if (conn != null)
    {
      conn.Dispose();
    }
      return allItems;
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
     conn.Open();
     var cmd = conn.CreateCommand() as MySqlCommand;
     //We call DB.Connection() to create our conn object, representing our connection to the database, and then call Open() upon it to open the connection. Remember, the DB before the method name here refers to the DB class defined in Database.cs
     cmd.CommandText = @"DELETE FROM items;";
     //built in command that modifies data instead of querying and returning it
     cmd.ExecuteNonQuery();
     //close databases
     conn.Close();
     if (conn != null)
     {
      conn.Dispose();
     }
    }
    //
    // public static Item Find(int searchId)
    // {
    //   return _instances[searchId-1];
    // }
    //
    // public int GetId()
    // {
    //   return _id;
    // }


  }
}
