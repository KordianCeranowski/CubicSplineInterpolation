// Load the Visualization API and the columnchart package.
google.load('visualization', '1', {packages: ['columnchart']});

function initMap() {
  // The following path marks a path from Mt. Whitney, the highest point in the
  // continental United States to Badwater, Death Valley, the lowest point.
  var path = [
      {lat: 53.65, lng: 17.36}, // Czułchów
      {lat: 54.42, lng: 18.57}  // Tfu Gdańsk
      ];

  // Create an ElevationService.
  var elevator = new google.maps.ElevationService;

  // Draw the path, using the Visualization API and the Elevation service.
  displayPathElevation(path, elevator, map);
}

function displayPathElevation(path, elevator, map) {


  // Create a PathElevationRequest object using this array.
  // Ask for 256 samples along that path.
  // Initiate the path request.
  elevator.getElevationAlongPath({
    'path': path,
    'samples': 256
  }, plotElevation);
}

// Takes an array of ElevationResult objects, draws the path on the map
// and plots the elevation profile on a Visualization API ColumnChart.
function plotElevation(elevations, status) {
  var chartDiv = document.getElementById('elevation_chart');

  // Create a new chart in the elevation_chart DIV.
  var chart = new google.visualization.ColumnChart(chartDiv);

  // Extract the data from which to populate the chart.
  // Because the samples are equidistant, the 'Sample'
  // column here does double duty as distance along the
  // X axis.
  var data = new google.visualization.DataTable();
  data.addColumn('string', 'Sample');
  data.addColumn('number', 'Elevation');
  
  var dane = document.getElementById('moje_dane');
  
  var lastLat = elevations[0].location.lat();
  var lastLng = elevations[0].location.lng();
  
  var totalDistance = 0;
  
  var KM_IN_DEGREE = 111.32;
  
  dane.innerHTML += 'total_distance_in_km, elevation<br/>'
  for (var i = 0; i < elevations.length; i++) {

		var currLat = elevations[i].location.lat();
    var diffLat = Math.abs(currLat - lastLat);
    lastLat = currLat;
    
    var currLng = elevations[i].location.lng();
    var diffLng = Math.abs(currLng - lastLng);
    lastLng = currLng;
    
    var diffOverall = Math.sqrt(Math.pow(diffLat, 2) + Math.pow(diffLng, 2));
    
    totalDistance += diffOverall;
    pyszne.innerHTML += totalDistance * KM_IN_DEGREE + ';'
   
    pyszne.innerHTML += elevations[i].elevation
    pyszne.innerHTML += '<br/>'
  }
}