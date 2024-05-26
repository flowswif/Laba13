using ClassLibrary1;
using ClassLibrary12_;
using System;

namespace MyCollectionNamespace
{
    public delegate void CollectionHandler(object source, CollectionHandlerEventArgs args);

    public class CollectionHandlerEventArgs : EventArgs
    {
        public string ChangeType { get; set; }
        public object ChangedItem { get; set; }

        public CollectionHandlerEventArgs(string changeType, object changedItem)
        {
            ChangeType = changeType;
            ChangedItem = changedItem;
        }

        public override string ToString()
        {
            return $"{ChangeType}: {ChangedItem}";
        }
    }

    public class MyObservableCollection<T> : MyCollection<T> where T : IInit, ICloneable, IComparable, new()
    {
        public event CollectionHandler CollectionCountChanged;
        public event CollectionHandler CollectionReferenceChanged;

        public MyObservableCollection() : base() { }
        public MyObservableCollection(int length) : base(length) { }

        public new void AddPoint(T item)
        {
            base.AddPoint(item);
            CollectionCountChanged?.Invoke(this, new CollectionHandlerEventArgs("Added", item));
        }

        public new bool RemoveData(T data)
        {
            bool result = base.RemoveData(data);
            if (result)
            {
                CollectionCountChanged?.Invoke(this, new CollectionHandlerEventArgs("Removed", data));
            }
            return result;
        }

        public new T this[int index]
        {
            get => base[index];
            set
            {
                T oldItem = base[index];
                base[index] = value;
                CollectionReferenceChanged?.Invoke(this, new CollectionHandlerEventArgs("Replaced", value));
            }
        }
    }
}
