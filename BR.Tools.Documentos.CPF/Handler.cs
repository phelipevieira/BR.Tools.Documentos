using System;

namespace BR.Tools.Documentos.CPF
{
    public class Handler
    {
        private static string GerarCPF()
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
            int n9 = rnd.Next(10);
            int somatorio1 = n1 * 10 + n2 * 9 + n3 * 8 + n4 * 7 + n5 * 6 + n6 * 5 + n7 * 4 + n8 * 3 + n9 * 2;
            int digito1 = 11 - (somatorio1 % 11);


            if ((somatorio1 % 11) < 2)
            {
                digito1 = 0;
            }

            int somatorio2 = n1 * 11 + n2 * 10 + n3 * 9 + n4 * 8 + n5 * 7 + n6 * 6 + n7 * 5 + n8 * 4 + n9 * 3 + digito1 * 2;
            int digito2 = 11 - (somatorio2 % 11);

            if ((somatorio2 % 11) < 2)
            {
                digito2 = 0;
            }

            string cpf = string.Concat(n1, n2, n3, n4, n5, n6, n7, n8, n9, digito1, digito2);

            return cpf;
        }

        public static string GerarCPFComPontuacao()
        {
            var cpf = GerarCPF();

            return string.Concat(cpf.Substring(0, 3), '.', cpf.Substring(3, 3), '.', cpf.Substring(6, 3), '-', cpf.Substring(9, 2));
        }

        public static long GerarCPFComoNumero()
        {
            var cpf = GerarCPF();
            if (long.TryParse(cpf, out long resultado))
            {
                return resultado;
            }

            throw new ApplicationException("Houve um erro ao gerar o CPF solicitado, a combinação de numeros não gerou um numero inteiro válido.");
        }

        public static bool ValidarCPFString(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
            {
                return false;
            }

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
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

            digito = resto.ToString();
            tempCpf += digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
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

            digito += resto.ToString();

            return cpf.EndsWith(digito);
        }

        public static bool ValidarCPFInteiro(long cpf)
        {
            var cpfTexto = cpf.ToString();
            var length = cpfTexto.Length;

            if (length == 11)
            {
                return ValidarCPFString(cpfTexto);
            }
            else
            {
                var loop = 11 - length;
                for (int i = 0; i < loop; i++)
                {
                    cpfTexto = String.Concat(0, cpfTexto);
                }

                return ValidarCPFString(cpfTexto);
            }
        }
    }
}
