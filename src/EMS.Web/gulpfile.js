var gulp = require('gulp'),
    flatten = require('gulp-flatten'),
    sass = require('gulp-sass'),
    typescript = require('gulp-typescript'),
    sourcemaps = require('gulp-sourcemaps'),
    rimraf = require('rimraf'),
    merge2 = require('merge2');

// Initialize directory paths.
var paths = {
    // Source Directory Paths
    node: "./node_modules/",
    scripts: "Scripts/",
    styles: "Styles/",
    webroot: "./wwwroot",
    images: "Images/"
}

// Destination Directory Paths
paths.wwwroot = paths.webroot + "/";
paths.css = paths.webroot + "/css/";
paths.fonts = paths.webroot + "/fonts/";
paths.img = paths.webroot + "/img/";
paths.js = paths.webroot + "/js/";
paths.lib = paths.webroot + "/lib/";

gulp.task("clean-lib", function (cb) {
    rimraf(paths.lib, cb);
});

gulp.task("copy-lib", ['clean-lib'], function () {
    var merge = new merge2();
    var node_modules = {
        "bootstrap": "bootstrap/dist/**/bootstrap*.{js,map,css}",
        "jquery": "jquery/dist/jquery*.{js,map}",
        "jquery-validation": "jquery-validation/dist/jquery.validate.{js,map}",
        "jquery-validation-unobtrusive": "jquery-validation-unobtrusive/jquery.validate.unobtrusive.{js,map}",
        "moment": "moment/moment.js",
        'react': 'react/dist/react*.{min.js,js}',
        'react-dom': 'react-dom/dist/react-dom.{min.js,js}',
        'systemjs': 'systemjs/dist/system*.{js,map}',
        'fetch': 'whatwg-fetch/fetch.js',
        'marked': 'marked/lib/marked.js',
        'vue': 'vue/dist/vue.js',
        'axios': 'axios/dist/*.{js,map}',
        'lodash': 'lodash/lodash*.js',
        'marked': 'marked/lib/*.js'
    }

    for (var destinationDir in node_modules) {
        merge.add(gulp.src(paths.node + node_modules[destinationDir])
            .pipe(gulp.dest(paths.lib + destinationDir)));
    }

    return merge;
});

gulp.task('clean-images', function (cb) {
    rimraf(paths.img, cb);
});

gulp.task("copy-images", ['clean-images'], function () {
    return gulp.src(paths.images + '**/*.{png,jpg,ico,gif}')
        .pipe(gulp.dest(paths.img));
});

gulp.task('clean-css', function (cb) {
    rimraf(paths.css, cb);
});

gulp.task('sass', ['clean-css'], function () {
    // get the files from the root
    return gulp.src(paths.styles + '**/*.scss')
        .pipe(sass().on('error', sass.logError))
        .pipe(gulp.dest(paths.css));
});