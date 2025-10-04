using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Common
{
    public static class ExtensionMethods
    {
        public static T PickOne<T>(this IEnumerable<T> source) => 
            source.ToArray()[Random.Range(0, source.Count())];

        public static bool Empty<T>(this IEnumerable<T> source) => !source.Any();

        public static bool IsOdd(this int i) => i % 2 != 0;

        public static void ResetAllTriggers(this Animator animator)
        {
            foreach (var p in animator.parameters)
            {
                if(p.type == AnimatorControllerParameterType.Trigger)
                    animator.ResetTrigger(p.name);
            }
        }
    }
}
