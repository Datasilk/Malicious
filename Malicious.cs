namespace Utility
{
    public class Malicious
    {

        public enum InputType
        {
            TextOnly = 0,
            ContainsJavaScript = 1,
            ContainsHtml = 2,
            IsJson = 3
        }

        public static bool IsMalicious(string input, InputType type = InputType.TextOnly)
        {
            return false;
        }
    }
}
