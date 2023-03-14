<img src="https://api.visitorbadge.io/api/visitors?path=nejimon.raveendran-dotnetcoredidemo&countColor=%234a748a&style=flat"></img>

# Nuances of .NET Core Dependency Injection
#### Discusses AddScoped / AddTransient / AddSingleton
##

This repo demonstrates different behaviors when using different lifetime scopes, i.e., AddScoped versus AddTransient versus AddSingleton.

The *Program.cs* class contains the code to register 2 runner classes (*FirstRunner* and *SecondRunner*) 2 service classes (*FirstService* and *SecondService*), and a model class (*MyModel*).

Depending upon what kind of lifetime scope you use, the behvavior of the *FirstRunner* and *SecondRunner* will change.

First of all, we create 2 separate scopes, one for the *FirstRunner* and the other for *SecondRunner*.  These basically mimic 2 separate requests, as in a web application. 

```csharp
 using (var scope1 = serviceProvider.CreateScope()) 
  {
      var runner = scope1.ServiceProvider.GetRequiredService<FirstRunner>();
      runner.Run();
  }

  //scope 2 (mimics request 2 in a web app)
  using (var scope2 = serviceProvider.CreateScope())
  {
      var runner = scope2.ServiceProvider.GetRequiredService<SecondRunner>();
      runner.Run();
  }
```

In the *FirstRunner* class, we set the value of MyModel through the FirstService, and subsequently read it from the SecondService.  

```csharp
  public void Run()
  {
      //if you use .AddTransient<IMyModel, MyModel>(), a separate instance of MyModel class is injected into *FirstService* and *SecondService*, so _secondService.GetCurrentValue() returns a different value than the one set using _firstService.SetValue().
      //if you use .AddScoped<IMyModel, MyModel>(), the same instance of MyModel class is injected into FirstService and SecondService, so _secondService.GetCurrentValue() returns the same value set using _firstService.SetValue().  But this is persistence is applicable to only the scope1 in Program.cs.  In scope2, _secondService.GetCurrentValue() returns a different value. 
      //if you use .AddSingleton<IMyModel, MyModel>(), the same instance of MyModel class is injected into FirstService and SecondService to all scopes, so _secondService.GetCurrentValue() returns the same value set using _firstService.SetValue() within both scope1 and scope2.

      var setVal = _firstService.SetValue(10);
      Console.WriteLine($"Value set from scope1 is {setVal}");

      var curVal = _secondService.GetCurrentValue();
      Console.WriteLine($"Value read back within scope1 is {curVal}");

  }
}
```

In the *SecondRunner* class, we again read the value of MyModel through the *SecondService*

## Results
Try changing the following line in *Program.cs* from *AddScoped* to *AddTransient*, and then to *AddSingleton* and observe the change in behavior of FirstRunner and SecondRunner classes.

```csharp
  //IMyModel is injected into both IFirstService and ISecondService.  Depending upon the type of DI scope used, its behvavior will be different.
  //Change this to AddScoped, AddTransient, and AddSingleton and observe the change in behavior of FirstRunner and SecondRunner classes.
  .AddSingleton<IMyModel, MyModel>() 
```

**When using AddSingleton()**
The same instance of MyModel class will be injected everywhere, even across all scopes.  For this reason, once the value of MyModel is set, it is reflected everywhere it is injected into.  The output will be:

![image](https://user-images.githubusercontent.com/68135957/224889660-a4cb78a8-04ee-4b60-a1df-213afab703ee.png)

**When using AddScoped()**
An instance of MyModel is created and the same instance is injected everwhere within the same scope.  When moving to another scope, a new instance will be created and injected.  For this reason, once the value is set, it will be reflected everywhere within the scope.  It does not encroach into the boundary of another scope.  The output will be: 

![image](https://user-images.githubusercontent.com/68135957/224890029-ae41210e-5a73-4bee-b1b8-6387bef74a33.png)

**When using AddTransient()**
There will be completely separate instances of MyModel at each place it is injected in, even within the same scope.  In our example, it is injected in 3 places, and at all those 3 places, it will be a different instance of MyModel class. So the output will be:

![image](https://user-images.githubusercontent.com/68135957/224890402-51939810-2668-42ad-8bb6-4858f0ffeff4.png)

## Why Scoping is Important
It is very important a developer use the correct DI scoping in an application.  An incorrect use can lead to unexpected application behavior and performance problems.  For example, if you are using a Singleton class for data access and you are using a variable for holding database results, chances are that one request overwrites the results retrieved by another request leading to presenting incorrect data to the user.    




