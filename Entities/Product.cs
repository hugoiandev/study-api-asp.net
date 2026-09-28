namespace study_api.Entities
{
    public class Product
    {
        public Product() { }

        public Product(int Id, string Name, string Color, decimal Price, int Stock)
        {
            this.Id = Id;
            this.Name = Name;
            this.Color = Color;
            this.Price = Price;
            this.Stock = Stock;
        }


        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
