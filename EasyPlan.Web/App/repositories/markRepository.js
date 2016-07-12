define(['constants', 'storageContext', 'http/storageHttpWrapper', 'mappers/itemMapper'],
    function (constants, storage, storageHttpWrapper, itemMapper) {

        return {
            setValue
        }

        function setValue(value, markId, boardId){
            return storageHttpWrapper.post(constants.storage.host + constants.storage.setMarkValueUrl, {value: value, id: markId}).then(function () {
                var board = storage.boards.find(function (elem) { return elem.id === boardId });
                var items = board.items;
                var mark;

                for (var i = 0; i < items.length; i++)
                {
                    var marks = items[i].marks;

                    for(var j = 0; j < marks.length; j++)
                    {
                        if(marks[j].id == markId)
                        {
                            marks[j].value = value;
                            return;
                        }
                    }
                }
            });
        }
    });