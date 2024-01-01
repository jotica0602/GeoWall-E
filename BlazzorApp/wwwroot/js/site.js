function saveAsFile(filename, data) {
    var blob = new Blob([data], { type: 'text/plain' });
    var url = window.URL.createObjectURL(blob);
    var anchor = document.createElement('a');
    anchor.href = url;
    anchor.download = filename;

    document.body.appendChild(anchor); // Necesario para Firefox
    anchor.click();
    window.URL.revokeObjectURL(url);
    document.body.removeChild(anchor);
}
