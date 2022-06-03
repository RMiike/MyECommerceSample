namespace MECS.Cart.API.Models
{
    public enum TipoDescontoVoucher
    {
        Porcentagem = 0,
        Valor = 1
    }
    public class Voucher
    {
        public string Codigo { get; set; }
        public decimal? Percentual { get; set; }
        public decimal? ValorDesconto { get; set; }
        public TipoDescontoVoucher TipoDesconto { get; set; }
    }
}
