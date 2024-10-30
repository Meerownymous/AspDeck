namespace Tonga.Pipe;

/// <summary>
/// Pipe which does nothing.
/// </summary>
public sealed class PassivePipe<TPayload>() : PipeEnvelope<TPayload>(
    new AsPipe<TPayload>(payload => payload)    
)
{ }