﻿using System.Collections.ObjectModel;

namespace Engine.Models
{
    public class Player : ObservableObject
    {
        private string? _name;
        private string? _characterClass;
        private int _experiencePoints;
        private int _level;
        private int _gold;
        private int _hitPoints;


        public string? Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string? CharacterClass
        {
            get => _characterClass;
            set
            {
                _characterClass = value;
                OnPropertyChanged(nameof(CharacterClass));
            }
        }
        public int HitPoints
        {
            get => _hitPoints;
            set
            {
                _hitPoints = value;
                OnPropertyChanged(nameof(HitPoints));
            }
        }
        public int ExperiencePoints
        {
            get => _experiencePoints;
            set
            {
                _experiencePoints = value;
                OnPropertyChanged(nameof(ExperiencePoints));
            }
        }
        public int Level
        {
            get => _level;
            set
            {
                _level = value;
                OnPropertyChanged(nameof(Level));
            }
        }
        public int Gold
        {
            get => _gold;
            set
            {
                _gold = value;
                OnPropertyChanged(nameof(Gold));
            }
        }
        public ObservableCollection<GameItem> Inventory { get; set; }
        public ObservableCollection<QuestStatus> Quests { get; set; }
        public List<GameItem> Weapons => Inventory.Where(i => i is Weapon).ToList();
        public Player()
        {
            Inventory = new();
            Quests = new();
        }

        public void AddItemToInventory(GameItem item)
        {
            Inventory.Add(item);
            OnPropertyChanged(nameof(Weapons));
        }

        public void RemoveItemFromInventory(GameItem item)
        {
            Inventory.Remove(item);
            OnPropertyChanged(nameof(Weapons));
        }

        public bool HasAllTheseItems(List<ItemQuantity> list)
        {
            foreach (ItemQuantity item in list)
            {
                if (Inventory.Count(i => i.ItemTypeID == item.ItemID) < item.Quantity)
                {
                    return false;
                }
            }
            return true;
        }

    }
}

