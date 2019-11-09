# Room Locator

## Adding Author tags

Add author tags in XML comments. Add it to the class if you are the one responsible. If you just made a single method in another's class, add it to the class. Example:

```csharp
/// <summary>
///	Description of what this does...
/// 	<author>John Doe, s123456</author>
/// </summary>
public class MyClass
{
	public void Method1()
	{
		// My awesome business logic
	}

	/// <summary>
	///	Description of what this does...
	/// 	<author>Another Author Here, s654321</author>
	/// </summary>
	public void AnotherFuncionality(string myStuff)
	{
		// Business logic here
	}
}
```

