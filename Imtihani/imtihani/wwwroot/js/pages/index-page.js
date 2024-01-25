var indexPage = Vue.component('index-page', {
    data: function () {
        return {
            countries: [],
            grades: [],
            subjects: [],
            assessments: [],
            isloading: true,
            systemid: "",
            subjectid: "",
            minprice: "",
            maxprice: "",
            pagenumber: 1,
            pagesize: 20,
            pagescount: 0,
            categoryid: ''
        };
    },
    methods: {
        init: function (options) {
            this.loadCountries();
            this.loadSubjects();
            this.loadGrades();
            //this.loadAssessments();
        },
        assessmentsLoaded(response) {
            this.filteredassessments = this.assessments = response.result;
            this.pagescount = response.pageCount;
            this.isloading = false;
        },
        loadAssessments() {
            apiservice.loadAssessments(this.systemid, this.subjectid, this.minprice, this.maxprice, this.pagenumber, this.pagesize, this.assessmentsLoaded);
        },
        loadGrades() {
            apiservice.loadGrades("", this.pagenumber, this.pagesize, this.gradesLoaded);
        },
        gradesLoaded(response) {
            this.grades = response.result;
            this.isloading = false;

        },
        loadCountries() {
            apiservice.loadCountries(this.pagenumber, this.pagesize, this.countriesLoaded);
        },
        countriesLoaded(response) {
            this.countries = response.result;
            this.categoryId = this.countries[0].id;
            this.isloading = false;
        },
        loadSubjects() {
            apiservice.loadSubjects(this.categoryId, this.pagenumber, this.pagesize, this.subjectsLoaded);
        },
        subjectsLoaded(response) {
            this.subjects = response.result;
            this.isloading = false;
        },
        filteredGrades: function () {
            return this.grades.filter(g => g.categoryId == this.categoryId);
        }
    },
    created: function () {
        events.$emit('created', this);
    }
});