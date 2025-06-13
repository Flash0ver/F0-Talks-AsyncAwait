using System.Diagnostics;
using System.Runtime.InteropServices;

namespace F0.Talks.AsyncAwait.ConsoleApp.Runtime;

internal sealed class ConsoleLifetime : IDisposable
{
    private readonly PosixSignalRegistration _sigInt;
    private readonly PosixSignalRegistration _sigQuit;
    private readonly PosixSignalRegistration _sigTerm;
    
    private readonly CancellationTokenSource _cts = new();

    public ConsoleLifetime()
    {
        Action<PosixSignalContext> handler = HandlePosixSignal;
        _sigInt = PosixSignalRegistration.Create(PosixSignal.SIGINT, handler);
        _sigQuit = PosixSignalRegistration.Create(PosixSignal.SIGQUIT, handler);
        _sigTerm = PosixSignalRegistration.Create(PosixSignal.SIGTERM, handler);
    }

    public CancellationToken CancellationToken => _cts.Token;

    private void HandlePosixSignal(PosixSignalContext context)
    {
        Debug.Assert(context.Signal == PosixSignal.SIGINT || context.Signal == PosixSignal.SIGQUIT || context.Signal == PosixSignal.SIGTERM);

        context.Cancel = true;
        _cts.Cancel();
        Console.WriteLine("Cancellation Requested");
    }

    public void Dispose()
    {
        _sigInt.Dispose();
        _sigQuit.Dispose();
        _sigTerm.Dispose();

        _cts.Dispose();
    }
}
