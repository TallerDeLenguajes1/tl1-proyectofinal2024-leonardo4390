using System;
using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TipoPersonaje
{
    Guerrero,
    Mago,
    Arquero,
    Asesino,
    Nigromante,
    Barbaro,
    Hechicero,
    Bruja
}
public class Personaje
{
    public Datos Datos{get;private set;}
    public Caracteristicas Caracteristicas{get;private set;}


    //constructor para el personaje
    //public Personaje(){}
    
    public Personaje(Datos datos, Caracteristicas caracteristicas)
    {
        Datos = datos;
        Caracteristicas = caracteristicas;
    }
}