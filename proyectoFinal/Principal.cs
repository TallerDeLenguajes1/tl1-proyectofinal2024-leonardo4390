using System;
using System.Threading.Tasks;


class Principal
{
    
    private string archivoPersonajeJson = "personajes.json";
    private string archivoHistorialJson = "historial.json";

    public async Task Menu()
    {
        TipoPersonaje tipoElegido;
        FabricaDePersonajes fabrica = new FabricaDePersonajes();
        PersonajeJson personajeJson = new PersonajeJson();
        HistorialJson historialJson = new HistorialJson();
        ConsumoAPI climaAPI = new ConsumoAPI();
        Random random = new Random();

        MostrarTitulo();
        MostrarSubtitulo();
        Console.WriteLine();
        Thread.Sleep(2000);

        //menu de personajes
        while (true)
        {
            Console.WriteLine("\nElige un personaje de tu preferencia");
            Console.WriteLine("* Guerrero");
            Console.WriteLine("* Mago");
            Console.WriteLine("* Arquero");
            Console.WriteLine("* Asesino");
            Console.WriteLine("* Nigromante");
            Console.WriteLine("* Barbaro");
            Console.WriteLine("* Hechicero");
            Console.WriteLine("* Bruja");
            Console.WriteLine();

            var elegido = Console.ReadLine();
            //verificacion de entrada del tipo personaje
            if (!int.TryParse(elegido, out _))
            {
                if (Enum.TryParse<TipoPersonaje>(elegido,true, out tipoElegido))
                {
                    break;
                }
                else{
                    Console.WriteLine("Tipo de personaje incorrecto, ingrese otra vez");
                }
            }
            else{
                Console.WriteLine("Tipo de personaje incorrecto, ingrese otra vez");
            } 
        }
        
        //leyendo personajes existentes
        //generando y verificando existencia de los personaje usuario 
        List<Personaje> personajesExistentes = personajeJson.LeerPersonajes(archivoPersonajeJson);

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

        if (personajeUsuario == null)
        {
            personajeUsuario = fabrica.GenerarPersonajeAleatorio(tipoElegido);
        }

        //generando personaje aleatorio y verificando existencia de los personaje aleatorio
        TipoPersonaje tipoAleatorio;
        Personaje personajeAleatorio = null;
        Console.WriteLine("\nGenerando Personaje Aleatorio");
        Thread.Sleep(2000);
        Console.WriteLine("\nCargando Personajes");
        Thread.Sleep(2000);

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

        //verificar existencia y guardar personajes no existentes
        if (!personajesExistentes.Exists(p => p.Datos.Tipo == tipoElegido))
        {
            personajeJson.AgregarPersonajes(new List<Personaje>{personajeUsuario}, archivoPersonajeJson);
        }
        
        if (!personajesExistentes.Exists(p => p.Datos.Tipo == tipoAleatorio))
        {
            personajeJson.AgregarPersonajes(new List<Personaje>{personajeAleatorio}, archivoPersonajeJson);
        }

        //obtener el estado del tiempo
        Clima clima = await climaAPI.ObtenerEstadoTiempo();
        //obtener la hora de salida del sol y puesta de sol (en UTC)
        //amanecar
        //obtener la hora de salida del sol y puesta de sol (en UTC)
        int amanecer=0;
        int atardecer=0;
        var estadoDelClima="";
        if (clima != null && clima.weather != null && clima.weather.Length > 0)
        {
            estadoDelClima = TraducirEstadoDelClima(clima.weather[0].main);
            var horaAmanecer = ConvertirUnixEnTiempoLocal(clima.sys.sunrise);
            var horaAtardecer = ConvertirUnixEnTiempoLocal(clima.sys.sunset);
            amanecer = Convert.ToInt32(horaAmanecer.ToString("HH"));
            atardecer = Convert.ToInt32(horaAtardecer.ToString("HH"));
            Console.WriteLine($"El estado del tiempo es: {estadoDelClima}");
            //Console.WriteLine($"El clima actual es: {clima.weather[0].main}");
            //Console.WriteLine($"La hora del amanecer es: {horaAmanecer}");
            //Console.WriteLine($"La puesta del sol es: {horaAtardecer}");
            
        }
        else{
            Console.WriteLine("Error, no se pudo cargar los datos del clima");
        }
        //Console.WriteLine($"La hora del amanecer es: {amanecer}");
        //Console.WriteLine($"La puesta del sol es: {atardecer}");
        
        
        //generando combate
        int primeraVueltaUsuario = 0;
        int primeraVueltaAleatorio = 0;
        Console.WriteLine("\nComienza el combate!");
        Thread.Sleep(3000);
        Console.WriteLine("\n----------------------------------------------------\n");
        while (personajeUsuario.Caracteristicas.Salud > 0 && personajeAleatorio.Caracteristicas.Salud > 0)
        {
            //personaje atacante usuario
            int danioGuerrero1;
            int puntoBeneficiosGuerrero1=0;
            if (amanecer > 0 && atardecer > 0 && estadoDelClima != null)
            {
                puntoBeneficiosGuerrero1 = PuntosBeneficio(personajeUsuario,amanecer, atardecer,estadoDelClima);
            }
            if (puntoBeneficiosGuerrero1 > 0 && primeraVueltaUsuario == 0)
            {
                danioGuerrero1 = CalcularDanio(personajeUsuario,personajeAleatorio) + puntoBeneficiosGuerrero1;
                personajeAleatorio.Caracteristicas.Salud -= danioGuerrero1;
                Console.WriteLine($"Al guerrero {personajeUsuario.Datos.Nombre} se le dio un beneficio por {puntoBeneficiosGuerrero1} punto por estado del clima");
                primeraVueltaUsuario = 1;

            }
            else{
                danioGuerrero1 = CalcularDanio(personajeUsuario,personajeAleatorio);
                personajeAleatorio.Caracteristicas.Salud -= danioGuerrero1;
            }
            

            Console.WriteLine($"{personajeUsuario.Datos.Nombre} ataca a {personajeAleatorio.Datos.Nombre} y le inflige {danioGuerrero1} puntos de daño");
            //comprobando salud del personaje defiende aleatorio
            if (personajeAleatorio.Caracteristicas.Salud <= 0)
            {
                Console.WriteLine("\n----------------------------------------------------\n");
                Console.WriteLine($"\n{personajeAleatorio.Datos.Nombre} ha sido derrotado!");
                
                // Validando ganador y guardando historial
                if (personajeUsuario.Caracteristicas.Salud > 0)
                {
                    historialJson.GuardarGanador(personajeUsuario, archivoHistorialJson);
                    Console.WriteLine($"{personajeUsuario.Datos.Nombre} es el ganador!");
                    Bono(personajeUsuario);
                
                    MensajeGanador(personajeUsuario);
                }
                else
                {
                    Console.WriteLine($"{personajeAleatorio.Datos.Nombre} también ha caído durante la batalla. No hay un ganador");
                }
                break;
            }

            //personaje atacante aleatorio
            int danioGuerrero2;
            int puntoBeneficiosGuerrero2=0;
            if (amanecer > 0 && atardecer > 0 && estadoDelClima != null)
            {
                puntoBeneficiosGuerrero2 = PuntosBeneficio(personajeAleatorio,amanecer, atardecer,estadoDelClima);
            }
                
            if (puntoBeneficiosGuerrero2 > 0 && primeraVueltaAleatorio == 0)
            {
                danioGuerrero2 = CalcularDanio(personajeAleatorio,personajeUsuario);
                Console.WriteLine($"Al guerrero {personajeAleatorio.Datos.Nombre} se le dio un beneficio por {puntoBeneficiosGuerrero2} punto por estado del clima");
                personajeUsuario.Caracteristicas.Salud -= danioGuerrero2;
                primeraVueltaAleatorio = 1;
                
            }
            else{
                danioGuerrero2 = CalcularDanio(personajeAleatorio,personajeUsuario);
                personajeUsuario.Caracteristicas.Salud -= danioGuerrero2;
            }
            
            Console.WriteLine($"{personajeAleatorio.Datos.Nombre} ataca a {personajeUsuario.Datos.Nombre} y le inflige {danioGuerrero2} puntos de daño");
            //comprobando salud del personaje defiende usuario
            if (personajeUsuario.Caracteristicas.Salud <= 0)
            {
                Console.WriteLine("\n----------------------------------------------------\n");
                Console.WriteLine($"\n{personajeUsuario.Datos.Nombre} ha sido derrotado!");

                // Validando ganador y guardando historial
                if (personajeAleatorio.Caracteristicas.Salud > 0)
                {
                    historialJson.GuardarGanador(personajeAleatorio, archivoHistorialJson);
                    Console.WriteLine($"{personajeAleatorio.Datos.Nombre} es el ganador!");
                    Bono(personajeAleatorio);
                    MensajeGanador(personajeAleatorio);
                }
                else
                {
                    Console.WriteLine($"{personajeAleatorio.Datos.Nombre} también ha caído durante la batalla. No hay un ganador");
                }
            break;
            } 

        }
        
        //mostrando personajes con mas victorias
        Console.WriteLine("\n----------------------------------------------------\n");
        Datos personajeMasVictorioso = historialJson.ObtenerPersonajeMasVictorioso("historial.json");
        if (personajeMasVictorioso != null)
        {
           int numeroVictorias = historialJson.ObtenerNumerosVictorias(personajeMasVictorioso.Nombre, "historial.json");
           Console.WriteLine($"El personaje con más victorias es {personajeMasVictorioso.Nombre} con {numeroVictorias} victoria/s");
        }
        else
        {
           Console.WriteLine("No hay registros de victorias aún");
        }
        
        
    }

