using System;
using System.Text.Json;
using System.IO;
using System.Linq;
class HistorialJson
{
    public void GuardarGanador(Personaje ganador, string nombreArchivo)
    {
        List<Ganador> historialGanador = LeerGanador(nombreArchivo);
        var ganadorExistente = historialGanador.Find(g => g.Datos.Tipo == ganador.Datos.Tipo);
        
        if (ganadorExistente != null)
        {
            ganadorExistente.Victorias++;       
        }
        else
        {
            var nuevoGanador = new Ganador(ganador.Datos);
            historialGanador.Add(nuevoGanador);
        }
        
        var opcionSerializacion = new JsonSerializerOptions
        {
            WriteIndented = true,
        };
        string json = JsonSerializer.Serialize(historialGanador, opcionSerializacion);
        File.WriteAllText(nombreArchivo, json);
    }

    public List<Ganador> LeerGanador(string nombreArchivo)
    {
        if (File.Exists(nombreArchivo))
        {
            string json = File.ReadAllText(nombreArchivo);
            return JsonSerializer.Deserialize<List<Ganador>>(json);
        }
        else
        {
            Console.WriteLine("No existe el archivo");
            return new List<Ganador>();
        }
    }

    public bool Existe(string nombreArchivo)
    {
        return File.Exists(nombreArchivo) && new FileInfo(nombreArchivo).Length > 0;
    }

    //personajes ganadores en forma descendente
    public List<(Datos Datos, int Victorias)> ObtenerPersonajesGanadores(string nombreArchivo)
    {
        List<Ganador> historial = LeerGanador(nombreArchivo);
        if (historial == null || historial.Count == 0)
        {
            return new List<(Datos Datos, int Victorias)>();
        }

        //victorias en forma descendente
        var personajesGanadores = historial.OrderByDescending(g => g.Victorias)
                                           .Select(g => (g.Datos, g.Victorias))
                                           .ToList();

        return personajesGanadores;
    }
}

class Ganador
{
    public Datos Datos{get;set;}
    public int Victorias{get;set;}

    //creando constructor
    public Ganador(){}

    public Ganador(Datos datos)
    {
        Datos = datos;
        Victorias = 1;//inicio en 1
    }
}