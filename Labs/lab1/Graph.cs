namespace Labs.lab1;

public class Graph
{
    private const int Infinity = int.MaxValue / 2;

    private readonly int vertexCount;
    private readonly int[,] adjacencyMatrix;
    private readonly int[,] distanceMatrix;

    public Graph(int[,] adjacencyMatrix)
    {
        vertexCount = adjacencyMatrix.GetLength(0);
        this.adjacencyMatrix = adjacencyMatrix;
        distanceMatrix = BuildDistanceMatrixFrom(adjacencyMatrix, vertexCount);
    }

    public Graph(int[][] adjacencyList)
    {
        vertexCount = adjacencyList.Length;
        adjacencyMatrix = BuildAdjacencyMatrix(adjacencyList, vertexCount);
        distanceMatrix = BuildDistanceMatrixFrom(adjacencyMatrix, vertexCount);
    }

    public Graph((int u, int v)[] edgeList, int vertexCount)
    {
        this.vertexCount = vertexCount;
        adjacencyMatrix = BuildAdjacencyMatrix(edgeList, vertexCount);
        distanceMatrix = BuildDistanceMatrixFrom(adjacencyMatrix, vertexCount);
    }

    private static int[,] BuildAdjacencyMatrix(int[][] adjacencyList, int vertexCount)
    {
        var adjacencyMatrix = new int[vertexCount, vertexCount];

        for (var i = 0; i < vertexCount; i++)
            foreach (var neighbor in adjacencyList[i])
            {
                adjacencyMatrix[i, neighbor] = 1;
                adjacencyMatrix[neighbor, i] = 1;
            }

        return adjacencyMatrix;
    }

    private static int[,] BuildAdjacencyMatrix((int u, int v)[] edgeList, int vertexCount)
    {
        var adjacencyMatrix = new int[vertexCount, vertexCount];

        foreach (var (u, v) in edgeList)
        {
            adjacencyMatrix[u, v] = 1;
            adjacencyMatrix[v, u] = 1;
        }

        return adjacencyMatrix;
    }

    private static int[,] BuildDistanceMatrixFrom(int[,] adjacencyMatrix, int vertexCount)
    {
        var distanceMatrix = InitializeDistanceMatrix(adjacencyMatrix, vertexCount);

        for (var k = 0; k < vertexCount; k++)
            for (var i = 0; i < vertexCount; i++)
                for (var j = 0; j < vertexCount; j++)
                    if (distanceMatrix[i, k] + distanceMatrix[k, j] < distanceMatrix[i, j])
                        distanceMatrix[i, j] = distanceMatrix[i, k] + distanceMatrix[k, j];

        return distanceMatrix;
    }

    private static int[,] InitializeDistanceMatrix(int[,] adjacencyMatrix, int vertexCount)
    {
        var dist = new int[vertexCount, vertexCount];
        
        for (var i = 0; i < vertexCount; i++)
        for (var j = 0; j < i; j++)
        {
            if (adjacencyMatrix[i, j] == 0)
            {
                dist[i, j] = Infinity;
                dist[j, i] = Infinity;
            }
            else
            {
                dist[i, j] = adjacencyMatrix[i, j];
                dist[j, i] = adjacencyMatrix[i, j];
            }
        }

        return dist;
    }

    public int[,] GetAdjacencyMatrix()
    {
        return adjacencyMatrix;
    }

    public int[,] GetDistanceMatrix()
    {
        return distanceMatrix;
    }

    public void PrintDistanceMatrix()
    {
        Console.WriteLine("    Матриця відстаней");

        Console.Write("   ");
        for (var i = 0; i < vertexCount; i++)
        {
            var vertexName = $"v{i}";
            Console.Write(vertexName.PadLeft(3));
            Console.Write(" ");
        }
        Console.Write("e".PadLeft(3));
        Console.WriteLine();

        for (var i = 0; i < vertexCount; i++)
        {
            var vertexName = $"v{i}";
            Console.Write(vertexName.PadLeft(3));

            var excentricitet = 0;
            for (var j = 0; j < vertexCount; j++)
            {
                var value = distanceMatrix[i, j];
                if (excentricitet < value)
                    excentricitet = value;
                Console.Write(value == Infinity ? "∞".PadLeft(3) : value.ToString().PadLeft(3));
                Console.Write(" ");
            }

            Console.Write($"{excentricitet}".PadLeft(3));
            Console.WriteLine();
        }
    }

    public int GetDiameter()
    {
        var max = 0;
        for (var i = 0; i < vertexCount; i++)
            for (var j = 0; j < vertexCount; j++)
                if (distanceMatrix[i, j] < Infinity && distanceMatrix[i, j] > max)
                    max = distanceMatrix[i, j];
        return max;
    }

    public int GetRadius()
    {
        var radius = Infinity;

        for (var i = 0; i < vertexCount; i++)
        {
            var ecc = 0;
            for (var j = 0; j < vertexCount; j++)
                ecc = Math.Max(ecc, distanceMatrix[i, j]);
            radius = Math.Min(radius, ecc);
        }

        return radius;
    }

    public List<int> GetCentralVertices()
    {
        var result = new List<int>();
        var radius = GetRadius();

        for (var i = 0; i < vertexCount; i++)
        {
            var ecc = 0;
            for (var j = 0; j < vertexCount; j++)
                ecc = Math.Max(ecc, distanceMatrix[i, j]);
            if (ecc == radius)
                result.Add(i);
        }

        return result;
    }

    public List<int> GetPeripheralVertices()
    {
        var result = new List<int>();
        var diameter = GetDiameter();

        for (var i = 0; i < vertexCount; i++)
        {
            var ecc = 0;
            for (var j = 0; j < vertexCount; j++)
                ecc = Math.Max(ecc, distanceMatrix[i, j]);
            if (ecc == diameter)
                result.Add(i);
        }

        return result;
    }
}
