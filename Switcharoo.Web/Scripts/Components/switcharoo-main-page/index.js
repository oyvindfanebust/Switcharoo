var domify = require('domify');
var template = require('./template');
var ko = require('knockout')();

module.exports = function (data) {
	var page = domify(template)[0];

	function render (node) {
		node.appendChild(page);
		ko.applyBindings(data, node);
	}
	return { render: render };
};