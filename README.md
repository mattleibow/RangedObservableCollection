# RangedObservableCollection

[![Build status](https://ci.appveyor.com/api/projects/status/31uqxvv3wm10emv9/branch/master?svg=true)](https://ci.appveyor.com/project/mattleibow/rangedobservablecollection/branch/master)  [![NuGet](https://img.shields.io/nuget/dt/RangedObservableCollection.svg)](https://www.nuget.org/packages/RangedObservableCollection)  [![NuGet Pre Release](https://img.shields.io/nuget/vpre/RangedObservableCollection.svg)](https://www.nuget.org/packages/RangedObservableCollection)

A small extension to ObservableCollection that allows for multiple items to be added, removed or replaced in a single operation.

# RangedObservableCollection

A small extension to ObservableCollection that allows for multiple items to be added, removed or replaced in a single operation.

This library is simple and just adds a few extra methods:

```csharp
var collection = new RangedObservableCollection<int>();

// bulk add
var itemsToAdd = new [] { 1, 2, 3, 4, 5 };
collection.AddRange(itemsToAdd);

// bulk remove
var itemsToRemove = new [] { 1, 2, 3, 4, 5 };
collection.RemoveRange(itemsToRemove);

// bulk clear and add
var itemsToReplace = new [] { 1, 2, 3, 4, 5 };
collection.ReplaceRange(itemsToReplace);
```
