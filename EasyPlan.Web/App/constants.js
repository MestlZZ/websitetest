define([], function(){
    return {
        storage: {
            host: 'http://localhost:52114/',
            boardsInfoUrl: 'boards/get-info',
            boardDataUrl: 'boards/get-data',
            setItemTitleUrl: 'boards/item/set-title',
            removeItemUrl: 'boards/item/remove',
            createNewItemUrl: 'boards/item/create',
            setMarkValueUrl: 'boards/mark/set-value'
        },
        popupTemplatesId: {
            confirmation: "#confirmation-popup-template"
        }
    }
});