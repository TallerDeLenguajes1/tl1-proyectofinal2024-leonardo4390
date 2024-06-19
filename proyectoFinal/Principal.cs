using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

class Principal
{
    private string url = "TU_URL";

    public async Task Menu()
    {
        TipoPersonaje tipoElegido;
        FabricaDePersonajes fabrica = new FabricaDePersonajes();
        PersonajeJson personajeJson = new PersonajeJson();
        HistorialJson historialJson = new HistorialJson();
        ConsumoAPI climaAPI = new ConsumoAPI();

        while (true)
        {
            Console.WriteLine("\nElige un personaje:\n");
            Console.WriteLine("* Guerrero");
            Console.WriteLine("* Mago");
            Console.WriteLine("* Arquero");
            Console.WriteLine("* Asesino");
            Console.WriteLine("* Nigromante");
            Console.WriteLine("* Barbaro");
            Console.WriteLine("* Hechicero");
            Console.WriteLine("* Bruja");
            Console.WriteLine();

            string entrada = Console.ReadLine().Trim();

            // Verificacion para lo que se espera para personaje
            if (!int.TryParse(entrada, out _))
            {
                if (Enum.TryParse(entrada, true, out tipoElegido))
                {
                    if (tipoElegido == TipoPersonaje.Arquero || tipoElegido == TipoPersonaje.Guerrero || tipoElegido == TipoPersonaje.Mago
                    || tipoElegido == TipoPersonaje.Asesino ||tipoElegido == TipoPersonaje.Barbaro || tipoElegido == TipoPersonaje.Bruja
                    || tipoElegido == TipoPersonaje.Hechicero || tipoElegido == TipoPersonaje.Nigromante)
                    {
                        break;
                    }
                }
            }

            Console.WriteLine("Ingreso incorrecto, ingrese otra vez");
        }
        

        //leyendo personajes existentes
        List<Personaje> personajesExistentes = personajeJson.LeerPersonajes("personajes.json");

        //personajeUsuario como null
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

        //generando combate
        Console.WriteLine("\nComienza el combate!");
        Thread.Sleep(3000);

        while (personajeUsuario.Caracteristicas.Salud > 0 && personajeAleatorio.Caracteristicas.Salud > 0)
        {
            int danioGuerrero1 = CalcularDanio(personajeUsuario,personajeAleatorio);
            personajeAleatorio.Caracteristicas.Salud -= danioGuerrero1;

            Console.WriteLine($"{personajeUsuario.Datos.Nombre} ataca a {personajeAleatorio.Datos.Nombre} y le inflige {danioGuerrero1} puntos de daño.");

            if (personajeAleatorio.Caracteristicas.Salud <= 0)
            {
                Console.WriteLine("\n---------------------------------\n");
                Console.WriteLine($"\n{personajeAleatorio.Datos.Nombre} ha sido derrotado!");
                Console.WriteLine($"{personajeUsuario.Datos.Nombre} es el ganador!");
                
                //generando historial
                // Validando ganador
                if (personajeUsuario.Caracteristicas.Salud > 0)
                {
                    historialJson.GuardarGanador(new List<Personaje> { personajeUsuario }, "HistorialJson.json");
                    Bono(personajeUsuario);
                    MensajeGanador(personajeUsuario);
                }
                else
                {
                    Console.WriteLine($"{personajeAleatorio.Datos.Nombre} también ha caído durante la batalla. No hay un ganador.");
                }
                break;
            }

            int danioGuerrero2 = CalcularDanio(personajeAleatorio,personajeUsuario);
            personajeUsuario.Caracteristicas.Salud -= danioGuerrero2;

            Console.WriteLine($"{personajeAleatorio.Datos.Nombre} ataca a {personajeUsuario.Datos.Nombre} y le inflige {danioGuerrero2} puntos de daño.");

            if (personajeUsuario.Caracteristicas.Salud <= 0)
            {
                Console.WriteLine("\n-------------------------------\n");
                Console.WriteLine($"\n{personajeUsuario.Datos.Nombre} ha sido derrotado!");
                Console.WriteLine($"{personajeAleatorio.Datos.Nombre} es el ganador!");
                
                //generando historial
                // Validando ganador
                if (personajeAleatorio.Caracteristicas.Salud > 0)
                {
                    historialJson.GuardarGanador(new List<Personaje> { personajeAleatorio }, "HistorialJson.json");
                    Bono(personajeAleatorio);
                    MensajeGanador(personajeAleatorio);
                }
                else
                {
                    Console.WriteLine($"{personajeAleatorio.Datos.Nombre} también ha caído durante la batalla. No hay un ganador.");
                }
            break;
        }
    }


        // Obtener el estado del tiempo
        Clima clima = await climaAPI.ObtenerEstadoTiempo(url);
        if (clima != null && clima.weather != null && clima.weather.Length > 0)
        {
            string estadoDelClima = TraducirEstadoDelClima(clima.weather[0].main);

            if (personajeUsuario.Datos.Beneficio == estadoDelClima)
            {
                Console.WriteLine("si");
            }
            Console.WriteLine($"El estado del tiempo es: {estadoDelClima}");
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
        Console.WriteLine($"Beneficio: {personaje.Datos.Beneficio}");

        Console.WriteLine($"\nCaracterísticas del personaje {tipo}:");
        Console.WriteLine($"Velocidad: {personaje.Caracteristicas.Velocidad}");
        Console.WriteLine($"Destreza: {personaje.Caracteristicas.Destreza}");
        Console.WriteLine($"Fuerza: {personaje.Caracteristicas.Fuerza}");
        Console.WriteLine($"Nivel: {personaje.Caracteristicas.Nivel}");
        Console.WriteLine($"Armadura: {personaje.Caracteristicas.Armadura}");
        Console.WriteLine($"Salud: {personaje.Caracteristicas.Salud}");
        
    }

    private string TraducirEstadoDelClima(string climaIngles)
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
        };
        //traduciendo, cambiando clave por valor
        if (traducciones.ContainsKey(climaIngles))
        {
            return traducciones[climaIngles];
        }
        else
        {
            return climaIngles;
        }
    }
    public int CalcularDanio(Personaje guerreroAtacante, Personaje guerreroDefensor)
    {
        Random random = new Random();
        int ataque = guerreroAtacante.Caracteristicas.Destreza * guerreroAtacante.Caracteristicas.Fuerza * guerreroAtacante.Caracteristicas.Nivel;
        int efectividad = random.Next(1, 101);
        int defensa = guerreroDefensor.Caracteristicas.Armadura * guerreroDefensor.Caracteristicas.Velocidad;
        int constante = 500;
        int danioProvocado = ((ataque * efectividad) - defensa) / constante;
        return danioProvocado;
    }

    //otorgar bono
    private void Bono(Personaje ganador)
    {
        Random random = new Random();
        int bono = random.Next(2) == 0 ? 5 : 10;
        if (random.Next(2) == 0)
        {
            ganador.Caracteristicas.Fuerza += bono;
            Console.WriteLine($"{ganador.Datos.Nombre} recibe un bono de {bono} puntos en Fuerza.");
        }
        else
        {
            ganador.Caracteristicas.Armadura += bono;
            Console.WriteLine($"{ganador.Datos.Nombre} recibe un bono de {bono} puntos en Armadura.");
        }
    }

    //mensaje al ganador
    private void MensajeGanador(Personaje ganador)
    {
        Console.WriteLine("\n------------------------------------------------");
        Console.WriteLine($" ¡{ganador.Datos.Nombre} es el merecedor del Trono de Hierro!");
        Console.WriteLine("------------------------------------------------\n");
        MostrarDatosPersonaje("ganador", ganador, ganador.Datos.Tipo);
    }
}


