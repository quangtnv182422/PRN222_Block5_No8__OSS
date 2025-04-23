var searchProductForm = {
    Page: 1,
    PageSize: 6,
    SortingCategory: "price",
    SearchString: "",
    CategoryId: 0
}
var isCustomer = false;

async function updateProductList(data, isUserAuthenticated) {
    let contentHtml = ``;
    console.log("Data:", data);
    for (const product of data) {
        contentHtml += `
                                    <div class="col-12 col-sm-6 col-md-4 mb-4">
    <div class="rounded position-relative fruite-item">
        <div class="fruite-img">
            <a href="home/redirectToProductDetails?productId=${product.productId}">
                <img src="${product.productImages != null && product.productImages.length > 0 ? product.productImages[0].imageUrl : "/assets/img/default-product.jpg"}"
                     class="img-fluid w-100 rounded-top" alt="${product.productName}">
            </a>
        </div>
        <div class="p-4 border border-secondary border-top-0 rounded-bottom">
            <h4>${product.productName}</h4>
            <div class="d-flex justify-content-between flex-lg-wrap">
                <p class="fs-5 fw-bold mb-0" style="color: red;">${product.productSpecs[0].salePrice} VND</p>
                <p class="fs-5 fw-bold mb-0 text-decoration-line-through text-muted">${product.productSpecs[0].basePrice} VND</p>

                                                    <!--Add to cart-->
                                                    ${isUserAuthenticated ? (
                isCustomer ? `
        <btn class="btn border border-secondary rounded-pill px-3 text-primary"
             data-productId="${product.productId}" 
             data-specId="${product.productSpecs[0].productSpecId}"
             onclick="handleClickAddToCart(event)">
            <i class="fa fa-shopping-bag me-2 text-primary"></i> Add to cart
        </btn>`
                    : `
        <a class="btn border border-secondary rounded-pill px-3 text-danger">
            <i class="fa fa-shopping-bag me-2 text-danger"></i> Only customers can add to cart
        </a>`
            ) : `
    <a href="/home/redirectToLoginPage" class="btn border border-secondary rounded-pill px-3 text-danger">
        <i class="fa fa-shopping-bag me-2 text-danger"></i> Please login to add to cart
    </a>`
}

            </div>
        </div>
    </div>
</div>

                                    `;
    }
    document.getElementById('productListSection').innerHTML = contentHtml;
}

async function updateCategoryList(data, selectedCategoryId) {
    let contentHtml = ``;
    contentHtml += `
      <li class="nav-item">
                            <button class="d-flex justify-content-between fruite-name border-0 bg-transparent " value="0"
                            onclick="handleClickCategory(event)"
                            >
                                 <i class="fas fa-apple-alt me-2 ${selectedCategoryId === 0 ? 'active' : ''}"></i>Tất cả
                            </button>
                        </li>
    `;
    for (const category of data) {
        contentHtml += `
      <li class="nav-item">
                                <button class="d-flex justify-content-between fruite-name border-0 bg-transparent " value="${category.categoryId}"
                                 onclick="handleClickCategory(event)"
                                >
                                    <i class="fas fa-apple-alt me-2 pt-2 ${selectedCategoryId === category.categoryId ? 'active' : ''}"></i>${category.name}
                                </button>
                            </li>
        `;
    }
    document.getElementById('categorySection').innerHTML = contentHtml
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

async function fetchData() {
    try {
        const res = await fetch(`/products/search?searchProduct=${encodeURIComponent(JSON.stringify(searchProductForm))}`);
        if (res.ok) {
            let data = await res.json();
            console.log(data);
            isCustomer = data.isCustomer;
            console.log(isCustomer);
            await updateProductList(data.products, data.isUserAuthenticated);
            await updateCategoryList(data.categories, searchProductForm.CategoryId);
            await updatePagination(searchProductForm.Page, Math.ceil(data.total / searchProductForm.PageSize));
        }
    } catch (error) {
        console.log(error);
    }
}

window.onload = async function () {
    await fetchData();
}

function handleClickAddToCart(event) {
    event.preventDefault();
    let productId = event.target.getAttribute('data-productId');
    let specId = event.target.getAttribute('data-specId');
    if (!isCustomer) {
        alert('This function is only available for customers');
        return;
    }
    window.location.href = '/home/redirectToProductDetails?productId=' + productId + '&specId=' + specId;
}

async function handleClickCategory(event) {
    event.preventDefault();
    let categoryId = parseInt(event.target.value);
    searchProductForm.CategoryId = categoryId;
    await fetchData();
}

document.getElementById("sortSelection").addEventListener('change', async function () {
    searchProductForm.SortingCategory = this.value;
    await fetchData();
});

async function handleClickPage(event) {
    event.preventDefault();
    const pageValue = event.target.getAttribute('data-value');
    searchProductForm.Page = parseInt(pageValue);
    await fetchData();
}

document.getElementById('search-icon-1').addEventListener('click', async function () {
    searchProductForm.SearchString = document.getElementById('searchInput').value;
    await fetchData();
})