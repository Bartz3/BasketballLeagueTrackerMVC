// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

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