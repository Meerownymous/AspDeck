using AspDeck;

namespace Tonga.Pipe;

/// <summary>
/// Simple pipe from push function.
/// </summary>
public sealed class AsPipe<TPayload>(Func<TPayload,TPayload> push) : IPipe<TPayload>
{
    public TPayload Push(TPayload payload) => push(payload);
}

/// <summary>
/// Simple pipe from push function.
/// </summary>
public static class AsPipe
{
    /// <summary>
    /// Simple pipe from push function.
    /// </summary>
    public static AsPipe<TPayload> _<TPayload>(Func<TPayload, TPayload> push) => new(push);
}