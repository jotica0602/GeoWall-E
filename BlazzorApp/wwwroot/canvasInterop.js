
function drawPoint(canvasId, x, y, color, radius) {
    var canvas = document.getElementById(canvasId);
    var ctx = canvas.getContext("2d");

    ctx.fillStyle = color;

    ctx.beginPath();
    ctx.arc(x, y, radius, 0, 2 * Math.PI);
    ctx.fill();
}

//this is a segment
function drawLine(canvasId, startX, startY, endX, endY, color) {
    var canvas = document.getElementById(canvasId);
    var ctx = canvas.getContext("2d");

    ctx.strokeStyle = color;

    ctx.beginPath();
    ctx.moveTo(startX, startY);
    ctx.lineTo(endX, endY);
    ctx.stroke();
}

function drawLabeledSegment(canvasId, startX, startY, endX, endY, color) {
    var canvas = document.getElementById(canvasId);
    var ctx = canvas.getContext("2d");

    ctx.strokeStyle = color;

    // Dibujar la línea
    ctx.beginPath();
    ctx.moveTo(startX, startY);
    ctx.lineTo(endX, endY);
    ctx.stroke();

    // Calcular el punto medio de la línea
    var midPointX = (startX + endX) / 2;
    var midPointY = (startY + endY) / 2;

    // Etiquetar el punto medio
    ctx.fillStyle = "black";
    ctx.font = "12px Arial";
    ctx.textAlign = "center";
    ctx.fillText(label, midPointX, midPointY);
}


function drawCircleOutline(canvasId, centerX, centerY, radius, color, lineWidth) {
    var canvas = document.getElementById(canvasId);
    var ctx = canvas.getContext("2d");

    ctx.strokeStyle = color;
    ctx.lineWidth = lineWidth;

    ctx.beginPath();
    ctx.arc(centerX, centerY, radius, 0, 2 * Math.PI);
    // ctx.arc(centerX, centerY, radius, 0, Math.PI);
    ctx.stroke();
}

function drawLabeledCircleOutline(canvasId, centerX, centerY, radius, label, color, lineWidth) {
    var canvas = document.getElementById(canvasId);
    var ctx = canvas.getContext("2d");

    ctx.strokeStyle = color;
    ctx.lineWidth = lineWidth;

    // Dibujar el círculo con contorno
    ctx.beginPath();
    ctx.arc(centerX, centerY, radius, 0, 2 * Math.PI);
    ctx.stroke();

    // Etiquetar el círculo
    ctx.fillStyle = "black";
    ctx.font = "12px Arial";
    ctx.textAlign = "center";
    ctx.fillText(label, centerX, centerY);
}

function drawCircle(canvasId, centerX, centerY, radius, color) {
    var canvas = document.getElementById(canvasId);
    var ctx = canvas.getContext("2d");

    ctx.fillStyle = color;

    ctx.beginPath();
    ctx.arc(centerX, centerY, radius, 0, 2 * Math.PI);
    ctx.fill();
}

function drawLabeledCircle(canvasId, centerX, centerY, radius, label, color) {
    var canvas = document.getElementById(canvasId);
    var ctx = canvas.getContext("2d");

    ctx.fillStyle = color;

    // Dibujar el círculo
    ctx.beginPath();
    ctx.arc(centerX, centerY, radius, 0, 2 * Math.PI);
    ctx.fill();

    // Etiquetar el círculo
    ctx.fillStyle = "black";
    ctx.font = "12px Arial";
    ctx.textAlign = "center";
    ctx.fillText(label, centerX, centerY);
}

// asdasdasd

function drawLineThroughPoints(canvasId, point1X, point1Y, point2X, point2Y, color, lineWidth) {
    // Get the canvas element
    var canvas = document.getElementById(canvasId);
    if (canvas.getContext) {
        // Get the 2d drawing context
        var ctx = canvas.getContext('2d');

        // Set the line color and width
        ctx.strokeStyle = color;
        ctx.lineWidth = lineWidth;

        // Calculate the slope of the line
        var slope = (point2Y - point1Y) / (point2X - point1X);

        // Calculate the y-intercept of the line
        var yIntercept = point1Y - slope * point1X;

        // Calculate the start and end points of the line
        var startX = 0;
        var startY = yIntercept;
        var endX = canvas.width;
        var endY = slope * endX + yIntercept;

        // Draw the line
        ctx.beginPath();
        ctx.moveTo(startX, startY);
        ctx.lineTo(endX, endY);
        ctx.stroke();
    } else {
        console.log('Canvas not supported');
    }
}
// this is line

// function drawLineThroughPoints(canvasId, point1X, point1Y, point2X, point2Y, color, lineWidth) {
//     var canvas = document.getElementById(canvasId);
//     var ctx = canvas.getContext("2d");

