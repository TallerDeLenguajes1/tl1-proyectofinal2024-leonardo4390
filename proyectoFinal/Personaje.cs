using System;
using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
enum TipoPersonaje
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
class Personaje
{
    public TipoPersonaje Tipo { get; set; }
    public Datos Datos{get;set;}
    public Caracteristicas Caracteristicas{get;set;}


    //constructor para el personaje
    
    public Personaje(TipoPersonaje tipo, Datos datos, Caracteristicas caracteristicas)
    {
        Tipo = tipo;
        Datos = datos;
        Caracteristicas = caracteristicas;
    }
}