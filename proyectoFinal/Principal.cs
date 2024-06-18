using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class Principal
{

    public void Menu()
    {
        TipoPersonaje tipoElegido;
        FabricaDePersonajes fabrica = new FabricaDePersonajes();
        PersonajeJson personajeJson = new PersonajeJson();

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
        // Verificar si el usuario ya existe
        

        
        //personaje aleatorio a combatir
        //TipoPersonaje tipoAleatorio = (TipoPersonaje)new Random().Next(0, 8);
        //intanciando personajes del usuario y el aleatorio 
        //Personaje personajeUsuario = fabrica.GenerarPersonajeAleatorio(tipoElegido);
        //Personaje personajeAleatorio = fabrica.GenerarPersonajeAleatorio(tipoAleatorio);
        List<Personaje> personajesExistentes = personajeJson.LeerPersonajes("personajes.json");

        Personaje personajeUsuario = personajesExistentes?.Find(p => p.Tipo == tipoElegido) ?? fabrica.GenerarPersonajeAleatorio(tipoElegido);

        Random random = new Random();
        TipoPersonaje tipoAleatorio;
        Personaje personajeAleatorio;

        do
        {
            tipoAleatorio = (TipoPersonaje)random.Next(Enum.GetValues(typeof(TipoPersonaje)).Length);
            personajeAleatorio = personajesExistentes?.Find(p => p.Tipo == tipoAleatorio) ?? fabrica.GenerarPersonajeAleatorio(tipoAleatorio);
        } while (personajeAleatorio == null);

        //mostrando personajes a combatir
        MostrarDatosPersonaje("usuario", personajeUsuario, tipoElegido);
        MostrarDatosPersonaje("aleatorio", personajeAleatorio, tipoAleatorio);

        // Verificar existencia y agregar personajes
        if (personajeJson.Existe("personajes.json"))
        {
            personajeJson.AgregarPersonajes(new List<Personaje> { personajeUsuario, personajeAleatorio }, "personajes.json");
        }
        else
        {
            personajeJson.GuardarPersonajes(new List<Personaje> { personajeUsuario, personajeAleatorio }, "personajes.json");
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
}


