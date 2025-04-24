var filterProduct = {
    Page: 1,
    PageSize: 10,
    SearchString: "",
    Status: ""
}

async function updatePagination(page, totalPage) {
    let contentHtml = ``;
    contentHtml += `<div class="pagination d-flex justify-content-center mt-5">
            <li class="page-item">
                <span class="page-link" data-value="1" onclick="handleClickPage(event)">Start</span>
            </li>`;

    contentHtml += `<li class="page-item">
            <span class="page-link" data-value="` + (page > 1 ? page - 1 : 1) + `" onclick="handleClickPage(event)">Previous</span>
        </li>`;

    for (let i = 1; i <= totalPage; i++) {
        if (i === 1 || i === totalPage || (i >= page - 2 && i <= page + 2)) {
            contentHtml += `<li class="page-item ` + (i === page ? 'active' : '') + `">
                    <span class="page-link" data-value="` + i + `" onclick="handleClickPage(event)">` + i + `</span>
                </li>`;
        } else if (i === page - 3 || i === page + 3) {
            contentHtml += `<li class="page-item disabled">
                    <span class="page-link">...</span>
                </li>`;
        }
    }

    contentHtml += `<li class="page-item">
            <span class="page-link" data-value="` + (page < totalPage ? page + 1 : totalPage) + `" onclick="handleClickPage(event)">Next</span>
        </li>`;

    contentHtml += `<li class="page-item">
            <span class="page-link" data-value="` + totalPage + `" onclick="handleClickPage(event)">End</span>
        </li>`;

    contentHtml += `</div>`;
    document.getElementById('paginationSection').innerHTML = contentHtml
}

async function updateProductList(data, isUserAuthenticated) {
    let contentHtml = ``;
    for (const product of data) {
        contentHtml += `
                                    <tr style="height: 50px">
                                        <td class="p-3">${product.productId}</td>
                                        <td class="p-3">${product.productName}</td>
                                        <td class="p-3 overflow-y-auto">${product.description}</td>
                                        <td class="p-3">${product.createdAt}</td>

                                        <td class="p-3 status-cell">
                                            <span class="badge ${product.productStatus ? "bg-success" : "bg-danger"}">
                                                ${product.productStatus ? "Published" : "Not Published"}
                                            </span>
                                        </td>


                                        <td class="p-3 text-end">
                                        <div class="d-flex">
                                           <button class="btn btn-link p-0 me-2 view-product" title="View"
                                           onclick="window.location.href='/adminProduct/getProduct?id=${product.productId}'"
                                           >
    <i class="bi bi-eye" style="font-size: 1rem"></i>
</button>

<button class="btn btn-link p-0 me-2 edit-product" onclick="window.location.href='/adminProduct/updateProduct?id=${product.productId}'" title="Edit">
    <i class="bi bi-pencil" style="font-size: 1rem; color: #007bff"></i>
</button>

${isUserAuthenticated && product.productStatus ? `
    <button class="btn btn-link p-0 text-danger delete-product" data-id="${product.productId}" onclick="handleClickDelete(event)" title="Delete">
       <i class="bi bi-trash" style="font-size: 1rem"></i>
    </button>` : isUserAuthenticated && !product.productStatus ? `
    <button class="btn btn-link p-0 text-danger delete-product" data-id="${product.productId}" onclick="handleClickActivate(event)" title="Delete">
       <i class="bi bi-unlock" style="font-size: 1rem"></i>
    </button>` : ""
            }
                                        </div>

                                        </td>
                                        <td></td>
                                    </tr>
        `;
    }
    document.getElementById('productTableSection').innerHTML = contentHtml
}



async function fetchData() {
    try {
        const res = await fetch(`/adminProduct/searchProduct?searchProductRequest=${encodeURIComponent(JSON.stringify(filterProduct))}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        });
        if (res.ok) {
            let data = await res.json();
            await updateProductList(data.products, data.isUserAuthenticated);
            await updatePagination(filterProduct.Page, data.totalPage);
        }
    } catch (error) {
        console.log(error);
    }
}

async function handleClickPage(event) {
    event.preventDefault();
    const pageValue = event.target.getAttribute('data-value');
    filterProduct.Page = parseInt(pageValue);
    await fetchData();
}
function handleClickDelete(event) {
    event.preventDefault();
    let confirmDelete = confirm("Are you sure you want to delete this product?");
    if (!confirmDelete) return;
    const button = event.target.closest('.delete-product');
    const productId = button?.getAttribute('data-id');

    if (productId) {
        window.location.href = `/adminProduct/deleteProduct?id=${productId}`;
    } else {
        alert("Could not find product ID.");
    }
}

function handleClickActivate(event) {
    event.preventDefault();
    let confirmDelete = confirm("Are you sure you want to activate this product?");
    if (!confirmDelete) return;
    const button = event.target.closest('.delete-product');
    const productId = button?.getAttribute('data-id');

    if (productId) {
        window.location.href = `/adminProduct/activateProduct?id=${productId}`;
    } else {
        alert("Could not find product ID.");
    }
}


window.onload = async function () {
    filterProduct.Status = document.getElementById('statusFilter').value
    await fetchData();
}

document.getElementById('statusFilter').addEventListener('change', async function () {
    filterProduct.Status = this.value;
    await fetchData();
});

document.getElementById('searchIcon').addEventListener('click', async function () {
    filterProduct.SearchString = document.getElementById('searchInput').value
    await fetchData();
});