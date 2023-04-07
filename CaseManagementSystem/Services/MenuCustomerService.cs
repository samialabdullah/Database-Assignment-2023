using CaseManagementSystem.Models;


namespace CaseManagementSystem.Services;

internal class MenuCustomerService
{
    public async Task CreateNewSituationAsync()
    {
        var customer = new Customers();
        var situations = new Situations();

        Console.WriteLine("\n- Felrapport: ");
        situations.Description = Console.ReadLine() ?? "";

        Console.WriteLine("\n**************************************************************************************\n");

        Console.Write("- Förnamn: ");
        customer.FirstName = Console.ReadLine() ?? "";

        Console.Write("- Efternamn: ");
        customer.LastName = Console.ReadLine() ?? "";

        Console.Write("- E-post: ");
        customer.Email = Console.ReadLine() ?? "";

        Console.Write("- Telefonnummer: ");
        customer.PhoneNumber = Console.ReadLine() ?? "";

        Console.Write("- Ange ny ärendestatus (0 = EjPåbörjad, 1 = Pågående, 2 = Aslutad):");
        var opt = Console.ReadLine();
        if (opt == "0")
            situations.Condition = "EjPåbörjad";
        else if (opt == "1")
            situations.Condition = "Pågående";
        else if (opt == "2")
            situations.Condition = "Aslutad";

        Console.WriteLine("\n - Ärendet skapades: " + DateTime.Now + "\n - Tack för den änmale, vi behandlar ditt ärende så fort vi kan!");
        situations.CreatedTime = DateTime.Now;

        await CustomerService.SaveCustomerAsync(situations, customer);
    }

    public async Task ListSpecificCustomerSituationAsync()
    {
        Console.Write("\n- Ange din e-post: ");
        var email = Console.ReadLine();
        if (email != null)
        {
            var situations = await CustomerService.GetCustomerAsync(email);
            if (situations != null)
            {
                Console.WriteLine($"Ärendenummer: {situations.Id}");
                Console.WriteLine($"Beskrivning: {situations.Description}");
                Console.WriteLine($"SkapadTid: {situations.CreatedTime}");
                Console.WriteLine($"Situation: {situations.Condition}");
                Console.WriteLine($"Namn: {situations.FirstName} {situations.LastName}");
                Console.WriteLine($"E-post: {situations.Email}");
                Console.WriteLine($"Telefonnummer: {situations.PhoneNumber}");
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"\n - Inget ärende har träffat med den e-post: {email} .");
                Console.WriteLine("");
            }
        }
        else
        {
            Console.WriteLine($" - Inget info.");
            Console.WriteLine("");
        }

    }
    public async Task UpdateMyInfoSituationAsync()
    {
        Console.Write("\n-Ange din e-post: ");
        var email = Console.ReadLine();
        if (!string.IsNullOrEmpty(email))
        {
            var customer = new Customers();
            var situations = await CustomerService.GetCustomerAsync(email);
            if (situations != null)
            {
                Console.WriteLine("Felrapport: ");
                situations.Description = Console.ReadLine() ?? "";


                Console.Write("Förnamn: ");
                customer.FirstName = Console.ReadLine() ?? "";

                Console.Write("Efternamn: ");
                customer.LastName = Console.ReadLine() ?? "";

                Console.Write("E-post: ");
                customer.Email = Console.ReadLine() ?? "";

                Console.Write("Telefonnummer: ");
                customer.PhoneNumber = Console.ReadLine() ?? "";

                Console.Write("Ange ny ärendestatus (0= EjPåbörjad, 1= Pågående, 2= Avslutad):");
                var opt = Console.ReadLine();
                if (opt == "0")
                    situations.Condition = "EjPåbörjad";
                else if (opt == "1")
                    situations.Condition = "Pågående";
                else if (opt == "2")
                    situations.Condition = "Avslutad";

                Console.WriteLine("\n -Ärendet uppdaterad : " + DateTime.Now + "\n -Tack för den info, vi återkommer så snart som möjligt!");
                situations.CreatedTime = DateTime.Now;


                await CustomerService.UpdateCustomerAsync(situations, customer);

                Console.WriteLine("Ditt ärende uppdaterat.");
            }
            else
            {
                Console.WriteLine($"\n- Det finns inte ärende med detta E-post.");
                Console.WriteLine("");
            }

        }
        else
        {
            Console.WriteLine($"\n- Inget info.");
            Console.WriteLine("");
        }
    }
    

    public async Task ShowCommentOnMySituationAsync()
    {
        Console.Write("Ange ärendemail: ");
        string email = Console.ReadLine() ?? "";
        var existingSituation = await CustomerService.GetCustomerAsync(email);

        if (existingSituation != null)
        {
            Console.WriteLine($"Beskrivning: {existingSituation.Description}");
            Console.WriteLine($"SkapadTid: {existingSituation.CreatedTime}");
            Console.WriteLine($"Situation: {existingSituation.Condition}");
            Console.WriteLine($"Kund namn: {existingSituation.FirstName} {existingSituation.LastName}");
            Console.WriteLine($"Kund e-post: {existingSituation.Email}");
            Console.WriteLine($"Kund Telefonnummer: {existingSituation.PhoneNumber}");
            Console.WriteLine();
        }
        if (existingSituation == null)
        {
            Console.WriteLine($"Inget ärende hittades med ID {email}");
            return;
        }
        var situationWithComments = await CustomerService.CheckUpMySituationAsync(email);

        if (situationWithComments.Any())
        {
            foreach (var comment in situationWithComments)
            {
                Console.WriteLine($"Kundtjänstmedarbetare kommentarer: {comment.Text} (posted on {comment.CreatedAt})");
            }

            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("Ingen kommentar i denna ärende.");
        }
    }

    public async Task DeleteMySituationAsync()
    {
        Console.Write("\n -Ange din @-Mail: ");
        var email = Console.ReadLine();
        if (!String.IsNullOrEmpty(email))
        {
            bool deleted = await CustomerService.DeleteCustomerAsync(email);

            if (deleted)
            {

                Console.WriteLine("\n --Ärende har raderat !!.");
            }
            else
            {
                Console.WriteLine("\n - Det finns inte ärende med detta @-mail.");
            }
        }
        else
        {
            Console.WriteLine($"\n -Inget info.");
            Console.WriteLine("");
        }
    }
}
