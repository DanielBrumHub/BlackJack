using BlackJack.Dominio.Jogos.Entidades;
using BlackJack.Dominio.Jogos.Repositorios;
using BlackJack.Infra.Uteis.Interfaces;
using System.Data.SqlClient;
using Dapper;

namespace BlackJack.Infra.Jogos.Repositorios
{
    public class CartasRepositorio : ICartasRepositorio
    {
        private readonly SqlConnection Con;
        public CartasRepositorio(IConexaoBanco conexaoBanco)
        {
            Con = new SqlConnection(conexaoBanco.GetConnection());
        }

        public IList<Carta> RecuperarDisponiveis(int idJogo)
        {
            Con.Open();
            IList<Carta> cartas;
            var query = @"SELECT c.Id, c.Valor, c.Descricao, 
                                 c.Id AS IdCarta, n.Id, n.Descricao
                            FROM tbl_cartas c 
                            LEFT JOIN tbl_nipes n ON NOT EXISTS (SELECT 1 FROM tbl_jogadas j
										                            WHERE j.IdJogo = @IDJOGO
										                            AND   j.IdNipe = n.Id
										                            AND   j.IdCarta = c.Id);";

            var parametros = new DynamicParameters();
            parametros.Add("@IDJOGO", idJogo);

            cartas = Con.Query<Carta, Nipe, Carta>(query, (carta, nipe) =>
            {
                carta.SetNipe(nipe);
                return carta;
            }, parametros, splitOn: "IdCarta").ToList();
            Con.Close();

            return cartas;
        }
    }
}
