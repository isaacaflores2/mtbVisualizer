// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var pinRadius = 10
var pinColor = 'rgba(0, 102, 0, 0.5)'
var pinSvg = ['<svg xmlns="http://www.w3.org/2000/svg" width="', (pinRadius * 2), '" height="', (pinRadius * 2), '">',
    '<circle cx="', pinRadius, '" cy="', pinRadius, '" r="', pinRadius, '" fill="', pinColor, '"/>',
    '<circle cx="', pinRadius, '" cy="', pinRadius, '" r="', pinRadius - 7, '" fill="', pinColor, '"/>',
    '</svg>'];

function createCustomClusteredPin(cluster) {
    //Define variables for minimum cluster radius, and how wide the outline area of the circle should be.
    var minRadius = 10;
    var outlineWidth = 7;

    //Get the number of pushpins in the cluster
    var clusterSize = cluster.containedPushpins.length;

    //Calculate the radius of the cluster based on the number of pushpins in the cluster, using a logarithmic scale.
    var radius = Math.log(clusterSize) / Math.log(10) * 3 + minRadius;

    //Default cluster color is red.
    var fillColor = 'rgba(255, 40, 40, 0.5)';

    if (clusterSize < 10) {
        //Make the cluster green if there are less than 10 pushpins in it.
        fillColor = 'rgba(0, 102, 0, 0.5)';
    } else if (clusterSize < 100) {
        //Make the cluster yellow if there are 10 to 99 pushpins in it.
        fillColor = 'rgba(255, 210, 40, 0.5)';
    }

    //Create an SVG string of two circles, one on top of the other, with the specified radius and color.
    var svg = ['<svg xmlns="http://www.w3.org/2000/svg" width="', (radius * 2), '" height="', (radius * 2), '">',
        '<circle cx="', radius, '" cy="', radius, '" r="', radius, '" fill="', fillColor, '"/>',
        '<circle cx="', radius, '" cy="', radius, '" r="', radius - outlineWidth, '" fill="', fillColor, '"/>',
        '</svg>'];

    //Customize the clustered pushpin using the generated SVG and anchor on its center.
    cluster.setOptions({
        icon: svg.join(''),
        anchor: new Microsoft.Maps.Point(radius, radius),
        textOffset: new Microsoft.Maps.Point(0, radius - 8) //Subtract 8 to compensate for height of text.
    });
}

var mapElementStyle = {
        area: { fillColor: '#b6e591' },
        water: { fillColor: '#148cb8' },
        tollRoad: { fillColor: '#6f0fd7', strokeColor: '#a964f4' },
        arterialRoad: { fillColor: '#707aa9', strokeColor: '#707aa9' },
        road: { fillColor: '#ff7300', strokeColor: '#ff9c4f' },
        street: { fillColor: '#ffffff', strokeColor: '#ffffff' },
        transit: { fillColor: '#000000' }
}
var mapLandColor =  '#dbcebd'    
    