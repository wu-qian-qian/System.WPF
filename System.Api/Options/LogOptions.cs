namespace System.Api.Options
{
    public class LogOptions
    {
        /// <summary>
        /// 是否计入Get
        /// </summary>
        public bool IsRecordGet { get; set; } = true;
        /// <summary>
        /// 是否计入日志
        /// </summary>
        public List<string> IsRecord { get; set; } = new List<string>();
    }
}
