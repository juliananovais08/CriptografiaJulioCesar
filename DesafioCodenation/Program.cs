using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace DesafioCodenation
{
    class Program
    {

        static void Main(string[] args)
        {                   
          
            EnviaRequisicaoGET();
            //EnviaRequisicaoPOST();
        }

            public static void EnviaRequisicaoGET()
            {
                var requisicaoWeb = WebRequest.CreateHttp("https://api.codenation.dev/v1/challenge/dev-ps/generate-data?token=aea570071bb5c870c7317ceb82282d308b21f2f9");
                requisicaoWeb.Method = "GET";

                using (var resposta = requisicaoWeb.GetResponse())
                {

                    var streamDados = resposta.GetResponseStream();
                    StreamReader reader = new StreamReader(streamDados);
                    object objResponse = reader.ReadToEnd();
                    var resp = JsonConvert.DeserializeObject<Resp>(objResponse.ToString());
                    var mensagem = Criptografia.Decifrar(resp.cifrado, resp.numero_casas);
                    var resumoMensagem = Criptografia.CalculateSHA1(mensagem, Encoding.ASCII);
                    resp.decifrado = mensagem;
                    resp.resumo_criptografico = resumoMensagem;
                    var jsonSerializado = JsonConvert.SerializeObject(resp);
                    gravarArquivo(jsonSerializado);                    
                    streamDados.Close();
                    resposta.Close();
                }

                //Console.ReadLine();
            }

            public static void gravarArquivo(string conteudoJson)
            {
                string path = @"C:\ESTUDOS\answer.json";
                // This text is added only once to the file.

                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(conteudoJson);
                }
            }

        public static void EnviaRequisicaoPOST()
        {
            try { 
            string requestURL = "https://api.codenation.dev/v1/challenge/dev-ps/submit-solution?token=aea570071bb5c870c7317ceb82282d308b21f2f9";
                FileStream fs = new FileStream("C:\\ESTUDOS\\answer.json", FileMode.Open, FileAccess.Read);
                byte[] data = new byte[fs.Length];
                fs.Read(data,0 , data.Length);
                fs.Close();
                WebClient wc = new WebClient();
            //var bytes = File.ReadAllBytes("C:\\ESTUDOS\answer.json");
            //byte[] bytes = File.ReadAllBytes(fileName);
            //byte[] bytes = wc.DownloadData(fileName); // You need to do this download if your file is on any other server otherwise you can convert that file directly to bytes  
            Dictionary<string, object> postParameters = new Dictionary<string, object>();
            postParameters.Add("answer", new FormUpload.FileParameter(data,"answer.json"));
            HttpWebResponse webResponse = FormUpload.MultipartFormPost(requestURL, postParameters);
            // Process response  
            StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
            var returnResponseText = responseReader.ReadToEnd();
            webResponse.Close();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"The file was not found: '{e}'");
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine($"The directory was not found: '{e}'");
            }
            catch (IOException e)
            {
                Console.WriteLine($"The file could not be opened: '{e}'");
            }

        }

        //public static void EnviaRequisicaoPOST()
        //{
        //    // Read file data
        //    FileStream fs = new FileStream("C:\ESTUDOS\answer.json", FileMode.Open, FileAccess.Read);
        //    byte[] data = new byte[fs.Length];
        //    fs.Read(data, , data.Length);
        //    fs.Close();

        //    // Generate post objects
        //    Dictionary<string, object> postParameters = new Dictionary<string, object>();
        //    postParameters.Add("file", new FormUpload.FileParameter(data, "answer.json", "application/msword"));

        //    // Create request and receive response
        //    string postURL = "https://api.codenation.dev/v1/challenge/dev-ps/submit-solution?token=aea570071bb5c870c7317ceb82282d308b21f2f9";
        //    string userAgent = "Someone";
        //    HttpWebResponse webResponse = FormUpload.MultipartFormPost(//(postURL, userAgent, postParameters);

        //    // Process response
        //    StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
        //    string fullResponse = responseReader.ReadToEnd();
        //    webResponse.Close();
        //    Response.Write(fullResponse);
        //}

        //public static void lerArquivo()
        //{
        //    // read file into a string and deserialize JSON to a type
        //    Resp r1 = JsonConvert.DeserializeObject<Resp>(File.ReadAllText(@"C:\ESTUDOS\answer.json"));

        //    // deserialize JSON directly from a file
        //    using (StreamReader file = File.OpenText(@"C:\ESTUDOS\answer.json"))
        //    {
        //        JsonSerializer serializer = new JsonSerializer();
        //        Resp r2 = (Resp)serializer.Deserialize(file, typeof(Resp));
        //    }
        //}



        //public static void EnviaRequisicaoPOST()
        //{
        //    //string dadosPOST = "title=macoratti";
        //    //dadosPOST = dadosPOST + "&body=teste de envio de post";
        //    //dadosPOST = dadosPOST + "&userId=1";
        //    //var dados = Encoding.UTF8.GetBytes(dadosPOST);

        //    //var requisicaoWeb = WebRequest.CreateHttp("https://api.codenation.dev/v1/challenge/dev-ps/submit-solution?token=aea570071bb5c870c7317ceb82282d308b21f2f9");

        //    //requisicaoWeb.Method = "POST";
        //    //requisicaoWeb.ContentType = "application/x-www-form-urlencoded";
        //    //requisicaoWeb.ContentLength = dados.Length;
        //    //requisicaoWeb.UserAgent = "RequisicaoWebDemo";

        //    ////precisamos escrever os dados post para o stream
        //    //using (var stream = requisicaoWeb.GetRequestStream())
        //    //{
        //    //    stream.Write(dados, 0, dados.Length);
        //    //    stream.Close();
        //    //}

        //    //using (var resposta = requisicaoWeb.GetResponse())
        //    //{
        //    //    var streamDados = resposta.GetResponseStream();
        //    //    StreamReader reader = new StreamReader(streamDados);
        //    //    object objResponse = reader.ReadToEnd();

        //    //    var post = JsonConvert.DeserializeObject<Resp>(objResponse.ToString());

        //    //    Console.WriteLine(post.Id + " " + post.title + " " + post.body);
        //    //    streamDados.Close();
        //    //    resposta.Close();

        //        string requestURL = ""https://api.codenation.dev/v1/challenge/dev-ps/submit-solution?token=aea570071bb5c870c7317ceb82282d308b21f2f9"";
        //        string fileName = "answer.json";
        //        WebClient wc = new WebClient();
        //        byte[] bytes = File.ReadAllBytes(fileName);
        //        //byte[] bytes = wc.DownloadData(fileName); // You need to do this download if your file is on any other server otherwise you can convert that file directly to bytes  
        //        Dictionary<string, object> postParameters = new Dictionary<string, object>();
        //        // Add your parameters here  
        //        postParameters.Add("fileToUpload", new FormUpload.FileParameter(bytes, Path.GetFileName(fileName)));
        //        string userAgent = "Someone";
        //        HttpWebResponse webResponse = FormUpload.MultipartFormDataPost(requestURL, userAgent, postParameters, headerkey, headervalue);
        //        // Process response  
        //        StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
        //        returnResponseText = responseReader.ReadToEnd();
        //        webResponse.Close();

        //}

        //    Console.ReadLine();
        //}

    }
    

}
