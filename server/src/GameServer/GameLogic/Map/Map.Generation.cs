using GameServer.Geometry;

namespace GameServer.GameLogic;

public partial class Map
{
    public void GenerateMap()
    {
        GenerateWalls();
        GenerateSupplies();
    }

    public void GenerateSupplies()
    {
        List<string> weaponNames =
        [
            Constant.Names.S686,
            Constant.Names.M16,
            Constant.Names.VECTOR,
            Constant.Names.AWM
        ];

        List<string> medicineNames =
        [
            Constant.Names.BANDAGE,
            Constant.Names.FIRST_AID
        ];

        List<string> armorNames =
        [
            Constant.Names.PRIMARY_ARMOR,
            Constant.Names.PREMIUM_ARMOR
        ];

        List<string> grenadeNames =
        [
            Constant.Names.GRENADE
        ];

        Dictionary<string, List<string>> supplyNames = new()
        {
            { "weapon", weaponNames },
            { "medicine", medicineNames },
            { "armor", armorNames },
            { "grenade", grenadeNames }
        };

        Dictionary<string, double> allAvailableSupplyProba = new()
        {
            { "weapon", _random.NextDouble() },
            { "medicine", _random.NextDouble() },
            { "armor", _random.NextDouble() },
            { "grenade", _random.NextDouble() }
        };

        // Normalize the probabilities
        double sum = allAvailableSupplyProba.Values.Sum();
        foreach (string key in allAvailableSupplyProba.Keys.ToList())
        {
            allAvailableSupplyProba[key] /= sum;
        }

        List<string> allAvailableSupplies = [];

        for (int i = 0; i < 1000; i++)
        {
            // Random choose a type of item by its probability
            double randomValue = _random.NextDouble();
            string itemType = allAvailableSupplyProba.First(x => x.Value >= randomValue).Key;

            // Random choose a specific item
            string itemSpecificName = supplyNames[itemType][_random.Next(0, supplyNames[itemType].Count)];

            // Add the item to the list
            allAvailableSupplies.Add(itemSpecificName);
        }

        // Iterate to generate the desired number of supply points
        for (int i = 0; i < _numSupplyPoints; i++)
        {
            Position nextPosition = GenerateValidPosition();

            string itemSpecificName = allAvailableSupplies[_random.Next(0, allAvailableSupplies.Count)];
            IItem.ItemKind itemType = IItem.GetItemKind(itemSpecificName);

            (int, int) range = GetItemCountRange(itemSpecificName);
            int itemCount = _random.Next(range.Item1, range.Item2 + 1);

            // Add the generated item to the supply point
            AddSupplies(
                (int)nextPosition.x,
                (int)nextPosition.y,
                new Item(
                    itemType, itemSpecificName, IItem.AllowPileUp(itemType) ? itemCount : 1
                )
            );

            if (itemType == IItem.ItemKind.Weapon)
            {
                (int, int) bulletCountRange = GetItemCountRange(Constant.Names.BULLET);
                int bulletCount = _random.Next(bulletCountRange.Item1, bulletCountRange.Item2 + 1);

                AddSupplies(
                    (int)nextPosition.x,
                    (int)nextPosition.y,
                    new Item(IItem.ItemKind.Bullet, Constant.Names.BULLET, bulletCount)
                );
            }
        }
    }

    public void GenerateWalls()
    {
        // Clear the map
        Clear();

        // Iterate _tryTimes and place them on the map
        for (int i = 0; i < _tryTimes; i++)
        {
            // Randomly select an obstacle shape
            ObstacleShape shape = _obstacleShapes[_random.Next(0, _obstacleShapes.Count)];

            // Place the obstacle shape on the map
            if (PlaceObstacleShape(shape))
            {
                // Successfully placed the obstacle shape
                // Continue to the next iteration
                continue;
            }
        }
    }

    public void Clear()
    {
        // Clear the map
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                MapChunk[x, y] = new Block(false);
            }
        }
    }

    private bool PlaceObstacleShape(ObstacleShape shape)
    {
        // Randomly select position for the obstacle shape
        int startX = _random.Next(0, Width - shape.MaxWidth);
        int startY = _random.Next(0, Height - shape.MaxHeight);

        // Check if the selected position is valid
        if (IsPositionValid(startX, startY, shape))
        {
            // Place the obstacle shape on the map
            for (int x = 0; x < shape.MaxWidth; x++)
            {
                for (int y = 0; y < shape.MaxHeight; y++)
                {
                    if (shape.IsSolid(x, y))
                    {
                        MapChunk[startX + x, startY + y] = shape.GetBlock(x, y);
                    }
                }
            }

            return true;
        }

        return false;
    }

    private bool IsPositionValid(int startX, int startY, ObstacleShape shape)
    {
        // Check if the position is within the map boundaries
        if (startX < 0 || startY < 0 || startX + shape.MaxWidth >= Width || startY + shape.MaxHeight >= Height)
        {
            return false;
        }

        // Check if the position overlaps with existing obstacles
        for (int x = 0; x < shape.MaxWidth; x++)
        {
            for (int y = 0; y < shape.MaxHeight; y++)
            {
                if (MapChunk[startX + x, startY + y] != null && MapChunk[startX + x, startY + y].IsWall)
                {
                    return false;
                }
            }
        }

        // Check if the position is reachable from the rest of the map
        // Implement connectivity check here (optional)

        return true;
    }

    private (int, int) GetItemCountRange(string itemName)
    {
        return itemName switch
        {
            Constant.Names.BANDAGE => (1, 10),
            Constant.Names.FIRST_AID => (1, 2),
            Constant.Names.BULLET => (5, 15),
            _ => (_minItemsPerSupply, _maxItemsPerSupply)
        };
    }
}
