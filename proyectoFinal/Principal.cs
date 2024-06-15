using System;

class Principal
{

    public void Menu()
    {
        TipoPersonaje tipoElegido;
        FabricaDePersonajes fabrica = new FabricaDePersonajes();

        while (true)
        {
            Console.WriteLine("\nElige un personaje:\n");
            Console.WriteLine("*Guerrero");
            Console.WriteLine("*Mago");
            Console.WriteLine("*Arquero");
            Console.WriteLine();

            string entrada = Console.ReadLine().Trim();

            // Verificacion para lo que se espera del menu de personajes
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
        //personaje aleatorio a combatir
        TipoPersonaje tipoAleatorio = (TipoPersonaje)new Random().Next(0, 3);
        //intanciando personajes del usuario y el aleatorio 
        Personaje personajeUsuario = fabrica.GenerarPersonajeAleatorio(tipoElegido);
        Personaje personajeAleatorio = fabrica.GenerarPersonajeAleatorio(tipoAleatorio);

        //mostrando personajes a combatir
        Console.WriteLine("\nDatos del personaje usuario:");
        Console.WriteLine($"Tipo: {tipoElegido}");
        Console.WriteLine($"Nombre: {personajeUsuario.Datos.Nombre}");
        Console.WriteLine($"Apodo: {personajeUsuario.Datos.Apodo}");
        Console.WriteLine($"Fecha de Nacimiento: {personajeUsuario.Datos.FechaDeNacimiento}");
        Console.WriteLine($"Edad: {personajeUsuario.Datos.Edad}");

        Console.WriteLine("\nCaracterísticas del personaje usuario:");
        Console.WriteLine($"Velocidad: {personajeUsuario.Caracteristicas.Velocidad}");
        Console.WriteLine($"Destreza: {personajeUsuario.Caracteristicas.Destreza}");
        Console.WriteLine($"Fuerza: {personajeUsuario.Caracteristicas.Fuerza}");
        Console.WriteLine($"Nivel: {personajeUsuario.Caracteristicas.Nivel}");
        Console.WriteLine($"Armadura: {personajeUsuario.Caracteristicas.Armadura}");
        Console.WriteLine($"Salud: {personajeUsuario.Caracteristicas.Salud}");

        Console.WriteLine("\nDatos del personaje aleatorio:");
        Console.WriteLine($"Tipo: {tipoAleatorio}");
        Console.WriteLine($"Nombre: {personajeAleatorio.Datos.Nombre}");
        Console.WriteLine($"Apodo: {personajeAleatorio.Datos.Apodo}");
        Console.WriteLine($"Fecha de Nacimiento: {personajeAleatorio.Datos.FechaDeNacimiento}");
        Console.WriteLine($"Edad: {personajeAleatorio.Datos.Edad}");

        Console.WriteLine("\nCaracterísticas del personaje  aleatorio:");
        Console.WriteLine($"Velocidad: {personajeAleatorio.Caracteristicas.Velocidad}");
        Console.WriteLine($"Destreza: {personajeAleatorio.Caracteristicas.Destreza}");
        Console.WriteLine($"Fuerza: {personajeAleatorio.Caracteristicas.Fuerza}");
        Console.WriteLine($"Nivel: {personajeAleatorio.Caracteristicas.Nivel}");
        Console.WriteLine($"Armadura: {personajeAleatorio.Caracteristicas.Armadura}");
        Console.WriteLine($"Salud: {personajeAleatorio.Caracteristicas.Salud}");
    }
}
