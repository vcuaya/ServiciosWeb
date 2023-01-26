using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// Librería HTTP de .NET
using System.Net.Http;
// Librería JSON
using Newtonsoft.Json.Linq;


namespace restclientAlumno
{
    public partial class Form1 : Form
    {
        private HttpClient clienteHTTP;

        public Form1()
        {
            InitializeComponent();
            clienteHTTP = new HttpClient();
            clienteHTTP.BaseAddress = new Uri("http://54.203.246.162/neo/classroom/sw/restapi/");
        }

        private RespuestaBienvenida jsonToRespuestaBienvenida(string json)
        {
            RespuestaBienvenida res = new RespuestaBienvenida();

            JObject jObject = JObject.Parse(json);
            JToken jUser = jObject;
            res.Code = (string)jUser["code"];
            res.Message = (string)jUser["message"];
            res.Status = (string)jUser["status"];

            return res;
        }

        private RespuestaBuscarAlumno jsonToRespuestaBuscarAlumno(string json)
        {
            RespuestaBuscarAlumno res = new RespuestaBuscarAlumno();

            JObject jObject = JObject.Parse(json);
            JToken jUser = jObject;
            res.Code = (string)jUser["code"];
            res.Message = (string)jUser["message"];
            res.Data = (string)jUser["data"];
            res.Status = (string)jUser["status"];

            return res;
        }

