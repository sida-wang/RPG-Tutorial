using Engine.EventArgs;
using Engine.Factories;
using Engine.Models;
using System.Security.Cryptography;

namespace Engine.ViewModels
{
    public class GameSession : ObservableObject
    {
        public event EventHandler<GameMessageEventArgs>? OnMessageRaised;
        private Location _currentLocation;
        private Monster? _currentMonster;
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
                GetMonsterAtLocation();
            }
        }
        public Monster? CurrentMonster
        {
            get => _currentMonster;
            set
            {
                _currentMonster = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasMonster));
                if (CurrentMonster != null)
                {
                    RaiseMessage("");
                    RaiseMessage($"You see a {CurrentMonster.Name}");
                }
            }
        }
        public Weapon? CurrentWeapon { get; set; }
        public World CurrentWorld { get; set; }

        public bool HasLocationToNorth => CurrentWorld.LocationExistsAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1);
        public bool HasLocationToSouth => CurrentWorld.LocationExistsAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1);
        public bool HasLocationToWest => CurrentWorld.LocationExistsAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate);
        public bool HasLocationToEast => CurrentWorld.LocationExistsAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate);
        public bool HasMonster => CurrentMonster != null;

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
            if (!CurrentPlayer.Weapons.Any())
            {
                CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(1001)!);
            }

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
        private void GetMonsterAtLocation()
        {
            CurrentMonster = CurrentLocation.GetMonster();
        }

        public void AttackCurrentMonster()
        {
            if (CurrentWeapon == null)
            {
                RaiseMessage("You must select a weapon to attack.");
                return;
            }

            int damageToMonster = RandomNumberGenerator.GetInt32(CurrentWeapon.MinimumDamage, CurrentWeapon.MaximumDamage + 1);
            if (damageToMonster == 0)
            {
                RaiseMessage($"You missed the {CurrentMonster?.Name}.");
            }
            else
            {
                CurrentMonster!.HitPoints -= damageToMonster;
                RaiseMessage($"You hit the {CurrentMonster?.Name} for {damageToMonster} damage.");
            }

            if (CurrentMonster?.HitPoints <= 0)
            {
                RaiseMessage("");
                RaiseMessage($"You defeated the {CurrentMonster.Name}!");
                CurrentPlayer.ExperiencePoints += CurrentMonster.RewardExperiencePoints;
                RaiseMessage($"You received {CurrentMonster.RewardExperiencePoints} exp.");
                CurrentPlayer.Gold += CurrentMonster.RewardGold;
                RaiseMessage($"You received {CurrentMonster.RewardGold} gold.");
                foreach (ItemQuantity itemQuantity in CurrentMonster.Inventory)
                {
                    GameItem item = ItemFactory.CreateGameItem(itemQuantity.ItemID)!;
                    CurrentPlayer.AddItemToInventory(item);
                    RaiseMessage($"You receieved {itemQuantity.Quantity} {item.Name}.");
                }
                GetMonsterAtLocation();
            }
            else
            {
                int damageToPlayer = RandomNumberGenerator.GetInt32(CurrentMonster!.MinimumDamage, CurrentMonster!.MaximumDamage + 1);
                if (damageToPlayer == 0)
                {
                    RaiseMessage("The monster attacks, but misses you.");
                }
                else
                {
                    CurrentPlayer.HitPoints -= damageToPlayer;
                    RaiseMessage($"The {CurrentMonster.Name} hit you for {damageToPlayer} damage.");
                }
                if (CurrentPlayer.HitPoints <= 0)
                {
                    RaiseMessage("");
                    RaiseMessage($"The {CurrentMonster.Name} killed you.");
                    CurrentLocation = CurrentWorld.LocationAt(0, -1);
                    CurrentPlayer.HitPoints = CurrentPlayer.Level * 10;
                }
            }

        }

        private void RaiseMessage(string message)
        {
            OnMessageRaised?.Invoke(this, new GameMessageEventArgs(message));
        }

    }
}
