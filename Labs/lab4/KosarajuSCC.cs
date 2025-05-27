namespace Labs.lab4;

using System;
using System.Collections.Generic;

public class KosarajuSCC
{
    private readonly int nodesCount;
    private readonly List<int>[] graph;
    private readonly List<int>[] reverseGraph;
    private readonly bool[] visited;
    private readonly Stack<int> finishOrder;

    public KosarajuSCC((int u, int v, int weight)[] edgesList)
    {
        nodesCount = edgesList.Max(x => Math.Max(x.u, x.v)) + 1;

        (graph, reverseGraph) = InitializeGraphs(edgesList, nodesCount);

        visited = new bool[nodesCount];
        finishOrder = new Stack<int>();
    }

    private static (List<int>[] graph, List<int>[] reverseGraph) InitializeGraphs((int u, int v, int weight)[] edgesList, int nodesCount)
    {
        var graph = new List<int>[nodesCount];
        var reverseGraph = new List<int>[nodesCount];

        for (var i = 0; i < nodesCount; i++)
        {
            graph[i] = [];
            reverseGraph[i] = [];
        }
        foreach (var (u, v, _) in edgesList)
        {
            graph[u].Add(v);
            reverseGraph[v].Add(u);
        }
        return (graph, reverseGraph);
    }

    public List<List<int>> GetStronglyConnectedComponents()
    {
        var result = new List<List<int>>();

        for (var i = 0; i < nodesCount; i++)
        {
            if (!visited[i])
                DFS1(i);
        }

        Array.Fill(visited, false);
        while (finishOrder.Count > 0)
        {
            int node = finishOrder.Pop();
            if (!visited[node])
            {
                var component = new List<int>();
                DFS2(node, component);
                result.Add(component);
            }
        }

        return result;
    }

    private void DFS1(int node)
    {
        visited[node] = true;
        foreach (var neighbor in graph[node])
        {
            if (!visited[neighbor])
                DFS1(neighbor);
        }
        finishOrder.Push(node);
    }

    private void DFS2(int node, List<int> component)
    {
        visited[node] = true;
        component.Add(node);
        foreach (var neighbor in reverseGraph[node])
        {
            if (!visited[neighbor])
                DFS2(neighbor, component);
        }
    }
}
