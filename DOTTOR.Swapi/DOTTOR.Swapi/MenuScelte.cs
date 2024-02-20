using Spectre.Console;

namespace DOTTOR.Swapi;

public class MenuScelte : SwapiAPI
{
    private List<string> choices { get; set; }
    
    private string GetChoice()
    {
        // inserisco nella lista le operazioni disponibili
        choices = new List<string>();
        choices.Add("Lista dei [yellow]personaggi[/]");
        choices.Add("Media [yellow]dell'altezza[/]");
        choices.Add("Media [yellow]del peso[/]");
        choices.Add("Ricerca di un [yellow]Personaggio[/]");
        choices.Add("[red]Esci dal menu[/]");
        
        // chiedo quale operazione l'utente vuole fare
        var menu = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Quale [green]informazione[/] voule ottenere?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
                .AddChoices(new[] {
                    choices[0], choices[1], choices[2], choices[3], choices[4]
                }));

        return menu;
    }

    public void ManageChoice()
    {
        while (true)
        {
            var choice = GetChoice();
            if (choice.Equals(choices[0]))
            {
                // lista dei nomi
                PrintNameList();
            }
            else if(choice.Equals(choices[1]))
            {
                // meedia dell'altezza
                Console.WriteLine("La media dell'altezza e': " + AvgHeight);
            }
            else if(choice.Equals(choices[2]))
            {
                // media del peso
                Console.WriteLine("La media del peso e': " + AvgWeight);
            
            }
            else if(choice.Equals(choices[3]))
            {
                // ritorno i personaggi che corrispondono a quello cercato dall'utente
                Console.Write("Cosa si voule cercare? ");
                var searched = Console.ReadLine();
                if (searched is not null)
                {
                    SearchPerson(searched);
                }
            }
            else if(choice.Equals(choices[4]))
            {
                break;
            }
            else
            {
                Console.WriteLine("Qualcosa è andato storto!");
            }
        }
    }
    
    
    private void SearchPerson(string searched)
    {
        // creo un tabella per printare i risultai della ricerca
        var table = new Table();
        table.AddColumn("[yellow]Nome[/]");
        table.AddColumn("[yellow]Colore degli occhi[/]");
        table.AddColumn("[yellow]Colore dei capelli[/]");
        table.AddColumn("[yellow]Colore della pelle[/]");
        table.AddColumn("[yellow]Sesso[/]");
        
        // aggiungo riga per riga le corrispondenze
        searched = searched.ToLower();
        var countChecked = 0;
        var countFinded = 0;
        foreach (var item in NameList)
        {
            string obj = item.ToLower();    // faccio questo per non avere la restrizione CASE-SENSITIVE 
            if (obj.Contains(searched) || obj.Equals(searched))
            {
                // aggiungo alla tabella
                table.AddRow(item, EyeList[countFinded], HairList[countFinded], SkinList[countFinded], GenderList[countFinded]);
                countFinded++;
            }

            countChecked++;
        }

        if (countFinded == 0)
            Console.WriteLine("Non ci sono corrispondenze!");
        else
        {
            Console.WriteLine($"Sono risultati {countFinded} elementi compatibili su {countChecked} elementi controllati:");
            AnsiConsole.Write(table);
        }
            
    }
    
    
    private void PrintNameList()
    {
        // creo una tabella con il nome, l'altezza e il peso dei personaggi presenti 
        Console.WriteLine($"Lista con {NameList.Count} elementi:");
        var table = new Table();
        table.AddColumn("[yellow]Nome[/]");
        table.AddColumn("[yellow]Altezza[/]");
        table.AddColumn("[yellow]Peso[/]");
        for (int i = 0; i < NameList.Count; i++)
        {
            table.AddRow(NameList[i], HeightList[i], WeightList[i]);
        }
        AnsiConsole.Write(table);
    }
}