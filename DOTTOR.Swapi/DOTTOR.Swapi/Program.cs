using System.Net.Http.Json;
using DOTTOR.Swapi;

var apiPath = "https://swapi.dev/api/people";
var client = new HttpClient();

var apiResponse = await client.GetFromJsonAsync<SwapiResponse>(apiPath);


MenuScelte swapi = new MenuScelte();
await swapi.GetFromJSON(apiResponse, client);

// chiamo la funzione che mi gestisce il menu e le azioni varie
swapi.ManageChoice();





    



