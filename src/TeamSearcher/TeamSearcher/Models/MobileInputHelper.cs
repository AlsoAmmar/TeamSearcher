// using System;
// using System.Runtime.InteropServices.JavaScript;
// using System.Runtime.Versioning;
// using System.Threading.Tasks;
// using Avalonia.Controls;
// using Avalonia.Interactivity;
//
// namespace TeamSearcher.Models;
//
// [SupportedOSPlatform("browser")]
// public partial class MobileInputHelper : IDisposable
// {
//     private TextBox? _activeTextBox;
//     private static MobileInputHelper? _instance;
//     
//     [JSImport("initMobileInput", "mobileInput")] private static partial Task InitMobileInputJs();
//
//     [JSImport("focusMobileInput", "mobileInput")] private static partial void FocusMobileInputJs();
//
//     [JSImport("blurMobileInput", "mobileInput")] private static partial void BlurMobileInputJs();
//
//     public static async Task<MobileInputHelper> InitializeAsync()
//     {
//         await JSHost.ImportAsync("mobileInput", "./mobileInput.js");
//         var instance = new MobileInputHelper();
//         _instance = instance;
//         await InitMobileInputJs();
//         return instance;
//     }
//
//     public void AttachToTextBox(TextBox textBox)
//     {
//         DetachCurrent();
//         _activeTextBox = textBox;
//         _activeTextBox.GotFocus += OnTextBoxGotFocus;
//         _activeTextBox.LostFocus += OnTextBoxLostFocus;
//     }
//
//     private void DetachCurrent()
//     {
//         if (_activeTextBox is null) return;
//         _activeTextBox.GotFocus -= OnTextBoxGotFocus;
//         _activeTextBox.LostFocus -= OnTextBoxLostFocus;
//         _activeTextBox = null;
//     }
//
//     private void OnTextBoxGotFocus(object? sender, GotFocusEventArgs e)
//     {
//         FocusMobileInputJs();
//     }
//
//     private void OnTextBoxLostFocus(object? sender, RoutedEventArgs e)
//     {
//         BlurMobileInputJs();
//     }
//
//     // Called from JS
//     [JSExport]
//     public static void OnTextReceived(string text)
//     {
//         if (_instance?._activeTextBox is null) return;
//         var tb = _instance._activeTextBox;
//         var caret = tb.CaretIndex;
//         tb.Text = tb.Text is null
//             ? text
//             : tb.Text.Insert(caret, text);
//         tb.CaretIndex = caret + text.Length;
//     }
//
//     [JSExport]
//     public static void OnBackspace()
//     {
//         if (_instance?._activeTextBox is null) return;
//         var tb = _instance._activeTextBox;
//         if (tb.Text is null || tb.Text.Length == 0 || tb.CaretIndex == 0) return;
//         var caret = tb.CaretIndex;
//         tb.Text = tb.Text.Remove(caret - 1, 1);
//         tb.CaretIndex = caret - 1;
//     }
//
//     [JSExport]
//     public static void OnEnter()
//     {
//         if (_instance?._activeTextBox is null) return;
//         if (_instance._activeTextBox.AcceptsReturn)
//         {
//             OnTextReceived("\n");
//         }
//     }
//
//     public void Dispose()
//     {
//         DetachCurrent();
//         _instance = null;
//     }
// }

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