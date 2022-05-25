using Logger.Debugging;

namespace Logger
{
    public class LoggerConsts
    {
        public const string LocalizationSourceName = "Logger";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "3a3fb27b538544b5a32991e96416f05f";
    }
}
