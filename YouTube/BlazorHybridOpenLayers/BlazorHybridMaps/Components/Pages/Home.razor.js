export function showMessage(message) {
    confirm(message);
}

//DotNet.invokeMethodAsync('BlazorHybridMaps', 'GetStringMessage')
//    .then(data => {
//        confirm(data);
//    });

export function testJStoNet(messageBuilder) {
    return messageBuilder.invokeMethodAsync('getStringMessage').then(data => {
            confirm(data);
        });
}

export function loadMap(id) {
    const map = new ol.Map({
        target: id,
        layers: [
            new ol.layer.Tile({
                source: new ol.source.OSM(),
            }),
        ],
        view: new ol.View({
            center: [0, 0],
            zoom: 2
        })
    });
}

