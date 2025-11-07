using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.app
{
    public class Tabla
    {
        public List<string> Columnas { get; set; } = new List<string>();
        public List<List<string>> Rows { get; set; } = new List<List<string>>();

        // Métodos públicos:
        public void DrawTable()
        {
            int[] lenghtArray = CalculateColumnWidths();
            int totalWidth = lenghtArray.Sum() + (3 * Columnas.Count) + 1; // Ancho total de la tabla

            // Imprimir las columnas (Títulos)
            for (int i = 0; i < Columnas.Count; i++)
            {
                string columna = Columnas[i];
                int columnWidth = lenghtArray[i] + 2; // Espacio adicional para padding
                Console.Write("| " + columna.PadRight(columnWidth - 1));
            }
            Console.Write("|");

            Console.Write("\n");
            for (int i = 0; i < totalWidth; i++)
            {
                Console.Write("=");
            }

            // Imprimir los items (Valores)
            for (int i = 0; i < Rows.Count; i++)
            {
                Console.CursorTop += 1;  // Mover a la siguiente línea
                Console.CursorLeft = 0;  // Reiniciar la posición del cursor al inicio de la línea

                for (int j = 0; j < Rows[i].Count; j++)
                {
                    string item = Rows[i][j];
                    int columnWidth = lenghtArray[j] + 2; // Espacio adicional para padding
                    Console.Write("| " + item.PadRight(columnWidth - 1));
                }
                Console.Write("|");
            }
        }

        public int CreateColumn(string nombreColumna)
        {
            Columnas.Add(nombreColumna);
            return Columnas.Count - 1;
        }

        public int addRow()
        {
            Rows.Add(new List<string>());
            return Rows.Count - 1;
        }

        public void addValueToRow(int idRow, string value)
        {
            Rows[idRow].Add(value);
        }

        public void modifyRowValue(int row, int column, string value)
        {
            if (row >= Rows.Count || column >= Columnas.Count)
            {
                throw new IndexOutOfRangeException("Índice de fila o columna fuera de rango.");
            }
            Rows[row][column] = value;
        }

        // Métodos privados:

        // Método para calcular el ancho máximo (Caracteres) de cada columna
        private int[] CalculateColumnWidths()
        {
            int[] columnWidths = new int[Columnas.Count];
            // Calcular el ancho máximo del título de cada columna
            for (int i = 0; i < Columnas.Count; i++)
            {
                columnWidths[i] = Columnas[i].Length;
            }
            // Calcular el ancho máximo de los datos en cada columna
            for (int i = 0; i < Rows.Count; i++)
            {
                for (int j = 0; j < Rows[i].Count; j++)
                {
                    int charsLength = Rows[i][j].Length;
                    if (charsLength > columnWidths[j])
                    {
                        columnWidths[j] = charsLength;
                    }
                }
            }
            return columnWidths;
        }
    }
}
