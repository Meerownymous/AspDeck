namespace AspDeck;

/// <summary>
/// A pipe that can inspect or transform a payload.
/// </summary>
public interface IPipe<TPayload>
{
    TPayload Push(TPayload payload);
}