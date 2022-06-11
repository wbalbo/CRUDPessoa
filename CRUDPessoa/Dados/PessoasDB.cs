using CRUDPessoa.Model;
using System.Data.SqlClient;

namespace CRUDPessoa.Dados
{
    public class PessoasDB : ICore
    {
        #region Variáveis Globais

        private readonly string stringConexao = @"Data Source=DESKTOP-D324NU8\SQL19;Initial Catalog=Tests;Integrated Security=True";
        private readonly string instrucaoSqlDelete = "DELETE FROM Pessoas WHERE IdPessoa = @IdPessoa";
        private readonly string instrucaoSqlInsert = "INSERT INTO Pessoas VALUES (@Pessoa, @Email, @Telefone, @EhAtivo)";
        private readonly string instrucaoSqlSelect = "SELECT IdPessoa, Pessoa, Email, Telefone, EhAtivo FROM Pessoas";
        private readonly string instrucaoSqlUpdate = "UPDATE Pessoas SET Pessoa = @Pessoa, Email = @Email WHERE IdPessoa = @IdPessoa";
        private SqlConnection _connection = null!;
        private SqlCommand _command = null!;

        #endregion

        #region Métodos Genéricos

        public void AdicionarParametros(string nome, object valor)
        {
            _command.Parameters.AddWithValue(nome, valor);
        }

        public void InstanciarObjetoCommand(string instrucaoSql)
        {
            _command = new(instrucaoSql);
        }

        public void InstanciarObjetoConexao()
        {
            _connection = new(stringConexao);
        }

        #endregion

        #region Métodos

        public void AdicionarPessoa(Pessoas pessoas)
        {
            try
            {
                InstanciarObjetoConexao();
                InstanciarObjetoCommand(instrucaoSqlInsert);

                _connection.Open();
                _command.Connection = _connection;

                AdicionarParametros("@Pessoa", pessoas.Pessoa);
                AdicionarParametros("@Email", pessoas.Email);
                AdicionarParametros("@Telefone", pessoas.Telefone);
                AdicionarParametros("@EhAtivo", pessoas.EhAtivo);

                _command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _connection.Close();
                _connection.Dispose();
            }
        }

        public void AtualizarPessoa(Pessoas pessoas)
        {
            try
            {
                InstanciarObjetoConexao();
                InstanciarObjetoCommand(instrucaoSqlUpdate);

                _connection.Open();
                _command.Connection = _connection;

                AdicionarParametros("@IdPessoa", pessoas.IdPessoa);
                AdicionarParametros("@Pessoa", pessoas.Pessoa);
                AdicionarParametros("@Email", pessoas.Email);
                AdicionarParametros("@EhAtivo", pessoas.EhAtivo);

                _command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _connection.Close();
                _connection.Dispose();
            }
        }

        public List<Pessoas> ListarPessoas()
        {
            List<Pessoas> listaDePessoas = new();

            try
            {
                InstanciarObjetoConexao();
                InstanciarObjetoCommand(instrucaoSqlSelect);

                _connection.Open();
                _command.Connection = _connection;

                SqlDataReader leitor = _command.ExecuteReader();
                if (leitor.HasRows)
                {
                    while (leitor.Read())
                    {
                        Pessoas pessoas = new()
                        {
                            IdPessoa = Convert.ToInt32(leitor["IdPessoa"]),
                            Pessoa = leitor["Pessoa"].ToString(),
                            Telefone = leitor["Telefone"].ToString(),
                            Email = leitor["Email"].ToString(),
                            EhAtivo = Convert.ToBoolean(leitor["EhAtivo"])
                        };

                        listaDePessoas.Add(pessoas);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _connection.Close();
                _connection.Dispose();
            }

            return listaDePessoas;
        }

        public void ExcluirPessoa(int idPessoa)
        {
            try
            {
                InstanciarObjetoConexao();
                InstanciarObjetoCommand(instrucaoSqlDelete);

                _connection.Open();
                _command.Connection = _connection;

                AdicionarParametros("@IdPessoa", idPessoa);
                
                _command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _connection.Close();
                _connection.Dispose();
            }
        }

        #endregion
    }
}
