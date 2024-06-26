using System;
using System.Threading.Tasks;


class Principal
{
    
    private string archivoPersonajeJson = "personajes.json";
    private string archivoHistorialJson = "historial.json";
    private const int NUMERO_PERSONAJES = 2;
    private Random random = new Random();
    private FabricaDePersonajes fabrica = new FabricaDePersonajes();
    private PersonajeJson personajeJson = new PersonajeJson();

    public async Task Menu()
    {
        TipoPersonaje tipoElegido;
        //FabricaDePersonajes fabrica = new FabricaDePersonajes();
        //PersonajeJson personajeJson = new PersonajeJson();
        HistorialJson historialJson = new HistorialJson();
        ConsumoAPI climaAPI = new ConsumoAPI();
        Random random = new Random();

        MostrarTitulo();
        MostrarSubtitulo();
        Console.WriteLine();
        Thread.Sleep(2000);

        List<Personaje> personajesExistentes = personajeJson.LeerPersonajes(archivoPersonajeJson);
        //generando personajes del usuario
        List<Personaje> personajesUsuario = GenerarPersonajesUsuario(personajesExistentes);

        //generando personajes aleatorios
        List<Personaje> personajesAleatorios = GenerarPersonajesAleatorios(personajesExistentes);
        // //generacion de personajes usuario
        // List<Personaje> personajesUsuario = new List<Personaje>();
        // for (int i = 0; i < NUMERO_PERONAJES; i++)
        // {
        //     //menu de personajes
        //     while (true)
        //     {
        //         //menu personaje
        //         MenuPersonaje();

        //         var elegido = Console.ReadLine();
        //         //verificacion de entrada del tipo personaje
        //         if (!int.TryParse(elegido, out _))
        //         {
        //             if (Enum.TryParse<TipoPersonaje>(elegido,true, out tipoElegido))
        //             {
        //                 break;
        //             }
        //             else{
        //                 Console.WriteLine("Tipo de personaje incorrecto, ingrese otra vez");
        //             }
        //         }
        //         else{
        //             Console.WriteLine("Tipo de personaje incorrecto, ingrese otra vez");
        //         } 
        //     }
            
        //     //leyendo personajes existentes y verificando existencia
        //     Personaje personajeUsuario = personajesExistentes.Find(p => p.Datos.Tipo == tipoElegido);
        //     if (personajeUsuario == null)
        //     {
        //         personajeUsuario = fabrica.GenerarPersonajeAleatorio(tipoElegido);
        //         personajesExistentes.Add(personajeUsuario);
        //     }

        //     personajesUsuario.Add(personajeUsuario);
            
        // }
        

        //  //generacion de personajes aleatorios
        // List<Personaje> personajesAleatorios = new List<Personaje>();
        // Console.WriteLine("\nGenerando Personaje Aleatorio");
        // Thread.Sleep(2000);
        // Console.WriteLine("\nCargando Personajes");
        // Thread.Sleep(2000);

        // for (int i = 0; i < NUMERO_PERONAJES; i++)
        // {
        //     TipoPersonaje tipoAleatorio;
        //     Personaje personajeAleatorio = null;

        //     do
        //     {
        //         tipoAleatorio = (TipoPersonaje)random.Next(Enum.GetValues(typeof(TipoPersonaje)).Length);
        //         personajeAleatorio = personajesExistentes.Find(p => p.Datos.Tipo == tipoAleatorio);
        //         if (personajeAleatorio == null)
        //         {
        //             personajeAleatorio = fabrica.GenerarPersonajeAleatorio(tipoAleatorio);
        //             personajesExistentes.Add(personajeAleatorio);
        //         }
        //     } while (personajeAleatorio == null);

        //     personajesAleatorios.Add(personajeAleatorio);
        // }

        //mostrando personajes a combatir
        PersonajeACombatir(personajesUsuario,personajesAleatorios);
        // Console.WriteLine("\nPersonajes a Combatir");
        // Thread.Sleep(2000);
        // foreach (var personaje in personajesUsuario)
        // {
        //     MostrarDatosPersonaje("usuario", personaje, personaje.Datos.Tipo);
        // }

        // foreach (var personaje in personajesAleatorios)
        // {
        //     MostrarDatosPersonaje("aleatorio", personaje, personaje.Datos.Tipo);
        // }
        
        //guardar personajes en el archivo JSON
        personajeJson.GuardarPersonajes(personajesExistentes, archivoPersonajeJson);

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
        Console.WriteLine($"La hora del amanecer es: {amanecer}");
        Console.WriteLine($"La puesta del sol es: {atardecer}");
        
        
        //generando combate
        // int primeraVueltaUsuario = 0;
        // int primeraVueltaAleatorio = 0;
        // Console.WriteLine("\nComienza el combate!");
        // Thread.Sleep(3000);
        // Console.WriteLine("\n----------------------------------------------------\n");
        
        //combate
        // Realizar combate
        Personaje ganador = RealizarCombate(personajesUsuario, personajesAleatorios, estadoDelClima,amanecer,atardecer);
        // int indiceUsuario = 0;
        // int indiceAleatorio = 0;
        // while (indiceUsuario < personajesUsuario.Count && indiceAleatorio < personajesAleatorios.Count)
        // {
        //     var personajeUsuario = personajesUsuario[indiceUsuario];
        //     var personajeAleatorio = personajesAleatorios[indiceAleatorio];

        //     while (personajeUsuario.Caracteristicas.Salud > 0 && personajeAleatorio.Caracteristicas.Salud > 0)
        //     {
        //         //ataque del personaje usuario
        //         int danioUsuario;
        //         int puntosBeneficioUsuario = PuntosBeneficio(personajeUsuario, amanecer, atardecer, estadoDelClima);
        //         if (puntosBeneficioUsuario > 0 && primeraVueltaUsuario == 0)
        //         {
        //             danioUsuario = CalcularDanio(personajeUsuario, personajeAleatorio) + puntosBeneficioUsuario;
        //             Console.WriteLine($"Al guerrero {personajeUsuario.Datos.Nombre} se le dio un beneficio por {puntosBeneficioUsuario} puntos por estado del clima");
        //             primeraVueltaUsuario ++;
        //         }
        //         else
        //         {
        //             danioUsuario = CalcularDanio(personajeUsuario, personajeAleatorio);
        //         }
        //         personajeAleatorio.Caracteristicas.Salud -= danioUsuario;
        //         Console.WriteLine($"{personajeUsuario.Datos.Nombre} ataca a {personajeAleatorio.Datos.Nombre} y le inflige {danioUsuario} puntos de daño");

        //         //verificar de salud del personaje aleatorio
        //         if (personajeAleatorio.Caracteristicas.Salud <= 0)
        //         {
        //             Console.WriteLine($"\n{personajeAleatorio.Datos.Nombre} ha sido derrotado!");
        //             //historialJson.GuardarGanador(personajeUsuario, archivoHistorialJson);
        //             Bono(personajeUsuario);
        //             break;
        //         }

        //         //ataque del personaje aleatorio
        //         int danioAleatorio;
        //         int puntosBeneficioAleatorio = PuntosBeneficio(personajeAleatorio, amanecer, atardecer, estadoDelClima);
        //         if (puntosBeneficioAleatorio > 0 && primeraVueltaAleatorio == 0)
        //         {
        //             danioAleatorio = CalcularDanio(personajeAleatorio, personajeUsuario) + puntosBeneficioAleatorio;
        //             Console.WriteLine($"Al guerrero {personajeAleatorio.Datos.Nombre} se le dio un beneficio por {puntosBeneficioAleatorio} puntos por estado del clima");
        //             primeraVueltaAleatorio ++;
        //         }
        //         else
        //         {
        //             danioAleatorio = CalcularDanio(personajeAleatorio, personajeUsuario);
        //         }
        //         personajeUsuario.Caracteristicas.Salud -= danioAleatorio;
        //         Console.WriteLine($"{personajeAleatorio.Datos.Nombre} ataca a {personajeUsuario.Datos.Nombre} y le inflige {danioAleatorio} puntos de daño");

        //         //verificar de salud del personaje usuario
        //         if (personajeUsuario.Caracteristicas.Salud <= 0)
        //         {
        //             Console.WriteLine($"\n{personajeUsuario.Datos.Nombre} ha sido derrotado!");
        //             //historialJson.GuardarGanador(personajeAleatorio, archivoHistorialJson);
        //             Bono(personajeAleatorio);
        //             break;
        //         }
        //     }

        //     //seguir con el siguiente personaje si el actual ha sido derrotado
        //     if (personajeUsuario.Caracteristicas.Salud <= 0)
        //     {
        //         indiceUsuario++;
        //         primeraVueltaUsuario = 0;
        //     }

        //     if (personajeAleatorio.Caracteristicas.Salud <= 0)
        //     {
        //         indiceAleatorio++;
        //         primeraVueltaAleatorio = 0;
        //     }
        // }

        //ganador del combate
        
        GanadorDelCombate(personajesUsuario, personajesAleatorios, ganador);
        // Personaje ganador = null;
        // if (indiceUsuario >= personajesUsuario.Count && indiceAleatorio < personajesAleatorios.Count)
        // {
        //     Console.WriteLine("\n¡Los personajes aleatorios han ganado el combate!");
        //     ganador = personajesAleatorios[indiceAleatorio];
        // }
        // else if (indiceAleatorio >= personajesAleatorios.Count && indiceUsuario < personajesUsuario.Count)
        // {
        //     Console.WriteLine("\n¡Los personajes del usuario han ganado el combate!");
        //     ganador = personajesUsuario[indiceUsuario];
        // }
        // else
        // {
        //     Console.WriteLine("\nNo hay un ganador definitivo.");
        // }

        // if (ganador != null)
        // {
        //     MensajeGanador(ganador);

        //     //solicitud de guardado o no guardado
        //     Console.Write("\n¿Desea guardar al ganador en el historial? (s para si): ");
        //     string respuesta = Console.ReadLine().ToLower();
        //     if (respuesta == "s")
        //     {
        //         historialJson.GuardarGanador(ganador, archivoHistorialJson);
        //         Console.WriteLine("Ganador guardado en el historial");
        //     }
        // }
        //restableciendo salud
        RestablecerSalud(personajesExistentes);
        personajeJson.GuardarPersonajes(personajesExistentes, archivoPersonajeJson);

        // //mostrar personajes con mas victorias
        // var topDosPersonajes = historialJson.ObtenerDosPersonajesMasVictoriosos(archivoHistorialJson);
        // Console.WriteLine("\nLos 2 personajes con más victorias:");

        // if (topDosPersonajes.Count == 0)
        // {
        //     Console.WriteLine("No hay victorias registradas");
        // }
        // else
        // {
        //     foreach (var personaje in topDosPersonajes)
        //     {
        //         Console.WriteLine($"Nombre: {personaje.Datos.Nombre} | Tipo: {personaje.Datos.Tipo} | Victorias: {personaje.Victorias}");
        //     }
        // }
        //personajes con mas victorias
        PersonajesConMasVictorias();
        // Console.WriteLine("\n----------------------------------------------------\n");
        
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

    private void MenuPersonaje()
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
    }
    //generando personajes
    private List<Personaje> GenerarPersonajesUsuario(List<Personaje> personajesExistentes)
    {
        List<Personaje> personajesUsuario = new List<Personaje>();
        for (int i = 0; i < NUMERO_PERSONAJES; i++)
        {
            TipoPersonaje tipoElegido;

            while (true)
            {
                MenuPersonaje();

                var elegido = Console.ReadLine();
                if (!int.TryParse(elegido, out _) && Enum.TryParse<TipoPersonaje>(elegido, true, out tipoElegido))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Tipo de personaje incorrecto, ingrese otra vez");
                }
            }

            Personaje personajeUsuario = personajesExistentes.Find(p => p.Datos.Tipo == tipoElegido);
            if (personajeUsuario == null)
            {
                personajeUsuario = fabrica.GenerarPersonajeAleatorio(tipoElegido);
                personajesExistentes.Add(personajeUsuario);
            }

            personajesUsuario.Add(personajeUsuario);
        }

