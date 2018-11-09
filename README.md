# FindNearestPlaces
Find nearest N places by DbGeography or only use Latitude &amp; Longitude. we tries this to find the best. It's a kind of spike.

1. Distance Between Two Given Points:

This uses the ‘haversine’ formula to calculate the great-circle distance between two points – that is,  the shortest distance over the earth’s surface – giving an ‘as-the-crow-flies’ distance between the points (ignoring any hills they fly over, of course!).

Haversine formula:

a = sin²(Δφ/2) + cos φ1 ⋅ cos φ2 ⋅ sin²(Δλ/2)

c = 2 ⋅ atan2( √a, √(1−a) )

d = R ⋅ c

where, φ is latitude, λ is longitude, R is earth’s radius (mean radius = 6,371km);

note that angles need to be in radians to pass to trig functions!

Real life implementation: Spherical Law of Cosines:
This makes the simpler law of cosines a reasonable 1-line alternative to the haversine formula for many geodesy purposes (if not for astronomy). The choice may be driven by programming language, 
processor, coding context, available trig func­tions (in different languages), etc – and, for very small distances an equirectangular approxima­tion may be more suitable.

Law of cosines: d = acos( sin φ1 ⋅ sin φ2 + cos φ1 ⋅ cos φ2 ⋅ cos Δλ ) ⋅ R

JavaScript: 
var φ1 = lat1.toRadians(), φ2 = lat2.toRadians(), Δλ = (lon2-lon1).toRadians(), R = 6371e3; // gives d in metres

var d = Math.acos( Math.sin(φ1)*Math.sin(φ2) + Math.cos(φ1)*Math.cos(φ2) * Math.cos(Δλ) ) * R;

Excel: =ACOS( SIN(lat1)*SIN(lat2) + COS(lat1)*COS(lat2)*COS(lon2-lon1) ) * 6371000

(or with lat/lon in degrees): =ACOS( SIN(lat1*PI()/180)*SIN(lat2*PI()/180) + COS(lat1*PI()/180)*COS(lat2*PI()/180)*COS(lon2*PI()/180-lon1*PI()/180) ) * 6371000

.............
read more:
https://knowledgesharetech.wordpress.com/2018/11/08/find-nearest-n-places-spike/
