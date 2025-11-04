using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Analisis_de_Datos.App
{
    public class Banco
    {
        public string Nombre { get; set; }
        public float[] valoresHistoricos = new float[3];

        // constructor
        public Banco(string nombre)
        {
            Nombre = nombre;
        }

        // Métodos:

        // Hacer que sólo hayan 3 valores historicos por banco
        public void AgregarValorHistorico(int anio, float valor)
        {
            if (anio <= 0 || anio > 3)
            {
                throw new ArgumentOutOfRangeException("El año debe estar entre 1 y 3.");
            }
            valoresHistoricos[anio - 1] = valor;
        }
    }
}
