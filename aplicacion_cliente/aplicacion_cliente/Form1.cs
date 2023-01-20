using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// Se agrega el espacio de nombres de la referencia de servicio conectada
using aplicacion_cliente.Alumnos_WS;
using aplicacion_cliente.AlumnosWS;

namespace aplicacion_cliente
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Instancia de Conexión a Web Service
            // Alumnos_WS.ClassroomWSPortTypeClient cliente = new Alumnos_WS.ClassroomWSPortTypeClient();
            AlumnosWS.ClassroomWS cliente = new AlumnosWS.ClassroomWS();

            // Se usa la instancia para invocar al método BuscarAlumno
            // La respuesta se almacena en la instancia de clase RespuestaBuscarAlumno
            // Alumnos_WS.RespuestaBuscarAlumno res = cliente.BuscarAlumno("pruebas", "12345678a", "201935660");
            AlumnosWS.RespuestaBuscarAlumno res = cliente.BuscarAlumno("pruebas", "12345678a", "201935660");

            textBox1.Text = "Código: " + res.code + "\r\n" +
                "Mensaje: " + res.message + "\r\n" +
                "Datos: " + res.data + "\r\n" +
                "Estatus: " + res.status;
        }
    }
}
