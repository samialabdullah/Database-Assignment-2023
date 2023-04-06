using CaseManagementSystem.Models;

namespace CaseManagementSystem.Services;

internal class MenuCustomerServiceEmployee
{        // Empolyee method
    public async Task ListAllSituationsAsync()
    {
        var (situationss, customers) = await CustomerServiceEmployee.GetAllAsync();

        if (situationss.Any())
        {
            foreach (Situations situations in situationss)
            {
                Console.WriteLine($"Ärendenummer: {situations.Id}");
                Console.WriteLine($"Beskrivning: {situations.Description}");
                Console.WriteLine($"SkapadTid: {situations.CreatedTime}");
                Console.WriteLine($"Situation: {situations.Condition}");
                Console.WriteLine($"Namn: {situations.FirstName} {situations.LastName}");
                Console.WriteLine($"E-post: {situations.Email}");
                Console.WriteLine($"Telefonnummer: {situations.PhoneNumber}");
                Console.WriteLine("");
            }
        }
    }

    public async Task ListSpecificSituationAsync()
    {
        Console.Write("Ange ID-ärende: ");
        var id = Console.ReadLine();
        if (!String.IsNullOrEmpty(id))
        {
            var situations = await CustomerServiceEmployee.GetAsync(int.Parse(id));
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
                Console.WriteLine($"Inget ärende med den angivna e-post {id} hittades.");
                Console.WriteLine("");
            }
        }
        else
        {
            Console.WriteLine($"Ange ärende-ID.");
            Console.WriteLine("");
        }

    }

    public async Task UpdateSpecificSituationConditionAsync()
    {
        Console.WriteLine("Ange det ID-ärende som vill du ha att uppdatera ");
        var id = Console.ReadLine();
        if (!String.IsNullOrEmpty(id))
        {

            var situations = await CustomerServiceEmployee.GetAsync(int.Parse(id));
            if (situations != null)
            {
                Console.Write("Ange ny status (0=NotStarted, 1=InProgress, 2=Completed):");
                var opt = Console.ReadLine();
                if (opt == "0")
                    situations.Condition = "NotStarted";
                else if (opt == "1")
                    situations.Condition = "InProgress";
                else if (opt == "2")
                    situations.Condition = "Completed";

                await CustomerServiceEmployee.UpdateAsync(situations);

                Console.WriteLine("Status uppdaterad");
            }
            else
            {
                Console.WriteLine($"Kunde inte att hitta något ärende med den ID.");
                Console.WriteLine("");
            }

        }
        else
        {
            Console.WriteLine($"Ange ärende-ID.");
            Console.WriteLine("");
        }
    }

    public async Task DeleteSpecificSituationAsync()
    {
        Console.Write("Ange det ID-ärende som vill du ha att ta bort: ");
        var id = Console.ReadLine();
        if (!String.IsNullOrEmpty(id))
        {
            bool deleted = await CustomerServiceEmployee.DeleteAsync(int.Parse(id));

            if (deleted)
            {
                Console.WriteLine("Ärende raderat");
            }
            else
            {
                Console.WriteLine("Kunde inte att hitta något ärende med den ID.");
            }
        }
        else
        {
            Console.WriteLine($"Ange ärende-ID.");
            Console.WriteLine("");
        }
    }

    public async Task AddCommentToSituationAsync()
    {
        Console.Write("Ange ID-ärende du vill kommentera: ");
        int situationId = int.Parse(Console.ReadLine() ?? "");

        Console.WriteLine("Vi behöver din information för vet vem som följer ärende");
        Console.WriteLine("Kundtjänstmedarbetare-information:");
        Console.Write("Förnamn: ");
        string firstName = Console.ReadLine() ?? "";

        Console.Write("Efternamn: ");
        string lastName = Console.ReadLine() ?? "";

        Console.Write("E-post: ");
        string email = Console.ReadLine() ?? "";

        Console.Write("Telefonnummer: ");
        string phoneNumber = Console.ReadLine() ?? "";

        Models.CustomerServiceEmployees customerServiceEmployee = new Models.CustomerServiceEmployees
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber
        };

        Console.Write("Skriv in kommentarstexten: ");
        string commentText = Console.ReadLine() ?? "";

        Comments comment = new Comments
        {
            Text = commentText,
            CreatedAt = DateTime.Now
        };

        await CustomerServiceEmployee.AddCommentAsync(situationId, comment, customerServiceEmployee);

        Console.WriteLine("Kommentaren har sparat.");
    }

    public async Task ShowCommentToSituationAsync()
    {
        Console.Write("Ange ärende-ID: ");
        int situationId = int.Parse(Console.ReadLine() ?? "");
        var existingSituation = await CustomerServiceEmployee.GetAsync(situationId);

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
            Console.WriteLine($"Inget ärende hittades med ID {situationId}");
            return;
        }
        var situationWithComments = await CustomerServiceEmployee.GetCommentsAsync(situationId);

        if (situationWithComments.Any())
        {
            foreach (var comment in situationWithComments)
            {
                Console.WriteLine($"Kundtjänstmedarbetare kommentar: {comment.Text} (posted on {comment.CreatedAt})");
            }

            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("Ingen kommentar i det ärende");
            Console.WriteLine("Vill du ha att lägga ny kommentar, JA eller NEJ");
            var option = Console.ReadLine() ?? "";
            if (option == "ja".ToLower())
            {
                Console.WriteLine("Vi behöver din information för vet vem som följer ärende");
                Console.WriteLine("Kundtjänstmedarbetare-information:");
                Console.Write("Förnamn: ");
                string firstName = Console.ReadLine() ?? "";

                Console.Write("Efternamn: ");
                string lastName = Console.ReadLine() ?? "";

                Console.Write("E-post: ");
                string email = Console.ReadLine() ?? "";

                Console.Write("Telefonnummer: ");
                string phoneNumber = Console.ReadLine() ?? "";
                Models.CustomerServiceEmployees customerServiceEmployee = new Models.CustomerServiceEmployees
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    PhoneNumber = phoneNumber
                };

                Console.Write("Skriv din kommentar: ");
                string commentText = Console.ReadLine() ?? "";

                Comments _comment = new()
                {
                    Text = commentText,
                    CreatedAt = DateTime.Now
                };

                await CustomerServiceEmployee.AddCommentAsync(situationId, _comment, customerServiceEmployee);

                Console.WriteLine("Kommentaren har sparat.");
            }
            else
            {
                Console.WriteLine("Välkommen åter, när vill att lägga en kommentar.");
            }
        }
    }
}
