namespace TouchPortalSDK
{
    public class TouchPortalOptions
    {
        public string IpAddress { get; set; } = "127.0.0.1";
        public int Port { get; set; } = 12136;
        /// <summary> If this is set to a non-null character, will split each incoming `action.data.id` value on this char and only keep the last part. </summary>
        public static char ActionDataIdSeparator { get; set; } = '\0';
        /// <summary> Set to `true` to skip validation of parameter values for all Command types. /// </summary>
        public static bool ValidateCommandParameters { get; set; } = true;
    }
}
