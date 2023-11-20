// Mini Project - Asset Tracking

List <Asset> items = new List<Asset> ();

showMainMenu();

void AddAsset()
{
    while(true)
    {
        int flag = 0; // Flag to record any invalid entries
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("To enter a new Asset - follow the steps | To Quit Enter : 'Q'");
        Console.ResetColor();

        //Entering Asset Type info
        Console.Write("Enter the type of Asset Computer/Phone : ");
        string type = Console.ReadLine();
        if (string.IsNullOrEmpty(type))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("This is an invalid entry.");
            Console.ResetColor();
            flag = 1;
        }
        else if(type.ToLower().Trim() == "q")
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Exiting the Add Assets application.");
            Console.ResetColor();
            break;
        }
        else if (!(type.ToLower().Trim() == "computer" || type.ToLower().Trim() == "phone"))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("This is an invalid entry.");
            Console.ResetColor();
            flag = 1;
        }

        //Entering Asset Brand info
        Console.Write("Enter the Brand of Asset : ");
        string brand = Console.ReadLine();
        if (string.IsNullOrEmpty(brand))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("This is an invalid entry.");
            Console.ResetColor();
            flag = 1;
        }
        else if (brand.ToLower().Trim() == "q")
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Exiting the Add Assets application.");
            Console.ResetColor();
            break;
        }

        //Entering Asset Model info
        Console.Write("Enter the Model of Asset : ");
        string model = Console.ReadLine();
        if (string.IsNullOrEmpty(model))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("This is an invalid entry.");
            Console.ResetColor();
            flag = 1;
        }
        else if (model.ToLower().Trim() == "q")
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Exiting the Add Assets application.");
            Console.ResetColor();
            break;
        }

        //Entering Asset Office info
        Console.Write("Enter the Office of Asset (Spain/Sweden/USA) : ");
        string office = Console.ReadLine();
        if (string.IsNullOrEmpty(office))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("This is an invalid entry.");
            Console.ResetColor();
            flag = 1;
        }
        else if (office.ToLower().Trim() == "q")
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Exiting the Add Assets application.");
            Console.ResetColor();
            break;
        }
        else if(!(office.ToLower().Trim() == "spain" || office.ToLower().Trim() == "sweden" || office.ToLower().Trim() == "usa"))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("This is an invalid entry.");
            Console.ResetColor();
            flag = 1;
        }

        //Entering Asset Price info
        Console.Write("Enter the Price of Asset (USD) : ");
        string price = Console.ReadLine();
        if (string.IsNullOrEmpty(price))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("This is an invalid entry.");
            Console.ResetColor();
            flag = 1;
        }
        else if (price.ToLower().Trim() == "q")
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Exiting the Add Assets application.");
            Console.ResetColor();
            break;
        }

        //Entering Asset Purchase Date info
        Console.Write("Enter the Purchase Date of Asset (mm/dd/yyyy): ");
        string purchase_date = Console.ReadLine();
        
        if (string.IsNullOrEmpty(purchase_date))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("This is an invalid entry.");
            Console.ResetColor();
            flag = 1;
        }
        else if (purchase_date.ToLower().Trim() == "q")
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Exiting the Add Assets application.");
            Console.ResetColor();
            break;
        }
        else
        {
            bool isValidDate = DateTime.TryParse(purchase_date, out DateTime out_purchase_date);
            if(!isValidDate) 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("This is an invalid entry.");
                Console.ResetColor();
                flag = 1;
            }
            if(out_purchase_date > DateTime.Today)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("This is an invalid entry. Purchase Date cannot be a future date.");
                Console.ResetColor();
                flag = 1;
            }

            if (flag == 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("There is an invalid entry. Please try again.");
                Console.ResetColor();
            }
            else
            {
                Asset asset = new Asset(type, brand, model, office, out_purchase_date, Convert.ToDouble(price), FindCurrency(office), FindLocalPrice(Convert.ToDouble(price), office));
                items.Add(asset);
                //Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("The Asset was successfully added to list.");
                Console.ResetColor();
            }       
        }
    }
    showMainMenu();
}

//Find the local price as per the office
double FindLocalPrice(double price, string office)
{
    switch (office.ToLower().Trim())
    {
        case "sweden":
            return price * 10.54;
        case "spain":
            return price * 0.92;
        default:
            return price;
    }
 }

//Find the currency as per the office
string FindCurrency(string office)
{
    switch(office.ToLower().Trim())
    {
        case "sweden":
            return "SEK";
        case "usa":
            return "USD";
        case "spain":
            return "EUR";
        default:
            return " ";
    }
}

