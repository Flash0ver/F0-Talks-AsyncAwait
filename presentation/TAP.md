# Asynchronous programming with async and await

via the _Task asynchronous programming model_ ([TAP][tap]).

---

> A series of asynchronous instructions that read like a sequence of statements
>
> which
> may be started concurrently
>
> and
> do not block, but await completion instead

---

> _I/O-bound_ operations
>
> vs
>
> _CPU-bound_ operations

---

## Control flow in async programs

```cs --project ./Snippets/Snippets.csproj --source-file ./Snippets/TapDemo.cs --region ControlFlow
```

---
###### [Terminology](./Terminology.md) < | > [AsyncAwait](./AsyncAwait.md)


[tap]: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/
