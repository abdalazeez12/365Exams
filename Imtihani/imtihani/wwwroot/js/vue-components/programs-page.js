var ResourcesPage = Vue.component('programs-page', {
    data: function () {
        return {
            keyword: "",
            subcategoyid: 0,
            categoryname: "",
            assessments: [],
            filteredproducts: [],
            subcategoris: [],
            isloading: true,
            selectedproductid: 0,
            selectedproduct: null,
            systemid: "",
            gradeId:"",
            subjectid: "",
            minprice: "",
            maxprice: "",
            sorting:"",
            pagenumber: 1,
            pagesize: 9,
            pagescount: 0,
        };
    },
    methods: {
        init: function (options) {
            
            if (options.subjectId != "" && options.gradeId !="") {
                this.gradeId = options.gradeId
                this.subjectid = options.subjectId
            }
            this.loadAssessments();

            this.gradeId = "";
            this.subjectid = "";
            //showLoader();
            //this.categoryname = options.categoryName;
            //events.$emit("loadassessments");
            //hideLoader();
        },
        assessmentsLoaded(response) {
            this.filteredassessments = this.assessments = response.result;
            this.pagescount = response.pageCount;
            this.isloading = false;
        },
        loadAssessments() {
            apiservice.loadAssessments(this.systemid, this.gradeId, this.subjectid, this.minprice, this.maxprice, this.pagenumber, this.pagesize, this.keyword, this.sorting, this.assessmentsLoaded);
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
                top: document.querySelector('#app').offsetTop-100,
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
        gotopage: function (pageno) {
            this.pagenumber = pageno;
            this.loadAssessments();
        },
        setsystem: function (systemid) {
            this.systemid = systemid;
            this.pagenumber = 1;
            this.loadAssessments();
        },
        setsubject: function (subjectid) {
            this.subjectid = subjectid;
            this.pagenumber = 1;
            this.loadAssessments();
        },
        setSubjectGrade: function (subjectid, gradeId) {
            this.subjectid = subjectid;
            this.gradeId = gradeId;
            this.pagenumber = 1;
            this.loadAssessments();
        },
        setgrade: function (gradeId) {
            this.gradeId = gradeId;
            this.pagenumber = 1;
            this.loadAssessments();
        },
        sortBY: function (sortby) {
            this.sorting = sortby;
            this.pagenumber = 1;
            this.loadAssessments();
        },
        setpricerange: function (minprice, maxprice) {
            this.minprice = minprice;
            this.maxprice = maxprice;
            this.pagenumber = 1;
            this.loadAssessments();
        },
        changeKeywordAssesment: function (e) {
            this.keyword = e.target.value;
            setTimeout(() => this.loadAssessments(),1000) 
     
         },
        sortByASC: function () {
            this.assessments = this.assessments.sort((a, b) => b.id - a.id);
        },
        sortByHighPrice: function () {
            this.assessments = this.assessments.sort((a, b) => b.price - a.price);
        },
        sortByLowPrice: function () {
            this.assessments = this.assessments.sort((a, b) => a.price - b.price);
        },

    },
    created: function () {
        events.$emit('created', this);
    }, mounted() {
        console.log(`the component is now mounted.`)
    }

});