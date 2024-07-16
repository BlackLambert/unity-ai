using System;

namespace SBaier.AI
{
    public abstract class NodeBase : MutableNode, IEquatable<Node>
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public abstract bool Execute();
        
        public virtual string GetInfo()
        {
            return $"{Name}";
        }

        public bool Equals(Node other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Node)obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public override string ToString()
        {
            return $"{GetType()} Node '{Name ?? string.Empty}' (Id: {Id})";
        }
    }
}