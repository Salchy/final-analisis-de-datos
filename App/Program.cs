using Final_Analisis_de_Datos.App;
using System;

class Program
{

    static void Main(string[] args)
    {
        bool closeApp = false;

        Banco[] bancos = new Banco[3];

        bancos[0] = new Banco("Banco Provincia");
        bancos[1] = new Banco("Banco Nacion");
        bancos[2] = new Banco("Banco Hipotecario");

        while (!closeApp)
        {
            Console.Clear();
            Console.WriteLine("===== MENÚ PRINCIPAL =====");
            Console.WriteLine("1 - Comenzar analisis ($850.000)");
            Console.WriteLine("2 - Comenzar analisis ($ a elegir)");
            Console.WriteLine("");
            Console.WriteLine("0 - Salir");
            Console.WriteLine("==========================");
            Console.Write("Seleccione una opción: ");

            string opcion = Console.ReadLine();
            Console.WriteLine();

            switch (opcion)
            {
                case "1":
                    SolicitarValoresHistoricos(bancos);
                    CalcularPromediosAnuales(bancos);
                    break;
                case "2":
                    SolicitarValoresHistoricos(bancos);
                    CalcularPromediosAnuales(bancos);
                    break;
                case "0":
                    closeApp = true;
                    Console.WriteLine("Saliendo del programa...");
                    break;

                default:
                    Console.WriteLine("Opción no válida. Presione una tecla para continuar...");
                    Console.Beep();
                    Console.ReadKey();
                    break;
            }
        }
    }

    public static void SolicitarValoresHistoricos(Banco[] bancos)
    {
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine($"Ingrese los valores historicos para {bancos[i].Nombre}:");
            for (int anio = 1; anio <= 3; anio++)
            {
                float valor;
                while (true)
                {
                    Console.Write($"Año {anio}: ");
                    string input = Console.ReadLine();
                    if (float.TryParse(input, out valor))
                    {
                        bancos[i].AgregarValorHistorico(anio, valor);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Valor inválido. Por favor, ingrese un número válido.");
                    }
                }
            }
            Console.WriteLine();
        }
    }

    public static void CalcularPromediosAnuales(Banco[] bancos)
    {
        Console.WriteLine("Promedios anuales de las tasas de los bancos:");
        for (int i = 0; i < bancos.Length; i++)
        {
            float suma = 0;
            for (int anio = 1; anio <= 3; anio++)
            {
                suma += bancos[i].valoresHistoricos[anio - 1];
            }
            float promedio = suma / bancos.Length;
            Console.WriteLine($"Banco {bancos[i].Nombre}: Promedio = {promedio}");
        }
        Console.WriteLine("Presione una tecla para continuar...");
        Console.ReadKey();
    }
    public static void MostrarValores(Banco[] bancos)
    {
        Console.WriteLine("Valores históricos ingresados:");
        for (int i = 0; i < bancos.Length; i++)
        {
            Console.WriteLine($"{bancos[i].Nombre}:");
            for (int anio = 1; anio <= 3; anio++)
            {
                Console.WriteLine($"  Año {anio}: {bancos[i].valoresHistoricos[anio - 1]}");
            }
            Console.WriteLine();
        }
        Console.WriteLine("Presione una tecla para continuar...");
        
        Console.ReadLine();
    }
}