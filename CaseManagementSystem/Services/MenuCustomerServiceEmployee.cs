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
                Console.WriteLine($"\n# Ärendenummer: {situations.Id}");
                Console.WriteLine($"- Beskrivning: {situations.Description}");
                Console.WriteLine($"- Situation och AnmälaTid: {situations.Condition + " - " + situations.CreatedTime}");
                Console.WriteLine($"- Namn: {situations.FirstName + " " + situations.LastName}");
                Console.WriteLine($"- Kund-info: {situations.Email + " ; tel: " + situations.PhoneNumber}");
                Console.WriteLine("-----------------------------------------------------------");
                Console.WriteLine("");
            }
        }
    }

    public async Task ListSpecificSituationAsync()
    {
        Console.Write("\n -Ange ärendenummer: ");
        var id = Console.ReadLine();
        if (!String.IsNullOrEmpty(id))
        {
            var situations = await CustomerServiceEmployee.GetAsync(int.Parse(id));
            if (situations != null)
            {
                Console.WriteLine("\n********************************************************");
                Console.WriteLine($"- Ärendenummer: {situations.Id}");
                Console.WriteLine($"- Beskrivning: {situations.Description}");
                Console.WriteLine($"- Situation och AnmälaTid: {situations.Condition + " - " + situations.CreatedTime}");
                Console.WriteLine($"- Namn: {situations.FirstName + " " + situations.LastName}");
                Console.WriteLine($"- Kund-info: {situations.Email + " ; tel: " + situations.PhoneNumber}\n");
                Console.WriteLine("********************************************************");
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"\n -Inget ärende träffat med det ärendenummer: {id}.");
                Console.WriteLine("");
            }
        }
        else
        {
            Console.WriteLine($"\n - Ange ärendenummer!");
            Console.WriteLine("");
        }

    }

    public async Task UpdateSpecificSituationConditionAsync()
    {
        Console.WriteLine("\n - Ange det ärendenummer som vill du ha att uppdatera: ");
        var id = Console.ReadLine();
        if (!String.IsNullOrEmpty(id))
        {

            var situations = await CustomerServiceEmployee.GetAsync(int.Parse(id));
            if (situations != null)
            {
                Console.Write("\n - Ange ny ärendestatus (0 = EjPåbörjad, 1 = Pågående, 2 = Avslutad): ");
                var opt = Console.ReadLine();
                if (opt == "0")
                    situations.Condition = "EjPåbörjad";
                else if (opt == "1")
                    situations.Condition = "Pågående";
                else if (opt == "2")
                    situations.Condition = "Avslutad";

                await CustomerServiceEmployee.UpdateAsync(situations);

                Console.WriteLine("\n - Status uppdaterad.");
            }
            else
            {
                Console.WriteLine($"\n - Kunde inte att hitta ärende med det nummer.");
                Console.WriteLine("");
            }

        }
        else
        {
            Console.WriteLine($" -Ange ärendenummer!");
            Console.WriteLine("");
        }
    }

    public async Task AddCommentToSituationAsync()
    {
        Console.Write("\n - Ange ärendenummer du vill kommentera: ");
        int situationId = int.Parse(Console.ReadLine() ?? "");

        Console.WriteLine("\n- Vi behöver din information för vet vem som följer ärende!");
        Console.WriteLine("\n - Kundtjänstmedarbetare-information:");
        Console.Write("\n - Förnamn: ");
        string firstName = Console.ReadLine() ?? "";

        Console.Write("\n - Efternamn: ");
        string lastName = Console.ReadLine() ?? "";

        Console.Write("\n - E-post: ");
        string email = Console.ReadLine() ?? "";

        Console.Write("\n - Telefonnummer: ");
        string phoneNumber = Console.ReadLine() ?? "";

        Models.CustomerServiceEmployees customerServiceEmployee = new Models.CustomerServiceEmployees
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber
        };

        Console.Write("\n - Skriv in kommentarstexten: ");
        string commentText = Console.ReadLine() ?? "";

        Comments comment = new Comments
        {
            Text = commentText,
            CreatedAt = DateTime.Now
        };

        await CustomerServiceEmployee.AddCommentAsync(situationId, comment, customerServiceEmployee);

        Console.WriteLine("\n +++++++ Kommentaren har sparat! +++++++++");
    }

    public async Task ShowCommentToSituationAsync()
    {
        Console.Write("\n - Ange ärendenummer : ");
        int situationId = int.Parse(Console.ReadLine() ?? "");
        var existingSituation = await CustomerServiceEmployee.GetAsync(situationId);

        if (existingSituation != null)
        {
            Console.WriteLine("\n############################################################");
            Console.WriteLine($"\n- Beskrivning: {existingSituation.Description}");
            Console.WriteLine($"- Situation och AnmälaTid: {existingSituation.Condition + " - " + existingSituation.CreatedTime}");
            Console.WriteLine($"- Kund namn: {existingSituation.FirstName + " " + existingSituation.LastName}");
            Console.WriteLine($"- Kund-info: {existingSituation.Email + " ; tel: " + existingSituation.PhoneNumber}\n");
            Console.WriteLine("############################################################");
            Console.WriteLine();
        }
        if (existingSituation == null)
        {
            Console.WriteLine($"\n -Inget ärende träffat med det ärendenummer: {situationId}.");
            return;
        }
        var situationWithComments = await CustomerServiceEmployee.GetCommentsAsync(situationId);

        if (situationWithComments.Any())
        {
            foreach (var comment in situationWithComments)
            {
                Console.WriteLine($" + Kundtjänstmedarbetare kommentar: {comment.Text} ( {comment.CreatedAt} ).");
            }

            Console.WriteLine();
        }
        else
        {
            Console.WriteLine(" - Ingen kommentar i det ärende\n");
            Console.WriteLine(" + Vill du ha att lägga ny kommentar, JA eller NEJ : ");
            var option = Console.ReadLine() ?? "";
            if (option == "ja".ToLower())
            {
                Console.WriteLine("\n - Vi behöver din information för vet vem som följer ärende!");
                Console.WriteLine(" - Kundtjänstmedarbetare-information:");
                Console.Write("\n - Förnamn: ");
                string firstName = Console.ReadLine() ?? "";

                Console.Write(" - Efternamn: ");
                string lastName = Console.ReadLine() ?? "";

                Console.Write(" - E-post: ");
                string email = Console.ReadLine() ?? "";

                Console.Write(" - Telefonnummer: ");
                string phoneNumber = Console.ReadLine() ?? "";
                Models.CustomerServiceEmployees customerServiceEmployee = new Models.CustomerServiceEmployees
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    PhoneNumber = phoneNumber
                };

                Console.Write("\n + Skriv din kommentar: ");
                string commentText = Console.ReadLine() ?? "";

                Comments _comment = new()
                {
                    Text = commentText,
                    CreatedAt = DateTime.Now
                };

                await CustomerServiceEmployee.AddCommentAsync(situationId, _comment, customerServiceEmployee);

                Console.WriteLine("\n + Kommentaren har sparat.");
            }
            else
            {
                Console.WriteLine("\n - Välkommen åter, när vill att lägga en kommentar.");
            }
        }
    }

    public async Task DeleteSpecificSituationAsync()
    {
        Console.Write("\n - Ange det ärendenummer som vill du ha att ta bort: ");
        var id = Console.ReadLine();
        if (!String.IsNullOrEmpty(id))
        {
            bool deleted = await CustomerServiceEmployee.DeleteAsync(int.Parse(id));

            if (deleted)
            {
                Console.WriteLine("\n -Ärende raderat!");
            }
            else
            {
                Console.WriteLine("\n - Kunde inte att hitta något ärende med det ärendenummer.");
            }
        }
        else
        {
            Console.WriteLine($"\n - Ange ärendenummer.");
            Console.WriteLine("");
        }
    }
}