//     ctx.strokeStyle = color;
//     ctx.lineWidth = lineWidth;
    
//     // Calcular la pendiente y la intersección y de la recta que pasa por los dos puntos dados
//     var slope = (point2Y - point1Y) / (point2X - point1X);

//     var interceptY = point1Y - slope * point1X;

//     // Calcular las intersecciones con los bordes del canvas
//     var intersectionTop = { x: -interceptY / slope, y: 0 };
//     var intersectionBottom = { x: (canvas.height - interceptY) / slope, y: canvas.height };
//     var intersectionLeft = { x: 0, y: interceptY };
//     var intersectionRight = { x: canvas.width, y: slope * canvas.width + interceptY };

//     // Determinar los puntos de inicio y fin de la línea dentro del canvas
//     var start, end;

//     if (isInsideCanvas(intersectionTop, canvas.width, canvas.height)) {
//         start = intersectionTop;
//     } else if (isInsideCanvas(intersectionLeft, canvas.width, canvas.height)) {
//         start = intersectionLeft;
//     } else if (isInsideCanvas(intersectionBottom, canvas.width, canvas.height)) {
//         start = intersectionBottom;
//     } else {
//         start = intersectionRight;
//     }

//     if (isInsideCanvas(intersectionBottom, canvas.width, canvas.height)) {
//         end = intersectionBottom;
//     } else if (isInsideCanvas(intersectionRight, canvas.width, canvas.height)) {
//         end = intersectionRight;
//     } else if (isInsideCanvas(intersectionTop, canvas.width, canvas.height)) {
//         end = intersectionTop;
//     } else {
//         end = intersectionLeft;
//     }

//     // Dibujar la línea
//     ctx.beginPath();
//     ctx.moveTo(start.x, start.y);
//     ctx.lineTo(end.x, end.y);
//     ctx.stroke();
// }

function drawLabeledLine(canvasId, point1X, point1Y, point2X, point2Y, label, color, lineWidth) {
    var canvas = document.getElementById(canvasId);
    var ctx = canvas.getContext("2d");

    ctx.strokeStyle = color;
    ctx.lineWidth = lineWidth;

    // Calcular la pendiente y la intersección y de la recta que pasa por los dos puntos dados
    var slope = (point2Y - point1Y) / (point2X - point1X);
    var interceptY = point1Y - slope * point1X;

    // Calcular las intersecciones con los bordes del canvas
    var intersectionTop = { x: -interceptY / slope, y: 0 };
    var intersectionBottom = { x: (canvas.height - interceptY) / slope, y: canvas.height };
    var intersectionLeft = { x: 0, y: interceptY };
    var intersectionRight = { x: canvas.width, y: slope * canvas.width + interceptY };

    // Determinar los puntos de inicio y fin de la línea dentro del canvas
    var start, end;

    if (isInsideCanvas(intersectionTop, canvas.width, canvas.height)) {
        start = intersectionTop;
    } else if (isInsideCanvas(intersectionLeft, canvas.width, canvas.height)) {
        start = intersectionLeft;
    } else if (isInsideCanvas(intersectionBottom, canvas.width, canvas.height)) {
        start = intersectionBottom;
    } else {
        start = intersectionRight;
    }

    if (isInsideCanvas(intersectionBottom, canvas.width, canvas.height)) {
        end = intersectionBottom;
    } else if (isInsideCanvas(intersectionRight, canvas.width, canvas.height)) {
        end = intersectionRight;
    } else if (isInsideCanvas(intersectionTop, canvas.width, canvas.height)) {
        end = intersectionTop;
    } else {
        end = intersectionLeft;
    }

    // Dibujar la línea
    ctx.beginPath();
    ctx.moveTo(start.x, start.y);
    ctx.lineTo(end.x, end.y);
    ctx.stroke();

    // Etiquetar el punto medio
    ctx.fillStyle = "black";
    ctx.font = "12px Arial";
    ctx.textAlign = "center";
    ctx.fillText(label, midPointX, midPointY);
}

function drawRayThroughPoints(canvasId, point1X, point1Y, point2X, point2Y, color, lineWidth) {
    var canvas = document.getElementById(canvasId);
    var ctx = canvas.getContext("2d");

    ctx.strokeStyle = color;
    ctx.lineWidth = lineWidth;

    // Calcular la pendiente y la intersección y de la semirrecta que pasa por los dos puntos dados
    var slope = (point2Y - point1Y) / (point2X - point1X);
    var interceptY = point1Y - slope * point1X;

    // Calcular la intersección con el borde derecho del canvas
    var intersectionRight = { x: canvas.width, y: slope * canvas.width + interceptY };

    // Determinar el punto de inicio y el punto final de la semirrecta dentro del canvas
    var start = { x: point1X, y: point1Y };
    var end;

    if (isInsideCanvas(intersectionRight, canvas.width, canvas.height)) {
        end = intersectionRight;
    } else {
        end = { x: canvas.width, y: slope * canvas.width + interceptY };
    }

    // Dibujar la semirrecta
    ctx.beginPath();
    ctx.moveTo(start.x, start.y);
    ctx.lineTo(end.x, end.y);
    ctx.stroke();
}

