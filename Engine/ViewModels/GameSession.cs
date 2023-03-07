using Engine.Factories;
using Engine.Models;

namespace Engine.ViewModels
{
    public class GameSession : ObservableObject
    {
        private Location _currentLocation;
        public Player CurrentPlayer { get; set; }
        public Location CurrentLocation
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
                GivePlayerQuestsAtLocation();
            }
        }
        public World CurrentWorld { get; set; }

        public bool HasLocationToNorth
        {
            get => CurrentWorld.LocationExistsAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1);
        }
        public bool HasLocationToSouth
        {
            get => CurrentWorld.LocationExistsAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1);
        }
        public bool HasLocationToWest
        {
            get => CurrentWorld.LocationExistsAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate);
        }
        public bool HasLocationToEast
        {
            get => CurrentWorld.LocationExistsAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate);
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

            CurrentWorld = WorldFactory.CreateWorld();

            _currentLocation = CurrentWorld.LocationAt(0, -1);
        }

        public void MoveNorth()
        {
            if (HasLocationToNorth)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1);
            }
        }
        public void MoveSouth()
        {
            if (HasLocationToSouth)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1);
            }
        }
        public void MoveEast()
        {
            if (HasLocationToEast)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate);
            }
        }
        public void MoveWest()
        {
            if (HasLocationToWest)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate);
            }
        }

        private void GivePlayerQuestsAtLocation()
        {
            foreach (Quest quest in CurrentLocation.QuestsAvailableHere)
            {
                if (!CurrentPlayer.Quests.Any(q => q.PlayerQuest.ID == quest.ID))
                {
                    CurrentPlayer.Quests.Add(new QuestStatus(quest));
                }
            }
        }

    }
}
