using System;
namespace Vendingmachine
{
	public class Vendingmachine
	{
		private Dictionary<string, Item> _items;
		private decimal _balance;
		private bool _isAdminMode;


		public Vendingmachine()
		{
			_items = new Dictionary<string, Item>();
			_balance = 0;
            _isAdminMode = false;

            //items
            _items.Add("1", new Item("Coke", 1.00m, 10));
            _items.Add("2", new Item("Pepsi", 1.00m, 10));
            _items.Add("3", new Item("Faxe Kondi", 0.50m, 10));
            _items.Add("4", new Item("Skumbanan", 0.25m, 100));
        }

		public void Run()
		{
			while (true)
			{
				Console.Clear();
                Console.WriteLine("----------------");
                Console.WriteLine("1. Insert Coin");
                Console.WriteLine("2. Select Item");
                Console.WriteLine("3. Cancel Purchase");
                Console.WriteLine("4. Admin Menu");
                Console.WriteLine("5. Exit");

                Console.Write("Choose an option: ");
                string option = Console.ReadLine();

				switch (option)
				{
                    case "1":
                        InsertCoin();
                        break;
                    case "2":
                        SelectItem();
                        break;
                    case "3":
                        CancelPurchase();
                        break;
                    case "4":
                        AdminMenu();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please choose a valid option.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;

                }

            }

		}
        private void InsertCoin()
        {
            Console.Write("Insert coin (0.25, 0.50, 1.00): ");
            string coin = Console.ReadLine();

            decimal amount;
            if (decimal.TryParse(coin, out amount))
            {
                if (amount == 0.25m || amount == 0.50m || amount == 1.00m)
                {
                    _balance += amount;
                    Console.WriteLine($"Coin inserted. Balance: {_balance:C}");
                }
                else
                {
                    Console.WriteLine("Invalid coin amount. Please insert a valid coin.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please insert a valid coin amount.");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void SelectItem()
        {
            Console.WriteLine("Select an item:");
            foreach (KeyValuePair<string, Item> item in _items)
            {
                Console.WriteLine($"{item.Key}. {item.Value.Name} - {item.Value.Price:C}");
            }

            Console.Write("Choose an item: ");
            string selectedItem = Console.ReadLine();

            if (_items.TryGetValue(selectedItem, out Item itemValue))
            {
                if (itemValue.Quantity > 0)
                {
                    if (_balance >= itemValue.Price)
                    {
                        _balance -= itemValue.Price;
                        itemValue.Quantity--;
                        Console.WriteLine($"Item dispensed. Balance: {_balance:C}");
                    }
                    else
                    {
                        Console.WriteLine("Insufficient balance. Please insert more coins.");
                    }
                }
                else
                {
                    Console.WriteLine("Item out of stock. Please choose another item.");
                }
            }
            else
            {
                Console.WriteLine("Invalid item selection. Please choose a valid item.");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void CancelPurchase()
        {
            Console.WriteLine($"Purchase cancelled. Refund: {_balance:C}");
            _balance = 0;

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        
        private void AdminMenu()
        {
            Console.WriteLine("Admin Menu");
            Console.WriteLine("---------");
            Console.WriteLine("1. Restock Items");
            Console.WriteLine("2. Remove Money");
            Console.WriteLine("3. Adjust Prices");
            Console.WriteLine("4. Exit Admin Menu");

            Console.Write("Choose an option: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    RestockItems();
                    break;
                case "2":
                    RemoveMoney();
                    break;
                case "3":
                    AdjustPrices();
                    break;
                case "4":
                    _isAdminMode = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please choose a valid option.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }

        private void RestockItems()
        {
            Console.WriteLine("Restock Items");
            Console.WriteLine("-------------");

            foreach (KeyValuePair<string, Item> item in _items)
            {
                Console.WriteLine($"Enter quantity for {item.Value.Name} (current quantity: {item.Value.Quantity}): ");
                string quantity = Console.ReadLine();

                int newQuantity;
                if (int.TryParse(quantity, out newQuantity))
                {
                    item.Value.Quantity = newQuantity;
                    Console.WriteLine($"Quantity for {item.Value.Name} updated to {newQuantity}.");
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid quantity.");
                }
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void RemoveMoney()
        {
            Console.WriteLine("Remove Money");
            Console.WriteLine("------------");

            Console.WriteLine($"Current balance: {_balance:C}");
            Console.Write("Enter amount to remove: ");
            string amount = Console.ReadLine();

            decimal removeAmount;
            if (decimal.TryParse(amount, out removeAmount))
            {
                if (removeAmount <= _balance)
                {
                    _balance -= removeAmount;
                    Console.WriteLine($"Amount removed. New balance: {_balance:C}");
                }
                else
                {
                    Console.WriteLine("Cannot remove more money than the current balance.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid amount.");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void AdjustPrices()
        {
            Console.WriteLine("Adjust Prices");
            Console.WriteLine("------------");

            foreach (KeyValuePair<string, Item> item in _items)
            {
                Console.WriteLine($"Enter new price for {item.Value.Name} (current price: {item.Value.Price:C}): ");
                string price = Console.ReadLine();

                decimal newPrice;
                if (decimal.TryParse(price, out newPrice))
                {
                    item.Value.Price = newPrice;
                    Console.WriteLine($"Price for {item.Value.Name} updated to {newPrice:C}.");
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid price.");
                }
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public class Item
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Item(string name, decimal price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }
    }

}
}

