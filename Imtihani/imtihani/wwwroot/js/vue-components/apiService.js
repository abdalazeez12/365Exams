var apiservice = {
    connectApi: function (method, api, data, success, err) {
        switch (method) {
            case 'get':
                axios.get(vue.apiurl + api, { headers: { 'Lang': 'en' } })
                    .then(response => {
                        success(response.data);
                    })
                    .catch(error => {
                        if (err)
                            err(error);
                        return console.log(error);
                    });
                break;
            case 'post':
                axios.post(vue.apiurl + api, data, { headers: { 'Lang': 'en' } },)
                    .then(response => {
                        success(response.data);
                    })
                    .catch(error => {
                        if (err)
                            err(error);
                        return console.log(error);
                    });
                break;
        }
    },
    loadAssessments: function (systemid, gradeId, subjectid, minprice, maxprice, pagenumber, pagesize, keyword, sorting, success, error) {
        this.connectApi('get', "courses/Assessments/List?SystemId=" + systemid + "&gradeId=" + gradeId + "&SubjectId=" + subjectid + "&MinPrice=" + minprice + "&MaxPrice=" + maxprice + "&PageNumber=" + pagenumber + "&PageSize=" + pagesize + "&keyword=" + keyword + "&sortBy=" + sorting, {},
            function (res) {
                for (i = 0; i < res.length; i++) {
                    res[i].ImagePath.FullName = res[i].ImagePath.FullName.replace("~/", vue.baseurl);
                };
                console.log("sys id= "+systemid);
                console.log("grd id= "+gradeId);
                console.log("sub id= "+subjectid);
                console.log("min p= "+minprice);
                console.log("max p= "+maxprice);
                console.log("pag num= "+pagenumber);
                console.log("page s= "+pagesize);
                console.log("key=" + keyword);
                console.log("sorting=" + sorting);

                success(res);
            }, error);
    },
    loadCountries: function (pagenumber, pagesize, success, error) {
        this.connectApi('get', "core/Categories/List?module=Country&objectType=Assessment&parentId=-7&PageNumber=" + pagenumber + "&PageSize=" + pagesize, {},
            function (res) {
                for (i = 0; i < res.result.length; i++) {
                    res.result[i].imagePath.fullName = res.result[i].imagePath.fullName.replace("~/", vue.baseurl);
                };
                res.result = res.result.sort((a, b) => a.rank - b.rank);

                success(res);
            }, error);
    },
    loadGrades: function (categoryid, pagenumber, pagesize, success, error) {
        this.connectApi('get', 'courses/Grades/List?CategoryId=' + categoryid + "&PageNumber=" + pagenumber + "&PageSize=" + pagesize, {}, success, error);
    },
    loadSubjects: function (categoryid, pagenumber, pagesize, success, error) {
        this.connectApi('get', "courses/Subjects/List?PageNumber=" + pagenumber + "&PageSize=" + pagesize, {}, success, error);
    },

}