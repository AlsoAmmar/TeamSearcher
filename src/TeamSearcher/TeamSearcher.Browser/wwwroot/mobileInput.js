let hiddenInput = null;
let exports = null;
let currentTarget = ""; // Tracks which TextBox is active

export function isMobileDevice() {
    return /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)
        || (navigator.maxTouchPoints > 0);
}

export async function initMobileInput() {
    const exports_all = await globalThis.getDotnetRuntime(0).getAssemblyExports("TeamSearcher.dll");
    console.log("Available Exports:", exports_all);
    exports = exports_all.TeamSearcher.Models.MobileInputHelper;

    hiddenInput = document.createElement('input');
    hiddenInput.type = 'text';
    hiddenInput.style.position = 'fixed';
    hiddenInput.style.opacity = '0';
    hiddenInput.style.pointerEvents = 'none';
    hiddenInput.style.width = '1px';
    hiddenInput.style.height = '1px';
    hiddenInput.style.top = '0';
    hiddenInput.style.left = '0';
    hiddenInput.style.zIndex = '-1';

    hiddenInput.setAttribute('autocomplete', 'off');
    hiddenInput.setAttribute('autocorrect', 'off');
    hiddenInput.setAttribute('autocapitalize', 'off');
    hiddenInput.setAttribute('spellcheck', 'false');
    document.body.appendChild(hiddenInput);

    hiddenInput.addEventListener('compositionend', (e) => {
        if (e.data && currentTarget) {
            exports.OnTextReceived(currentTarget, e.data);
        }
        hiddenInput.value = '';
    });

    // Handles standard typing
    hiddenInput.addEventListener('input', (e) => {
        if (!e.isComposing && e.data && currentTarget) {
            exports.OnTextReceived(currentTarget, e.data);
            hiddenInput.value = '';
        }
    });

    // Handles control keys
    hiddenInput.addEventListener('keydown', (e) => {
        if (!currentTarget) return;

        if (e.key === 'Backspace') {
            exports.OnBackspace(currentTarget);
        } else if (e.key === 'Enter') {
            exports.OnEnter(currentTarget);
        }
    });
}

/**
 * @param {string} target - The name of the field (e.g., "UserName")
 * @param {string} type - HTML input type (e.g., "text" or "password")
 */
export function focusMobileInput(target, type = "text") {
    if (hiddenInput) {
        currentTarget = target;
        hiddenInput.type = type;
        hiddenInput.value = '';
        hiddenInput.focus();
    }
}

export function blurMobileInput() {
    if (hiddenInput) {
        hiddenInput.blur();
        currentTarget = "";
    }
}