import http from './http.js';

var store = new DevExpress.data.CustomStore({
    load: function (loadOptions) {

        return new Promise(resolve => {
            return http('admin/matchs-all').asGet().then(data => {
                resolve(data);
            });
        });       

    },
    insert: (data) => {

        return new Promise(resolve =>
            http('admin/matchs-insert').asPost(data).then(result => {
                DevExpress.ui.notify('data added');
                resolve(result);
            })
        )
    },
    update: (data, dataModificada) => {

        return new Promise(resolve =>
            http('admin/matchs-update').asPost({...data, ...dataModificada }).then(result => {
                DevExpress.ui.notify('data updated');
                resolve(result);
            })

        )
    },
    remove: match => {
        return new Promise(resolve =>
            http('admin/matchs-remove/' + match.id).asGet().then(result => {
                DevExpress.ui.notify('date removed', 'error');
                resolve(result);
            })
        )
    },
});

$(function () {
    let teams = []
    http('admin/teams-all').asGet().then(teams => {

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
                    height: 550,
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
                        items: ["teamHomeId", "teamAwayId", "eventDate","priority",'isLive', {
                            dataField: "url",
                            editorType: "dxTextArea",
                            colSpan: 1,
                            editorOptions: {
                                height: 150
                            }
                        }]
                    }]
                }
            },
            columns: [

                {
                    dataField: "teamHomeId",
                    caption: "Equipo de Casa",
                    lookup: {
                        dataSource: teams,
                        displayExpr: "name",
                        valueExpr: "id"
                    }
                },
                {
                    dataField: "teamAwayId",
                    caption: "Visitante",
                    lookup: {
                        dataSource: teams,
                        displayExpr: "name",
                        valueExpr: "id"
                    }
                },
                {
                    dataField: "eventDate",
                    caption: "Fecha",
                    width: 150,
                    dataType: "datetime"
                },               
                {
                    dataField: "priority",
                    dataType: "number"
                },
                {
                    dataField: "isLive",
                    dataType: "boolean"
                },
                {
                    dataField: "url",
                },
            ]
        });
    });
});