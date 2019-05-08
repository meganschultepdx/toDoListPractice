using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using ToDoList.Models;


namespace ToDoList.Tests
{
  [TestClass]
  public class ItemTest : IDisposable
  {

    public void Dispose()
    {
      Item.ClearAll();
    }


    public ItemTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=to_do_list_test;";
    }

    [TestMethod]
    public void ItemConstructor_CreatesInstanceOfItem_Item()
    {
      Item newItem = new Item("test");
      Assert.AreEqual(typeof(Item), newItem.GetType());
    }

    [TestMethod]
    public void GetDescription_ReturnsDescription_String()
    {
      //Arrange
      string description = "Walk the dog.";
      Item newItem = new Item(description);

      //Act
      string result = newItem.GetDescription();

      //Assert
      Assert.AreEqual(description, result);
    }

    [TestMethod]
    public void SetDescription_SetDescription_String()
    {
      //Arrange
      string description = "Walk the dog.";
      Item newItem = new Item(description);

      //Act
      string updatedDescription = "Do the dishes";
      newItem.SetDescription(updatedDescription);
      string result = newItem.GetDescription();

      //Assert
      Assert.AreEqual(updatedDescription, result);
    }

    [TestMethod]
    public void GetAll_ReturnsEmptyListFromDatabase_ItemList()
    {
      //Arrange
      List<Item> newList = new List<Item> { };

      //Act
      List<Item> result = Item.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfDescriptionsAreTheSame_Item()
    {
      // Arrange, Act
      Item firstItem = new Item("Mow the lawn");
      Item secondItem = new Item("Mow the lawn");


      // Assert
      Assert.AreEqual(firstItem, secondItem);
    }


    [TestMethod]
    public void Find_ReturnsCorrectItemFromDatabase_Item()
    {
      //Arrange
      Item testItem = new Item("Sleeeep");
      testItem.Save();

      //Act
      Item foundItem = Item.Find(testItem.GetId());
      Console.WriteLine(testItem.GetId());
      Console.WriteLine(foundItem.GetId());

      //Assert
      Assert.AreEqual(testItem, foundItem);
    }

    [TestMethod]
  public void Edit_UpdatesItemInDatabase_String()
  {
    //Arrange
    string firstDescription = "Walk the Dog";
    Item testItem = new Item(firstDescription);
    testItem.Save();
    string secondDescription = "Mow the lawn";

    //Act
    testItem.Edit(secondDescription);
    string result = Item.Find(testItem.GetId()).GetDescription();

    //Assert
    Assert.AreEqual(secondDescription, result);
  }

  }
}

    // [TestMethod]
    // public void Save_AssignsIdToObject_Id()
    // {
      //   //Arrange
      //   Item testItem = new Item("Mow the lawn");
      //
      //   //Act
      //   testItem.Save();
      //   Item savedItem = Item.GetAll()[0];
      //
      //   int result = savedItem.GetId();
      //   int testId = testItem.GetId();
      //
      //   //Assert
      //   Assert.AreEqual(testId, result);
      // }
