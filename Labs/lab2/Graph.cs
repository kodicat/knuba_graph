using Labs.lab2.Utils;

namespace Labs.lab2;

public class Graph
{
    private readonly (int u, int v)[] edges;
    private readonly int vertexCount;

    public Graph((int u, int v, int weight)[] weightedEdges)
    {
        edges = weightedEdges.Select(e => (e.u, e.v)).ToArray();
        vertexCount = weightedEdges
            .SelectMany(e => new[] { e.u, e.v })
            .Distinct()
            .Max() + 1;
    }

    public List<int> Solve()
    {
        var vertices = Enumerable.Range(0, vertexCount).ToArray();

        for (var k = 1; k <= vertexCount; k++)
        {
            foreach (var subset in Combinations.GetCombinations(vertices, k))
            {
                var coverSet = subset.ToHashSet();
                if (AreAllEdgesCovered(coverSet))
                    return subset.ToList();
            }
        }

        return [];
    }

    private bool AreAllEdgesCovered(HashSet<int> cover)
    {
        foreach (var (u, v) in edges)
        {
            if (!cover.Contains(u) && !cover.Contains(v))
                return false;
        }
        return true;
    }
}