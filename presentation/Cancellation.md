# Cooperative Cancellation

via [`CancellationToken` Struct](https://docs.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)
and [`CancellationTokenSource` Class (IDisposable)](https://docs.microsoft.com/en-us/dotnet/api/system.threading.cancellationtokensource)
```cs --project .\Snippets\Snippets.csproj --source-file .\Snippets\CancellationDemo.cs --region Cancellation_Operation
```

cancel specific [IAsyncEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.iasyncenumerable-1)
```cs --project .\Snippets\Snippets.csproj --source-file .\Snippets\CancellationDemo.cs --region Cancellation_AsyncStream1
```

cancel any [IAsyncEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.iasyncenumerable-1)
([AsyncEnumerable](https://github.com/dotnet/reactive/blob/master/Ix.NET/Source/System.Linq.Async/System/Linq/AsyncEnumerable.cs))
```cs --project .\Snippets\Snippets.csproj --source-file .\Snippets\CancellationDemo.cs --region Cancellation_AsyncStream2
```

---
###### [Exceptions](./Exceptions.md) < | > [Progress](./Progress.md)
