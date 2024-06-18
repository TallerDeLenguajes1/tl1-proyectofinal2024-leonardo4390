using System;
using System.Text.Json;
using System.IO;

class PersonajeJson
{
    public void GuardarPersonajes(List<Personaje> personajes, string nombreArchivo)
    {
        // Convertir el tipo de Enum a string al serializar
        var opcionSerializacion = new JsonSerializerOptions
        {

            WriteIndented = true,

            //Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        };
        
        string json = JsonSerializer.Serialize(personajes, opcionSerializacion);
        File.WriteAllText(nombreArchivo, json);
    }

    public List<Personaje> LeerPersonajes(string nombreArchivo)
    {
        if (File.Exists(nombreArchivo))
        {
            string json = File.ReadAllText(nombreArchivo);
            // Convertir el tipo de string a Enum al deserializar
            var opcionesDeserializacion = new JsonSerializerOptions
            {
                //Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
            };

            return JsonSerializer.Deserialize<List<Personaje>>(json, opcionesDeserializacion);
        }
        else
        {
            Console.WriteLine("El archivo no existe.");
            return new List<Personaje>();
        }
    }

    public bool Existe(string nombreArchivo)
    {
        return File.Exists(nombreArchivo) && new FileInfo(nombreArchivo).Length > 0;
    }

    public void AgregarPersonajes(List<Personaje> nuevosPersonajes, string nombreArchivo)
    {
        List<Personaje> personajesExistentes = LeerPersonajes(nombreArchivo);
        personajesExistentes.AddRange(nuevosPersonajes);
        GuardarPersonajes(personajesExistentes, nombreArchivo);
    }
}
/*// Clase para manejar la lectura y escritura de personajes en formato JSON
class PersonajeJson
{
    public void GuardarPersonajes(List<Personaje> personajes, string nombreArchivo)
    {
        string json = JsonSerializer.Serialize(personajes);
        File.WriteAllText(nombreArchivo, json);
    }

    public List<Personaje> LeerPersonajes(string nombreArchivo)
    {
        if (File.Exists(nombreArchivo))
        {
            string json = File.ReadAllText(nombreArchivo);
            return JsonSerializer.Deserialize<List<Personaje>>(json);
        }
        else
        {
            return new List<Personaje>();
        }
    }

    public bool Existe(string nombreArchivo)
    {
        return File.Exists(nombreArchivo) && new FileInfo(nombreArchivo).Length > 0;
    }

    public void AgregarPersonajes(List<Personaje> nuevosPersonajes, string nombreArchivo)
    {
        List<Personaje> personajesExistentes = LeerPersonajes(nombreArchivo);
        personajesExistentes.AddRange(nuevosPersonajes);
        GuardarPersonajes(personajesExistentes, nombreArchivo);
    }
}

*/