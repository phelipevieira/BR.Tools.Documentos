using FluentAssertions;
using Xunit;

namespace BR.Tools.Documentos.Testes.Unidade
{
    public class TestesUnidadeCPF
    {
        [Fact]
        public void GerarCpfComPontuacao()
        {
            var cpf = CPF.Handler.GerarCPFComPontuacao();

            cpf.Should().BeOfType(typeof(string));
            cpf.Should().NotBeNullOrEmpty();
            cpf.Should().Contain(".");
            cpf.Should().Contain("-");
            cpf.Length.Should().Be(14);
            cpf.Substring(3, 1).Should().Be(".");
            cpf.Substring(7, 1).Should().Be(".");
            cpf.Substring(11, 1).Should().Be("-");
        }

        [Fact]
        public void GerarCpfComoNumero()
        {
            var cpf = CPF.Handler.GerarCPFComoNumero();

            cpf.Should().BeOfType(typeof(long));
            cpf.Should().NotBe(0);
            cpf.ToString().Length.Should().Be(11);            
        }

        [Fact]
        public void ValidarCpfComPontuacaoSucesso()
        {
            var cpf = CPF.Handler.GerarCPFComPontuacao();

            var isCPF = CPF.Handler.ValidarCPFString(cpf);

            isCPF.Should().BeTrue();
        }

        [Fact]
        public void ValidarCpfComPontuacaoFalha()
        {
            var cpf = "1234567890";

            var isCPF = CPF.Handler.ValidarCPFString(cpf);

            isCPF.Should().BeFalse();
        }

        [Fact]
        public void ValidarCpfComoNumeroSucesso()
        {
            var cpf = CPF.Handler.GerarCPFComoNumero();

            var isCPF = CPF.Handler.ValidarCPFInteiro(cpf);

            isCPF.Should().BeTrue();
        }

        [Fact]
        public void ValidarCpfComoNumeroFalha()
        {
            var cpf = 2345678901;

            var isCPF = CPF.Handler.ValidarCPFInteiro(cpf);

            isCPF.Should().BeFalse();
        }

    }
}
