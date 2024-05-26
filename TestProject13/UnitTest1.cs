using Laba10;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCollectionNamespace;
using System.Collections.Generic;

namespace MyCollectionNamespace.Tests
{
    [TestClass]
    public class JournalEntryTests
    {
        [TestMethod]
        public void JournalEntry_ToString_ReturnsCorrectFormat()
        {
            // Arrange
            var entry = new JournalEntry("TestCollection", "Add", "TestData");

            // Act
            var result = entry.ToString();

            // Assert
            Assert.AreEqual("Collection: TestCollection, Change: Add, Item: TestData", result);
        }

        [TestMethod]
        public void JournalEntry_Constructor_SetsPropertiesCorrectly()
        {
            // Arrange
            var entry = new JournalEntry("TestCollection", "Remove", "TestData");

            // Assert
            Assert.AreEqual("TestCollection", entry.CollectionName);
            Assert.AreEqual("Remove", entry.ChangeType);
            Assert.AreEqual("TestData", entry.ItemData);
        }
    }

    [TestClass]
    public class JournalTests
    {
        [TestMethod]
        public void Journal_AddEntry_AddsEntryToList()
        {
            // Arrange
            var journal = new Journal();

            // Act
            journal.AddEntry("TestCollection", "Add", "TestData");

            // Assert
            var entriesField = typeof(Journal).GetField("entries", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var entries = entriesField.GetValue(journal) as List<JournalEntry>;

            Assert.AreEqual(1, entries.Count);
            Assert.AreEqual("TestCollection", entries[0].CollectionName);
            Assert.AreEqual("Add", entries[0].ChangeType);
            Assert.AreEqual("TestData", entries[0].ItemData);
        }

        [TestMethod]
        public void Journal_ToString_ReturnsCorrectFormat()
        {
            // Arrange
            var journal = new Journal();
            journal.AddEntry("TestCollection1", "Add", "TestData1");
            journal.AddEntry("TestCollection2", "Remove", "TestData2");

            // Act
            var result = journal.ToString();

            // Assert
            var expected = "Collection: TestCollection1, Change: Add, Item: TestData1\nCollection: TestCollection2, Change: Remove, Item: TestData2";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Journal_AddMultipleEntries_AddsAllEntries()
        {
            // Arrange
            var journal = new Journal();

            // Act
            journal.AddEntry("TestCollection1", "Add", "TestData1");
            journal.AddEntry("TestCollection2", "Remove", "TestData2");
            journal.AddEntry("TestCollection3", "Update", "TestData3");

            // Assert
            var entriesField = typeof(Journal).GetField("entries", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var entries = entriesField.GetValue(journal) as List<JournalEntry>;

            Assert.AreEqual(3, entries.Count);
            Assert.AreEqual("TestCollection1", entries[0].CollectionName);
            Assert.AreEqual("Add", entries[0].ChangeType);
            Assert.AreEqual("TestData1", entries[0].ItemData);
            Assert.AreEqual("TestCollection2", entries[1].CollectionName);
            Assert.AreEqual("Remove", entries[1].ChangeType);
            Assert.AreEqual("TestData2", entries[1].ItemData);
            Assert.AreEqual("TestCollection3", entries[2].CollectionName);
            Assert.AreEqual("Update", entries[2].ChangeType);
            Assert.AreEqual("TestData3", entries[2].ItemData);
        }

        [TestMethod]
        public void AddPoint_ShouldTriggerCollectionCountChangedEvent()
        {
            // Arrange
            var collection = new MyObservableCollection<ControlElement>();
            bool eventTriggered = false;

            collection.CollectionCountChanged += (sender, args) =>
            {
                eventTriggered = true;
                Assert.AreEqual("Added", args.ChangeType);
                Assert.IsInstanceOfType(args.ChangedItem, typeof(ControlElement));
            };

            var element = new ControlElement("test", 10.0, 20.0, 1);

            // Act
            collection.AddPoint(element);

            // Assert
            Assert.IsTrue(eventTriggered);
        }

        [TestMethod]
        public void RemoveData_ShouldTriggerCollectionCountChangedEvent()
        {
            // Arrange
            var collection = new MyObservableCollection<ControlElement>();
            var element = new ControlElement("test", 10.0, 20.0, 1);
            collection.AddPoint(element);
            bool eventTriggered = false;

            collection.CollectionCountChanged += (sender, args) =>
            {
                eventTriggered = true;
                Assert.AreEqual("Removed", args.ChangeType);
                Assert.AreEqual(element, args.ChangedItem);
            };

            // Act
            collection.RemoveData(element);

            // Assert
            Assert.IsTrue(eventTriggered);
        }

        [TestMethod]
        public void Indexer_Set_ShouldTriggerCollectionReferenceChangedEvent()
        {
            // Arrange
            var collection = new MyObservableCollection<ControlElement>();
            var oldElement = new ControlElement("old", 10.0, 20.0, 1);
            var newElement = new ControlElement("new", 30.0, 40.0, 2);
            collection.AddPoint(oldElement);
            bool eventTriggered = false;

            collection.CollectionReferenceChanged += (sender, args) =>
            {
                eventTriggered = true;
                Assert.AreEqual("Replaced", args.ChangeType);
                Assert.AreEqual(newElement, args.ChangedItem);
            };

            // Act
            collection[0] = newElement;

            // Assert
            Assert.IsTrue(eventTriggered);
        }

        [TestMethod]
        public void AddPoint_ShouldIncreaseCount()
        {
            // Arrange
            var collection = new MyObservableCollection<ControlElement>();
            var element = new ControlElement("test", 10.0, 20.0, 1);

            // Act
            collection.AddPoint(element);

            // Assert
            Assert.AreEqual(1, collection.Count);
        }

        [TestMethod]
        public void RemoveData_ShouldDecreaseCount()
        {
            // Arrange
            var collection = new MyObservableCollection<ControlElement>();
            var element = new ControlElement("test", 10.0, 20.0, 1);
            collection.AddPoint(element);

            // Act
            collection.RemoveData(element);

            // Assert
            Assert.AreEqual(0, collection.Count);
        }

        [TestMethod]
        public void Indexer_Get_ShouldReturnCorrectItem()
        {
            // Arrange
            var collection = new MyObservableCollection<ControlElement>();
            var element = new ControlElement("test", 10.0, 20.0, 1);
            collection.AddPoint(element);

            // Act
            var retrievedElement = collection[0];

            // Assert
            Assert.AreEqual(element, retrievedElement);
        }
    }
}
