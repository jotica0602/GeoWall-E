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

function clearCanvas(canvasId) {
    var canvas = document.getElementById(canvasId);
    var context = canvas.getContext('2d');
    context.clearRect(0, 0, canvas.width, canvas.height);
}

window.getFileContent = async function (fileInput) {
    const file = fileInput.files[0];
    if (file) {
        const content = await file.text();
        return content;
    }
    return null;
}

window.openPDF = function (pdfUrl) {
    var newWindow = window.open(pdfUrl, "_blank");
    if (!newWindow || newWindow.closed || typeof newWindow.closed == 'undefined') {
        // Bloqueadores de ventanas emergentes pueden interferir
        alert("Por favor, habilite las ventanas emergentes para ver el PDF.");
    }
}

window.updateTextAreaValue = (element, value) => {
    element.value = value;
}

window.syncScroll = (element1, element2) => {
    element1.onscroll = () => {
        element2.scrollTop = element1.scrollTop;
        element2.scrollLeft = element1.scrollLeft;
    };
    element2.onscroll = () => {
        element1.scrollTop = element2.scrollTop;
        element1.scrollLeft = element2.scrollLeft;
    };
}

window.simulateClick = (elementId) => {
    var element = document.getElementById(elementId);
    if (element) element.click();
}
