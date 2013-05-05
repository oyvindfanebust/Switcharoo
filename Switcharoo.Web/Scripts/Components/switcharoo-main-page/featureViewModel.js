function featureViewModel (data) {
	var id = data.id,
		created = data.created,
		app = data.app,
		feature = data.feature,
		environments = data.environments;
		
	return { 
		id: id,
		created: created,
		app: app,
		feature: feature,
		environments: environments
	};
}

module.exports = featureViewModel;