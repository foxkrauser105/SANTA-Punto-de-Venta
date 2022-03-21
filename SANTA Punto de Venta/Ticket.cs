using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SANTA_Punto_de_Venta
{
    public class Ticket
    {
        private static StringBuilder linea = new StringBuilder();
        

        int maxCaracter = 40, cortar;

        //Metodo para dibujar lineas guion
        public string lineasGuia()
        {
            string lineasGuion = "";
            for(int i = 0; i < maxCaracter; i++)
            {
                lineasGuion += "-";
            }
            return linea.AppendLine(lineasGuion).Append(Environment.NewLine).ToString();
        }

        public string lineasAsterisco()
        {
            string lineasAsterisco = "";
            for (int i = 0; i < maxCaracter; i++)
            {
                lineasAsterisco += "*";
            }
            return linea.AppendLine(lineasAsterisco).ToString();
        }

        public string lineasIgual()
        {
            string lineasIgual = "";
            for (int i = 0; i < maxCaracter; i++)
            {
                lineasIgual += "=";
            }
            return linea.AppendLine(lineasIgual).ToString();
        }

        public void Encabezado()
        {
            linea.Append("ARTICULO            |PRECIO|CANTIDAD|IMPORTE");
        }

        public void textoIzquierda(string texto)
        {
            if(texto.Length > maxCaracter)
            {
                int caracterActual = 0;
                for(int longitudTexto = texto.Length; longitudTexto > maxCaracter; longitudTexto -= maxCaracter)
                {
                    linea.AppendLine(texto.Substring(caracterActual, maxCaracter));
                    caracterActual += maxCaracter;
                }
                linea.AppendLine(texto.Substring(caracterActual, texto.Length - caracterActual));
            }
            else
            {
                linea.AppendLine(texto);
            }
        }

        public void textoDerecho(string texto)
        {
            if (texto.Length > maxCaracter)
            {
                int caracterActual = 0;
                for (int longitudTexto = texto.Length; longitudTexto > maxCaracter; longitudTexto -= maxCaracter)
                {
                    linea.AppendLine(texto.Substring(caracterActual, maxCaracter));
                    caracterActual += maxCaracter;
                }
                string espacios = "";
                for(int i = 0; i < (maxCaracter - texto.Substring(caracterActual, texto.Length - caracterActual).Length); i++)
                {
                    espacios += " ";
                }

                linea.AppendLine(espacios + texto.Substring(caracterActual, texto.Length - caracterActual));
            }
            else
            {
                string espacios = "";
                for (int i = 0; i < (maxCaracter - texto.Length); i++)
                {
                    espacios += " ";
                }
                linea.AppendLine(texto);
            }
        }

        public void textoCentro(string texto)
        {
            if (texto.Length > maxCaracter)
            {
                int caracterActual = 0;
                for (int longitudTexto = texto.Length; longitudTexto > maxCaracter; longitudTexto -= maxCaracter)
                {
                    linea.AppendLine(texto.Substring(caracterActual, maxCaracter));
                    caracterActual += maxCaracter;
                }
                string espacios = "";
                int centrar = (maxCaracter - texto.Substring(caracterActual, texto.Length - caracterActual).Length) / 2;
                for (int i = 0; i < centrar; i++)
                {
                    espacios += " ";
                }

                linea.AppendLine(espacios + texto.Substring(caracterActual, texto.Length - caracterActual));
            }
            else
            {
                string espacios = "";
                int centrar = (maxCaracter - texto.Length) / 2;
                for (int i = 0; i < centrar; i++)
                {
                    espacios += " ";
                }

                linea.AppendLine(espacios + texto);
            }
        }

        public void textoExtremos(string textoIzquierdo, string textoDerecho)
        {
            string textoIzq, textoDer, textoCompleto = "", espacios = "";

            if (textoIzquierdo.Length > 18)
            {
                cortar = textoIzquierdo.Length - 18;
                textoIzq = textoIzquierdo.Remove(18, cortar);
            }
            else
            {
                textoIzq = textoIzquierdo;
            }

            if (textoDerecho.Length > 20)
            {
                cortar = textoDerecho.Length - 20;
                textoDer = textoDerecho.Remove(20, cortar);
            }
            else
            {
                textoDer = textoDerecho;
            }

            int nroEspacios = maxCaracter - (textoIzq.Length + textoDer.Length);

            for (int i = 0; i < nroEspacios; i++)
            {
                espacios += " ";
            }
            textoCompleto += espacios + textoDerecho;
            linea.AppendLine(textoCompleto);
        }

        public void agregarTotales(string texto, decimal total)
        {
            string resumen, valor, textoCompleto, espacios = "";

            if (texto.Length > 25)
            {
                cortar = texto.Length - 25;
                resumen = texto.Remove(25, cortar);
            }
            else
            {
                resumen = texto;
            }

            textoCompleto = resumen;
            valor = total.ToString("#,#.00");

            int nroEspacios = maxCaracter - (resumen.Length + valor.Length);
            for(int i = 0; i < nroEspacios; i++)
            {
                espacios += " ";
            }
            textoCompleto += espacios + valor;
            linea.AppendLine(textoCompleto);
        }

        public void agregarArticulos(string articulo, decimal precio, int cant, decimal importe)
        {
            if (cant.ToString().Length <= 5 && precio.ToString().Length <= 7 && importe.ToString().Length <= 8)
            {
                string elemento = "", espacios = "";
                bool bandera = false;
                int nroEspacios = 0;

                if (articulo.Length > 20)
                {
                    nroEspacios = (5 - cant.ToString().Length);
                    espacios = "";
                    for(int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";
                    }
                    elemento += espacios + cant.ToString();

                    nroEspacios = (7 - precio.ToString().Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";
                    }
                    elemento += espacios + precio.ToString();

                    nroEspacios = (8 - importe.ToString().Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";
                    }
                    elemento += espacios + importe.ToString();

                    int caracterActual = 0;
                    for(int longitudTexto = articulo.Length; longitudTexto > 20; longitudTexto -= 20)
                    {
                        if(bandera == false)
                        {
                            linea.AppendLine(articulo.Substring(caracterActual, 20) + elemento);
                        }
                        else
                        {
                            linea.AppendLine(articulo.Substring(caracterActual, 20));
                        }
                        caracterActual += 20;
                    }

                    linea.AppendLine(articulo.Substring(caracterActual, articulo.Length - caracterActual));
                }
                else
                {
                    for(int i = 0; i < (20 - articulo.Length); i++)
                    {
                        espacios = " ";
                    }
                    elemento = articulo + espacios;

                    nroEspacios = (5 - cant.ToString().Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";
                    }
                    elemento += espacios + cant.ToString();

                    nroEspacios = (7 - precio.ToString().Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";
                    }
                    elemento += espacios + precio.ToString();

                    nroEspacios = (8 - importe.ToString().Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";
                    }
                    elemento += espacios + importe.ToString();

                    linea.AppendLine(elemento);
                }
            }
            else
            {
                linea.AppendLine("Los valores ingresados para esta fila");
                linea.AppendLine("superan las columnas soportadas por éste.");
                throw new Exception("Los valores ingresados para algunas filas del ticket\nsuperan las columnas soportadas por éste");
            }
        }

        public void cortarTicket()
        {
            linea.AppendLine("\x1B" + "m");
            linea.AppendLine("\x1B" + "d" + "\x09");
        }

        public void imprimirTicket(string impresora)
        {
            RawPrinterHelper.SendStringToPrinter(impresora, linea.ToString());         
            linea.Length = 0;
        }

        public string imprimeLinea()
        {
            return linea.ToString();
        }
    }
}
