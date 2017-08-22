using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3Core.Models
{
    public class Item: IEquatable<Item>, IEqualityComparer<Item>
    {
        public string Name { get; set; }
        public string Slot { get; set; }
        public Build Build { get; set; }
        public bool IsOwn => ItemStash.OwnedItems.Contains(Name);

        public bool Equals(Item other)
        {
            return other != null && other.Name == Name;
        }

        public bool Equals(Item x, Item y)
        {
            return x != null && y != null && x.Name == y.Name;
        }

        public int GetHashCode(Item obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
