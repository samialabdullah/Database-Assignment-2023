using CaseManagementSystem.Services;


var menuCustomer = new MenuCustomerService();
var menuCustomerServiceEmployee = new MenuCustomerServiceEmployee();
while (true)
{
    Console.Clear();
    Console.WriteLine("\n-----------------------------  Välkommen till vårt ärendehanteringssystem  ----------------------\n");

    Console.WriteLine("\n 1- Är du kund, tryck nummer: 1 .\n");
    Console.WriteLine("\n 2- Är du kundtjänstmedarbetare, tryck nummer: 2 .");

    int SystemUser = int.Parse(Console.ReadLine() ?? "");

    if (SystemUser == 1)
    {
        Console.Clear();
        Console.WriteLine("\n1- Skapa en ny felanmälan.\n");
        Console.WriteLine("2- Visa ditt ärende.\n");
        Console.WriteLine("3- Uppdatera din ärendeinformation.\n");
        Console.WriteLine("4- Kontrollera kommentarerna i ditt ärende.\n");
        Console.WriteLine("5- Ta bort ditt ärende.");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    await menuCustomer.CreateNewSituationAsync();
                    break;

                case "2":
                    Console.Clear();
                    await menuCustomer.ListSpecificCustomerSituationAsync();
                    break;
                case "3":
                    Console.Clear();
                    await menuCustomer.UpdateMyInfoSituationAsync();
                    break;

                case "4":
                    Console.Clear();
                    await menuCustomer.ShowCommentOnMySituationAsync();
                    break;

                case "5":
                    Console.Clear();
                    await menuCustomer.DeleteMySituationAsync();
                    break;

            }

    }
    else if (SystemUser == 2)
    {
        Console.Clear();
        Console.WriteLine("\n1- Visa alla ärendena.\n");
        Console.WriteLine("2- Visa en specifik ärende.\n");
        Console.WriteLine("3- Uppdatera status för ett ärende.\n");
        Console.WriteLine("4- Lägg kommentar till ärende.\n");
        Console.WriteLine("5- Visa ärende med den kommentar.\n");
        Console.WriteLine("6- Ta bort ärende.");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    await menuCustomerServiceEmployee.ListAllSituationsAsync();
                    break;

                case "2":
                    Console.Clear();
                    await menuCustomerServiceEmployee.ListSpecificSituationAsync();
                    break;

                case "3":
                    Console.Clear();
                    await menuCustomerServiceEmployee.UpdateSpecificSituationConditionAsync();
                    break;

                case "4":
                    Console.Clear();
                    await menuCustomerServiceEmployee.AddCommentToSituationAsync();
                    break;

                case "5":
                    Console.Clear();
                    await menuCustomerServiceEmployee.ShowCommentToSituationAsync();
                    break;
                case "6":
                    Console.Clear();
                    await menuCustomerServiceEmployee.DeleteSpecificSituationAsync();
                    break;
            }

    }
    
    else
    {
        Console.WriteLine("\n Ogiltig info. Vänligen försök igen.\n");
    }

    Console.WriteLine("\n Tryck på valfri knapp för att fortsätta...!");
    Console.ReadLine();

}