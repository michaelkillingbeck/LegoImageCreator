var pixelateButton = document.getElementById("pixelate-button");
var sourceInputElement = document.getElementById("source-input");

pixelateButton.onclick = function(event) {
    var formData = new FormData();

    var dataString = document.getElementById("source-image").toDataURL("image/jpeg");
    formData.append('sourceFile', dataString);

    disableButtons();

    $.ajax({
        url: '/Home/Process',
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false
    }).done(function(response) {
        enableButtons();
    }).fail(function (jqXHR, response) {
        enableButtons();
        console.log('failed');
    });
}

sourceInputElement.onchange = function(event) {
    var canvas = document.getElementById("source-image");
    loadImage(this, canvas);
}

function disableButtons() {
    pixelateButton.disabled = true;
    pixelateButton.innerHTML = "Processing...";
    
    sourceInputElement.disabled = true;
    document.getElementById("source-span").classList.add("disabled");
}

function enableButtons() {
    pixelateButton.disabled = false;
    pixelateButton.innerHTML = "Pixelate!";
    
    sourceInputElement.disabled = false;
    document.getElementById("source-span").classList.remove("disabled");
}

function loadImage(input, canvas) {
    var file, fr, img;
    
    file = input.files[0];
    fr = new FileReader();
    fr.onload = createImage;
    fr.readAsDataURL(file);

    function createImage() {
        img = new Image();
        img.onload = imageLoaded;
        img.src = fr.result;
    }

    function imageLoaded() {
        canvas.width = img.width;
        canvas.height = img.height;
        var ctx = canvas.getContext("2d");
        ctx.drawImage(img, 0, 0, canvas.width, canvas.height);
    }
}