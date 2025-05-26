namespace Labs.lab2;

public class GraphColoring
{
    private Dictionary<int, List<int>> adjacencyList;

    public GraphColoring((int u, int v, int weight)[] edges)
    {
        adjacencyList = InitializeAdjacencyList(edges);
    }

    private static Dictionary<int, List<int>> InitializeAdjacencyList((int u, int v, int weight)[] edges)
    {
        var dic = new Dictionary<int, List<int>>();
        foreach (var (u, v, _) in edges)
        {
            if (!dic.ContainsKey(u))
                dic[u] = [];
            if (!dic.ContainsKey(v))
                dic[v] = [];

            dic[u].Add(v);
            dic[v].Add(u);
        }
        return dic;
    }

    public Dictionary<int, int> ColorVertices()
    {
        var result = new Dictionary<int, int>();
        foreach (var vertex in adjacencyList.Keys.OrderBy(v => v))
        {
            var usedColors = new HashSet<int>();

            foreach (var neighbor in adjacencyList[vertex])
            {
                if (result.TryGetValue(neighbor, out var value))
                    usedColors.Add(value);
            }

            var color = 0;
            while (usedColors.Contains(color))
                color++;

            result[vertex] = color;
        }
        return result;
    }
}
