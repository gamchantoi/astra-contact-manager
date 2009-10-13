/*!
* Ext JS Library 3.0.0
* Copyright(c) 2006-2009 Ext JS, LLC
* licensing@extjs.com
* http://www.extjs.com/license
*/
Ext.onReady(function() {

    // create the Data Store
    var store = new Ext.data.JsonStore({
        root: 'Clients',
        //totalProperty: 'totalCount',
        idProperty: 'UserId',
        //remoteSort: true,

        fields: ['UserId', 'UserName', 'Role', 'FullName', 'ProfileName', 'Balance', 'Status'],


        //        fields: [
        //            'title', 'forumtitle', 'forumid', 'author',
        //            { name: 'replycount', type: 'int' },
        //            { name: 'lastpost', mapping: 'lastpost', type: 'date', dateFormat: 'timestamp' },
        //            'lastposter', 'excerpt'
        //        ],

        // load using script tags for cross domain, if the data in on the same domain as
        // this page, an HttpProxy would be better
        proxy: new Ext.data.ScriptTagProxy({ url: 'User/IndexAJAX'})
    });
    //store.setDefaultSort('lastpost', 'desc');


    // pluggable renders
    //    function renderTopic(value, p, record) {
    //        return String.format(
    //                '<b><a href="http://extjs.com/forum/showthread.php?t={2}" target="_blank">{0}</a></b><a href="http://extjs.com/forum/forumdisplay.php?f={3}" target="_blank">{1} Forum</a>',
    //                value, record.data.forumtitle, record.id, record.data.forumid);
    //    }
    //    function renderLast(value, p, r) {
    //        return String.format('{0}<br/>by {1}', value.dateFormat('M j, Y, g:i a'), r.data['lastposter']);
    //    }

    var grid = new Ext.grid.GridPanel({
        width: 900,
        height: 500,
        title: 'Client List',
        store: store,
        trackMouseOver: false,
        disableSelection: true,
        loadMask: true,
        renderTo: 'topic-grid',
        sm: new Ext.grid.RowSelectionModel({ singleSelect: true }),


        // grid columns
        columns: [{
            //id: 'UserName', // id assigned so we can apply custom css (e.g. .x-grid-col-topic b { color:#333 })
            header: "Name",
            dataIndex: 'UserName',
            width: 150,
            //renderer: renderTopic,
            sortable: true
        }, {
            header: "Role",
            dataIndex: 'Role',
            width: 150,
            //hidden: true,
            sortable: true
        }, {
            header: "FullName",
            dataIndex: 'FullName',
            width: 150,
            //hidden: true,
            sortable: true
        }, {
            header: "ProfileName",
            dataIndex: 'ProfileName',
            width: 150,
            //hidden: true,
            sortable: true
        }, {
            header: "Balance",
            dataIndex: 'Balance',
            width: 150,
            //hidden: true,
            sortable: true
        }, {
            header: "Status",
            dataIndex: 'Status',
            width: 140,
            //hidden: true,
            sortable: true
}]
        });

        // render it
        //grid.render('topic-grid');

        // trigger the data store load
        store.load({ params: { start: 0, limit: 25} });
    });
