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
            boardRemoveUrl: 'board/Remove',
            boardInviteUrl: 'board/InviteUser',
            getBoardUsersInfoUrl: 'board/GetBoardUserInfo',
            removeUserFromBoardUrl: 'board/RemoveUser'
        },
        popupTemplatesId: {
            confirmation: "#confirmation-popup-body-template"
        },
        EVENT:{
            BOARD: {
                ITEM: {
                    TITLE_CHANGED: 'board_item:title',
                    REMOVED: 'board_item:remove',
                    ADDED: 'board_item:add'
                },
                CRITERION: {
                    TITLE_CHANGED: 'board_criterion:title',
                    WEIGHT_CHANGED: 'board_criterion:weight',
                    REMOVED: 'board_criterion:remove',
                    ADDED: 'board_criterion:add'
                },
                MARK: {
                    VALUE_CHANGED: 'board_mark:value',
                    ADDED: 'board_mark:add'
                },
                TITLE_CHANGED: 'board:title'
            }
        },
        ROLE: {
            ADMIN: 1,
            EDITOR: 2,
            VIEWER: 3
        }
    }
});