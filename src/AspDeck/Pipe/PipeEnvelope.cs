using AspDeck;

namespace Tonga.Pipe;

/// <summary>
/// Envelope for Pipes.
/// </summary>
public abstract class PipeEnvelope<TPayload>(IPipe<TPayload> origin) : IPipe<TPayload>
{
    public TPayload Push(TPayload payload) => origin.Push(payload);
}