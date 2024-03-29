﻿namespace Engine.Models
{
    public class World
    {
        private readonly List<Location> _locations = new();

        internal void AddLocation(int xCoordinate, int yCoordinate, string name, string description, byte[] image)
        {
            Location loc = new()
            {
                XCoordinate = xCoordinate,
                YCoordinate = yCoordinate,
                Name = name,
                Description = description,
                Image = image
            };

            _locations.Add(loc);
        }

        public Location LocationAt(int xCoordinate, int yCoordinate)
        {
            foreach (Location loc in _locations)
            {
                if (loc.XCoordinate == xCoordinate && loc.YCoordinate == yCoordinate)
                {
                    return loc;
                }
            }
            throw new ArgumentException($"Location at x:{xCoordinate} y:{yCoordinate} does not exist");
        }

        public bool LocationExistsAt(int xCoordinate, int yCoordinate)
        {
            try
            {
                LocationAt(xCoordinate, yCoordinate);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
    }
}
