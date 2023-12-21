using UnityEngine;

namespace Battle.Shared
{
    public interface ITarget
    {
        bool IsInvalid { get; }
        int FactionLayer { get; }
        Vector2 Position { get; }
        Vector2 Center { get; }
        GameObject GameObject { get; }
    }
}