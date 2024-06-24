using System;
using System.Text.Json;
using System.IO;

class HistorialJson
{
    public void GuardarGanador(Personaje ganador, string nombreArchivo)
    {
        List<Ganador> historialGanador = LeerGanador(nombreArchivo);
        var ganadorExistente = historialGanador.Find(g =>g.Datos.Tipo == ganador.Datos.Tipo);
        
        if (ganadorExistente != null)
        {
            ganadorExistente.Victorias ++;       
        }
        else{
            var nuevoGanador = new Ganador(ganador.Datos);
            historialGanador.Add(nuevoGanador);
        }
        
        var opcionSerializacion = new JsonSerializerOptions
        {
            WriteIndented = true,
        };
        string json = JsonSerializer.Serialize(historialGanador,opcionSerializacion);
        File.WriteAllText(nombreArchivo,json);
    }

    public List<Ganador> LeerGanador(string nombreArchivo)
    {
        if (File.Exists(nombreArchivo))
        {
            string json = File.ReadAllText(nombreArchivo);
            return JsonSerializer.Deserialize<List<Ganador>>(json);
        }
        else{
            Console.WriteLine("No existe el archivo");
            return new List<Ganador>();
        }
    }

    public bool Existe(string nombreArchivo)
    {
        return File.Exists(nombreArchivo) && new FileInfo(nombreArchivo).Length > 0;
    }
    //obtener Victorias
    public Datos ObtenerPersonajeMasVictorioso(string nombreArchivo)
    {
        List<Ganador> historial = LeerGanador(nombreArchivo);
        bool perosonajeMasVictorioso = Existe(nombreArchivo);

        if (perosonajeMasVictorioso)
        {
            //ordenar historial por victorias forma descendente
            historial.Sort((x, y) => y.Victorias.CompareTo(x.Victorias));

            // elprimer elemento del historial tiene más victorias
            return historial[0].Datos;
            
        }else{
            Console.WriteLine("No hay registros de victorias.");
            return null;
        }

        
    }

    public int ObtenerNumerosVictorias(string personaje, string nombreArchivo)
    {
        List<Ganador> historial = LeerGanador(nombreArchivo);
        var batalla = historial.Find(b => b.Datos.Nombre == personaje);
        if (batalla != null)
        {
            return batalla.Victorias;
        }else{
            Console.WriteLine($"El personaje {personaje} No tiene victorias registradas");
            return 0;
        }
    }
}

class Ganador
{
    public Datos Datos{get;set;}
    public int Victorias{get;set;}

    //creando constructor
    public Ganador(){}

    public Ganador(Datos datos)
    {
        Datos = datos;
        Victorias = 1;//inicio en 1
    }
}