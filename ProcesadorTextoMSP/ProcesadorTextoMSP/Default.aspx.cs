using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProcesadorTextoMSP
{
    public partial class Main : System.Web.UI.Page
    {
        private static HashSet<Char> vocales;
        protected void Page_Load(object sender, EventArgs e)
        {
            vocales = new HashSet<Char> { 'a', 'e', 'i', 'o', 'u' };
        }
        private static String ProcesaXSilabas(int columnas, String texto)
        {
            String salida = "<br />";
            int c = -1;
            Boolean final = false;
            Boolean escribo = false;
            int lineStart = 0;
            int p = 0;
            while (c < texto.Length)
            {
                if (columnas == 1)
                {
                    for (int i = 0; i <= texto.Length; i++)
                    {
                        salida += texto[i] + "<br />";
                    }
                }
                else
                {
                    //preparando el c y el comienzo de linea
                    if (c + columnas < texto.Length - 1)
                    {
                        c += columnas;
                        while (texto[lineStart] == ' ')//evitar espacios al comienzo
                        {
                            if (c < texto.Length - 1) c++;
                            if (lineStart > texto.Length - 1) return salida;
                            lineStart++;
                        }
                    }
                    if (c + columnas >= texto.Length-1)//si estamos al final
                    {
                        c = texto.Length - 1;
                        while (texto[lineStart] == ' ')
                        {
                            lineStart++;
                            if (lineStart > texto.Length - 1) return salida;
                        }
                        final = true;
                    }

                    //analizando por silabas
                    if (final || texto[c + 1] == ' ') //si cabe la palabra hasta el final
                    {
                        for (int i = lineStart; i <= c; i++) salida += texto[i];
                        if (!final)
                        {
                            salida += "<br />";
                            lineStart = c + 1;
                        }
                        else break;
                    }
                    else
                    {
                        p = c;
                        // si el ultimo caracter era un espacio
                        if (texto[p] == ' ')
                        {
                            for (int i = lineStart; i < p; i++) salida += texto[i];
                            salida += "<br />";
                            lineStart = p + 1;
                            c = p;
                            continue;
                        }
                        while (p >= lineStart)
                        {
                            p--;
                            // si encontramos un espacio
                            if (texto[p] == ' ')
                            {
                                for (int i = lineStart; i < p; i++) salida += texto[i];
                                salida += "<br />";
                                lineStart = p + 1;
                                c = p;
                                continue;
                            }
                            else
                            {
                                //hemos caido en vocal
                                if (vocales.Contains(texto[p]))
                                {
                                    if (vocales.Contains(texto[p + 1]))
                                    {
                                        escribo = true;
                                        if (texto[p] == 'u' && (texto[p + 1] == 'e' || texto[p + 1] == 'i'))
                                        {
                                            if (p - 1 >= 0)
                                            {
                                                if (texto[p - 1] == 'q') escribo = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (vocales.Contains(texto[p + 2])) escribo = true;
                                        else if ((texto[p + 1] == 'c' && texto[p + 2] == 'h') || texto[p + 2] == 'r' || texto[p + 2] == 'l') escribo = true;
                                    }
                                }
                                //hemos caido en consonante
                                else
                                {
                                    if (!vocales.Contains(texto[p + 1]))
                                    {
                                        if ((texto[p] != 'c' || texto[p + 1] != 'h') && (vocales.Contains(texto[p + 2]) && (texto[p + 1] != 'r' && texto[p + 1] != 'l'))) escribo = true;
                                        else
                                        {
                                            if (p + 3 < texto.Length)
                                            {
                                                if ((texto[p + 1] == 'c' && texto[p + 2] == 'h') || (vocales.Contains(texto[p + 3]) && (texto[p + 2] == 'r' || texto[p + 2] == 'l'))) escribo = true;
                                            }
                                        }
                                    }

                                }

                            }
                            if (escribo)
                            {
                                for (int i = lineStart; i <= p; i++) salida += texto[i];
                                salida += "-" + "<br />";
                                lineStart = p + 1;
                                c = p;
                                escribo = false;
                                continue;
                            }
                            // si solo nos queda una letra y no nos ha cabido una silaba
                            if (p == lineStart && !escribo)
                            {
                                for (int i = lineStart; i < c; i++) salida += texto[i];
                                salida += "-" + "<br />";
                                escribo = false;
                                lineStart = c--;
                            }
                        }
                    }
                }
            }
            return salida;
        }

        protected void ProcButton_Click(object sender, EventArgs e)
        {

            String texto = Texto.Text;
            int columnas;
            if (Int32.TryParse(Columnas.Text, out columnas)) ;
            else
            {
                String message = Columnas.Text + " no es un número";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
            }
            TextoProcesadoLabel.Text = ProcesaXSilabas(columnas, texto);
        }
    }
}