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
            using var con = new SqlConnection(conexaoBanco.GetConnection());
            con.Open();
            var query = @"SELECT c.Id, c.Valor, c.Descricao, 
                                     c.Id AS IdCarta, n.Id, n.Descricao
                            FROM tbl_cartas c 
                            LEFT JOIN tbl_nipes n ON NOT EXISTS (SELECT 1 FROM tbl_jogadas j
										                            WHERE j.IdJogo = @IDJOGO
										                            AND   j.IdNipe = n.Id
										                            AND   j.IdCarta = c.Id);";

            var parametros = new DynamicParameters();
            parametros.Add("@IDJOGO", idJogo);

            cartas = con.Query<Carta, Nipe, Carta>(query, (carta, nipe) =>
            {
                carta.SetNipe(nipe);
                return carta;
            }, parametros, splitOn: "IdCarta").ToList();
            con.Close();

            return cartas;
        }
    }
}
