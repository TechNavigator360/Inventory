namespace Inventory.Domain.Entities
{
    public class Location
    {
        private readonly Guid _id;
        private readonly string _name;
        private readonly int _capacity;

        public Guid Id => _id;
        public string Name => _name;
        public int Capacity => _capacity;

        public Location(Guid id, string name, int capacity)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id mag niet leeg zijn.", nameof(id));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), "Capaciteit moet groter zijn dan nul.");

            _id = id;
            _name = name;
            _capacity = capacity;
        }
    }
}