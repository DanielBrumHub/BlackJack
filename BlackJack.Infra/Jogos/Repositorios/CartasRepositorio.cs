using BlackJack.Dominio.Jogos.Entidades;
using BlackJack.Dominio.Jogos.Repositorios;
using BlackJack.Infra.Uteis.Interfaces;
using Dapper;
using System.Data.SqlClient;

namespace BlackJack.Infra.Jogos.Repositorios
{
    public class CartasRepositorio : ICartasRepositorio
    {
        IConexaoBanco conexaoBanco;
        public CartasRepositorio(IConexaoBanco conexaoBanco)
        {
            this.conexaoBanco = conexaoBanco;
        }

        public IList<Carta> RecuperarDisponiveis(int idJogo)
        {
            IList<Carta> cartas;
            using (var con = new SqlConnection(conexaoBanco.GetConnection()))
            {
                con.Open();
                var query = @"SELECT c.Descricao, n.Descricao AS Nipe, c.Id, c.Valor 
                            FROM tbl_cartas c 
                            LEFT JOIN tbl_nipes n ON NOT EXISTS (SELECT 1 FROM tbl_jogadas j
										                            WHERE j.IdJogo = @IDJOGO
										                            AND   j.IdNipe = n.Id
										                            AND   j.IdCarta = c.Id);";

                var parametros = new DynamicParameters();
                parametros.Add("@IDJOGO", idJogo);

                cartas = con.Query<Carta>(query, parametros).ToList();
                con.Close();

                return cartas;
            }
        }
    }
}
