using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project
{
    public static class LinqExtensions
    {
        public static IEnumerable<float> SoftMax(this IEnumerable<float> source)
        {
            var exp = source.Select(Mathf.Exp).ToArray();
            var sum = exp.Sum();
            return exp.Select(x => x / sum);
        }
    }
}