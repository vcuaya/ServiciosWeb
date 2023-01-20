using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using soapclientAlumno.servicio_alumno;
using System.Web.Services.Protocols;

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
                ClassroomWSPortTypeClient cliente = new ClassroomWSPortTypeClient();
                
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
                        evaluar = cliente.EvaluarAlumno(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);
                        response = "Código: " + evaluar.code + "\r\n" +
                            "Mensaje: " + evaluar.message + "\r\n" +
                            "Datos: " + evaluar.data + "\r\n" +
                            "Promedio: " + evaluar.promedio + "\r\n" +
                            "Estatus: " + evaluar.status;
                        break;
                }

                textBox5.Text = response;

            }catch(SoapException ex)
            {
                // Excepción SOAP-Fault
                textBox5.Text = "Código: " + ex.Code.ToString() + "\r\n" +
                    "Message: " + ex.Message.ToString();
            }
        }
    }
}
