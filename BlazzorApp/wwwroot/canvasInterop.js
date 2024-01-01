function drawPoint(canvasId, x, y, color, radius) {
    var canvas = document.getElementById(canvasId);
    var ctx = canvas.getContext("2d");

    ctx.fillStyle = color;

    ctx.beginPath();
    ctx.arc(x, y, radius, 0, 2 * Math.PI);
    ctx.fill();
}

function drawLine(canvasId, startX, startY, endX, endY, color) {
    var canvas = document.getElementById(canvasId);
    var ctx = canvas.getContext("2d");

    ctx.strokeStyle = color;

    ctx.beginPath();
    ctx.moveTo(startX, startY);
    ctx.lineTo(endX, endY);
    ctx.stroke();
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

function drawCircle(canvasId, centerX, centerY, radius, color) {
    var canvas = document.getElementById(canvasId);
    var ctx = canvas.getContext("2d");

    ctx.fillStyle = color;

    ctx.beginPath();
    ctx.arc(centerX, centerY, radius, 0, 2 * Math.PI);
    ctx.fill();
}

function drawLineThroughPoints(canvasId, point1X, point1Y, point2X, point2Y, color, lineWidth) {
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

function drawArcBetweenPoints(canvasId, centerX, centerY, pointBX, pointBY, pointCX, pointCY, radius, color, lineWidth) {
    var canvas = document.getElementById(canvasId);
    var ctx = canvas.getContext("2d");

    ctx.strokeStyle = color;
    ctx.lineWidth = lineWidth;

    // Calcular los ángulos correspondientes a los puntos B y C con respecto al punto A (centro)
    var angleB = Math.atan2(pointBY - centerY, pointBX - centerX);
    var angleC = Math.atan2(pointCY - centerY, pointCX - centerX);

    // Dibujar el arco
    ctx.beginPath();
    ctx.arc(centerX, centerY, radius, angleB, angleC);
    ctx.stroke();
}

function drawArcBetweenPoints2(canvasId, centerX, centerY, pointBX, pointBY, pointCX, pointCY, radius, color, lineWidth) {
    var canvas = document.getElementById(canvasId);
    var ctx = canvas.getContext("2d");

    ctx.strokeStyle = color;
    ctx.lineWidth = lineWidth;

    // Calcular los ángulos correspondientes a los puntos B y C con respecto al punto A (centro)
    var angleB = Math.atan2(pointBY - centerY, pointBX - centerX);
    var angleC = Math.atan2(pointCY - centerY, pointCX - centerX);

    // Asegurarse de que los ángulos estén en el rango correcto (de B a C en sentido horario)
    if (angleB > angleC) {
        var temp = angleB;
        angleB = angleC;
        angleC = temp;
    }

    // Dibujar el arco
    ctx.beginPath();
    ctx.arc(centerX, centerY, radius, angleB, angleC);
    ctx.stroke();
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