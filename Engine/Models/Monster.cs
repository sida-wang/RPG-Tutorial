using System.Collections.ObjectModel;

namespace Engine.Models
{
    public class Monster : ObservableObject
    {
        private int _hitPoints;
        public string Name { get; private set; }
        public byte[] Image { get; set; }
        public int MaximumHitPoints { get; private set; }
        public int HitPoints
        {
            get => _hitPoints;
            set
            {
                _hitPoints = value;
                OnPropertyChanged();
            }
        }
        public int MinimumDamage { get; set; }
        public int MaximumDamage { get; set; }
        public int RewardExperiencePoints { get; private set; }
        public int RewardGold { get; private set; }
        public ObservableCollection<ItemQuantity> Inventory { get; set; }
        public Monster(string name, byte[] image, int maximumHitPoints,
            int hitPoints, int minimumDamage, int maximumDamage, int rewardExperiencePoints, int rewardGold)
        {
            Name = name;
            Image = image;
            MaximumHitPoints = maximumHitPoints;
            HitPoints = hitPoints;
            MinimumDamage = minimumDamage;
            MaximumDamage = maximumDamage;
            RewardExperiencePoints = rewardExperiencePoints;
            RewardGold = rewardGold;
            Inventory = new();
        }

    }
}
