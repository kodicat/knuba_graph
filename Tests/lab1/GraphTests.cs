using FluentAssertions;
using FluentAssertions.Execution;
using Labs.lab1;

namespace Tests.lab1;

[TestFixture]
public class GraphTests
{
    private readonly int[,] adjacencyMatrix =
    {
        // 0  1  2  3  4  5
        { 0, 1, 0, 1, 0, 1 }, // 0
        { 1, 0, 0, 0, 0, 1 }, // 1
        { 0, 0, 0, 1, 0, 1 }, // 2
        { 1, 0, 1, 0, 1, 1 }, // 3
        { 0, 0, 0, 1, 0, 1 }, // 4
        { 1, 1, 1, 1, 1, 0 }, // 5
    };

    private readonly int[][] adjacencyList =
    [
        [1, 3, 5],
        [0, 5],
        [3, 5],
        [0, 2, 4, 5],
        [3, 5],
        [0, 1, 2, 3, 4],
    ];

    private readonly (int u, int v)[] edgeList =
    [
        (0, 1),
        (0, 3),
        (0, 5),
        (1, 5),
        (2, 3),
        (2, 5),
        (3, 4),
        (3, 5),
        (4, 5),
    ];

    [Test]
    public void TestDistanceMatrixIsTheSame()
    {
        var graph1 = new Graph(adjacencyMatrix);
        var adjacencyMatrix1 = graph1.GetAdjacencyMatrix();
        var distanceMatrix1 = graph1.GetDistanceMatrix();

        var graph2 = new Graph(adjacencyList);
        var adjacencyMatrix2 = graph2.GetAdjacencyMatrix();
        var distanceMatrix2 = graph2.GetDistanceMatrix();

        var graph3 = new Graph(edgeList, 6);
        var adjacencyMatrix3 = graph3.GetAdjacencyMatrix();
        var distanceMatrix3 = graph3.GetDistanceMatrix();

        using (new AssertionScope());
        adjacencyMatrix1.Should().BeEquivalentTo(adjacencyMatrix2);
        adjacencyMatrix1.Should().BeEquivalentTo(adjacencyMatrix3);
        distanceMatrix1.Should().BeEquivalentTo(distanceMatrix2);
        distanceMatrix1.Should().BeEquivalentTo(distanceMatrix3);
    }

    [Test]
    public void TestDistanceMatrixProperties()
    {
        var graph = new Graph(edgeList, 6);

        var radius = graph.GetRadius();
        var diameter = graph.GetDiameter();

        var centralVertices = graph.GetCentralVertices();
        var peripheralVertices = graph.GetPeripheralVertices();

        using var scope = new AssertionScope();
        radius.Should().Be(1);
        diameter.Should().Be(2);

        centralVertices.Should().BeEquivalentTo([5]);
        peripheralVertices.Should().BeEquivalentTo([0, 1, 2, 3, 4]);
    }
    
    [Test]
    public void PrintDistanceMatrix()
    {
        var graph = new Graph(edgeList, 6);
        graph.PrintDistanceMatrix();
    }
}
