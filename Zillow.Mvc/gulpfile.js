/// <binding AfterBuild='default' />
var gulp = require('gulp');

gulp.task('default', function (done) {
    gulp.src([
      'node_modules/angular/angular.min.js',
      'node_modules/angular-resource/angular-resource.min.js',
      'node_modules/angular-route/angular-route.min.js',
      'node_modules/ngmap/build/scripts/ng-map.min.js',
      'node_modules/es6-shim/es6-shim.min.js*',
      'node_modules/systemjs/dist/*.*',
      'node_modules/jquery/dist/jquery.*js',
      'node_modules/bootstrap/dist/js/bootstrap*.js'
    ]).pipe(gulp.dest('./wwwroot/libs/'));

    gulp.src([
      'node_modules/bootstrap/dist/css/bootstrap.css'
    ]).pipe(gulp.dest('./wwwroot/libs/css'));
});