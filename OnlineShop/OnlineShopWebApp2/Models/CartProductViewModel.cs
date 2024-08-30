namespace OnlineShopWebApp.Models
{
    public class CartProductViewModel
    {
        public ProductViewModel Product { get; set; }

        public int Amount {  get; set; }

        public decimal Cost 
        {
            get => Product.Cost * Amount;
        }

        public int Bonus
        {

            get => Product.Bonus * Amount;
        }
    }
}
