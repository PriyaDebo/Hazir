function ready() {
	if (document.readyState == 'complete') {
		Webcam.set({
			width: 920,
			height: 720,
			image_format: 'jpeg',
			jpeg_quality: 180
		});
		try {
			Webcam.attach('#camera');
		} catch (e) {
			alert(e);
		}
	}
}

function take_snapshot() {
	// take snapshot and get image data
	var dataURI = null;
	Webcam.snap(function (data_uri) {

		dataURI = data_uri;
	});
	return dataURI;
}