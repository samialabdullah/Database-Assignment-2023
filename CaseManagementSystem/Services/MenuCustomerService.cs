using CaseManagementSystem.Models;


namespace CaseManagementSystem.Services;

internal class MenuCustomerService
{
    public async Task CreateNewSituationAsync()
    {
        var customer = new Customers();
        var situations = new Situations();

        Console.WriteLine("Felrapport: ");
        situations.Description = Console.ReadLine() ?? "";

        Console.WriteLine(DateTime.Now);
        situations.CreatedTime = DateTime.Now;

        Console.Write("Ange ny status (0=NotStarted, 1=InProgress, 2=Completed):");
        var opt = Console.ReadLine();
        if (opt == "0")
            situations.Condition = "NotStarted";
        else if (opt == "1")
            situations.Condition = "InProgress";
        else if (opt == "2")
            situations.Condition = "Completed";

        Console.Write("Förnamn: ");
        customer.FirstName = Console.ReadLine() ?? "";

        Console.Write("Efternamn: ");
        customer.LastName = Console.ReadLine() ?? "";

        Console.Write("E-post: ");
        customer.Email = Console.ReadLine() ?? "";

        Console.Write("Telefonnummer: ");
        customer.PhoneNumber = Console.ReadLine() ?? "";

        await CustomerService.SaveCustomerAsync(situations, customer);
    }

    public async Task ListSpecificCustomerSituationAsync()
    {
        Console.Write("Ange din e-post: ");
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
                Console.WriteLine($"Inget ärende med den angivna e-post {email} hittades.");
                Console.WriteLine("");
            }
        }
        else
        {
            Console.WriteLine($"Inget ID.");
            Console.WriteLine("");
        }

    }
    public async Task UpdateMyInfoSituationAsync()
    {
        Console.Write("Ange din e-post: ");
        var email = Console.ReadLine();
        if (!string.IsNullOrEmpty(email))
        {
            var customer = new Customers();
            var situations = await CustomerService.GetCustomerAsync(email);
            if (situations != null)
            {
                Console.WriteLine("Felrapport: ");
                situations.Description = Console.ReadLine() ?? "";

                Console.WriteLine(DateTime.Now);
                situations.CreatedTime = DateTime.Now;

                Console.Write("Ange ny status (0=NotStarted, 1=InProgress, 2=Completed):");
                var opt = Console.ReadLine();
                if (opt == "0")
                    situations.Condition = "NotStarted";
                else if (opt == "1")
                    situations.Condition = "InProgress";
                else if (opt == "2")
                    situations.Condition = "Completed";

                Console.Write("Förnamn: ");
                customer.FirstName = Console.ReadLine() ?? "";

                Console.Write("Efternamn: ");
                customer.LastName = Console.ReadLine() ?? "";

                Console.Write("E-post: ");
                customer.Email = Console.ReadLine() ?? "";

                Console.Write("Telefonnummer: ");
                customer.PhoneNumber = Console.ReadLine() ?? "";

                await CustomerService.UpdateCustomerAsync(situations, customer);

                Console.WriteLine("Ditt ärende uppdaterat.");
            }
            else
            {
                Console.WriteLine($"Det finns inte ärende med detta ID.");
                Console.WriteLine("");
            }

        }
        else
        {
            Console.WriteLine($"Inget ID.");
            Console.WriteLine("");
        }
    }
    public async Task DeleteMySituationAsync()
    {
        Console.Write("Ange din @-Mail: ");
        var email = Console.ReadLine();
        if (!String.IsNullOrEmpty(email))
        {
            bool deleted = await CustomerService.DeleteCustomerAsync(email);

            if (deleted)
            {

                Console.WriteLine("Ärende har raderat.");
            }
            else
            {
                Console.WriteLine("Det finns inte ärende med detta @-mail.");
            }
        }
        else
        {
            Console.WriteLine($"Inget ID.");
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
}
