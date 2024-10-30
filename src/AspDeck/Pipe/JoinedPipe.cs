using AspDeck;
using Tonga.Enumerable;

namespace Tonga.Pipe;

/// <summary>
/// Multiple pipes joined togethers
/// </summary>
/// <param name="pipes"></param>
/// <typeparam name="TPayload"></typeparam>
public sealed class JoinedPipe<TPayload>(IEnumerable<IPipe<TPayload>> pipes) : PipeEnvelope<TPayload>(
    new AsPipe<TPayload>(payload =>
    {
        foreach (var pipe in pipes)
            payload = pipe.Push(payload);
        return payload;
    })
)
{
    public JoinedPipe(params IPipe<TPayload>[] pipes) : this(AsEnumerable._(pipes))
    { }
}

public static class JoinedPipes
{
    public static JoinedPipe<TPayload> _<TPayload>(IEnumerable<IPipe<TPayload>> pipes) => new(pipes);
}