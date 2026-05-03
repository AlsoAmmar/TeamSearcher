using System;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;

namespace TeamSearcher.Models
{
    public partial class MobileInputHelper
    {
        // Events now include the target string so the ViewModel knows which property to update
        public static event Action<string, string>? TextReceived;
        public static event Action<string>? BackspacePressed;
        public static event Action<string>? EnterPressed;

        [JSExport]
        public static void OnTextReceived(string target, string text)
        {
            TextReceived?.Invoke(target, text);
        }

        [JSExport]
        public static void OnBackspace(string target)
        {
            BackspacePressed?.Invoke(target);
        }

        [JSExport]
        public static void OnEnter(string target)
        {
            EnterPressed?.Invoke(target);
        }

        /// <summary>
        /// Triggers the hidden HTML input focus.
        /// </summary>
        /// <param name="targetName">The ID of the field (e.g., "RoomCode" or "Username")</param>
        /// <param name="type">The HTML input type ("text", "password", "number")</param>
        [JSImport("focusMobileInput", "mobileInput.js")]
        public static partial void Focus(string targetName, string type = "text");

        [JSImport("blurMobileInput", "mobileInput.js")]
        public static partial void Blur();
        
        [JSImport("isMobileDevice", "mobileInput.js")]
        public static partial bool IsMobile();
        
        [JSImport("initMobileInput", "mobileInput.js")]
        public static partial Task InitMobileInputJs();
    }
}