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
        private  static HashSet<Char> vocales;
        protected void Page_Load(object sender, EventArgs e)
        {
            vocales = new HashSet<Char> { 'a', 'e', 'i', 'o' , 'u'};
        }
        private static String ProcesaTexto(String texto, int lineas)// procesa el texto
        {
            String exit = "";
            String nextLine = "";
            String posLine = ""; // salida con el ultimo espacio
            int antI = 0; // parametro para recordar donde se encontro el ultimo espacio
            int c = 0;
            
            Boolean espacio = false;
            for (int i = 0; i < texto.Length; i++)
            {
                if (texto[i] == ' ')
                {
                    posLine = nextLine;
                    espacio = true;
                    antI = i;
                }
                nextLine += texto[i];
                c++;
                if (c == 1 && texto[i] == ' ') //para que no se empiece una linea con un espacio en el caso de que la linea anterior acabara con una palabra completa
                {
                    nextLine = "";
                    c--;
                }
                if (c == lineas || i == texto.Length - 1)// si se llega al límite o se acaba el texto
                {
                    if (i < texto.Length - 1)
                    {
                        if (texto[i + 1] == ' ') espacio = false; // si se ha alcanzado el final de una palabra no hace falta volver al espacio anterior
                    }
                    if (espacio == true && i != texto.Length - 1)
                    {
                        nextLine = posLine;
                        i = antI;
                    }
                    if (nextLine.Length > 0)// para evitar escribir lineas con un espacio
                    {
                        exit += "<br />" + nextLine;
                    }
                    c = 0;
                    espacio = false;
                    nextLine = "";
                }
            }
            return exit;
        }


        private static String ProcesaSilabas(int columnas, String nextline, int espacio, String nextChars)
        {
            int end = nextline.Length -1;
            if (vocales.Contains(nextline[end])){
                if (vocales.Contains(nextChars[0]))
                {
                    return nextline;
                }
            }
                
            
            //vocal + vocal -> separa
            //vocal + consonante + consonante si no es rr o ll o ch separa despues de la primera cons
            //vocal + conssonante + consonante || + consonante  pero si la tercera es una r o una l separar rn la primera


            return "";
        }

        protected void ProcButton_Click(object sender, EventArgs e)
        {
             
            String texto = Texto.Text;
            int columnas;
            if (Int32.TryParse(Columnas.Text, out columnas));
            else {
                String message = Columnas.Text + " no es un número";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
            }
            TextoProcesadoLabel.Text = ProcesaTexto(texto, columnas);
        }
    }
}