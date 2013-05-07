var featureViewModel = require('./featureViewModel');

function featuresViewModel (data, mediator) {
	var models = data.features.map(function (element) {
		return featureViewModel(element, mediator);
	});
	var features = ko.observableArray(models);

	return { features: features };
}
module.exports = featuresViewModel;