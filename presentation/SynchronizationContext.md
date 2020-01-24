# SynchronizationContext

```cs --project .\Snippets\Snippets.csproj --source-file .\Snippets\TapDemo.cs --region SynchronizationContext
```

### Frameworks with an implementation of [SynchronizationContext](https://docs.microsoft.com/en-us/dotnet/api/system.threading.synchronizationcontext)
* ASP.NET
* WPF
* WinForms

### Frameworks without [SynchronizationContext.Current](https://docs.microsoft.com/en-us/dotnet/api/system.threading.synchronizationcontext.current)
* Console
* ASP.NET Core

### Deadlocks

### ConfigureAwait(Boolean)
* use `.ConfigureAwait(false)` in framework-agnostic library code.
* in framework-dependent code w/ `SynchronizationContext`, use `.ConfigureAwait(false)` where possible
* in framework-dependent code w/o `SynchronizationContext`, `.ConfigureAwait(false)` is not necessary

---
###### [Continuations](./Continuations.md) < | > [AsyncStateMachine](./AsyncStateMachine.md)
