﻿using Engine.Models;
using System.Security.Cryptography;

namespace Engine.Factories
{
    public class MonsterFactory
    {
        public static Monster GetMonster(int monsterID)
        {
            switch (monsterID)
            {
                case 1:
                    Monster snake = new("Snake", Images.Images.Snake, 4, 4, 1, 2, 5, 1);
                    AddLootItem(snake, 9001, 25);
                    AddLootItem(snake, 9002, 75);
                    return snake;
                case 2:
                    Monster rat = new("Rat", Images.Images.Rat, 5, 5, 1, 2, 5, 1);
                    AddLootItem(rat, 9003, 25);
                    AddLootItem(rat, 9004, 75);
                    return rat;
                case 3:
                    Monster giantSpider = new("Giant Spider", Images.Images.GiantSpider, 10, 10, 1, 4, 10, 3);
                    AddLootItem(giantSpider, 9005, 25);
                    AddLootItem(giantSpider, 9006, 75);
                    return giantSpider;
                default:
                    throw new ArgumentException($"MonsterType {monsterID} does not exist.");
            }
        }

        private static void AddLootItem(Monster monster, int itemID, int percentage)
        {
            if (RandomNumberGenerator.GetInt32(1, 101) <= percentage)
            {
                monster.Inventory.Add(new ItemQuantity(itemID, 1));
            }
        }


    }
}
