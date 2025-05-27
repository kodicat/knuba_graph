using FluentAssertions;
using Labs.lab2;

namespace Tests.lab2;

public class GraphTests
{
    private readonly (int u, int v, int weight)[] edgesList =
    [
        (0, 2, 12),
        (0, 3, 10),
        (1, 3, 8),
        (1, 4, 7),
        (2, 5, 18),
        (3, 2, 5),
        (3, 4, 15),
        (3, 6, 5),
        (3, 8, 5),
        (4, 9, 3),
        (4, 11, 12),
        (5, 6, 4),
        (5, 10, 20),
        (6, 10, 4),
        (7, 6, 1),
        (7, 12, 4),
        (7, 13, 4),
        (8, 7, 6),
        (8, 14, 2),
        (9, 8, 20),
        (10, 11, 17),
        (12, 11, 16),
        (13, 12, 2),
        (14, 9, 1),
        (14, 13, 1),
    ];

    [Test]
    public void TestMinimumVertexCoverSolver()
    {
        var solver = new Graph(edgesList);

        var result = solver.Solve();

        result.Should().BeEquivalentTo([0, 3, 4, 5, 7, 8, 10, 12, 14]);
    }

    [Test]
    public void TestMinimumEdgeCoverSolver()
    {
        var solver = new MinimumEdgeCoverSolver(edgesList);

        var result = solver.Solve();

        result.Should().BeEquivalentTo([(0, 2), (0, 3), (1, 4), (5, 6), (7, 12), (9, 8), (10, 11), (14, 13)]);
    }

    [Test]
    public void TestGraphColoring()
    {
        var solver = new GraphColoring(edgesList);

        var result = solver.ColorVertices();

        var expected = new Dictionary<int, int>()
        {
            [0] = 0,
            [1] = 0,
            [2] = 1,
            [3] = 2,
            [4] = 1,
            [5] = 0,
            [6] = 1,
            [7] = 0,
            [8] = 1,
            [9] = 0,
            [10] = 2,
            [11] = 0,
            [12] = 1,
            [13] = 2,
            [14] = 3,
        };
        result.Should().BeEquivalentTo(expected);
    }
}