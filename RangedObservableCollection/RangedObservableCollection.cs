using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Collections.ObjectModel
{
	public class RangedObservableCollection<T> : ObservableCollection<T>
	{
		private const string CountName = "Count";
		private const string IndexerName = "Item[]";

		public RangedObservableCollection()
			: base()
		{
		}

		public RangedObservableCollection(IEnumerable<T> collection)
			: base(collection)
		{
		}

		public void AddRange(IEnumerable<T> collection)
		{
			if (collection == null)
				throw new ArgumentNullException(nameof(collection));

			CheckReentrancy();

			var startIndex = Count;
			var changedItems = new List<T>(collection);

			foreach (var i in changedItems)
				Items.Add(i);

			OnPropertyChanged(new PropertyChangedEventArgs(CountName));
			OnPropertyChanged(new PropertyChangedEventArgs(IndexerName));
			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, changedItems, startIndex));
		}

		public void ReplaceRange(IEnumerable<T> collection)
		{
			if (collection == null)
				throw new ArgumentNullException(nameof(collection));

			CheckReentrancy();

			var oldItems = new List<T>(Items);
			var changedItems = new List<T>(collection);

			Items.Clear();
			foreach (var i in changedItems)
				Items.Add(i);

			OnPropertyChanged(new PropertyChangedEventArgs(CountName));
			OnPropertyChanged(new PropertyChangedEventArgs(IndexerName));
			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, changedItems, oldItems));
		}

		public void RemoveRange(IEnumerable<T> collection)
		{
			if (collection == null)
				throw new ArgumentNullException(nameof(collection));

			CheckReentrancy();

			var changedItems = new List<T>(collection);
			for (int i = 0; i < changedItems.Count; i++)
			{
				if (!Items.Remove(changedItems[i]))
				{
					changedItems.RemoveAt(i);
					i--;
				}
			}

			if (changedItems.Count > 0)
			{
				OnPropertyChanged(new PropertyChangedEventArgs(CountName));
				OnPropertyChanged(new PropertyChangedEventArgs(IndexerName));
				OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, changedItems, -1));
			}
		}

		public void Replace(T item)
		{
			CheckReentrancy();

			var oldItems = new List<T>(Items);
			var changedItems = new List<T> { item };

			Items.Clear();
			Items.Add(item);

			OnPropertyChanged(new PropertyChangedEventArgs(CountName));
			OnPropertyChanged(new PropertyChangedEventArgs(IndexerName));
			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, changedItems, oldItems));
		}
	}
}
