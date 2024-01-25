var events = new Vue({});

var ResourcesPage = Vue.component('breadcrumb', {
    template: "#breadcrumb-template",
    props: {
        maincategory: { Type: String, default: 'Main Category' },
        category: { Type: String, default: 'Category' },
        product: { Type: String, default: 'Product' },
    },
    methods: {
        init: function (options) {
            //showLoader();
            //this.maincategory = options.maincategory;
            //this.category = options.category;
            //this.product = options.product;
            //events.$emit("loadproducts");
            //hideLoader();
        },
        /*productsLoaded(categoryName, products) {
            this.filteredproducts = this.products = products;
            this.isloading = false;
        },
        loadProducts() {
            this.connectApi('get', 'Products/List/' + this.categoryname, {}, {}, true, function (res) {
                //var albumId = new URL(res.request.responseURL).searchParams.get("albumId");
                for (i = 0; i < res.length; i++) {
                    res[i].ImagePath.FullName = res[i].ImagePath.FullName.replace("~/", vue.baseurl);
                }
                events.$emit('productsloaded', this.categoryname, res);
            }, function (err) { console.log(err); });
        },
        searchProducts: function () {
            this.selectedproductid = 0;
            this.filteredproducts = this.products.filter(s => s.LocalizedName.toLowerCase().indexOf(this.keyword.toLowerCase()) > -1 && (this.subcategoyid === 0 || s.CategoryId === this.subcategoyid));
        },
        categoryProducts: function (catId) {
            return this.products.filter(s => s.CategoryId === catId);
        },
        setSubCategory: function (categoryId) {
            this.selectedproductid = 0;
            this.subcategoyid = categoryId;
            window.scroll({
                top: document.querySelector('#app').offsetTop - 100,
                behavior: 'smooth'
            });
            this.searchProducts();
        },
        selectproduct: function (productId) {
            this.selectedproductid = productId;
            this.selectedproduct = this.products.find(s => s.Id == productId);
            window.scroll({
                top: document.querySelector('#app').offsetTop - 100,
                behavior: 'smooth'
            });
        },
        changeKeyword: function (e) {
            this.selectedproductid = 0;
            this.keyword = e.target.value;
            this.searchProducts();
        },
        connectApi: function (method, api, data, showLoader, success, err) {
            //if (showLoader) {
            //    $('html, body').addClass('overlay-body');
            //    $('#intloader').addClass('loader');
            //}
            switch (method) {
                case 'get':
                    axios.get(vue.baseurl + api)
                        .then(response => {
                            //if (showLoader) {
                            //    $('html, body').removeClass('overlay-body');
                            //    $('#intloader').removeClass('loader');
                            //}
                            return success(response.data);
                        })
                        .catch(error => {
                            return err(error);
                        });
                    break;
                case 'post':
                    axios.post(vue.baseurl + api, data)
                        .then(response => {
                            //if (showLoader) {
                            //    $('html, body').removeClass('overlay-body');
                            //    $('#intloader').removeClass('loader');
                            //}
                            success(response.data);
                        })
                        .catch(error => {
                            return err(error);
                        });
                    break;
            }
        }*/
    },
    created: function () {
        events.$emit('created', this);

        /*events.$on('loadproducts', () => {
            this.loadProducts();
        });

        events.$on('productsloaded', (categoryName, products) => {
            this.productsLoaded(categoryName, products);
        });*/
    }
});