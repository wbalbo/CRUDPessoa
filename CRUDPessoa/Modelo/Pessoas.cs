namespace CRUDPessoa.Model
{
    public class Pessoas
    {
        public int IdPessoa { get; set; }
        public string Pessoa { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public bool EhAtivo { get; set; } = true;
    }
}
