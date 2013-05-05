var domify = require('domify');
var ko = require('knockout')();
var template = require('./template');
var viewModel = require('./featuresViewModel');

module.exports = function (data, postbox) {
	var page = domify(template)[0];

	function render (node) {
		node.appendChild(page);
		ko.applyBindings(viewModel(data), node);
	}
	return { render: render };
};