using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class HistorialJson
{
    public void GuardarGanador(List<Personaje> personajes, string nombreArchivo)
    {
        List<HistorialBatalla> historialExistente = LeerHistorial(nombreArchivo);

        foreach (var personaje in personajes)
        {
            var historialBatalla = new HistorialBatalla(personaje, DateTime.Now.ToString("dd-MM-yyyy"));

            historialExistente.Add(historialBatalla);
        }

        var opcionesSerializacion = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(historialExistente, opcionesSerializacion);
        File.WriteAllText(nombreArchivo, json);
    }

    public List<HistorialBatalla> LeerHistorial(string nombreArchivo)
    {
        if (File.Exists(nombreArchivo))
        {
            string json = File.ReadAllText(nombreArchivo);
            return JsonSerializer.Deserialize<List<HistorialBatalla>>(json);
        }
        else
        {
            Console.WriteLine("El archivo no existe.");
            return new List<HistorialBatalla>();
        }
    }
}



public class HistorialBatalla
{
    public Personaje Ganador { get; set; }
    public string Fecha { get; set; }

    public HistorialBatalla(Personaje ganador, string fecha) // Ajuste aquí
    {
        Ganador = ganador;
        Fecha = fecha;
    }
}







