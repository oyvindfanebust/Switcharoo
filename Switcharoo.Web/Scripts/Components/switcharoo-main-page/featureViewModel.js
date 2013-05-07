var environmentViewModel = require('./environmentViewModel');
function featureViewModel (data, mediator) {
	var id = data.id,
		created = data.created,
		app = data.app,
		feature = data.feature,
		environments = data.environments.map(environmentViewModel);

		environments.forEach(function (env) {
			env.on.subscribe(function (newValue) {
				mediator.publish('switch', feature, env.name(), newValue);
			});
		});
	return {
		id: id,
		created: created,
		app: app,
		feature: feature,
		environments: environments
	};
}

module.exports = featureViewModel;