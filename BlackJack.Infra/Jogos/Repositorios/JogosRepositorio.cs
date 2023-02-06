using BlackJack.Dominio.Jogos.Repositorios;
using BlackJack.Infra.Uteis.Interfaces;
using System.Data.SqlClient;
using Dapper;
using BlackJack.Dominio.Jogos.Entidades;
using BlackJack.Infra.Jogos.Repositorios.Consultas;

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
            using var con = new SqlConnection(conexaoBanco.GetConnection());
            con.Open();
            var query = @"INSERT INTO tbl_jogos (Descricao) VALUES (@NOMEJOGADOR);
                              SELECT Id FROM tbl_jogos j WHERE j.Descricao = @NOMEJOGADOR;";

            var parametros = new DynamicParameters();
            parametros.Add("@NOMEJOGADOR", nomeJogador);

            idJogo = con.Query<int>(query, parametros).Single();
            con.Close();

            return idJogo;
        }

        public void InserirJogada(Carta carta, int idJogo, bool dealer)
        {
            using var con = new SqlConnection(conexaoBanco.GetConnection());
            con.Open();
            var query = @"INSERT INTO tbl_jogadas (IdtDealer, IdCarta, IdNipe, IdJogo) VALUES (@IDTDEALER, @IDCARTA, @IDNIPE, @IDJOGO);";

            var parametros = new DynamicParameters();
            parametros.Add("@IDTDEALER", dealer);
            parametros.Add("@IDCARTA", carta.Id);
            parametros.Add("@IDNIPE", carta.Nipe.Id);
            parametros.Add("@IDJOGO", idJogo);

            idJogo = con.Execute(query, parametros);
            con.Close();
        }

        public IList<JogadasConsulta> RecuperarJogadas(int idJogo)
        {
            IList<JogadasConsulta> jogadas;
            using var con = new SqlConnection(conexaoBanco.GetConnection());
            con.Open();
            var query = @"SELECT j.*, 
	                               c.Descricao AS DescricaoCarta,
	                               c.Valor AS ValorCarta,
	                               n.Descricao AS DescricaoNipe 
                            FROM tbl_jogadas j
                            INNER JOIN tbl_cartas c ON c.Id = j.IdCarta
                            INNER JOIN tbl_nipes n ON n.Id = j.IdNipe
                            WHERE j.IdJogo = @IDJOGO;";

            var parametros = new DynamicParameters();
            parametros.Add("@IDJOGO", idJogo);

            jogadas = con.Query<JogadasConsulta>(query, parametros).ToList();
            con.Close();

            return jogadas;
        }
    }
}
