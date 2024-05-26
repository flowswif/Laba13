using System;
using System.Collections.Generic;

namespace MyCollectionNamespace
{
    public class JournalEntry
    {
        public string CollectionName { get; set; }
        public string ChangeType { get; set; }
        public string ItemData { get; set; }

        public JournalEntry(string collectionName, string changeType, string itemData)
        {
            CollectionName = collectionName;
            ChangeType = changeType;
            ItemData = itemData;
        }

        public override string ToString()
        {
            return $"Collection: {CollectionName}, Change: {ChangeType}, Item: {ItemData}";
        }
    }

    public class Journal
    {
        private List<JournalEntry> entries = new List<JournalEntry>();

        public void AddEntry(string collectionName, string changeType, string itemData)
        {
            entries.Add(new JournalEntry(collectionName, changeType, itemData));
        }

        public override string ToString()
        {
            return string.Join("\n", entries);
        }
    }
}
