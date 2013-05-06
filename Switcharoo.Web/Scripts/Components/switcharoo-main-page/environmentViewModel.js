function environmentViewModel (data) {
	var on = ko.observable(data.on),
		name = ko.observable(data.name);

	return {
		on: on,
		name: name
	};
}
module.exports = environmentViewModel;