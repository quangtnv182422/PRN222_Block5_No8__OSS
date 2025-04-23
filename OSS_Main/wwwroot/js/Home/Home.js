
var isCustomer = false;

async function updateCategoryList(data, selectedCategoryId) {
    let contentHtml = ``;
    contentHtml += `
      <li class="nav-item">
                            <button class="d-flex m-2 py-2 border-0 rounded-pill ${selectedCategoryId === null ? 'btn-warning' : 'bg-light'}"
onclick="handleClickCategory(event)"
data-category-id="null">
                                <span class="text-dark" style="width: 130px;">All Products</span>
                            </button>
                        </li>
    `;
    for (const category of data) {
        contentHtml += `
      <li class="nav-item">
                                <button class="d-flex m-2 py-2 border-0 rounded-pill active ${selectedCategoryId === category.categoryId ?  'btn-warning' : 'bg-light'}" onclick="handleClickCategory(event)" data-category-id="${category.categoryId}"
                                    >
                                    <span class="text-dark" style="width: 130px;">${category.name}</span>
                                </button>
                            </li>
        `;
    }
    document.getElementById('navCategoryList').innerHTML = contentHtml
}

async function updateProductList(data, isUserAuthenticated) {
    let contentHtml = ``;
    for (const product of data) {
        contentHtml += `
                                    <div class="col-md-6 col-lg-4 col-xl-3">
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
                                                    <p class=" fs-5 fw-bold mb-0" style="color: red; font-weight: bold;"> ${product.productSpecs[0].salePrice} VND</p>
                                                    <p class="text-dark fs-5 fw-bold mb-0" style="color: gray; text-decoration: line-through;">${product.productSpecs[0].basePrice} VND</p>

                                                    <!--Add to cart-->
                                                   ${isUserAuthenticated && isCustomer ?

                `<btn class="btn border border-secondary rounded-pill px-3 text-primary"
              data-productId="${product.productId}" data-specId="${product.productSpecs[0].productSpecId}"
              onclick="handleClickAddToCart(event)"
                ><i class="fa fa-shopping-bag me-2 text-primary"></i> Add to cart</btn>`

                : !isCustomer ? `<a class="btn border border-secondary rounded-pill px-3 text-danger">
                                                            <i class="fa fa-shopping-bag me-2 text-danger"></i> Only
                                                            customers can add to cart
                                                        </a>` :
                    `<a href="/home/redirectToLoginPage" class="btn border border-secondary rounded-pill px-3 text-danger">
                                                            <i class="fa fa-shopping-bag me-2 text-danger"></i> Please login to add to cart
                                                        </a>`}
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    `;
    }
    document.getElementById('listProductSection').innerHTML = contentHtml;
}

async function handleClickCategory(event) {
    event.preventDefault();
    let categoryId = event.currentTarget.dataset.categoryId;
    try {
        const res = await fetch("/home/info?categoryId=" + parseInt(categoryId), {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
        });
        if (res.ok) {
            const data = await res.json();
            await updateCategoryList(data.categories, data.selectedCategoryId);
            await updateProductList(data.products, data.isUserAuthenticated);

        }
    } catch (error) {
        console.log(error);
    }
}

window.onload = async function () {
    try {
        const res = await fetch("/home/info?categoryId=" + null, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
        });
        if (res.ok) {
            const data = await res.json();
            isCustomer = data.isCustomer;
            await updateCategoryList(data.categories, data.selectedCategoryId);
            await updateProductList(data.products, data.isUserAuthenticated);
        }
    } catch (error) {
        console.log(error);
    }
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