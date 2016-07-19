define([], function(){
    return {
        storage: {
            boardsInfoUrl: 'boards/get-info',
            boardDataUrl: 'boards/get-data',
            setItemTitleUrl: 'boards/item/set-title',
            removeItemUrl: 'boards/item/remove',
            createNewItemUrl: 'boards/item/create',
            setMarkValueUrl: 'boards/mark/set-value',
            createMarkUrl: 'boards/mark/create',
            setCriterionWeightUrl: 'boards/criterion/set-weight',
            setCriterionTitleUrl: 'boards/criterion/set-title',
            removeCriterionUrl: 'boards/criterion/remove',
            createNewCriterionUrl: 'boards/criterion/create'
        },
        popupTemplatesId: {
            confirmation: "#confirmation-popup-body-template"
        }
    }
});