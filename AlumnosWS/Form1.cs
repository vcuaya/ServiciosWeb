using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.Services.Protocols;
using ClassroomWS_cliente;
using System.Net;

namespace soapclientAlumno
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int index = comboBox1.SelectedIndex;
                string response = "";

                // Instancia de Conexión a Web Service
                ClassroomWS cliente = new ClassroomWS();

                // La respuesta se almacena en la instancia de clase RespuestaBuscarAlumno

                switch (index)
                {
                    case 0:
                        // Se usa la instancia para invocar al método Bienvenida
                        RespuestaBienvenida bienvenida = new RespuestaBienvenida();
                        bienvenida = cliente.Bienvenida(textBox1.Text, textBox2.Text, textBox3.Text);
                        response = "Código: " + bienvenida.code + "\r\n" +
                            "Mensaje: " + bienvenida.message + "\r\n" +
                            "Estatus: " + bienvenida.status;
                        break;
                    case 1:
                        // Se usa la instancia para invocar al método BuscarAlumno
                        RespuestaBuscarAlumno buscar = new RespuestaBuscarAlumno();
                        buscar = cliente.BuscarAlumno(textBox1.Text, textBox2.Text, textBox3.Text);
                        response = "Código: " + buscar.code + "\r\n" +
                            "Mensaje: " + buscar.message + "\r\n" +
                            "Datos: " + buscar.data + "\r\n" +
                            "Estatus: " + buscar.status;
                        break;
                    case 2:
                        // Se usa la instancia para invocar al método EvaluarAlumno
                        RespuestaEvaluarAlumno evaluar = new RespuestaEvaluarAlumno();

                        int number;
                        bool value;

                        value = int.TryParse(textBox4.Text, out number);

                        if (value && number > 0)
                        {
                            evaluar = cliente.EvaluarAlumno(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);
                            response = "Código: " + evaluar.code + "\r\n" +
                                "Mensaje: " + evaluar.message + "\r\n" +
                                "Datos: " + evaluar.data + "\r\n" +
                                "Promedio: " + evaluar.promedio + "\r\n" +
                                "Estatus: " + evaluar.status;
                        }
                        else
                            response = "Message: El valor del semestre no puede ser menor a 0";
                        break;
                }

                textBox5.Text = response;

            }
            catch (WebException ex)
            {
                textBox5.Text = "Response: " + ex.Response.ToString() + "\r\n" + "Message: " + ex.Message.ToString();
            }
            catch (SoapException ex)
            {
                // Excepción SOAP-Fault
                textBox5.Text = "Code: " + ex.Code.ToString() + "\r\n" + "Message: " + ex.Message.ToString();
            }
            catch (Exception ex)
            {
                textBox5.Text = "Source: " + ex.Source.ToString() + "\r\n" + "Message: " + ex.Message.ToString();
            }
        }
    }
}
