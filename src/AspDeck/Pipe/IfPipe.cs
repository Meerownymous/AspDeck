using AspDeck;

namespace Tonga.Pipe;

/// <summary>
/// Add only if 
/// </summary>
public sealed class IfPipe<TPayload>(
    Func<TPayload, bool> condition, 
    IPipe<TPayload> consequence
) : PipeEnvelope<TPayload>(
    new AsPipe<TPayload>(
        payload => condition(payload) 
        ? consequence.Push(payload)
        : payload
    )
)
{ }