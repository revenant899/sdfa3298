// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function deleteCategory(data) {
    const category = JSON.parse(data);
    const message = document.getElementById("modalDeleteMessage");
    const link = document.getElementById("modalDeleteUrl");
    const title = document.getElementById("modalTitle");
    title.innerText = "Видалити категорію";
    message.innerText = `Ви дійсно хочете видалити категорію "${category.Name}"?`;
    link.href = "/Category/Delete/" + category.Id;
}
function deleteProduct(data) {
    const product = JSON.parse(data);
    const message = document.getElementById("modalDeleteMessage");
    const link = document.getElementById("modalDeleteUrl");
    const title = document.getElementById("modalTitle");
    title.innerText = "Видалити товар";
    message.innerText = `Ви дійсно хочете видалити товар "${product.Name}"?`;
    link.href = "/Product/Delete/" + product.Id;
}

document.addEventListener("DOMContentLoaded", function () {
    const total = document.getElementById("total");
    const subtotal = document.getElementById("subtotal");
    total.innerText = subtotal.innerText;
});
