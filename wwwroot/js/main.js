/* 
 * vanilla js here
 * pull request welcome
 */
'use strict';
var main = ((ctx) => {
    ctx.handleActiveRoutes = (routeInfoArr) => {
        var toRemove = Object.keys(routecache);
        var active = routeInfoArr.map(g => g.routeData.routeId);
        console.log(toRemove);
        console.log(active);
        // todo.
    };
    var routecache = {}; //route => board
    var
        cache = [],
        rts = [],
        trails = [],
        map,
        newTrail = (id) => {
            return {
                type: 'Feature',
                properties: {
                    id: id
                },
                geometry: {
                    type: 'LineString',
                    coordinates: []
                }
            };
        };
    ctx.telemetryArrived = (obj) => {
        if (!obj.board) return;
        var was = !!cache[obj.board];
        cache[obj.board] = cache[obj.board] || [];
        cache[obj.board].push(obj);
        if (!was) {
            initRealtime(obj.board);
            (routecache[obj.route] = routecache[obj.route] || []).push(obj.board);
        }
    };
    ctx.unsubs = (route) => {
        while ((board = routecache[route].pop())) ((v) => {
            rts[v].stop();
            map.removeLayer(rts[v]);
            cache[v] = undefined;
            rts[v] = undefined;
        })(board);
    };

    var initRealtime = (board) => {
        var realtime = rts[board] ||
            L.realtime(function (success, error) {
                var gg = cache[board].shift();
                if (!gg) { return; }
                var data =
                {
                    "geometry": { "type": "Point", "coordinates": [gg.longitude, gg.latitude] }, "type": "Feature",
                    "properties": {
                        "name": "Route " + board
                    }
                };
                (function (data) {
                    var trail = trails[board] || newTrail(board);
                    var trailCoords = trail.geometry.coordinates;
                    trailCoords.push(data.geometry.coordinates);
                    trailCoords.splice(0, Math.max(0, trailCoords.length - 10));
                    trails[board] = trail;
                    success({
                        type: 'FeatureCollection',
                        features: [data, trail]
                    });
                })(data);

            }, {
                interval: 250
            }).addTo(map);
        rts[board] = realtime;
    };
    ctx.initMap = () => {
        // Where you want to render the map.
        var element = document.getElementById('osm-map');

        // Create Leaflet map on map element.
        map = L.map(element, {

            fullscreenControl: {
                pseudoFullscreen: !false // if true, fullscreen to page width and height
            }
        });

        var vectorTileStyling = {

            water: {
                fill: true,
                weight: 1,
                fillColor: '#06cccc',
                color: '#06cccc',
                fillOpacity: 0.2,
                opacity: 0.4,
            },
            admin: {
                weight: 1,
                fillColor: 'pink',
                color: 'pink',
                fillOpacity: 0.2,
                opacity: 0.4
            },
            waterway: {
                weight: 1,
                fillColor: '#2375e0',
                color: '#2375e0',
                fillOpacity: 0.2,
                opacity: 0.4
            },
            landcover: {
                fill: true,
                weight: 1,
                fillColor: '#53e033',
                color: '#53e033',
                fillOpacity: 0.2,
                opacity: 0.4,
            },
            landuse: {
                fill: true,
                weight: 1,
                fillColor: '#e5b404',
                color: '#e5b404',
                fillOpacity: 0.2,
                opacity: 0.4
            },
            park: {
                fill: true,
                weight: 1,
                fillColor: '#84ea5b',
                color: '#84ea5b',
                fillOpacity: 0.2,
                opacity: 0.4
            },
            boundary: {
                weight: 1,
                fillColor: '#c545d3',
                color: '#c545d3',
                fillOpacity: 0.2,
                opacity: 0.4
            },
            aeroway: {
                weight: 1,
                fillColor: '#51aeb5',
                color: '#51aeb5',
                fillOpacity: 0.2,
                opacity: 0.4
            },
            road: {	// mapbox & nextzen only
                weight: 1,
                fillColor: '#f2b648',
                color: '#f2b648',
                fillOpacity: 0.2,
                opacity: 0.4
            },
            tunnel: {	// mapbox only
                weight: 0.5,
                fillColor: '#f2b648',
                color: '#f2b648',
                fillOpacity: 0.2,
                opacity: 0.4,
                // 					dashArray: [4, 4]
            },
            bridge: {	// mapbox only
                weight: 0.5,
                fillColor: '#f2b648',
                color: '#f2b648',
                fillOpacity: 0.2,
                opacity: 0.4,
                // 					dashArray: [4, 4]
            },
            transportation: {	// openmaptiles only
                weight: 0.5,
                fillColor: '#f2b648',
                color: '#f2b648',
                fillOpacity: 0.2,
                opacity: 0.4,
                // 					dashArray: [4, 4]
            },
            transit: {	// nextzen only
                weight: 0.5,
                fillColor: '#f2b648',
                color: '#f2b648',
                fillOpacity: 0.2,
                opacity: 0.4,
                // 					dashArray: [4, 4]
            },
            building: {
                fill: true,
                weight: 1,
                fillColor: '#2b2b2b',
                color: '#2b2b2b',
                fillOpacity: 0.2,
                opacity: 0.4
            },
            water_name: {
                weight: 1,
                fillColor: '#022c5b',
                color: '#022c5b',
                fillOpacity: 0.2,
                opacity: 0.4
            },
            transportation_name: {
                weight: 1,
                fillColor: '#bc6b38',
                color: '#bc6b38',
                fillOpacity: 0.2,
                opacity: 0.4
            },
            place: {
                weight: 1,
                fillColor: '#f20e93',
                color: '#f20e93',
                fillOpacity: 0.2,
                opacity: 0.4
            },
            housenumber: {
                weight: 1,
                fillColor: '#ef4c8b',
                color: '#ef4c8b',
                fillOpacity: 0.2,
                opacity: 0.4
            },
            poi: {
                weight: 1,
                fillColor: '#3bb50a',
                color: '#3bb50a',
                fillOpacity: 0.2,
                opacity: 0.4
            },
            earth: {	// nextzen only
                fill: true,
                weight: 1,
                fillColor: '#c0c0c0',
                color: '#c0c0c0',
                fillOpacity: 0.2,
                opacity: 0.4
            },


            // Do not symbolize some stuff for mapbox
            country_label: [],
            marine_label: [],
            state_label: [],
            place_label: [],
            waterway_label: [],
            poi_label: [],
            road_label: [],
            housenum_label: [],


            // Do not symbolize some stuff for openmaptiles
            country_name: [],
            marine_name: [],
            state_name: [],
            place_name: [],
            waterway_name: [],
            poi_name: [],
            road_name: [],
            housenum_name: [],
        };

        // Monkey-patch some properties for nextzen layer names, because
        // instead of "building" the data layer is called "buildings" and so on
        vectorTileStyling.buildings = vectorTileStyling.building;
        vectorTileStyling.boundaries = vectorTileStyling.boundary;
        vectorTileStyling.places = vectorTileStyling.place;
        vectorTileStyling.pois = vectorTileStyling.poi;
        vectorTileStyling.roads = vectorTileStyling.road;



        // Add OSM tile leayer to the Leaflet map.
        var osmLayer = L.tileLayer('http://{s}.tile.osm.org/{z}/{x}/{y}.png', {
            attribution: '&copy; <a href="https://osm.org/copyright">OpenStreetMap</a> contributors'
        }).addTo(map);

        var esriTilesUrl = "https://map.md/api/tiles/data/v3/{z}/{x}/{y}.pbf?v=2019-12-27T09:44:24Z";

        var esriVectorTileOptions = {
            rendererFactory: L.canvas.tile,
            attribution: '© Point',
            vectorTileLayerStyles: vectorTileStyling,
        };

        var pointMap = L.vectorGrid.protobuf(esriTilesUrl, esriVectorTileOptions);
        var chisinau = ['47.0255351', '28.8218434'];

        var target = L.latLng(chisinau);

        map.setView(target, 14);

        L.control.layers({
            "Openstreet": osmLayer,
            "Map.md": pointMap
        }, {}, { collapsed: false }).addTo(map);

    };

    return ctx;
})(window);