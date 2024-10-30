namespace AspDeck;

public interface IOptional<TValue>
{
    bool Has();

    IOptional<TValue> IfHas(Action<TValue> action);
    IOptional<TValue> IfNot(Action action);
    TValue Out();
}