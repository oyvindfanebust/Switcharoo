function featuresViewModel (data) {
	var features = ko.observableArray(data.features);
	return { features: features };
}
module.exports = featuresViewModel;