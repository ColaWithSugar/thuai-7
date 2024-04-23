using GameServer.GameLogic;
using GameServer.Geometry.Shapes;

namespace GameServer.Geometry;

public class CollisionDetector
{
    public static double CrossProduct(Position a, Position b, Position c)
    {
        return (b.x - a.x) * (c.y - a.y) - (c.x - a.x) * (b.y - a.y);
    }

    public static bool AreOnSameSide(Position a, Position b, Position[] positions)
    {
        double abCrossProduct = CrossProduct(a, b, positions[0]);
        for (int i = 1; i < positions.Length; i++)
        {
            double currentCrossProduct = CrossProduct(a, b, positions[i]);
            if (currentCrossProduct * abCrossProduct < 0)
            {
                return false; // Points are not on the same side
            }
        }
        return true; // All points are on the same side
    }

    public static bool CheckCross(Position a, Position b, int i, int j)
    {
        Position[] positions = new Position[]
        {
            new Position(i, j),
            new Position(i, j + 1),
            new Position(i + 1, j),
            new Position(i + 1, j + 1)
        };
        if (!AreOnSameSide(a, b, positions))
        {
            return false;
        }
        return true;
    }

    public static bool IsCrossing(Segment segment, Circle circle)
    {
        Position a = segment.Start;
        Position b = segment.End;
        Position c = circle.Center;

        Position direction = new Position(b.x - a.x, b.y - a.y).Normalize();
        double distance = Position.Distance(a, b);
        double step = 0.02;
        Position currentPosition = a;
        while (distance > 0)
        {
            if (distance < step)
            {
                return Position.Distance(currentPosition, c) < circle.Radius;
            }
            currentPosition += direction * step;
            distance -= step;
            if (Position.Distance(currentPosition, c) < circle.Radius)
            {
                return true;
            }
        }
        return Position.Distance(currentPosition, c) < circle.Radius;
    }
}
