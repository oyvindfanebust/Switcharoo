var featureViewModel = require('./featureViewModel');

function featuresViewModel (data) {
	var models = data.features.map(featureViewModel);
	var features = ko.observableArray(models);

	return { features: features };
}
module.exports = featuresViewModel;