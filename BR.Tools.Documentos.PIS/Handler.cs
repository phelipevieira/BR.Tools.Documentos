using System;

namespace BR.Tools.Documentos.PIS
{
    public static class Handler
    {
        public static string GerarPISString()
        {
            Random rnd = new Random();
            int n1 = 1;
            int n2 = 2;
            int n3 = rnd.Next(5, 10);
            int n4 = rnd.Next(10);
            int n5 = rnd.Next(10);
            int n6 = rnd.Next(10);
            int n7 = rnd.Next(10);
            int n8 = rnd.Next(10);
            int n9 = rnd.Next(10);
            int n10 = rnd.Next(10);

            int somatorio = (n1 * 3) + (n2 * 2) + (n3 * 9) + (n4 * 8) + (n5 * 7) + (n6 * 6) + (n7 * 5) + (n8 * 4) + (n9 * 3) + (n10 * 2);
            int resto = (somatorio % 11);
            int resultado = 0;
            if (resto > 1)
            {
                resultado = 11 - resto;
            };

            string numeroPuro = string.Concat(n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, resultado);

            return numeroPuro;
        }

        public static int GerarPISInteiro()
        {
            var pis = GerarPISString();

            if (int.TryParse(pis, out int resultado))
            {
                return resultado;
            }

            throw new ApplicationException("Houve um erro ao gerar o PIS solicitado, a combinação de numeros não gerou um numero inteiro válido.");
        }

        public static bool ValidarPISString(string pis)
        {
            int[] multiplicador = new int[10] { 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            if (pis.Trim().Length != 11)
            {
                return false;
            }

            pis = pis.Trim();
            pis = pis.Replace("-", "").Replace(".", "").PadLeft(11, '0');

            soma = 0;

            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(pis[i].ToString()) * multiplicador[i];
            }
            resto = soma % 11;

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            return pis.EndsWith(resto.ToString());
        }

        public static bool ValidarPISInteiro(int pis)
        {
            return ValidarPISString(pis.ToString());
        }
    }
}
