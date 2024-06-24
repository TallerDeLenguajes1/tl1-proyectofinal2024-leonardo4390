using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

class ConsumoAPI
{
    private string url =  "https://api.openweathermap.org/data/2.5/weather?q=san%20miguel%20de%20tucuman&appid=ae4b960965d0cece848194ecb3e1582b";
    public async Task<Clima> ObtenerEstadoTiempo()
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
        /*try
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();
            var dato = JsonSerializer.Deserialize<Clima>(json);
            return dato;
        }
        else
        {
            Console.WriteLine($"Error en la respuesta de la API: {response.StatusCode}");
            return null;
        }
    }
    catch (HttpRequestException e)
    {
        Console.WriteLine($"Excepción de solicitud HTTP: {e.Message}");
        return null;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Excepción general: {ex.Message}");
        return null;
    }
        */
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