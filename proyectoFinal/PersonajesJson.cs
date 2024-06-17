using System;
using System.Text.Json;
using System.IO;

class PersonajeJson
{
    public void GuardarPersonajes(List<Personaje> personajes, string nombreArchivo)
    {
        //serializando personajes
        string json = JsonSerializer.Serialize(personajes);
        File.WriteAllText(nombreArchivo,json);
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
            Console.WriteLine("El archivo no existe.");
            return null;
        }
    }

    public bool Existe(string nombreArchivo)
    {
        return File.Exists(nombreArchivo) && new FileInfo(nombreArchivo).Length > 0;
    }



}