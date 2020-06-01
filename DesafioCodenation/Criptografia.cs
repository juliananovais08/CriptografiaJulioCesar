using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace DesafioCodenation
{
    class Criptografia
    {

        public static string Decifrar(string cifrado, int numero_casas)
        {
            //numero_casas = 1;
            //cifrado = "hppe jt uif fofnz pg hsfbu, cvu hsfbu jt uif fofnz pg tijqqfe. kfggsfz afmenbo";
            //string frase = r.cifrado;

            var stringMinuscula = cifrado.ToLower();
            var decifrado = "";

            if(numero_casas != 0)
            {
                foreach (var caracter in cifrado)
                {
                    //é uma letra
                    if (Char.IsLetter(caracter))
                    {
                        //tablea ascii - alfabeto minúsculo
                        var primeiroCaracter = 97;
                        var ultimoCaracter = 122;

                        var ascCripitografia = (int)caracter;
                        var ascDescriptografado = ascCripitografia - numero_casas;

                        if (ascDescriptografado < primeiroCaracter)
                        {
                            var diferenca = primeiroCaracter - ascDescriptografado;
                            ascDescriptografado = (ultimoCaracter + 1) - diferenca;
                        }
                        var letraDecifrada = (char)(ascDescriptografado);
                        decifrado += letraDecifrada;

                    }
                    else
                    {
                        decifrado += caracter;
                    }

                }

            }

            return decifrado;

        }

        public static string CalculateSHA1(string text, Encoding enc)
        {
            try
            {
                byte[] buffer = enc.GetBytes(text);
                System.Security.Cryptography.SHA1CryptoServiceProvider cryptoTransformSHA1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
                string hash = BitConverter.ToString(cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "").ToLower();
                return hash;
            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }
        }

    }
}