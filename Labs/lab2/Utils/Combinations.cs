namespace Labs.lab2.Utils;

public static class Combinations
{
    public static IEnumerable<IEnumerable<T>> GetCombinations<T>(T[] items, int k)
    {
        var n = items.Length;
        var result = new T[k];

        return Recurse(0, 0);

        IEnumerable<IEnumerable<T>> Recurse(int start, int depth)
        {
            if (depth == k)
            {
                yield return result.ToArray();
            }
            else
            {
                for (var i = start; i < n; i++)
                {
                    result[depth] = items[i];
                    foreach (var r in Recurse(i + 1, depth + 1))
                        yield return r;
                }
            }
        }
    }
}