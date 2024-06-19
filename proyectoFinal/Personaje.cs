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
    public Datos Datos{get;set;}
    public Caracteristicas Caracteristicas{get;set;}


    //constructor para el personaje
    
    public Personaje(Datos datos, Caracteristicas caracteristicas)
    {
        Datos = datos;
        Caracteristicas = caracteristicas;
    }
}