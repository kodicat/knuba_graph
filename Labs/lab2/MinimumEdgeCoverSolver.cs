using Labs.lab2.Utils;

namespace Labs.lab2;

public class MinimumEdgeCoverSolver
{
    private readonly (int u, int v)[] edges;
    private readonly HashSet<int> vertices;

    public MinimumEdgeCoverSolver((int u, int v, int weight)[] weightedEdges)
    {
        edges = weightedEdges.Select(e => (e.u, e.v)).ToArray();
        vertices = weightedEdges
            .SelectMany(e => new[] { e.u, e.v })
            .ToHashSet();
    }

    public List<(int u, int v)> Solve()
    {
        for (var k = 1; k <= edges.Length; k++)
        {
            foreach (var subset in Combinations.GetCombinations(edges, k))
            {
                var coveredVertices = new HashSet<int>();
                foreach (var (u, v) in subset)
                {
                    coveredVertices.Add(u);
                    coveredVertices.Add(v);
                }

                if (coveredVertices.SetEquals(vertices))
                    return subset.ToList();
            }
        }

        return [];
    }
}