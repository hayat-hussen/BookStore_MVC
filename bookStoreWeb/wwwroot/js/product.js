$(document).ready(function () {
    fetchProducts();


    function fetchProducts() {
        $.ajax({
            url: "/admin/product/getall",
            method: 'GET',
            success: function (response) {
                console.log("API success", response);

                const books = response?.data?.$values || [];
                const tableBody = $('#tblData tbody');
                tableBody.empty();

                books.forEach(book => {
                    if (book?.title) {
                        const bookRow = `
                        <tr>
                            <td style="width: 20%;">${book.title}</td>
<td style="width: 15%;">${book.isbn}</td>
<td style="width: 15%;">${book.author}</td>
<td style="width: 5%;">$${book.price}</td>
<td style="width: 10%;">${book.categoryName}</td>
<td style="width: 20%;">
    <a href="/admin/product/Upsert/${book.id}" class="btn btn-success">Edit</a>
<a onClick="Delete('/admin/product/Delete/${book.id}')" class="btn btn-danger">Delete</a></td>
                        </tr>`;
                        tableBody.append(bookRow);
                    } else {
                        console.error('Book or category name is undefined');
                    }
                });
                $('#tblData').DataTable();
            },
            error: function (error) {
                console.error('Error fetching data:', error);
            }
        });
    }
});
function Delete(url) {
    Swal.fire({
        title: "Are you sure?", text: "You won't be able to revert this!", icon:
            "warning", showCancelButton: true, confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33", confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed)
        { console.log("Sending DELETE request to:", url);
            $.ajax({
                url: url, type: 'DELETE', success: function (data)
                { console.log("Server response:", data);
                    const tableBody = $('#tblData tbody');
                    window.location.reload();
                    toastr.success(data.message);
                }, error: function (xhr, status, error) {
                    console.error("Error deleting data:", error);
                    toastr.error("Failed to delete data.");
                }
            });
        }
    });
}