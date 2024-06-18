using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

class Principal
{
    private string url = "https://api.openweathermap.org/data/2.5/weather?q=palpala&appid=ae4b960965d0cece848194ecb3e1582b";

    public async Task Menu()
    {
        TipoPersonaje tipoElegido;
        FabricaDePersonajes fabrica = new FabricaDePersonajes();
        PersonajeJson personajeJson = new PersonajeJson();
        ConsumoAPI climaAPI = new ConsumoAPI();

        while (true)
        {
            Console.WriteLine("\nElige un personaje:\n");
            Console.WriteLine("*Guerrero");
            Console.WriteLine("*Mago");
            Console.WriteLine("*Arquero");
            Console.WriteLine();

            string entrada = Console.ReadLine().Trim();

            // Verificacion para lo que se espera para personaje
            if (!int.TryParse(entrada, out _))
            {
                if (Enum.TryParse(entrada, true, out tipoElegido))
                {
                    if (tipoElegido == TipoPersonaje.Arquero || tipoElegido == TipoPersonaje.Guerrero || tipoElegido == TipoPersonaje.Mago)
                    {
                        break;
                    }
                }
            }

            Console.WriteLine("Ingreso incorrecto, ingrese otra vez");
        }
        

        //leyendo personajes existentes
        List<Personaje> personajesExistentes = personajeJson.LeerPersonajes("personajes.json");

        //personajeUsuario` como null
        Personaje personajeUsuario = null;

        // verificando existencia de personaje tipoelegido en personajes existentes
        foreach (var personaje in personajesExistentes)
        {
            if (personaje.Datos.Tipo == tipoElegido)
            {
                personajeUsuario = personaje;
                break;
            }
        }

        // Si no se encontró un personaje del tipo elegido, se genera uno nuevo
        if (personajeUsuario == null)
        {
            personajeUsuario = fabrica.GenerarPersonajeAleatorio(tipoElegido);
        }

        // Generar y encontrar el personaje aleatorio de manera similar
        Random random = new Random();
        TipoPersonaje tipoAleatorio;
        Personaje personajeAleatorio = null;

        do
        {
            tipoAleatorio = (TipoPersonaje)random.Next(Enum.GetValues(typeof(TipoPersonaje)).Length);
            foreach (var personaje in personajesExistentes)
            {
                if (personaje.Datos.Tipo == tipoAleatorio)
                {
                    personajeAleatorio = personaje;
                    break;
                }
            }
            
            if (personajeAleatorio == null)
            {
                personajeAleatorio = fabrica.GenerarPersonajeAleatorio(tipoAleatorio);
            }
        } while (personajeAleatorio == null);

        //mostrando personajes a combatir
        MostrarDatosPersonaje("usuario", personajeUsuario, tipoElegido);
        MostrarDatosPersonaje("aleatorio", personajeAleatorio, tipoAleatorio);

        // Verificar existencia y guardar personajes no existentes
        if (!personajesExistentes.Exists(p => p.Datos.Tipo == tipoElegido))
        {
            personajeJson.AgregarPersonajes(new List<Personaje> { personajeUsuario }, "personajes.json");
        }
        
        if (!personajesExistentes.Exists(p => p.Datos.Tipo == tipoAleatorio))
        {
            personajeJson.AgregarPersonajes(new List<Personaje> { personajeAleatorio }, "personajes.json");
        }


        // Obtener el estado del tiempo
        Clima clima = await climaAPI.ObtenerEstadoTiempo(url);
        if (clima != null && clima.weather != null && clima.weather.Length > 0)
        {
            Console.WriteLine($"El clima actual es: {clima.weather[0].main}");
            Console.WriteLine($"La hora del amanecer es: {clima.sys.sunrise}");
            Console.WriteLine($"La puesta del sol es: {clima.sys.sunset}");
        }
        
    }

    private void MostrarDatosPersonaje(string tipo, Personaje personaje, TipoPersonaje tipoPersonaje)
    {
        Console.WriteLine($"\nDatos del personaje {tipo}:");
        Console.WriteLine($"Tipo: {tipoPersonaje}");
        Console.WriteLine($"Nombre: {personaje.Datos.Nombre}");
        Console.WriteLine($"Apodo: {personaje.Datos.Apodo}");
        Console.WriteLine($"Fecha de Nacimiento: {personaje.Datos.FechaDeNacimiento} a.E.C");
        Console.WriteLine($"Edad: {personaje.Datos.Edad}");

        Console.WriteLine($"\nCaracterísticas del personaje {tipo}:");
        Console.WriteLine($"Velocidad: {personaje.Caracteristicas.Velocidad}");
        Console.WriteLine($"Destreza: {personaje.Caracteristicas.Destreza}");
        Console.WriteLine($"Fuerza: {personaje.Caracteristicas.Fuerza}");
        Console.WriteLine($"Nivel: {personaje.Caracteristicas.Nivel}");
        Console.WriteLine($"Armadura: {personaje.Caracteristicas.Armadura}");
        Console.WriteLine($"Salud: {personaje.Caracteristicas.Salud}");
        
    }

    /*    private string TraducirEstadoDelClima(string estadoEnIngles)
{
    Dictionary<string, string> traducciones = new Dictionary<string, string>
    {
        {"Thunderstorm", "Tormenta"},
        {"Drizzle", "Llovizna"},
        {"Rain", "Lluvia"},
        {"Snow", "Nieve"},
        {"Mist", "Niebla"},
        {"Smoke", "Humo"},
        {"Haze", "Neblina"},
        {"Fog", "Niebla"},
        {"Sand", "Arena"},
        {"Dust", "Ventoso"},
        {"Ash", "Ceniza"},
        {"Squall", "Chubasco"},
        {"Tornado", "Tornado"},
        {"Clear", "Despejado"},
        {"Clouds", "Nublado"}
        // Puedes agregar más traducciones según tus necesidades
    };

    if (traducciones.ContainsKey(estadoEnIngles))
    {
        return traducciones[estadoEnIngles];
    }
    else
    {
        // Si no hay una traducción disponible, devolver el estado en inglés
        return estadoEnIngles;
    }
}
    */
}

