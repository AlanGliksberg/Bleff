
var simpleModalHtml = '<div class="modal" tabindex="-1" role="dialog">\
    <div class="modal-dialog" role="document">\
        <div class="modal-content">\
            <div class="modal-header">\
                <h5 class="modal-title">%modal-title%</h5>\
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">\
                    <span aria-hidden="true">&times;</span>\
                </button>\
            </div >\
            <div class="modal-body">\
                <p>%modal-message%</p>\
            </div>\
            <div class="modal-footer">\
                <button type="button" class="btn btn-primary" data-dismiss="modal">OK</button>\
            </div>\
        </div >\
    </div>\
</div>';

function showSimpleModal(title, message) {
    var modal = $(simpleModalHtml.replace("%modal-title%", title).replace("%modal-message%", message));

    modal.modal("show");
}