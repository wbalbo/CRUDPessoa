namespace CRUDPessoa.Dados
{
    public interface ICore
    {
        void AdicionarParametros(string nome, object valor);
        void InstanciarObjetoCommand(string instrucaoSql);

        void InstanciarObjetoConexao();
    }
}
