import http from './http.js';

var store = new DevExpress.data.CustomStore({
    load: function (loadOptions) {

        return new Promise(resolve => {
            return http('admin/teams-all').asGet().then(data => {
                resolve(data);
            });
        });

    },
    insert: (data) => {
        return new Promise(resolve =>
            http('admin/teams-insert').asPost(data).then(result => {
                DevExpress.ui.notify('data added');
                resolve(result);
            })
        )
    },
    update: (data, dataModificada) => {

        return new Promise(resolve =>
            http('admin/teams-update').asPost({ ...data, ...dataModificada }).then(result => {
                DevExpress.ui.notify('data updated');
                resolve(result);
            })

        )
    },
    remove: match => {
        return new Promise(resolve =>
            http('admin/teams-remove/' + match.id).asGet().then(result => {
                DevExpress.ui.notify('date removed', 'error');
                resolve(result);
            })
        )
    },
});

$(function () {
    let teams = []

    $("#grid").dxDataGrid({
        dataSource: store,
        keyExpr: "id",
        showBorders: true,
        editing: {
            mode: "popup",
            allowUpdating: true,
            allowDeleting: true,
            allowAdding: true,
            popup: {
                title: "Employee Info",
                showTitle: true,
                width: 400,
                height: 500,
                position: {
                    my: "top",
                    at: "top",
                    of: window
                }
            },
            form: {
                items: [{
                    itemType: "group",
                    colCount: 1,
                    colSpan: 2,
                    items: ["name", "imageName"]
                }]
            }
        },
        columns: ['name','imageName']
    });

});