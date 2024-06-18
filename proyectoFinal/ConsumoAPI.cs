using System;
using System.Data;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

class ConsumoAPI
{
    public async Task<Clima> ObtenerEstadoTiempo(string url)
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();
            var dato = JsonSerializer.Deserialize<Clima>(json);
            return dato;

        }
        else{
            Console.WriteLine("No se puede obtener datos del timepo");
            return null;
        }
    }


}

class Clima
{
    public EstadoClima[] weather { get; set; }
    public MomentoDia sys { get; set; }
}

class EstadoClima
{
    public string main{get;set;}
}

class MomentoDia
{
    public long sunrise { get; set; }
    public long sunset { get; set; }
}