using System;
using System.Linq;
using HttpRestApi;

namespace HttpRestApiConsole
{
    class Program
    {

        //static void Main(string[] args)
        //{

        //    var result = generalizedGCD(5, new int[5] { 2,4,6,8,10});

        //    Console.WriteLine(result);

        //    //for (int i = 0; i < result.Length; i++)
        //    //{
        //    //    Console.Write($"{result[i]} ");
        //    //}

        //    Console.ReadKey();
        //}


        //public static int generalizedGCD(int num, int[] arr)
        //{
        //    var listNumber = arr.ToList().OrderBy(x=> x);

        //    var number =  listNumber.LastOrDefault(x => x%num == 0);

        //    return number/num;
        //}
        

        //public static int[] cellCompete(int[] states, int days)
        //{
        //    int[] oldStates = (int[])states.Clone(); //new int[states.Length];

        //    //for (int i = 0; i < states.Length; i++)
        //    //    oldStates[i] = states[i];

        //    for (int k = 0; k < days; k++)
        //    {
        //        for (int j = 1; j < oldStates.Length - 1; j++)
        //        {
        //            if ((oldStates[j - 1] == 1 && oldStates[j + 1] == 1) || (oldStates[j - 1] == 0 && oldStates[j + 1] == 0))
        //                states[j] = 0;
        //            else
        //                states[j] = 1;
        //        }

        //        if (oldStates[1] == 0)
        //            states[0] = 0;
        //        else
        //            states[0] = 1;

        //        if (oldStates[6] == 0)
        //            states[7] = 0;
        //        else
        //            states[7] = 1;

        //        for (int i = 0; i < states.Length; i++)
        //            oldStates[i] = states[i];
        //    }

        //    return states;

        //}

        static void Main(string[] args)
        {
            // ViaCEp

            using (var client2 = new HttpRestApiClient("http://viacep.com.br/"))
            {
                string cep = "03260000";
                client2.Request = HttpRestApiRequestFactory.Create($"ws/{cep}/json/");

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
