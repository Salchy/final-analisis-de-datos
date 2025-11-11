//using App.app;
using App.app;
using Final_Analisis_de_Datos.App;
using System;
using System.Net;
using System.Runtime.CompilerServices;

class Program
{

    static void Main(string[] args)
    {
        bool closeApp = false;

        float capital;

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
                    capital = 850000f;
                    SolicitarValoresHistoricos(bancos);
                    MostrarPromedios(CalcularPromediosAnuales(bancos));
                    RealizarAnalisis(bancos, capital);
                    break;
                case "2":
                    SolicitarCapital(out capital);
                    SolicitarValoresHistoricos(bancos);
                    MostrarPromedios(CalcularPromediosAnuales(bancos));
                    RealizarAnalisis(bancos, capital);
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
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }
    }
    private static void SolicitarValoresHistoricos(Banco[] bancos)
    {
        // Obtener el año actual
        int currentYear = DateTime.Now.Year;
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine($"Ingrese los valores historicos para {bancos[i].Nombre}:");
            for (int anio = 1; anio <= 3; anio++)
            {
                float valor;

                while (true)
                {
                    Console.Write($"Año {currentYear - (3 - anio)}: ");
                    string input = Console.ReadLine();
                    if (float.TryParse(input, out valor))
                    {
                        // validar que el flotante sea con coma y no con punto, para evitar bug
                        if (input.Contains(","))
                        {
                            Console.WriteLine("Por favor, utilice un punto (.) para los decimales en lugar de la coma (,)");
                            continue;
                        }
                        // Validar que sea positivo
                        if (valor < 0)
                        {
                            Console.WriteLine("Por favor, ingrese un valor positivo.");
                            continue;
                        }
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

    private static float[] CalcularPromediosAnuales(Banco[] bancos)
    {
        float[] promedios = new float[3];
        for (int i = 0; i < bancos.Length; i++)
        {
            float suma = 0;
            for (int anio = 0; anio < 3; anio++)
            {
                suma += bancos[i].valoresHistoricos[anio];
            }
            promedios[i] = (suma / bancos.Length);
        }
        return promedios;
    }

    private static void MostrarPromedios(float[] promedios)
    {
        Console.WriteLine("Promedios anuales de las tasas de los bancos:");
        for (int i = 0; i < promedios.Length; i++)
        {
            Console.WriteLine($"Banco {i + 1}: Promedio = {promedios[i].ToString("0.00")}");
        }
    }

    private static void SolicitarCapital(out float capital)
    {
        float value;

        while (true)
        {
            Console.WriteLine("Ingrese el capital a invertir:");
            string input = Console.ReadLine();
            if (float.TryParse(input, out value))
            {
                // validar que el flotante sea con coma y no con punto, para evitar bug   
                if (input.Contains(","))
                {
                    Console.WriteLine("Por favor, utilice un punto (.) para los decimales en lugar de la coma (,)");
                    continue;
                }
                // Validar numero positivo
                if (value < 0)
                {
                    Console.WriteLine("Por favor, ingrese un valor positivo.");
                    continue;
                }
                capital = value;
                break;
            }
            else
            {
                Console.WriteLine("Valor inválido. Por favor, ingrese un número válido.");
            }
        }
    }

    private static void RealizarAnalisis(Banco[] bancos, float capital)
    {
        Console.Clear();

        float[] promedios = CalcularPromediosAnuales(bancos);
        float[] rendimientosAnual = punto3A(promedios, capital);
        float[] rendimientosTrimestral = punto3B(promedios, capital);
        float[] rendimientosMensual = punto3C(promedios, capital);

        drawAnalisis(bancos, capital, promedios, rendimientosAnual, rendimientosTrimestral, rendimientosMensual);
        drawEvaluacion(bancos, capital, promedios, rendimientosAnual, rendimientosTrimestral, rendimientosMensual);
    }

    private static void drawAnalisis(Banco[] bancos, float capital, float[] promedios, float[] rendimientosAnual, float[] rendimientosTrimestral, float[] rendimientosMensual)
    {
        Tabla table = new Tabla();

        table.CreateColumn("Banco");
        table.CreateColumn("TNA Promedio");
        table.CreateColumn("Anual");
        table.CreateColumn("Trimestral");
        table.CreateColumn("Mensual");

        for (int i = 0; i < 3; i++)
        {
            int newRow = table.addRow();
            table.addValueToRow(newRow, bancos[i].Nombre);
            table.addValueToRow(newRow, promedios[i].ToString("0.00") + " %");
            table.addValueToRow(newRow, "$ " + rendimientosAnual[i].ToString("0.00"));
            table.addValueToRow(newRow, "$ " + rendimientosTrimestral[i].ToString("0.00"));
            table.addValueToRow(newRow, "$ " + rendimientosMensual[i].ToString("0.00"));
        }
        table.DrawTable();

        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("Presione una tecla para continuar...");
        Console.ReadKey();
    }

    private static void drawEvaluacion(Banco[] bancos, float capital, float[] promedios, float[] rendimientosAnual, float[] rendimientosTrimestral, float[] rendimientosMensual)
    {
        Tabla table = new Tabla();

        float[][] Rendimientos = new float[3][];

        Rendimientos[0] = rendimientosAnual;
        Rendimientos[1] = rendimientosTrimestral;
        Rendimientos[2] = rendimientosMensual;

        int mejorBanco = 0;
        string mejorModalidad = "";
        float mejorGanancia = 0;

        for (int i = 0; i < bancos.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (Rendimientos[i][j] > mejorGanancia)
                {
                    mejorGanancia = Rendimientos[i][j];
                    mejorBanco = i;
                    switch (j)
                    {
                        case 0:
                            mejorModalidad = "Anual";
                            break;
                        case 1:
                            mejorModalidad = "Trimestral";
                            break;
                        case 2:
                            mejorModalidad = "Mensual";
                            break;
                    }
                }
            }
        }

        Console.WriteLine();
        Console.WriteLine("----- Evaluación de Inversiones -----");
        Console.WriteLine("En el plan de ahorro de un año, la mejor opción es la siguiente:");

        table.CreateColumn("Banco");
        table.CreateColumn("TNA Promedio");
        table.CreateColumn("Modalidad");
        table.CreateColumn("Ganancia");

        table.addRow();
        table.addValueToRow(0, bancos[mejorBanco].Nombre);
        table.addValueToRow(0, promedios[mejorBanco].ToString("0.00") + " %");
        table.addValueToRow(0, mejorModalidad);
        table.addValueToRow(0, "$ " + mejorGanancia.ToString("0.00"));

        table.DrawTable();

        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("Presione una tecla para continuar...");
        Console.ReadKey();
    }

    // Analiza el anual
    private static float[] punto3A(float[] promedios, float capital)
    {
        float[] ganancias = new float[3];
        for (int i = 0; i < promedios.Length; i++)
        {
            ganancias[i] = (promedios[i] / 100 * capital);
        }

        return ganancias;
    }

    // Analiza el trimestral
    private static float[] punto3B(float[] promedios, float capital)
    {
        float[] ganancias = new float[3];
        for (int i = 0; i < 3; i++)
        {
            ganancias[i] = capitalizar(capital, promedios[i], 4);
        }

        return ganancias;
    }

    // Analiza el mensual
    private static float[] punto3C(float[] promedios, float capital)
    {
        float[] ganancias = new float[3];
        for (int i = 0; i < 3; i++)
        {
            ganancias[i] = capitalizar(capital, promedios[i], 12);
        }

        return ganancias;
    }

    private static float capitalizar(float capital, float TNA, int periodos)
    {
        float capitalFinal;

        float tasa = (TNA / 100) / periodos;
        capitalFinal = capital * (float)Math.Pow(1 + tasa, periodos);

        return capitalFinal - capital;
    }
}