void ListAsset()
{
    if(items.Count > 0)
    {
        //Sorted list by Office
        Console.WriteLine("----------------------------------");
        Console.WriteLine("Sorted Asset List by Office and Purchase Date");
        List<Asset> SortedAsset = items.OrderBy(Asset => Asset.Office).ThenBy(Asset => Asset.Purchase_Date).ToList();
        Console.WriteLine("Type".PadRight(15) + "Brand".PadRight(15) + "Model".PadRight(15) + "Office".PadRight(15) + "Purchase Date".PadRight(15) + "Price in USD".PadRight(15) + "Currency".PadRight(15)+"Local Price Today");
        foreach (Asset asset in SortedAsset)
        {
            DateTime EndOfLife_Date = asset.Purchase_Date.AddYears(3);
            
            if (DateTime.Today > EndOfLife_Date)
            {
                if (DateTime.Today <= EndOfLife_Date.AddDays(90))
                { 
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(asset.Type.ToUpper().PadRight(15) + asset.Brand.ToUpper().PadRight(15) + asset.Model.ToUpper().PadRight(15) + asset.Office.ToUpper().PadRight(15) + asset.Purchase_Date.ToString("MM-dd-yyyy").PadRight(15) + asset.Price_USD.ToString().PadRight(15) + asset.Currency.ToUpper().PadRight(15) + asset.Local_Price.ToString());
                    Console.ResetColor();
                }
                else if (DateTime.Today > EndOfLife_Date.AddDays(90) && DateTime.Today <= EndOfLife_Date.AddDays(180))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(asset.Type.ToUpper().PadRight(15) + asset.Brand.ToUpper().PadRight(15) + asset.Model.ToUpper().PadRight(15) + asset.Office.ToUpper().PadRight(15) + asset.Purchase_Date.ToString("MM-dd-yyyy").PadRight(15) + asset.Price_USD.ToString().PadRight(15) + asset.Currency.ToUpper().PadRight(15) + asset.Local_Price.ToString());
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine(asset.Type.ToUpper().PadRight(15) + asset.Brand.ToUpper().PadRight(15) + asset.Model.ToUpper().PadRight(15) + asset.Office.ToUpper().PadRight(15) + asset.Purchase_Date.ToString("MM-dd-yyyy").PadRight(15) + asset.Price_USD.ToString().PadRight(15) + asset.Currency.ToUpper().PadRight(15) + asset.Local_Price.ToString());
                    Console.ResetColor();
                }

            }
            else
            {
                Console.WriteLine(asset.Type.ToUpper().PadRight(15) + asset.Brand.ToUpper().PadRight(15) + asset.Model.ToUpper().PadRight(15) + asset.Office.ToUpper().PadRight(15) + asset.Purchase_Date.ToString("MM-dd-yyyy").PadRight(15) + asset.Price_USD.ToString().PadRight(15) + asset.Currency.ToUpper().PadRight(15) + asset.Local_Price.ToString());
            }
        }

    }
    showMainMenu();
}

//Function to show the main menu
void showMainMenu()
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Welcome to the Assets Tracking application! Please select the relevant option.");
    Console.WriteLine("1-Add an Asset");
    Console.WriteLine("2-List the Assets");
    Console.WriteLine("0-Quit");

    Console.Write("Enter your choice : ");
    Console.ResetColor();

    string userInput = Console.ReadLine();
    switch (userInput)
    {
        case "1":
            AddAsset();//Add an Asset to the list
            break;
        case "2":
            ListAsset();//Show all the Assets in the list
            break;
        case "0":
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Thank you for using this application!");//Quit the application
            Console.ResetColor();
            break;

        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid Selection. Please try again.");//Invalid input from the user
            Console.ResetColor();
            showMainMenu();
            break;
    }
}

//Asset class
class Asset
{
    public Asset(string type, string brand, string model, string office, DateTime purchase_Date, double price_USD, string currency, double local_Price)
    {
        Type = type;
        Brand = brand;
        Model = model;
        Office = office;
        Purchase_Date = purchase_Date;
        Price_USD = price_USD;
        Currency = currency;
        Local_Price = local_Price;
    }


    public string Type { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string Office { get; set; }
    public DateTime Purchase_Date { get; set; }
    public double Price_USD { get; set; }
    public string Currency { get; set; }
    public double Local_Price { get; set; }

}
        
