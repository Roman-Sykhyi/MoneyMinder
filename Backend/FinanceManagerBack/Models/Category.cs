using System;

namespace FinanceManagerBack.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public bool IsDefault { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Category category &&
                   Id == category.Id &&
                   Name == category.Name &&
                   Icon == category.Icon &&
                   IsDefault == category.IsDefault;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Icon, IsDefault);
        }
    }
}