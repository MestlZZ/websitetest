define(['constants', 'http/storageHttpWrapper'],
    function (constants, storageHttpWrapper) {

        return {
            getCurrentUser
        }

        function getCurrentUser() {
            return storageHttpWrapper.post(constants.storage.currentUserUrl);
        }
    });