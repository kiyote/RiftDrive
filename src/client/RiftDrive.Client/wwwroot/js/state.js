window.appState = {
    setItem: function (key, value) {
        window.sessionStorage.setItem(key, JSON.stringify(value));
    },

    getItem: function (key) {
        const item = window.sessionStorage.getItem(key);

        if (item) {
            return JSON.parse(item);
        }

        return null;
    }
};