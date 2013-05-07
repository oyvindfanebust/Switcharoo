var domify = require('domify');
var ko = require('knockout')();
var template = require('./template');
var viewModel = require('./featuresViewModel');

module.exports = function (data, mediator) {
	var page = domify(template)[0];
	
	mediator.subscribe('switch', function (feature, env, isOn) {
		console.log('Setting feature ' + feature + ' in environment ' + env + ' to "' + isOn + '"!');
	});

	function render (node) {
		node.appendChild(page);
		ko.applyBindings(viewModel(data, mediator), node);
	}
	return { render: render };
};