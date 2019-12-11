using System;
using HttpRestApi;

namespace HttpRestApiConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // ViaCEp

            using (var client2 = new HttpRestApiClient("http://viacep.com.br/"))
            {
                string cep = "03260000";
                client2.Request = new HttpRestApiRequest($"ws/{cep}/json/");

                var response2 = client2.GetAsync<ViaCepEndereco>();

                Console.WriteLine(response2.Result.Logradouro);
                Console.WriteLine(response2.Result.Cep);
                Console.WriteLine(response2.Result.Complemento);
                Console.WriteLine(response2.Result.Bairro);
                Console.WriteLine(response2.Result.Localidade);
                Console.WriteLine(response2.Result.Uf);
                Console.WriteLine(response2.Result.Unidade);
                Console.WriteLine(response2.Result.Ibge);
                Console.WriteLine(response2.Result.Gia);

                Console.ReadKey();
            }
        }
        public class ViaCepEndereco
        {
            public string Cep { get; set; }
            public string Logradouro { get; set; }
            public string Complemento { get; set; }
            public string Bairro { get; set; }
            public string Localidade { get; set; }
            public string Uf { get; set; }
            public string Unidade { get; set; }
            public string Ibge { get; set; }
            public string Gia { get; set; }
        }
    }
}
