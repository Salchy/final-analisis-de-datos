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

            // Imprimir las columnas (Títulos)
            for (int i = 0; i < Columnas.Count; i++)
            {
                string columna = Columnas[i].NombreColumna;
                int columnWidth = lenghtArray[i] + 2; // Espacio adicional para padding
                Console.Write("| " + columna.PadRight(columnWidth - 1));
            }

            // Imprimir los items (Valores)
            for (int i = 0; i < Items.Count; i++)
            {
                Console.CursorTop += 1;  // Mover a la siguiente línea
                Console.CursorLeft = 0; // Reiniciar la posición del cursor al inicio de la línea
                // TO DO: No funciona, debería ser una linea para un item (o row, como datagridlist de mta:sa) entero, con todas sus columnas ()
                string item = Items[i].Valor;
                int columnWidth = lenghtArray[Items[i].idColumna] + 2; // Espacio adicional para padding
                Console.Write("| " + item.PadRight(columnWidth - 1));
            }
        }

        public int CreateColumn(string nombreColumna)
        {
            Columnas.Add(new Columna { IdColumna = Columnas.Count, NombreColumna = nombreColumna });
            return Columnas.Count;
        }

        public void addItem(int idColumn, string value)
        {
            Items.Add(new Item { idColumna = idColumn, Valor = value });
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
