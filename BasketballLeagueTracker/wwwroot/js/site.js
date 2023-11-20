// Function displays input photo after user choose one
function handleFileInputChange(input,fileName) {
    var file = input.files[0];
    if (file) {
        var reader = new FileReader();
        reader.onload = function (e) {
            document.getElementById(fileName).src = e.target.result;
        };
        reader.readAsDataURL(file);
    } else {
        document.getElementById(fileName).src = '#';
    }
}