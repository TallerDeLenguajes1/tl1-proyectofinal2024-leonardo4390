using System;
using System.Text.Json;
using System.IO;

class PersonajeJson
{
    public void GuardarPersonajes(List<Personaje> personajes, string nombreArchivo)
    {
        // JsonSerializerOption lo uso para tener una mejor impresion cuando serializo por writeIndented
        var opcionSerializacion = new JsonSerializerOptions{WriteIndented = true,};
        
        string json = JsonSerializer.Serialize(personajes, opcionSerializacion);
        File.WriteAllText(nombreArchivo, json);
    }

    public List<Personaje> LeerPersonajes(string nombreArchivo)
    {
        if (File.Exists(nombreArchivo))
        {
            string json = File.ReadAllText(nombreArchivo);
            //var opcionesDeserializacion = new JsonSerializerOptions{};

            return JsonSerializer.Deserialize<List<Personaje>>(json);//, opcionesDeserializacion);
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

    // public void AgregarPersonajes(List<Personaje> nuevosPersonajes, string nombreArchivo)
    // {
    //     List<Personaje> personajesExistentes = LeerPersonajes(nombreArchivo);
    //     personajesExistentes.AddRange(nuevosPersonajes);
    //     GuardarPersonajes(personajesExistentes, nombreArchivo);
    // }
    public void ActualizarPersonaje(Personaje personajeGanador, string nombreArchivo)
    {
        List<Personaje> personajes = LeerPersonajes(nombreArchivo);

        //buscando personaje a actualizar
        int ganador = personajes.FindIndex(p => p.Datos.Tipo == personajeGanador.Datos.Tipo && p.Datos.Nombre == personajeGanador.Datos.Nombre);
        if (ganador >= 0)
        {
            personajes[ganador] = personajeGanador;
            GuardarPersonajes(personajes, nombreArchivo);
        }
    }

}