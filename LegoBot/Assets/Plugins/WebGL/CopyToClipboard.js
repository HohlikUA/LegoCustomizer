mergeInto(LibraryManager.library, {
    CopyToClipboard: function (strPtr) {
        const text = UTF8ToString(strPtr);
        if (navigator.clipboard) {
            navigator.clipboard.writeText(text).then(function () {
                console.log("Copied to clipboard!");
            }).catch(function (err) {
                console.error("Clipboard copy failed:", err);
                alert("Не вдалося скопіювати текст. Спробуйте вручну.");
            });
        } else {
            alert("Clipboard API not supported in this browser.");
        }
    }
});