        private RespuestaEvaluarAlumno jsonToRespuestaEvaluarAlumno(string json)
        {
            RespuestaEvaluarAlumno res = new RespuestaEvaluarAlumno();

            JObject jObject = JObject.Parse(json);
            JToken jUser = jObject;
            res.Code = (string)jUser["code"];
            res.Message = (string)jUser["message"];
            res.Data = (string)jUser["data"].ToString();
            res.Promedio = (string)jUser["promedio"];
            res.Status = (string)jUser["status"];

            return res;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int index = comboBox1.SelectedIndex;
            RespuestaBienvenida bienvenida = null;
            RespuestaBuscarAlumno buscarAlumno = null;
            RespuestaEvaluarAlumno evaluarAlumno = null;

            try
            {
                switch (index)
                {
                    case 0:
                        // Armado y envío del request usando POST
                        var postTaskBienvenida = clienteHTTP.PostAsync("Bienvenida/", new MultipartFormDataContent {
                            {new StringContent(textBox1.Text), "\"user\""},
                            {new StringContent(textBox2.Text), "\"pass\""},
                            {new StringContent(textBox3.Text), "\"matricula\""},
                            {new StringContent(textBox4.Text), "\"semestre\""}
                        });

                        postTaskBienvenida.Wait();

                        var resultBienvenida = postTaskBienvenida.Result;

                        // Verificación del estatus del response http
                        if (resultBienvenida.IsSuccessStatusCode)
                        {
                            var readTask = resultBienvenida.Content.ReadAsStringAsync();

                            readTask.Wait();

                            var json = readTask.Result;

                            // Deserialización de la respuesta JSON
                            bienvenida = jsonToRespuestaBienvenida(json);

                            // Se muestran los atributos de la respuesta
                            textBox5.Text = "Código: " + bienvenida.Code + "\r\n" +
                            "Mensaje: " + bienvenida.Message + "\r\n" +
                            "Estatus: " + bienvenida.Status + "\r\n";
                        }
                        else
                        {
                            // Se muestra información del response con error
                            textBox5.Text = resultBienvenida.StatusCode + " - " + resultBienvenida.RequestMessage;
                        }
                        break;
                    case 1:
                        // Armado y envío del request usando POST
                        var postTaskBuscarAlumno = clienteHTTP.PostAsync("BuscarAlumno/", new MultipartFormDataContent {
                            {new StringContent(textBox1.Text), "\"user\""},
                            {new StringContent(textBox2.Text), "\"pass\""},
                            {new StringContent(textBox3.Text), "\"matricula\""},
                            {new StringContent(textBox4.Text), "\"semestre\""}
                        });

                        postTaskBuscarAlumno.Wait();

                        var resultBuscarAlumno = postTaskBuscarAlumno.Result;

                        // Verificación del estatus del response http
                        if (resultBuscarAlumno.IsSuccessStatusCode)
                        {
                            var readTask = resultBuscarAlumno.Content.ReadAsStringAsync();

                            readTask.Wait();

                            var json = readTask.Result;

                            // Deserialización de la respuesta JSON
                            buscarAlumno = jsonToRespuestaBuscarAlumno(json);



                            // Se muestran los atributos de la respuesta
                            textBox5.Text = "Código: " + buscarAlumno.Code + "\r\n" +
                            "Mensaje: " + buscarAlumno.Message + "\r\n" +
                            "Datos: " + buscarAlumno.Data + "\r\n" +
                            "Estatus: " + buscarAlumno.Status + "\r\n";
                        }
                        else
                        {
                            // Se muestra información del response con error
                            textBox5.Text = resultBuscarAlumno.StatusCode + " - " + resultBuscarAlumno.RequestMessage;
                        }

                        break;
                    case 2:
                        // Armado y envío del request usando POST
                        var postTaskEvaluarAlumno = clienteHTTP.PostAsync("EvaluarAlumno/", new MultipartFormDataContent {
                            {new StringContent(textBox1.Text), "\"user\""},
                            {new StringContent(textBox2.Text), "\"pass\""},
                            {new StringContent(textBox3.Text), "\"matricula\""},
                            {new StringContent(textBox4.Text), "\"semestre\""}
                        });

                        postTaskEvaluarAlumno.Wait();

                        var resultEvaluarAlumno = postTaskEvaluarAlumno.Result;

                        // Verificación del estatus del response http
                        if (resultEvaluarAlumno.IsSuccessStatusCode)
                        {
                            var readTask = resultEvaluarAlumno.Content.ReadAsStringAsync();

                            readTask.Wait();

                            var json = readTask.Result;

                            // Deserialización de la respuesta JSON
                            evaluarAlumno = jsonToRespuestaEvaluarAlumno(json);



                            // Se muestran los atributos de la respuesta
                            textBox5.Text = "Código: " + evaluarAlumno.Code + "\r\n" +
                            "Mensaje: " + evaluarAlumno.Message + "\r\n" +
                            "Datos: " + evaluarAlumno.Data + "\r\n" +
                            "Estatus: " + evaluarAlumno.Status + "\r\n";
                        }
                        else
                        {
                            // Se muestra información del response con error
                            textBox5.Text = resultEvaluarAlumno.StatusCode + " - " + resultEvaluarAlumno.RequestMessage;
                        }
                        break;
                }
            }
            catch (HttpRequestException error)
            {
                // Se muestra el mensaje de error en caso de alguna excepción
                textBox5.Text = error.Message;
            }
        }
    }

    internal class RespuestaBienvenida
    {
        private string code;
        private string message;
        private string status;

        public string Code { get => code; set => code = value; }
        public string Message { get => message; set => message = value; }
        public string Status { get => status; set => status = value; }
    }

    internal class RespuestaBuscarAlumno
    {
        private string code;
        private string message;
        private string data;
        private string status;

        public string Code { get => code; set => code = value; }
        public string Message { get => message; set => message = value; }
        public string Data { get => data; set => data = value; }
        public string Status { get => status; set => status = value; }
    }

    internal class RespuestaEvaluarAlumno
    {
        private string code;
        private string message;
        private string data;
        private string promedio;
        private string status;

        public string Code { get => code; set => code = value; }
        public string Message { get => message; set => message = value; }
        public string Data { get => data; set => data = value; }
        public string Promedio { get => promedio; set => promedio = value; }
        public string Status { get => status; set => status = value; }
    }
}
