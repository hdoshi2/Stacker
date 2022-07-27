using System.Threading.Tasks;
using Supabase;

namespace Stacker.Supabase
{
    internal class ConnectionBase
    {
        #region

        private const string ServerKey =
            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImpyZGJ0eWlreWJ4aWh1dm1ibm9mIiwicm9s" +
            "ZSI6ImFub24iLCJpYXQiOjE2NDcyMDk2MjgsImV4cCI6MTk2Mjc4NTYyOH0.mV0y9rTwk4Lgj3eGog6YIqanN1ZeWnUWUfCkbPyAR2c";

        private const string ServerUrl = "https://jrdbtyikybxihuvmbnof.supabase.co";

        #endregion

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="DesignBuildDB" /> is connected.
        /// </summary>
        /// <value>
        ///     <c>true</c> if connected; otherwise, <c>false</c>.
        /// </value>
        public bool Connected { get; set; }

        /// <summary>
        ///     Initializes the supabase connection.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> InitializeSupabase()
        {
            Connected = false;

            var client = await Client.InitializeAsync(ServerUrl, ServerKey);

            if (client != null)
            {
                Connected = true;
            }

            return Connected;
        }
    }
}
