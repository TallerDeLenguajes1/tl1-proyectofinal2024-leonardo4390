using System;

class FabricaDePersonajes
{
    public static DateTime GenerarFechaNacimiento(Random random)
    {
        int dia = random.Next(1, 31); // D entre 1-30  
        int mes = random.Next(1, 13); // M entre 1-12
        int anio = random.Next(1, 1500); // A entre 1-1500

        //fecha aleatoria
        DateTime fechaNacimiento = new DateTime(anio, mes, dia);

        return fechaNacimiento;
    }

    public Personaje GenerarPersonajeAleatorio(TipoPersonaje tipo)
    {
        Random random = new Random();
        string nombre = "";
        string apodo = "";
        string beneficio = "";

        switch (tipo)
        {
            
            case TipoPersonaje.Arquero:
                nombre = "Hydron";
                apodo = "El Preciso Humedo";
                beneficio = "Lluvia";
                break;
            case TipoPersonaje.Mago:
                nombre = "Pyrus";
                apodo = "El Ardiente Sabio";
                beneficio = "Soleado";//despejado
                break;
            case TipoPersonaje.Guerrero:
                nombre = "Durandal";
                apodo = "El Senior del Viento";
                beneficio = "Ventoso";
                break;
            case TipoPersonaje.Nigromante:
                nombre = "Nekros";
                apodo = "El Amo de la oscuridad";
                beneficio = "Noche";//despejado
                break;
            case TipoPersonaje.Barbaro:
                nombre = "Grom";
                apodo = "El Devorador del Sol";
                beneficio = "Soleado";//despejado 
                break;
            case TipoPersonaje.Asesino:
                nombre = "Lurker";
                apodo = "El Fantasma de la Noche";
                beneficio = "Noche";
                break;
            case TipoPersonaje.Bruja:
                nombre = "Morgana";
                apodo = "La Reina del Rayo";
                beneficio = "Tormenta";
                break;
            case TipoPersonaje.Hechicero:
                nombre = "Mistralia";
                apodo = "Susurro velado";
                beneficio = "Niebla";
                break;
        }

        DateTime fechaNacimiento = GenerarFechaNacimiento(random);
        //formatendo fecha deseada
        string fechaFormateda = $"{fechaNacimiento.Day} de {fechaNacimiento.ToString("MMMM")} de {fechaNacimiento.Year}";

        Datos datos = new Datos
        {
            Tipo = tipo,
            Nombre = nombre,
            Apodo = apodo,
            FechaDeNacimiento = fechaFormateda,
            Edad = random.Next(0, 301),
            Beneficio = beneficio
        };

        Caracteristicas caracteristicas = new Caracteristicas();
        {
            caracteristicas.Velocidad = random.Next(1, 11);
            caracteristicas.Destreza = random.Next(1, 6);
            caracteristicas.Fuerza = random.Next(1, 11);
            caracteristicas.Nivel = random.Next(1, 11);
            caracteristicas.Armadura = random.Next(1, 11);
            caracteristicas.Salud = 100;
        }
        
          

        //creando una instancia del personajes con sus datos y caracteristicas
        return new Personaje(datos, caracteristicas);
    }
}