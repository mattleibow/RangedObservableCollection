using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Xunit;

namespace RangedObservableCollection.Tests
{
	public class RangedObservableCollectionTest
	{
		private static int[] ItemsToAdd => new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };

		private static int[] AlternateItemsToAdd => new[] { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };

		[Fact]
		public void AddRangeRaisesCollectionChangedOnce()
		{
			var collection = new RangedObservableCollection<int>();

			var didTrigger = false;
			collection.CollectionChanged += (s, e) =>
			{
				Assert.False(didTrigger);
				didTrigger = true;
			};

			collection.AddRange(ItemsToAdd);

			Assert.True(didTrigger);
		}

		[Fact]
		public void AddRangeRaisesCorrectCollectionChangedEvent()
		{
			var collection = new RangedObservableCollection<int>();

			collection.CollectionChanged += (s, e) =>
			{
				Assert.Equal(NotifyCollectionChangedAction.Add, e.Action);
				Assert.Null(e.OldItems);
				Assert.Equal(ItemsToAdd, e.NewItems);
			};

			collection.AddRange(ItemsToAdd);

			Assert.Equal(ItemsToAdd, collection.ToArray());
		}

		[Fact]
		public void ReplaceRaisesCollectionChangedOnce()
		{
			var collection = new RangedObservableCollection<int>();
			collection.AddRange(ItemsToAdd);

			var didTrigger = false;
			collection.CollectionChanged += (s, e) =>
			{
				Assert.False(didTrigger);
				didTrigger = true;
			};

			collection.Replace(1);

			Assert.True(didTrigger);
		}

		[Fact]
		public void ReplaceRaisesCorrectCollectionChangedEvent()
		{
			var collection = new RangedObservableCollection<int>();
			collection.AddRange(ItemsToAdd);

			collection.CollectionChanged += (s, e) =>
			{
				Assert.Equal(NotifyCollectionChangedAction.Replace, e.Action);
				Assert.Equal(ItemsToAdd, e.OldItems);
				Assert.Equal(new[] { 11 }, e.NewItems);
			};

			collection.Replace(11);

			Assert.Equal(new[] { 11 }, collection.ToArray());
		}

		[Fact]
		public void ReplaceRangeRaisesCollectionChangedOnce()
		{
			var collection = new RangedObservableCollection<int>();
			collection.AddRange(ItemsToAdd);

			var didTrigger = false;
			collection.CollectionChanged += (s, e) =>
			{
				Assert.False(didTrigger);
				didTrigger = true;
			};

			collection.ReplaceRange(AlternateItemsToAdd);

			Assert.True(didTrigger);
		}

		[Fact]
		public void ReplaceRangeRaisesCorrectCollectionChangedEvent()
		{
			var collection = new RangedObservableCollection<int>();
			collection.AddRange(ItemsToAdd);

			collection.CollectionChanged += (s, e) =>
			{
				Assert.Equal(NotifyCollectionChangedAction.Replace, e.Action);
				Assert.Equal(ItemsToAdd, e.OldItems);
				Assert.Equal(AlternateItemsToAdd, e.NewItems);
			};

			collection.ReplaceRange(AlternateItemsToAdd);

			Assert.Equal(AlternateItemsToAdd, collection.ToArray());
		}

		[Fact]
		public void RemoveRangeRaisesCollectionChangedOnce()
		{
			var collection = new RangedObservableCollection<int>();
			collection.AddRange(ItemsToAdd);

			var didTrigger = false;
			collection.CollectionChanged += (s, e) =>
			{
				Assert.False(didTrigger);
				didTrigger = true;
			};

			collection.RemoveRange(ItemsToAdd);

			Assert.True(didTrigger);
		}

		[Fact]
		public void RemoveRangeDoesNotRaiseCollectionChangedForMismatchingItems()
		{
			var collection = new RangedObservableCollection<int>();
			collection.AddRange(ItemsToAdd);

			var didTrigger = false;
			collection.CollectionChanged += (s, e) =>
			{
				Assert.False(didTrigger);
				didTrigger = true;
			};

			collection.RemoveRange(AlternateItemsToAdd);

			Assert.False(didTrigger);
		}

		[Fact]
		public void RemoveRangeRaisesCorrectCollectionChangedEvent()
		{
			var collection = new RangedObservableCollection<int>();
			collection.AddRange(ItemsToAdd);

			collection.CollectionChanged += (s, e) =>
			{
				Assert.Equal(NotifyCollectionChangedAction.Remove, e.Action);
				Assert.Equal(ItemsToAdd, e.OldItems);
				Assert.Null(e.NewItems);
			};

			collection.RemoveRange(ItemsToAdd);

			Assert.Equal(new int[0], collection.ToArray());
		}

		[Fact]
		public void RemoveRangeRaisesCorrectCollectionChangedEventForPartialRemoves()
		{
			var collection = new RangedObservableCollection<int>();
			collection.AddRange(ItemsToAdd);

			collection.CollectionChanged += (s, e) =>
			{
				Assert.Equal(NotifyCollectionChangedAction.Remove, e.Action);
				Assert.Equal(new[] { 1, 2, 3, 4, 5 }, e.OldItems);
				Assert.Null(e.NewItems);
			};

			collection.RemoveRange(new[] { 1, 2, 3, 4, 5 });

			Assert.Equal(new[] { 6, 7, 8, 9, 0 }, collection.ToArray());
		}

		[Fact]
		public void RemoveRangeRaisesCorrectCollectionChangedEventForNonExistentRemoves()
		{
			var collection = new RangedObservableCollection<int>();
			collection.AddRange(ItemsToAdd);

			collection.CollectionChanged += (s, e) =>
			{
				Assert.Equal(NotifyCollectionChangedAction.Remove, e.Action);
				Assert.Equal(new[] { 1, 2, 3, 4, 5 }, e.OldItems);
				Assert.Null(e.NewItems);
			};

			collection.RemoveRange(new[] { 1, 2, 3, 4, 5, 60, 70, 80, 90, 100 });

			Assert.Equal(new[] { 6, 7, 8, 9, 0 }, collection.ToArray());
		}
	}
}
