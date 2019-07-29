namespace RequestManager.Model
{
    public class Package
    {
        public string Name { get; set; }
        public PackageType Type { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, Type: {Type}";
        }
    }
}