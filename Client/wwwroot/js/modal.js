$(document).ready(function () {
	$('#imageModal').on('show.bs.modal', function (event) {
		var image = $(event.relatedTarget);
		var fullImageSrc = image.data('full-image');
		var modalImage = $('#modalImage');
		modalImage.attr('src', fullImageSrc);
	});
});