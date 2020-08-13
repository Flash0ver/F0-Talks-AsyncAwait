# _async_ and _await_

* Since _C# 5_
* C# 8.0 added
  * await foreach (`IAsyncEnumerable<T>`)
  * await using (`IAsyncDisposable`)

## async

The contextual keyword [async][async] modifies a
* method
* lambda expression
* anonymous method

to be asynchronous, referred to as an _async method_.

In all other contexts, it's an identifier.

```cs --project .\Snippets\Snippets.csproj --source-file .\Snippets\AsyncAwaitDemo.cs --region Async
```

### Async Return Types
* [`Task`](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)
* [`Task<TResult>`](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)
* [`void`](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/void): Use only for event handlers!
* Since_C# 7.0_: any type implementing [ICriticalNotifyCompletion](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.icriticalnotifycompletion) with an accessible `GetAwaiter` method
  * [`ValueTask`](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.valuetask)
  * [`ValueTask<TResult>`](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.valuetask-1)
* Since _C# 8.0_: async iterator method (asynchronous streams)
  * [`IAsyncEnumerable<T>`](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.iasyncenumerable-1)

### Async Methods

Async methods cannot declare any
[in](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/in-parameter-modifier),
[ref](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/ref)
nor
[out](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/out-parameter-modifier)
parameters.

Async methods cannot have a
[reference return values](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/ref-returns)
nor
any ´ref struct´ local variables.


## await

Requires the `async` modifier.

The [await][await] operator:
* when applied to an operand that has not completed yet
  * does not block the thread that evaluates the async method
  * it suspends evaluation of the enclosing async method until the asynchronous operation represented by its operand completes
  * the control returns to the caller of the method
* when applied to an operand that has already completed, it returns the result immediately without suspension of the enclosing async method

Cannot await in
* a body of a [lock](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/lock-statement) statement
* an [unsafe](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/unsafe) context

```cs --project .\Snippets\Snippets.csproj --source-file .\Snippets\AsyncLockDemo.cs --region AsyncLock
```

### Awaitable expressions

The operands are usually
* `Task` / `ValueTask`
  * await `t` is `void`
* `Task<TResult>` / `ValueTask<TResult>`
  * await `t` is `TResult`

`await t`, where `t`
* has an accessible instance or extension method called `GetAwaiter` with no parameters and no type parameters, returning a Type
  * implementing `INotifyCompletion`
  * has an accessible, readable instance property `IsCompleted` of type bool
  * has an accessible instance method `GetResult` with no parameters and no type parameters
    * return `void`
    * return `T`


```cs
bool IsCompleted { get; }
void OnCompleted(Action continuation);
TResult GetResult(); //void GetResult();
```

---
###### [TAP](./TAP.md) < | > [AsyncMethods](./AsyncMethods.md)


[async]: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/async
[await]: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/await
