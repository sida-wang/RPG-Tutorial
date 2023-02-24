﻿using Engine.Factories;
using Engine.Models;

namespace Engine.ViewModels
{
    public class GameSession : ObservableObject
    {
        private Location? _currentLocation;
        public Player CurrentPlayer { get; set; }
        public Location? CurrentLocation
        {
            get => _currentLocation;
            set
            {
                _currentLocation = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasLocationToNorth));
                OnPropertyChanged(nameof(HasLocationToSouth));
                OnPropertyChanged(nameof(HasLocationToWest));
                OnPropertyChanged(nameof(HasLocationToEast));
            }
        }
        public World CurrentWorld { get; set; }

        public bool HasLocationToNorth
        {
            get => CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1) != null;
        }
        public bool HasLocationToSouth
        {
            get => CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1) != null;
        }
        public bool HasLocationToWest
        {
            get => CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate) != null;
        }
        public bool HasLocationToEast
        {
            get => CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate) != null;
        }

        public GameSession()
        {
            CurrentPlayer = new Player
            {
                Name = "Sida",
                Gold = 1000000,
                CharacterClass = "Ranger",
                HitPoints = 10,
                ExperiencePoints = 0,
                Level = 1,
            };

            WorldFactory factory = new();
            CurrentWorld = factory.CreateWorld();

            CurrentLocation = CurrentWorld.LocationAt(0, -1);
        }

        public void MoveNorth()
        {
            CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1);
        }
        public void MoveSouth()
        {
            CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1);
        }
        public void MoveEast()
        {
            CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate);
        }
        public void MoveWest()
        {
            CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate);
        }

    }
}
