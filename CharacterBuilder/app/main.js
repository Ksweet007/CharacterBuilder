/* global jQuery:false, $:false, ko:false, require:false, requirejs:false, moment:false */
define('jquery', function () { return jQuery; });
define('knockout', ko);
define('moment', function () { return moment; });

requirejs.config({
    paths: {
        'text': '../lib/require/text',
        'durandal': '../lib/durandal/js',
        'plugins': '../lib/durandal/js/plugins',
        'transitions': '../lib/durandal/js/transitions',
        'bootstrap': '../lib/bootstrap/js/bootstrap',
        'propeller': '../lib/propeller/'
    }
});

define(['durandal/system', 'durandal/app', 'durandal/viewLocator', 'knockout', '_custom/all'], function (system, app, viewLocator, ko, all) {
    //>>excludeStart("build", true);
    system.debug(true);
    //>>excludeEnd("build");

    requirejs.config(require.config);
    window.require = require;
    window.requirejs = requirejs;
    window.define = define;

    ko.punches.enableAll();

    app.title = 'Character Builder';

    app.configurePlugins({
        router: true,
        dialog: true,
        widget: {
            kinds: ['expander']
        }
    });

    app.start().then(function () {
        app.setRoot('shell/shell');
    });
});