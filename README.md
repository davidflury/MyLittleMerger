# MyLittleMerger
This library offers a generic way to merge two instances of an object.

## Usage
### Initialize a new merger structure
```csharp
var merger = new MyLittleMerger<ContactObject>();
merger.Initialize(new MergerOptions
{
    AutoAddTypeAssembly = true
});
```

### Register a new method to resolve conflicts
```csharp
merger.RegisterResolver<string>("ContactObject.FirstName", (left, right) =>
{
    left.Value += right.Value;
    executed = true;
    return left;
});
```

### Merge two instance by using the resolving methods
```csharp
var instance = ContactData.David;
var compare = ContactData.Jennifer;
merger.Resolve(instance, compare);
```
