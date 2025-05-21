namespace Blastangel
{
    internal abstract class Item
    {
        string _symbol;
        public string Symbol { get { return _symbol; } set { _symbol = value; } }
        public string Name { get; set; }
        public Item(string name, string sym) { }
    }
}
