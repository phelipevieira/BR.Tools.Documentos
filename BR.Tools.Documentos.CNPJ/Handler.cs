using System;

namespace BR.Tools.Documentos.CNPJ
{
    public static class Handler
    {
        private static string GerarCNPJ()
        {
            Random rnd = new Random();
            int n1 = rnd.Next(10);
            int n2 = rnd.Next(10);
            int n3 = rnd.Next(10);
            int n4 = rnd.Next(10);
            int n5 = rnd.Next(10);
            int n6 = rnd.Next(10);
            int n7 = rnd.Next(10);
            int n8 = rnd.Next(10);
            int n9 = 0;
            int n11 = 0;
            int n10 = 0;
            int n12 = 1;
            int d1 = n12 * 2 + n11 * 3 + n10 * 4 + n9 * 5 + n8 * 6 + n7 * 7 + n6 * 8 + n5 * 9 + n4 * 2 + n3 * 3 + n2 * 4 + n1 * 5;
            d1 = 11 - (d1 % 11);

            if (d1 >= 10) d1 = 0;
            int d2 = d1 * 2 + n12 * 3 + n11 * 4 + n10 * 5 + n9 * 6 + n8 * 7 + n7 * 8 + n6 * 9 + n5 * 2 + n4 * 3 + n3 * 4 + n2 * 5 + n1 * 6;
            d2 = 11 - (d2 % 11);
            if (d2 >= 10) d2 = 0;


            string cnpj = string.Concat(n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, n11, n12, d1, d2);

            return cnpj;
        }

        public static string GerarCNPJComPontuacao()
        {
            var cnpj = GerarCNPJ();

            return string.Concat(cnpj.Substring(0, 2), '.', cnpj.Substring(2, 3), '.', cnpj.Substring(5, 3), '/', cnpj.Substring(8, 4), '-', cnpj.Substring(13, 2));
        }
        public static int GerarCNPJComoInteiro()
        {
            var cnpj = GerarCNPJ();
            if (int.TryParse(cnpj, out int resultado))
            {
                return resultado;
            }

            throw new ApplicationException("Houve um erro ao gerar o CNPJ solicitado, a combinação de numeros não gerou um numero inteiro válido.");
        }

        public static bool ValidarCNPJString(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpj.Length != 14)
            {
                return false;
            }

            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;

            for (int i = 0; i < 12; i++)
            {
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            }

            resto = (soma % 11);

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito = resto.ToString();
            tempCnpj += digito;
            soma = 0;

            for (int i = 0; i < 13; i++)
            {
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            }

            resto = (soma % 11);

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito += resto.ToString();

            return cnpj.EndsWith(digito);
        }

        public static bool ValidarCNPJInteiro(int cnpj)
        {
            return ValidarCNPJString(cnpj.ToString());
        }
    }
}
