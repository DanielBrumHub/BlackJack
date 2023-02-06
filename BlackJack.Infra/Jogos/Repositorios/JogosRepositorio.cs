using BlackJack.Dominio.Jogos.Entidades;
using BlackJack.Infra.Jogos.Repositorios.Consultas;
using BlackJack.Dominio.Jogos.Repositorios;
using BlackJack.Infra.Uteis.Interfaces;
using System.Data.SqlClient;
using Dapper;

namespace BlackJack.Infra.Jogos.Repositorios
{
    public class JogosRepositorio : IJogosRepositorio
    {
        private readonly SqlConnection Con;

        public JogosRepositorio(IConexaoBanco conexaoBanco)
        {
            Con = new SqlConnection(conexaoBanco.GetConnection());
        }

        public int Inserir(string nomeJogador)
        {
            int idJogo;
            Con.Open();
            var query = @"INSERT INTO tbl_jogos (Descricao) VALUES (@NOMEJOGADOR);
                          SELECT Id FROM tbl_jogos j WHERE j.Descricao = @NOMEJOGADOR;";

            var parametros = new DynamicParameters();
            parametros.Add("@NOMEJOGADOR", nomeJogador);

            idJogo = Con.Query<int>(query, parametros).Single();
            Con.Close();

            return idJogo;
        }

        public void InserirJogada(Carta carta, int idJogo, bool dealer)
        {
            Con.Open();
            var query = @"INSERT INTO tbl_jogadas (IdtDealer, IdCarta, IdNipe, IdJogo) VALUES (@IDTDEALER, @IDCARTA, @IDNIPE, @IDJOGO);";

            var parametros = new DynamicParameters();
            parametros.Add("@IDTDEALER", dealer);
            parametros.Add("@IDCARTA", carta.Id);
            parametros.Add("@IDNIPE", carta.Nipe.Id);
            parametros.Add("@IDJOGO", idJogo);

            Con.Execute(query, parametros);
            Con.Close();
        }

        public void EncerrarJogo(int idJogo)
        {
            Con.Open();
            var query = @"UPDATE tbl_jogos j
                          SET j.Encerrado = 1
                          WHERE j.Id = @IDJOGO;";

            var parametros = new DynamicParameters();
            parametros.Add("@IDJOGO", idJogo);

            Con.Execute(query, parametros);
            Con.Close();
        }

        public IList<JogadasConsulta> RecuperarJogadas(int idJogo)
        {
            Con.Open();
            IList<JogadasConsulta> jogadas;
            var query = @"SELECT j.*, 
                                 jo.Encerrado
	                             c.Descricao AS DescricaoCarta,
	                             c.Valor AS ValorCarta,
	                             n.Descricao AS DescricaoNipe 
                            FROM tbl_jogadas j
                            INNER JOIN tbl_jogos jo ON jo.Id = j.IdJogo
                            INNER JOIN tbl_cartas c ON c.Id = j.IdCarta
                            INNER JOIN tbl_nipes n ON n.Id = j.IdNipe
                            WHERE j.IdJogo = @IDJOGO;";

            var parametros = new DynamicParameters();
            parametros.Add("@IDJOGO", idJogo);

            jogadas = Con.Query<JogadasConsulta>(query, parametros).ToList();
            Con.Close();

            return jogadas;
        }
    }
}
