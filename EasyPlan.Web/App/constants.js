define([], function(){
    return {
        storage: {
            boardsInfoUrl: 'boards/get-info',
            boardDataUrl: 'boards/get-data',
            setItemTitleUrl: 'boards/item/set-title',
            removeItemUrl: 'boards/item/remove',
            createNewItemUrl: 'boards/item/create',
            setMarkValueUrl: 'boards/mark/set-value',
            createMarkUrl: 'boards/mark/create'
        },
        popupTemplatesId: {
            confirmation: "#confirmation-popup-body-template"
        }
    }
});