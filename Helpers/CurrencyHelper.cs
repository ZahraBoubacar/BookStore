namespace BookStore.Helpers
{
    public static class CurrencyHelper
    {
        public static string FormatTND(decimal amount) => $"{amount:F3} DT";
    }
}
