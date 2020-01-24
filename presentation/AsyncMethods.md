# Async Methods and Operations

* _.NET_: `Task`
* _Java_: `Future`
* _JavaScript_: `Promise`


## `Task`
.NET Framework 4.0, .NET Core 1.0, .NET Standard 1.0
```cs --project .\Snippets\Snippets.csproj --source-file .\Snippets\TplDemo.cs --region Task_Void
```

## `Task<TResult>`
.NET Framework 4.0, .NET Core 1.0, .NET Standard 1.0
```cs --project .\Snippets\Snippets.csproj --source-file .\Snippets\TplDemo.cs --region Task_TResult
```

## `ValueTask`
.NET Core 2.1, .NET Standard 2.1, System.Threading.Tasks.Extensions
```cs --project .\Snippets\Snippets.csproj --source-file .\Snippets\TplDemo.cs --region ValueTask_Void
```

## `ValueTask<TResult>`
.NET Core 2.0, .NET Standard 2.1, System.Threading.Tasks.Extensions
```cs --project .\Snippets\Snippets.csproj --source-file .\Snippets\TplDemo.cs --region ValueTask_TResult
```

## `IAsyncEnumerable<T>`
.NET Core 3.0, .NET Standard 2.1, Microsoft.Bcl.AsyncInterfaces
```cs --project .\Snippets\Snippets.csproj --source-file .\Snippets\TplDemo.cs --region AsyncStream
```

## custom awaiter
```cs --project .\Snippets\Snippets.csproj --source-file .\Snippets\TplDemo.cs --region Awaiter
```

## `CancellationTokenSource`
```cs --project .\Snippets\Snippets.csproj --source-file .\Snippets\TplDemo.cs --region CancellationTokenSource
```

## `TaskCompletionSource<TResult>`
```cs --project .\Snippets\Snippets.csproj --source-file .\Snippets\TplDemo.cs --region TaskCompletionSource
```

---
###### [AsyncAwait](./AsyncAwait.md) < | > [Continuations](./Continuations.md)