    private void MostrarTitulo()
    {
        string titulo =@" __      __        _   _               ___ _         _    
 \ \    / /__ __ _| |_| |_  ___ _ _   / __| |__ _ __| |_  
  \ \/\/ / -_) _` |  _| ' \/ -_) '_| | (__| / _` (_-< ' \ 
   \_/\_/\___\__,_|\__|_||_\___|_|    \___|_\__,_/__/_||_|
                                                          ";

        Console.WriteLine(titulo);
    }
    private void MostrarSubtitulo()
    {
        string subtitulo = "\t\t\tDuelos Miticos";

        foreach (var letra in subtitulo)
        {
            Console.Write(letra);
            Thread.Sleep(100);
        }

    }
    private void MostrarDatosPersonaje(string tipo, Personaje personaje, TipoPersonaje tipoPersonaje)
    {
        Console.WriteLine("\n----------------------------------------------------\n");
        Console.WriteLine($"Datos del personaje {tipo}");
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
        Console.WriteLine();
        
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
    private int CalcularDanio(Personaje guerreroAtacante, Personaje guerreroDefensor)
    {
        Random random = new Random();
        int ataque = guerreroAtacante.Caracteristicas.Destreza * guerreroAtacante.Caracteristicas.Fuerza * guerreroAtacante.Caracteristicas.Nivel;
        int efectividad = random.Next(1, 101);
        int defensa = guerreroDefensor.Caracteristicas.Armadura * guerreroDefensor.Caracteristicas.Velocidad;
        int constante = 500;
        int danioProvocado = ((ataque * efectividad) - defensa) / constante;
        return danioProvocado;
    }
    //conversion de fecha
    private DateTime ConvertirUnixEnTiempoLocal(long unixTimeStamp)
    {
        // Unix timestamp is seconds past epoch
        var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
        return dateTime;
    }
    //calcular puntos beneficio
    private int PuntosBeneficio(Personaje personaje,int horaAmanecer, int horaAtardecer,string estadoClima)
    {
        Random random = new Random();
        DateTime hora = DateTime.Now;
        int actualHora = Convert.ToInt32(hora.ToString("HH"));
        int puntosBeneficios=0;
        if (personaje.Datos.Beneficio == "Soleado")
        {
            if (estadoClima == "Despejado" && actualHora > horaAmanecer && actualHora < horaAtardecer)
            {
                puntosBeneficios = random.Next(1,11);
            }
            
        }
        else{
            if (personaje.Datos.Beneficio == "Noche")
            {
                if (actualHora > horaAtardecer && actualHora < horaAmanecer)
                {
                    puntosBeneficios = random.Next(1,11);
                }
            }
            else{
                if (personaje.Datos.Beneficio == estadoClima)
                {
                    puntosBeneficios = random.Next(1,11);
                }
            }
        }
        return puntosBeneficios;
        
    }

    //otorgar bono
    private void Bono(Personaje ganador)
    {
        Random random = new Random();
        int bono = random.Next(2) == 0 ? 5 : 10;
        if (random.Next(2) == 0)
        {
            //ajustando para que no se pase de limite 1-10
            int ajusteBono = Math.Min(bono, 10 - ganador.Caracteristicas.Fuerza);
            ganador.Caracteristicas.Fuerza += ajusteBono;
            Console.WriteLine($"{ganador.Datos.Nombre} recibe un bono de {bono} puntos en Fuerza.");
        }
        else
        {
            //ajustando para que no se pase de limite 1-10
            int ajusteBono = Math.Min(bono, 10 - ganador.Caracteristicas.Armadura);
            ganador.Caracteristicas.Armadura += ajusteBono;
            Console.WriteLine($"{ganador.Datos.Nombre} recibe un bono de {bono} puntos en Armadura.");
        }


        //guardar los cambios en personajes.json
        PersonajeJson personajeJson = new PersonajeJson();
        personajeJson.ActualizarPersonaje(ganador, archivoPersonajeJson);
    }

    //mensaje al ganador
    private void MensajeGanador(Personaje ganador)
    {
        Console.WriteLine("\n----------------------------------------------------");
        Console.WriteLine($"    {ganador.Datos.Nombre} es el merecedor del Trono de Hierro!");
        Console.WriteLine("----------------------------------------------------\n");
        MostrarDatosPersonaje("ganador", ganador, ganador.Datos.Tipo);
    }
}








