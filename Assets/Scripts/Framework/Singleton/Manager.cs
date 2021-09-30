namespace Framework.Singleton
{
    using Framework.Extensions;

#if ODIN_INSPECTOR
    public class Manager<T> : MonobehaviourSingleton<T> where T : Sirenix.OdinInspector.SerializedMonoBehaviour
#else
    public class Manager<T> : MonobehaviourSingleton<T> where T : UnityEngine.MonoBehaviour
#endif
    {
        private string p_headerColor = string.Empty;

        private string p_hexColorLog = string.Empty;

        protected virtual string HexColorLog
        {
            get
            {
                if (this.p_hexColorLog != string.Empty)
                {
                    return this.p_hexColorLog;
                }

                return this.p_hexColorLog = typeof(T).Name.ToHexColor();
            }
        }

        protected enum E_LogType
        {
            Normal,
            Warning,
            Error,
            Important
        }

        public void Log(string text)
        {
            UnityEngine.Debug.Log($"<b>{this.GetStylizedHeaderLog(E_LogType.Normal)}{this.GetColouredName()}</b> \r\n\t {text}");
        }

        public void LogWarning(string text)
        {
            UnityEngine.Debug.LogWarning($"<b>{this.GetStylizedHeaderLog(E_LogType.Warning)} {this.GetColouredName()}</b> \r\n\t {text}");
        }

        public void LogError(string text)
        {
            UnityEngine.Debug.LogWarning($"<b>{this.GetStylizedHeaderLog(E_LogType.Error)} {this.GetColouredName()}</b> \r\n\t {text}");
        }

        public void LogImportant(string text)
        {
            UnityEngine.Debug.Log($"<b>{this.GetStylizedHeaderLog(E_LogType.Important)} {this.GetColouredName()}</b> \r\n\t {text}");
        }

        protected string GetStylizedHeaderLog(E_LogType logType)
        {
            switch (logType)
            {
                case E_LogType.Warning:
                    this.p_headerColor = "#F7D960";
                    break;
                case E_LogType.Error:
                    this.p_headerColor = "#FA2723";
                    break;
                case E_LogType.Important:
                    this.p_headerColor = "#A952FA";
                    break;
                case E_LogType.Normal:
                default:
                    break;
            }

            return this.p_headerColor != string.Empty ? $"<color={this.p_headerColor}>{logType}</color>" : string.Empty;
        }

        protected string GetColouredName()
        {
            return string.Format("<color={0}>#{1}</color>", this.HexColorLog, typeof(T).Name);
        }
    }
}