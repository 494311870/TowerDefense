using UnityEngine;

namespace Battle.Shared
{
    public static class TargetExtension
    {
        public static ITarget ToTarget(this Transform transform)
        {
            return new TransformTarget(transform);
        }
    }
}