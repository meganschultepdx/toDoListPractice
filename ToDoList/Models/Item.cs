using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace ToDoList.Models
{
  public class Item
  {
    private string _description;
    private int _id;
    private int _categoryId;

    public Item (string description, int categoryId, int id = 0)
    {
      _description = description;
      _id = id;
      _categoryId = categoryId;
    }

    public string GetDescription()
    {
      return _description;
    }

    public void SetDescription(string newDescription)
    {
      _description = newDescription;
    }

    public int GetId()
    {
      return _id;
    }

    public static List<Item> GetAll()
  {
    List<Item> allItems = new List<Item> {};
    MySqlConnection conn = DB.Connection();
    conn.Open();
    var cmd = conn.CreateCommand() as MySqlCommand;
    cmd.CommandText = @"SELECT * FROM items;";
    var rdr = cmd.ExecuteReader() as MySqlDataReader;
    while(rdr.Read())
    {
      int itemId = rdr.GetInt32(0);
      string itemDescription = rdr.GetString(1);
      int itemCategoryId = rdr.GetInt32(2);
      Item newItem = new Item(itemDescription, itemCategoryId, itemId);
      allItems.Add(newItem);
    }
    conn.Close();
    if (conn != null)
    {
      conn.Dispose();
    }
    return allItems;
  }

    // public static List<Item> GetAll()
    // {
    //   //instantiate and return new empty list meant to hold Items. Item objects from DB will go here.
    //   List<Item> allItems = new List<Item> {};
    //   MySqlConnection conn = DB.Connection();
    //   conn.Open();
    //   //create a new instance of on of the special objects that are SQL Commands. create new sqlCommand object named cmd
    //   //CreateCommand is build in method. as MySqlCommand at end creates an expression that casts cmd into a MySqlCommand object, specifies MySqlCommand over other types of sql databases
    //   MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
    //   //actual text of our SQL Commands
    //   cmd.CommandText = @"SELECT * From items;";
    //   //create Data Reader Object, repsonsible for reading data returned by DB
    //   MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
    //   //must call build in .Read() method to make rdr do Something
    //   while(rdr.Read())
    //   {
    //     //call methods on rdr to retrieve Item information from DB
    //     int itemId = rdr.GetInt32(0);
    //     string itemDescription = rdr.GetString(1);
    //     //once data collected we can use it to instantiate new Item objects and add them to out allItems list. must reconstruct data into c# object with constructor
    //     Item newItem = new Item(itemDescription, itemId);
    //     allItems.Add(newItem);
    //   }
    //
    //   //must close DB connection when done
    //   conn.Close();
    //   if (conn != null)
    //   {
    //     conn.Dispose();
    //   }
    //   return allItems;
    // }
    //
    //
    // public static void ClearAll()
    // {
    //   MySqlConnection conn = DB.Connection();
    //   conn.Open();
    //   //create and open a new database connection
    //   var cmd = conn.CreateCommand() as MySqlCommand;
    //   //We call DB.Connection() to create our conn object, representing our connection to the database, and then call Open() upon it to open the connection. Remember, the DB before the method name here refers to the DB class defined in Database.cs
    //   cmd.CommandText = @"DELETE FROM items;";
    //   //built in command that modifies data instead of querying and returning it
    //   cmd.ExecuteNonQuery();
    //   //close databases
    //   conn.Close();
    //   if (conn != null)
    //   {
    //     conn.Dispose();
    //   }
    // }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM items;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static Item Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM `items` WHERE id = @thisId;";
      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int itemId = 0;
      string itemDescription = "";
      while (rdr.Read())
      {
        itemId = rdr.GetInt32(0);
        itemDescription = rdr.GetString(1);
      }
      Item foundItem= new Item(itemDescription, itemId);  // This line is new!
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundItem;  // This line is new!
    }

    public override bool Equals(System.Object otherItem)
    {
      if (!(otherItem is Item))
      {
        return false;
      }
      else
      {
        Item newItem = (Item) otherItem;
        bool idEquality = this.GetId() == newItem.GetId();
        bool descriptionEquality = this.GetDescription() == newItem.GetDescription();
        bool categoryEquality = this.GetCategoryId() == newItem.GetCategoryId();
        return (idEquality && descriptionEquality && categoryEquality);
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO items (description, category_id) VALUES (@description, @category_id);";
      MySqlParameter description = new MySqlParameter();
      description.ParameterName = "@description";
      description.Value = this._description;
      cmd.Parameters.Add(description);
      MySqlParameter categoryId = new MySqlParameter();
      categoryId.ParameterName = "@category_id";
      categoryId.Value = this._categoryId;
      cmd.Parameters.Add(categoryId);
      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void Edit(string newDescription)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE items SET description = @newDescription WHERE id = @searchId;";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);
      MySqlParameter description = new MySqlParameter();
      description.ParameterName = "@newDescription";
      description.Value = newDescription;
      cmd.Parameters.Add(description);
      cmd.ExecuteNonQuery();
      _description = newDescription; // <--- This line is new!
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public int GetCategoryId()
    {
      return _categoryId;
    }

  }
}
