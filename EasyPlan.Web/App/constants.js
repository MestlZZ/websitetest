define([], function(){
    return {
        storage: {
            boardsInfoUrl: 'board/GetBoardsInfo',
            boardDataUrl: 'board/GetBoardData',
            setItemTitleUrl: 'board/item/SetItemTitle',
            removeItemUrl: 'board/item/RemoveItem',
            createNewItemUrl: 'board/item/CreateItem',
            setMarkValueUrl: 'board/mark/SetMarkValue',
            createMarkUrl: 'board/mark/CreateMark',
            setCriterionWeightUrl: 'board/criterion/SetCriterionWeight',
            setCriterionTitleUrl: 'board/criterion/SetCriterionTitle',
            removeCriterionUrl: 'board/criterion/RemoveCriterion',
            createNewCriterionUrl: 'board/criterion/CreateCriterion',
            currentUserUrl: 'account/GetUserData',
            boardCreateUrl: 'board/Create',
            boardSetTitleUrl: 'board/SetTitle',
            boardRemoveUrl: 'board/Remove'
        },
        popupTemplatesId: {
            confirmation: "#confirmation-popup-body-template"
        }
    }
});