function drawLabeledRay(canvasId, point1X, point1Y, point2X, point2Y, label, color, lineWidth) {
    var canvas = document.getElementById(canvasId);
    var ctx = canvas.getContext("2d");

    ctx.strokeStyle = color;
    ctx.lineWidth = lineWidth;

    // Calcular la pendiente y la intersección y de la semirrecta que pasa por los dos puntos dados
    var slope = (point2Y - point1Y) / (point2X - point1X);
    var interceptY = point1Y - slope * point1X;

    // Calcular la intersección con el borde derecho del canvas
    var intersectionRight = { x: canvas.width, y: slope * canvas.width + interceptY };

    // Determinar el punto de inicio y el punto final de la semirrecta dentro del canvas
    var start = { x: point1X, y: point1Y };
    var end;

    if (isInsideCanvas(intersectionRight, canvas.width, canvas.height)) {
        end = intersectionRight;
    } else {
        end = { x: canvas.width, y: slope * canvas.width + interceptY };
    }

    // Dibujar la semirrecta
    ctx.beginPath();
    ctx.moveTo(start.x, start.y);
    ctx.lineTo(end.x, end.y);
    ctx.stroke();

    // Etiquetar el punto de inicio
    ctx.fillStyle = "black";
    ctx.font = "12px Arial";
    ctx.textAlign = "start";
    ctx.fillText(label, start.x + 5, start.y - 5);
}

function drawArcBetweenPoints(canvasId, centerX, centerY, pointBX, pointBY, pointCX, pointCY, radius, color, lineWidth) {
    var canvas = document.getElementById(canvasId);
    var ctx = canvas.getContext("2d");

    ctx.strokeStyle = color;
    ctx.lineWidth = lineWidth;

    // Calcular los ángulos correspondientes a los puntos B y C con respecto al punto A (centro)
    var angleB = Math.atan2(pointBY - centerY, pointBX - centerX);
    var angleC = Math.atan2(pointCY - centerY, pointCX - centerX);
    
    // Asegurarse de que los ángulos estén en el rango correcto (de B a C en sentido horario)
    if (angleB > angleC) 
    {
        angleC+=Math.PI*2;
    }

    // Dibujar el arco
    ctx.beginPath();
    ctx.arc(centerX, centerY, radius, angleB, angleC);
    ctx.stroke();
}

function drawLabeledArc(canvasId, centerX, centerY, pointBX, pointBY, pointCX, pointCY, radius, label, color, lineWidth) {
    var canvas = document.getElementById(canvasId);
    var ctx = canvas.getContext("2d");

    ctx.strokeStyle = color;
    ctx.lineWidth = lineWidth;

    // Calcular los ángulos correspondientes a los puntos B y C con respecto al punto A (centro)
    var angleB = Math.atan2(pointBY - centerY, pointBX - centerX);
    var angleC = Math.atan2(pointCY - centerY, pointCX - centerX);

    // Asegurarse de que los ángulos estén en el rango correcto (de B a C en sentido horario)
    if (angleB > angleC) 
    {
        angleC+=Math.PI*2;
    }

    // Dibujar el arco
    ctx.beginPath();
    ctx.arc(centerX, centerY, radius, angleB, angleC);
    ctx.stroke();
    
    // Etiquetar el arco
    ctx.fillStyle = "black";
    ctx.font = "12px Arial";
    ctx.textAlign = "center";
    ctx.fillText(label, centerX, centerY - radius - 5);
}

function isInsideCanvas(point, canvasWidth, canvasHeight) {
    return point.x >= 0 && point.x <= canvasWidth && point.y >= 0 && point.y <= canvasHeight;
}

function drawLabeledPoint(canvasId, x, y, label, color, radius) {
    var canvas = document.getElementById(canvasId);
    var ctx = canvas.getContext("2d");

    ctx.fillStyle = color;

    // Dibujar el punto
    ctx.beginPath();
    ctx.arc(x, y, radius, 0, 2 * Math.PI);
    ctx.fill();

    // Etiquetar el punto
    ctx.fillStyle = "black";
    ctx.font = "12px Arial";
    ctx.textAlign = "start";
    ctx.fillText(label, x + radius + 5, y);
}

function resizeCanvas(canvasId) {
    var canvas = document.getElementById(canvasId);
    canvas.width = canvas.offsetWidth;
    canvas.height = canvas.offsetHeight;
}