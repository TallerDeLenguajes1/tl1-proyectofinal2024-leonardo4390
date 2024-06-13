using System;

class Personaje
{
    public Datos Datos{get;set;}
    public Caracteristicas Caracteristicas{get;set;}


    //constructor para el personaje
    public Personaje (Datos datos, Caracteristicas caracteristicas)
    {
        Datos = datos;
        Caracteristicas = caracteristicas;
    }
}