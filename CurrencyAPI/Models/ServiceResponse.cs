namespace WalletSystemAPI.Models
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceResponse<T>
    {
        /// <summary>
        ///
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        ///
        /// </summary>
        public string Message { get; set; } = null;
    }
}