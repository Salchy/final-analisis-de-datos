using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.app
{
    public class Tabla
    {
        public List<Columna> Columnas { get; set; } = new List<Columna>();
        public List<Item> Items { get; set; } = new List<Item>();

        // Métodos públicos:
        public void DrawTable()
        {
            int[] lenghtArray = CalculateColumnWidths();

            for (int i = 0; i < Columnas.Count; i++)
            {
                string columna = Columnas[i].NombreColumna;
                int columnWidth = lenghtArray[i] + 2; // Espacio adicional para padding
                Console.Write("| " + columna.PadRight(columnWidth - 1));
            }
        }

        public int CreateColumn(string nombreColumna)
        {
            Columnas.Add(new Columna { IdColumna = Columnas.Count, NombreColumna = nombreColumna });
            return Columnas.Count;
        }

        // Métodos privados:

        // Método para calcular el ancho máximo (Caracteres) de cada columna
        private int[] CalculateColumnWidths()
        {
            int[] columnWidths = new int[Columnas.Count];
            // Calcular el ancho máximo del título de cada columna
            for (int i = 0; i < Columnas.Count; i++)
            {
                columnWidths[i] = Columnas[i].NombreColumna.Length;
            }
            // Calcular el ancho máximo de los datos en cada columna
            for (int i = 0; i < Items.Count; i++)
            {
                int charsLength = Items[i].Valor.Length;
                if (charsLength > columnWidths[Items[i].idColumna])
                {
                    columnWidths[i] = charsLength;
                }
            }
            return columnWidths;
        }
    }
}
