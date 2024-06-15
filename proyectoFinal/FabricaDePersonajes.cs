using System;

class FabricaDePersonajes
{


    //generando personajes aleatorios
    public Personaje GenerarPersonajeAleatorio(TipoPersonaje tipo)
    {
        Random random = new Random();

        //datos del personaje
        Datos datos = new Datos
        {
            Nombre = "Personaje " + tipo.ToString(),
            Apodo = "Apodo " + tipo.ToString(),
            FechaDeNacimiento = random.Next(1900, 2022),
            Edad = random.Next(0, 300)
        };

        //caracteristicas del personaje
        Caracteristicas caracteristicas = new Caracteristicas();
        if (tipo == TipoPersonaje.Arquero || tipo == TipoPersonaje.Guerrero || tipo == TipoPersonaje.Mago)
        {
            caracteristicas.Velocidad = random.Next(1, 11);
            caracteristicas.Destreza = random.Next(1, 11);
            caracteristicas.Fuerza = random.Next(1, 11);
            caracteristicas.Nivel = random.Next(1, 11);
            caracteristicas.Armadura = random.Next(1, 11);
            caracteristicas.Salud = 100;
        }
        /*switch (tipo)
        {
            case TipoPersonaje.Guerrero:
                caracteristicas.Velocidad = random.Next(1, 11);
                caracteristicas.Destreza = random.Next(1, 11);
                caracteristicas.Fuerza = random.Next(1, 11);
                caracteristicas.Nivel = random.Next(1, 11);
                caracteristicas.Armadura = random.Next(1, 11);
                caracteristicas.Salud = 100;
                break;
            case TipoPersonaje.Mago:
                caracteristicas.Velocidad = random.Next(1, 11);
                caracteristicas.Destreza = random.Next(1, 11);
                caracteristicas.Fuerza = random.Next(1, 11);
                caracteristicas.Nivel = random.Next(1, 11);
                caracteristicas.Armadura = random.Next(1, 11);
                caracteristicas.Salud = 100;
                break;
            case TipoPersonaje.Arquero:
                caracteristicas.Velocidad = random.Next(1, 11);
                caracteristicas.Destreza = random.Next(1, 11);
                caracteristicas.Fuerza = random.Next(1, 11);
                caracteristicas.Nivel = random.Next(1, 11);
                caracteristicas.Armadura = random.Next(1, 11);
                caracteristicas.Salud = 100;
                break;
        }
        */

        // creando una instancia del personajes con sus datos
        Personaje personaje = new Personaje(datos, caracteristicas);
        return personaje;
    }
}