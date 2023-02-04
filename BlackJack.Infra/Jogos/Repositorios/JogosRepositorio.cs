using BlackJack.Dominio.Jogos.Repositorios;
using BlackJack.Infra.Uteis.Interfaces;
using System.Data.SqlClient;
using Dapper;
using BlackJack.Dominio.Jogos.Entidades;

namespace BlackJack.Infra.Jogos.Repositorios
{
    public class JogosRepositorio : IJogosRepositorio
    {
        IConexaoBanco conexaoBanco;
        public JogosRepositorio(IConexaoBanco conexaoBanco)
        {
            this.conexaoBanco = conexaoBanco;
        }

        public int Inserir(string nomeJogador)
        {
            int idJogo;
            using (var con = new SqlConnection(conexaoBanco.GetConnection()))
            {
                con.Open();
                var query = @"INSERT INTO tbl_jogos (Descricao) VALUES (@NOMEJOGADOR);
                              SELECT Id FROM tbl_jogos j WHERE j.Descricao = @NOMEJOGADOR;";

                var parametros = new DynamicParameters();
                parametros.Add("@NOMEJOGADOR", nomeJogador);

                idJogo = con.Query<int>(query, parametros).Single();
                con.Close();

                return idJogo;
            }
        }

        public void InserirJogada(Carta carta, int idJogo)
        {
            using (var con = new SqlConnection(conexaoBanco.GetConnection()))
            {
                con.Open();
                var query = @"INSERT INTO tbl_jogadas (Descricao) VALUES (@NOMEJOGADOR);";

                var parametros = new DynamicParameters();
                parametros.Add("@NOMEJOGADOR", nomeJogador);
                parametros.Add("@NOMEJOGADOR", nomeJogador);

                idJogo = con.Query<int>(query, parametros).Single();
                con.Close();
            }
        }
    }
}