        return personajesUsuario;
    }

    private List<Personaje> GenerarPersonajesAleatorios(List<Personaje> personajesExistentes)
    {
        List<Personaje> personajesAleatorios = new List<Personaje>();
        Console.WriteLine("\nGenerando Personaje Aleatorio");
        Thread.Sleep(2000);
        Console.WriteLine("\nCargando Personajes");
        Thread.Sleep(2000);

        for (int i = 0; i < NUMERO_PERSONAJES; i++)
        {
            TipoPersonaje tipoAleatorio;
            Personaje personajeAleatorio = null;

            do
            {
                tipoAleatorio = (TipoPersonaje)random.Next(Enum.GetValues(typeof(TipoPersonaje)).Length);
                personajeAleatorio = personajesExistentes.Find(p => p.Datos.Tipo == tipoAleatorio);
                if (personajeAleatorio == null)
                {
                    personajeAleatorio = fabrica.GenerarPersonajeAleatorio(tipoAleatorio);
                    personajesExistentes.Add(personajeAleatorio);
                }
            } while (personajeAleatorio == null);

            personajesAleatorios.Add(personajeAleatorio);
        }

        return personajesAleatorios;
    }

    //personajes a combatir
    private void PersonajeACombatir(List<Personaje> personajesUsuario, List<Personaje> personajesAleatorios)
    {
        Console.WriteLine("\nPersonajes a Combatir");
        Thread.Sleep(2000);
        foreach (var personaje in personajesUsuario)
        {
            MostrarDatosPersonaje("usuario", personaje, personaje.Datos.Tipo);
        }

        foreach (var personaje in personajesAleatorios)
        {
            MostrarDatosPersonaje("aleatorio", personaje, personaje.Datos.Tipo);
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
    //simulando combate
    private Personaje RealizarCombate(List<Personaje> personajesUsuario, List<Personaje> personajesAleatorios, string estadoDelClima, int amanecer, int atardecer)
    {
        int primeraVueltaUsuario = 0;
        int primeraVueltaAleatorio = 0;

        Console.WriteLine("\nComienza el combate!");
        Thread.Sleep(3000);
        Console.WriteLine("\n----------------------------------------------------\n");

        int indiceUsuario = 0;
        int indiceAleatorio = 0;

        while (indiceUsuario < personajesUsuario.Count && indiceAleatorio < personajesAleatorios.Count)
        {
            var personajeUsuario = personajesUsuario[indiceUsuario];
            var personajeAleatorio = personajesAleatorios[indiceAleatorio];

            while (personajeUsuario.Caracteristicas.Salud > 0 && personajeAleatorio.Caracteristicas.Salud > 0)
            {
                int danioUsuario = Ataque(personajeUsuario, personajeAleatorio, amanecer, atardecer, estadoDelClima, ref primeraVueltaUsuario);
                personajeAleatorio.Caracteristicas.Salud -= danioUsuario;

                if (personajeAleatorio.Caracteristicas.Salud <= 0)
                {
                    Console.WriteLine($"\n{personajeAleatorio.Datos.Nombre} ha sido derrotado!");
                    Bono(personajeUsuario);
                    break;
                }

                int danioAleatorio = Ataque(personajeAleatorio, personajeUsuario, amanecer, atardecer, estadoDelClima, ref primeraVueltaAleatorio);
                personajeUsuario.Caracteristicas.Salud -= danioAleatorio;

                if (personajeUsuario.Caracteristicas.Salud <= 0)
                {
                    Console.WriteLine($"\n{personajeUsuario.Datos.Nombre} ha sido derrotado!");
                    Bono(personajeAleatorio);
                    break;
                }
            }

            if (personajeUsuario.Caracteristicas.Salud <= 0)
            {
                indiceUsuario++;
                primeraVueltaUsuario = 0;
            }

            if (personajeAleatorio.Caracteristicas.Salud <= 0)
            {
                indiceAleatorio++;
                primeraVueltaAleatorio = 0;
            }
        }

        if (indiceUsuario >= personajesUsuario.Count && indiceAleatorio < personajesAleatorios.Count)
        {
            Console.WriteLine("\n¡Los personajes aleatorios han ganado el combate!");
            return personajesAleatorios[indiceAleatorio];
        }
        else if (indiceAleatorio >= personajesAleatorios.Count && indiceUsuario < personajesUsuario.Count)
        {
            Console.WriteLine("\n¡Los personajes del usuario han ganado el combate!");
            return personajesUsuario[indiceUsuario];
        }
        else
        {
            Console.WriteLine("\nNo hay un ganador definitivo.");
            return null;
        }
    }

    private int Ataque(Personaje atacante, Personaje defensor, int amanecer, int atardecer, string estadoDelClima, ref int primeraVuelta)
    {
        int puntosBeneficio = PuntosBeneficio(atacante, amanecer, atardecer, estadoDelClima);
        int danio = CalcularDanio(atacante, defensor);

        if (puntosBeneficio > 0 && primeraVuelta == 0)
        {
            danio += puntosBeneficio;
            Console.WriteLine($"Al guerrero {atacante.Datos.Nombre} se le dio un beneficio por {puntosBeneficio} puntos por estado del clima");
            primeraVuelta++;
        }

        Console.WriteLine($"{atacante.Datos.Nombre} ataca a {defensor.Datos.Nombre} y le inflige {danio} puntos de daño");
        return danio;
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

    //ganador del combate
    private void GanadorDelCombate(List<Personaje> personajesUsuario, List<Personaje> personajesAleatorios, Personaje ganador)
    {
        if (ganador != null)
        {
            MensajeGanador(ganador);

            Console.Write("\n¿Desea guardar al ganador en el historial? (s para si): ");
            string respuesta = Console.ReadLine().ToLower();
            if (respuesta == "s")
            {
                HistorialJson historialJson = new HistorialJson();
                historialJson.GuardarGanador(ganador, archivoHistorialJson);
                Console.WriteLine("Ganador guardado en el historial");
            }
        }
    }
    //mensaje al ganador
    private void MensajeGanador(Personaje ganador)
    {
        Console.WriteLine("\n----------------------------------------------------");
        Console.WriteLine($"    {ganador.Datos.Nombre} es el merecedor del Trono de Hierro!");
        Console.WriteLine("----------------------------------------------------\n");
        MostrarDatosPersonaje("ganador", ganador, ganador.Datos.Tipo);
    }

    //vover la salud a su  estado inicial
    private void RestablecerSalud(List<Personaje> personajes)
    {
        foreach (var personaje in personajes)
        {
            personaje.Caracteristicas.Salud = 100;
        }
    }

    private void PersonajesConMasVictorias()
    {
        HistorialJson historialJson = new HistorialJson();
        var topDosPersonajes = historialJson.ObtenerDosPersonajesMasVictoriosos(archivoHistorialJson);
        Console.WriteLine("\nLos 2 personajes con más victorias:");

        if (topDosPersonajes.Count == 0)
        {
            Console.WriteLine("No hay victorias registradas");
        }
        else
        {
            foreach (var personaje in topDosPersonajes)
            {
                Console.WriteLine($"Nombre: {personaje.Datos.Nombre} | Tipo: {personaje.Datos.Tipo} | Victorias: {personaje.Victorias}");
            }
        }

        Console.WriteLine("\n----------------------------------------------------\n");
    }

}









