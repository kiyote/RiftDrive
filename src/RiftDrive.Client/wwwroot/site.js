
window.profile = {
    readUploadedFileAsText: function (inputFile) {
        const temporaryFileReader = new FileReader();
        return new Promise((resolve, reject) => {
            temporaryFileReader.onerror = () => {
                temporaryFileReader.abort();
                reject(new DOMException("Problem parsing input file."));
            };
            temporaryFileReader.onload = function (event) {
                resolve(event.target.result);
            };

            temporaryFileReader.readAsDataURL(inputFile.files[0]);
        });
    }
};

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

window.dragDrop = {

    registerDraggable: function (id, csElement) {
        $("#" + id).on("dragstart", function (e) {
            var ev = e.originalEvent;
            ev.dataTransfer.setData("text/plain", ev.target.id);
            ev.dataTransfer.dropEffect = "move";
            csElement.invokeMethodAsync("OnDragStarted");
        });
        $("#" + id).on("dragend", function (e) {
            csElement.invokeMethodAsync("OnDragEnded");
        });
    },

    unregisterDraggable: function (id) {
        $("#" + id).off("dragstart");
        $("#" + id).off("dragend");
    },

    registerDroppable: function (id, csElement) {
        $("#" + id).on("dragover", function (e) {
            e.preventDefault();
        });
        $("#" + id).on("drop", function (e) {
            var ev = e.originalEvent;
            ev.preventDefault();
            var data = ev.dataTransfer.getData("text/plain");
            csElement.invokeMethodAsync("OnDropped", data);
        });
    },

    unregisterDroppable: function (id) {
        $("#" + id).off("dragover");
        $("#" + id).off("drop");
    }
};