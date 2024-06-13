using System;

class Principal
{
    enum TipoPersonaje
    {
        Guerrero,
        Mago,
        Arquero
    }

    public void Menu()
    {
        TipoPersonaje tipoElegido;

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
    }
}
