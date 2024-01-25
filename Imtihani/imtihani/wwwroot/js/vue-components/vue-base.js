var vue = new Vue({
    el: '#app',
    data: {
        baseurl: "",
        apiurl: "",
        options: {},
        components: [],
        isLoading: true,
        isLoaded: false
    },
    methods: {
        init(options) {
            this.baseurl = options.baseurl;
            this.apiurl = options.apiurl;
            this.options = options;
            this.components.forEach(function (item, index) {
                item.init(options);
            });
        },
        handlecreate(child) {
            this.components.push(child);
        },
        showLoader() {
            showLoader();
        },
        hideLoader() {
            hideLoader();
        }
    },
    beforeCreate() {
        events.$on('created', (child) => {
            this.components.push(child);
        });
    }
});