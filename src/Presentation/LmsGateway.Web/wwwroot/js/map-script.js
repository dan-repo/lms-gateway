$(document).ready(function(){

(function(){

	var map;
	map = new GMaps({
		el: '#map',
		lat: 6.3096503,
		lng: 7.5369514,

		//lat: 6.309565,
		//lng: 7.53708,
		scrollwheel:false,
		zoom: 15,
		zoomControl : false,
		panControl : false,
		streetViewControl : true,
		mapTypeControl: false,
		overviewMapControl: false,
		clickable: false
	});

	var image = '../images/map.png';
	map.addMarker({
	    lat: 6.3096503,
	    lng: 7.5369514,

	    //lat: 6.309565,
	    //lng: 7.53708,
		icon: image,
		animation: google.maps.Animation.DROP,
		verticalAlign: 'bottom',
		horizontalAlign: 'left',
		backgroundColor: '#efece0',
	});

	var styles = [

		{
			"featureType": "road",
			"stylers": [
				{ "color": "#ffffff" }
			]
		},{
			"featureType": "water",
			"stylers": [
				{ "color": "#bde5f6" }
			]
		 },{
		  "featureType": "landscape",
			 "stylers": [
			 { "color": "#faf5e8" }
			 ]
		},{
			"elementType": "labels.text.fill",
			"stylers": [
				{ "color": "#999" }
			]
		},{
			"featureType": "poi",
			"stylers": [
			 { "color": "#aaa" }
			]
		},{
			"elementType": "labels.text",
			"stylers": [
				{ "saturation": 1 },
				{ "weight": 0.1 },
				{ "color": "#a8a8a8" }
			]
		}

	];

	 map.addStyle({
		styledMapName:"Styled Map",
		styles: styles,
		mapTypeId: "map_style"
	});

	map.setStyle("map_style");
}());

});