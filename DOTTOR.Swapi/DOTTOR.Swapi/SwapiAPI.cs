using System.Net.Http.Json;

namespace DOTTOR.Swapi;

public class SwapiAPI : SwapiResponse
{
    protected List<string> NameList { get; private set; }
    protected List<string> WeightList { get; private set; }
    protected List<string> HeightList { get; private set; }
    protected List<string> HairList { get; private set; }
    protected List<string> SkinList { get; private set; }
    protected List<string> EyeList { get; private set; }
    protected List<string> GenderList { get; private set; }

    protected double AvgHeight
    {
        get
        {
            double avg = 0.0, sum = 0.0;
            foreach (var item in HeightList)
            {
                if(item != "unknown")
                    double.TryParse(item, out avg);
                sum += avg;
            }

            return (sum / HeightList.Count);
        }
    }

    protected double AvgWeight
    {
        get
        {
            double avg = 0.0, sum = 0.0;
            foreach (var item in WeightList)
            {
                if(item != "unknown")
                    double.TryParse(item, out avg);
                sum += avg;
            }

            return (sum / WeightList.Count);
        }
    }

    public SwapiAPI()
    {
        NameList = new List<string>();
        WeightList = new List<string>();
        HeightList = new List<string>();
        EyeList = new List<string>();
        HairList = new List<string>();
        SkinList = new List<string>();
        GenderList = new List<string>();
    }
    
    public async Task GetFromJSON(SwapiResponse response, HttpClient client)
    {
        var nextPage = response.Next;
    
        // da fare una lista con il peso e una per l'altezza 
        foreach (var person in response.People)
        {
            NameList.Add(person.Name);
            WeightList.Add(person.Mass);
            HeightList.Add(person.Height);
            EyeList.Add(person.EyeColor);
            HairList.Add(person.HairColor);
            SkinList.Add(person.SkinColor);
            GenderList.Add(person.Gender);
        }

        if (nextPage != null)
        {
            // domando alla pagina successiva i dati
            var nextResponse = await client.GetFromJsonAsync<SwapiResponse>(nextPage);
            nextPage = nextResponse.Next;
            await GetFromJSON(nextResponse, client);
        }
    }
}