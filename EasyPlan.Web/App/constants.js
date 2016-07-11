define([], function(){
    return {
        storage: {
            host: 'http://localhost:52114/',
            boardsUrl: 'boards/get-data',
            setItemTitleUrl: 'boards/item/set-title',
            removeItemUrl: 'boards/item/remove',
            createNewItemUrl: 'boards/item/create',
        }
    }
});